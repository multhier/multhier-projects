using System;
using System.ComponentModel.DataAnnotations;
using Web.Enums;

namespace Web.ViewModels
{
    public class UserVM
    {
        [Required(ErrorMessage = "Kullanıcı ismi gereklidir")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }



        [Required(ErrorMessage = "Email alanı gereklidir")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Şifre gereklidir")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Cinsiyet")]
        public Gender Gender { get; set; }

        [Display(Name = "Görsel")]
        public string ImageUrl { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
    }
}
