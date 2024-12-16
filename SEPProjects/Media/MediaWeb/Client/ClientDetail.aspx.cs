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

public partial class Client_ClientDetail : ESP.Web.UI.PageBase
{
    private int id = 0;
    private int alertvalue = 0;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hidUrl.Value = ESP.Media.Access.Utilities.Global.Url.ClientList;
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
        }
        if (alertvalue ==1)
        {
            this.lbClose.Text = " <input type='button' value='关闭' onclick='window.close();' class='widebuttons' />";
            this.lbClose1.Text = " <input type='button' value='关闭' onclick='window.close();' class='widebuttons' />";
        }
        else
        {
            string backurl = "";
            if (!string.IsNullOrEmpty(Request["backurl"]))
                backurl = Request["backurl"];
            else
                backurl = "ClientList.aspx";
            string param = string.Empty;
            param = ESP.Media.Access.Utilities.Global.AddParam(Request.Url.Query, "alert", (alertvalue - 1).ToString());
            param = ESP.Media.Access.Utilities.Global.AddParam(Request.Url.Query, "backurl", Request["backurl"]);
            if (param.Length > 0)
            {
                param = "?" + param;
            }
            this.lbClose.Text = string.Format(" <input type='button' value='返回' onclick=\"window.location='{0}{1}';\" class='widebuttons' />",backurl, param);
            this.lbClose1.Text = string.Format(" <input type='button' value='返回' onclick=\"window.location='{0}{1}';\" class='widebuttons' />",backurl, param);
        }
        if (Request["Cid"] != null)
        {
            if (!string.IsNullOrEmpty(Request["Plid"]))
            {
                this.btnEdit.Visible = false;
                this.benEdit1.Visible = false;
            }
            else
            {
                this.btnEdit.Visible = true;
                this.benEdit1.Visible = true;
            }
            if (int.TryParse(Request["Cid"], out id))
            {
                if (!IsPostBack)
                {

                    hidClientId.Value = Request["Cid"];
                    ClientsInfo mlClient = ClientsManager.GetModel(id);
                    if (mlClient != null)
                    {
                        if (mlClient.Lastmodifiedbyuserid > 0)
                        {
                            labLastModifyUser.Text = new ESP.Compatible.Employee(mlClient.Lastmodifiedbyuserid).Name;
                        }
                        if (mlClient.Lastmodifieddate != null && mlClient.Lastmodifieddate.Length > 0)
                        {
                            labLastModifyDate.Text = mlClient.Lastmodifieddate;
                        }

                        this.labClientName.Text = "公司名称：" + mlClient.Clientcfullname;
                        this.labChFullName.Text = mlClient.Clientcfullname;
                        this.labChShortName.Text = mlClient.Clientcshortname;
                        this.labEnFullName.Text = mlClient.Clientefullname;
                        this.labEnShortName.Text = mlClient.Clienteshortname;
                        this.labBrief.Text = mlClient.Clientdescription;
                        this.imgLogo.Visible = true;
                        this.lblCategory.Text = mlClient.Categoryname;
                        if (mlClient.Clientlogo != string.Empty)
                        {
                            this.imgLogo.ImageUrl = mlClient.Clientlogo.Replace(".jpg", "_225.jpg");
                            imgTitleFull.ImageUrl = mlClient.Clientlogo.Replace(".jpg", "_full.jpg");
                        }
                        else
                        {
                            this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
                            this.imgLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath.Replace(".jpg", "_full.jpg");
                        }
                        //this.imgLogo.Width = 80;
                        //this.imgLogo.Height = 80;

                        

                        Watch();
                    }
                }
                if (id > 0)
                {
                    ListBind(id);
                }
            }
        }
        setAddProductLine();
    }

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "ProductLineTitle#ProductLineName#ProductLineDescription";
        string strHeader = "产品线图片#产品线名称#描述";
        string sort = "#ProductLineName#";
        string strH = "center##";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    /// <param name="clientID">The client ID.</param>
    private void ListBind(int clientID)
    {
        string term = "ClientID = @ClientID";
        Hashtable ht = new Hashtable();
        ht.Add("@ClientID", clientID);
        DataTable dt = ProductlinesManager.GetList(term, ht);
        this.dgList.DataSource = dt.DefaultView;


    }

    /// <summary>
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string param = string.Empty;
            param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?')+1);
            int plid = Convert.ToInt32( dgList.DataKeys[e.Row.RowIndex].Value);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", plid);
            param = ESP.Media.Access.Utilities.Global.AddParam(param,"backurl",ESP.Media.Access.Utilities.Global.Url.ClientDetail);
            if (param.IndexOf("Operate") != -1)
            {
                param = ESP.Media.Access.Utilities.Global.RemoveParam(param, "Operate");
            }
            //param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue+1).ToString());
            if (e.Row.Cells[0].Text == "&nbsp;")
            {
              //  e.Row.Cells[0].Text = string.Format("<img  src='{0}'style='width:80px;height:60px;'  runat='server'/>", ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
                //if (alertvalue==0)
                //{
                //    e.Row.Cells[0].Text = string.Format("<img src=' {0}'style='width:80px;height:60px;'  runat='server' onclick=\"window.open('ProductLineDisplay.aspx?{1}','','{2}');\"/>", ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath, param, ESP.Media.Access.Utilities.Global.OpenClass.Common);
                //}
                //else
                //{
                    e.Row.Cells[0].Text = string.Format("<img src=' {0}'style='width:80px;height:60px;'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?{1}';\"/>",  ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath,param);
                //}
            }
            else
            {
              //  e.Row.Cells[0].Text = string.Format("<img  src='{0}'style='width:80px;height:60px;'  runat='server'/>", e.Row.Cells[0].Text.Substring(e.Row.Cells[0].Text.IndexOf("~") + 1));
                //if (alertvalue==0)
                //{
                //    e.Row.Cells[0].Text = string.Format("<img src='{1}' style='width:80px;height:60px;'  runat='server' onclick=\"window.open('ProductLineDisplay.aspx?{0}','','{2}');\"/>", param, e.Row.Cells[0].Text.Substring(e.Row.Cells[0].Text.IndexOf("~") + 1), ESP.Media.Access.Utilities.Global.OpenClass.Common);
                //}
                //else
                //{
                    e.Row.Cells[0].Text = string.Format("<img src='{1}' style='width:80px;height:60px;'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?{0}';\"/>", param, e.Row.Cells[0].Text.Substring(e.Row.Cells[0].Text.IndexOf("~") + 1));
                //}
            }

            e.Row.Cells[0].Wrap = false;

            //if (alertvalue==0)
            //{
            //    e.Row.Cells[1].Text = string.Format("<a href='#' onclick=\"window.open('ProductLineDisplay.aspx?{0}','','{1}');\" >{2}</a>", param, ESP.Media.Access.Utilities.Global.OpenClass.Common, e.Row.Cells[1].Text);
            //    //e.Row.Cells[3].Text = string.Format("<a href='ProductLineAddAndEdit.aspx?Operate=EDIT&{0}' ><img src='{1}' /></a>", param, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

            //    //e.Row.Cells[3].Text = string.Format("<a href='ProductLineAddAndEdit.aspx?Operate=DEL&{0}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{1}' /></a>", param, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);
            //}
            //else
            //{
                e.Row.Cells[1].Text = string.Format("<a href='#' onclick=\"window.location='ProductLineDisplay.aspx?{0}';\" >{1}</a>", param, e.Row.Cells[1].Text);
            //}
                       

            
        }
    }
    #endregion

    #region 添加
    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("ProductLineAddAndEdit.aspx?Operate=ADD&Cid={0}&CientOperate={1}", id, Request["Operate"]));
    }
    #endregion

    /// <summary>
    /// Watches this instance.
    /// </summary>
    private void Watch()
    {
        if (Request["Cid"] != null)
        {
            //Response.Redirect(string.Format("ClientContentsList.aspx?Cid={0}", Request["Cid"]));
            string ctrl = string.Empty; ;
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", Request["Cid"]);
            if (alertvalue == 0)
            {
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", "1");
                param = ESP.Media.Access.Utilities.Global.RemoveParam(param, "backurl");
                ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.open('ClientContentsList.aspx?{0}','','{1}');\" value={2} class='widebuttons'/>", param,ESP.Media.Access.Utilities.Global.OpenClass.Common, "历史查看");
            }
            else
            {
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
                ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.location = 'ClientContentsList.aspx?{0}';\" value={1} class='widebuttons'/>", param, "历史查看");
            }
            ltOperate.Text = ctrl;   
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        //int mid = 0;
        if (Request["Cid"] != null)
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", Request["Cid"]);
        if (!string.IsNullOrEmpty(Request["alert"]))
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) + 1).ToString());

        string sname = Guid.NewGuid().ToString();// DateTime.Now.ToShortTimeString();
        List<string> trunto = new List<string>();
        trunto.Add("ClientDetail.aspx");

        Session[sname] = trunto;

        if (Request["backurl"] != null)
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "backurl", Request["backurl"]);

        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "EDIT");
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "sname", sname);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "truntocount", "0");
        Response.Redirect(string.Format("ClientAddAndEdit.aspx?{0}", param));
    }

    private void setAddProductLine()
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "ADD");
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
        param = ESP.Media.Access.Utilities.Global.AddParam(param,"backurl", "ClientDetail.aspx");
        string url = string.Format(@"ProductLineAddAndEdit.aspx?{0}", param);
        //Response.Redirect(url);
        btnLink.Attributes["onclick"] =  "javascript:window.open('" + url + "','','" + ESP.Media.Access.Utilities.Global.OpenClass.Common + "');return false;";
    }
}
