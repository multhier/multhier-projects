using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Concrete
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        // Bağlantı cümlesi artık kodda değil: Startup, "ConnectionStrings:DefaultConnection"
        // ayarını (appsettings / User Secrets / ortam değişkeni) okuyup buraya options olarak verir.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CategoryProduct> CategoryProduct { get; set; }
        public DbSet<ProductAttribute> ProductAttribute { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public virtual DbSet<MainComment> MainComment { get; set; }
        public virtual DbSet<SubComment> SubComment { get; set; }

        protected override void OnModelCreating(ModelBuilder m)
        {
            m.Entity<Category>().Property(x => x.Name).HasMaxLength(100);
            m.Entity<Product>().Property(x => x.Name).HasMaxLength(150);
 

            m.Entity<CategoryProduct>().HasKey(x=> new {x.CategoryId,x.ProductId });
          
            m.Entity<ProductAttribute>().HasOne(p => p.Product).WithMany(p => p.ProductAttributes).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.ClientSetNull);
            m.Entity<Image>().HasOne(p => p.Product).WithMany(p => p.Images).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.ClientSetNull);
            
            m.Entity<Campaign>(x =>
            {
                x.Property(e => e.Description).HasMaxLength(500);
                x.Property(e => e.Link).IsRequired().HasMaxLength(100);
            });

        
            m.Entity<Brand>().HasOne(b => b.Category).WithMany(m => m.Brands).HasForeignKey(m => m.CategoryId).OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(m);
        }

    }
}
