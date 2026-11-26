using Microsoft.AspNetCore.Identity;
namespace Web.Infastructure
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError() { Code = "invalidusername", Description = $"Bu {userName} geçersizdir" };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError() { Code = "duplicateusername", Description = $"Bu {userName} adı kullanılmaktadır" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError() { Code = "invalidemail", Description = $"Bu {email} geçersizdir" };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError() { Code = "invalidemail", Description = $"Şifreniz en az {length} karakter olmalıdır." };
        }
        //burdaki şifre uzunluğunu startup.cs de söylemiştik.( x.Password.RequiredLength = 3;)
    }
}
 
