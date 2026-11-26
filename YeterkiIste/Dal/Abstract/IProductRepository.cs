using Core.Abstract;
using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Abstract
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        ICollection<Product> GetProductsIncludeCategory();
        ICollection<Product> GetProductDetails();
        Product GetProductDetail(int id);
        void AddSubComment(SubComment comment);
    }
}
