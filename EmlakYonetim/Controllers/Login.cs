using EmlakYonetim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EmlakYonetim.Controllers
{
    public class Login : Controller
    {
        private VeriTabani vt;
        public Login(VeriTabani vt)
        {
            this.vt = vt;
        }
        public IActionResult Index()
        {   
            return View();
        }

        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(Kullanici ke)
        {
            if (ke != null)
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    // Şifreyi UTF-8 olarak byte dizisine dönüştürüp hash hesaplıyoruz.
                    byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(ke.Sifre));
                    // Hash değerini Base64 formatında bir string'e dönüştürüyoruz.
                    ke.Sifre = Convert.ToBase64String(hashBytes);
                }

                // Veritabanına kullanıcıyı ekleme
                await vt.AddAsync(ke);
                await vt.SaveChangesAsync(); // Değişiklikleri kaydet

                // Kaydın başarılı olduğunda giriş sayfasına yönlendirme
                return RedirectToAction("Index","Login");
            }
            return View(ke); // Model geçerli değilse formu tekrar göster
        }

        public async Task<IActionResult> GirisYap(Kullanici k,Admin a)
        {
            if (!string.IsNullOrEmpty(k.ePosta) && !string.IsNullOrEmpty(k.Sifre) && !string.IsNullOrEmpty(a.Eposta) && !string.IsNullOrEmpty(a.Sifre))
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    // Şifreyi UTF-8 olarak byte dizisine dönüştürüp hash hesaplıyoruz.
                    byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(a.Sifre));
                    byte[] hashBytes2 = sha512.ComputeHash(Encoding.UTF8.GetBytes(k.Sifre));
                    // Hash değerini Base64 formatında bir string'e dönüştürüyoruz.
                    a.Sifre = Convert.ToBase64String(hashBytes);
                    k.Sifre = Convert.ToBase64String(hashBytes2);
                }


                Admin a2 = await vt.Adminler.FirstOrDefaultAsync(x => x.Eposta.Equals(a.Eposta) && x.Sifre.Equals(a.Sifre));
                Kullanici k2 = await vt.Kullanicilar.FirstOrDefaultAsync(x => x.ePosta.Equals(k.ePosta) && x.Sifre.Equals(k.Sifre));
                if (k2 != null)
                {
                      HttpContext.Session.SetInt32("kullaniciID", Convert.ToInt32(k2.ID));
                    HttpContext.Session.SetString("Ad", k2.Ad);
                    HttpContext.Session.SetString("Eposta", k2.ePosta);
                    // HttpContext.Session.SetString("KullaniciID", k2.ID.ToString());

                    return RedirectToAction("Index", "EmlakYonetim", new { ID = k2.ID });
                }
               
                else if (a2 != null)
                {
                    HttpContext.Session.SetString("AdminID", a2.ID.ToString());
                    HttpContext.Session.SetString("Eposta", a2.Eposta);
                    //Oturum açılacak
                    return RedirectToAction("Index", "Admin", new { ID = a2.ID });
                }
                else
                {
                    TempData["Hata"] = "Hatalı Kullanıcı Adı Ya da Şifre";
                    return RedirectToAction("Index", "Login");
                }
            }

            

            return RedirectToAction("Index", "Login");

        }

        public IActionResult OturumuKapat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "EmlakYonetim");
        }
    }
}
