using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KvartsShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public int CatalogId { get; set; }
        public int SubCatalogId { get; set; }
        public int SubSubCatalogId { get; set; }
    }
}