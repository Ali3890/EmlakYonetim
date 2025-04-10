using Azure;
using EmlakYonetim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PagedList.Core;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Text;

namespace EmlakYonetim.Controllers
{
    
    public class EmlakYonetimController : Controller
    {
        private VeriTabani vt;

       
        public EmlakYonetimController(VeriTabani vt)
        {
            this.vt = vt;
        }

        public IActionResult Index(string Aranan, int page = 1, int pageSize = 4)
        {
            var Ilanlar = from i in vt.Ilanlar
                          select new
                          {
                              i.ID,
                              Ilanlar = i.Foto + " " + i.EmlakTipi + " Fiyat: " + i.Fiyat + " m²: " + i.MetreKare + " " + i.Il + " " + i.Ilce + " " + i.IlanTarihi
                          };
            ViewBag.Ilanlar = new SelectList(Ilanlar, "ID", "Ilanlar");

            var OneCikanIlanlar = from od in vt.OneCikanIlanlar
                                  join i in vt.Ilanlar on od.IlanID equals i.ID

                                  select new
                                  {
                                      od.ID,
                                      i.Foto,
                                      i.EmlakTipi,
                                      i.Fiyat,
                                      i.MetreKare,
                                      i.Il,
                                      i.Ilce,
                                      i.IlanTarihi
                                  };
            if (!string.IsNullOrEmpty(Aranan))
            {
                Aranan = Aranan.ToUpper();
                @ViewData["Aranan"] = Aranan;
                OneCikanIlanlar = OneCikanIlanlar.Where(od => od.EmlakTipi.Contains(Aranan) || od.Il.Contains(Aranan) || od.Ilce.Contains(Aranan) ).OrderBy(od => od.IlanTarihi);
            }

            var pagedList = OneCikanIlanlar.OrderBy(od => od.IlanTarihi).ToPagedList(page, pageSize);
            if (pagedList.PageCount != 0 && pagedList.PageCount < page)
            {
                return RedirectToAction("Index", new { page = 1 });
            }


            return View(pagedList);
        }
        

        public async Task<IActionResult> Profil(long ID)
        {
            var ga = await vt.Kullanicilar.FindAsync(ID);
            return View(ga);
        }

