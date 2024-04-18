using eCommerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace eCommerceAPI.Services.ApplicationUserService
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Register(RegisterViewModel model)
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
            //create user cart
            Cart userCart = new Cart
            {
                User = newUser
            };
            newUser.Cart = userCart;

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

                return ("User successfully registered");
            }
            else
            {
                return ("Error registering");
            }
        }
    }
}
