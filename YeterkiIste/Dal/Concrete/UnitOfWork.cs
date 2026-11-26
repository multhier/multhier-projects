using Bll.Abstract;
using System;
using System.Threading.Tasks;

namespace Dal.Concrete
{
    // Veritabanı işlemlerini tek bir context üzerinden birleştirir.
    // Servisler artık kendi repository'lerini oluşturmaz; DI container bunları bağlar (bkz. Web/Startup.cs).
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(
            AppDbContext _context,
            ICategoryService categoryService,
            IProductService productService,
            ICampaignService campaignService,
            IBrandService brandService,
            IColorService colorService,
            ICartService cartService,
            IOrderService orderService)
        {
            context = _context;
            Categoryuow = categoryService;
            Productuow = productService;
            Campaignuow = campaignService;
            Branduow = brandService;
            Coloruow = colorService;
            Cartuow = cartService;
            Orderuow = orderService;
        }

        public ICategoryService Categoryuow { get; }
        public IProductService Productuow { get; }
        public ICampaignService Campaignuow { get; }
        public IBrandService Branduow { get; }
        public IColorService Coloruow { get; }
        public ICartService Cartuow { get; }
        public IOrderService Orderuow { get; }

        public async Task<bool> SaveChangesAsyn()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public int Save => context.SaveChanges();

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
