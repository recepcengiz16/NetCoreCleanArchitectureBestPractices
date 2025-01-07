using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create;

public class CreateProductRequestValidator: AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository; // bunu neden yazdık. Altta metotta kullanabilmek için veritabanı sorgusunda
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository; 
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün ismi gereklidir")
            .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır");
            //.Must(MustUniqueName).WithMessage("Ürün ismi veritabanında bulunmaktadır"); // bu şekilde de veritabanında kontrolünü yapmış olduk senkron olarak.

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Ürün kategori değeri 0'dan büyük olmalıdır");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok adeti 1 ile 100 arasında olmalıdır"); // InclusiveBetween sayısal değerlerde aralık belirtir.
    }

    // 1. yol senkron kontrol
    // private bool MustUniqueName(string name)
    // {
    //     return !_productRepository.Where(x=>x.Name == name).Any(); // niye değilini aldık eğer öyle bir product varsa true dönecek hata olması
    //                                                                // için değilini aldık. Çünkü false olunca hata var manasında belirledik ya
    //     // false bir hata var
    //     // true bir hata yok 
    // }
}