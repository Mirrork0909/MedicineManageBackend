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
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// 获得当前用户数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("all/num")]
        public IActionResult countAllCustomerNum()
        {
            /*if (!SessionUtil.checkSessionExisted(this.HttpContext))
            {
                return Ok(new JsonCreate { message = ConstMessage.AUTHORIZATION_EXPIRED });
            }*/
            CustomerManager customerManager = new CustomerManager();
            int num = customerManager.getAllCustomerNum();
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = num });
        }

        /// <summary>
        /// 顾客注册
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult registerCustomer([FromForm] RegisterDTO registerDTO)
        {
            CustomerManager customerManager = new CustomerManager();
            return Ok(new JsonCreate { message = customerManager.createCustomer(registerDTO) });
        }
        /// <summary>
        /// 顾客登录
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult loginAccount([FromForm] LoginDTO loginDTO)
        {
            SessionUtil.addTokenToSession(this.HttpContext,loginDTO._id==null?loginDTO._phone:loginDTO._id);
            CustomerManager customerManager = new CustomerManager();
            String result = "";
            if (loginDTO._phone != null)
            {
                result = customerManager.verifyPasswordAndPhone(loginDTO._phone, loginDTO._password);
            }
            else if (loginDTO._id != null)
            {
                result = customerManager.verifyPasswordAndId(loginDTO._id, loginDTO._password);
                
            }
            else
            {
                result = ConstMessage.NOT_FOUND;
            }
            return Ok(new JsonCreate() { message = result});
        }

    }
}
