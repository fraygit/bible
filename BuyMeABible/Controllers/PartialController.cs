using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyMeABible.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            if (!User.Identity.IsAuthenticated)
            {
                //Response.Redirect("/login");
            }
            return PartialView();
        }

        public ActionResult LeftMenu()
        {
            return PartialView();
        }

        public ActionResult Login()
        {
            return PartialView();
        }
    }
}