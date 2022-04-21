using BloggerPortal.Models;
using BloggerPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Services
{
    public class SessionService
    {

        /// <summary>
        /// GetLoggedInUserID 
        /// </summary>
        /// <returns></returns>
        public static byte GetLoggedInUserID()
        {
            return Convert.ToByte(HttpContext.Current.Session["UserID"]);
            // return 1;
        }


        /// <summary>
        /// GetLoggedInUserID 
        /// </summary>
        /// <returns></returns>
        public static string GetLoggedInUserName()
        {
            if(HttpContext.Current.Session["UserName"] == null)
            {
                return string.Empty;
            }
            return HttpContext.Current.Session["UserName"].ToString();
            //  return "Admin";
        }   
        
        public static string GetRoleName()
        {
            if (HttpContext.Current.Session["RoleId"] == null)
            {
                return string.Empty;
            }
            return HttpContext.Current.Session["RoleId"].ToString();
            //  return "Admin";
        }

        public static bool CheckLoggedInUserID()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["UserID"])))
                return true;

            return false;
        }



        public static void SetLoginDetail(TBL_User obj)
        {
            HttpContext.Current.Session["UserID"] = obj.UserId;
            HttpContext.Current.Session["RoleId"] = obj.RoleId;
            HttpContext.Current.Session["UserName"] = obj.UserName;
        }

        public static void RemoveLoginDetail()
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}