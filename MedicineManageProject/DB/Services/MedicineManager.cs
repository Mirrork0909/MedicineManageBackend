using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using SqlSugar;

namespace MedicineManageProject.DB.Services
{
    public class MedicineManager:DBContext
    {
        public bool insertNewMedicine(MedicineDTO medicine)
        {
            try
            {
                Db.Ado.BeginTran();
                MEDICINE_INFORMATION medicineInformation = new MEDICINE_INFORMATION()
                {
                    MEDICINE_ID = medicine._medicine_id,
                    MEDICINE_APPLICABILITY = medicine._medicine_applicability,
                    MEDICINE_CHARACTER = medicine._medicine_character,
                    MEDICINE_ATTENTION = medicine._medicine_attention,
                    MEDICINE_NAME = medicine._medicine_name,
                    MEDICINE_TYPE = medicine._medicine_type,
                    MEDICINE_INGREDIENTS = medicine._medicine_ingredients,
                    MEDICINE_USAGE = medicine._medicine_usage
                };

                MEDICINE_INSTANCE medicineInstance = new MEDICINE_INSTANCE()
                {
                    MEDICINE_ID = medicine._medicine_id,
                    BATCH_ID = medicine._batch_id,
                    PRODUCTION_DATE = medicine._production_date,
                    VALIDITY_DATE = medicine._validity_date,
                    SALE_PRICE = medicine._sale_price,
                    PURCHASE_PRICE = medicine._purchase_price,
                    SUPPLIER_ID = medicine._supplier_id,
                    MEDICINE_IMAGE = medicine._medicine_image
                };

                MEDICINE_STOCK medicineStock = new MEDICINE_STOCK()
                {
                    MEDICINE_ID = medicine._medicine_id,
                    BATCH_ID = medicine._batch_id,
                    AMOUNT = 0
                };

                bool judge = Db.Queryable<MEDICINE_INFORMATION>().Where(it => it.MEDICINE_ID == medicine._medicine_id).Any();
                if (!judge)
                {
                    Db.Insertable(medicineInformation).ExecuteCommand();
                }
                Db.Insertable(medicineInstance).ExecuteCommand();
                Db.Insertable(medicineStock).IgnoreColumns(it => new { it.STOCK_ID }).ExecuteCommand();

                Db.Ado.CommitTran();
                return true;

            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }

        public List<MedicineDTO> getMedicineInfoAll()
        {
            DateTime dateTime = DateTime.Now;

            var discountID = Db.Queryable<MEDICINE_INSTANCE, SET_DISCOUNT, DISCOUNT>((mins, sd, d) => mins.MEDICINE_ID == sd.MEDICINE_ID
                  && mins.BATCH_ID == sd.BATCH_ID && d.DISCOUNT_ID == sd.DISCOUNT_ID).Where((mins, sd, d) => d.START_TIME < dateTime && d.END_TIME > dateTime)
                  .Select((mins,sd,d) => d.DISCOUNT_ID).ToList();
           
            var dtos_has = Db.Queryable<MEDICINE_INFORMATION, MEDICINE_INSTANCE, MEDICINE_STOCK, SET_DISCOUNT, DISCOUNT>
                ((minfo, mins, ms, sd, d) => minfo.MEDICINE_ID == mins.MEDICINE_ID && mins.MEDICINE_ID == ms.MEDICINE_ID
                && mins.BATCH_ID == ms.BATCH_ID && mins.MEDICINE_ID == sd.MEDICINE_ID && mins.BATCH_ID == sd.BATCH_ID &&
                sd.DISCOUNT_ID == d.DISCOUNT_ID)
                .Where((minfo, mins, ms, sd, d) => d.START_TIME < dateTime && d.END_TIME > dateTime)
                .Select((minfo, mins, ms, sd, d) => new MedicineDTO
                {
                    _medicine_id = minfo.MEDICINE_ID,
                    _batch_id = mins.BATCH_ID,
                    _discount_price = d.DISCOUNT_PRICE,
                    _medicine_amount = ms.AMOUNT,
                    _medicine_applicability = minfo.MEDICINE_APPLICABILITY,
                    _medicine_attention = minfo.MEDICINE_ATTENTION,
                    _medicine_character = minfo.MEDICINE_CHARACTER,
                    _medicine_ingredients = minfo.MEDICINE_INGREDIENTS,
                    _medicine_type = minfo.MEDICINE_TYPE,
                    _medicine_image = mins.MEDICINE_IMAGE,
                    _medicine_name = minfo.MEDICINE_NAME,
                    _medicine_usage = minfo.MEDICINE_USAGE,
                    _production_date = mins.PRODUCTION_DATE,
                    _purchase_price = mins.PURCHASE_PRICE,
                    _sale_price = mins.SALE_PRICE,
                    _supplier_id = mins.SUPPLIER_ID,
                    _validity_date = mins.VALIDITY_DATE
                }).ToList();

            var dtos_hasnot = Db.Queryable<MEDICINE_INFORMATION, MEDICINE_INSTANCE, MEDICINE_STOCK, SET_DISCOUNT, DISCOUNT>
                ((minfo, mins, ms, sd, d) => new object[]
                {JoinType.Left,minfo.MEDICINE_ID == mins.MEDICINE_ID,
                 JoinType.Left,(mins.MEDICINE_ID == ms.MEDICINE_ID && mins.BATCH_ID == ms.BATCH_ID),
                 JoinType.Left,(mins.MEDICINE_ID == sd.MEDICINE_ID && mins.BATCH_ID == sd.BATCH_ID),
                 JoinType.Left,sd.DISCOUNT_ID == d.DISCOUNT_ID })
                 .Where((minfo,mins,ms,sd,d)=> SqlFunc.IsNullOrEmpty(d.DISCOUNT_ID) || !discountID.Contains(d.DISCOUNT_ID))
                 .Select((minfo, mins, ms, sd, d) => new MedicineDTO
                 {
                     _medicine_id = minfo.MEDICINE_ID,
                     _batch_id = mins.BATCH_ID,
                     _discount_price = 0,
                     _medicine_amount = ms.AMOUNT,
                     _medicine_applicability = minfo.MEDICINE_APPLICABILITY,
                     _medicine_attention = minfo.MEDICINE_ATTENTION,
                     _medicine_character = minfo.MEDICINE_CHARACTER,
                     _medicine_ingredients = minfo.MEDICINE_INGREDIENTS,
                     _medicine_type = minfo.MEDICINE_TYPE,
                     _medicine_image = mins.MEDICINE_IMAGE,
                     _medicine_name = minfo.MEDICINE_NAME,
                     _medicine_usage = minfo.MEDICINE_USAGE,
                     _production_date = mins.PRODUCTION_DATE,
                     _purchase_price = mins.PURCHASE_PRICE,
                     _sale_price = mins.SALE_PRICE,
                     _supplier_id = mins.SUPPLIER_ID,
                     _validity_date = mins.VALIDITY_DATE
                 }).ToList();

            var dtos = dtos_has.Union(dtos_hasnot).ToList();
            return dtos;
        }

        public MedicineDTO getMedicineInfoById(String medicineId,String batchId)
        {
            DateTime dateTime = DateTime.Now;

            var dto_base = Db.Queryable<MEDICINE_INFORMATION, MEDICINE_INSTANCE, MEDICINE_STOCK>
                ((minfo, mins, ms) => minfo.MEDICINE_ID == mins.MEDICINE_ID && mins.MEDICINE_ID == ms.MEDICINE_ID
                    && mins.BATCH_ID == ms.BATCH_ID).Where((minfo, mins, ms) => mins.MEDICINE_ID == medicineId
                    && mins.BATCH_ID == batchId).Select((minfo, mins, ms) => new MedicineDTO
                    {
                        _medicine_id = minfo.MEDICINE_ID,
                        _batch_id = mins.BATCH_ID,
                        _medicine_amount = ms.AMOUNT,
                        _medicine_applicability = minfo.MEDICINE_APPLICABILITY,
                        _medicine_attention = minfo.MEDICINE_ATTENTION,
                        _medicine_character = minfo.MEDICINE_CHARACTER,
                        _medicine_ingredients = minfo.MEDICINE_INGREDIENTS,
                        _medicine_type = minfo.MEDICINE_TYPE,
                        _medicine_image = mins.MEDICINE_IMAGE,
                        _medicine_name = minfo.MEDICINE_NAME,
                        _medicine_usage = minfo.MEDICINE_USAGE,
                        _production_date = mins.PRODUCTION_DATE,
                        _purchase_price = mins.PURCHASE_PRICE,
                        _sale_price = mins.SALE_PRICE,
                        _supplier_id = mins.SUPPLIER_ID,
                        _validity_date = mins.VALIDITY_DATE
                    }).ToList();

            var discountPrice = Db.Queryable<SET_DISCOUNT, DISCOUNT>((sd, d) => sd.DISCOUNT_ID == d.DISCOUNT_ID)
                .Where((sd, d) => sd.MEDICINE_ID == medicineId && sd.BATCH_ID == batchId).Select((sd, d) => d.DISCOUNT_PRICE).Single();

            MedicineDTO dto = dto_base[0];
            dto._discount_price = discountPrice;

            return dto;

            
           

        }

        public bool updateMedicineInfo(MedicineDTO medicineDTO)
        {
            try
            {
                Db.Ado.BeginTran();
                MEDICINE_INFORMATION medicineInformation = new MEDICINE_INFORMATION()
                {
                    MEDICINE_ID = medicineDTO._medicine_id,
                    MEDICINE_APPLICABILITY = medicineDTO._medicine_applicability,
                    MEDICINE_CHARACTER = medicineDTO._medicine_character,
                    MEDICINE_ATTENTION = medicineDTO._medicine_attention,
                    MEDICINE_NAME = medicineDTO._medicine_name,
                    MEDICINE_TYPE = medicineDTO._medicine_type,
                    MEDICINE_INGREDIENTS = medicineDTO._medicine_ingredients,
                    MEDICINE_USAGE = medicineDTO._medicine_usage
                };

                MEDICINE_INSTANCE medicineInstance = new MEDICINE_INSTANCE()
                {
                    MEDICINE_ID = medicineDTO._medicine_id,
                    BATCH_ID = medicineDTO._batch_id,
                    PRODUCTION_DATE = medicineDTO._production_date,
                    VALIDITY_DATE = medicineDTO._validity_date,
                    SALE_PRICE = medicineDTO._sale_price,
                    PURCHASE_PRICE = medicineDTO._purchase_price,
                    SUPPLIER_ID = medicineDTO._supplier_id,
                    MEDICINE_IMAGE = medicineDTO._medicine_image
                };

                Db.Updateable(medicineInformation);
                Db.Updateable(medicineInstance);

                Db.Ado.CommitTran();
                return true;
            }
            catch(Exception ex)
            {
                Db.Ado.RollbackTran();
                return false;
            }
        }

        public Decimal getMedicinePrice(String medicineId, String batchId)
        {
            var price = Db.Queryable<MEDICINE_INSTANCE>().Where(it => it.BATCH_ID == batchId
                && it.MEDICINE_ID == medicineId).Select(it => it.SALE_PRICE).Single().ObjToDecimal();
            return price;
        }
    }
}
