using Microsoft.AspNetCore.Identity;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Infastructure
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            string[] Digits = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            foreach (var item in Digits)
            {
                if (user.UserName[0].ToString() == item)
                {
                    errors.Add(new IdentityError() { Code = "User Name Contais FirstLetter Digit", Description = "Kullanıcı adının ilk harfi sayısal karakter içeremez" });
                }
            }
            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));

        }
    }
}