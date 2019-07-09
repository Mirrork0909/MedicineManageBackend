using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class SupplierManager:DBContext
    {
        public List<SupplierDTO> getSupplierDTOs()
        {
            List<SUPPLIER> suppliers = Db.Queryable<SUPPLIER>().ToList();
            List<SupplierDTO> supplierDTOs = new List<SupplierDTO>();
            foreach(SUPPLIER temp in suppliers)
            {
                SupplierDTO s = new SupplierDTO();
                s._supplier_id = temp.SUPPLIER_ID;
                s._name = temp.NAME;
                s._phone = temp.PHONE;
                s._credit_level = temp.CREDIT_LEVEL;
                supplierDTOs.Add(s);
            }

            return supplierDTOs;
        }

        public List<MedicineBySupplierDTO> getAllMedicineBySupplier(int id)
        {
            List<MedicineBySupplierDTO> list= Db.Queryable<MEDICINE_INFORMATION, MEDICINE_INSTANCE>((minfo, mins) => minfo.MEDICINE_ID == mins.MEDICINE_ID)
                .Where((minfo, mins) => mins.SUPPLIER_ID == id).Select((minfo, mins) => new MedicineBySupplierDTO
                {
                    _medicine_id = minfo.MEDICINE_ID,
                    _medicine_name = minfo.MEDICINE_NAME,
                    _purchase_price=mins.PURCHASE_PRICE
                }).ToList();
            return list;
        }
        public List<MedicineTypeBySupplierDTO> getMedicineGroupBySupplier()
        {
            List<MedicineTypeBySupplierDTO> group = Db.Queryable<CONTRACT, CONTRACT_ITEM, SUPPLIER>((c, ci, s) => c.SUPPLIER_ID == s.SUPPLIER_ID &&
              c.CONTRACT_ID == ci.CONTRACT_ID).GroupBy((c, ci, s) => s.SUPPLIER_ID)
            .Select((c, ci, s) => new MedicineTypeBySupplierDTO {
                _type_number = SqlFunc.AggregateCount(ci.MEDICINE_ID),
                _supplier_id=s.SUPPLIER_ID
            }).ToList();
            foreach(MedicineTypeBySupplierDTO temp in group)
            {
                SUPPLIER s = Db.Queryable<SUPPLIER>().InSingle(temp._supplier_id);
                temp._name = s.NAME;
            }

            return group;
        }

    }
}
