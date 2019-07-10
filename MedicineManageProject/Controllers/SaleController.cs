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
        /// <summary>
        /// 按年月分组获得每月的销售额
        /// </summary>
        /// <returns></returns>
        [HttpGet("all/amount/month")]
        public IActionResult getSaleAmountByMonth()
        {
            SalesManager salesManager = new SalesManager();
            List<SalesDataByMonthDTO> salesData = salesManager.getSalesAmountByMonth();
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = salesData });
        }
        /// <summary>
        /// 获取前10条订单
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 获取所有销售订单（包含是否退货）
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 获取所有药品的销量信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("all/medicine/salesRecord")]
        public IActionResult getAllMedicinSalesData()
        {
            SalesManager salesManager = new SalesManager();
            List<MedicineSaleDataDTO> medicineSaleDatas  = salesManager.getAllMedicineSaleData();
            return Ok(new JsonCreate { message = ConstMessage.GET_SUCCESS, data = medicineSaleDatas });
        }
        /// <summary>
        /// 获取某顾客的所有订单
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("records/customer/{customerId}")]
        public IActionResult getRecordUnderCustomer(String customerId)
        {
            SalesManager salesManager = new SalesManager();
            List<SaleInformationDTO> saleInformationDTOs = salesManager.getRecordsUnderCustomer(customerId);
            return Ok(new JsonCreate { message = saleInformationDTOs != null && saleInformationDTOs.Count > 0 ? ConstMessage.GET_SUCCESS : ConstMessage.NOT_FOUND, data = saleInformationDTOs });
        }
        

       
    }
}
