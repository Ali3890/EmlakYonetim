using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmlakYonetim.Models
{
    [Table("Odemeler")]
    public class Odeme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [MaxLength(50)]
        public string? adSoyad { get; set; }
        [MaxLength(11)]
        public string? KartNumarasi { get; set; }
        public DateOnly SonKulTar { get; set; }
        [MaxLength(3)]
        public string? Cvv { get; set; }
        [MaxLength(50)]
        public string? emlakci { get; set; }
        public string? ilanNo { get; set; }
        public string? EmlakTuru { get; set; }
        public string? KiralamaSuresi { get; set; }

        public Odeme()
        {
        }

        public Odeme(long ID, string? adSoyad, string? kartNumarasi, DateOnly sonKulTar, string? cvv, string? emlakci, string? ilanNo, string? emlakTuru, string? kiralamaSuresi)
        {
            this.ID = ID;
            this.adSoyad = adSoyad;
            KartNumarasi = kartNumarasi;
            SonKulTar = sonKulTar;
            Cvv = cvv;
            this.emlakci = emlakci;
            this.ilanNo = ilanNo;
            EmlakTuru = emlakTuru;
            KiralamaSuresi = kiralamaSuresi;
        }
    }
}
