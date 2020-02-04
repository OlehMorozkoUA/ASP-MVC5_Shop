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
    public class SubCatalogController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Del(int id)
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    db.SubCatalogs.Remove(db.SubCatalogs.Find(id));

                    IEnumerable<SubSubCatalog> subsubcatalogs = db.SubSubCatalogs.Where(subsubcatalog => subsubcatalog.SubCatalogId == id);

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
            ViewBag.Catalogs = db.Catalogs;
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubCatalogAdmin subcatalogAdmin)
        {
            if (Session["userId"] != null)
            {
                try
                {
                    if (db.Users.Find(Session["userId"]).Status == Status.admin)
                    {
                        SubCatalog subcatalog = new SubCatalog();
                        string sliceOfPath = "/Content/Images/ImageCategory/";
                        if (subcatalogAdmin.File.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(subcatalogAdmin.File.FileName);
                            string filepath = Path.Combine(Server.MapPath("~/Content/Images/ImageCategory"), filename);
                            subcatalogAdmin.File.SaveAs(filepath);
                        }
                        subcatalog.Name = subcatalogAdmin.Name;
                        subcatalog.ImagePath = sliceOfPath + subcatalogAdmin.File.FileName;
                        subcatalog.CatalogId = subcatalogAdmin.CatalogId;

                        db.SubCatalogs.Add(subcatalog);
                        db.SaveChanges();

                        ViewBag.Products = db.Products;
                        ViewBag.Catalogs = db.Catalogs;
                        ViewBag.SubCatalogs = db.SubCatalogs;
                        ViewBag.SubSubCatalogs = db.SubSubCatalogs;
                        return View("~/Areas/Admin/Views/Home/Admin.cshtml");
                    }
                    return HttpNotFound();
                }
                catch (Exception)
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