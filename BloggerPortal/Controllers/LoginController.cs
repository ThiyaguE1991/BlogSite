using BloggerPortal.Models;
using BloggerPortal.Models.ViewModels;
using BloggerPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloggerPortal.Controllers
{
    public class LoginController : Controller
    {
       
        public ActionResult Index()
        {
            SessionService.RemoveLoginDetail();
            return View(new LoginVM());
        }


        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            var login = UserAccount.ValidateLoginCredentials(model);

            if(login != null && login.UserId > 0)
            {
                SessionService.SetLoginDetail(login);
                return Json(login.RoleId, JsonRequestBehavior.AllowGet);
            }
            return Json(-100, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult CreateUser()
        {
            var obj = new TBL_User();
            ViewBag.RoleList = UserAccount.GetRoleTypeList();
            return PartialView("_CreateUser", obj);
        }


        [HttpPost]
        public ActionResult SaveUser(TBL_User model)
        {          
            var exists = UserAccount.CheckUserAlreadyExists(model.UserId, model.EmailId,model.UserName);
            if (exists)
            {
                return Json(-200, JsonRequestBehavior.AllowGet);
            }
            int UserId = UserAccount.SaveUserAccount(model, 0, false);
            if (UserId != 0)
            {               
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(-100, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            var obj = new TBL_User();
            ViewBag.RoleList = UserAccount.GetRoleTypeList();
            return PartialView("_ForgotPassword", obj);
        }

        [HttpPost]
        public ActionResult UpdateForgotPassword(string UserName, string EmailId)
        {
            TBL_User obj = new TBL_User();
            obj.UserName = UserName;
            obj.EmailId = EmailId;
            string OTP = UserAccount.SendOTP(obj);

            if (!string.IsNullOrEmpty(OTP))
            {
                //SessionService.SetLoginDetail(login);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(-100, JsonRequestBehavior.AllowGet);

        } 

        [HttpPost]
        public ActionResult CheckOTPVerification(string OTP)
        {
            string _OTP = Convert.ToString(Session["OTP"]);
            //TempData.Keep("OTP");
            if (_OTP == OTP)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateUserPassword(string pwd, string confirmpwd)
        {
            TBL_User obj = new TBL_User();
            obj.City = Convert.ToString(Session["OTP"]);
            obj.UserId = Convert.ToByte(Session["ForgetUserID"]);
            obj.Password = pwd;
            obj.ConfirmPassword = confirmpwd;
            bool isUpdated = UserAccount.UpdatePassword(obj);
            if (isUpdated == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}