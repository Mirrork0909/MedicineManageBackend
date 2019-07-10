using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineManageProject.Utils;
using Qiniu.Util;
using Qiniu.IO.Model;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QiniuController : ControllerBase
    {
        [HttpGet("token")]
        public IActionResult getToken()
        {
            Mac mac = new Mac(QiniuMessage.AK, QiniuMessage.SK);
            Auth auth = new Auth(mac);
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = QiniuMessage.BUCKET;
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            return Ok(JsonCreate.newInstance(ConstMessage.GET_SUCCESS, token));
        }
    }
}