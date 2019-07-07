using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        [HttpGet("get/{name}")]
        public IActionResult getUserInformation(String name)
        {
            return Ok();

        }
    }
}
