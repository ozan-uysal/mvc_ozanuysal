using AutoMapper;
using MVC_20211129078_OZANUYSAL.Models;
using MVC_20211129078_OZANUYSAL.ViewModels;

namespace MVC_20211129078_OZANUYSAL.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<AppUser, UserModel>().ReverseMap();
            CreateMap<AppUser, RegisterModel>().ReverseMap();
        }
    }
}
