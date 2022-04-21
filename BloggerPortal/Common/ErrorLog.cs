using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BloggerPortal.Common
{
    public static class ErrorLog

    {
        public static void WriteLog(string message, string page)
        {
            message = DateTime.Now.ToString() + " ---- " + page + " ------ " + message;
            string path = HttpContext.Current.Server.MapPath("~/ErrorLog/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += DateTime.Today.ToString("yyyyMMdd") + ".txt";

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter w = File.AppendText(path))
            {
                //w.WriteLine("\r\nLog Entry : ");
                //w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = message;
                w.WriteLine(err);
                //w.WriteLine("__________________________");
                w.Flush();
                //w.Close();
            }

        }
    }
}