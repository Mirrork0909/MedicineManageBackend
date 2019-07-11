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
                    MEDICINE_USAGE = medicine._medicine_usage,
                    SUPPLIER_ID = medicine._supplier_id,
                    MEDICINE_IMAGE = medicine._medicine_image
                };

                bool judge = Db.Queryable<MEDICINE_INFORMATION>().Where(it => it.MEDICINE_ID == medicine._medicine_id).Any();
                if (!judge)
                {
                    Db.Insertable(medicineInformation).ExecuteCommand();
                }
            
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
                    _medicine_image = minfo.MEDICINE_IMAGE,
                    _medicine_name = minfo.MEDICINE_NAME,
                    _medicine_usage = minfo.MEDICINE_USAGE,
                    _production_date = mins.PRODUCTION_DATE,
                    _purchase_price = mins.PURCHASE_PRICE,
                    _sale_price = mins.SALE_PRICE,
                    _supplier_id = minfo.SUPPLIER_ID,
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
                     _medicine_image = minfo.MEDICINE_IMAGE,
                     _medicine_name = minfo.MEDICINE_NAME,
                     _medicine_usage = minfo.MEDICINE_USAGE,
                     _production_date = mins.PRODUCTION_DATE,
                     _purchase_price = mins.PURCHASE_PRICE,
                     _sale_price = mins.SALE_PRICE,
                     _supplier_id = minfo.SUPPLIER_ID,
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
                        _medicine_image = minfo.MEDICINE_IMAGE,
                        _medicine_name = minfo.MEDICINE_NAME,
                        _medicine_usage = minfo.MEDICINE_USAGE,
                        _production_date = mins.PRODUCTION_DATE,
                        _purchase_price = mins.PURCHASE_PRICE,
                        _sale_price = mins.SALE_PRICE,
                        _supplier_id = minfo.SUPPLIER_ID,
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
                    MEDICINE_USAGE = medicineDTO._medicine_usage,
                    SUPPLIER_ID = medicineDTO._supplier_id,
                    MEDICINE_IMAGE = medicineDTO._medicine_image
                };

                Db.Updateable(medicineInformation).ExecuteCommand();

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

        public List<MedicineSearchResultDTO> getMedicineListByKeyword(String keyword)
        {
            var results = Db.Queryable<MEDICINE_INFORMATION>()
                .Where(mif => mif.MEDICINE_NAME.Contains(keyword)).ToList();
            List<MedicineSearchResultDTO> medicineSearchResults = new List<MedicineSearchResultDTO>();
            foreach(var e in results)
            {
                MedicineSearchResultDTO medicine = new MedicineSearchResultDTO();
                medicine._medicine_id = e.MEDICINE_ID;
                medicine._medicine_name = e.MEDICINE_NAME;
                medicine._medicine_type = e.MEDICINE_TYPE;
                medicine._medicine_ingredients = e.MEDICINE_INGREDIENTS;
                medicine._medicine_character = e.MEDICINE_CHARACTER;
                medicine._medicine_applicability = e.MEDICINE_APPLICABILITY;
                medicine._medicine_usage = e.MEDICINE_USAGE;
                medicine._medicine_attention = e.MEDICINE_ATTENTION;
                medicine._supplier_id = e.SUPPLIER_ID;
                medicine._medicine_image = e.MEDICINE_IMAGE;
                medicine._max_purchase_price = Db.Queryable<MEDICINE_INSTANCE>().Where(it => it.MEDICINE_ID == e.MEDICINE_ID).Max(it => it.SALE_PRICE);
                medicine._min_purchase_price = Db.Queryable<MEDICINE_INSTANCE>().Where(it => it.MEDICINE_ID == e.MEDICINE_ID).Min(it => it.SALE_PRICE);
                medicineSearchResults.Add(medicine);
            }
            return medicineSearchResults;
        }


        public List<MedicineBySupplierDTO> getAllMedicineWithSupplier()
        {
            List<MedicineBySupplierDTO> list = Db.Queryable<MEDICINE_INFORMATION, SUPPLIER>((mi, s) => mi.SUPPLIER_ID == s.SUPPLIER_ID).Select((mi, s) => new MedicineBySupplierDTO
            {
                _medicine_applicability = mi.MEDICINE_APPLICABILITY,
                _medicine_attention = mi.MEDICINE_ATTENTION,
                _medicine_character=mi.MEDICINE_CHARACTER,
                _medicine_id=mi.MEDICINE_ID,
                _medicine_image=mi.MEDICINE_IMAGE,
                _medicine_ingredients=mi.MEDICINE_INGREDIENTS,
                _medicine_name=mi.MEDICINE_NAME,
                _medicine_type=mi.MEDICINE_TYPE,
                _medicine_usage=mi.MEDICINE_USAGE,
                _name=s.NAME,
                _supplier_id=s.SUPPLIER_ID,
            }).ToList();
            return list;
        }

        public List<MedicineInstanceDTO> getMedicineInstanceById(String m_id)
        {
            List<MedicineInstanceDTO> list = Db.Queryable<MEDICINE_INSTANCE>().Where((m) => m.MEDICINE_ID == m_id).Select((m)=> new MedicineInstanceDTO
            {
                _medicine_id=m.MEDICINE_ID,
                _batch_id=m.BATCH_ID,
                _production_date=m.PRODUCTION_DATE,
                _purchase_price=m.PURCHASE_PRICE,
                _sale_price=m.SALE_PRICE,
                _validity_date=m.VALIDITY_DATE,
            }).ToList();
            return list;
        }

    }
}
