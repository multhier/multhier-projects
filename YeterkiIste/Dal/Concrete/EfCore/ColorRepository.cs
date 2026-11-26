using Core.Concrete;
using Dal.Abstract;
using Model;

namespace Dal.Concrete.EfCore
{
    public  class ColorRepository : BaseRepository<Color, AppDbContext>, IColorRepository
    {
        public ColorRepository(AppDbContext prm) : base(prm)
        {

        }

    }
}
