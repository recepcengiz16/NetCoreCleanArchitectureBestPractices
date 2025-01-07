using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;

namespace App.Application.Features.Categories;

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