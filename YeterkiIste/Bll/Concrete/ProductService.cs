using Bll.Abstract;
using Bll.Abstract.Repositories;
using Model;
using Model.Comments;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bll.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repo;

        // Repository artık DI ile geliyor; servis hiçbir veritabanı tipini bilmiyor.
        public ProductService(IProductRepository prm)
        {
            repo = prm;
        }

        public ICollection<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return repo.GetAll(filter);
        }

        public Product GetById(int id)
        {
            return repo.Get(x => x.Id == id);
        }

        public ICollection<Product> GetProductsIncludeCategory()
        {
            return repo.GetProductsIncludeCategory();
        }

        public ICollection<Product> GetProductDetails()
        {
            return repo.GetProductDetails();
        }

        public Product GetProductDetail(int id)
        {
            return repo.GetProductDetail(id);
        }

        // Sorgu kurma ve sayfalama işi repository'de (veritabanında) yapılır.
        public ProductSearchResult Search(ProductSearchCriteria criteria)
        {
            return repo.Search(criteria);
        }

        public Product Update(Product prm)
        {
            return repo.Update(prm);
        }

        public void AddSubComment(SubComment prm)
        {
            repo.AddSubComment(prm);
        }

        public void Dispose()
        {
        }
    }
}
