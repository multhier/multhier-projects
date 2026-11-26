using Bll.Abstract.Repositories;
using Core.Abstract;
using Model;

namespace Dal.Concrete.EfCore
{

    public class CampaignRepository : BaseEfRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext prm):   base (prm)
        {

        }
    }
}
