using Bll.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        //miras aldığım yerlerde kullanaağız bunları o yüzden protected kullanabiliriz.
        protected UserManager<AppUser> userManager { get; }
        protected SignInManager<AppUser> signInManager { get; }
        protected RoleManager<AppRole> roleManager { get; }

        protected IUnitOfWork uow;

        public BaseController(IUnitOfWork _uow, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager = null)
        {
            uow = _uow;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        // .Result yerine await edilebilen sürüm (sync-over-async deadlock/thread tüketimi riskini önler).
        protected Task<AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(User);


        protected void AddModelError(IdentityResult result)
        {
            foreach (IdentityError item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }

    }
}