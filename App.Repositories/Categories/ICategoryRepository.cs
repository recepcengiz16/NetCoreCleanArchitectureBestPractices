namespace Repositories.Categories;

public interface ICategoryRepository: IGenericRepository<Category> 
{
    // repositoryler entity alır ve geriye entity döner, servis katmanındakiler dto alır ve geriye dto döner
    Task<Category?> GetCategoryWithProductsAsync(int id);
    IQueryable<Category?> GetCategoryByProductsAsync(); 
}