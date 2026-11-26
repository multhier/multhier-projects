using Bll.Abstract.Repositories;
using Core.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace Dal.Concrete.EfCore
{
    public class CategoryRepository : BaseEfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext prm) : base(prm)
        {
        }

        // Alt kategori sayımı (CategoryProducts) SQL'de yapılsın diye Include ile çekiliyor.
        public ICollection<Category> GetActiveChildrenWithProducts(int parentId)
        {
            return context.Category
                .Include(x => x.CategoryProducts)
                .Where(x => x.ParentId == parentId && x.IsActive)
                .ToList();
        }
    }
}
