using Model;
using System.Collections.Generic;

namespace Bll.Abstract
{
    public class ProductSearchCriteria
    {
        public string Search { get; set; }

        /// <summary>
        /// null: kategori filtresi yok. Boş liste: eşleşen kategori yok, sonuç da boş olur.
        /// </summary>
        public ICollection<int> CategoryIds { get; set; }

        public int? BrandId { get; set; }

        /// <summary>2: yeni çıkanlar, 3: ucuzdan pahalıya, 4: pahalıdan ucuza, diğer: Id'ye göre.</summary>
        public int? SortBy { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 9;
    }

    public class ProductSearchResult
    {
        public List<Product> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
