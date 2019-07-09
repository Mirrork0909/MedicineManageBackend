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
        [HttpGet("all")]
        public IActionResult getSupplierAll()
        {
            SupplierManager supplierManager = new SupplierManager();
            List<SupplierDTO> supplierDTOs = supplierManager.getSupplierDTOs();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = supplierDTOs });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/medicine")]
        public IActionResult getAllMedicineBySupplier(int id)
        {
            SupplierManager contractManager = new SupplierManager();
            List<MedicineBySupplierDTO> medicineInformation = contractManager.getAllMedicineBySupplier(id);
            if (medicineInformation != null)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = medicineInformation });

            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.NOT_FOUND, data = medicineInformation });
            }
            
        }

        [HttpGet("medicine/group")]
        public IActionResult getMedicineGroupBySupplier()
        {
            SupplierManager supplierManager = new SupplierManager();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = supplierManager.getMedicineGroupBySupplier() });
        }

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
