using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.DB.Services;
using MedicineManageProject.Utils;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockRecordController : ControllerBase
    {
        /// <summary>
        /// 入库记录获取
        /// </summary>
        /// <returns></returns>
        [HttpGet("in")]
        public IActionResult getAllStockInRecords()
        {
            StockRecordManager stockRecordManager = new StockRecordManager();
            var result = stockRecordManager.getInStcokRecords();
            return Ok(JsonCreate.newInstance(ConstMessage.GET_SUCCESS, result));
        }

        /// <summary>
        /// 出库记录获取
        /// </summary>
        /// <returns></returns>
        [HttpGet("out")]
        public IActionResult getAllStockOutRecords()
        {
            StockRecordManager stockRecordManager = new StockRecordManager();
            var result = stockRecordManager.getOutStcokRecords();
            return Ok(JsonCreate.newInstance(ConstMessage.GET_SUCCESS, result));
        }
    }
}