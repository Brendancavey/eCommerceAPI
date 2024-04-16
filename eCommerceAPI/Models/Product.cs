using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public byte[]? img { get; set; }
        public byte[]? img2 { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isNew { get; set; }
        public int price { get; set; }
        public int salePrice { get; set; }


        //navigation properties
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Cart>? Carts { get; set; }

        public Product()
        {
            Categories = new List<Category>();
        }
    }
}
