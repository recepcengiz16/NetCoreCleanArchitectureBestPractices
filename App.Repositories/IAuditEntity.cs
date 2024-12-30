namespace Repositories;

public interface IAuditEntity // veritabanında ne zaman oluştu veya ne zaman güncellendi bilgisi için yazdık bu interface i
{
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}