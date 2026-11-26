using Bll.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Web.ViewsComponent
{
    public class CampaignComponent: BaseComponent
    {
        public CampaignComponent(IUnitOfWork uow):base(uow)
        {

        }
        public IViewComponentResult Invoke()
        {
            var data = uow.Campaignuow.GetAll().ToList();
            return View(data);
        }



    }
}
