using Core.Abstract;
using Model;
using Model.Comments;
using Bll.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.Abstract.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        ICollection<Product> GetProductsIncludeCategory();
        ICollection<Product> GetProductDetails();
        Product GetProductDetail(int id);
        void AddSubComment(SubComment comment);

        /// <summary>Filtreleme, sıralama ve sayfalama veritabanında yapılır.</summary>
        ProductSearchResult Search(ProductSearchCriteria criteria);
    }
}
