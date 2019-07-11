using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using MedicineManageProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MedicineManageProject.DB.Services
{
    public class CustomerManager : DBContext
    {
        static int BY_PHONE = 0;
        static int BY_ID = 1;
        public List<CUSTOMER> getAllCustomersInformation()
        {
            return Db.Queryable<CUSTOMER>().ToList();
        }
        public int getAllCustomerNum()
        {
            return Db.Queryable<CUSTOMER>().Count();
        }

        public CustomerInformationDTO getCustomerByPhone(String phone)
        {
            CUSTOMER customer = Db.Queryable<CUSTOMER>().Where(it => it.PHONE == phone).First();
           
            if (customer != null)
            {
                return convertCustomerToDto(customer);
            }
            return null;
        }
        public CustomerInformationDTO getCustomerById(String id)
        {
            CUSTOMER customer = Db.Queryable<CUSTOMER>().InSingle(id);
            if (customer != null)
            {
                return convertCustomerToDto(customer);
            }
            return null;
        }
        public CustomerInformationDTO convertCustomerToDto(CUSTOMER customer){
            CustomerInformationDTO customerInformation = new CustomerInformationDTO();
            customerInformation._id = customer.CUSTOMER_ID;
            customerInformation._name = customer.NAME;
            customerInformation._phone = customer.PHONE;
            customerInformation._bouns_point = customer.BONUS_POINT;
            customerInformation._sign_date = customer.SIGN_DATE;
            return customerInformation;
        }


        public List<CUSTOMER> getCustomerInformationByName(string name)
        {
            return Db.Queryable<CUSTOMER>().Where(it => it.NAME == name).ToList();
        }

        public void insertCustomer(CUSTOMER customer)
        {
            customer.SIGN_DATE = DateTime.Now;
            Db.Insertable(customer).ExecuteCommand();
        }
        public String verifyPasswordAndPhone(String phone,String password)
        {
            CUSTOMER customer = Db.Queryable<CUSTOMER>().Where(it => it.PHONE == phone).First();
            return verifyCustomer(customer, password,BY_PHONE);
        }

        public String verifyPasswordAndId(String id, String password)
        {
            CUSTOMER customer = Db.Queryable<CUSTOMER>().InSingle(id);
            return verifyCustomer(customer, password,BY_ID);
        }
        public String createCustomer(RegisterDTO registerDTO)
        {
            CUSTOMER customer0 = Db.Queryable<CUSTOMER>().InSingle(registerDTO._id);
            CUSTOMER customer1 = Db.Queryable<CUSTOMER>().Where(it => it.PHONE == registerDTO._phone).First();
            if(customer0 != null)
            {
                return AccountConstMessage.EXISTED_ID;
            }
            if(customer1 != null)
            {
                return AccountConstMessage.EXISTED_PHONE;
            }
            CUSTOMER customer = new CUSTOMER();
            customer.CUSTOMER_ID = registerDTO._id;
            customer.NAME = registerDTO._name;
            customer.PASSWORD = Md5.getMd5Hash(registerDTO._password);
            customer.PHONE = registerDTO._phone;
            customer.SIGN_DATE = DateTime.Now;
            Db.Insertable(customer).ExecuteCommand();
            return AccountConstMessage.REGISTER_SUCCESS;
        }

        private String verifyCustomer(CUSTOMER customer,String password,int type)
        {
            if (customer != null)
            {
                if (Md5.verifyMd5Hash(password, customer.PASSWORD))
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

        public bool resetCustomerPassword(String id, String password)
        {
            CUSTOMER customer = Db.Queryable<CUSTOMER>().InSingle(id);
            if (customer != null)
            {
                customer.PASSWORD = Md5.getMd5Hash(password);
                Db.Updateable(customer).ExecuteCommand();
                CUSTOMER _updatedCustomer = Db.Queryable<CUSTOMER>().InSingle(id);
                if (_updatedCustomer.PASSWORD == Md5.getMd5Hash(password))
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
        public bool resetCustomerInformation(String id, String name, String phone)
        {
            CUSTOMER customer = Db.Queryable<CUSTOMER>().InSingle(id);
            if (customer != null)
            {
                customer.NAME = name;
                customer.PHONE = phone;
                Db.Updateable(customer).ExecuteCommand();
                CUSTOMER updatedCustomer = Db.Queryable<CUSTOMER>().InSingle(id);
                if (updatedCustomer.NAME == name && updatedCustomer.PHONE == phone)
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
