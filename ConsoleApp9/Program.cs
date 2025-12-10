using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

// Bölüm 1: Struct ve Değer Tipleri
public struct Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Gpa { get; set; }

    public override string ToString() => $"Id={Id}, Name={Name}, Gpa={Gpa:F2}";
}

// Bölüm 1.3: Obsolete kullanımı
public static class LegacyMethods
{
    [Obsolete("Use NewMethodInstead() - warning only", false)]
    public static void OldMethodWarning()
    {
        Console.WriteLine("OldMethodWarning çalıştı (uyarı ile işaretli).");
    }

    // Bu metot kullanılırsa derleme hattı verir (isError = true).
    // Kullanmayın — çağırılmıyor, böylece derleme hatası oluşmaz.
    [Obsolete("This method is removed and will cause a build error if used", true)]
    public static void OldMethodError()
    {
        Console.WriteLine("OldMethodError çalıştı.");
    }
}

// Bölüm 1.4: Custom Attribute
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class DeveloperInfoAttribute : Attribute
{
    public string Developer { get; set; }
    public string Version { get; set; }
    public DeveloperInfoAttribute(string developer, string version)
    {
        Developer = developer;
        Version = version;
    }
}

[DeveloperInfo("Ahmet Örnek", "1.0")]
public class SampleClass
{
    [DeveloperInfo("Ahmet Örnek", "1.0")]
    public void MethodA() { }

    [DeveloperInfo("Ayşe Deneme", "1.1")]
    public void MethodB() { }

    public void MethodC() { }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Bölüm 1.1: Struct ve Değer Tipleri ===");
        var s1 = new Student { Id = 1, Name = "Ali", Gpa = 3.5 };
        var s2 = new Student { Id = 2, Name = "Veli", Gpa = 2.9 };
        var s3 = new Student { Id = 3, Name = "Ayşe", Gpa = 3.8 };

        var list = new List<Student> { s1, s2, s3 };
        Console.WriteLine("Başlangıç listesi:");
        foreach (var s in list) Console.WriteLine(s);

        // Değer tipi davranışı gösterimi
        var copy = list[0];
        copy.Name = "Değişti"; // list[0] değişmez çünkü copy bir kopyadır
        Console.WriteLine("\nKopya üzerinde değişiklik yapıldı:");
        Console.WriteLine($"Kopya: {copy}");
        Console.WriteLine($"List[0] (orijinal): {list[0]}");

        Console.WriteLine("\n=== Bölüm 1.2: Exception Handling ===");
        try
        {
            Console.Write("Bir sayı girin: ");
            var aStr = Console.ReadLine();
            Console.Write("Bölünecek sayı girin: ");
            var bStr = Console.ReadLine();

            int a = int.Parse(aStr ?? throw new FormatException());
            int b = int.Parse(bStr ?? throw new FormatException());

            Console.WriteLine($"Sonuç: {a / b}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Hata: Sıfıra bölme hatası yakalandı.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Hata: Geçersiz format (sayı yerine harf girilmiş olabilir).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Beklenmeyen hata: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Finally bloğu her zaman çalışır — işlem tamamlandı.");
        }

        Console.WriteLine("\n=== Bölüm 1.3: Obsolete Attribute ===");
        LegacyMethods.OldMethodWarning(); // Warning olarak işaretlidir (kullanımda derleyici uyarısı verir)
        // LegacyMethods.OldMethodError(); // Eğer açarsanız, çağrı derleme hatası oluşturur — raporda belirtiniz.

        Console.WriteLine("\n=== Bölüm 1.4: Reflection ile Attribute Raporu ===");
        GenerateAttributeReport();

        Console.WriteLine("\nBitti. Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }

    static void GenerateAttributeReport()
    {
        var asm = Assembly.GetExecutingAssembly();
        var targetType = typeof(SampleClass);
        Console.WriteLine($"Sınıf: {targetType.FullName}");
        var classAttrs = targetType.GetCustomAttributes(false);
        foreach (var ca in classAttrs)
        {
            Console.WriteLine($"  Class Attribute: {ca.GetType().Name} => {FormatAttributeProperties(ca)}");
        }

        var methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        foreach (var m in methods)
        {
            Console.WriteLine($"\n  Method: {m.Name}");
            var attrs = m.GetCustomAttributes(false);
            if (attrs.Length == 0) Console.WriteLine("    (Attribute yok)");
            foreach (var a in attrs)
            {
                Console.WriteLine($"    Attribute: {a.GetType().Name} => {FormatAttributeProperties(a)}");
            }
        }
    }

    static string FormatAttributeProperties(object attr)
    {
        var props = attr.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var parts = new List<string>();
        foreach (var p in props)
        {
            try
            {
                var val = p.GetValue(attr);
                parts.Add($"{p.Name}={val}");
            }
            catch { }
        }
        return string.Join(", ", parts);
    }
}