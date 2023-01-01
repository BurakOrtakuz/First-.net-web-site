using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramlama.Models
{
    public class Randevu
    {
        public DateTime randevuZamani { get; set; }
        [ForeignKey("User")]
        public int  userId { get; set; }
    }
}
