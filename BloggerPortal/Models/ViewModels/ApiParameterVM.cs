using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Models.ViewModels
{
    public class SaveCommentVM
    {
        public int BlogId { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int ReplyCommentId { get; set; }
    }


    public class LoginAPIVM
    {
        public string emailId { get; set; }
        public string password { get; set; }
    }
    public class CommentsListAPIVM
    {
        public int blogId { get; set; }
    }


    public class SaveCommentAPIVM
    {
        public string comment { get; set; }
        public int blogId { get; set; }

        public int userId { get; set; }

        public int replyCommentId { get; set; }
    }

    public class AllBlogAPIVM
    {
        public int userId { get; set; }
    }


    public class RemoveBlogAPIVM
    {
        public int userId { get; set; }
        public int blogId { get; set; }
    }

    public class CommentBlogAPIVM
    {
        public int blogId { get; set; }
    }


    public class SaveBlogApiVM
    {
        public int blogId { get; set; }
        public int userId { get; set; }
        public string description { get; set; }
        public byte mediaType { get; set; }
        public int mediaFileId { get; set; }
        public string publishedDate { get; set; }
        public string isVisible { get; set; }
        public string title { get; set; }
    }

    public class UserRegistrationAPIVM
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string emailId { get; set; }
        public string password { get; set; }
        public byte roleId { get; set; }
        public string mobileNo { get; set; }
        public string address { get; set; }
        public string city { get; set; }
    }

    public class UserDetailsVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public byte RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string OTP { get; set; }
    }


    public class BlogDetailsVM
    {
        //public partial class TBL_Blog
        //{                     
        //    public int BlogId { get; set; }
        //    public string Title { get; set; }
        //    public string Description { get; set; }
        //    public byte MediaType { get; set; }            
        //    public string MediaURL { get; set; }
        //    public int MediaFileId { get; set; }            
        //    public DateTime PublishedDate { get; set; }
        //    public string PublishedDateStr { get; set; }
        //    public bool IsVisible { get; set; }    
        //    public bool IsActive { get; set; }
        //}
    }

}