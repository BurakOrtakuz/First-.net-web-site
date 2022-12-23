using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebProgramlama.Models
{
    public class User
    {
        [Required]
        public string username { get; set; }

        [Required]
        [PasswordPropertyText]
        public string password { get; set; }
    }
}
