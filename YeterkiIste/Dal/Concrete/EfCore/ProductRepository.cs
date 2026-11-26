using Bll.Abstract.Repositories;
using Bll.Abstract;
using Core.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dal.Concrete.EfCore
{
   public class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
        public ProductSearchResult Search(ProductSearchCriteria c)
        {
            // Filtre, sıralama ve sayfalama tek bir SQL sorgusunda, veritabanında çalışır.
            IQueryable<Product> query = context.Product
                .Include(x => x.CategoryProducts)
                .ThenInclude(y => y.Category);

            if (!string.IsNullOrEmpty(c.Search))
            {
                query = query.Where(x => x.Name != null && x.Name.Contains(c.Search));
            }

            if (c.CategoryIds != null)
            {
                var ids = c.CategoryIds.ToList();
                query = query.Where(x => x.CategoryProducts.Any(cp => ids.Contains(cp.CategoryId)));
            }

            if (c.BrandId.HasValue)
            {
                query = query.Where(x => x.BrandId == c.BrandId.Value);
            }

            if (c.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= c.MinPrice.Value);
            }

            if (c.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= c.MaxPrice.Value);
            }

            switch (c.SortBy)
            {
                case 2: query = query.OrderByDescending(x => x.Id); break;
                case 3: query = query.OrderBy(x => x.Price); break;
                case 4: query = query.OrderByDescending(x => x.Price); break;
                default: query = query.OrderBy(x => x.Id); break;
            }

            var page = c.Page > 0 ? c.Page : 1;
            var pageSize = c.PageSize > 0 ? c.PageSize : 9;

            return new ProductSearchResult
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            };
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
