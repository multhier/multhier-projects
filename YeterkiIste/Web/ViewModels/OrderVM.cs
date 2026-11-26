using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    // Eskiden hiçbir alanda [Required] yoktu, bu yüzden ModelState.IsValid hiçbir şeyi yakalamıyordu.
    public class OrderVM
    {
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad gereklidir")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Soyad gereklidir")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Adres gereklidir")]
        [StringLength(500)]
        public string Address { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Şehir gereklidir")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Telefon numarası gereklidir")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin")]
        public string Email { get; set; }

        [Display(Name = "Kart üzerindeki İsim")]
        [Required(ErrorMessage = "Kart üzerindeki isim gereklidir")]
        public string CardName { get; set; }

        [Display(Name = "Kart Numarası")]
        [Required(ErrorMessage = "Kart numarası gereklidir")]
        [CreditCard(ErrorMessage = "Geçerli bir kart numarası girin")]
        public string CardNumber { get; set; }

        [Display(Name = "Ay")]
        [Required(ErrorMessage = "Son kullanma ayı gereklidir")]
        [RegularExpression(@"^(0?[1-9]|1[0-2])$", ErrorMessage = "Ay 1-12 arasında olmalıdır")]
        public string ExpirationMonth { get; set; }

        [Display(Name = "Yıl")]
        [Required(ErrorMessage = "Son kullanma yılı gereklidir")]
        [RegularExpression(@"^(\d{2}|\d{4})$", ErrorMessage = "Yıl 2 veya 4 haneli olmalıdır")]
        public string ExpirationYear { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "CVV gereklidir")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV 3 veya 4 haneli olmalıdır")]
        public string ExpirationCvv { get; set; }

        public CartVM CartVM { get; set; }
    }
}
