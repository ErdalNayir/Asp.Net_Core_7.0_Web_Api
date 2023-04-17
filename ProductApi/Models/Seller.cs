namespace ProductApi.Models
{
    #nullable disable

    public class Seller
    {
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        // Diğer özellikler

        public ICollection<Product> Products { get; set; } // Product sınıfı ile ilişki
    }
}
