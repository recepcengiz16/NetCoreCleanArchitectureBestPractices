using System.Net;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Products;

namespace Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);

        var productsAsDto = products.Select(p => new ProductDto(p.ProductId,p.Name,p.Price,p.Stock)).ToList();
        
        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productsAsDto = products.Select(p => new ProductDto(p.ProductId,p.Name,p.Price,p.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        int skip = pageSize * (pageNumber - 1);
        var products = await productRepository.GetAll().Skip(skip).Take(pageSize).ToListAsync();
        var productsAsDto = products.Select(p => new ProductDto(p.ProductId,p.Name,p.Price,p.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        { 
            ServiceResult<ProductDto>.Fail("Product not found",HttpStatusCode.NotFound);
        }
        
        var productAsDto = new ProductDto(product!.ProductId,product.Name,product.Price,product.Stock);

        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };
        
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.ProductId), $"api/products/{product.ProductId}");
        //productRepository.AddAsync(product) metodu, Product nesnesini takip edilen bir duruma (tracked state) ekler.
        //Ancak, bu noktada ProductId henüz atanmaz.
        // unitOfWork.SaveChangesAsync() çağrıldığında, Entity Framework değişiklikleri veritabanına yazar. Bu işlem sırasında:
        // Eğer ProductId bir otomatik artırılan (auto-increment) birincil anahtarsa, veritabanı yeni bir kimlik (ID) üretir.
        // Entity Framework, bu üretilen ID'yi otomatik olarak product.ProductId özelliğine doldurur.
        // Bu nedenle, SaveChangesAsync çağrısından sonra, product.ProductId dolu bir değer döner.
    }

    public async Task<ServiceResult> UpdateAsync(int productId, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(productId);
        // Guard clauses: Önce bir gardını al. Tüm olumsuz durumları if ile kontrol et else ler olmasın
        if (product is null) // Fast fail: İlk başta olumsuz durumları kontrol edip döndüğümüz yöntem
        {
            return ServiceResult.Fail("Product not found",HttpStatusCode.NotFound);
        }
        product.Name= request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent); // güncellemede bir şey dönmüyoruz
    }

    public async Task<ServiceResult> DeleteAsync(int productId)
    {
        var product = await productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found",HttpStatusCode.NotFound);
        }
        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found",HttpStatusCode.NotFound);
        }
        product.Stock = request.Quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }  
}