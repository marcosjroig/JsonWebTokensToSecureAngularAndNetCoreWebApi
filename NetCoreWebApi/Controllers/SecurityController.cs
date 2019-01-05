using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PtcApi.Model;
using PtcApi.Security;

namespace PtcApi.Controllers
{
    [Route("api/[controller]")]
    public class SecurityController: BaseApiController
    {
        private readonly ISecurityManager _securityManager;
        public SecurityController(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

        // POST: api/security/login
        [HttpPost("login")]
        public IActionResult Post([FromBody]AppUser user)
        {
            var auth = _securityManager.ValidateUser(user);
            if (auth.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status200OK, auth);
            }

            return StatusCode(StatusCodes.Status404NotFound,"Invalid User Name/Password.");
        }
    }
}
