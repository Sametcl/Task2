# Backend Task Projesi

Bu proje, .NET 7 Web API kullanılarak oluşturulmuş bir backend uygulamasıdır.  
Projede kullanıcı yönetimi, ürün işlemleri ve önbellekleme gibi temel backend fonksiyonları uygulanmıştır.

## Kullanılan Teknolojiler

- .NET 7 (ASP.NET Core Web API)  
- C#  
- Entity Framework Core  
- PostgreSQL  
- Redis (Önbellekleme)  
- ASP.NET Core Identity & JWT  
- Swagger  

## Kurulum ve Çalıştırma Adımları

### Ön Gereksinimler

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)  
- PostgreSQL veritabanı sunucusu  
- Redis sunucusu  

### Kurulum

1. **Proje Klonlama:**
```bash
git clone <repository-url>
cd <proje-klasoru>
```

Veritabanı ve Servis Bağlantıları:
API/appsettings.json dosyasını açın ve aşağıdaki bölümleri kendi sunucu bilgilerinizle güncelleyin:
```bash
{
  "ConnectionStrings": {
    "PostgreSqlConnection": "Host=localhost;Port=5432;Database=BackendTaskDb;Username=postgres;Password=SIFRENIZ"
  },
  "CacheSettings": {
    "ConnectionString": "localhost:6379"
  },
  "Jwt": {
    "Key": "COK_GUVENLI_VE_TAHMIN_EDILEMEYECEK_BIR_ANAHTAR_GIRINIZ",
    "Issuer": "https://sizin-adresiniz.com",
    "Audience": "https://sizin-adresiniz.com"
  }
}
```
Veritabanı Migration:
Aşağıdaki komutu proje kök dizininde çalıştırarak veritabanını oluşturun ve şemayı uygulayın:
```bash
add-migration initialcreat
update-database
```
Uygulamayı Çalıştırma:
```bash
dotnet run --project API
```
