namespace BloggerPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Comment { get; set; }

        public int BlogId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DeletedOn { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public int? ReplyCommentId { get; set; }

        public bool? IsReply { get; set; }

        public virtual TBL_Blog TBL_Blog { get; set; }

        public virtual TBL_User TBL_User { get; set; }

        public virtual TBL_User TBL_User1 { get; set; }

        public virtual TBL_User TBL_User2 { get; set; }
    }
}
