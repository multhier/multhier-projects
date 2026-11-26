using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface IProductService : IDisposable
    {
        ICollection<Product> GetAll(Expression<Func<Product, bool>> filter = null);
        IQueryable<Product> GetAllWithQuery();
        ICollection<Product> GetProductsIncludeCategory();
        ICollection<Product> GetProductDetails();
        Product GetProductDetail(int id);
        Product GetById(int id);

        /// <summary>Filtreleme, sıralama ve sayfalamayı veritabanında yapar.</summary>
        ProductSearchResult Search(ProductSearchCriteria criteria);

        Product Update(Product prm);
        void AddSubComment(SubComment prm);




        //ICollection<Product> GetProductbyParentCategory(string categoryname);


    }
}
