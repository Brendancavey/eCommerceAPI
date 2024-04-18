using eCommerceAPI.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize]
        [HttpGet]
        [Route("getcart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _cartService.GetCart(userId));

        }
        [Authorize]
        [HttpPut]
        [Route("updatecart")]
        public async Task<IActionResult> UpdateCart([FromForm] Dictionary<string, int> productIdsMap) //key value pair [productId: quantityOfProduct]
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _cartService.UpdateCart(userId, productIdsMap));

        }
    }
}
