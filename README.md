# Publishing House Management System

Bir yayınevi yönetim sisteminin yapılması

## Getting Started

Bu talimatlar, geliştirme ve test amacıyla projenin bir kopyasını yerel makinenizde çalışır duruma getirecektir.

### Prerequisites

Yazılımı yüklemek için gerekenler

[Repo](https://github.com/Murathanucar/publishing-house-management-system) 'nun indirilmesi

```
.NET 8.0
```
```
xUnit
```
```
Moq
```

### Description

Projede verileri saklamak için file kullanılmıştır. Eğer gerekli olursa database kullanımı için uygundur.
Aşağıda database uygunluğu anlatılacaktır.
WebAPI projesi Swagger üzerinden çalıştırılarak test edilebilir.
Katman olarak Data Access Layer kullanılmıştır. Repository ve model bu katmandadır.
 Data Access Layer, WebAPI projesine Dependency Injection ile "Transient" tipinde dahil edilmiştir.
Küçük bir proje olduğu için çok farkedilmeyecektir fakat bu sayede her servis çağrısında yeni bir instance oluşturulur
ve işi bitince destroy edilir. Bağlayıcılığı en az olan lifetime seçeneğidir.
Test kısmı ayrı proje olarak uygulamaya dahil edilmişir ve üç adet repository tarafında test yazılmıştır.
Test projesine sağ tıklayarak "run test" diyerek çalıştırılabilir.
Repository kısmına tekrar dönersek, repoların birer interface sınıfları tanımlanmıştır. Bu sayede şu an file kullanılsa da
database kullanımı için bu interfaceler implemente edilerek database repoları oluşturulabilir. Proje katmanında herhangi bir
değişikliğe gerek kalmadan Data Access Layer katmanında yapılabilecek bu değişiklik ile tercih edilen depolama yöntemi kullanılabilir.
Yayınevi, yazar ve kitap bilgilerinin CRUD operasyonlarını yapan API endpointleri ile beraber bunların birbiriyle olan ilişkilerine 
göre hepsinin bir arada gösterildiği MERGEDDATA endpointi mevcuttur. MVC yapısına bağlı kalınarak Model ve Controller oluşturulmuştur.
Fakat UI olmadığı için View katmanı dahil edilmemiştir.
SOLID prensipleri gereği repo interface leri generic yazılmamıştır.(Single Responsibility Principle)
Projenin ana dizininde yer alan FileDatabase klasörü içerisinde dummy data oluşturulmuştur.


## Authors

* [Murathan Uçar](https://github.com/Murathanucar)
