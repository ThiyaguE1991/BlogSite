using BloggerPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloggerPortal.Controllers
{
    public class BlogViewController : Controller
    {
        // GET: BlogView
        public ActionResult Index()
        {
            //if (!SessionService.CheckLoggedInUserID())
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            var list = BlogServices.GetBlogViewList();
            return View(list);
        }


        public int SaveComments(int blogId, string comments)
        {
            int commentId = UserComments.SaveUserComments(comments, blogId, SessionService.GetLoggedInUserID(),0, false);
            return commentId;
        }


        public PartialViewResult LoadComments(int blogId)
        {
            var list = UserComments.GetUserComments(blogId);
            return PartialView("_ViewComments",list);
        }

    }
}