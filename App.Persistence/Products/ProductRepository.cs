using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products;

public class ProductRepository(AppDbContext context): GenericRepository<Product,int>(context), IProductRepository
{
    public Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
        // tek satrda dönüyorsak async await yapmamıza gerek yok. Başka işlemler olur o ayrı 
        return Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync(); // miras aldığımız genericrepository sınıfından geldi
                                                                                           // büyük harfli Context, buradaki contexti de kullanabilirdik
                                                                                           // onu da görmüş olduk
    }
}