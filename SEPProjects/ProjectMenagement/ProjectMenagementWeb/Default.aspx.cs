using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ExtExtenders;
using ESP.Compatible;
using ESP.Finance.Utility;

namespace FinanceWeb
{
    public partial class NewDefault :ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUserID);
                if (emp.BaseInfoOK == false)
                {
                    Response.Redirect("http://xhr.shunyagroup.com/");
                }
                ESP.Framework.Entity.UserInfo currentuser = ESP.Framework.BusinessLogic.UserManager.Get();
                if (currentuser != null)
                    labUserName.Text += currentuser.FullNameCN;

                src.Attributes["src"] = WorkSpaceUrl;

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
                    return "project/Default.aspx";
                return workSpaceUrl;
            }
        }
    }
}