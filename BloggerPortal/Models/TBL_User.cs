namespace BloggerPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_User()
        {
            TBL_Blog = new HashSet<TBL_Blog>();
            TBL_Blog1 = new HashSet<TBL_Blog>();
            TBL_Blog2 = new HashSet<TBL_Blog>();
            TBL_Comment = new HashSet<TBL_Comment>();
            TBL_Comment1 = new HashSet<TBL_Comment>();
            TBL_Comment2 = new HashSet<TBL_Comment>();
            TBL_MediaFile = new HashSet<TBL_MediaFile>();
            TBL_MediaFile1 = new HashSet<TBL_MediaFile>();
            TBL_MediaFile2 = new HashSet<TBL_MediaFile>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; } 
        
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [StringLength(200)]
        public string EmailId { get; set; }

        [StringLength(20)]
        public string MobileNo { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        public byte RoleId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public string OTP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Blog> TBL_Blog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Blog> TBL_Blog1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Blog> TBL_Blog2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Comment> TBL_Comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Comment> TBL_Comment1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Comment> TBL_Comment2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_MediaFile> TBL_MediaFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_MediaFile> TBL_MediaFile1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_MediaFile> TBL_MediaFile2 { get; set; }

        public virtual TBL_Role TBL_Role { get; set; }
    }
}
