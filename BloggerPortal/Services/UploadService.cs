using BloggerPortal.Common;
using BloggerPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BloggerPortal.Services
{
    public class UploadServices
    {
        public static Tuple<string, int> SaveMediaFile(int userId,
            string extension,
            string fileSize,
            int width,
            int height,
            string mimeType,
            string mediaFileNameId
           )
        {
            var result = SaveMediaFile(userId, extension, fileSize,
                width, height, mimeType, string.Empty, mediaFileNameId);
            return new Tuple<string, int>(result.MediaFileName, result.MediaFileId);
        }

        public static string RemoveMediaFile(byte userId, long mediaFildId, string mode)
        {
            string mediaFileName = RemoveMediaFile(userId, mediaFildId);


            return mediaFileName;
        }


        public static TBL_MediaFile SaveMediaFile(
           int userId,
           string extension,
           string fileSize,
           int width,
           int height,
           string mimeType,
           string mode,string mediaFileNameId
           )
        {
            try
            {
               
                TBL_MediaFile obj = new TBL_MediaFile();
                obj.MediaFileName = mediaFileNameId.ToString() + extension;
                obj.Extension = extension;
                obj.FileSize = fileSize;
                obj.Width = width;
                obj.Height = height;
                obj.MimeType = mimeType;
                obj.CreatedOn = DateTime.Now;
                obj.IsDeleted = false;
                obj.IsActive = true;
                obj.CreatedBy = userId;
                using (var dbEntity = new BloggerModel())
                {
                    dbEntity.TBL_MediaFile.Add(obj);
                    dbEntity.SaveChanges();
                }
                return obj;
            }
            catch (DbUpdateException ex)
            {
                UpdateException updateException = (UpdateException)ex.InnerException;
                SqlException sqlException = (SqlException)updateException.InnerException;

                foreach (SqlError error in sqlException.Errors)
                {
                    // TODO: Do something with your errors
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "Upload DP - SaveMediaFile");
                return null;
            }
        }

        public static string RemoveMediaFile(byte userId, long mediaFildId)
        {
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    var objEdit = dbEntity.TBL_MediaFile.Where(w => w.MediaFileId == mediaFildId && w.IsDeleted == false).SingleOrDefault();
                    if (objEdit == null)
                        return string.Empty;
                    objEdit.IsDeleted = true;
                    objEdit.DeletedOn = DateTime.UtcNow;
                    objEdit.DeletedBy = userId;
                    dbEntity.SaveChanges();
                    return objEdit.MediaFileName;
                    // RemoveMediaFilePhysically(objEdit.MediaFileName);
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
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "Upload DP -RemoveMediaFile");
                return null;
            }
            return null;
        }


        public static string GetMediaFileURL(int mediaFileId)
        {
            try
            {
                if(mediaFileId == 0)
                {
                    return string.Empty;
                }
                using (var dbEntity = new BloggerModel())
                {
                    var objEdit = dbEntity.TBL_MediaFile.Where(w => w.MediaFileId == mediaFileId && w.IsDeleted == false).SingleOrDefault() ?? new TBL_MediaFile();
                    return "/Content/MediaFiles/" + objEdit.MediaFileName;
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
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "Upload DP -RemoveMediaFile");
                return null;
            }
            return null;
        }

    }
}