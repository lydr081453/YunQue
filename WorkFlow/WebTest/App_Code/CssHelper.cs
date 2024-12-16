using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///CssHelper 的摘要说明
/// </summary>
public class CssHelper
{
 
        static public void AddStyleSheet(Page page, string cssPath)
        {
            HtmlLink link = new HtmlLink();
            link.Href = cssPath;
            link.Attributes["rel"] = "stylesheet";
            link.Attributes["type"] = "text/css";
            page.Header.Controls.Add(link);
        }

}
