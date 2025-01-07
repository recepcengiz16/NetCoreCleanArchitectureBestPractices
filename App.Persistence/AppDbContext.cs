using System.Reflection;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)

{
        // burada bir tane dbContext olduğu için generic şekinde yazmayabiliriz de. DbContextOptions options gibi
        // primary constructor geldi .Net 8.0 ile beraber. Onu da parantez içinde yazıyoruz yukarıdaki gibi
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // entity classların configurasyon ayarlarlamalarını yapması için:
                                                                                           // ProductConfigurationdaki ayarlar mesela
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
}