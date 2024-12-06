using System.ComponentModel.DataAnnotations;

namespace JwtApp.Front.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }
    }
}
