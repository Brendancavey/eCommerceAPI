using Microsoft.AspNetCore.Identity;

namespace eCommerceAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set;}

        [PersonalData]
        public string? Address { get; set; }

        [PersonalData]    
        public string? City { get; set; }

        [PersonalData]
        public string? ZipCode { get; set; }
        
        List<Product> Cart { get; set; }
    }
}
