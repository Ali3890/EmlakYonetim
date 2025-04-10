using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmlakYonetim.Models
{
    [Table("Adminler")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(100)]
        public string? Eposta { get; set; }
        [MaxLength(128)]
        public string? Sifre { get; set; }

        public Admin()
        {
        }

        public Admin(int ıD, string? eposta, string? sifre)
        {
            ID = ıD;
            Eposta = eposta;
            Sifre = sifre;
        }
    }
}
