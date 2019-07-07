using MedicineManageProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MedicineManageProject.DB.Services
{
    public class CustomerManager:DBContext
    {
        public int getAllCustomerNum()
        {
            return Db.Queryable<CUSTOMER>().Count();
        }

        public List<CUSTOMER> getCustomerInformationByName(string name)
        {
            return Db.Queryable<CUSTOMER>().Where(it => it.NAME == name).ToList();
        }
        
    }
}
