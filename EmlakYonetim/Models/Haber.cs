using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmlakYonetim.Models
{
    [Table("Haberler")]
    public class Haber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [MaxLength(100)]
        public string? Baslik { get; set; }

        [MaxLength(500)]
        public string? Ozet { get; set; }

        public DateTime YayinTarihi { get; set; }

        [MaxLength(200)]
        public string? FotoUrl { get; set; }

        public Haber()
        {
        }

        public Haber(long ID, string? baslik, string? ozet, DateTime yayinTarihi, string? fotoUrl)
        {
            this.ID = ID;
            Baslik = baslik;
            Ozet = ozet;
            YayinTarihi = yayinTarihi;
            FotoUrl = fotoUrl;
        }
    }
}