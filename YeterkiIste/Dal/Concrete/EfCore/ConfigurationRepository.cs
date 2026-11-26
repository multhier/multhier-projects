using Dal.Concrete;
using Model;

namespace Dal.Concrete.EfCore
{
    // Eskiden singleton'dı ve her çağrıda "new AppDbContext()" açıyordu.
    // Artık DI'dan gelen context'i kullanır.
    public class ConfigurationRepository
    {
        protected readonly AppDbContext context;

        public ConfigurationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Config GetConfig(string Key)
        {
            return context.Configurations.Find(Key);
        }

        public int PageSize()
        {
            var pageSizeConfig = context.Configurations.Find("PageSize");
            return pageSizeConfig != null && int.TryParse(pageSizeConfig.Value, out var size) ? size : 5;
        }

        public int ShopPageSize()
        {
            var pageSizeConfig = context.Configurations.Find("ShopPageSize");
            return pageSizeConfig != null && int.TryParse(pageSizeConfig.Value, out var size) ? size : 6;
        }
    }
}
