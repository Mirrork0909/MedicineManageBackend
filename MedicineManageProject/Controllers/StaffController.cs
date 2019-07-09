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
    public class StaffController:ControllerBase
    {
        [HttpPost("update/information")]
        public IActionResult updateStaffInformation([FromForm] StaffInformaitonDTO newStaffInformaiton)
        {
            StaffManager staffManager = new StaffManager();
            bool isSuccess = staffManager.resetStaffInformation(newStaffInformaiton._id, 
                newStaffInformaiton._name, newStaffInformaiton._phone);
            JsonCreate jsonResult = new JsonCreate();
            jsonResult.message = isSuccess ? ConstMessage.UPDATE_SUCCESS : ConstMessage.UPDATE_FAIL;
            jsonResult.data = isSuccess;
            return Ok(jsonResult);
        }
        [HttpPost("update/password")]
        public IActionResult updatePassword([FromForm]StaffInformaitonDTO staff) 
        {
            StaffManager staffManager = new StaffManager();
            bool isSuccess = staffManager.resetStaffPassword(staff._id, staff._password);
            JsonCreate jsonResult = new JsonCreate();
            jsonResult.message = isSuccess ? ConstMessage.UPDATE_SUCCESS : ConstMessage.UPDATE_FAIL;
            jsonResult.data = isSuccess;
            return Ok(jsonResult);
        }
        [HttpGet("{id}")]
        public IActionResult getStaffInformation(String id)
        {
            JsonCreate jsonResult = new JsonCreate();
            StaffManager staffManager = new StaffManager();
            STAFF staff = staffManager.getStaffInformation(id);
            if (staff != null)
            {
                StaffInformaitonDTO staffInformation = new StaffInformaitonDTO();
                staffInformation._name = staff.NAME;
                staffInformation._phone = staff.PHONE;
                jsonResult.message = ConstMessage.GET_SUCCESS;
                jsonResult.data = staffInformation;
                return Ok(jsonResult);
            }
            else
            {
                jsonResult.message =ConstMessage.NOT_FOUND;
                return NotFound(jsonResult);
            }
        }
        [HttpPost("register")]
        public IActionResult registerStaff([FromForm] RegisterDTO registerDTO)
        {
            StaffManager staffManager = new StaffManager();
            return Ok(new JsonCreate { message = staffManager.createStaff(registerDTO) });
        }
        [HttpPost("login")]
        public IActionResult loginAccount([FromForm] LoginDTO loginDTO)
        {
            SessionUtil.addTokenToSession(this.HttpContext, loginDTO._id == null ? loginDTO._phone : loginDTO._id);
             StaffManager  staffManager = new  StaffManager();
            String result = "";
            if (loginDTO._phone != null)
            {
                result =  staffManager.verifyPasswordAndPhone(loginDTO._phone, loginDTO._password);
            }
            else if (loginDTO._id != null)
            {
                result =  staffManager.verifyPasswordAndId(loginDTO._id, loginDTO._password);
            }
            else
            {
                result = ConstMessage.NOT_FOUND;
            }
            return Ok(new JsonCreate() { message = result });
        }


    }
}
