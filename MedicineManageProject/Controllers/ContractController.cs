using MedicineManageProject.DB.Services;
using MedicineManageProject.DTO;
using MedicineManageProject.Model;
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
    public class ContractController:ControllerBase
    {
        [HttpGet("all")]
        public IActionResult getContractAll()
        {          
            ContractManager contractManager = new ContractManager();
            List<ContractDTO> contractDTOs = contractManager.getAllContract();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = contractDTOs });
            //return Ok(contractManager.getAllContractInformation());
        }


        [HttpGet("{contractItemId}")]
        public IActionResult getOneContractItem(int contractItemId)
        {
            ContractManager contractManager = new ContractManager();
            //return Ok(contractManager.getAllContractItem(contractItemId));
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = contractManager.getAllContractItem(contractItemId) });
        }


        [HttpPost("insert")]
        public IActionResult insertNewContract(ContractDTO contractDTO)
        {
            if (contractDTO == null)
            {
                return BadRequest(new JsonCreate() { message = Utils.ConstMessage.BAD_REQUEST, data = false });
            }
            
            ContractManager contractManager = new ContractManager();
            ContractDTO judge = contractManager.insertContract(contractDTO);
            if (judge!=null)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.INSERT_SUCCESS, data = judge });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = false });
            }
        }

        [HttpPost("update")]
        public IActionResult completeOneContract(CompleteContractDTO completeContractDTO)
        {
            ContractManager contractManager = new ContractManager();
            bool flag = contractManager.completeOneContract(completeContractDTO);
            if (flag == true)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.UPDATE_SUCCESS,data=true });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.UPDATE_FAIL, data = false });
            }
        }

        [HttpGet("cost/month")]
        public IActionResult getSumCostByMonth()
        {
            ContractManager contractManager = new ContractManager();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = contractManager.getCostByMonth() });
        } 


    }
}
