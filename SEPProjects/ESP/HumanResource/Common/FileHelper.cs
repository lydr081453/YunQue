using System;
using System.Net;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;

namespace ESP.HumanResource.Common
{
    public class FileHelper
    {
        public static void SaveFile(string fn, string body)
        {
            using (StreamWriter savefile = new StreamWriter(fn, false, System.Text.Encoding.Default))
            {
                savefile.WriteLine(body);
                savefile.Close();
            }
        }

        public static void DeleteFile(string fn)
        {
            if (!Directory.Exists(fn))
            {
                FileInfo finfo = new FileInfo(fn);
                finfo.Delete();                
            }

        }
    }
}
