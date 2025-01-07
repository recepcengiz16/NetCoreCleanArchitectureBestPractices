using App.Domain.Entities;

namespace App.Application.Contracts.Persistence;

public interface ICategoryRepository: IGenericRepository<Category,int> 
{
    // repositoryler entity alır ve geriye entity döner, servis katmanındakiler dto alır ve geriye dto döner
    Task<Category?> GetCategoryWithProductsAsync(int id);
    Task<List<Category?>> GetCategoryWithProductsAsync(); 
}