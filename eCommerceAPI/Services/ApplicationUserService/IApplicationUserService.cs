using eCommerceAPI.Models;

namespace eCommerceAPI.Services.ApplicationUserService
{
    public interface IApplicationUserService
    {
        public Task<string> Register(RegisterViewModel model);
    }
}
