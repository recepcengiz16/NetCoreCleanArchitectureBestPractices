using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Repositories.Interceptors;

public class AuditDbContextInterceptor: SaveChangesInterceptor // veritabanında güncelleme ya da kaydederken araya giren sınıf yazdık.
// burada da sadece güncelleme ve kaydetme zamanları ile ilgili işlem yapması için yazdık.
{
    
    // burada action delegesini yazdık entitiystate in durumuna göre. Çünkü action parametre alan ve geriye bir değer dönmeyen bir delege ya
    //  buradaki metotlar da parametre alıyor ve void metotlar, böyle olduğunda bu metotları çalıştırsın diye. Readonly yaptık ki ya constructor da
    // ya da bu şekilde model oluşturulurken bu şekilde, sonradan değiştirilmesin diye
    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> _behaviors = new()
    {
        { EntityState.Added, AddBehavior },
        { EntityState.Modified, ModifyBehavior },
    };
    
    private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.Now;
        context.Entry(auditEntity).Property(p=>p.Updated).IsModified = false; // updated ile bir işlem yapmasın çünkü
        // şu an ekleme ile ilgili işlem yapıyoruz. Yani updated ı sql cümleceğine eklemiyor güncelleme yaparken sadece created da güncelleme yapıyor.
        
    }

    private static void ModifyBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Updated = DateTime.Now;
        context.Entry(auditEntity).Property(p => p.Created).IsModified = false;
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            if (entry.Entity is not IAuditEntity auditEntity)  // eğer bu entity IAuditEntity implement eden bir class değil ise devam etsin dedik
            {
                continue;
            }

            if (entry.State is not (EntityState.Added or EntityState.Modified)) continue;
            
            _behaviors[entry.State](entry.Context,auditEntity);
            
            // _behaviors[entry.State] açıklaması
            //
            // entry.State'e göre sözlükten bir işlem (Action<DbContext, IAuditEntity>) alınır. Yani dictionarydeki bir elemana erişmek gibi düşünebilirsin.
            //     Örneğin:
            // Eğer entry.State EntityState.Added ise, bu AddBehavior metodunu işaret eder.
            
            // switch (entry.State)
            // {
            //     case EntityState.Added:
            //        AddBehavior(entry.Context, auditEntity);
            //         break;
            //     case EntityState.Modified:
            //         ModifyBehavior(entry.Context, auditEntity);
            //         break;
            // }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    
}