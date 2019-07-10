using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.DTO;
using MedicineManageProject.Utils;
using MedicineManageProject.DB.Services;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        /// <summary>
        /// 获得所有反馈信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult getAllFeedBacks()
        {

            var feedbackManager = new FeedbackManager();
            List<FeedbackDTO> result = feedbackManager.getAllFeedBack();
            return Ok(JsonCreate.newInstance(ConstMessage.GET_SUCCESS, result));
        }

        /// <summary>
        /// 发送一条反馈
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost("insertFeedback")]
        public IActionResult insertNewFeedback([FromForm] FeedbackDTO feedback)
        {
            if(feedback == null)
            {
                return BadRequest(JsonCreate.newInstance(ConstMessage.BAD_REQUEST, false));
            }
            var feedbackManager = new FeedbackManager();
            bool result = feedbackManager.insertOneFeedBack(feedback);
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