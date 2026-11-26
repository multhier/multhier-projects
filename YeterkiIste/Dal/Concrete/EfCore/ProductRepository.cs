using Core.Concrete;
using Dal.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dal.Concrete.EfCore
{
   public class ProductRepository : BaseRepository<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public ICollection<Product> GetProductsIncludeCategory()
        {
            return context.Product
                    .Include(x=>x.CategoryProducts)
                    //.Include(x => x.bra)
                    //.Include(x => x.Color)
                    .ThenInclude(i => i.Category).ToList();
        }

        public ICollection<Product> GetProductDetails()
        {
            return context.Product
                .Include(x => x.Images)
                .Include(x => x.ProductAttributes)
                .Include(x => x.CategoryProducts)
                .ThenInclude(i => i.Category).ToList();
        }

        // Tek ürün: tüm ürünleri çekip bellekte filtrelemek yerine veritabanında Id ile arar.
        public Product GetProductDetail(int id)
        {
            return context.Product
                .Include(x => x.Images)
                .Include(x => x.ProductAttributes)
                .Include(x => x.CategoryProducts)
                .ThenInclude(i => i.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public void AddSubComment(SubComment comment)
        {
            context.SubComment.Add(comment);
        }



        //public ICollection<Product> GetProductbyParentCategory(string categoryname)
        //{
        //    var data = context.Product.Include(x => x.CategoryProducts).ThenInclude(i => i.Category).OrderByDescending(x => x.Id).AsQueryable();

        //    if (!string.IsNullOrEmpty(categoryname))
        //    {
        //        data = data.Where(x => x..Categ.Category.Name == categoryname);

        //    }
        //    return data.ToList();
        //}



    }
}
