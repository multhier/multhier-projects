using Bll.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewsComponent
{
    [ViewComponent]
    public abstract class BaseComponent : ViewComponent
    {
        protected IUnitOfWork uow;
        public BaseComponent(IUnitOfWork _prm)
        {
            uow = _prm;
        }
    }
}
