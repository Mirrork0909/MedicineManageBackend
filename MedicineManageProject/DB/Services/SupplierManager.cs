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
            List<MedicineBySupplierDTO> list = Db.Queryable<MEDICINE_INFORMATION>()
                .Where((minfo) => minfo.SUPPLIER_ID == id).Select((minfo) => new MedicineBySupplierDTO
                {
                    _medicine_id = minfo.MEDICINE_ID,
                    _medicine_name = minfo.MEDICINE_NAME,
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

        public SupplierDTO createSupplier(SupplierDTO supplierDTO)
        {
            try
            {
                Db.Ado.BeginTran();

                SUPPLIER supplier = new SUPPLIER();
                supplier.NAME = supplierDTO._name;
                supplier.PHONE = supplierDTO._phone;
                supplier.CREDIT_LEVEL = supplierDTO._credit_level;

                Db.Insertable(supplier).IgnoreColumns(it => new { it.SUPPLIER_ID }).ExecuteCommand();

                var cid = Db.Ado.SqlQuery<int>("select ISEQ$$_75582.currval from dual");
                var id = cid[0];
              
                Db.Ado.CommitTran();
                supplierDTO._supplier_id = id;
                return supplierDTO;
            }        
            catch(Exception e)
            {
                Db.Ado.RollbackTran();
                return null;
            }
        }

        public bool updateSupplier(SupplierDTO supplierDTO)
        {
            SUPPLIER s = Db.Queryable<SUPPLIER>().InSingle(supplierDTO._supplier_id);
            if (s == null)
            {
                return false;
            }
            s.NAME = supplierDTO._name;
            s.PHONE = supplierDTO._phone;
            s.CREDIT_LEVEL = supplierDTO._credit_level;
            Db.Updateable(s).ExecuteCommand();
            return true;
        }

        public bool deleteSupplier(int id)
        {
            try
            {
                var t3 = Db.Deleteable<SUPPLIER>().In(id).ExecuteCommand();
                if (t3 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public int getSupplierIdByName(string name)
        {
            SUPPLIER s = Db.Queryable<SUPPLIER>().Where(it => it.NAME == name).First();
            if (s != null)
            {
                return s.SUPPLIER_ID;
            }
            else
            {
                return -1;
            }
        }

        public SupplierDTO getSupplierByContractId(int contractId)
        {
            try
            {
                CONTRACT contract = Db.Queryable<CONTRACT>().InSingle(contractId);
                SUPPLIER supplier = Db.Queryable<SUPPLIER>().InSingle(contract.SUPPLIER_ID);
                SupplierDTO supplierDTO = new SupplierDTO();
                supplierDTO._supplier_id = supplier.SUPPLIER_ID;
                supplierDTO._name = supplier.NAME;
                supplierDTO._phone = supplier.PHONE;
                supplierDTO._credit_level = supplier.CREDIT_LEVEL;
                return supplierDTO;
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

    }
}
