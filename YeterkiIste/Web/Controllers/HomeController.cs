using Dal.Identity;
using Bll.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Threading.Tasks;
using Web.Helper;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        //private ICartService cartservice;
        public HomeController( IUnitOfWork uow, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(uow, userManager, signInManager, roleManager)
        {
            //cartservice = _cartservice;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserVM prm)
        {
            if (!ModelState.IsValid)
            {
                return View(prm);
            }

            AppUser appuser = new AppUser();
            appuser.UserName = prm.UserName;
            appuser.Email = prm.Email;
            // PasswordHash'e düz şifre atanmıyor; CreateAsync şifreyi kendisi hash'ler.

            IdentityResult result = await userManager.CreateAsync(appuser, prm.Password);

            if (result.Succeeded)
            {
               uow.Cartuow.InitializeCart(appuser.Id);
               await uow.SaveChangesAsyn();
                TempData.Show("Tutucu1", new ResultMessage() { Title = "Tebrikler", Message = "Sitemize başarılı bir şekilde üye oldunuz. Lütfen, giriş yapın.", Css = "success" });


                return RedirectToAction("SignIn");
            }
            else
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(prm);
        }



        [HttpGet]
        public IActionResult SignIn(string returnUrl=null)
        {
            return View(new LoginVM()
            { ReturnUrl=returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginVM prm)
        {
            if (!ModelState.IsValid)
            {
                return View(prm);
            }

           if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(prm.Email);
                if (user != null)
                {
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınız bir süreliğine kilitlenmiştir.Lütfen daha sonra tekrar deneyiniz.");
                        return View(prm);
                    }
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, prm.Password, prm.RememberMe, false);
                    if (result.Succeeded)
                    {
                        await userManager.ResetAccessFailedCountAsync(user);
                        //return RedirectToAction(prm.ReturnUrl ?? "/");
                        TempData.Show("tutucu1", new ResultMessage() { Title = "Hoşgeldin "+ user.UserName +"" , Message= "Seni aramızda görmekten çok mutluyuz."  + "", Css = "success" });
                        return RedirectToAction("Index","Home");
                     
                    }
                    else
                    {
                        await userManager.AccessFailedAsync(user);
                        int fail = await userManager.GetAccessFailedCountAsync(user);
                        ModelState.AddModelError("", $"{fail} kez başarısız giriş.");
                        if (fail == 3)
                        {
                            await userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(5)));
                            ModelState.AddModelError("", "Hesabınız 3 başarısız giriş dolayısıyla 5dk. süreyle kısıtlanmıştır.Daha sonra tekrar deneyin");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Kullanıcı adı veya Email hesabı hatalı");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Bu email adresine kayıtlı kullanıcı bulunamamıştır.");
                    return View(prm);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Bu email adresine kayıtlı kullanıcı bulunamamıştır.");
            }
            //TempData["TutucuUserIdentityName"] = User.Identity.Name;
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            // Eskiden await edilmiyordu; yönlendirme, çerez silinmeden önce gerçekleşebiliyordu.
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




    }
}