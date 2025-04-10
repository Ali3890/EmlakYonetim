using Azure;
using EmlakYonetim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Text;

namespace EmlakYonetim.Controllers
{
    public class AdminController : Controller
    {
        private VeriTabani vt;
        public AdminController(VeriTabani vt)
        {
            this.vt = vt;
        }
       
        public IActionResult Index(int page = 1, int pageSize = 4)
        {
            var Ilanlar = from d in vt.Ilanlar
                          select new
                          {
                              d.ID,
                              Ilanlar = d.EmlakTipi + " - Fiyat: " +Convert.ToInt32 (d.Fiyat) + " - m²: " + d.MetreKare + " - İl: " + d.Il + " - İlçe: " + d.Ilce + " - İlanTarihi: " + d.IlanTarihi
                          };
            ViewBag.Ilanlar = new SelectList(Ilanlar, "ID", "Ilanlar");

            var OneCikanIlanlar = from od in vt.OneCikanIlanlar
                                  join d in vt.Ilanlar on od.IlanID equals d.ID
                                
                                 select new
                                 {
                                     od.ID,
                                     d.Foto,
                                     d.EmlakTipi,
                                     d.Fiyat,
                                     d.MetreKare,
                                     d.Il,
                                     d.Ilce,
                                     d.IlanTarihi
                                 };

            var pagedList = OneCikanIlanlar.OrderBy(od => od.IlanTarihi).ToPagedList(page, pageSize);
            if (pagedList.PageCount != 0 && pagedList.PageCount < page )
            {
                return RedirectToAction("Index", new { page = 1 });
            }
            return View(pagedList);
        }

        public async Task<IActionResult> Ekle(int Ilanid)
        {
            var varmi = vt.OneCikanIlanlar.FirstOrDefault(od => od.IlanID == Ilanid);
            if (varmi == null)
            {
                OneCikanIlanlar od = new()
                {
                    IlanID = Ilanid
                   
                };
                vt.Add(od);
                await vt.SaveChangesAsync();
                TempData["Bildirim"] = "Kayıt Başarıyla Eklendi.";
            }
            else
            {
                TempData["Bildirim"] = "Aynı Kayıt Olduğundan Eklenemedi.";
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Sil(long ID)
        {
            var sd = await vt.OneCikanIlanlar.FindAsync(ID);
            if (sd != null)
            {
                vt.Remove(sd);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

      



        [HttpGet]
        public async Task<IActionResult> AdminDuzenle(int ID)
        {
            var ga = await vt.Adminler.FindAsync(ID);
            return View(ga);
        }

        [HttpPost]
        public async Task<IActionResult> AdminDuzenle(Admin ga)
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
            return RedirectToAction("Index", "Admin", new { ID = ga.ID });
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
        [HttpGet]
        public async Task<IActionResult> OdemeDuzenle(long ID)
        {
            var o = await vt.Odemeler.FindAsync(ID);
            return View(o);
        }
        [HttpPost]
        public async Task<IActionResult> OdemeDuzenle(Odeme o)
        {
            if (o != null)
            {
                vt.Update(o);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Odemelerim","Admin");
        }
        public async Task<IActionResult> Odemeİptal(long ID)
        {
            var so = await vt.Odemeler.FindAsync(ID);
            if (so != null)
            {
                vt.Remove(so);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Odemelerim");
        }

        [HttpGet]
        public IActionResult DanismanEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DanismanEkle(Danisman da)
        {
            await vt.AddAsync(da);
            await vt.SaveChangesAsync();
            return RedirectToAction("Danismanlar");
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
                Danismanlar = Danismanlar.Where(d => d.Ad.Contains(Aranan) || d.Soyad.Contains(Aranan)).OrderBy(d => d.ID);
            }


            var pagedList = Danismanlar.OrderBy(d => d.ID).ToPagedList(page, pageSize);
            if (pagedList.PageCount != 0 && pagedList.PageCount < page)
            {
                return RedirectToAction("Danismanlar", new { page = 1 });
            }
            return View(pagedList);
        }

        public async Task<IActionResult> DanismanSil(long ID)
        {
            var so = await vt.Danismanlar.FindAsync(ID);
            if (so != null)
            {
                vt.Remove(so);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Danismanlar");
        }
        [HttpGet]
        public async Task<IActionResult> DanismanDuzenle(long ID)
        {
            var gr = await vt.Danismanlar.FindAsync(ID);
            return View(gr);
        }
        [HttpPost]
        public async Task<IActionResult> DanismanDuzenle(Danisman gr)
        {
            if (gr != null)
            {
                vt.Update(gr);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("Danismanlar");
        }

        [HttpGet]
        public IActionResult HaberEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HaberEkle(Haber haber, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null && foto.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/haberler");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + foto.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                    }

                    haber.FotoUrl = "/img/haberler/" + uniqueFileName;
                }

                haber.YayinTarihi = DateTime.Now;

                vt.Haberler.Add(haber);
                await vt.SaveChangesAsync();

                return RedirectToAction("HaberListesi");
            }

            return View(haber);
        }

        public IActionResult HaberListesi(string arama, int? page)
        {
            int pageSize = 2; // Sayfa başına gösterilecek haber sayısı
            int pageNumber = page ?? 1; // Sayfa numarası (varsayılan: 1)

            var haberler = string.IsNullOrEmpty(arama)
                ? vt.Haberler.OrderByDescending(h => h.YayinTarihi).ToPagedList(pageNumber, pageSize)
                : vt.Haberler
                    .Where(h => h.Baslik.Contains(arama) || h.Ozet.Contains(arama))
                    .OrderByDescending(h => h.YayinTarihi)
                    .ToPagedList(pageNumber, pageSize);

            ViewData["arama"] = arama;
            return View(haberler);
        }


        [HttpGet("Admin/HaberDuzenle/{id:long}")]
        public async Task<IActionResult> HaberDuzenle(long id)
        {
            var haber = await vt.Haberler.FindAsync(id);
            if (haber == null)
            {
                return NotFound();
            }
            return View(haber);
        }

        [HttpPost("Admin/HaberDuzenle/{id:long}")]
        public async Task<IActionResult> HaberDuzenle(Haber haber, IFormFile yeniFoto)
        {
            if (ModelState.IsValid)
            {
                var mevcutHaber = await vt.Haberler.FindAsync(haber.ID);
                if (mevcutHaber != null)
                {
                    mevcutHaber.Baslik = haber.Baslik;
                    mevcutHaber.Ozet = haber.Ozet;

                    if (yeniFoto != null && yeniFoto.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/haberler");

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + yeniFoto.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await yeniFoto.CopyToAsync(fileStream);
                        }

                        mevcutHaber.FotoUrl = "/img/haberler/" + uniqueFileName;
                    }

                    await vt.SaveChangesAsync();
                    return RedirectToAction("HaberListesi");
                }
            }

            return View(haber);
        }
        public async Task<IActionResult> HaberSil(long id)
        {
            var haber = await vt.Haberler.FindAsync(id);
            if (haber != null)
            {
                vt.Haberler.Remove(haber);
                await vt.SaveChangesAsync();
            }
            return RedirectToAction("HaberListesi");
        }
    }
}
