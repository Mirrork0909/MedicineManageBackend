using MedicineManageProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class StaffManager:DBContext
    {
        public void insertStaff(STAFF staff)
        {
            staff.SIGN_DATE = DateTime.Now;
            Db.Insertable(staff).ExecuteCommand();
        }
        public List<STAFF> getAllStaffsInformation()
        {
            return Db.Queryable<STAFF>().ToList();
        }
        public STAFF getStaffInformation(String id)
        {
            return Db.Queryable<STAFF>().InSingle(id);
        }

        public List<STAFF> getStaffInformationByName(string name)
        {
            return Db.Queryable<STAFF>().Where(it => it.NAME == name).ToList();
        }
        public bool resetStaffPassword(String id,String password)
        {
            STAFF _staff = Db.Queryable<STAFF>().InSingle(id);
            if (_staff != null)
            {
                _staff.PASSWORD = password;
                Db.Updateable(_staff);
                STAFF _updatedStaff = Db.Queryable<STAFF>().InSingle(id);
                if (_updatedStaff.PASSWORD == password)
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
