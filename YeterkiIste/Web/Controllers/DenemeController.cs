using Dal.Identity;
using Bll.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels;
namespace Web.Controllers
{
    public class DenemeController : BaseController
    {
        public DenemeController(IUnitOfWork uow, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(uow, userManager, signInManager, roleManager)
        {
        }

        public IActionResult Products(string search, string category, int? prm_page, int? prm_brand, int? sortBy, int? minPrice, int? maxPrice)
        {

            var page = prm_page.HasValue && prm_page.Value > 0 ? prm_page.Value : 1;
            const int pageSize = 9;

            var branddata = uow.Branduow.GetAll().ToList();

            // Filtreleme artık veritabanında (bkz. ProductService.Search).
            List<int> categoryIds = null;
            if (!string.IsNullOrEmpty(category))
            {
                categoryIds = uow.Categoryuow.GetAll()
                    .Where(c => Web.Models.Infrastructure.F_Tool.URLDuzelt(c.Name) == category)
                    .Select(c => c.Id)
                    .ToList();
            }

            var result = uow.Productuow.Search(new ProductSearchCriteria
            {
                Search = search,
                CategoryIds = categoryIds,
                BrandId = prm_brand,
                SortBy = sortBy,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Page = page,
                PageSize = pageSize
            });

            var pager = new Pager(result.TotalCount, page, pageSize);

            return View(new MyProductVM { Products = result.Items, Pager = pager, CategoryRoute = category, sortBy = sortBy, Brands = branddata, BrandRoute = prm_brand, search = search, minPrice = minPrice, maxPrice = maxPrice });
        }

        public int Adet = 6;
        public IActionResult Urunler(string category, int page = 1)
        {
            var data = uow.Productuow.GetProductsIncludeCategory(); //26 ürün geldi

            if (!string.IsNullOrEmpty(category))
            {
                data = data.Where(x => x.CategoryProducts.Any(a => Web.Models.Infrastructure.F_Tool.URLDuzelt(a.Category.Name) == category)).ToList();  //13 ürün geldi
            }

            var dataCount = data.Count();  // Kategorisiz 26 kategorili 13

            data = data.Skip(Adet * (page - 1))  //Data artı 6 adet oldu
                   .Take(Adet).ToList();

            return View(new ProductPagesVM() { Category = category, Products = data, E_Page = new E_Page() { CurrentPage = page, ItemsPerPage = Adet, TotalItems = dataCount } }); //dataCount 26
        }

        public IActionResult deneme()
        {

            return View();
        }

        public IActionResult deneme2()
        {

            return View();
        }









        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginVM prm)
        {

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
                        // Eskiden TempData["Tutucu1"] içindeki JSON mesaj URL sanılıp Redirect ediliyordu,
                        // diğer dal ise var olmayan Deneme/Index'e gidiyordu.
                        return RedirectToAction("Index", "Home");
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
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Bu email adresine kayıtlı kullanıcı bulunamamıştır.");
            }
            //TempData["TutucuUserIdentityName"] = User.Identity.Name;
            return View();
        }







    }
}