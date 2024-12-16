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
using System.Text;
using ESP.Media.Entity;
using ESP.Media.BusinessLogic;
//using System.Collections;


public partial class Client_ProductSelectClientList : ESP.Web.UI.PageBase
{
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {

    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "ClientID#ClientLogo#ClientCFullName#ClientCShortName#ClientEFullName#ClientEShortName#ClientDescription";
        string strHeader = "选择#Logo#中文全称#中文简称#英文全称#英文简称#描述";
        string sort = "##ClientCFullName#ClientCShortName#ClientEFullName#ClientEShortName#";
        string strH = "#center#####";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,sort,strH, this.dgList);
    }
    #endregion

    int alertvalue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
            btnBack.Visible = false;
            btnClose.Visible = true;
        }
        
        ListBind();
        dgList.Columns[5].Visible = false;
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtChFullName.Text = string.Empty;
        this.txtChShortName.Text = string.Empty;
        this.txtEnFullName.Text = string.Empty;
        this.txtEnShortName.Text = string.Empty;
        ListBind();
    }

    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(string.Format("ClientAddAndEdit.aspx?Operate=ADD&CientOperate={0}",Request["Operate"]));
    }
    #endregion

    #region 绑定列表
    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        if (txtChFullName.Text.Trim() != "")
        {
            str.Append(" and ClientCFullName like '%'+@cfname+'%'");
            ht.Add("@cfname", txtChFullName.Text.Trim());
        }
        if (txtChShortName.Text.Trim() != string.Empty)
        {
            str.Append(" and ClientCShortName like '%'+@csname+'%'");
            ht.Add("@csname", txtChShortName.Text.Trim());
        }
        if (txtEnFullName.Text.Trim() != string.Empty)
        {
            str.Append(" and ClientEFullName like '%'+@efname+'%'");
            ht.Add("@efname", txtEnFullName.Text.Trim());
        }
        if (txtEnShortName.Text.Trim() != string.Empty)
        {
            str.Append(" and ClientEShortName like '%'+@esname+'%'");
            ht.Add("@esname", txtEnShortName.Text.Trim());
        }
        DataTable dt = ClientsManager.GetList(str.ToString(),ht);
        this.dgList.DataSource = dt.DefaultView;
    }

    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {
    }

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = Convert.ToInt32(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", id);
            param = ESP.Media.Access.Utilities.Global.AddParam(param,"CientOperate",string.IsNullOrEmpty( Request["Operate"])?string.Empty:Request["Operate"]);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", alertvalue + 1);
            e.Row.Cells[0].Text = string.Format("<input type=radio id=radNo name=radNo value={0} runat= 'server'  onclick='selectone(this)'/>", e.Row.Cells[0].Text);
           
            if (e.Row.Cells[1].Text == "&nbsp;")
            {
                if(alertvalue > 0)
                  //  e.Row.Cells[1].Text = string.Format("<a href='ClientDetail.aspx?backurl=ProductSelectClientList.aspx&{0}'><img  style='width:80px;height:60px;'runat='server' src='{1}' /></a>", param, ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
                    e.Row.Cells[1].Text = string.Format("<img  style='width:80px;height:60px;'runat='server' src='{0}' />",  ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
                else
                 //   e.Row.Cells[1].Text = string.Format("<img  style='width:80px;height:60px;'runat='server' src='{0}' onclick=\"window.open('ClientDetail.aspx?{1}','','{2}');\"/>", ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath, param, ESP.Media.Access.Utilities.Global.OpenClass.Common);
                e.Row.Cells[1].Text = string.Format("<img  style='width:80px;height:60px;'runat='server' src='{0}'/>", ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
            }
            else
            {
                if (alertvalue > 0)
                //    e.Row.Cells[1].Text = string.Format("<a href='ClientDetail.aspx?backurl=ProductSelectClientList.aspx&{0}'><img style='width:80px;height:60px;'  runat='server'  src='{0}' /></a>", param, e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1));
                    e.Row.Cells[1].Text = string.Format("<img style='width:80px;height:60px;'  runat='server'  src='{0}' />",  e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1));
                else
                 //   e.Row.Cells[1].Text = string.Format("<img style='width:80px;height:60px;'  runat='server'  src='{0}' onclick=\"window.open('ClientDetail.aspx?{1}','','{2}');\"/>", e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1), param, ESP.Media.Access.Utilities.Global.OpenClass.Common);
                    e.Row.Cells[1].Text = string.Format("<img style='width:80px;height:60px;'  runat='server'  src='{0}' />", e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1));
            }

            e.Row.Cells[1].Width = 120;
            e.Row.Cells[1].Wrap = false;


            //if (alertvalue > 0)
            //    e.Row.Cells[2].Text = string.Format("<a href='ClientDetail.aspx?backurl=ProductSelectClientList.aspx&{0}'>{1}</a>", param, e.Row.Cells[2].Text);                
            //else
            //    e.Row.Cells[2].Text = string.Format("<a href='#'  onclick=\"window.open('ClientDetail.aspx?{1}','','{2}');\">{0}</a>", e.Row.Cells[2].Text, param, ESP.Media.Access.Utilities.Global.OpenClass.Common);
           
        }
    }
    #endregion


    protected void btnOk_Click(object sender, System.EventArgs e)
    {
        int clientid = Convert.ToInt32(this.hidChkID.Value.Trim());
        if (clientid == 0)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format(";alert('请选择关联客户！');return false;"), true);
            return;
        }
        string js = "";
        ClientsInfo customer = ClientsManager.GetModel(clientid);
        if (string.IsNullOrEmpty(Request["Plid"]) || Request["Operate"] == "Edit")
        {
            js += "<script>opener.document.all.ctl00_contentMain_txtCustom.value = '" + customer.Clientcfullname + "';</script>";
            js += "<script>opener.document.all.ctl00_contentMain_hidCustom.value = '" + customer.Clientid + "';</script>";
            js += "<script>window.close();</script>";
            
        }
        else
        {
            string errmsg = "";
            ProductlinesInfo mp = ProductlinesManager.GetModel(int.Parse(Request["Plid"]));
            mp.Clientid = clientid;
            int ret = ProductlinesManager.Update(mp, null, CurrentUserID, out errmsg);
            js += "<script>opener.document.all.ctl00_contentMain_lnkClientName.innerText = '" + customer.Clientcfullname + "';</script>";
            js += "<script>window.close();</script>";
        }
        Response.Write(js);
    }

    protected void btnBack_Click(object sender, System.EventArgs e)
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
       
        if (alertvalue > 0)
        {
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
        }
        string url = string.Format(@"ProductLineDisplay.aspx?{0}", param);
        Response.Redirect(url);
    }
}