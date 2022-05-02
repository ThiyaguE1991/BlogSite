using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloggerPortal.Common;
using BloggerPortal.Models;
using BloggerPortal.Models.ViewModels;

namespace BloggerPortal.Services
{
    public class UserComments
    {
        public static int SaveUserComments(string comment, int blogId, int userId, int replyCommentId = 0, bool isDeleteMode = false)
        {
            TBL_Comment obj_val = new TBL_Comment();
            try
            {

                obj_val.IsActive = true;
                obj_val.CommentId = 0;
                obj_val.Comment = comment;
                obj_val.BlogId = blogId;
                obj_val.CreatedOn = obj_val.ModifiedOn = DateTime.Now;
                obj_val.CreatedBy = obj_val.ModifiedBy = userId;


                obj_val.IsDeleted = isDeleteMode;

                using (var dbEntity = new BloggerModel())
                {
                    if (replyCommentId != 0)
                    {
                        obj_val.ReplyCommentId = replyCommentId;
                        obj_val.IsReply = true;
                        obj_val.BlogId = dbEntity.TBL_Comment.Where(w => w.CommentId == replyCommentId).Select(s => s.BlogId).SingleOrDefault(); ;
                    }
                    dbEntity.TBL_Comment.Add(obj_val);
                    dbEntity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "SaveUserComments");
                return 0;
            }
            return obj_val.CommentId;
        }

        public static List<CommentsListVM> GetUserComments(int blogId)
        {
            try
            {
                List<CommentsListVM> lstUserComments = new List<CommentsListVM>();
                using (var dbEntity = new BloggerModel())
                {
                    lstUserComments = (from n in dbEntity.TBL_Comment.Where(w => w.BlogId == blogId &&
                                       w.IsDeleted == false && w.IsActive == true && w.IsReply == false).ToList()
                                       join m in dbEntity.TBL_User on n.CreatedBy equals m.UserId
                                       select new CommentsListVM
                                       {
                                           CommentId = n.CommentId,
                                           UserName = m.UserName,
                                           publishDateTime = n.CreatedOn.ToString("yyyy-MM-dd hh:mm"),
                                           Comment = n.Comment,
                                           ReplyComments = GetReplyComments(n.CommentId)
                                       }).ToList();

                    if (lstUserComments.Count > 0)
                    {
                        return lstUserComments;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "GetUserComments");
            }
            return new List<CommentsListVM>();
        }


        public static List<CommentsListVM> GetReplyComments(int commentId)
        {
            try
            {
                List<CommentsListVM> lstUserComments = new List<CommentsListVM>();
                using (var dbEntity = new BloggerModel())
                {
                    lstUserComments = (from n in dbEntity.TBL_Comment.Where(w => w.ReplyCommentId == commentId &&
                                       w.IsDeleted == false && w.IsActive == true).ToList()
                                       join m in dbEntity.TBL_User on n.CreatedBy equals m.UserId
                                       select new CommentsListVM
                                       {
                                           CommentId = n.CommentId,
                                           UserName = m.UserName,
                                           publishDateTime = n.CreatedOn.ToString("yyyy-MM-dd hh:mm"),
                                           Comment = n.Comment
                                       }).ToList();

                    if (lstUserComments.Count > 0)
                    {
                        return lstUserComments;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "GetUserComments");
            }
            return new List<CommentsListVM>();
        }

    }
}