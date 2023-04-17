using System.ComponentModel;

namespace ProductApi.ViewModels
{
    public class CategoryViewModel
    {
        [DisplayName("Kategori Id")]
        public int CategoryId { get; set; }

        [DisplayName("Kategori İsmi")]
        public string CategoryName { get; set; }
    }
}
