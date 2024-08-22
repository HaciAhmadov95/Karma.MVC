using AutoMapper;
using Karma.MVC.Models;
using Karma.MVC.ViewModels;

namespace Karma.MVC.Profiles;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CreateProductVM, Product>();
        CreateMap<Product, GetProductVM>()
            .ForMember(n => n.BrandName, c => c.MapFrom(n => n.Brand.Name))
            .ForMember(n => n.Images, c => c.MapFrom(n => n.Images));
        CreateMap<Product, GetProductDetailVM>()
            .ForMember(n => n.CategoryName, c => c.MapFrom(n => n.Category.Name));
        CreateMap<CartProduct, CartProductVM>()
            .ForMember(n => n.Title, c => c.MapFrom(n => n.Product.Title))
            .ForMember(n => n.DiscountValue, c => c.MapFrom(n => n.Product.DiscountValue))
            .ForMember(n => n.Price, c => c.MapFrom(n => n.Product.Price))
            .ForMember(n => n.Images, c => c.MapFrom(n => n.Product.Images))
            .ForMember(n => n.TotalPrice, c => c.MapFrom(n => n.Cart.TotalPrice))
            .ForMember(n => n.ProductId, c => c.MapFrom(n => n.Product.Id));
        CreateMap<Image, ImageVM>();
    }
}
