using BloggerPortal.Models;
using BloggerPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Services
{
    public class DashboardServices
    {
        public static DashboardVM GetDahsboardDetails()
        {
            DashboardVM obj = new DashboardVM();
            using (var dbEntity = new BloggerModel())
            {
                obj.TotalUsers = dbEntity.TBL_User.Where(w => w.RoleId == 2 && w.IsActive).Count();
                obj.TotalBloggers = dbEntity.TBL_User.Where(w => w.RoleId == 1 && w.IsActive).Count();
                obj.TotalBlogs = dbEntity.TBL_Blog.Where(w => w.IsDeleted == false && w.IsActive).Count();
                obj.TotlaComments = dbEntity.TBL_Comment.Where(w => w.IsDeleted == false && w.IsActive).Count();
            }

            return obj;
        }
    }
}