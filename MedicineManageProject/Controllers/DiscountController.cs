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
        /// <summary>
        /// 获得所有折扣信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult getDiscountAll()
        {
            DiscountManager discountManager = new DiscountManager();
            List<DiscountDTO> discountDTOs = discountManager.getDiscountDTOs();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = discountDTOs });
        }

        /// <summary>
        /// 获得某个药品的折扣信息(不分批号)
        /// </summary>
        /// <param name="medicineId"></param>
        /// <returns></returns>
        [HttpGet("{medicineId}")]
        public IActionResult getDiscountById(String medicineId)
        {
            DiscountManager discountManager = new DiscountManager();
            List<DiscountDTO> discountDTOs = discountManager.getDiscountDTOById(medicineId);
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = discountDTOs });
        }
        
        /// <summary>
        /// 设置折扣信息
        /// </summary>
        /// <param name="discountDTO"></param>
        /// <returns></returns>
        [HttpPost("setDiscount")]
        public IActionResult setDiscount([FromForm] DiscountDTO discountDTO)
        {
            if(discountDTO == null)
            {
                return BadRequest(new JsonCreate() { message = Utils.ConstMessage.BAD_REQUEST, data = false });
            }
            DiscountManager discount = new DiscountManager();
            bool judge = discount.insertNewDiscount(discountDTO);
            if (judge)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.INSERT_SUCCESS, data = judge });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = false });
            }
        }

        [HttpDelete("deleteDiscount/{discountId}")]
        public IActionResult deleteDiscount(int discountId)
        {
            DiscountManager discountManager = new DiscountManager();
            bool judge = discountManager.deleteDiscount(discountId);
            if (judge)
            {
                return Ok(JsonCreate.newInstance(Utils.ConstMessage.DELETE_SUCCESS, judge));
            }
            else
            {
                return Conflict(JsonCreate.newInstance(Utils.ConstMessage.DELETE_FAIL, judge));
            }
        }
    }

   


}