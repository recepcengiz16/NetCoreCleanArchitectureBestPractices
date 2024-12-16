namespace Repositories;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!; // default değeri olmayacak dedik. Default değeri null. Burada da null olmayacak bir değer geçecek manasında oluyor.
                                                 // Efcore da nullable olmayacak demek bu değer. default!, nullable uyarılarından kaçınmak için kullanılmıştır. Altı çizili gösteriyor ya
                                                 
    public decimal Price { get; set; }
    public int Stock { get; set; }
}