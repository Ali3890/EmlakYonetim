using Microsoft.EntityFrameworkCore;

namespace EmlakYonetim.Models
{
    public class VeriTabani:DbContext
    {
        public VeriTabani(DbContextOptions<VeriTabani> options) : base(options)
        {

        }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<IlanPaylasma> Ilanlar { get; set; }
        public DbSet<Odeme> Odemeler { get; set; }
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<OneCikanIlanlar> OneCikanIlanlar { get; set; }
        public DbSet<Danisman> Danismanlar { get; set; }
        public DbSet<Haber> Haberler { get; set; }
        public DbSet<yorum> Yorumlar { get; set; }
    }
}
