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
    public class SaleController : ControllerBase
    {
        [HttpGet("all/amount/month")]
        public IActionResult getSaleAmountByMonth()
        {
            SalesManager salesManager = new SalesManager();
            List<SalesDataByMonthDTO> salesData = salesManager.getSalesAmountByMonth();
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = salesData });
        }
        [HttpGet("ten/records")]
        public IActionResult getTenSaleRecords()
        {
            SalesManager salesManager = new SalesManager();
            List<SaleInformationDTO> saleRecords = salesManager.getSaleRecords();
            if (saleRecords != null)
            {
                return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS,data = saleRecords });
            }
            else
            {
                return Ok(new JsonCreate { message = ConstMessage.NOT_FOUND, data = saleRecords });
            }
        }

        [HttpPost("purchase")]
        public IActionResult purchase(PurchaseDTO purchaseDTO)
        {
            SalesManager salesManager = new SalesManager();
            bool judge = salesManager.purchase(purchaseDTO);
            if (judge)
            {
                return Ok(JsonCreate.newInstance(ConstMessage.INSERT_SUCCESS, judge));
            }
            else
            {
                return Conflict(JsonCreate.newInstance(ConstMessage.CONFILICT, judge));
            }
        }

        [HttpGet("all/records")]
        public IActionResult getAllRecords()
        {
            SalesManager salesManager = new SalesManager();
            object result = salesManager.getAllSaleRecords();
            return Ok(JsonCreate.newInstance(ConstMessage.GET_SUCCESS, result));
        }

       [HttpGet("records/{saleId}")]
       public IActionResult getSaleItemFromOneRecord(Decimal saleId)
        {
            SalesManager salesManager = new SalesManager();
            object result = salesManager.getAllOrderItemOfOneSaleInfo(saleId);
            return Ok(JsonCreate.newInstance(ConstMessage.GET_SUCCESS, result));
        }

        [HttpGet("all/medicine/salesRecord")]
        public IActionResult getAllMedicinSalesData()
        {
            SalesManager salesManager = new SalesManager();
            List<MedicineSaleDataDTO> medicineSaleDatas  = salesManager.getAllMedicineSaleData();
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = medicineSaleDatas });
        }
        [HttpGet("records/customer/{customerId}")]
        public IActionResult getRecordUnderCustomer(String customId)
        {
            SalesManager salesManager = new SalesManager();
            List<SaleInformationDTO> saleInformationDTOs = salesManager.getRecordsUnderCustomer(customId);
            return Ok(new JsonCreate { message = saleInformationDTOs != null && saleInformationDTOs.Count > 0 ? ConstMessage.GET_SUCCESS : ConstMessage.NOT_FOUND, data = saleInformationDTOs });
        }
        

       
    }
}
