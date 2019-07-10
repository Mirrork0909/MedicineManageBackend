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

        /// <summary>
        /// 得到所有合同的信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult getContractAll()
        {          
            ContractManager contractManager = new ContractManager();
            List<ContractDTO> contractDTOs = contractManager.getAllContract();
            if (contractDTOs != null)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = contractDTOs });
            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = contractDTOs });
            }
            //return Ok(contractManager.getAllContractInformation());
        }

        /// <summary>
        /// 得到某个合同的所有子项信息
        /// </summary>
        /// <param name="contractItemId"></param>
        /// <returns></returns>
        [HttpGet("{contractItemId}")]
        public IActionResult getOneContractItem(int contractItemId)
        {
            ContractManager contractManager = new ContractManager();
            //return Ok(contractManager.getAllContractItem(contractItemId));
            var t = contractManager.getAllContractItem(contractItemId);
            if (t != null)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = t });
            }
            else
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.NOT_FOUND, data = null });
            }
            
        }


        /// <summary>
        /// 添加一个新的合同
        /// </summary>
        /// <param name="contractDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 完成某个合同及其所有子项，添加药品入库
        /// </summary>
        /// <param name="completeContractDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 按月份分组，得到每个月签订的所有合同的总金额
        /// </summary>
        /// <returns></returns>
        [HttpGet("cost/month")]
        public IActionResult getSumCostByMonth()
        {
            ContractManager contractManager = new ContractManager();
            return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = contractManager.getCostByMonth() });
        } 


    }
}
