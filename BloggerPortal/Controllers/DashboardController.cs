using BloggerPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloggerPortal.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (!SessionService.CheckLoggedInUserID())
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = DashboardServices.GetDahsboardDetails();
            LoadListViewBags("menu-dashboard");
            return View(obj);
        }
    }
}