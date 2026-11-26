using Bll;
using Dal;
using Dal.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using Web.Infastructure;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false); 
            services.Configure<IyzicoSettings>(Configuration.GetSection("Iyzico"));

            // Composition root: her katman kendi kayıtlarını yapar, Web yalnızca çağırır.
            services.AddBusinessServices();                                                     // Bll: servisler
            services.AddDataAccess(Configuration.GetConnectionString("DefaultConnection"));     // Dal: DbContext, repository'ler, UnitOfWork

            services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.User.RequireUniqueEmail = true;
                x.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@ÇçĞğÜüÖöİıŞş";
                x.Password.RequiredLength = 3;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = false;

            })
                .AddPasswordValidator<CustomPasswordValidator>()
                .AddUserValidator<CustomUserValidator>()
                .AddDataAccessStores()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "YourCookie";
            cookieBuilder.HttpOnly = true;
            cookieBuilder.SameSite = SameSiteMode.Lax;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/Home/SignIn");
                x.LogoutPath = new PathString("/Home/LogOut");
                x.AccessDeniedPath = new PathString("/Member/AccessDenied");
                x.Cookie = cookieBuilder;
                x.SlidingExpiration = true;
                x.ExpireTimeSpan = System.TimeSpan.FromDays(60);
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            //    context.Database.Migrate(); 
            //}

 
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(x =>
            {

                x.MapRoute("1", "admin/products", new { controller = "Admin", Action = "ProductList" });
                x.MapRoute("2", "admin/products/{id?}", new { controller = "Admin", Action = "EditProduct" });
                x.MapRoute(name: "Cart", template: "cart", defaults: new { controller = "Cart", action = "Index" });
                x.MapRoute(name: "orders", template: "orders", defaults: new { controller = "Cart", action = "GetOrders" });
                x.MapRoute(name: "checkout", template: "checkout", defaults: new { controller = "Cart", action = "checkout" });

                x.MapRoute("3", "{ustcategoryname}", new { routeName = "3", controller = "Ogeler", Action = "Index" });
                x.MapRoute("4", "Product/{detailname}/{Id}", new { routeName = "4", controller = "Ogeler", Action = "detail" });

                x.MapRoute("default", "{controller}/{action}/{id?}", new { controller = "Home", Action = "Index" });


       
            });

        }
    }
}
