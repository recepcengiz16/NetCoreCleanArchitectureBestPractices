using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class GenericRepository<T>(AppDbContext dbContext) : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext Context = dbContext; // miras alınan sınıflarda da kullanabilelim diye
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();
    
    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();
   

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
   

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
   

    public void Update(T entity) => _dbSet.Update(entity);
   

    public void Delete(T entity) => _dbSet.Remove(entity);
    
}