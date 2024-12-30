using AutoMapper;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Categories.Update;

namespace Services.Categories;

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