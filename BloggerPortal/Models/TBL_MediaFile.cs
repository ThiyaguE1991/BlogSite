namespace BloggerPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_MediaFile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_MediaFile()
        {
            TBL_Blog = new HashSet<TBL_Blog>();
        }

        [Key]
        public int MediaFileId { get; set; }

        [Required]
        [StringLength(50)]
        public string MediaFileName { get; set; }

        [Required]
        [StringLength(50)]
        public string Extension { get; set; }

        [Required]
        [StringLength(50)]
        public string FileSize { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        [Required]
        [StringLength(100)]
        public string MimeType { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        public int? DeletedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Blog> TBL_Blog { get; set; }

        public virtual TBL_User TBL_User { get; set; }

        public virtual TBL_User TBL_User1 { get; set; }

        public virtual TBL_User TBL_User2 { get; set; }
    }
}
