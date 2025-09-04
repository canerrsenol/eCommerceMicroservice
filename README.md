# E-Ticaret Mikroservis Projesi

Bu proje, .NET 8 kullanılarak geliştirilmiş, modern ve ölçeklenebilir bir e-ticaret platformudur. Proje, mikroservis mimarisi temel alınarak tasarlanmış olup, her bir servis kendi iş mantığından sorumlu olacak şekilde ayrıştırılmıştır. Bu sayede hem geliştirme süreçleri hızlandırılmış hem de sistemin esnekliği ve dayanıklılığı artırılmıştır.

## Mimarî Yaklaşım

Projede, Clean Architecture prensipleri benimsenmiştir. Bu sayede kodun okunabilirliği, sürdürülebilirliği ve test edilebilirliği en üst düzeye çıkarılmıştır. Her bir mikroservis, kendi veritabanı ve iş mantığına sahip olup, servisler arası iletişim API Gateway üzerinden senkron bir şekilde sağlanmaktadır.

Projenin temelini oluşturan mimari bileşenler şunlardır:

- **API Gateway (Ocelot):** Dış dünyadan gelen tüm istekleri karşılayan ve ilgili mikroservise yönlendiren bir geçit. Bu sayede güvenlik, hız sınırlama (rate limiting) ve önbelleğe alma (caching) gibi işlemler merkezi bir noktadan yönetilmektedir.
- **Identity API:** Kullanıcı kimlik doğrulama ve yetkilendirme işlemlerinden sorumlu olan servis. JWT (JSON Web Token) tabanlı rol bazlı bir yetkilendirme mekanizması kullanmaktadır.
- **Product API:** Ürünlerin listelenmesi, oluşturulması, güncellenmesi ve silinmesi gibi işlemlerden sorumludur.
- **Order API:** Sipariş süreçlerini yöneten, kullanıcıların sipariş oluşturmasını ve geçmiş siparişlerini görüntülemesini sağlayan servistir.

## Kullanılan Teknolojiler

Projenin geliştirilmesinde en güncel ve popüler teknolojiler tercih edilmiştir:

- **Backend:** .NET 8, ASP.NET Core
- **Mimari:** Mikroservis Mimarisi, Clean Architecture
- **API Gateway:** Ocelot
- **Kimlik Doğrulama:** JWT (JSON Web Token)
- **Veritabanı:** Her servisin kendi ihtiyacına göre şekillenebilecek (SQL Server, PostgreSQL, vb.) veritabanı altyapısı
- **Test:** xUnit ile Unit Testing

## Öne Çıkan Özellikler

- **Dayanıklılık ve Güvenilirlik:** Gecikme (latency), hız sınırlama (rate limiting) ve devre kesici (circuit breaker) gibi stratejilerle servislerin dayanıklılığı sağlanmıştır.
- **Güvenlik:** Rol bazlı JWT kimlik doğrulaması ile güvenli bir erişim kontrolü sunulmaktadır.
- **Test Edilebilirlik:** Projenin her katmanı için birim testleri (unit tests) yazılarak kod kalitesi güvence altına alınmıştır.
- **Ölçeklenebilirlik:** Mikroservis mimarisi sayesinde her bir servis bağımsız olarak ölçeklendirilebilir.
- **Senkron İletişim:** Servisler arasında veri tutarlılığını sağlamak için senkron iletişim desenleri kullanılmıştır.
