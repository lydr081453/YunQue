using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUserID);
        if (emp.BaseInfoOK == false)
        {
            Response.Redirect("http://xhr.shunyagroup.com/");
        }

    }


    protected string WorkSpaceUrl
    {
        get
        {
            string workSpaceUrl = Request.QueryString["contentUrl"];
            if (workSpaceUrl != null)
            {
                workSpaceUrl = workSpaceUrl.Trim().ToLower();
                if (workSpaceUrl.Length > 0 && workSpaceUrl[0] == '/')
                    workSpaceUrl = workSpaceUrl.Substring(1);
            }


            if (workSpaceUrl == null || workSpaceUrl.Length == 0 || "default.aspx" == workSpaceUrl)
                return "Purchase/Default2.aspx";
            return workSpaceUrl;
        }
    }


}
