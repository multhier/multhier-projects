using Bll.Abstract.Repositories;
using Core.Abstract;
using Model;

namespace Dal.Concrete.EfCore
{
    public  class ColorRepository : BaseEfRepository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext prm) : base(prm)
        {

        }

    }
}
