using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Update;

namespace Services.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper): ICategoryService
{
    public async Task<ServiceResult<int>> Create(CreateCategoryRequest createCategoryRequest)
    {
        var anyCategory = await categoryRepository.Where(x=>x.Name == createCategoryRequest.Name).AnyAsync();
        if (anyCategory)
        {
            return ServiceResult<int>.Fail("Kategori ismi veritabanında bulunmaktadır.",HttpStatusCode.NotFound);
        }
        
        var newCategory = new Category { Name = createCategoryRequest.Name };
        await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.Success(newCategory.CategoryId);

    }

    public async Task<ServiceResult> UpdateAsync(int categoryId, UpdateCategoryRequest updateCategoryRequest)
    {
        var category = await categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
        {
            return ServiceResult.Fail("Güncellenecek kategori bulunamadı", HttpStatusCode.NotFound);
        }
        
        var isCategoryNameExist = await categoryRepository.Where(x=>x.Name == updateCategoryRequest.Name && x.CategoryId != category.CategoryId).AnyAsync();
        if (isCategoryNameExist)
        {
            return ServiceResult.Fail("Kategori ismi veritabanında bulunmaktadır", HttpStatusCode.BadRequest);
        }
        
        category = mapper.Map(updateCategoryRequest, category);
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}