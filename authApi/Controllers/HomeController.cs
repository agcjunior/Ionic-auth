using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authApi.Controllers
{
    [Route("api/[controller]")]
    public class HomeController: Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get(){
            return Ok(new { id = "1", title = "Titulo"});
        }
    }
}