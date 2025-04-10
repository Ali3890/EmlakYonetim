using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmlakYonetim.Models
{
    [Table("Danismanlar")]
    public class Danisman
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

        public Danisman()
        {
        }

        public Danisman(long ID, string? ad, string? soyad, string? ePosta, string? telefon)
        {
            this.ID = ID;
            Ad = ad;
            Soyad = soyad;
            this.ePosta = ePosta;
            Telefon = telefon;
        }
    }
}