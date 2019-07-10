using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.Utils;


namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QiniuController : ControllerBase
    {
        [HttpGet("token")]
        public IActionResult getToken()
        {
            QiniuManager qiniuManager = new QiniuManager();
            var token = qiniuManager.getQiniuToken();
            return Ok(JsonCreate.newInstance(ConstMessage.GET_SUCCESS, token));
        }
    }
}