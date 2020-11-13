using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExchangeLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Server.Controllers
{
    [ApiController]
    public class MandlBrotController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();


        [HttpPost]
        [Route("api/calc")]
        public ActionResult<string> CalcMandlbrot([FromBody] CalculationRequest calculationRequest)  
        {
            //return Ok(new List<(int, int, int)>());
            double xReminder = -2;
            double yReminder = 1.2;

            int maxIterations = 18;
            int maxBetrag = 4;

            //int yPixels = 400;
            //int xPixels = 400;

            // 0.02
            double step = 0.013;

            double parts = 16;

            double fors = Math.Sqrt(parts);

            double yyReminder = yReminder;
            double xxReminder = xReminder;
            double currY = 0;
            double currX = 0;
            double xStep = ((double)calculationRequest.Width) / fors;
            double yStep = ((double)calculationRequest.Height) / fors;

            List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>();

            List<(int, int, int)> coordinatedValues = new List<(int, int, int)>();

            for (int k = 0; k < fors; k++)
            {
                
                
                for (int i = 0; i < fors; i++)
                {
                    

                    CalculationPartRequest cpr = new CalculationPartRequest((int)currX, (int)(currX + xStep), (int)currY, (int)(currY + yStep), xxReminder, step, yyReminder, step, maxIterations, maxBetrag);

                    var json = JsonConvert.SerializeObject(cpr);

                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    //Task<Task<HttpResponseMessage>> task = Task.Factory.StartNew(async () => { return await client.PostAsync("https://localhost:44364/api/calcpart", data); });
                    //Func<Task<HttpResponseMessage>> tas = (() => { return client.PostAsync("https://localhost:44364/api/calcpart", data); });
                    
                    
                    /*Task<HttpResponseMessage> task = client.PostAsync("https://localhost:44364/api/calcpart", data);*/

                    MandlBrotCalcer calcer = new MandlBrotCalcer();

                    List<(int, int, int)> list = calcer.CalcPart(cpr.XMin, cpr.XMax, cpr.YMin, cpr.YMax, cpr.MaxIterations, cpr.MaxBetrag, cpr.XCurr, cpr.YCurr, step);

                    coordinatedValues.AddRange(list);

                    //tasks.Add(task);
                    currX = currX + xStep;
                    xxReminder = xxReminder + (step * xStep);
                }

                xxReminder = xReminder;
                currX = 0;
                currY = currY + yStep;
                yyReminder = yyReminder - (step*yStep);
            }
            

            Task.WaitAll(tasks.ToArray());




            /* for (int j = 0; j < tasks.Count; j++)
             {
                 //Func<string> f = async () => await tasks.ElementAt(j).Result.Content.ReadAsStringAsync();

                 //var respstr = tasks.ElementAt(j).Result.Content.ReadAsStringAsync();

                 //Task.WaitAll(new[] { respstr });

                // var list = JsonConvert.DeserializeObject<List<(int,int,int)>>(respstr); 

                 //List<(int,int,int)> list = JsonConvert.DeserializeObject<List<(int, int, int)>>(JsonConvert.DeserializeObject<List<(int,int,int)>>(tasks.ElementAt(j).Result.Result.Content));

                 //coordinatedValues.AddRange(list);
             }*/

            // return Ok(coordinatedValues);

            var json1 = JsonConvert.SerializeObject(coordinatedValues);
            return Ok(json1);
        }

        [HttpPost]
        [Route("api/calcpart")]
        public ActionResult CalcMandlbrotPart([FromBody] CalculationPartRequest calculationPartRequest)
        {
            MandlBrotCalcer calcer = new MandlBrotCalcer();
            CalculationPartRequest cpr = calculationPartRequest;

            List<(int,int,int)> list = calcer.CalcPart(cpr.XMin, cpr.XMax, cpr.YMin, cpr.YMax, cpr.MaxIterations, cpr.MaxBetrag, cpr.XCurr, cpr.YCurr, cpr.XStep);
            
            return Ok(list);
                       
        }

    }
}