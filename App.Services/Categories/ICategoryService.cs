using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Categories.Update;

namespace Services.Categories;

public interface ICategoryService
{
    Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
    Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest createCategoryRequest);
    Task<ServiceResult> UpdateAsync(int categoryId, UpdateCategoryRequest updateCategoryRequest);
    Task<ServiceResult> DeleteAsync(int categoryId);
    Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
    Task<ServiceResult<CategoryDto>> GetByIdAsync(int categoryId);
    Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
}