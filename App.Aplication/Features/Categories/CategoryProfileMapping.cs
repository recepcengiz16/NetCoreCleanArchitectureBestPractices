using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Categories;

public class CategoryProfileMapping: Profile
{
    public CategoryProfileMapping()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<CategoryWithProductsDto, Category>().ReverseMap();
        CreateMap<CreateCategoryRequest,Category>().ForMember(
            x=>x.Name, 
            opt=> opt.MapFrom(y=>y.Name.ToLowerInvariant())); 
        // veritabanında isimler küçük harfle yazılsın diye ToLowerInvariant ı yazdık
        CreateMap<UpdateCategoryRequest,Category>().ForMember(x=>x.Name, 
            opt=>opt.MapFrom(y=>y.Name.ToLowerInvariant()));
    }
}