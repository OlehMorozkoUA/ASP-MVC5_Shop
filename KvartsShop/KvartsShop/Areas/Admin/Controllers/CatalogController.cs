using KvartsShop.Areas.Admin.Models;
using KvartsShop.Models;
using KvartsShop.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KvartsShop.Areas.Admin.Controllers
{
    public class CatalogController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Del(int id)
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    db.Catalogs.Remove(db.Catalogs.Find(id));

                    IEnumerable<SubCatalog> subcatalogs = db.SubCatalogs.Where(subcatalog => subcatalog.CatalogId == id);
                    IEnumerable<SubSubCatalog> subsubcatalogs = db.SubSubCatalogs.Where(subsubcatalog => subsubcatalog.CatalogId == id);

                    if(subcatalogs != null)
                    {
                        foreach (var subcatalog in subcatalogs)
                        {
                            db.SubCatalogs.Remove(subcatalog);
                        }
                    }

                    if (subsubcatalogs != null)
                    {
                        foreach (var subsubcatalog in subsubcatalogs)
                        {
                            db.SubSubCatalogs.Remove(subsubcatalog);
                        }
                    }
                    db.SaveChanges();

                    ViewBag.Products = db.Products;
                    ViewBag.Catalogs = db.Catalogs;
                    ViewBag.SubCatalogs = db.SubCatalogs;
                    ViewBag.SubSubCatalogs = db.SubSubCatalogs;
                    return View("~/Areas/Admin/Views/Home/Admin.cshtml");
                }
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        public ActionResult Create()
        {
            if (Session["userId"] != null && db.Users.Find(Session["userId"]).Status == Status.admin)
            {
                return View();
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Create(CatalogAdmin catalogAdmin)
        {
            if (Session["userId"] != null)
            {
                try
                {
                    if (db.Users.Find(Session["userId"]).Status == Status.admin)
                    {
                        Catalog catalog = new Catalog();
                        string sliceOfPath = "/Content/Images/ImageCategory/";
                        if (catalogAdmin.File.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(catalogAdmin.File.FileName);
                            string filepath = Path.Combine(Server.MapPath("~/Content/Images/ImageCategory"), filename);
                            catalogAdmin.File.SaveAs(filepath);
                        }
                        catalog.Name = catalogAdmin.Name;
                        catalog.ImagePath = sliceOfPath + catalogAdmin.File.FileName;

                        db.Catalogs.Add(catalog);
                        db.SaveChanges();

                        ViewBag.Products = db.Products;
                        ViewBag.Catalogs = db.Catalogs;
                        ViewBag.SubCatalogs = db.SubCatalogs;
                        ViewBag.SubSubCatalogs = db.SubSubCatalogs;
                        return View("~/Areas/Admin/Views/Home/Admin.cshtml");
                    }
                    return HttpNotFound();
                }
                catch (Exception e)
                {
                    ViewBag.Products = db.Products;
                    ViewBag.Catalogs = db.Catalogs;
                    ViewBag.SubCatalogs = db.SubCatalogs;
                    ViewBag.SubSubCatalogs = db.SubSubCatalogs;
                    return View("~/Areas/Admin/Views/Home/Admin.cshtml");
                }
            }
            return HttpNotFound();           
        }
    }
}