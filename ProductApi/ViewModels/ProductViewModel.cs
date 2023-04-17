using System.ComponentModel;

namespace ProductApi.ViewModels
{
    public class ProductViewModel
    {
        [DisplayName("Ürün Id")]
        public int? Id { get; set; }

        [DisplayName("Ürün İsmi")]
        public string? Name { get; set; }

        [DisplayName("Ürün Fiyatı")]
        public int Price { get; set; }

        [DisplayName("Ürün Açıklaması")]
        public string? Description { get; set; }

        [DisplayName("Ürün Kategori Id")]
        public int CategoryId { get; set; }

        [DisplayName("Ürün Satıcısı Id")]
        public int SellerId { get; set; }

    }
}
