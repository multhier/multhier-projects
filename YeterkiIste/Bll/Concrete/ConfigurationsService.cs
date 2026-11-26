using Dal.Concrete;
using Dal.Concrete.EfCore;

namespace Bll.Concrete
{
    public class ConfigurationsService:ConfigurationRepository
    {
        public ConfigurationsService(AppDbContext context) : base(context)
        {
        }
    }
}
