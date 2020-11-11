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
    public class MandlBrotController : ControllerBase
    {
        
        [HttpPost]
        [Route("ap/mandlbrot")]
        public ActionResult ggg([FromBody]CalculationRequest calculationRequest)  //kalkulier ma den scheiß
        {


            return Ok(4);
        }
    }
}