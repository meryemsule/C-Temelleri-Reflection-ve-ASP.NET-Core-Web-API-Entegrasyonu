# 🚀 Modern C# & ASP.NET Core Web API Integration

Bu proje, **temel C# programlama kavramlarından ileri seviye Reflection tekniklerine ve ASP.NET Core Web API mimarisine** kadar geniş bir konuyu kapsayan kapsamlı bir yazılım geliştirme çalışmasıdır.

Proje aynı çözüm (**Solution**) içinde iki farklı uygulamadan oluşmaktadır:

- 🖥 **Console Application** → C# temel kavramları ve Reflection örnekleri
- 🌐 **ASP.NET Core Web API** → RESTful API mimarisi ve middleware kullanımı

---

# 📚 Project Structure

## 1️⃣ C# Fundamentals & Reflection (Console Application)

Bu bölümde C# dilinin temel yapı taşları ve çalışma zamanında kod analizi üzerine çalışmalar yapılmıştır.

### Struct & Value Types
`Student` struct yapısı kullanılarak **değer tipi davranışları** incelenmiştir.

### Exception Handling
`try-catch-finally` blokları kullanılarak hata yönetimi gerçekleştirilmiştir.

Örnek hatalar:

- `DivideByZeroException`
- `FormatException`

### Attribute Usage
`[Obsolete]` attribute kullanılarak **derleme zamanı uyarıları ve hata senaryoları** test edilmiştir.

### Reflection Report
Custom attribute kullanılarak sınıfların çalışma zamanında analiz edilmesi sağlanmıştır.

Örnek:

[DeveloperInfo]


Reflection sayesinde:

- Attribute içeren sınıflar
- İşaretlenmiş metodlar

dinamik olarak taranarak raporlanır.

---

# 🌐 ASP.NET Core Web API

Bu bölümde **arayüzsüz (headless) bir backend mimarisi** oluşturulmuştur.

## Model Validation

`ProductDto` üzerinde veri doğrulama attribute'ları kullanılmıştır.

Örnekler:

- `[Required]`
- `[Range]`
- `[StringLength]`

---

## CRUD Operations

`ProductsController` üzerinden tam işlevsel **CRUD işlemleri** gerçekleştirilmiştir.

- Create
- Read
- Update
- Delete

Routing yapısı **Attribute-Based Routing** kullanılarak oluşturulmuştur.

---

# ⚙ Middleware & Filter Architecture

Projede ASP.NET Core’un güçlü middleware ve filter mimarisi kullanılmıştır.

### Custom Middleware

HTTP istek ve yanıtları loglanmaktadır.

Örnek:

- Request logging
- Response logging

---

### Action Filter

Action metodlarının **çalışma sürelerini ölçmek** için kullanılmıştır.

---

### Exception Filter

Beklenmeyen hatalar yakalanarak kullanıcıya **standart JSON formatında hata mesajı** döndürülmektedir.

Örnek çıktı:

```json
{
  "status": 500,
  "message": "Unexpected error occurred"
}
🔍 Reflection Endpoint
Sistemdeki tüm controller ve action metodları reflection ile analiz edilerek bir endpoint üzerinden sunulmaktadır.

Endpoint:

/api/system/attribute-map
Bu endpoint:

Controller'ları

Action metodlarını

Attribute kullanımını

JSON formatında döndürür.

🛠 Tech Stack
Teknoloji	Açıklama
C#	Ana programlama dili
.NET	Uygulama platformu
ASP.NET Core	Web API geliştirme
Reflection	Çalışma zamanı kod analizi
DataAnnotations	Model doğrulama
📋 Installation
1️⃣ Repoyu klonlayın
git clone https://github.com/username/csharp-reflection-webapi-project.git
2️⃣ Solution dosyasını açın
Visual Studio ile:

project.sln
dosyasını açın.

▶ Run Console Application
Console uygulamasını çalıştırmak için:

Console projesine sağ tıklayın

Set as Startup Project seçin

Programı çalıştırın

Çalıştırıldığında Reflection Attribute Raporu konsolda görüntülenecektir.

▶ Run Web API
Web API projesini çalıştırdıktan sonra:

http://localhost:[PORT]/api/system/attribute-map
adresine giderek sistemdeki controller ve action metadata bilgilerini JSON formatında görebilirsiniz.

📌 Project Goals
Bu proje aşağıdaki konuları uygulamalı olarak göstermeyi amaçlamaktadır:

C# temel programlama kavramları

Exception handling

Attribute kullanımı

Reflection

ASP.NET Core Web API geliştirme

Middleware ve Filter mimarisi
