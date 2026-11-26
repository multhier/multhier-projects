using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Abstract;
using Bll.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Comments;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OgelerController : BaseController
    {

        public OgelerController(IUnitOfWork uow, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(uow, userManager, signInManager, roleManager)
        {
        }


        public IActionResult Index(string ustcategoryname, string search, string category, int? prm_page, int? prm_brand, int? sortBy, int? minPrice, int? maxPrice)
        {
            if (String.IsNullOrEmpty(ustcategoryname))
            {
                return RedirectToAction("Index","Home");
            }
            
            var page = prm_page.HasValue && prm_page.Value > 0 ? prm_page.Value : 1;
            const int pageSize = 9; // Pager'ın varsayılanı

            var branddata = uow.Branduow.GetAll().ToList();

            // Kategori slug'ı (FriendlyUrl) C# ile hesaplandığı için SQL'e çevrilemez.
            // Bu yüzden önce (küçük olan) kategori tablosundan eşleşen Id'ler bulunur,
            // ürün filtreleme/sıralama/sayfalama ise veritabanında yapılır.
            var slug = !string.IsNullOrEmpty(category) ? category : ustcategoryname;
            var categoryIds = uow.Categoryuow.GetAll()
                .Where(c => Url.FriendlyUrl(c.Name) == slug)
                .Select(c => c.Id)
                .ToList();

            var result = uow.Productuow.Search(new ProductSearchCriteria
            {
                Search = search,
                CategoryIds = categoryIds,
                BrandId = prm_brand,
                SortBy = sortBy,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Page = page,
                PageSize = pageSize
            });

            var pager = new Pager(result.TotalCount, page, pageSize);

            return View(new MyProductVM { Products = result.Items, Pager = pager, CategoryRoute = category, sortBy = sortBy, Brands = branddata, BrandRoute = prm_brand, search = search, minPrice = minPrice, maxPrice = maxPrice });
        }




        public IActionResult Detail(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            // Eskiden tüm ürünler (resim, özellik, kategoriyle) belleğe çekilip bu ürün aranıyordu.
            var product = uow.Productuow.GetProductDetail(id.Value);
            if (product == null)
            {
                // Eskiden View(null) dönülüyor ve sayfa NullReferenceException veriyordu.
                return NotFound();
            }

            var data = new ProductDetailVM()
            {
                Product = product,
                Images = product.Images,
                ProductAttributes = product.ProductAttributes,
                Categories = product.CategoryProducts.Select(a => a.Category).ToList()
            };
            return View(data);

            #region Direk IQueryable ile yazabilirdik.  
            //return View(uow.Productuow.GetAllWithQuery()
            //.Include(x => x.Images)
            //.Include(x => x.ProductAttributes)
            //.Include(x => x.CategoryProducts)
            //.ThenInclude(x => x.Category).Where(x => x.Id == id)
            //.Select(x => new ProductDetailVM()
            //{
            //Product = x,
            //Images = x.Images,
            //ProductAttributes = x.ProductAttributes,
            //Categories = x.CategoryProducts.Select(a => a.Category).ToList()
            //})
            //.FirstOrDefault());
            #endregion

        }






        //Maalesef Detail sayfası viewModel ile onunla update kısmı yarım kaldı.
        //Eğer Model olsaydı data2 gibi alır ve kolay şekilde entegre ederdik.
        //        public async Task<IActionResult> Comment(CommentVM prm, int? id)
        //        {
        //            if (!ModelState.IsValid)
        //                return RedirectToAction("Index","Home");

        //             var data2 = uow.Productuow.GetById(prm.PostId);

        //            var data = uow.Productuow.GetProductDetails().Where(x => x.Id == id)
        //       .Select(x => new ProductDetailVM()
        //       {
        //           Product = x,
        //           Images = x.Images,
        //           ProductAttributes = x.ProductAttributes,
        //           Categories = x.CategoryProducts.Select(a => a.Category).ToList()
        //       })
        //       .FirstOrDefault();




        //            if (prm.MainCommentId == 0)
        //            {
        //                data.MainComments = data.MainComments ?? new List<MainComment>();
        //                data.MainComments.Add(new MainComment
        //                {
        //                    Message = prm.Message,
        //                    AddedTime = DateTime.Now,
        //                });
        //                uow.Productuow.Update(data);
        //            }
        //            else
        //            {
        //                var comment = new SubComment
        //                {
        //                    MainCommentId = prm.MainCommentId,
        //                    Message = prm.Message,
        //                    AddedTime = DateTime.Now,
        //                };
        //                uow.Productuow.AddSubComment(comment);
        //            }
        //            await uow.SaveChangesAsyn();
        //            return RedirectToAction("Detail", new { Id = prm.PostId });
        //        }


    }
}

