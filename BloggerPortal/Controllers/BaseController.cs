using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloggerPortal.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public void LoadListViewBags(string pageName)
        {
            ViewBag.ActiveMenu = pageName;
            ViewBag.PageSize = 10;
        }
    }
}