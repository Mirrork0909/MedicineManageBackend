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


        [HttpGet("Info/all")]
        public IActionResult getMedicineInfoAll()
        {
            MedicineManager medicineManager = new MedicineManager();
            object result = medicineManager.getMedicineInfoAll();
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = result });
        }

        [HttpGet("Info/{medicineId}/{batchId}")]
        public IActionResult getMedicinInfoById(String medicineId,String batchId)
        {
            MedicineManager medicineManager = new MedicineManager();
            object result = medicineManager.getMedicineInfoById(medicineId,batchId);
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = result });
        }

        [HttpPost("update")]
        public IActionResult updateMedicineInfo([FromForm] MedicineDTO medicine)
        {
            if (medicine == null)
            {
                return BadRequest(new JsonCreate() { message = Utils.ConstMessage.BAD_REQUEST, data = false });
            }
            MedicineManager medicineManager = new MedicineManager();
            bool judge = medicineManager.updateMedicineInfo(medicine);
            if (judge)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.INSERT_SUCCESS, data = true });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = false });
            }
        }

        [HttpGet("search/name/{keyword}")]
        public IActionResult getMedicineListByKeyword(String keyword)
        {
            MedicineManager medicineManager = new MedicineManager();
            List<MedicineSearchResultDTO> medicines = medicineManager.getMedicineListByKeyword(keyword);
            return Ok(new JsonCreate
            {
                message = medicines!=null && medicines.Count >0 ?ConstMessage.GET_SUCCESS:ConstMessage.NOT_FOUND,
                data = medicines
            });
        }
    }
}