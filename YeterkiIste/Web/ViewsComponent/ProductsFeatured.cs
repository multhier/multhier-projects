using Bll.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewsComponent
{
 
     public class ProductsFeatured : ViewComponent
    {
        IUnitOfWork uow;
        public ProductsFeatured(IUnitOfWork _prm)
        {
            uow = _prm;
        }

        public IViewComponentResult Invoke() //Invoke çağırmak demektir.
        {
            var producthome = uow.Productuow.GetProductsIncludeCategory().Where(x => x.IsFeatured == true && x.IsActive).OrderByDescending(x => x.Id); 
            return View(producthome);
        }

    }
}
