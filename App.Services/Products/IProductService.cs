using Services.Products.Update;
using Services.Products.UpdateStock;

namespace Services.Products.Create;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(int productId, UpdateProductRequest request);
    Task<ServiceResult> DeleteAsync(int productId);
    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
}