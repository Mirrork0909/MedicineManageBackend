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
            CustomerInformationDTO customerInformation = new CustomerInformationDTO();
            if (loginDTO._phone != null)
            {
                result = customerManager.verifyPasswordAndPhone(loginDTO._phone, loginDTO._password);
                customerInformation = customerManager.getCustomerByPhone(loginDTO._phone);
            }
            else if (loginDTO._id != null)
            {
                result = customerManager.verifyPasswordAndId(loginDTO._id, loginDTO._password);
                customerInformation = customerManager.getCustomerById(loginDTO._id);
            }
            else
            {
                result = ConstMessage.NOT_FOUND;
            }
            return Ok(new JsonCreate() { message = result,data = customerInformation});
        }

        /// <summary>
        /// 更新顾客信息
        /// </summary>
        /// <param name="newCustomerInformaiton"></param>
        /// <returns></returns>
        [HttpPost("update/information")]
        public IActionResult updateStaffInformation([FromForm] CustomerInformationDTO newCustomerInformaiton)
        {
            CustomerManager customerManager = new CustomerManager();
            bool isSuccess = customerManager.resetCustomerInformation(newCustomerInformaiton._id,
                newCustomerInformaiton._name, newCustomerInformaiton._phone);
            JsonCreate jsonResult = new JsonCreate();
            jsonResult.message = isSuccess ? ConstMessage.UPDATE_SUCCESS : ConstMessage.UPDATE_FAIL;
            CustomerInformationDTO customerInformation = customerManager.getCustomerById(newCustomerInformaiton._id);
            jsonResult.data = customerInformation;
            return Ok(jsonResult);
        }
        /// <summary>
        /// 修改顾客密码
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("update/password")]
        public IActionResult updatePassword([FromForm]CustomerInformationDTO customer)
        {
            CustomerManager customerManager = new CustomerManager();
            bool isSuccess = customerManager.resetCustomerPassword(customer._id, customer._password);
            JsonCreate jsonResult = new JsonCreate();
            jsonResult.message = isSuccess ? ConstMessage.UPDATE_SUCCESS : ConstMessage.UPDATE_FAIL;
            jsonResult.data = isSuccess;
            return Ok(jsonResult);
        }

    }
}
