using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmlakYonetim.Models
{
    [Table("Randevular")]
    public class Randevu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [MaxLength(50)]
        public string? adSoyad { get; set; }
        [MaxLength(11)]
        public string? tel { get; set; }
        [MaxLength(50)]
        public string? ePosta { get; set; }
        [MaxLength(50)]
        public string? emlakci { get; set; }
        public string? ilanNo { get; set; }
        public DateOnly randevuTarihi { get; set; }
        public TimeOnly randevuSaati { get; set; }

        public Randevu()
        {
        }

        public Randevu(long ID, string? adSoyad, string? tel, string? ePosta, string? emlakci, string? ilanNo, DateOnly randevuTarihi, TimeOnly randevuSaati)
        {
            this.ID = ID;
            this.adSoyad = adSoyad;
            this.tel = tel;
            this.ePosta = ePosta;
            this.emlakci = emlakci;
            this.ilanNo = ilanNo;
            this.randevuTarihi = randevuTarihi;
            this.randevuSaati = randevuSaati;
        }
    }
}
