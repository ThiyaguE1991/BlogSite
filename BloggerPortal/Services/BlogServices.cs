using BloggerPortal.Common;
using BloggerPortal.Models;
using BloggerPortal.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BloggerPortal.Services
{
    public class BlogServices
    {

        public static string SQLDBConnectionString = ConfigurationManager.ConnectionStrings["BloggerModelStr"].ToString();

        public static int SaveBlog(TBL_Blog obj, bool isDeleteMode = false)
        {
            var result = SaveBlog(obj, obj.UserId, isDeleteMode);
            return result;
        }

        public static int DeleteBlog(int id)
        {
            using (var dbEntity = new BloggerModel())
            {
                var objEdit = dbEntity.TBL_Blog.Where(d => d.BlogId == id).SingleOrDefault();
                objEdit.DeletedOn = DateTime.Now;
                objEdit.DeletedBy = SessionService.GetLoggedInUserID();
                objEdit.IsDeleted = true;
                dbEntity.SaveChanges();
            }
            return 1;
        }


        public static int SaveBlog(TBL_Blog obj, int userId, bool isDeleteMode = false)
        {
            try
            {
                if (obj.BlogId == 0)
                {
                    obj.CreatedOn = obj.PublishedDate = obj.ModifiedOn = DateTime.Now;
                    obj.IsDeleted = false;
                    obj.IsActive = true;
                    obj.CreatedBy = obj.ModifiedBy = userId;
                    using (var dbEntity = new BloggerModel())
                    {
                        dbEntity.TBL_Blog.Add(obj);
                        dbEntity.SaveChanges();
                    }
                }
                else
                {
                    using (var db = new BloggerModel())
                    {
                        var objEdit = db.TBL_Blog.Where(d => d.BlogId == obj.BlogId).SingleOrDefault();
                        if (!isDeleteMode)
                        {
                            objEdit.Title = obj.Title;
                            objEdit.Description = obj.Description;
                            objEdit.MediaType = obj.MediaType;
                            objEdit.MediaFileId = obj.MediaFileId;
                            objEdit.PublishedDate = DateTime.Now;
                            objEdit.IsVisible = obj.IsVisible;
                            objEdit.ModifiedOn = DateTime.Now;
                            objEdit.ModifiedBy = userId;
                        }
                        else
                        {
                            objEdit.DeletedOn = DateTime.Now;
                            objEdit.DeletedBy = userId;
                            objEdit.IsDeleted = true;
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                UpdateException updateException = (UpdateException)ex.InnerException;
                SqlException sqlException = (SqlException)updateException.InnerException;

                foreach (SqlError error in sqlException.Errors)
                {
                    // TODO: Do something with your errors
                }
                //return null;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "SaveBlog");
                return 0;
            }
            return obj.BlogId;
        }

        public static TBL_Blog GetBlog(int blogId)
        {
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    return dbEntity.TBL_Blog.Where(w => w.BlogId == blogId && w.IsDeleted == false && w.IsActive).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "Get Blog");
            }
            return new TBL_Blog();

        }

        public static bool CheckBlogAlreadyExists(int blogId, string title)
        {
            List<TBL_Blog> result = new List<TBL_Blog>();
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    if (blogId > 0)
                        result = dbEntity.TBL_Blog.Where(w => w.BlogId != blogId && title == w.Title && w.IsActive && w.IsDeleted == false).ToList();
                    else
                        result = dbEntity.TBL_Blog.Where(w => title == w.Title && w.IsActive && w.IsDeleted == false).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "CheckBlogAlreadyExists");
            }

            if (result != null && result.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static List<DropDownListVM> GetMediaTypeList()
        {
            var obj = new DropDownListVM(1, "Image");
            var list = new List<DropDownListVM>();

            list.Add(obj);
            obj = new DropDownListVM(2, "Video");

            list.Add(obj);

            return list;
        }

        public static List<BlogViewListVM> GetBlogViewList()
        {
            StringBuilder mJsonResponse = new StringBuilder();
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[3];

                using (SqlDataReader reader = SqlHelper.ExecuteReader(
                    SQLDBConnectionString,
                    CommandType.StoredProcedure,
                    "Admin_GetBlogViewList"
                  ,
                    commandParameters))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            mJsonResponse.Append(Convert.ToString(reader.GetValue(0)));
                        }
                    }
                }
                return JsonConvert.DeserializeObject<List<BlogViewListVM>>(mJsonResponse.ToString()).ToList(); ;
            }
            catch (Exception ex)
            {
                //  LogService.AddGeneralExceptionLogs(ex, System.Reflection.MethodBase.GetCurrentMethod(), LogMessageType.PortalRequest, Guid.Empty, 4, "PORTAL");
            }
            return new List<BlogViewListVM>();
        }

        public static List<BlogListVM> GetBlogListForUser(int userId)
        {
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    var list = (from n in dbEntity.TBL_Blog
                                join m in dbEntity.TBL_MediaFile on n.MediaFileId equals m.MediaFileId
                                where n.IsDeleted == false && n.IsActive == true && n.CreatedBy == userId
                                select new BlogListVM
                                {
                                    //RowNumber = 0,
                                    BlogId = n.BlogId,
                                    Title = n.Title,
                                    Description = n.Description,
                                    MediaType = n.MediaType == 1 ? "Image" : "Video",
                                    MediaURL = "/Content/MediaFiles/" + m.MediaFileName,
                                    IsActive = n.IsActive,
                                    ModifiedOn = n.ModifiedOn
                                }).OrderByDescending(n => n.ModifiedOn).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

            }
            return new List<BlogListVM>();
        }

        public static bool RemoveBlog(int blogId, int userId)
        {
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    var objEdit = dbEntity.TBL_Blog.Where(d => d.BlogId == blogId).SingleOrDefault();
                    if (objEdit != null)
                    {
                        objEdit.DeletedOn = DateTime.Now;
                        objEdit.DeletedBy = userId;
                        objEdit.IsDeleted = true;
                        dbEntity.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static List<CommentListVM> GetCommentListForBlog(int blogId)
        {
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    var list = (from n in dbEntity.TBL_Comment
                                join m in dbEntity.TBL_Blog on n.BlogId equals m.BlogId
                                join u in dbEntity.TBL_User on n.CreatedBy equals u.UserId
                                where n.IsDeleted == false && n.IsActive == true && n.BlogId == blogId
                                select new CommentListVM
                                {
                                    //RowNumber = 0,
                                    BlogId = n.BlogId,
                                    BlogTitle = m.Title,
                                    Comment = n.Comment,
                                    CommentId = n.CommentId,
                                    CreatedOn = n.CreatedOn,
                                    IsActive = n.IsActive,
                                    UserName = u.UserName
                                }).OrderByDescending(n => n.CreatedOn).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
            }
            return new List<CommentListVM>();
        }

    }
}