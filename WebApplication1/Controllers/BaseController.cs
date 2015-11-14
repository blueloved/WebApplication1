using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        public ActionResult Debug()
        {
            return View();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            if (Request.IsLocal)
            {
                this.Redirect("/?unknown-action=" + actionName).ExecuteResult(this.ControllerContext);
            }
            else
            {
                base.HandleUnknownAction(actionName);

            }
        }
    }
}