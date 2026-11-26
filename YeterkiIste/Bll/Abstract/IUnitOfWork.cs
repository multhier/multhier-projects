using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Abstract
{
   public interface IUnitOfWork:IDisposable
    {
        ICategoryService Categoryuow { get; }
        IProductService Productuow { get; }
        ICampaignService Campaignuow { get; }
        IBrandService Branduow { get; }
        IColorService Coloruow { get; }
        ICartService Cartuow { get; }
        IOrderService Orderuow { get; }
      
        Task<bool> SaveChangesAsyn();
        int SaveChanges();
        int Save { get; }
    }
}
