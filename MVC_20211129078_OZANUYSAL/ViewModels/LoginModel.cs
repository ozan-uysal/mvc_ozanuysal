using System.ComponentModel.DataAnnotations;

namespace MVC_20211129078_OZANUYSAL.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Input Username")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Input Password!")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool KeepMe { get; set; }
    }
}
