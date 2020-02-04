using KvartsShop.Areas.Admin.Models;
using KvartsShop.Models;
using KvartsShop.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KvartsShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public DataContext db = new DataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["userId"] != null)
            {
                Session["userId"] = null;
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            User user = db.Users.Find(Session["userId"]);
            foreach (User u in db.Users)
            {
                if (u.Email == user.Email && u.Password == user.Password && u.Status == Status.admin)
                {
                    ViewBag.Products = db.Products;
                    return View("Admin");
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            ViewBag.Email = user.Email;
            ViewBag.Password = user.Password;
            ViewBag.Users = db.Users;
            foreach (User u in db.Users)
            {
                if (u.Email == user.Email && u.Password == user.Password && u.Status == Status.admin)
                {
                    ViewBag.Products = db.Products;
                    ViewBag.Catalogs = db.Catalogs;
                    ViewBag.SubCatalogs = db.SubCatalogs;
                    ViewBag.SubSubCatalogs = db.SubSubCatalogs;
                    Session["userId"] = u.Id;
                    return View("Admin");
                }
                if (u.Email == user.Email && u.Password == user.Password && u.Status == Status.user)
                {
                    Session["userId"] = u.Id;
                    ViewBag.User = u;
                    return View("User");
                }
            }
            return View("Registration");
        }

        public ActionResult Registration()
        {
            return View("Registration");
        }
        [HttpPost]
        public ActionResult Registration(User user)
        {
            user.Status = Status.user;
            db.Users.Add(user);
            db.SaveChanges();

            ViewBag.User = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            Session["userId"] = ViewBag.User.Id;
            
            return View("User");
        }

        public ActionResult Find(ProductFindAdmin productFindAdmin)
        {           
            try
            {
                if (Session["userId"] != null)
                {
                    if (db.Users.Find(Session["userId"]).Status == Status.admin)
                    {
                        if (productFindAdmin.Name != null && productFindAdmin.Brand != null)
                        {
                            ViewBag.Products = db.Products.Where(product => product.Brand == productFindAdmin.Brand && product.Name == productFindAdmin.Name);
                        }
                        else if (productFindAdmin.Name == null && productFindAdmin.Brand != null)
                        {
                            ViewBag.Products = db.Products.Where(product => product.Brand == productFindAdmin.Brand);
                        }
                        else if (productFindAdmin.Name != null && productFindAdmin.Brand == null)
                        {
                            ViewBag.Products = db.Products.Where(product => product.Name == productFindAdmin.Name);
                        }
                        else
                        {
                            ViewBag.Products = db.Products;
                        }
                        return View("Admin");
                    }
                    return HttpNotFound();
                }                
                return HttpNotFound();
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Del(int id)
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    db.Products.Remove(db.Products.Find(id));
                    db.SaveChanges();
                    return Redirect("/Admin/Home/Login");
                }
                return HttpNotFound();
            }          
            return HttpNotFound();
        }

        public ActionResult Det(int id)
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    ViewBag.Catalog = db.Catalogs.Find(db.Products.Find(id).CatalogId).Name;
                    ViewBag.SubCatalog = db.SubCatalogs.Find(db.Products.Find(id).SubCatalogId).Name;
                    ViewBag.SubSubCatalog = db.SubSubCatalogs.Find(db.Products.Find(id).SubSubCatalogId).Name;
                    return View(db.Products.Find(id));
                }
                return HttpNotFound();
            }
            return HttpNotFound();           
        }

        public ActionResult Upd(int id)
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    Product person = db.Products.Find(id);
                    if (person == null)
                    {
                        return HttpNotFound();
                    }
                    return View(person);
                }
                return HttpNotFound();
            }           
            return HttpNotFound();           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upd(ProductAdmin productAdmin)
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    Product product = db.Products.Find(productAdmin.Id);
                    string sliceOfPath = "/Content/Images/Product/";
                    if (productAdmin.File.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(productAdmin.File.FileName);
                        string filepath = Path.Combine(Server.MapPath("~/Content/Images/Product"), filename);
                        productAdmin.File.SaveAs(filepath);
                    }
                    product.Brand = productAdmin.Brand;
                    product.Name = productAdmin.Name;
                    product.Price = productAdmin.Price;
                    product.ImagePath = sliceOfPath + productAdmin.File.FileName;
                    product.Description = productAdmin.Description;
                    product.FullDescription = productAdmin.FullDescription;
                    product.CatalogId = productAdmin.CatalogId;
                    product.SubCatalogId = productAdmin.SubCatalogId;
                    product.SubSubCatalogId = productAdmin.SubSubCatalogId;
                    if (ModelState.IsValid)
                    {
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    return View(product);
                }
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        public ActionResult Add()
        {
            if (Session["userId"] != null)
            {
                if (db.Users.Find(Session["userId"]).Status == Status.admin)
                {
                    return View();
                }
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Add(ProductAdmin productAdmin)
        {
            if (Session["userId"] != null)
            {
                try
                {
                    if (db.Users.Find(Session["userId"]).Status == Status.admin)
                    {
                        Product product = new Product();
                        string sliceOfPath = "/Content/Images/Product/";
                        if (productAdmin.File.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(productAdmin.File.FileName);
                            string filepath = Path.Combine(Server.MapPath("~/Content/Images/Product"), filename);
                            productAdmin.File.SaveAs(filepath);
                        }
                        product.Brand = productAdmin.Brand;
                        product.Name = productAdmin.Name;
                        product.Price = productAdmin.Price;
                        product.ImagePath = sliceOfPath + productAdmin.File.FileName;
                        product.Description = productAdmin.Description;
                        product.FullDescription = productAdmin.FullDescription;
                        product.CatalogId = productAdmin.CatalogId;
                        product.SubCatalogId = productAdmin.SubCatalogId;
                        product.SubSubCatalogId = productAdmin.SubSubCatalogId;

                        db.Products.Add(product);
                        db.SaveChanges();

                        ViewBag.Catalogs = db.Catalogs;

                        return Redirect("/Admin/Home/Login");
                    }
                    return HttpNotFound();                  
                }
                catch
                {
                    return Redirect("/Admin/Home/Login");
                }               
            }
            return HttpNotFound();           
        }
    }
}