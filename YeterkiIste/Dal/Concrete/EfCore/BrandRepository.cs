using Core.Concrete;
using Dal.Abstract;
using Model;

namespace Dal.Concrete.EfCore
{
    public class BrandRepository : BaseRepository<Brand, AppDbContext>, IBrandRepository
    {
        public BrandRepository(AppDbContext prm) : base(prm)
        {

        }

    }
}
