using eCommerceAPI.Models;
using eCommerceAPI.Services.ApplicationUserService;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _userService;
        public ApplicationUserController(IApplicationUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return Ok(await _userService.Register(model));
        }
    }
}
