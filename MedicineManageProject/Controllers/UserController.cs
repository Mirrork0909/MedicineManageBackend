﻿using System;
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
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 仅用于后端测试
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("insert/customer")]
        public IActionResult insertNewCustomer([FromForm] CUSTOMER customer)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.insertCustomer(customer);
            return Ok(new JsonCreate());
        }
        /// <summary>
        /// 仅用于后端测试
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPost("insert/staff")]
        public IActionResult insertNewStaff([FromForm]STAFF staff)
        {
            StaffManager staffManager = new StaffManager();
            staffManager.insertStaff(staff);
            return Ok(new JsonCreate());
        }
        /// <summary>
        /// 获取所有用户信息(包括员工和顾客)
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/all")]
        public IActionResult getAllUserInformation()
        {
            StaffManager staffManager = new StaffManager();
            CustomerManager customerManager = new CustomerManager();
            List<STAFF> staffs = staffManager.getAllStaffsInformation();
            List<CUSTOMER> customers = customerManager.getAllCustomersInformation();
            return Ok(UserMergeUtil.addDataToResult(staffs,customers));
        }
        /// <summary>
        /// 获取用户信息Byname
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("get/{name}")]
        public IActionResult getUserInformation(String name)
        {
            JsonCreate resultJson = new JsonCreate();
            StaffManager staffManager = new StaffManager();
            CustomerManager customerManager = new CustomerManager();
            List<STAFF> staffs = staffManager.getStaffInformationByName(name);
            List<CUSTOMER> customers = customerManager.getCustomerInformationByName(name);
            return Ok(UserMergeUtil.addDataToResult(staffs,customers));
        }

       
    }
}
