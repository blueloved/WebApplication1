using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
            var data = new NewProductVM()
            {
                Price = 100,
                ProductName = "Hoodie"
            };

            ViewData["MyName"] = "Will";
            ViewData["MyTitle"] = "ASP .NET MVC 1";
            ViewBag.Title = "ASP .NET MVC 2";

            ViewBag.Prodcuts = db.Products.Take(5).ToList();
            TempData["msg"] = "Test";
            ViewData.Model = data;

            return View();
        }


    }
}