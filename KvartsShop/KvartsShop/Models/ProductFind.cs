using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KvartsShop.Models
{
    public class ProductFind
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int PriceLow { get; set; }
        public int PriceHigh { get; set; }

        public int CatalogId { get; set; }
        public int SubCatalogId { get; set; }
        public int SubSubCatalogId { get; set; }
    }
}