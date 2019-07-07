using MedicineManageProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class StaffManager:DBContext
    {
        public STAFF getStaffInformation(String id)
        {
            return Db.Queryable<STAFF>().InSingle(id);
        }

        public List<STAFF> getStaffInformationByName(string name)
        {
            return Db.Queryable<STAFF>().Where(it => it.NAME == name).ToList();
        }

        public bool resetStaffInformation(String id, String name, String phone)
        {
            STAFF _staff = Db.Queryable<STAFF>().InSingle(id);
            if (_staff != null)
            {
                _staff.NAME = name;
                _staff.PHONE = phone;
                Db.Updateable(_staff).ExecuteCommand();
                STAFF _updatedStaff = Db.Queryable<STAFF>().InSingle(id);
                if (_updatedStaff.NAME == name && _updatedStaff.PHONE == phone)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
