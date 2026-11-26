using Microsoft.AspNetCore.Identity;
using System;

namespace Dal.Identity
{
    // Identity kullanıcısı bir altyapı (ASP.NET Identity + EF) detayıdır; bu yüzden Model'de değil Dal'dadır.
    // Böylece en içteki Model katmanı hiçbir dış pakete bağımlı kalmaz.
    public class AppUser : IdentityUser
    {
        public string City { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        public string ImageUrl { get; set; } = "";
        public int Gender { get; set; } = 0;
    }
}
