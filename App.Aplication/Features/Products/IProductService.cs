using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;

namespace App.Application.Features.Products;

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