using Core.Concrete;
using Dal.Abstract;
using Model;

namespace Dal.Concrete.EfCore
{
    public class CategoryRepository : BaseRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext prm) : base (prm)
        {

        }
    }
}


