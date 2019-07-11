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
        /// <summary>
        /// 新建一个药品信息
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获得所有药品相关信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Info/all")]
        public IActionResult getMedicineInfoAll()
        {
            MedicineManager medicineManager = new MedicineManager();
            object result = medicineManager.getMedicineInfoAll();
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = result });
        }

        /// <summary>
        /// 根据药品编号和批号获取信息
        /// </summary>
        /// <param name="medicineId"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        [HttpGet("Info/{medicineId}/{batchId}")]
        public IActionResult getMedicinInfoById(String medicineId,String batchId)
        {
            MedicineManager medicineManager = new MedicineManager();
            object result = medicineManager.getMedicineInfoById(medicineId,batchId);
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = result });
        }

        /// <summary>
        /// 更新药品信息
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns></returns>
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
                return Ok(new JsonCreate() { message = Utils.ConstMessage.UPDATE_SUCCESS, data = true });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = false });
            }
        }
        /// <summary>
        /// 按名称关键字搜索药品信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 得到所有药品以及供应商名字信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllMedicineWithSupplier")]
        public IActionResult getAllMedicineWithSupplier()
        {
            MedicineManager medicineManager = new MedicineManager();
            var r = medicineManager.getAllMedicineWithSupplier();
            if (r != null && r.Count != 0)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = r });
            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.NOT_FOUND, data = null });
            }
        }

        /// <summary>
        /// 根据MedicineId去MEDICINE_INSTANCE表里找出符合条件的所有药
        /// </summary>
        /// <param name="mId"></param>
        /// <returns></returns>
        [HttpGet("getMedicineInstanceById/{mId}")]
        public IActionResult getMedicineInstanceById(String mId)
        {
            MedicineManager medicineManager = new MedicineManager();
            var r = medicineManager.getMedicineInstanceById(mId);
            if (r != null && r.Count != 0)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = r });
            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.NOT_FOUND, data = null });
            }
        }
    }
}