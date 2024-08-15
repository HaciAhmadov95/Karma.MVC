using AutoMapper;
using Karma.MVC.Models;
using Karma.MVC.ViewModels;

namespace Karma.MVC.Profiles;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, GetProductVM>();
        CreateMap<Product, GetProductDetailVM>()
            .ForMember(n => n.CategoryName, c => c.MapFrom(n => n.Category.Name));
    }
}
