using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;


namespace MedicineManageProject.DB.Services
{
    public class DiscountManager:DBContext
    {
        // 获取所有的折扣信息
        public List<DiscountDTO> getDiscountDTOs()
        {
            var dtos = Db.Queryable<DISCOUNT, SET_DISCOUNT, MEDICINE_INSTANCE, MEDICINE_INFORMATION>((st, sc, sc2, ss) =>
                   st.DISCOUNT_ID == sc.DISCOUNT_ID && sc.MEDICINE_ID == sc2.MEDICINE_ID 
                   && sc.BATCH_ID == sc2.BATCH_ID
                   && sc.MEDICINE_ID == ss.MEDICINE_ID)
                .Select((st, sc, sc2, ss) => new DiscountDTO
                {
                    _medicine_name = ss.MEDICINE_NAME,
                    _medicine_id = sc.MEDICINE_ID,
                    _amount = st.DISCOUNT_PRICE,
                    _batch_id = sc.BATCH_ID,
                    _context = st.CONTEXT,
                    _discount_id = st.DISCOUNT_ID,
                    _start_time = st.START_TIME,
                    _end_time = st.END_TIME
                }).ToList();
            return dtos;
        }

        // 获取某种药品的折扣信息
        public List<DiscountDTO> getDiscountDTOById(String medicineId)
        {
            var dtos = Db.Queryable<DISCOUNT, SET_DISCOUNT, MEDICINE_INSTANCE, MEDICINE_INFORMATION>((ds, sds, mi, minfo) =>
                       ds.DISCOUNT_ID == sds.DISCOUNT_ID && sds.MEDICINE_ID == mi.MEDICINE_ID && sds.BATCH_ID == mi.BATCH_ID
                       && mi.MEDICINE_ID == minfo.MEDICINE_ID).Where((ds, sds) => sds.MEDICINE_ID == medicineId)
                    .Select((ds, sds, mi, minfo) => new DiscountDTO
                    {
                        _medicine_name = minfo.MEDICINE_NAME,
                        _medicine_id = sds.MEDICINE_ID,
                        _amount = ds.DISCOUNT_PRICE,
                        _batch_id = sds.BATCH_ID,
                        _context = ds.CONTEXT,
                        _discount_id = ds.DISCOUNT_ID,
                        _start_time = ds.START_TIME,
                        _end_time = ds.END_TIME
                    }).ToList();
            return dtos;
        }

        public Decimal getDiscountPriceById(String medicineId,String batchId)
        {
            DateTime dateTime = DateTime.Now;
            var discountPrice = Db.Queryable<DISCOUNT, SET_DISCOUNT>((d, sd) => d.DISCOUNT_ID == sd.DISCOUNT_ID)
                .Where((d, sd) => sd.MEDICINE_ID == medicineId && sd.BATCH_ID == batchId 
                && d.START_TIME < dateTime && d.END_TIME > dateTime)
                .Select((d, sd) => d.DISCOUNT_PRICE).Single().ObjToDecimal();
            return discountPrice;
        }


        //建立一条新的优惠信息
        public bool insertNewDiscount(DiscountDTO discountDTO)
        {
            
            try
            {
                Db.Ado.BeginTran();
                DISCOUNT discount = new DISCOUNT
                {
                    DISCOUNT_PRICE = discountDTO._amount,
                    START_TIME = discountDTO._start_time,
                    END_TIME = discountDTO._end_time,
                    CONTEXT = discountDTO._context
                };

                var existId = Db.Queryable<SET_DISCOUNT>().Where(it => it.MEDICINE_ID == discountDTO._medicine_id
                && it.BATCH_ID == discountDTO._batch_id).Select(it => it.DISCOUNT_ID).ToList();

                var cId = 0;
                if(existId.Count == 0)
                {
                    Db.Insertable<DISCOUNT>(discount).
                    IgnoreColumns(it => new { it.DISCOUNT_ID }).
                    ExecuteCommandIdentityIntoEntity();


                    var id = Db.Ado.SqlQuery<int>("select ISEQ$$_75598.currval from dual");
                    cId = id[0];
                    SET_DISCOUNT setDiscount = new SET_DISCOUNT
                    {
                        BATCH_ID = discountDTO._batch_id,
                        MEDICINE_ID = discountDTO._medicine_id,
                        DISCOUNT_ID = cId
                    };
                    Db.Insertable(setDiscount).ExecuteCommand();
                }
                else
                {
                    cId = existId[0];
                    discount.DISCOUNT_ID = cId;
                    Db.Updateable(discount).ExecuteCommand();
                }

                

                Db.Ado.CommitTran();
                return true;

            }catch(Exception ex)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }


        public bool deleteDiscount(int discountId)
        {
            bool judge = Db.Queryable<DISCOUNT>().Where(it => it.DISCOUNT_ID == discountId).Any();
            if (judge)
            {
                var result = Db.Deleteable<DISCOUNT>().In(discountId).ExecuteCommand();
                if(result != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
