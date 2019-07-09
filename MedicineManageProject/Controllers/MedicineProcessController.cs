using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.Model;
using MedicineManageProject.Utils;
using MedicineManageProject.DB.Services;
using MedicineManageProject.DTO;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineProcessController : ControllerBase
    {
        [HttpPost("problemMedicine")]
        public IActionResult processProblemMedicine([FromForm] ProcessDTO processDTO)
        {
            ProcessManager processManager = new ProcessManager();
            var result = processManager.processProblemMedicine(processDTO);
            if (result)
            {
                return Ok(JsonCreate.newInstance(ConstMessage.INSERT_SUCCESS, result));
            }
            else
            {
                return Conflict(JsonCreate.newInstance(ConstMessage.CONFILICT, result));
            }
        }


        [HttpPost("expiredMedicine")]
        public IActionResult processExpiredMedicine([FromForm] ProcessDTO processDTO)
        {
            ProcessManager processManager = new ProcessManager();
            var result = processManager.processExpiredMedicine(processDTO);
            if (result)
            {
                return Ok(JsonCreate.newInstance(ConstMessage.INSERT_SUCCESS, result));
            }
            else
            {
                return Conflict(JsonCreate.newInstance(ConstMessage.CONFILICT, result));
            }
        }
    }
}