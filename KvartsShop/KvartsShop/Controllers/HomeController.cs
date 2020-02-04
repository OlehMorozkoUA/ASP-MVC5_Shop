using KvartsShop.Models;
using KvartsShop.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KvartsShop.Controllers
{
    public class HomeController : Controller
    {
        public DataContext db = new DataContext();
        public ActionResult Index(int id = 0)
        {
            if (Session["userId"] != null)
            {
                User user = db.Users.Find(Session["userId"]);
                ViewBag.userName = user.FirstName;
            }
            ViewBag.Products = db.Products.ToList<Product>();
            ViewBag.ProductSliders = db.ProductSliders.ToList<ProductSlider>();
            ViewBag.id = id;
            ViewBag.Catalogs = db.Catalogs.ToList<Catalog>();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}