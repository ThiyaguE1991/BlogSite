using BloggerPortal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;   

namespace BloggerPortal.Services
{
    public class MailSendingService
    {
        public static bool SendingMail(StringBuilder body,string toMailId,string toAliasName)
        {
            bool returnValue = false;
            try
            {
                string fromMailId = System.Configuration.ConfigurationManager.AppSettings["Mail:FromMailId"];
                string fromAliasName = System.Configuration.ConfigurationManager.AppSettings["Mail:FromAliasName"];


                string fromMailPassword = System.Configuration.ConfigurationManager.AppSettings["Mail:FromMailPassword"];
                string mailHost = System.Configuration.ConfigurationManager.AppSettings["Mail:MailHost"];
                string mailPort = System.Configuration.ConfigurationManager.AppSettings["Mail:MailPort"];
                bool isEnableSSL = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Mail:IsEnableSSL"] ?? "true");
                //string siteName = GlobalService.GetSettingsByFieldValue("Mail.SiteName");
                //string domainName = GlobalService.GetSettingsByFieldValue("Mail.DomainName");
                //string pageName = GlobalService.GetSettingsByFieldValue("Mail.PageName");
                //string subject = GlobalService.GetSettingsByFieldValue("Mail.ContactSubject");

                //body = body.Replace("##Sitename##", siteName);
                //body = body.Replace("##Domain##", domainName);
                //body = body.Replace("##Page##", pageName);

                var fromAddress = new MailAddress(fromMailId, fromAliasName);
                var toAddress = new MailAddress(toMailId, toAliasName);

                var smtp = new SmtpClient
                {
                    Host = mailHost,
                    Port = Convert.ToInt32(mailPort ?? "587"),
                    EnableSsl = isEnableSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromMailPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Blogger Site | Password Reset",
                    Body = (body ?? new StringBuilder()).ToString(),
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex.ToString(), "SendingMail");
            }
            return returnValue;
        }
    }
}