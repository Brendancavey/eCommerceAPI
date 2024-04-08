using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //navigation properties
        public ICollection<Product>? Products { get; set; }

        public Category() 
        {
            Products = new List<Product>();
        }

    }
}
