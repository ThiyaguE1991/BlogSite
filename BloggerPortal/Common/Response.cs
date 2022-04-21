 using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Common
{
    public class Response
    {
        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }
    }


    public class GetResponse
    {
        public Response GetResponseStatus(string StrResponseCode, string StrResponseMessage)
        {
            Response PPTR = new Response();
            PPTR.ResponseCode = StrResponseCode;
            PPTR.ResponseMessage = StrResponseMessage;
            dynamic MyModels = new ExpandoObject();
            MyModels.Response = PPTR; 
            return PPTR;
        }
    }
}