using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.DTO;
using MedicineManageProject.DB.Services;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        [HttpPost("insert")]
        public IActionResult insertNewMedicineInfo([FromForm] MedicineInformation medicine)
        {
            if(medicine == null)
            {
                return BadRequest();
            }
            MedicineManager medicineManager = new MedicineManager();
            medicineManager.insertNewMedicine(medicine);
            return Ok("成功了");
        }

    }
}