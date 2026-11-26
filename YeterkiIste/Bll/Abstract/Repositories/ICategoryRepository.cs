using Core.Abstract;
using Model;
using System.Collections.Generic;

namespace Bll.Abstract.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        /// <summary>Verilen üst kategorinin aktif alt kategorileri, her birinin ürün ilişkisiyle birlikte.</summary>
        ICollection<Category> GetActiveChildrenWithProducts(int parentId);
    }
}
