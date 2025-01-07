using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class Product: BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; } = default!; // default değeri olmayacak dedik. Default değeri null string ifadelerin. Burada da null olmayacak bir değer geçecek manasında oluyor.
                                                 // Efcore da nullable olmayacak demek bu değer. default!, nullable uyarılarından kaçınmak için kullanılmıştır. Altı çizili gösteriyor ya
                                                 // Bu atamanın güvenli olduğunu garanti ediyorum buraya bir şey gelecek manasında asında
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!; // Bir product ın illa ki bir kategorisi olmalı
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}