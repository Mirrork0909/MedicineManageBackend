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
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = salesData });
        }
        [HttpGet("records")]
        public IActionResult getTenSaleRecords()
        {
            SalesManager salesManager = new SalesManager();
            List < SaleInformationDTO > saleRecords = salesManager.getSaleRecords();
            if (saleRecords != null)
            {
                return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS,data = saleRecords });
            }
            else
            {
                return Ok(new JsonCreate { message = ConstMessage.NOT_FOUND, data = saleRecords });
            }
        }
    }
}
