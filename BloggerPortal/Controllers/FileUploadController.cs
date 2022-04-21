using BloggerPortal.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloggerPortal.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: Admin/FileUpload
        public ActionResult Index()
        {
            if (!SessionService.CheckLoggedInUserID())
            {
                return RedirectToAction("Index", "Login");
            }
            bool isSavedSuccessfully = true;

            Tuple<string, int> resultItem = new Tuple<string, int>(string.Empty, 0);
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    string id = Convert.ToString(Request.Form.GetValues("id")[0]);
                    //string mode = Convert.ToString(Request.Form.GetValues("mode")[0]);

                    Guid mediaFileNameId = Guid.NewGuid();

                    resultItem = UploadServices.SaveMediaFile(SessionService.GetLoggedInUserID(),
                      Path.GetExtension(file.FileName),
                      Convert.ToString(file.ContentLength),
                      0, 0, file.ContentType, mediaFileNameId.ToString());

                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/MediaFiles"));
                        string pathString = System.IO.Path.Combine(path.ToString());
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                        var uploadpath = string.Format("{0}\\{1}", pathString, resultItem.Item1);
                        file.SaveAs(uploadpath);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    FileName = resultItem.Item1,
                    FileId = resultItem.Item2,
                    Message = "success"
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error"
                });
            }
        }

        [HttpPost]
        public JsonResult RemoveFile(long mediaFileId, string mode)
        {
            var mediaFileName = UploadServices.RemoveMediaFile(SessionService.GetLoggedInUserID(), mediaFileId, mode);
            if (!string.IsNullOrEmpty(mediaFileName))
            {
                var path = Path.Combine(Server.MapPath("~/Content/MediaFiles"));
                string pathString = System.IO.Path.Combine(path.ToString());
                var deletePath = string.Format("{0}\\{1}", pathString, mediaFileName);
                System.IO.File.Delete(deletePath);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            bool isSavedSuccessfully = true;
            Tuple<string, int> resultItem = new Tuple<string, int>(string.Empty, 0);
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];

                        // string id = Convert.ToString(Request.Form.GetValues("UserID")[0]);
                        //string mode = Convert.ToString(Request.Form.GetValues("mode")[0]);

                        Guid mediaFileNameId = Guid.NewGuid();

                        resultItem = UploadServices.SaveMediaFile(SessionService.GetLoggedInUserID(),
                          Path.GetExtension(file.FileName),
                          Convert.ToString(file.ContentLength),
                          0, 0, file.ContentType, mediaFileNameId.ToString());

                        if (file != null && file.ContentLength > 0)
                        {
                            var path = Path.Combine(Server.MapPath("~/Content/MediaFiles"));
                            string pathString = System.IO.Path.Combine(path.ToString());
                            bool isExists = System.IO.Directory.Exists(pathString);
                            if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                            var uploadpath = string.Format("{0}\\{1}", pathString, resultItem.Item1);
                            file.SaveAs(uploadpath);
                        }


                    }
                }
                catch (Exception ex)
                {
                    isSavedSuccessfully = false;
                }
                if (isSavedSuccessfully)
                {
                    return Json(new
                    {
                        FileName = resultItem.Item1,
                        FileId = resultItem.Item2,
                        Message = "success"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Message = "Error"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    Message = "Error"
                }); 
            }
        }

    }
}