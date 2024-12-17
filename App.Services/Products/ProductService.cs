using Repositories.Products;

namespace Services.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    
}