using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ActionFilters;

namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string id)
        {
            ViewBag.Message = id + "  Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [MyFilter]
        public ActionResult Test()
        {
            System.Diagnostics.Debug.WriteLine("Test Action");

            throw new Exception("BAD");

            return View();
        }

        public ActionResult ViewTest(bool enable = true)
        {
            ViewBag.IsEnabled = enable;
            int[] data = new int[] {1, 2, 3, 4, 5};

            return View(data);
        }
    }
}