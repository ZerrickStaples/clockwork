using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Clockwork.API.Controllers
{
    public class CurrentTimeController : Controller
    {
        [Route("api/[controller]")]
        // GET api/currenttime
        [HttpGet]
        public IActionResult Get()
        {
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now;
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime
            };

            using (var db = new ClockworkContext())
            {
                db.CurrentTimeQueries.Add(returnVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    Console.WriteLine(" - {0}", CurrentTimeQuery.UTCTime);
                }
            }

            return Ok(returnVal);
        }
        [Route("api/[controller]/all")]
        //GET api/currenttime/all
        [HttpGet]
        public IActionResult GetAll()
        {
            var returnVal = new ClockworkContext().CurrentTimeQueries;

            return Ok(returnVal);
        }
    }
}
