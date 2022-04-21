using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using BloggerPortal.Common;
using BloggerPortal.Models;
using BloggerPortal.Models.ViewModels;

namespace BloggerPortal.Services
{
    public class UserAccount
    {
        public static int SaveUserAccount(TBL_User obj, byte userId, bool isDeleteMode = false)
        {
            try
            {
                TBL_User obj_val = new TBL_User();
                if (obj.UserId == 0)
                {
                    obj_val.CreatedOn = DateTime.Now;
                    obj_val.IsActive = true;
                    //  obj_val.UserId = 0;
                    obj_val.UserName = obj.UserName;
                    obj_val.EmailId = obj.EmailId;
                    obj_val.ConfirmPassword = obj.ConfirmPassword;
                    obj_val.Password = obj.Password;
                    obj_val.RoleId = obj.RoleId;
                    obj_val.MobileNo = obj.MobileNo;
                    obj_val.Address = obj.Address;
                    obj_val.City = obj.City;

                    using (var dbEntity = new BloggerModel())
                    {
                        dbEntity.TBL_User.Add(obj_val);
                        dbEntity.SaveChanges();
                        obj.UserId = obj_val.UserId;
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "Save User");
                return 0;
            }
            return obj.UserId;
        }

        public static TBL_User ValidateLoginCredentials(LoginVM model)
        {
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    return dbEntity.TBL_User.Where(w => w.UserName == model.UserName && w.Password == model.Password && w.IsActive == true).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "ValidateLoginAccount");
            }
            return new TBL_User();

        }


        public static UserDetailsVM ValidateAPILoginCredentials(string emailId, string password)
        {
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    return (from n in
                                dbEntity.TBL_User.Where(w => w.EmailId == emailId
                                && w.Password == password && w.IsActive == true)
                            select new UserDetailsVM
                            {
                                UserId = n.UserId,
                                UserName = n.UserName,
                                EmailId = n.EmailId,
                                MobileNo = n.MobileNo,
                                City = n.City,
                                Address = n.Address,
                                IsActive = n.IsActive,
                                RoleId = n.RoleId,
                                OTP = n.OTP
                            }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "ValidateAPILoginAccount");
            }
            return new UserDetailsVM();

        }



        public static List<DropDownListVM> GetRoleTypeList()
        {
            var obj = new DropDownListVM(1, "Admin");
            var list = new List<DropDownListVM>();

            list.Add(obj);
            obj = new DropDownListVM(2, "User");

            list.Add(obj);

            return list;
        }

        public static bool CheckUserAlreadyExists(int userId, string email, string userName)
        {
            List<TBL_User> result = new List<TBL_User>();
            try
            {
                using (var dbEntity = new BloggerModel())
                {
                    if (userId > 0)
                        result = dbEntity.TBL_User.Where(w => w.UserId != userId && (email == w.EmailId || w.UserName == userName) && w.IsActive).ToList();
                    else
                        result = dbEntity.TBL_User.Where(w => (email == w.EmailId || w.UserName == userName)).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "CheckUserAlreadyExists");
            }

            if (result != null && result.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static StringBuilder GetOTPHTML(string otp)
        {
            StringBuilder bodyText = new StringBuilder();
            string templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/OtpMailTemplate.html");
            bodyText.Append(System.IO.File.ReadAllText(templatePath));

            bodyText = bodyText.Replace("##otp##", otp);

            return bodyText;
        }

        public static string SendOTP(TBL_User model)
        {
            try
            {
                string otp = GenerateRandomOTP(4);
                HttpContext.Current.Session["OTP"] = otp;
                HttpContext.Current.Session["Username"] = model.UserName;
                using (var dbEntity = new BloggerModel())
                {
                    var objEdit = dbEntity.TBL_User.Where(w => w.UserName == model.UserName && w.EmailId == model.EmailId && w.IsActive == true).SingleOrDefault();
                    objEdit.UserId = objEdit.UserId;
                    objEdit.UserName = model.UserName;
                    objEdit.EmailId = model.EmailId;
                    objEdit.OTP = otp;
                    dbEntity.SaveChanges();
                    HttpContext.Current.Session["ForgetUserID"] = objEdit.UserId;
                }
                // TRigger OTP to register user mailId

                var bodyHTML = GetOTPHTML(otp);

                var thred = new Thread(() =>
                {
                    MailSendingService.SendingMail(bodyHTML, model.EmailId, model.UserName);
                });
                thred.Start();
                return otp;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "SendOTP");
            }
            return "";

        }

        /// <summary>
        /// Generate the random otp for forgot password
        /// </summary>
        /// <param name="iOTPLength"></param>
        /// <returns></returns>
        public static string GenerateRandomOTP(int iOTPLength)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;

        }

        public static bool UpdatePassword(TBL_User model)
        {
            try
            {

                using (var dbEntity = new BloggerModel())
                {
                    var objEdit = dbEntity.TBL_User.Where(w => w.UserId == model.UserId && w.IsActive == true).SingleOrDefault();
                    objEdit.UserId = objEdit.UserId;
                    objEdit.UserName = objEdit.UserName;
                    objEdit.Password = model.ConfirmPassword;
                    objEdit.City = model.City;
                    dbEntity.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "ValidateLoginAccount");
            }
            return false;

        }


        public static List<UserListVM> GetAllUserList()
        {
            try
            {
                int count = 1;
                using (var dbEntity = new BloggerModel())
                {
                    var list = (from n in dbEntity.TBL_User
                                join m in dbEntity.TBL_Role on n.RoleId equals m.RoleId
                                where n.IsActive == true
                                select new UserListVM
                                {                                    
                                    UserId = n.UserId,
                                    RoleId = n.RoleId,
                                    RoleName = m.RoleName,
                                    City = n.City,
                                    Address = n.Address,
                                    EmailId = n.EmailId,
                                    MobileNo = n.MobileNo,
                                    CreatedOn = n.CreatedOn,
                                    IsActive = n.IsActive,
                                    UserName = n.UserName
                                }).OrderByDescending(n => n.CreatedOn).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
            }
            return new List<UserListVM>();
        }

    }
}