using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmlakYonetim.Models
{
    [Table("Ilanlar")]
    public class IlanPaylasma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [MaxLength(50)]
        public string? Foto { get; set; }

        public string? Il { get; set; }

        [MaxLength(15)]
        public string? Ilce { get; set; }

        [MaxLength(50)]
        public string? Adres { get; set; }

        public DateOnly IlanTarihi { get; set; }

        public string? EmlakTipi { get; set; }

        [MaxLength(50)]
        public string? Kategori { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Metre Kare must be a positive number.")]
        public int MetreKare { get; set; }

        [Range(0, 1000, ErrorMessage = "Bina Yasi must be a valid number.")]
        public int BinaYasi { get; set; }

        public string? OdaSayisi { get; set; }

        
        public float Fiyat { get; set; }

        [MaxLength(10000)]
        public string? Aciklama{ get; set;}

        public IlanPaylasma()
        {
        }

        public IlanPaylasma(long ID, string? foto, string? ıl, string? ılce, string? adres, DateOnly ılanTarihi, string? emlakTipi, string? kategori, int metreKare, int binaYasi, string? odaSayisi, float fiyat, string? aciklama)
        {
            this.ID = ID;
            Foto = foto;
            Il = ıl;
            Ilce = ılce;
            Adres = adres;
            IlanTarihi = ılanTarihi;
            EmlakTipi = emlakTipi;
            Kategori = kategori;
            MetreKare = metreKare;
            BinaYasi = binaYasi;
            OdaSayisi = odaSayisi;
            Fiyat = fiyat;
            Aciklama = aciklama;
        }
    }
}
