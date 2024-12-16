using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

using ESP.Compatible;

public partial class SignOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!IsPostBack)
        {
            //SessionManager.SetSignOut(this.Session);
            //            string script = string.Empty;
            //            script = string.Format("top.location='{0}';", ConfigManager.SiteLogonUrl);
            //            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), null, script,true);

            string url = ESP.Security.PassportAuthentication.GetLogoutUrl(ConfigManager.SiteLogonUrl);
            Response.Redirect(url);
        }
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion
}
