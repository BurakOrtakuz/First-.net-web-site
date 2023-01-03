using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProgramlama.Areas.Identity.Data;

namespace WebProgramlama.Models
{
    public class Randevu
    {

        [Key]
        public int randevuId { get; set; }
        [DisplayName("Randevu Zamanı")]
        public DateTime randevuZamani { get; set; }
        [ForeignKey("User")]
        public string userId { get; set; }
        public virtual User User { get; set; } 
        [DisplayName("Aracınızın Plakası")]
        public String plaka { get; set; }
        [DisplayName("Yapacağınız işlem")]
        public string islemId { get; set; }
        public virtual Islem Islem { get; set; }
    }
}
