namespace eCommerceAPI.Models
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isNew { get; set; }
        public int price { get; set; }
        public int salePrice { get; set; }
        public int[] SelectedCategoryIds { get; set; } 
    }
}
