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
                return BadRequest(new JsonCreate() { message=Utils.ConstMessage.BAD_REQUEST,data=false});
            }
            MedicineManager medicineManager = new MedicineManager();
            bool judge = medicineManager.insertNewMedicine(medicine);
            if (judge)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.INSERT_SUCCESS, data = true });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = false });
            }
        }

    }
}