namespace App.Domain.Options;

public class ConnectionStringOption
{
    // options pattern connectionStringi direk IConfgiguration ile değil de tip güvenlli yaklaşım ile okumamızı sağlıyor. Onu da repository katmanında tanımlamamız lazım.
    public const string Key = "ConnectionStrings";
    public string PostgreSql { get; set; } = default!; // Connectionstringdeki isimle aynı olmalı
}