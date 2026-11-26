using Bll.Abstract;
 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Enums;
using Web.ViewModels;


namespace Web.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {
        public MemberController(IUnitOfWork uow, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(uow, userManager, signInManager, roleManager)
        {
        }
        // Profil fotoğrafı için izin verilen uzantılar ve boyut. Eskiden hiç kontrol yoktu:
        // .html/.svg gibi dosyalar wwwroot'a yazılıp siteden servis edilebiliyordu (stored XSS).
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxImageBytes = 2 * 1024 * 1024; // 2 MB

        public async Task<IActionResult> Index()
        {
            ViewBag.TutucuGender = new SelectList(Enum.GetNames(typeof(Gender)));
            var user = await GetCurrentUserAsync();
            UserVM uservm = new UserVM();
            uservm.ImageUrl = user.ImageUrl;
            uservm.BirthDate = user.BirthDate;
            uservm.City = user.City;
            uservm.Email = user.Email;
            uservm.Gender = (Enums.Gender)Enum.Parse(typeof(Enums.Gender), user.Gender.ToString());
            uservm.PhoneNumber = user.PhoneNumber;
            uservm.UserName = user.UserName;
            return View(uservm);
        }



        public async Task<IActionResult> EditProfile()
        {
            AppUser user = await GetCurrentUserAsync();
            ViewBag.TutucuGender = new SelectList(Enum.GetNames(typeof(Gender)));
            UserVM uservm = new UserVM();
            uservm.ImageUrl = user.ImageUrl;
            uservm.BirthDate = user.BirthDate;
            uservm.City = user.City;
            uservm.Email = user.Email;
            uservm.Gender = (Enums.Gender)Enum.Parse(typeof(Enums.Gender), user.Gender.ToString());
            uservm.PhoneNumber = user.PhoneNumber;
            uservm.UserName = user.UserName;
            return View(uservm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserVM prm, IFormFile prmImageUrl) //UserVM model parametre gelecek
        {
            ModelState.Remove("Password");
            ViewBag.TutucuGender = new SelectList(Enum.GetNames(typeof(Gender)));

            string imageExtension = null;
            if (prmImageUrl != null && prmImageUrl.Length > 0)
            {
                imageExtension = Path.GetExtension(prmImageUrl.FileName)?.ToLowerInvariant();
                if (!AllowedImageExtensions.Contains(imageExtension) || prmImageUrl.ContentType == null || !prmImageUrl.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("", "Sadece jpg, jpeg, png, gif veya webp resim yükleyebilirsiniz.");
                }
                else if (prmImageUrl.Length > MaxImageBytes)
                {
                    ModelState.AddModelError("", "Resim en fazla 2 MB olabilir.");
                }
            }

            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                //UserVM uservm = user.Adapt<UserVM>();
                user.UserName = prm.UserName;
                user.Email = prm.Email;
                user.PhoneNumber = prm.PhoneNumber;
                user.City = prm.City;
                user.BirthDate = prm.BirthDate;
                user.Gender = (int)prm.Gender;

                if (imageExtension != null)
                {
                    var fileName = Guid.NewGuid().ToString() + imageExtension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await prmImageUrl.CopyToAsync(stream);
                        user.ImageUrl = fileName;
                    }
                }

                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.SignInAsync(user, true);
                    ViewBag.tutucuBasari = "basariliTutucu";
                }
                else
                {
                    AddModelError(result);
                }
            }
            return View(prm); //prm yazmamızın nedeni hata olduğun inputların içi boş kalmasın  
        }
        //burda eski kullanıcı adımı görürüm ama mobil aplikasyonda kişi yeni kullanıcı adımı görür



        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordChangeVM prm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await GetCurrentUserAsync();
            if (user != null)
            {
                bool exist = await userManager.CheckPasswordAsync(user, prm.PasswordOld);
                if (exist)
                {
                    IdentityResult result = await userManager.ChangePasswordAsync(user, prm.PasswordOld, prm.PasswordNew);
                    if (result.Succeeded)
                    {
                        // Eskiden bu üç çağrı await edilmiyordu; oturum yenilemesi yarım kalabiliyordu.
                        await userManager.UpdateSecurityStampAsync(user);
                        await signInManager.SignOutAsync();
                        await signInManager.PasswordSignInAsync(user, prm.PasswordNew, true, false);
                        TempData["TutucuBasari"] = "Başarıyla Güncellendi";
                    }
                    else
                    {
                        AddModelError(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski Şifreniz Yanlış");
                }
            }
            return View();
        }


    }
}