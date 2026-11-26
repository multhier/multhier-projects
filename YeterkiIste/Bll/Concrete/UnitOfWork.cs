using Bll.Abstract;
using Dal.Concrete;
using Dal.Concrete.EfCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public UnitOfWork(AppDbContext _context)
        {
            context = _context;
        }

        private ICategoryService icategoryService;
        private IProductService iproductService;
        private ICampaignService icampaignService;
        private IColorService icolorService;
        private IBrandService ibrandService;
        private ICartService icartService;
        private IOrderService iorderService;


        public ICategoryService Categoryuow => icategoryService ?? (icategoryService = new CategoryService(new CategoryRepository(context)));  
        public IProductService Productuow { get { return iproductService ?? (iproductService = new ProductService(new ProductRepository(context))); } }
        public ICampaignService Campaignuow => icampaignService ?? (icampaignService = new CampaignService(new CampaignRepository(context)));
        public IColorService Coloruow => icolorService ?? (icolorService = new ColorService(new ColorRepository(context)));
        public IBrandService Branduow => ibrandService ?? (ibrandService = new BrandService(new BrandRepository(context)));
        public ICartService Cartuow => icartService ?? (icartService = new CartService(new CartRepository(context)));
        public IOrderService Orderuow => iorderService ?? (iorderService = new OrderService(new OrderRepository(context)));
      



        public async Task<bool> SaveChangesAsyn()
        {
            if (await context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
        public int Save => context.SaveChanges();



        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                  
                }
                disposedValue = true;
            }
        }

       
        public void Dispose()
        {
            Dispose(true); 
            GC.SuppressFinalize(this);
        }



        #endregion
    }
}
