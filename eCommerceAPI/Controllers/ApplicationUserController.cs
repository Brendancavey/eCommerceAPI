using eCommerceAPI.DBContext;
using eCommerceAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EcommerceDBContext _context;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, EcommerceDBContext context)
        {
            _userManager = userManager;
            _context = context;
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

            return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet]
        [Route("getcart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCart = await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var listOfProducts = await _context.Products
                .Where(product => userCart.Products.Contains(product)).ToListAsync();

            List<int> listOfProductIds = new List<int>();

            foreach(var product in listOfProducts)
            {
                listOfProductIds.Add(product.Id);
            }
            return Ok(listOfProductIds);
            
        }
        [HttpPut]
        [Route("updatecart")]
        public async Task<IActionResult> UpdateCart([FromForm] Dictionary<string, int> productIds) //key value pair [productId: quantityOfProduct]
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var existingCart = await _context.Carts
                    .Include(c => c.Products)
                    .Include(c => c.CartProducts)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                if (existingCart != null)
                {
                    //clear items in cart
                    existingCart.Products.Clear();

                    //add new items to cart
                    foreach (var stringId in productIds.Keys)
                    {
                        int id = Int32.Parse(stringId);

                        //add item to cart first
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                        existingCart.Products.Add(product);
                        _context.Carts.Update(existingCart);
                        await _context.SaveChangesAsync();

                        //then update quantity to avoid null object reference
                        var entry = existingCart.CartProducts.FirstOrDefault(cp => cp.ProductId == id);
                        entry.Quantity = productIds[stringId];
                    }   
                }
                _context.Carts.Update(existingCart);
                await _context.SaveChangesAsync();
                return Ok("Cart update success");

            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured: {ex.Message}");
            }
            
        }

    }
}
