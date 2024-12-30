using System.Linq.Expressions;

namespace Repositories;

public interface IGenericRepository<T,in TId> where T : class where TId : struct // in ile belirtince
                                                                                 // Tür parametresi sadece metodlara giriş olarak kullanılıyorsa
                                                                                 // (örneğin, metod parametresi olarak).
                                                                                 // Bu parametreyi değiştirme veya dışarı aktarma (return) gereksinimi yoksa.
{
    Task<bool> AnyAsync(TId id);
    IQueryable<T> GetAll(); // Çünkü bu yaklaşımla where ya da order by manası veri tabanında yapılıp gelsin. Neden async yapadık ne zaman toList dersek
                            // o da servis katmanında
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    
    ValueTask<T?> GetByIdAsync(int id); // Örneğin, bir işlem çoğu zaman hemen tamamlanıyorsa (cached bir veri döndürüyorsanız).
                                        // Yüksek performans gereksinimlerinde gereksiz Task tahsislerini önlemek için ValueTask kullanılır.
                                        // Heap yerine Stack üzerinde oluşturulur. Task ise Heap te oluşturulur.
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}