using Bll.Abstract;
using Bll.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Bll
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Uygulama (iş) katmanının servislerini kaydeder.
        /// Servisler yalnızca repository arayüzlerine bağlıdır; bu arayüzlerin uygulamaları Dal.AddDataAccess ile kaydedilir.
        /// </summary>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
