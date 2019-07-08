using MedicineManageProject.DB.Services;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using MedicineManageProject.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        [HttpGet("all/num")]
        public IActionResult countAllCustomerNum()
        {
            CustomerManager customerManager = new CustomerManager();
            int num = customerManager.getAllCustomerNum();
            return Ok(addDataToResult(ConstMessage.GET_SUCCESS, num));
        }

        public JsonCreate addDataToResult(String message,object data)
        {
            JsonCreate jsonCreate = new JsonCreate();
            jsonCreate.message = message;
            jsonCreate.data = data;
            return jsonCreate;
        }
    }
}
