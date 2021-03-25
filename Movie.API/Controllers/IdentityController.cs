using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy: "clientIdPolicy")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return 
            new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
