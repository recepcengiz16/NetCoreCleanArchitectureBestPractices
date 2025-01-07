using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories;

public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category,int>(dbContext), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductsAsync(int id)
    {
       return dbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x=>x.Id == id);
    }

    public Task<List<Category?>> GetCategoryWithProductsAsync()
    {
        return dbContext.Categories.Include(x => x.Products).ToListAsync();
    }

    // public IQueryable<Category?> GetCategoryWithProducts()
    // {
    //     return dbContext.Categories.Include(x => x.Products).AsQueryable();
    // }
}