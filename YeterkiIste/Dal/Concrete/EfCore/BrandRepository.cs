using Bll.Abstract.Repositories;
using Core.Abstract;
using Model;

namespace Dal.Concrete.EfCore
{
    public class BrandRepository : BaseEfRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext prm) : base(prm)
        {

        }

    }
}