        [HttpPost]
        public async Task<IActionResult> Profil(Kullanici ga)
        {
            if (ga != null)
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    // Şifreyi UTF-8 olarak byte dizisine dönüştürüp hash hesaplıyoruz.
                    byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(ga.Sifre));
                    // Hash değerini Base64 formatında bir string'e dönüştürüyoruz.
                    ga.Sifre = Convert.ToBase64String(hashBytes);
                }
                vt.Update(ga);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Index", "EmlakYonetim", new { ID = ga.ID });
        }
        public IActionResult Emlaklar()
        {
            var ilanListesi = vt.Ilanlar.ToList();
            return View(ilanListesi);
        }

        public IActionResult EmlaklarDetay(IlanPaylasma i,OneCikanIlanlar s)
        {
            var ilanListesi = vt.Ilanlar.Where(r => r.ID == i.ID || r.ID==s.IlanID ).ToList();
            return View(ilanListesi);
        }

        [HttpGet]
        public IActionResult Randevu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Randevu(Randevu er)
        {
            
                var mevcutRandevu = await vt.Randevular.FirstOrDefaultAsync(r => r.randevuTarihi.Equals(er.randevuTarihi) && r.randevuSaati.Equals(er.randevuSaati));
              if (mevcutRandevu != null)
              {
                ModelState.AddModelError(string.Empty, "Seçtiğiniz saat dolu.Lütfen başka bir saat seçiniz.");
                return View(er);
              }
             await vt.AddAsync(er);
             await vt.SaveChangesAsync();
             return RedirectToAction("Randevularım");
           
        }

        public IActionResult Randevularım( string Aranan, int page = 1, int pageSize = 5)
        {
            if (page < 1)
            {
                return RedirectToAction("Index", new { page = 1 });
            }
            var Randevular = from d in vt.Randevular
                          select d;
            if (!string.IsNullOrEmpty(Aranan))
            {
                //Aranan = Aranan.ToUpper();
                @ViewData["Aranan"] = Aranan;
                Randevular = Randevular.Where(d => d.adSoyad.Contains(Aranan) || d.emlakci.Contains(Aranan) || d.ePosta.Contains(Aranan)) .OrderBy(d => d.ID);
            }


            var pagedList = Randevular.OrderBy(d => d.ID).ToPagedList(page, pageSize);
                if (pagedList.PageCount != 0 && pagedList.PageCount < page)
                {
                    return RedirectToAction("Randevularım", new { page = 1 });
                }
                return View(pagedList);
            


        }

        public async Task<IActionResult> Randevuİptal(long ID)
        {
            var so = await vt.Randevular.FindAsync(ID);
            if (so != null)
            {
                vt.Remove(so);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Randevularım");
        }

        [HttpGet]
        public async Task<IActionResult> RandevuDuzenle(long ID)
        {
            var gr = await vt.Randevular.FindAsync(ID);
            return View(gr);
        }
        [HttpPost]
        public async Task<IActionResult> RandevuDuzenle(Randevu gr)
        {
            if (gr != null)
            {
                vt.Update(gr);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Randevularım");
        }
        public IActionResult Yorumlar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ilanPaylaşma()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ilanPaylaşma(IlanPaylasma i)
        {
            await vt.AddAsync(i);
            await vt.SaveChangesAsync();
            return RedirectToAction("Emlaklar");
        }

        [HttpGet]
        public IActionResult SifremiUnuttum()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Odeme()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Odeme(Odeme o)
        {
            await vt.AddAsync(o);
            await vt.SaveChangesAsync();
            return RedirectToAction("Odemelerim");
        }

        public IActionResult Odemelerim(string Aranan, int page = 1, int pageSize = 5)
        {

            if (page < 1)
            {
                return RedirectToAction("Index", new { page = 1 });
            }
            var Odemeler = from d in vt.Odemeler
                             select d;
            if (!string.IsNullOrEmpty(Aranan))
            {
                //Aranan = Aranan.ToUpper();
                @ViewData["Aranan"] = Aranan;
                Odemeler = Odemeler.Where(d => d.adSoyad.Contains(Aranan) || d.emlakci.Contains(Aranan) || d.ilanNo.Contains(Aranan) || d.EmlakTuru.Contains(Aranan)).OrderBy(d => d.ID);
            }


            var pagedList = Odemeler.OrderBy(d => d.ID).ToPagedList(page, pageSize);
            if (pagedList.PageCount != 0 && pagedList.PageCount < page)
            {
                return RedirectToAction("Odemelerim", new { page = 1 });
            }
            return View(pagedList);
        }
        
        public IActionResult Danismanlar(string Aranan, int page = 1, int pageSize = 5)
        {
            if (page < 1)
            {
                return RedirectToAction("Index", new { page = 1 });
            }
            var Danismanlar = from d in vt.Danismanlar
                           select d;
            if (!string.IsNullOrEmpty(Aranan))
            {
                //Aranan = Aranan.ToUpper();
                @ViewData["Aranan"] = Aranan;
                Danismanlar = Danismanlar.Where(d => d.Ad.Contains(Aranan) || d.Soyad.Contains(Aranan) ).OrderBy(d => d.ID);
            }


            var pagedList = Danismanlar.OrderBy(d => d.ID).ToPagedList(page, pageSize);
            if (pagedList.PageCount != 0 && pagedList.PageCount < page)
            {
                return RedirectToAction("Danismanlar", new { page = 1 });
            }
            return View(pagedList);
        }

        public IActionResult EmlakHaberleri(string arama, int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);

            var haberler = string.IsNullOrEmpty(arama)
                ? vt.Haberler.OrderByDescending(h => h.YayinTarihi).ToPagedList(pageNumber, pageSize)
                : vt.Haberler.Where(h => h.Baslik.Contains(arama) || h.Ozet.Contains(arama)).OrderByDescending(h => h.YayinTarihi).ToPagedList(pageNumber, pageSize);

            ViewData["arama"] = arama;
            return View(haberler);
        }

        public IActionResult Yorum(yorum y)
        {
            // Gönderilen Yorum nesnesinin null olup olmadığını kontrol ediyoruz.
            if (y != null)
            {
               
                // Veritabanına yeni yorumu ekliyoruz.
                vt.Yorumlar.Add(y);

                // Veritabanında yapılan değişiklikleri kaydediyoruz.
                vt.SaveChanges();
            }

            // İşlem tamamlandıktan sonra belirtilen sayfaya yönlendiriyoruz.
            return RedirectToAction("Newsbreak", "Home");
        }

    }
}