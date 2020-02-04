using KvartsShop.Models;
using KvartsShop.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KvartsShop.Controllers
{
    public class ProductController : Controller
    {
        public DataContext db = new DataContext();

        public ActionResult Index(int id, int? otherId = 0)
        {
            ViewBag.id = otherId;
            ViewBag.catalogId = id;
            ViewBag.Products = db.Products.Where(product => product.CatalogId == id).ToList<Product>();
            ViewBag.Title = db.Catalogs.Where(catalog => catalog.Id == id).Select(catalog => catalog.Name).First();
            ViewBag.SubCatalogs = db.SubCatalogs.Where(subcategory => subcategory.CatalogId == id).ToList<SubCatalog>();
            ViewBag.NameSubCatalogs = db.SubCatalogs.Where(subcategory => subcategory.CatalogId == id).Select(subcatalog => subcatalog.Name);
            ViewBag.SubSubCatalogs = db.SubSubCatalogs.Where(subsubcategory => subsubcategory.CatalogId == id).ToList<SubSubCatalog>();
            ViewBag.NameSubSubCatalogs = db.SubSubCatalogs.Where(subsubcategory => subsubcategory.CatalogId == id).Select(subcatalog => subcatalog.Name);
            ViewBag.Max = db.Products.Where(product => product.CatalogId == id).Select(product => product.Price).Max();
            ViewBag.Min = db.Products.Where(product => product.CatalogId == id).Select(product => product.Price).Min();
            return View();
        }

        [HttpPost]
        public ActionResult Find(ProductFind productFind, int? otherId = 0)
        {
            ViewBag.ProductFind = productFind;
            ViewBag.Name = productFind.Name;
            ViewBag.catalogId = productFind.CatalogId;
            int subCatalogId = productFind.SubCatalogId;
            int subSubCatalogId = productFind.SubSubCatalogId;
            ViewBag.subCatalogId = subCatalogId;
            ViewBag.subSubCatalogId = subSubCatalogId;

            if (productFind.Name != null && productFind.Brand != null)
            {
                ViewBag.Products = db.Products.Where(product =>
                product.CatalogId == productFind.CatalogId && product.Name == productFind.Name && product.Brand == productFind.Brand
                && product.SubCatalogId == subCatalogId
                && product.SubSubCatalogId == subSubCatalogId
                && product.Price > productFind.PriceLow && product.Price < productFind.PriceHigh).ToList<Product>();
            }
            else if (productFind.Name != null && productFind.Brand == null)
            {
                ViewBag.Products = db.Products.Where(product =>
                product.CatalogId == productFind.CatalogId && product.Name == productFind.Name
                && product.SubCatalogId == subCatalogId
                && product.SubSubCatalogId == subSubCatalogId
                && product.Price > productFind.PriceLow && product.Price < productFind.PriceHigh).ToList<Product>();
            }
            else if (productFind.Name == null && productFind.Brand != null)
            {
                ViewBag.Products = db.Products.Where(product =>
                product.CatalogId == productFind.CatalogId && product.Brand == productFind.Brand
                && product.SubCatalogId == subCatalogId
                && product.SubSubCatalogId == subSubCatalogId
                && product.Price > productFind.PriceLow && product.Price < productFind.PriceHigh).ToList<Product>();
            }
            else
            {
                var products = db.Products.Where(product =>
                product.CatalogId == productFind.CatalogId
                && product.SubCatalogId == subCatalogId
                && product.SubSubCatalogId == subSubCatalogId
                && product.Price > productFind.PriceLow && product.Price < productFind.PriceHigh).ToList<Product>();
                ViewBag.Products = products;
                ViewBag.Title = db.Catalogs.Where(catalog => catalog.Id == productFind.CatalogId).Select(catalog => catalog.Name).First();
                ViewBag.SubCatalogs = db.SubCatalogs.Where(subcategory => subcategory.CatalogId == productFind.CatalogId).ToList<SubCatalog>();
                ViewBag.NameSubCatalogs = db.SubCatalogs.Where(subcategory => subcategory.CatalogId == productFind.CatalogId).Select(subcatalog => subcatalog.Name);
                ViewBag.SubSubCatalogs = db.SubSubCatalogs.Where(subsubcategory => subsubcategory.CatalogId == productFind.CatalogId).ToList<SubSubCatalog>();
                ViewBag.NameSubSubCatalogs = db.SubSubCatalogs.Where(subsubcategory => subsubcategory.CatalogId == productFind.CatalogId).Select(subcatalog => subcatalog.Name);
                ViewBag.Max = db.Products.Where(product => product.CatalogId == productFind.CatalogId).Select(product => product.Price).Max();
                ViewBag.Min = db.Products.Where(product => product.CatalogId == productFind.CatalogId).Select(product => product.Price).Min();
                return View("Index");
            }

            ViewBag.Title = db.Catalogs.Where(catalog => catalog.Id == productFind.CatalogId).Select(catalog => catalog.Name).First();
            ViewBag.SubCatalogs = db.SubCatalogs.Where(subcategory => subcategory.CatalogId == productFind.CatalogId).ToList<SubCatalog>();
            ViewBag.NameSubCatalogs = db.SubCatalogs.Where(subcategory => subcategory.CatalogId == productFind.CatalogId).Select(subcatalog => subcatalog.Name);
            ViewBag.SubSubCatalogs = db.SubSubCatalogs.Where(subsubcategory => subsubcategory.CatalogId == productFind.CatalogId).ToList<SubSubCatalog>();
            ViewBag.NameSubSubCatalogs = db.SubSubCatalogs.Where(subsubcategory => subsubcategory.CatalogId == productFind.CatalogId).Select(subcatalog => subcatalog.Name);
            ViewBag.Max = db.Products.Where(product => product.CatalogId == productFind.CatalogId).Select(product => product.Price).Max();
            ViewBag.Min = db.Products.Where(product => product.CatalogId == productFind.CatalogId).Select(product => product.Price).Min();
            return View("Index");
        }
    }
}