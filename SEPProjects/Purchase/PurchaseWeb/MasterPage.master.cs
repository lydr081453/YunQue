using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string pageName = Request.Path.Split('/')[3].Split('.')[0].ToString();
                FileStream f = string.IsNullOrEmpty(Request["helpfile"]) ? File.OpenRead(Server.MapPath("~/HelpFile/" + pageName + ".htm")) : File.OpenRead(Server.MapPath("~/HelpFile/" + pageName + Request["helpfile"] + ".htm"));
                StreamReader t = new StreamReader(f);
                string line;
                while ((line = t.ReadLine()) != null)
                {
                    labHelp.Text += line;

                }
                f.Close();
            }
            catch (Exception ex)
            {

            }

            try
            {
                string pageName = Request.Path.Split('/')[3].Split('.')[0].ToString();
                FileStream f1 = string.IsNullOrEmpty(Request["helpfile"]) ? File.OpenRead(Server.MapPath("~/PageHelp/" + pageName + ".htm")) : File.OpenRead(Server.MapPath("~/PageHelp/" + pageName + Request["helpfile"] + ".htm"));
                StreamReader t1 = new StreamReader(f1);
                string line1;
                while ((line1 = t1.ReadLine()) != null)
                {
                    pageHelper.Text += line1;
                }
                f1.Close();
            }
            catch { }

        }
    }
}
