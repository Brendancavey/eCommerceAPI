using eCommerceAPI.DBContext;
using Microsoft.EntityFrameworkCore;

namespace eCommerceAPI.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly EcommerceDBContext _context;

        public CartService(EcommerceDBContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<int, int>> GetCart(string userId)
        {
            Dictionary<int, int> productIdsMap = new Dictionary<int, int>(); //key value pair of [productId : productQuantity]

            var userCart = await _context.Carts
                .Include(c => c.Products)
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var listOfProducts = await _context.Products
                .Where(product => userCart.Products.Contains(product)).ToListAsync();

            //convert to map of [id : quantity] because product objects too large for browser cache
            foreach (var product in listOfProducts)
            {
                var entry = userCart.CartProducts.FirstOrDefault(cp => cp.ProductId == product.Id);
                productIdsMap[product.Id] = entry.Quantity;
            }
            return (productIdsMap);
        }

        public async Task<string> UpdateCart(string userId, Dictionary<string, int> productIdsMap) //key value pair [productId: quantityOfProduct]
        {
            try
            {
                var existingCart = await _context.Carts
                    .Include(c => c.Products)
                    .Include(c => c.CartProducts)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                if (existingCart != null)
                {
                    //clear items in cart
                    existingCart.Products.Clear();

                    //add new items to cart
                    foreach (var stringId in productIdsMap.Keys)
                    {
                        int id = Int32.Parse(stringId);

                        //add item to cart first
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                        existingCart.Products.Add(product);
                        _context.Carts.Update(existingCart);
                        await _context.SaveChangesAsync();

                        //then update quantity to avoid null object reference
                        var entry = existingCart.CartProducts.FirstOrDefault(cp => cp.ProductId == id);
                        entry.Quantity = productIdsMap[stringId];
                    }
                }
                _context.Carts.Update(existingCart);
                await _context.SaveChangesAsync();
                return("Cart update success");
            }
            catch (Exception ex)
            {
                return($"An error occured: {ex.Message}");
            }
        }
    }
}
