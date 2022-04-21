using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Models.ViewModels
{
    public class DashboardVM
    {
        public int TotalUsers { get; set; }

        public int TotalBlogs { get; set; }

        public int TotlaComments { get; set; }

        public int TotalBloggers { get; set; }
    }
}