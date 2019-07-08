using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.DB.Services;
using MedicineManageProject.DTO;
using MedicineManageProject.Utils;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        [HttpGet("all")]
        public IActionResult getDiscountAll()
        {
            DiscountManager discountManager = new DiscountManager();
            List<DiscountDTO> discountDTOs = discountManager.getDiscountDTOs();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = discountDTOs });
        }

        [HttpGet("{medicineId}")]
        public IActionResult getDiscountById(String medicineId)
        {
            DiscountManager discountManager = new DiscountManager();
            List<DiscountDTO> discountDTOs = discountManager.getDiscountDTOById(medicineId);
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = discountDTOs });
        }
        
    }

   


}