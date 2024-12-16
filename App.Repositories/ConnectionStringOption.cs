namespace Repositories;

public class ConnectionStringOption
{
    // options pattern connectionStringi direk IConfgiguration ile değil de tip güvenlli yaklaşım ile okumamızı sağlıyor. Onu da repository katmanında tanımlamamız lazım.
    public string PostgreSql { get; set; } // Connectionstringdeki isimle aynı olmalı
}