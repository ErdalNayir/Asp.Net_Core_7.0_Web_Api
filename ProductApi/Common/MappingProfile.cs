using AutoMapper;
using ProductApi.Models;
using ProductApi.ViewModels;

namespace ProductApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //For Products
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
           


            //For Seller
            CreateMap<Seller, SellerViewModel>();
            CreateMap<SellerViewModel, Seller>();
            CreateMap<SellerRegisterViewModel, Seller>();
            CreateMap<SellerLoginViewModel, Seller>();

            //For Categories
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
        }
    }
}
