using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using MedicineManageProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.DB.Services
{
    public class StaffManager:DBContext
    {
        static int BY_PHONE = 0;
        static int BY_ID = 1;
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
            STAFF staff = Db.Queryable<STAFF>().InSingle(id);
            if (staff != null)
            {
                staff.PASSWORD = Md5.getMd5Hash(password);
                Db.Updateable(staff).ExecuteCommand();
                STAFF _updatedStaff = Db.Queryable<STAFF>().InSingle(id);
                if (_updatedStaff.PASSWORD == Md5.getMd5Hash(password))
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
            STAFF staff = Db.Queryable<STAFF>().InSingle(id);
            if (staff != null)
            {
                staff.NAME = name;
                staff.PHONE = phone;
                Db.Updateable(staff).ExecuteCommand();
                STAFF updatedStaff = Db.Queryable<STAFF>().InSingle(id);
                if (updatedStaff.NAME == name && updatedStaff.PHONE == phone)
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
        public String verifyPasswordAndPhone(String phone, String password)
        {
            STAFF staff = Db.Queryable<STAFF>().Where(it => it.PHONE == phone).First();
            return verifyStaff(staff, password, BY_PHONE);
        }

        public String verifyPasswordAndId(String id, String password)
        {
            STAFF staff = Db.Queryable<STAFF>().InSingle(id);
            return verifyStaff(staff, password, BY_ID);
        }
        private String verifyStaff(STAFF staff, String password, int type)
        {
            if (staff != null)
            {
                if (Md5.verifyMd5Hash(password, staff.PASSWORD))
                {
                    return AccountConstMessage.LOGIN_SUCCESS;
                }
            }
            if (type == BY_PHONE)
            {
                return AccountConstMessage.INVALID_INFO_PHONE;
            }
            else
            {
                return AccountConstMessage.INVALID_INFO_ID;
            }
        }
        public String createStaff(RegisterDTO registerDTO)
        {
            STAFF staff0 = Db.Queryable<STAFF>().InSingle(registerDTO._id);
            STAFF staff1 = Db.Queryable<STAFF>().Where(it => it.PHONE == registerDTO._phone).First();
            if (staff0 != null)
            {
                return AccountConstMessage.EXISTED_ID;
            }
            if (staff1 != null)
            {
                return AccountConstMessage.EXISTED_PHONE;
            }
            STAFF staff = new STAFF();
            staff.STAFF_ID = registerDTO._id;
            staff.NAME = registerDTO._name;
            staff.PASSWORD = Md5.getMd5Hash(registerDTO._password);
            staff.PHONE = registerDTO._phone;
            staff.SIGN_DATE = DateTime.Now;
            Db.Insertable(staff).ExecuteCommand();
            return AccountConstMessage.REGISTER_SUCCESS;
        }

    }
    

}
