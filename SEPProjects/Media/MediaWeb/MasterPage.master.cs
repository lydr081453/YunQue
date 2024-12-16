using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

using ESP.Compatible;
using ESP.Framework.BusinessLogic;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbDate.Text = "今天是&nbsp; &nbsp;" + DateTime.Now.ToString("yyyy") +"年"+ DateTime.Now.ToString("MM") + "月"+DateTime.Now.ToString("dd")+"日" + "&nbsp; &nbsp; &nbsp; &nbsp;" +DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")); ;
            ESP.Framework.Entity.UserInfo currentuser = ESP.Framework.BusinessLogic.UserManager.Get();
            string chineseName = string.Empty;
            string username = string.Empty;
            if (currentuser != null)
            {
                chineseName = currentuser.FullNameCN;
                username = currentuser.Username;
            }
           // lblCaption.Text = "欢迎您，" +username+ "--" + pos;
            lblCaption.Text = "欢迎您，" + chineseName + "[" + username + "]";
            getHelp();
        }
    }

    void getHelp()
    {
        try
        {
            string pageName = Request.Path.Substring(Request.Path.LastIndexOf('/') + 1).Split('.')[0].ToString();
            string helpfilename = pageName + ".htm";
            string path = Server.MapPath("~/HelpFile/");
            string fullname = path + helpfilename;
            if (!Directory.Exists(path)) return;
            if (!File.Exists(fullname)) return;
            FileStream fs = new FileStream(fullname, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                labHelp.Text += line;
            }
            sr.Close();
            fs.Close();
        }
        catch (Exception ex)
        {

        }
    }
}
