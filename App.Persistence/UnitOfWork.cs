using App.Application.Contracts.Persistence;

namespace App.Persistence;

public class UnitOfWork(AppDbContext dbContext): IUnitOfWork
{
    public Task<int> SaveChangesAsync() => dbContext.SaveChangesAsync();
   
}