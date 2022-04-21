namespace BloggerPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_Blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Blog()
        {
            TBL_Comment = new HashSet<TBL_Comment>();
        }

        [NotMapped]
        public int UserId { get; set; }

        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        public byte MediaType { get; set; }

        [NotMapped]
        public string MediaURL { get; set; }
        public int MediaFileId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PublishedDate { get; set; }

        [NotMapped]
        public string PublishedDateStr { get; set; }

        public bool IsVisible { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public virtual TBL_User TBL_User { get; set; }

        public virtual TBL_User TBL_User1 { get; set; }

        public virtual TBL_MediaFile TBL_MediaFile { get; set; }

        public virtual TBL_User TBL_User2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Comment> TBL_Comment { get; set; }
    }
}
