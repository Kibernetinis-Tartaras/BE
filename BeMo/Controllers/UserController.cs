using BeMo.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeMo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        [HttpGet(Name = "login")]
        public Activity login()
        {
            return new Activity
            {
                Id = 1,
                Distance = 100,
                Type = ActivityType.Bike,
                Start = DateTime.Now.AddDays(-1),
                End = DateTime.Now,
                Elapsed = 100
            };
        }
    }
}
