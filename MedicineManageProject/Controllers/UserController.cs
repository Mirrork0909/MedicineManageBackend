using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineManageProject.DB.Services;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using MedicineManageProject.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        [HttpPost("insert/customer")]
        public IActionResult insertNewCustomer([FromForm] CUSTOMER customer)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.insertCustomer(customer);
            return Ok(new JsonCreate());
        }
        [HttpPost("insert/staff")]
        public IActionResult insertNewStaff([FromForm]STAFF staff)
        {
            StaffManager staffManager = new StaffManager();
            staffManager.insertStaff(staff);
            return Ok(new JsonCreate());
        }
        [HttpGet("get/all")]
        public IActionResult getAllUserInformation()
        {
            JsonCreate resultJson = new JsonCreate();
            StaffManager staffManager = new StaffManager();
            CustomerManager customerManager = new CustomerManager();
            List<STAFF> staffs = staffManager.getAllStaffsInformation();
            List<CUSTOMER> customers = customerManager.getAllCustomersInformation();
            List<UserInformationDTO> userList = new List<UserInformationDTO>();
            if (staffs != null && staffs.Count > 0)
            {
                userList.AddRange(addIdentity(staffs, UserInformationDTO._STAFF));
            }
            if (customers != null && customers.Count > 0)
            {
                userList.AddRange(addIdentity(customers, UserInformationDTO._CUSTOMER));
            }
            resultJson.message = userList.Count.ToString();
            resultJson.data = userList;
            return Ok(resultJson);
        }
        [HttpGet("get/{name}")]
        public IActionResult getUserInformation(String name)
        {
            JsonCreate resultJson = new JsonCreate();
            StaffManager staffManager = new StaffManager();
            CustomerManager customerManager = new CustomerManager();
            List<STAFF> staffs = staffManager.getStaffInformationByName(name);
            List<CUSTOMER> customers = customerManager.getCustomerInformationByName(name);
            List<UserInformationDTO> userList = new List<UserInformationDTO>();
            if (staffs != null && staffs.Count > 0)
            {
                userList.AddRange(addIdentity(staffs, UserInformationDTO._STAFF));
            }
            if (customers != null && customers.Count > 0)
            {
                userList.AddRange(addIdentity(customers, UserInformationDTO._CUSTOMER));
            }
            resultJson.message = userList.Count.ToString();
            resultJson.data = userList;
            return Ok(resultJson);
        }
        public List<UserInformationDTO> addIdentity(List<CUSTOMER> users,String identity)
        {
            List<UserInformationDTO> userList = new List<UserInformationDTO>();
            foreach(CUSTOMER user in users)
            {
                UserInformationDTO userInformationDTO = new UserInformationDTO();
                userInformationDTO._id = user.CUSTOMER_ID;
                userInformationDTO._name = user.NAME;
                userInformationDTO._identity = identity;
                userInformationDTO._phone = user.PHONE;
                userInformationDTO._sign_date = user.SIGN_DATE;
                userList.Add(userInformationDTO);
            }
            return userList;
        }
        public List<UserInformationDTO> addIdentity(List<STAFF> users, String identity)
        {
            List<UserInformationDTO> userList = new List<UserInformationDTO>();
            foreach (STAFF user in users)
            {
                UserInformationDTO userInformationDTO = new UserInformationDTO();
                userInformationDTO._id = user.STAFF_ID;
                userInformationDTO._name = user.NAME;
                userInformationDTO._identity = identity;
                userInformationDTO._phone = user.PHONE;
                userInformationDTO._sign_date = user.SIGN_DATE;
                userList.Add(userInformationDTO);
            }
            return userList;
        }
    }
}
