using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email hesabınız")]
        [Required(ErrorMessage = "Email alanınızı girmelisiniz")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Şifreniz")]
        [Required(ErrorMessage = "Şifre girmelisiniz")]
        //[DataType(DataType.Password)]
        [UIHint("password")]
        [MinLength(3, ErrorMessage = "Şifreniz en az 3 karakter olmalıdır.")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
