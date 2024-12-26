using AutoMapper;
using Repositories.Products;
using Services.Products;
using Services.Products.Create;
using Services.Products.Update;

namespace Services.Mapping.Products;

public class ProductsMappingProfile: Profile
{
    public ProductsMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequest, Product>().ForMember(
            p=>p.Name,  // ilk parametre hedef yani Product
            o => o.MapFrom(p=>p.Name.ToLowerInvariant())); 
        // mapfrom dediği kaynak yani CreateProductRequest
        CreateMap<UpdateProductRequest, Product>().ForMember(
            p=>p.Name, 
            o => o.MapFrom(p=>p.Name.ToLowerInvariant()));
    }
}