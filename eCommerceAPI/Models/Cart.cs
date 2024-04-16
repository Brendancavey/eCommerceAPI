using eCommerceAPI.Services.ProductService;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        //navigation properties
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
