using System;
using System.Collections;
using System.Collections.Generic;
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
using ESP.Media.BusinessLogic;


public partial class JsonData_GetProjectMember : System.Web.UI.Page
{
    protected string strJsonSource = string.Empty;
    string term = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Controls.Clear();
        int start = Convert.ToInt32(Request["pageIndex"]);       //分页需要limit,start是mysql里用的（或取当页开始的记录标识编号）
        int limit = Convert.ToInt32(Request["pageCount"]);  //或取每页记录数
        string sort = "";            //或取排序方向
        string dir = "";              //或取所要排序的字段名
        term = "";
        GetJsonSouceString(start, limit, sort, dir);

        Response.Write(strJsonSource);
    }


    private void GetJsonSouceString(int start, int limit, string sort, string dir)
    {

    }
}
