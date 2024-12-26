using Services.Products;

namespace Services.Categories;

public record CategoryWithProductsDto(int CategoryId, string Name, List<ProductDto> Products);
