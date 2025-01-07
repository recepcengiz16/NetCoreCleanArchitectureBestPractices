using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Products;

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