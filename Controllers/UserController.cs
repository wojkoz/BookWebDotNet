using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> users = 


            return Ok(users);
        }
    }
}
