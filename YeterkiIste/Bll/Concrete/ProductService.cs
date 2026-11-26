using Bll.Abstract;
using Dal.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Concrete
{
    public class ProductService : IProductService
    {
        IProductRepository repo;
        public ProductService(IProductRepository prm)
        {
            repo = prm;
        }

        //public ICollection<Product> GetProductbyParentCategory(string categoryname)
        //{
        //    var data = context.Post.Include(x => x.Category).OrderByDescending(x => x.Id).AsQueryable();

        //    if (!string.IsNullOrEmpty(categoryname))
        //    {
        //        data = data.Where(x => x.Category.Name == categoryname);

        //    }
        //    return data.ToList();
        //}



        // Kullanılmayan singleton ve "new AppDbContext()" ile çalışan SearchProducts kaldırıldı:
        // singleton'da repo null kalıyordu, SearchProducts da tüm ürünleri belleğe çekiyordu.

        public ICollection<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            // filter parametresi eskiden yok sayılıyordu.
            return repo.GetAll(filter);
        }

        public Product GetProductDetail(int id)
        {
            return repo.GetProductDetail(id);
        }

        public ProductSearchResult Search(ProductSearchCriteria c)
        {
            // Eskiden tüm ürünler ToList() ile belleğe çekilip orada filtreleniyordu.
            // Artık sorgu IQueryable olarak kurulur; filtre, sıralama ve sayfalama SQL'e çevrilir.
            IQueryable<Product> query = repo.GetAllWithIQueryable()
                .Include(x => x.CategoryProducts)
                .ThenInclude(y => y.Category);

            if (!string.IsNullOrEmpty(c.Search))
            {
                // SQL Server'ın varsayılan collation'ı büyük/küçük harf duyarsızdır; ToLower gerekmez.
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
                case 2: query = query.OrderByDescending(x => x.Id); break;   // yeni çıkanlar
                case 3: query = query.OrderBy(x => x.Price); break;          // ucuzdan pahalıya
                case 4: query = query.OrderByDescending(x => x.Price); break; // pahalıdan ucuza
                default: query = query.OrderBy(x => x.Id); break;            // sayfalama için sabit sıra şart
            }

            var page = c.Page > 0 ? c.Page : 1;
            var pageSize = c.PageSize > 0 ? c.PageSize : 9;

            return new ProductSearchResult
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            };
        }
 
        public Product GetById(int id)
        {
            return repo.Get(x => x.Id == id);
        }
        public IQueryable<Product> GetAllWithQuery()
        {
            return repo.GetAllWithIQueryable();
        }

        public ICollection<Product> GetProductsIncludeCategory()
        {
            return repo.GetProductsIncludeCategory();
        }
        public ICollection<Product> GetProductDetails()
        {
            return repo.GetProductDetails();
        }

         
        public Product Update(Product prm)
        {
            return repo.Update(prm);
        }

        public void AddSubComment(SubComment prm)
        {
            repo.AddSubComment(prm);
        }





        #region IDisposable Support
        private bool disposedValue = false; // Artık çağrıları algılama

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: yönetilen durumu (yönetilen nesneleri) bırakın.
                }

                // TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve aşağıda bir sonlandırıcıyı geçersiz kılın.
                // TODO: büyük alanları null olarak ayarlayın.

                disposedValue = true;
            }
        }

        // TODO: yalnızca yukarıdaki Dispose(bool disposing) ifadesi yönetilmeyen kaynakları serbest bırakacak koda sahipse sonlandırıcıyı geçersiz kılın.
        // ~ProductService()
        // {
        //   // Bu kodu değiştirmeyin. Temizleme kodunu yukarıdaki Dispose(bool disposing) içine yerleştirin.
        //   Dispose(false);
        // }

        // Atılabilir deseni doğru uygulamak için bu kod eklendi.
        public void Dispose()
        {
            // Bu kodu değiştirmeyin. Temizleme kodunu yukarıdaki Dispose(bool disposing) içine yerleştirin.
            Dispose(true);
            // TODO: sonlandırıcı yukarıda geçersiz kılınırsa aşağıdaki satırın açıklamasını kaldırın.
            // GC.SuppressFinalize(this);
        }





        #endregion
    }
}
