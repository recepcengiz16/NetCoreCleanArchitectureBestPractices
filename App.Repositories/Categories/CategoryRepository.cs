using Microsoft.EntityFrameworkCore;

namespace Repositories.Categories;

public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category,int>(dbContext), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductsAsync(int id)
    {
       return dbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x=>x.Id == id);
    }

    public IQueryable<Category?> GetCategoryWithProducts()
    {
        return dbContext.Categories.Include(x => x.Products).AsQueryable();
    }
}