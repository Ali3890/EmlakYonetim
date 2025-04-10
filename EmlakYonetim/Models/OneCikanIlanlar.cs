using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmlakYonetim.Models
{
    [Table("OneCikanIlanlar")]
    public class OneCikanIlanlar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [ForeignKey("IlanID")]
        public IlanPaylasma? Ilan { get; set; }
        public long IlanID { get; set; }

    }
}
