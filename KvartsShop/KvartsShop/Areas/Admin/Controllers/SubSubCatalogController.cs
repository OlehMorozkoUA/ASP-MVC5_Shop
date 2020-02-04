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
    public class SubSubCatalogController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Del(int id)
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    db.SubSubCatalogs.Remove(db.SubSubCatalogs.Find(id));
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
            ViewBag.SubCatalogs = db.SubCatalogs;
            ViewBag.Catalogs = db.Catalogs;
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubSubCatalogAdmin subsubcatalogAdmin)
        {
            if (Session["userId"] != null)
            {
                try
                {
                    if (db.Users.Find(Session["userId"]).Status == Status.admin)
                    {
                        SubSubCatalog subsubcatalog = new SubSubCatalog();
                        string sliceOfPath = "/Content/Images/ImageCategory/";
                        if (subsubcatalogAdmin.File.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(subsubcatalogAdmin.File.FileName);
                            string filepath = Path.Combine(Server.MapPath("~/Content/Images/ImageCategory"), filename);
                            subsubcatalogAdmin.File.SaveAs(filepath);
                        }
                        subsubcatalog.Name = subsubcatalogAdmin.Name;
                        subsubcatalog.ImagePath = sliceOfPath + subsubcatalogAdmin.File.FileName;
                        subsubcatalog.CatalogId = subsubcatalogAdmin.CatalogId;
                        subsubcatalog.SubCatalogId = subsubcatalogAdmin.SubCatalogId;

                        db.SubSubCatalogs.Add(subsubcatalog);
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