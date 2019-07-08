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
        public IActionResult updateStaffInformation([FromForm] StaffInformaitonDTO _newStaffInformaiton)
        {
            StaffManager _staffManager = new StaffManager();
            bool _isSuccess = _staffManager.resetStaffInformation(_newStaffInformaiton._id, 
                _newStaffInformaiton._name, _newStaffInformaiton._phone);
            JsonCreate jsonResult = new JsonCreate();
            jsonResult.message = _isSuccess.ToString();
            jsonResult.data = _isSuccess;
            return Ok(jsonResult);
        }
        [HttpPost("update/password")]
        public IActionResult updatePassword([FromForm]StaffInformaitonDTO _staff) 
        {
            StaffManager _staffManager = new StaffManager();
            bool _isSuccess = _staffManager.resetStaffPassword(_staff._id, _staff._password);
            JsonCreate _jsonResult = new JsonCreate();
            _jsonResult.message = _isSuccess.ToString();
            _jsonResult.data = _isSuccess;
            return Ok(_jsonResult);
        }
        [HttpGet("{id}")]
        public IActionResult getStaffInformation(String id)
        {
            JsonCreate _jsonResult = new JsonCreate();
            StaffManager _staffManager = new StaffManager();
            STAFF _staff = _staffManager.getStaffInformation(id);
            if (_staff != null)
            {
                StaffInformaitonDTO _staffInformation = new StaffInformaitonDTO();
                _staffInformation._name = _staff.NAME;
                _staffInformation._phone = _staff.PHONE;
                _jsonResult.data = _staffInformation;
                return Ok(_jsonResult);
            }
            else
            {
                _jsonResult.message = "不存在该信息";
                return NotFound(_jsonResult);
            }
            
        }


    }
}
