using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
#nullable disable
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Ürün İsmi")]
        public string Name { get; set; }

        [DisplayName("Ürün Fiyatı")]
        public int Price { get; set; }

        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; }

        [DisplayName("Ürün Kategori Id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Category sınıfı ile ilişki

        [DisplayName("Ürün Satıcısı Id")]
        public int SellerId { get; set; }
        public Seller Seller { get; set; } // Seller sınıfı ile ilişki

        [DisplayName("Ürün Eklenme Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
