using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Models.ViewModels
{
    public class BlogListVM
    {
        public int BlogId { get; set; }
        public string Title { get; set; }

        //public int RowNumber { get; set; }
        public string MediaURL { get; set; }
        public string Description { get; set; }
        public string MediaType { get; set; }
        public int MediaFileId { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PublishedDateStr { get; set; }
        public bool IsVisible { get; set; }
        //public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        //public DateTime? DeletedOn { get; set; }
        //public byte CreatedBy { get; set; }
        //public byte ModifiedBy { get; set; }
        //public byte? DeletedBy { get; set; }
        //public bool? IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }


    public class UserListVM
    {
      //  public int RowNumber { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        //public string ConfirmPassword { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string RoleName { get; set; }
        public byte RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

    }


    public class CommentListVM
    {
        //public int RowNumber { get; set; }
        public int CommentId { get; set; }
        public int BlogId { get; set; }
        public string Comment { get; set; }
        public string BlogTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }

        public string ModifiedOn { get; set; }

    }
}