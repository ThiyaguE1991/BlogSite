using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Models.ViewModels
{
    public class BlogViewListVM
    {

        public int BlogId { get; set; }

        public string BloggerName { get; set; }

        public string publishDateTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string MediaURL { get; set; }

        public int MediaType { get; set; }

        public int TotalComments { get; set; }
    }

    public class CommentsListVM
    {
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public string publishDateTime { get; set; }
        public string Comment { get; set; }
        public bool IsReply { get; set; }
        public List<CommentsListVM> ReplyComments { get; set; }
    }

}