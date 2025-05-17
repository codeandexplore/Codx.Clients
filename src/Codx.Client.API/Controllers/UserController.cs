using Microsoft.AspNetCore.Mvc;

namespace Codx.Client.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        [HttpGet("get-user-claims")]
        public IActionResult GetUserClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Ok(new
            {
                Claims = claims
            });
        }
    }
}
