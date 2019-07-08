using MedicineManageProject.DB.Services;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController:ControllerBase
    {
        [HttpGet("get")]
        public IActionResult getAllContractInfo()
        {          
            ContractManager contractManager = new ContractManager();
            return Ok(contractManager.getAllContractInformation());
        }
        [Route("{id}")]
        public IActionResult getOneContractItem(int id)
        {
            ContractManager contractManager = new ContractManager();
            return Ok(contractManager.getOneContractItem(id));
        }
    }
}
