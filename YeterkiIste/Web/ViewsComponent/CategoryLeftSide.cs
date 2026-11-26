using Bll.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Models;

namespace Web.ViewsComponent
{
    public class CategoryLeftSide : BaseComponent
    {

        public CategoryLeftSide(IUnitOfWork _prm):base(_prm)
        {
        }



        public IViewComponentResult Invoke()
        {
            
            string ustkategori = ViewContext.RouteData.Values["ustcategoryname"].ToString();

            var rowsId = uow.Categoryuow.GetAll().Where(b => Url.FriendlyUrl(b.Name) == ustkategori).Select(x => x.Id).FirstOrDefault();


            var categoryside = uow.Categoryuow.GetAllWithQuery().Where(x => x.ParentId.Equals(rowsId) &&  x.IsActive).Select(x => new CategoryVM
            {
                Id = x.Id,
                Name = x.Name,
                Count = x.CategoryProducts.Count()
            });
            return View(categoryside);


            //var categoryside = uow.Categoryuow.GetAllWithQuery().Where(x => x.ParentId !=null  && x.IsActive).Select(x => new 
        }
    }
}

