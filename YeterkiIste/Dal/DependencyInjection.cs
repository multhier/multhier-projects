using Bll.Abstract;
using Bll.Abstract.Repositories;
using Dal.Concrete;
using Dal.Concrete.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dal
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Altyapı (veri erişimi) katmanını kaydeder: DbContext, Bll'deki repository arayüzlerinin EF uygulamaları ve UnitOfWork.
        /// Web katmanı Dal'ın somut sınıflarını tek tek bilmez; yalnızca bu metodu çağırır.
        /// </summary>
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionStrings:DefaultConnection ayarı bulunamadı (appsettings, User Secrets veya ortam değişkeni).");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        /// <summary>
        /// ASP.NET Identity'nin kullanıcı/rol verisini EF ile saklamasını sağlar.
        /// Böylece Web katmanı AppDbContext'i doğrudan tanımak zorunda kalmaz.
        /// </summary>
        public static IdentityBuilder AddDataAccessStores(this IdentityBuilder builder)
        {
            return builder.AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
