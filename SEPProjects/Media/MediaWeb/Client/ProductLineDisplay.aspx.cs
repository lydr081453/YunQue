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
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.BusinessLogic;
using ESP.Compatible;

public partial class Client_ProductLineDisplay : ESP.Web.UI.PageBase
{
    int alertvalue = 0;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
        }

        if (Request["backurl"] != null)
        {
            if (alertvalue > 1)
            {
                this.hidUrl.Value = Request["backurl"] + string.Format("?alert={0}&Cid={1}&Operate={2}",alertvalue-1, Request["Cid"], Request["Operate"]);
            }
            else
            {
                this.hidUrl.Value = Request["backurl"] + string.Format("?Cid={0}&Operate={1}", Request["Cid"], Request["Operate"]);
            }
        }
        else
        {
            if (Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] != null)
                this.hidUrl.Value = Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage].ToString();
            else
                this.hidUrl.Value  = "../Client/ProductLineList.aspx";
        }
        if (!IsPostBack)
        {
            InitPage();

        }
        if (alertvalue==1)
        {
            lClose.Text = "<input type='button' value='关闭' onclick='javascipt:window.close();return false;' class='widebuttons' />";lClose.Text = "<input type='button' value='关闭' onclick='javascipt:window.close();return false;' class='widebuttons' />";
            lClose1.Text = "<input type='button' value='关闭' onclick='javascipt:window.close();return false;' class='widebuttons' />";
        }
        else
        { 
            lClose.Text =string.Format( "<input type='button' value='返回' onclick=\"window.location='{0}'\" class='widebuttons' />",hidUrl.Value);
            lClose1.Text = string.Format("<input type='button' value='返回' onclick=\"window.location='{0}'\" class='widebuttons' />", hidUrl.Value);
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        int mid = 0;
        if (Request["Plid"] != null)
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", Request["Plid"]);

        string sname = Guid.NewGuid().ToString();//DateTime.Now.ToShortTimeString();
        List<string> trunto = new List<string>();
        trunto.Add("ProductLineDisplay.aspx");

        Session[sname] = trunto;

        if (Request["backurl"] != null)
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "backurl", Request["backurl"]);
        if (!string.IsNullOrEmpty(Request["alert"]))
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) + 1).ToString());

        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "EDIT");
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "sname", sname);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "truntocount", "0");
        Response.Redirect(string.Format("ProductLineAddAndEdit.aspx?{0}", param));
    }

    #region 绑定页面信息
    /// <summary>
    /// Inits the page.
    /// </summary>
    private void InitPage()
    {
        int id;
        this.Title = "编辑产品线";
        if (Request["Plid"] != null && int.TryParse(Request["Plid"], out id))
        {
            ProductlinesInfo mlProductLine = ProductlinesManager.GetModel(id);
            if(mlProductLine.Clientid != 0)
            {
                 ClientsInfo mc = ClientsManager.GetModel(mlProductLine.Clientid);
                 this.lnkClientName.Text = mc.Clientcfullname;
            }
            else
            {
                this.lnkClientName.Text = "";
            }
            if (mlProductLine != null)
            {
                if (mlProductLine.Lastmodifiedbyuserid > 0)
                {
                    labLastModifyUser.Text = new Employee(mlProductLine.Lastmodifiedbyuserid).Name;
                }
                if (mlProductLine.Lastmodifieddate != null && mlProductLine.Lastmodifieddate.Length > 0)
                {
                    labLastModifyDate.Text = mlProductLine.Lastmodifieddate;
                }
                this.labProductLineName.Text = mlProductLine.Productlinename;
                this.labDes.Text = mlProductLine.Productlinedescription;                
                this.imgTitle.Visible = true;
                if (mlProductLine.Productlinetitle != string.Empty)
                {
                   
                    this.imgTitle.ImageUrl = mlProductLine.Productlinetitle;
                    imgTitleFull.ImageUrl = mlProductLine.Productlinetitle.Replace(".jpg","_full.jpg");
                }
                else
                {
                    this.imgTitle.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
                    this.imgTitle.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath.Replace(".jpg", "_full.jpg");
                }
                this.imgTitle.Width = 80;
                this.imgTitle.Height = 80;
            }
            Watch();
        }
    }
    #endregion

    /// <summary>
    /// Watches this instance.
    /// </summary>
    private void Watch()
    {
        if (Request["Plid"] != null)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", Request["Plid"]);
            string ctrl = string.Empty;
            if (alertvalue==0)
            {
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", "1");
                param = ESP.Media.Access.Utilities.Global.RemoveParam(param, "backurl");
                ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.open('ProductLineContentsList.aspx?{0}','','{1}');\" value={2} class='widebuttons'/>",param, ESP.Media.Access.Utilities.Global.OpenClass.Common,"历史查看");
            }
            else
            {
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
                ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.location='ProductLineContentsList.aspx?{0}';\" value={1} class='widebuttons'/>", param, "历史查看");
            }
            ltOperate.Text = ctrl;
        }
    }

    protected void btnChangeClient_Click(object sender, EventArgs e)
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        string url = string.Format(@"ProductSelectClientList.aspx?{0}", param);
        Response.Redirect(url);                   
    }
}