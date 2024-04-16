using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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


        //navigation property
        public Cart Cart { get; set; }
    }
}
