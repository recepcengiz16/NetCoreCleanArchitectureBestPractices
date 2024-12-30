using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Categories.Update;

namespace Services.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper): ICategoryService
{
    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
        if (category is null)
        {
            return ServiceResult<CategoryWithProductsDto>.Fail("Kategori bulunamadı",HttpStatusCode.NotFound);
        }
        var mappedCategories = mapper.Map<CategoryWithProductsDto>(category);
        return ServiceResult<CategoryWithProductsDto>.Success(mappedCategories);
    }
    
    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync() // burada da tüm kategoriler ve onlara ait productsları dönüyoruz
    {
        var categories = await categoryRepository.GetCategoryWithProducts().ToListAsync();
        var categoryListAsDto = mapper.Map<List<CategoryWithProductsDto>>(categories);
        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryListAsDto);
    }
    public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAll().ToListAsync();
        var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesDto);
    }
    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int categoryId)
    {
        var category = await categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
        {
            return ServiceResult<CategoryDto>.Fail("Kategori bulunamadı", HttpStatusCode.NotFound);
        }
        var categoryDto = mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryDto);
    }
    public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest createCategoryRequest)
    {
        var anyCategory = await categoryRepository.Where(x=>x.Name == createCategoryRequest.Name).AnyAsync();
        if (anyCategory)
        {
            return ServiceResult<int>.Fail("Kategori ismi veritabanında bulunmaktadır.",HttpStatusCode.NotFound);
        }
        
        var newCategory = mapper.Map<Category>(createCategoryRequest);
        await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.SuccessAsCreated(newCategory.Id,$"api/categories/{newCategory.Id}");

    }
    public async Task<ServiceResult> UpdateAsync(int categoryId, UpdateCategoryRequest updateCategoryRequest)
    {
       
        
        var isCategoryNameExist = await categoryRepository.Where(x=>x.Name == updateCategoryRequest.Name && x.Id != categoryId).AnyAsync();
        if (isCategoryNameExist)
        {
            return ServiceResult.Fail("Kategori ismi veritabanında bulunmaktadır", HttpStatusCode.BadRequest);
        }
        
        var category = mapper.Map<Category>(updateCategoryRequest);
        category.Id = categoryId;
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
    public async Task<ServiceResult> DeleteAsync(int categoryId)
    {
        var category = await categoryRepository.GetByIdAsync(categoryId);
        // if (category is null)
        // {
        //     return ServiceResult.Fail("Kategori bulunamadı", HttpStatusCode.NotFound);
        // }
        categoryRepository.Delete(category!);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}