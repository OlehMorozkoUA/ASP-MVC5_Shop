using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KvartsShop.Models
{
    public class SubCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int CatalogId { get; set; }
    }
}