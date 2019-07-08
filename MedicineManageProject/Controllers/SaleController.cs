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
    public class SaleController: ControllerBase
    {
        [HttpGet("all/amount/month")]
        public IActionResult getSaleAmountByMonth()
        {
            SalesManager salesManager = new SalesManager();
            List<SalesDataDTO> salesData = salesManager.getSalesAmountByMonth();
            return Ok(addDataToResult(ConstMessage.GET_SUCCESS,salesData));
        }
        [HttpGet("records")]
        public IActionResult getSaleRecords()
        {
            SalesManager salesManager = new SalesManager();
            List < SaleInformationDTO > saleRecords = salesManager.getSaleRecords();
            if (saleRecords != null)
            {
                return Ok(addDataToResult(ConstMessage.GET_SUCCESS, saleRecords));
            }
            else
            {
                return Ok(addDataToResult(ConstMessage.NOT_FOUND, saleRecords));
            }
        }
        public JsonCreate addDataToResult(String message, object data)
        {
            JsonCreate jsonCreate = new JsonCreate();
            jsonCreate.message = message;
            jsonCreate.data = data;
            return jsonCreate;
        }
    }
}
