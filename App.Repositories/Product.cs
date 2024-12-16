namespace Repositories;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!; // default değeri olmayacak dedik. Default değeri null string ifadelerin. Burada da null olmayacak bir değer geçecek manasında oluyor.
                                                 // Efcore da nullable olmayacak demek bu değer. default!, nullable uyarılarından kaçınmak için kullanılmıştır. Altı çizili gösteriyor ya
                                                 // Bu atamanın güvenli olduğunu garanti ediyorum buraya bir şey gelecek manasında asında
    public decimal Price { get; set; }
    public int Stock { get; set; }
}