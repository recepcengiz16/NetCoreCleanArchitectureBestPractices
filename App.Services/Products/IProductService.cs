using Repositories.Products;

namespace Services.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(int ProductId, UpdateProductRequest request);
    Task<ServiceResult> DeleteAsync(int ProductId);
}