using System.ComponentModel.DataAnnotations.Schema;

namespace EmlakYonetim.Models
{
    [Table("Yorumlar")]
    public class yorum
    {
        public int ID { get; set; }
        public string YorumMetni { get; set; }
        public int Derecelendirme { get; set; }

        public yorum()
        {
        }

        public yorum(int ID, string yorumMetni, int derecelendirme)
        {
            this.ID = ID;
            YorumMetni = yorumMetni;
            Derecelendirme = derecelendirme;
        }
    }
}
