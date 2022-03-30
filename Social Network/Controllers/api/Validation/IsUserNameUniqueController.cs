using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Network.Controllers.api.Validation
{

    [Route("api/[controller]")]
    [ApiController]
    public class IsUserNameUniqueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IsUserNameUniqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<string[]>> GetRes(string login)
        {

            string[] arr= { "The Login is not unique" };


            var order = _context.Users.Where(x => x.Login == login).FirstOrDefault();

            if (order != null)
            {
                arr = new string[] { "The Login is not free" };
                return arr;
            }
            else
            {
                arr = new string[] { "The Login is unique"};
                return arr;
            }
            
        }
    }
}
