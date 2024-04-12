

namespace eCommerceAPI.Models
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string Email { get; set;}
        public string Password { get; set;}
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        
        List<Product>? Cart { get; set; }
    }
}
