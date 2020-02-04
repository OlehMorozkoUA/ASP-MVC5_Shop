using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KvartsShop.Areas.Admin.Models
{
    public class SubCatalogAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase File { get; set; }
        public int CatalogId { get; set; }
    }
}