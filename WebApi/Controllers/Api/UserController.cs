using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [Route("GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var _User = new { FirstName = "Mohammad", LastName = "Rahimi" };

            return Ok(new { User = _User, Status = "200" });
        }
    }
}
