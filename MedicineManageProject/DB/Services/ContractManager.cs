using MedicineManageProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class ContractManager:DBContext
    {
        public CUSTOMER getAllContractInformation()
        {
            var allContract = Db.Queryable<CUSTOMER>().InSingle(1);
            return allContract;
        }
        public void insertContract(CONTRACT contract)
        {
       
            contract.CONTRACT_STATUS = 0;
            contract.DELIVERY_DATE = new DateTime();
            contract.SIGN_DATE = new DateTime();
            contract.SUPPLIER_ID = 2;
            contract.STAFF_ID = "3";
            var t2 = Db.Insertable(contract).ExecuteCommand();
        }
    }
}
