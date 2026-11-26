using Bll.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Models;

namespace Web.ViewsComponent
{
    public class CategoryParent : BaseComponent
    {

        public CategoryParent(IUnitOfWork _prm) : base(_prm)
        {
        }

        public IViewComponentResult Invoke()
        {

            var data = uow.Categoryuow.GetAll().Where(x => x.ParentId == null).ToList();
            return View(data);
        }
    }
}