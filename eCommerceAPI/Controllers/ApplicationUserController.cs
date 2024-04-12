using eCommerceAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //create user
            var newUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode
            };
            
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                //Add to role
                await _userManager.AddToRoleAsync(newUser, "Member");

                //add new claims
                var newClaims = new List<Claim>
                {
                    new Claim("FirstName", model.FirstName),
                    new Claim("LastName", model.LastName),
                    new Claim("Address", model.Address),
                    new Claim("City", model.City),
                    new Claim("ZipCode", model.ZipCode),
                };
                await _userManager.AddClaimsAsync(newUser, newClaims);

            return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
