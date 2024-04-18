namespace eCommerceAPI.Services.CartService
{
    public interface ICartService
    {
        public Task<Dictionary<int, int>> GetCart(string userId);
        public Task<string> UpdateCart(string userId, Dictionary<string, int> productIdsMap);
    }
}
