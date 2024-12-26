using Microsoft.EntityFrameworkCore;

namespace Repositories.Categories;

public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductsAsync(int id)
    {
       return dbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x=>x.CategoryId == id);
    }

    public IQueryable<Category?> GetCategoryByProductsAsync()
    {
        return dbContext.Categories.Include(x => x.Products).AsQueryable();
    }
}