using Microsoft.AspNetCore.Identity; 
using System;

namespace Model
{
    public class AppUser :IdentityUser
    {
        public string City { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        public string ImageUrl { get; set; } = "";
        public int Gender { get; set; } = 0;
    }
}
