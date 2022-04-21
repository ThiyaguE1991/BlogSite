using BloggerPortal.Common;
using BloggerPortal.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BloggerPortal.Controllers
{
    public class AccountController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/login/login")]
        public IHttpActionResult Login(string emailId, string password)
        {
            GetResponse res = new GetResponse();
            try
            {
                dynamic objResponse = new ExpandoObject();
                dynamic user = new ExpandoObject();
                objResponse.ResponseCode = "1";
                objResponse.ResponseMessage = "Success...";
                var userDetails = UserAccount.ValidateAPILoginCredentials(emailId, password);
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
    }
}