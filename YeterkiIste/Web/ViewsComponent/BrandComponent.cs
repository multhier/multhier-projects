using Bll.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Web;
using Web.ViewModels;

namespace Web.ViewsComponent
{
    public class BrandComponent:BaseComponent
    {
        public BrandComponent(IUnitOfWork _prm) : base(_prm)
        {
        }



        public IViewComponentResult Invoke( )
        {

            string ustkategori = ViewContext.RouteData.Values["ustcategoryname"].ToString();
            var rowsId = uow.Categoryuow.GetAll().Where(b => Url.FriendlyUrl(b.Name) == ustkategori).Select(x => x.Id).FirstOrDefault();

            var brandside = uow.Branduow.GetAllWithQuery().Where(x => x.CategoryId.Equals(rowsId));



            var queryStringBrand = HttpUtility.ParseQueryString(Request.QueryString.ToString()).Get("prm_brand");
            if (!String.IsNullOrEmpty(queryStringBrand))
            {
                queryStringBrand = queryStringBrand.Split(",")[0];
            }

            return View(new MyProductVM
            {
                Brands = brandside
                ,BrandRoute = Convert.ToInt32(queryStringBrand)

            });


            //var brandside = uow.Branduow.GetAllWithQuery().Where(x => x.CategoryId.Equals(rowsId)).Select(x => new MyProductVM
            //{
            //    BrandId = x.Id,
            //    BrandName= x.Name

            //}) ;
            //return View(brandside);


        }
    }
}

