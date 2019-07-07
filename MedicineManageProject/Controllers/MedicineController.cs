using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.DTO;
using MedicineManageProject.DB.Services;
using MedicineManageProject.Utils;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        [HttpPost("insert")]
        public IActionResult insertNewMedicineInfo([FromForm] MedicineDTO medicine)
        {
            if(medicine == null)
            {
                return BadRequest(new JsonCreate() { message="接收对象为空",data=false});
            }
            MedicineManager medicineManager = new MedicineManager();
            bool judge = medicineManager.insertNewMedicine(medicine);
            if (judge)
            {
                return Ok(new JsonCreate() { message = "插入成功", data = true });
            }
            else
            {
                return Conflict(new JsonCreate() { message = "插入失败", data = false });
            }
        }

    }
}