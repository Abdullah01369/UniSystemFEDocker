using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage ="Mail Boş Bırakılamaz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Tabiki Boş Bırakılamaz")]
        public string Password { get; set; }
    }
}
