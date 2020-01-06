using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class AllTimeController : CurrentTimeController
    {
        // GET api/AllTime
        [HttpGet]
        public IActionResult GetAll()
        {
            var returnVal = new List<CurrentTimeQuery>();

            using (var db = new ClockworkContext())
            {
                returnVal.AddRange(db.CurrentTimeQueries.ToList());
            }

            return Ok(returnVal);
        }
    }
}
