using System.Net;
using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using AutoMapper;


namespace App.Application.Features.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : IProductService
{
    private const string ProductListCacheKey = "ProductListCacheKey";
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);

        //var productsAsDto = products.Select(p => new ProductDto(p.ProductId,p.Name,p.Price,p.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        
        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        // cache aside design patterns da adımlar
        // 1) cache de var mı kontrol et
        // 2) yoksa dbden getir
        // 3) cache koy
        
        // bu yöntemden daha iyisi decarative ve prozy design pattern yöntemi var. Ona da bakabilirsin.
        
        var productListAsCached = await cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);

        if (productListAsCached is not null)
        {
            return ServiceResult<List<ProductDto>>.Success(productListAsCached);
        }
        
        var products = await productRepository.GetAllAsync();
        //var productsAsDto = products.Select(p => new ProductDto(p.ProductId,p.Name,p.Price,p.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        
        await cacheService.AddAsync(ProductListCacheKey, productsAsDto, TimeSpan.FromMinutes(1));

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        int skip = pageSize * (pageNumber - 1);
        var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);
        //var productsAsDto = products.Select(p => new ProductDto(p.ProductId,p.Name,p.Price,p.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int productId)
    {
        var product = await productRepository.GetByIdAsync(productId);

        if (product is null)
        { 
            return ServiceResult<ProductDto?>.Fail("Product not found",HttpStatusCode.NotFound);
        }
        
        //var productAsDto = new ProductDto(product!.ProductId,product.Name,product.Price,product.Stock);
        var productAsDto = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var anyProduct = await productRepository.AnyAsync(x=>x.Name==request.Name);
        if (anyProduct)
        {
            return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır",HttpStatusCode.BadRequest);
        }
        var product = mapper.Map<Product>(request); // elimde bir product nesnesi olmadığı için map içinde generic olarak verdim.
        
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
        //productRepository.AddAsync(product) metodu, Product nesnesini takip edilen bir duruma (tracked state) ekler.
        //Ancak, bu noktada ProductId henüz atanmaz.
        // unitOfWork.SaveChangesAsync() çağrıldığında, Entity Framework değişiklikleri veritabanına yazar. Bu işlem sırasında:
        // Eğer ProductId bir otomatik artırılan (auto-increment) birincil anahtarsa, veritabanı yeni bir kimlik (ID) üretir.
        // Entity Framework, bu üretilen ID'yi otomatik olarak product.ProductId özelliğine doldurur.
        // Bu nedenle, SaveChangesAsync çağrısından sonra, product.ProductId dolu bir değer döner.
    }

    public async Task<ServiceResult> UpdateAsync(int productId, UpdateProductRequest request)
    {
       
        
        var isProductNameExist = await productRepository.AnyAsync(x=>x.Name==request.Name && x.Id!=productId);
        // Guard clauses: Önce bir gardını al. Tüm olumsuz durumları if ile kontrol et else ler olmasın
        // Fast fail: İlk başta olumsuz durumları kontrol edip döndüğümüz yöntem
        if (isProductNameExist)
        {
            return ServiceResult.Fail("Ürün ismi veritabanında bulunmaktadır",HttpStatusCode.BadRequest);
        }
        
        // product.Name= request.Name;
        // product.Price = request.Price;
        // product.Stock = request.Stock;
        
        //product = mapper.Map(request, product); // bu şekilde de mapleme yapabiliriz. Burada da product nesnesi elimde olduğu için generic
        //değil bu şekilde yaptım.
        var product = mapper.Map<Product>(request); 
        product.Id = productId; // UpdateProductRequest sınıfında Id değeri olmadığı için bu propertyi maplemeyecek bu şekilde elle yazdık.
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent); // güncellemede bir şey dönmüyoruz
    }

    public async Task<ServiceResult> DeleteAsync(int productId)
    {
        var product = await productRepository.GetByIdAsync(productId);
        // if (product is null)
        // {
        //     return ServiceResult.Fail("Product not found",HttpStatusCode.NotFound);
        // }
        productRepository.Delete(product!);
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