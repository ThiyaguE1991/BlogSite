using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Models.ViewModels
{
    public class LoginVM
    {
        public string UserName { get; set; }        

        public byte RoleId { get; set; }

        public string Password { get; set; }

        public string HasKey { get; set; }
    }
}