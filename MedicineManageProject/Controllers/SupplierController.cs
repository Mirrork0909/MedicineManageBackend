using MedicineManageProject.DB.Services;
using MedicineManageProject.DTO;
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
    public class SupplierController: ControllerBase
    {
        /// <summary>
        /// 得到所有的供应商信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult getSupplierAll()
        {
            SupplierManager supplierManager = new SupplierManager();
            List<SupplierDTO> supplierDTOs = supplierManager.getSupplierDTOs();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = supplierDTOs });
        }

        /// <summary>
        /// 得到某个供应商提供的所有药品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/medicine")]
        public IActionResult getAllMedicineBySupplier(int id)
        {
            SupplierManager contractManager = new SupplierManager();
            List<MedicineBySupplierDTO> medicineInformation = contractManager.getAllMedicineBySupplier(id);
            if (medicineInformation.Count!= 0)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = medicineInformation });

            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.NOT_FOUND, data = medicineInformation });
            }
            
        }
        /// <summary>
        /// 按供应商分组，分别得到他们提供的所有药品
        /// </summary>
        /// <returns></returns>
        [HttpGet("medicine/group")]
        public IActionResult getMedicineGroupBySupplier()
        {
            SupplierManager supplierManager = new SupplierManager();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = supplierManager.getMedicineGroupBySupplier() });
        }

        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="supplierDTO"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public IActionResult createSupplier([FromForm]SupplierDTO supplierDTO)
        {
            SupplierManager supplierManager = new SupplierManager();
            SupplierDTO temp = supplierManager.createSupplier(supplierDTO);
            if (supplierDTO != null)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.INSERT_SUCCESS, data = temp });
            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = temp });
            }
        }
        /// <summary>
        /// 更改某个供应商的信息
        /// </summary>
        /// <param name="supplierDTO"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public IActionResult updateSupplier([FromForm]SupplierDTO supplierDTO)
        {
            SupplierManager supplierManager = new SupplierManager();
            bool temp = supplierManager.updateSupplier(supplierDTO);
            if (temp == true)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.UPDATE_SUCCESS, data = temp });
            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.UPDATE_FAIL, data = temp });
            }
        }

        /// <summary>
        /// 删除某个供应商（不太好用）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public IActionResult deleteSupplier(int id)
        {
            SupplierManager supplierManager = new SupplierManager();
            bool flag = supplierManager.deleteSupplier(id);
            if (flag == true)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.UPDATE_SUCCESS, data = null });
            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.UPDATE_FAIL, data = null });
            }
        }


    }

}
