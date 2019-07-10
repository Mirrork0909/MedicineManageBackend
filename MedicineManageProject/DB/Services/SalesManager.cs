using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using MedicineManageProject.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class SalesManager:DBContext
    {
        public List<SaleInformationDTO> getSaleRecords()
        {
            var temp = Db.Queryable<SALE_RECORD>().OrderBy(it => it.SALE_DATE, OrderByType.Desc);
            List<SALE_RECORD> salesList = temp.Take(10).ToList();
            List<SaleInformationDTO> saleInformationDTO = new List<SaleInformationDTO>();
            foreach(SALE_RECORD sale in salesList)
            {
                SaleInformationDTO tempSale = new SaleInformationDTO();
                tempSale._customer_id = sale.CUSTOMER_ID;
                tempSale._sale_date = sale.SALE_DATE;
                tempSale._sale_id = sale.SALE_ID;
                tempSale._sale_price = sale.SALE_PRICE;
                tempSale._staff_id = sale.STAFF_ID;
                saleInformationDTO.Add(tempSale);
            }
            return saleInformationDTO;
        }
        public List<SalesDataByMonthDTO> getSalesAmountByMonth()
        {
            //得到包含年，月，钱的列表
            var list = Db.Queryable<SALE_RECORD>().
                Select(it => new
                {
                    _year = it.SALE_DATE.Year,
                    _month = it.SALE_DATE.Month,
                    _amount = it.SALE_PRICE
                }).ToList();
            List<SalesDataByMonthDTO> salesDatas = new List<SalesDataByMonthDTO>();
            foreach (var e in list)
            {
                SalesDataByMonthDTO sales = new SalesDataByMonthDTO { _year = e._year, _month = e._month, _amount = e._amount };
                salesDatas.Add(sales);
            }
            //分组
            for (int i = 0; i < salesDatas.Count; i++)
            {
                for (int j = i + 1; j < salesDatas.Count; j++)
                {
                    if (salesDatas[i]._year == salesDatas[j]._year && salesDatas[i]._month == salesDatas[j]._month)
                    {
                        salesDatas[i]._amount += salesDatas[j]._amount;
                        salesDatas.RemoveAt(j);
                        j--;
                    }
                }
            }
            salesDatas.Sort(new MedicineManageProject.Utils.YMSortCompare());
            return salesDatas;
        }

        //购买操作
        public bool purchase(PurchaseDTO purchaseDTO)
        {
            try
            {
                Db.Ado.BeginTran();
                List<OrderItemDTO> orderItems = purchaseDTO._order_item_list;
                DateTime dateTime = DateTime.Now;
                MedicineManager medicineManager = new MedicineManager();
                DiscountManager discountManager = new DiscountManager();
                Decimal totalPrice = 0;
                List<Decimal> priceStore = new List<Decimal>();

                foreach(OrderItemDTO orderItem in orderItems)
                {
                    var price = medicineManager.getMedicinePrice(orderItem._medicine_id, orderItem._batch_id);
                    var discountPrice = discountManager.getDiscountPriceById(orderItem._medicine_id, orderItem._batch_id);
                    var truePrice = price - discountPrice;
                    priceStore.Add(truePrice);
                    totalPrice += truePrice;
                }

                CUSTOMER customer = Db.Queryable<CUSTOMER>().Where(it => it.CUSTOMER_ID == purchaseDTO._customer_id).Single();
                customer.BONUS_POINT = customer.BONUS_POINT + (int)totalPrice;
                Db.Updateable(customer).ExecuteCommand();

                SALE_RECORD saleRecord = new SALE_RECORD
                {
                    SALE_DATE = dateTime,
                    SALE_PRICE = totalPrice,
                    STAFF_ID = purchaseDTO._staff_id,
                    CUSTOMER_ID = purchaseDTO._customer_id
                };

                Db.Insertable(saleRecord).IgnoreColumns(it => new { it.SALE_ID }).ExecuteCommand();

                var id = Db.Ado.SqlQuery<int>("select ISEQ$$_75609.currval from dual");
                var cId = id[0];
                int count = 0;


                foreach(OrderItemDTO orderItem in orderItems){
                    
                    var stock = Db.Queryable<MEDICINE_STOCK>().Where((it => it.MEDICINE_ID == orderItem._medicine_id
                        && it.BATCH_ID == orderItem._batch_id)).Single();
                    if(stock.AMOUNT < orderItem._sale_num)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        stock.AMOUNT -= orderItem._sale_num;
                        Db.Updateable(stock).ExecuteCommand();
                        SALE_RECORD_ITEM saleRecordItem = new SALE_RECORD_ITEM
                        {
                            SALE_ID = cId,
                            ORDER_ID = count,
                            UNIT_PRICE = priceStore[count],
                            SALE_NUM = orderItem._sale_num,
                            BATCH_ID = orderItem._batch_id,
                            MEDICINE_ID = orderItem._medicine_id
                        };
                        Db.Insertable(saleRecordItem).ExecuteCommand();

                        STOCK_OUT stockOut = new STOCK_OUT
                        {
                            STOCK_ID = stock.STOCK_ID,
                            OUT_NUM = orderItem._sale_num,
                            OUT_TIME = dateTime,
                            SALE_ID = cId,
                            STAFF_ID = purchaseDTO._staff_id
                        };
                        Db.Insertable(stockOut).ExecuteCommand();
                        count++;
                    }
                }

                Db.Ado.CommitTran();

                return true;
            }catch(Exception ex)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }

        // 退货操作
        public bool backRecords(Decimal saleId)
        {
            try
            {
                Db.Ado.BeginTran();
                DateTime dateTime = DateTime.Now;
                var orderItemList = Db.Queryable<SALE_RECORD_ITEM>()
                    .Where(it => it.SALE_ID == saleId).ToList();
                var saleRecord = Db.Queryable<SALE_RECORD>().Where(it => it.SALE_ID == saleId).Single();
                if(orderItemList == null || saleRecord == null)
                {
                    throw new Exception();
                }
                foreach(SALE_RECORD_ITEM item in orderItemList)
                {
                    RETURN_RECORD returnRecord = new RETURN_RECORD
                    {
                        SALE_ID = saleRecord.SALE_ID,
                        BACK_NUM = item.SALE_NUM,
                        BACK_DATE = dateTime,
                        CUSTOMER_ID = saleRecord.CUSTOMER_ID,
                        STAFF_ID = saleRecord.STAFF_ID,
                        ORDER_ID = item.ORDER_ID
                    };

                    var itemStock = Db.Queryable<MEDICINE_STOCK>()
                        .Where(it => it.MEDICINE_ID == item.MEDICINE_ID && it.BATCH_ID == item.BATCH_ID)
                        .Single();

                    itemStock.AMOUNT += item.SALE_NUM;
                    Db.Updateable(itemStock).ExecuteCommand();
                    Db.Insertable(returnRecord).IgnoreColumns(it => new { it.RETURN_ID }).ExecuteCommand();
                }

                Db.Ado.CommitTran();
                return true;
                
            }catch(Exception e)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }



        //获取全部销售记录
        public List<SaleInformationPlusDTO> getAllSaleRecords()
        {
            var result = Db.Queryable<SALE_RECORD>().Select(it => new SaleInformationPlusDTO
            {
                _sale_id = it.SALE_ID,
                _sale_date = it.SALE_DATE,
                _staff_id = it.STAFF_ID,
                _sale_price = it.SALE_PRICE,
                _customer_id = it.CUSTOMER_ID
            }).ToList();
            foreach(var e in result)
            {
                e._is_return =  Db.Queryable<RETURN_RECORD>().Where(it=>it.SALE_ID == e._sale_id).First() == null ? false:true;
            }
            List<SaleInformationPlusDTO> list = result;
            list.Sort();
            return list;
        }


        //获取某个销售记录的订单子项
        public List<SaleItemDTO> getAllOrderItemOfOneSaleInfo(Decimal saleId)
        {
            var result = Db.Queryable<SALE_RECORD_ITEM, MEDICINE_INFORMATION>(
                (sri, minfo) => sri.MEDICINE_ID == minfo.MEDICINE_ID)
                .Where(sri => sri.SALE_ID == saleId)
                .Select((sri,minfo) => new {
                    sri.BATCH_ID,
                    sri.MEDICINE_ID,
                    minfo.MEDICINE_NAME,
                    sri.UNIT_PRICE,
                    sri.SALE_NUM  
                }).ToList();

            List<SaleItemDTO> saleItems = new List<SaleItemDTO>();
            for(int i = 0; i < result.Count; i++)
            {
                SaleItemDTO saleItem = new SaleItemDTO();
                saleItem._batch_id = result[i].BATCH_ID;
                saleItem._medicine_id = result[i].MEDICINE_ID;
                saleItem._medicine_name = result[i].MEDICINE_NAME;
                saleItem._per_price = result[i].UNIT_PRICE;
                saleItem._sale_num = result[i].SALE_NUM;
                saleItems.Add(saleItem);
            }

            return saleItems;
        }

        //获取所有药品的销售记录
        public List<MedicineSaleDataDTO> getAllMedicineSaleData()
        {
            List<MedicineSaleDataDTO> group = Db.Queryable<SALE_RECORD_ITEM, MEDICINE_INFORMATION>(
                (s, mi) => s.MEDICINE_ID == mi.MEDICINE_ID ).GroupBy((s, mi) =>mi.MEDICINE_ID)
             .Select((s, mi) => new MedicineSaleDataDTO
             {
                 _medicine_id = mi.MEDICINE_ID,
                 _sale_num = SqlFunc.AggregateSum(s.SALE_NUM)
             }).ToList();
            foreach (MedicineSaleDataDTO temp in group)
            {
                MEDICINE_INFORMATION s = Db.Queryable<MEDICINE_INFORMATION>().InSingle(temp._medicine_id);
                int amount = Db.Queryable<MEDICINE_STOCK>().Where(it => it.MEDICINE_ID == temp._medicine_id).Sum(it=>it.AMOUNT);
                temp._medicine_name = s.MEDICINE_NAME;
                temp._amount = amount;
            }
            group.Sort();
            return group;
        }
        public List<SaleInformationDTO> getRecordsUnderCustomer(String customerId)
        {
            List<SaleInformationDTO> resultList = Db.Queryable<SALE_RECORD>().Where(it => it.CUSTOMER_ID == customerId)
                .Select(it => new SaleInformationDTO
                {
                    _customer_id = it.CUSTOMER_ID,
                    _sale_date = it.SALE_DATE,
                    _sale_id = it.SALE_ID,
                    _sale_price = it.SALE_PRICE,
                    _staff_id = it.STAFF_ID
                })
                .ToList();
            resultList.Sort();
            return resultList;
        }

    }
}
