using BloggerPortal.Common;
using BloggerPortal.Models;
using BloggerPortal.Models.ViewModels;
using BloggerPortal.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BloggerPortal.Controllers
{
    public class BlogsViewController : ApiController
    {


        #region Public Part

        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/getAllBlog")]
        public IHttpActionResult GetAlluPblicBlogList()
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                var blogList = BlogServices.GetBlogViewList();
                objResponse.blogList = blogList;
                return Ok(objResponse);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "BlogsViewController => GetBlogList");
            }
            return Ok(res.GetResponseStatus("0", "No Records"));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/getAllComments")]
        public IHttpActionResult GetAllCommentsForBlog(int blogId)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                var commentList = UserComments.GetUserComments(blogId);
                objResponse.CommentList = commentList;
                return Ok(objResponse);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "BlogsViewController => GetAllCommentsForBlog");
            }
            return Ok(res.GetResponseStatus("0", "No Records"));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/saveComment")]
        public IHttpActionResult SaveCommentForBlog(string comment, int blogId, int userId, int replyCommentId)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                var commentId = UserComments.SaveUserComments(comment, blogId, userId, replyCommentId, false);
                var commentList = UserComments.GetUserComments(blogId);
                objResponse.CommentList = commentList;
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "BlogsViewController => SaveCommentForBlog");
            }
            return Ok(res.GetResponseStatus("0", "Comment not saved..."));
        }

        #endregion



        #region Portal Part

        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/getBlogForBlogger")]
        public IHttpActionResult GetAllBlogForBlogger(int userId)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                objResponse.BlogList = BlogServices.GetBlogListForUser(userId);

                //objResponse.blogList = blogList;
                return Ok(objResponse);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "BlogsViewController => GetBlogList");
            }
            return Ok(res.GetResponseStatus("0", "No Records"));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/saveBlog")]
        public IHttpActionResult SaveBlogDetails(
            int blogId, int userId,
            string description, byte mediaType,
            int mediaFileId, string publishedDate,
            string isVisible, string title)
        {
            TBL_Blog json = new TBL_Blog();
            GetResponse res = new GetResponse();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var exists = BlogServices.CheckBlogAlreadyExists(blogId, title);
                if (exists)
                    return Ok(res.GetResponseStatus("-1", "Blog already exists..."));

                var filePath = HttpContext.Current.Server.MapPath("\\Content\\MediaFiles\\");


                if (httpRequest != null && httpRequest.Files.Count > 0)
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    foreach (string fileName in httpRequest.Files.Keys)
                    {
                        Guid mediaFileNameId = Guid.NewGuid();
                        var file = httpRequest.Files[fileName];
                        string extension = file.FileName.Split('.').LastOrDefault();
                        var savedFileName = mediaFileNameId.ToString() + "." + extension;
                        file.SaveAs(filePath + "//" + savedFileName);

                        var resultItem = UploadServices.SaveMediaFile(userId, Path.GetExtension(file.FileName),
                                        Convert.ToString(file.ContentLength),
                                        0, 0, file.ContentType, mediaFileNameId.ToString());
                        mediaFileId = resultItem.Item2;
                    }
                }

                json.UserId = userId;
                json.BlogId = blogId;
                json.Description = description;
                json.Title = title;
                json.MediaType = mediaType;
                json.MediaFileId = mediaFileId;
                json.IsVisible = isVisible == "true" ? true : false;
                json.PublishedDate = Common.DateConversion.StringToNullableDateConversion(publishedDate, "dd/MM/yyyy") ?? DateTime.Now;


                var result = BlogServices.SaveBlog(json);
                dynamic objResponse = new ExpandoObject();
                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";
                objResponse.BlogList = BlogServices.GetBlogListForUser(json.UserId);
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "SaveBlogDetails");
            }
            return Ok(res.GetResponseStatus("0", "Blog not saved."));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/removeBlog")]
        public IHttpActionResult RemoveBlog(int blogId, int userId)
        {
            GetResponse res = new GetResponse();
            try
            {
                var isDeleted = BlogServices.RemoveBlog(blogId, userId);
                dynamic objResponse = new ExpandoObject();
                if (isDeleted)
                {
                    objResponse.ResponseCode = "1";
                    objResponse.ResponseMessage = "Success...";
                    objResponse.BlogList = BlogServices.GetBlogListForUser(userId);
                    return Ok(objResponse);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "RemoveBlogDetails");
            }
            return Ok(res.GetResponseStatus("0", "Blog not deleted."));
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/getCommentsForBlog")]
        public IHttpActionResult GetCommentsForBlog(int blogId)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                objResponse.CommentList = BlogServices.GetCommentListForBlog(blogId);
                //objResponse.blogList = blogList;
                return Ok(objResponse);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "BlogsViewController => GetCommentsForBlog");
            }
            return Ok(res.GetResponseStatus("0", "No Records"));
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/getAllUsers")]
        public IHttpActionResult GetAllUser()
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();
                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";
                objResponse.UserList = UserAccount.GetAllUserList();
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "BlogsViewController => GetAllUser");
            }
            return Ok(res.GetResponseStatus("0", "No Records"));
        }



        #endregion


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}