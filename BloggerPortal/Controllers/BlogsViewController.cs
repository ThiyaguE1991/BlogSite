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
        public IHttpActionResult GetAllCommentsForBlog([FromBody] CommentsListAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                var commentList = UserComments.GetUserComments(json.blogId);
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
        public IHttpActionResult SaveCommentForBlog([FromBody] SaveCommentAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                var commentId = UserComments.SaveUserComments(json.comment, json.blogId, json.userId, json.replyCommentId, false);
                var commentList = UserComments.GetUserComments(json.blogId);
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
        public IHttpActionResult GetAllBlogForBlogger([FromBody] AllBlogAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";
                objResponse.BlogList = BlogServices.GetBlogListForUser(json.userId);

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
        [Route("api/blog/mediaFileUpload")]
        public IHttpActionResult FileUpload()
        {
            GetResponse res = new GetResponse();
            try
            {
                
                var httpRequest = HttpContext.Current.Request;
                var filePath = HttpContext.Current.Server.MapPath("\\Content\\MediaFiles\\");
                int mediaFileId = 0;

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

                        var resultItem = UploadServices.SaveMediaFile(1, Path.GetExtension(file.FileName),
                                        Convert.ToString(file.ContentLength),
                                        0, 0, file.ContentType, mediaFileNameId.ToString());
                        mediaFileId = resultItem.Item2;
                    }
                    dynamic objResponse = new ExpandoObject();
                    objResponse.ResponseCode = "1";
                    objResponse.ResponseMessage = "Success...";
                    objResponse.MediaFileId = mediaFileId;
                    return Ok(objResponse);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "Blog Media Upload");
            }
            return Ok(res.GetResponseStatus("0", "Blog Media not saved."));

        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/saveBlog")]
        public IHttpActionResult SaveBlogDetails([FromBody] SaveBlogApiVM json)
        {//
            TBL_Blog obj = new TBL_Blog();
            GetResponse res = new GetResponse();
            try
            {

                var exists = BlogServices.CheckBlogAlreadyExists(json.blogId, json.title);
                if (exists)
                    return Ok(res.GetResponseStatus("-1", "Blog already exists..."));

                obj.UserId = json.userId;
                obj.BlogId = json.blogId;
                obj.Description = json.description;
                obj.Title = json.title;
                obj.MediaType = json.mediaType;
                obj.MediaFileId = json.mediaFileId;
                obj.IsVisible = json.isVisible == "true" ? true : false;
                obj.PublishedDate = Common.DateConversion.StringToNullableDateConversion(json.publishedDate, "dd/MM/yyyy") ?? DateTime.Now;


                var result = BlogServices.SaveBlog(obj);
                dynamic objResponse = new ExpandoObject();
                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";
                objResponse.BlogList = BlogServices.GetBlogListForUser(obj.UserId);
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
        public IHttpActionResult RemoveBlog([FromBody] RemoveBlogAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                var isDeleted = BlogServices.RemoveBlog(json.blogId, json.userId);
                dynamic objResponse = new ExpandoObject();
                if (isDeleted)
                {
                    objResponse.ResponseCode = "1";
                    objResponse.ResponseMessage = "Success...";
                    objResponse.BlogList = BlogServices.GetBlogListForUser(json.userId);
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
        public IHttpActionResult GetCommentsForBlog([FromBody] CommentBlogAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();

                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";

                objResponse.CommentList = BlogServices.GetCommentListForBlog(json.blogId);
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


        #region User Registration


        [AllowAnonymous]
        [HttpPost]
        [Route("api/blog/login")]
        public IHttpActionResult Login([FromBody] LoginAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();
                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";
                var userDetails = UserAccount.ValidateAPILoginCredentials(json.emailId, json.password);
                if (null == userDetails)
                {
                    return Ok(res.GetResponseStatus("0", "Incorrect Username/Password"));
                }
                objResponse.UserDetails = userDetails;
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "AccountController => Login");
            }
            return Ok(res.GetResponseStatus("0", "Incorrect Username/Password"));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/createuser")]
        public IHttpActionResult UserRegistration([FromBody] UserRegistrationAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                var exists = UserAccount.CheckUserAlreadyExists(json.userId, json.emailId, json.userName);
                if (exists)
                {
                    objResponse.ResponseCode = "-1";
                    objResponse.ResponseMessage = "User email already exists...";
                    return Ok(objResponse);
                }

                var isSaved = UserAccount.SaveUserAccount(json.userId, json.userName, json.emailId,
                    json.password, json.roleId, json.mobileNo, json.address, json.city);

                if (isSaved != 0)
                {
                    objResponse.ResponseCode = "1";
                    objResponse.ResponseMessage = "Success...";
                    // objResponse.UserList = UserAccount.GetAllUserList();
                    return Ok(objResponse);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "SaveUserAccount");
            }
            return Ok(res.GetResponseStatus("0", "User not saved..."));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/removeUser")]
        public IHttpActionResult RemoveUser([FromBody] AllBlogAPIVM json)
        {
            GetResponse res = new GetResponse();
            try
            {
                var isDeleted = UserAccount.RemoveUser(json.userId);
                dynamic objResponse = new ExpandoObject();
                if (isDeleted)
                {
                    objResponse.ResponseCode = "1";
                    objResponse.ResponseMessage = "Success...";
                    objResponse.UserList = UserAccount.GetAllUserList();
                    return Ok(objResponse);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "RemoveUserDetails");
            }
            return Ok(res.GetResponseStatus("0", "User not deleted."));
        }

        #endregion                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      


    }
}