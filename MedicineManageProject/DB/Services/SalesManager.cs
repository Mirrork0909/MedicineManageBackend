using MedicineManageProject.DTO;
using MedicineManageProject.Model;
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
        public List<SalesDataDTO> getSalesAmountByMonth()
        {
            //).ToShortDateString().ToString()
            // var temp = Db.SqlQueryable<dynamic>("select to_char(SALE_DATE,'yyyymm')as time ,sum(SALE_PRICE) as amount  from SALE_RECORD group by to_char(SALE_DATE,'yyyymm')").ToList();
            var temp = Db.Queryable<SALE_RECORD>().GroupBy(it => SqlFunc.DateValue(it.SALE_DATE, DateType.Year)).GroupBy(it=>SqlFunc.DateValue(it.SALE_DATE, DateType.Month));
            temp.Select(it => new {time = SqlFunc.DateValue(it.SALE_DATE, DateType.Year) ,amount = SqlFunc.AggregateSum(it.SALE_PRICE) }).ToList(); 
            List<SalesDataDTO> salesDatas = new List<SalesDataDTO>();
          
           
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
                        Db.Ado.RollbackTran();
                        return false;
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


        //获取全部销售记录
        public List<SaleInformationDTO> getAllSaleRecords()
        {
            var result = Db.Queryable<SALE_RECORD>().Select(it => new SaleInformationDTO
            {
                _sale_id = it.SALE_ID,
                _sale_date = it.SALE_DATE,
                _staff_id = it.STAFF_ID,
                _sale_price = it.SALE_PRICE,
                _customer_id = it.CUSTOMER_ID
            }).ToList();
            return result;
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


    }
}
