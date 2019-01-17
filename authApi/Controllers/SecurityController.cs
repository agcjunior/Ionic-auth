using authApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace authApi.Controllers
{
  [Route("api/[controller]")]
  public class SecurityController : Controller
  {
    private readonly SecurityManager securityManager;
    public SecurityController(SecurityManager securityManager)
    {
      this.securityManager = securityManager;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] AppUser user)
    {
        var auth = securityManager.ValidateUser(user);
        if (auth.IsAuthenticated)
        {
            return Ok(auth);
        }

        return NotFound(auth);
    }
  }
}