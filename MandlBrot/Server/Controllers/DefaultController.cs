using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpPost]
        [Route("api/client")]
        public ActionResult Aar([FromBody]CalculationRequest calculationRequest)
        {
            return Ok(4);
        }
    }
}