using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
 

namespace Web.Models.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "E_Page")] //bir div içinde tanımlanacak, adı my_attr olsun
    public class PageButtonTagHelper : TagHelper
    {
        private IUrlHelperFactory urlhelper;
        public PageButtonTagHelper(IUrlHelperFactory _prm)
        {
            urlhelper = _prm;
        }



        [ViewContext]
        [HtmlAttributeNotBound] //Attribute ile bağlanmayacak Contextimiz
        public ViewContext ViewContext { get; set; }
        public E_Page E_Page { get; set; } 
        public string PageAction { get; set; }//Sayfanın Linki



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlhelper.GetUrlHelper(ViewContext);

            var result = new TagBuilder("div");

            for (int i = 1; i <= E_Page.TotalPages(); i++)
            {
                var tag = new TagBuilder("a");//her sayfaya göre bir a etiketi oluşturalım.
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                tag.InnerHtml.Append(i.ToString());

                if (i == E_Page.CurrentPage)
                {
                    tag.AddCssClass("btn active");
                }
                else
                {
                    tag.AddCssClass("btn btn-primary");
                }
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}