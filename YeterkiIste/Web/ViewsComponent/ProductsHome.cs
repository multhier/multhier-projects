using Bll.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Web.ViewsComponent
{
    public class ProductsHome:BaseComponent
    {
        
        public ProductsHome(IUnitOfWork uow):base(uow)  
        {
        }
        public IViewComponentResult Invoke() //Invoke çağırmak demektir.
        {
            var producthome = uow.Productuow.GetProductsIncludeCategory().Where(x=> x.IsHome == true && x.IsActive).OrderByDescending(x=> x.Id);
            return View(producthome);
        }
    }
}
