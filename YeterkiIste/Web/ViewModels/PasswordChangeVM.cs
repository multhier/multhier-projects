using System.ComponentModel.DataAnnotations;
namespace Web.ViewModels
{
    public class PasswordChangeVM
    {
        [Display(Name = "Eski Şifreniz")]
        public string PasswordOld { get; set; }

        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Yeni Şifrenizi girmediniz")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakter olmalıdır")]
        public string PasswordNew { get; set; }

        [Display(Name = "Şifrenizi Tekrar Girin")]
        [Required(ErrorMessage = "Şifrenizi tekrarlamanız gerekir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakter olmalıdır")]
        [Compare("PasswordNew", ErrorMessage = "Şifreler uyuşmamaktadır")]
        public string PasswordConfirm { get; set; }
    }
}
