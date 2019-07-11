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
    public class SubscribeController: ControllerBase
    {
        /// <summary>
        /// 新增订阅
        /// </summary>
        /// <param name="subscribeDTO"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public IActionResult insertSubsribe([FromForm]SubscribeDTO subscribeDTO)
        {
            SubscribeManager subscribeManager = new SubscribeManager();
            bool flag = subscribeManager.insertSubsribe(subscribeDTO);
            if (flag == true)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.INSERT_SUCCESS, data = true });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.CONFILICT, data = false });
            }
        }

        /// <summary>
        /// 得到某个顾客的订阅
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult getSubscribe(String id)
        {
            SubscribeManager subscribeManager = new SubscribeManager();
            var flag = subscribeManager.getSubscribeDTOs(id);
            if (flag!=null&&flag.Count!= 0)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.GET_SUCCESS, data = flag });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.NOT_FOUND, data = flag });
            }
        }


        /// <summary>
        /// 删除某个用户的某个订阅
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult deleteSubscribe([FromForm]SubscribeDTO s)
        {
            SubscribeManager subscribeManager = new SubscribeManager();
            var flag = subscribeManager.deleteSubscribe(s);
            if (flag==true)
            {
                return Ok(new JsonCreate() { message = Utils.ConstMessage.UPDATE_SUCCESS, data = true });
            }
            else
            {
                return Conflict(new JsonCreate() { message = Utils.ConstMessage.UPDATE_FAIL, data = false });
            }
        }
    }
}
