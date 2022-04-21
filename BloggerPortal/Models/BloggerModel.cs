using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BloggerPortal.Models
{
    public partial class BloggerModel : DbContext
    {
        public BloggerModel()
            : base("name=BloggerModel")
        {
        }

        public virtual DbSet<TBL_Blog> TBL_Blog { get; set; }
        public virtual DbSet<TBL_Comment> TBL_Comment { get; set; }
        public virtual DbSet<TBL_MediaFile> TBL_MediaFile { get; set; }
        public virtual DbSet<TBL_Role> TBL_Role { get; set; }
        public virtual DbSet<TBL_User> TBL_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBL_Blog>()
                .HasMany(e => e.TBL_Comment)
                .WithRequired(e => e.TBL_Blog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_MediaFile>()
                .Property(e => e.MediaFileName)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_MediaFile>()
                .Property(e => e.Extension)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_MediaFile>()
                .Property(e => e.FileSize)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_MediaFile>()
                .HasMany(e => e.TBL_Blog)
                .WithRequired(e => e.TBL_MediaFile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_Role>()
                .HasMany(e => e.TBL_User)
                .WithRequired(e => e.TBL_Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_Blog)
                .WithRequired(e => e.TBL_User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_Blog1)
                .WithOptional(e => e.TBL_User1)
                .HasForeignKey(e => e.DeletedBy);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_Blog2)
                .WithRequired(e => e.TBL_User2)
                .HasForeignKey(e => e.ModifiedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_Comment)
                .WithRequired(e => e.TBL_User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_Comment1)
                .WithOptional(e => e.TBL_User1)
                .HasForeignKey(e => e.DeletedBy);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_Comment2)
                .WithRequired(e => e.TBL_User2)
                .HasForeignKey(e => e.ModifiedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_MediaFile)
                .WithRequired(e => e.TBL_User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_MediaFile1)
                .WithOptional(e => e.TBL_User1)
                .HasForeignKey(e => e.DeletedBy);

            modelBuilder.Entity<TBL_User>()
                .HasMany(e => e.TBL_MediaFile2)
                .WithOptional(e => e.TBL_User2)
                .HasForeignKey(e => e.ModifiedBy);
        }
    }
}
