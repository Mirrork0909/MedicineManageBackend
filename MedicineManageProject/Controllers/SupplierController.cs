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

    }

}
