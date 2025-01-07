namespace App.Application.Features.Products.Dto;

public record ProductDto(int Id,string Name, decimal Price,int Stock,int CategoryId);

// recordlar da arkada classlara dönüşüyor aslında arkada, classların özel hali gibi düşünebiliriz.
// public record ProductDto
// { // recordlar şu manaya yarar. İki record da karşılaştırma yaparken propertyleri karşılaştırır. Ama iki class karşılaştırma yapılırken heapdeki
//   // adresleri karşılaştırılır. Biz de heapdeki adresler değil de propertyler aynı mı diye bakıyoruz. O yüzden record yaptık
//     public int ProductId { get; init; } // init yaparsak o nesne örneğinde bir kere atandıktan sonra değişiklik yapamaz.
//     public string Name { get; init; }
//     public decimal Price { get; init; }
//     public int Stock { get; init; }
//
// }