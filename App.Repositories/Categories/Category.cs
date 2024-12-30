using Repositories.Products;

namespace Repositories.Categories;

public class Category: BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; } = default!;
    
    // navigation property
    public List<Product>? Products { get; set; } // bir kategorinin illa ki products ı olacak diye bir şey yok, o yüzden nullable yaptık
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}