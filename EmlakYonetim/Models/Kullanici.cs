using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EmlakYonetim.Models
{
    [Table("Kullanicilar")]
    public class Kullanici
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [MaxLength(50)]
        public string? Ad { get; set; }
        [MaxLength(50)]
        public string? Soyad { get; set; }
        [MaxLength(50)]
        public string? ePosta { get; set; }
        [MaxLength(11)]
        public string? Telefon { get; set; }
        [MaxLength(255)]
        public string? Sifre { get; set; }

        public Kullanici()
        {
        }

        public Kullanici(long ID, string? ad, string? soyad, string? ePosta, string? telefon, string? sifre)
        {
            this.ID = ID;
            Ad = ad;
            Soyad = soyad;
            this.ePosta = ePosta;
            Telefon = telefon;
            Sifre = sifre;
        }

       
    }
}
