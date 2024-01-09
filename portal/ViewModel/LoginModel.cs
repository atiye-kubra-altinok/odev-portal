using System.ComponentModel.DataAnnotations;

namespace portal.ViewModel
{
    public class LoginModel
    {
        [Display(Name = "Email girin")]
        [Required(ErrorMessage = "Email adresini Giriniz!")]
        public string email { get; set; }


        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Parola Giriniz!")]
        public string Sifre { get; set; }
    }
}
