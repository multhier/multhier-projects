using Core.Concrete;
using Dal.Abstract;
using Model;

namespace Dal.Concrete.EfCore
{

    public class CampaignRepository : BaseRepository<Campaign,AppDbContext>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext prm):   base (prm)
        {

        }
    }
}
