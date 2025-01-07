using System.Linq.Expressions;
using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class GenericRepository<T,TId>(AppDbContext dbContext) : IGenericRepository<T,TId> where T : BaseEntity<TId> where TId : struct
{
    protected readonly AppDbContext Context = dbContext; // miras alınan sınıflarda da kullanabilelim diye
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));
    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => _dbSet.AnyAsync(expression);
    

    public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();
   

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize) => _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    
    
    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();
   

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
   

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
   

    public void Update(T entity) => _dbSet.Update(entity);
   

    public void Delete(T entity) => _dbSet.Remove(entity);
    
}