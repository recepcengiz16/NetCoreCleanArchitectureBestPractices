using Repositories.Products;

namespace Repositories.Categories;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    
    // navigation property
    public List<Product>? Products { get; set; } // bir kategorinin illa ki products ı olacak diye bir şey yok, o yüzden nullable yaptık
}