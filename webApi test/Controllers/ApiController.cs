using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webApi_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        [HttpGet]
        [Route("public")]
        public ActionResult Get()
        {
            return Ok(new { msg = "Hey this is a public methods that can be accessed by anyone" });
        }

        
        [HttpGet]
        [Route("private")]
        [Authorize]
        public ActionResult Getprivate()
        {
            return Ok(new{msg = "Hey this is a private methods you need to be authorized to view this method or string." });
        }
    }
}
