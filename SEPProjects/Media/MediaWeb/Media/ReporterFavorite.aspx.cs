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
using System.Text;
using ESP.Compatible;

public partial class Media_ReporterFavorite : ESP.Web.UI.PageBase
{
    DataTable dt = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (Request["action"] == "add")
            {
                btnAdd_Click();
            }
            if (Request["action"] == "del")
            {
                btnDel_Click();
            }
            ListBind();
        
        
    }

    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
        int userid = CurrentUserID;
    }
    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
       
            string strColumn = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
            string strH = "center########";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);

            string strColumn2 = "reporterid#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
            string strHeader2 = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
            string sort2 = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain#####";
            string strH2 = "center########";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn2, strHeader2, sort2,strH2, this.dgUnList);
    }
    #endregion

    #region 绑定列表
    private void ListBind()
    {
        Hashtable ht = new Hashtable();

        //  this.dgList.HideColumns(new int[] { 0 });
        
        
        int userid = CurrentUserID;

        //绑定已收藏记者
        dt = ESP.Media.BusinessLogic.ReporterfavoriteManager.GetSelectedReporter(userid,null,null);        
        if (dt != null)
        {
            this.dgList.DataSource = dt.DefaultView;
        }
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }
        if (dgList.Rows.Count > 0)
        {
            this.btnReporterSign.Visible = true;
            this.btnReporterContact.Visible = true;
        }
        else
        {
            this.btnReporterSign.Visible = false;
            this.btnReporterContact.Visible = false;
        }
        //绑定未收藏记者
        DataTable dt2 = null;
        dt2 = ESP.Media.BusinessLogic.ReporterfavoriteManager.GetUnSelectedReporter(userid, null, null);
        if (dt2 != null)
        {
            this.dgUnList.DataSource = dt2.DefaultView;
        }
        for (int i = 0; i < dgUnList.Columns.Count; i++)
        {
            dgUnList.Columns[i].HeaderStyle.Wrap = false;
        }
    }
   
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "<input type='checkbox' id='chkHeader1' onclick=selectedcheck('Header1','Rep1'); value='" + e.Row.Cells[0].Text + "' />选择";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input  id='chkRep1' name='chkRep1' type='checkbox' runat='server' value='{0}'/>", e.Row.Cells[0].Text);
        }

    }
    protected void dgUnList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "<input type='checkbox' id='chkHeader2' onclick=selectedcheck('Header2','Rep2'); value='" + e.Row.Cells[0].Text + "' />选择";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input  id='chkRep2' name='chkRep2' type='checkbox' runat='server' value='{0}'/>", e.Row.Cells[0].Text);
        }

    }
    #endregion

    protected void btnAdd_Click() 
    {
        string reporter = Request["ids"].Trim();   
        
        StringBuilder tems = new StringBuilder();
        

        string[] strs = reporter.Split(',');
        int[] ints = new int[strs.Length];
        for (int i = 0; i < strs.Length; i++)
        {
            ints[i] = int.Parse(strs[i].ToString().Trim());
        }
        int ret = ESP.Media.BusinessLogic.ReporterfavoriteManager.add(ints, CurrentUserID);
        Response.Redirect("ReporterFavorite.aspx");
        
 
    }
    protected void btnDel_Click()
    {
        string reporter = Request["ids"].Trim();

        StringBuilder tems = new StringBuilder();
        

        string[] strs = reporter.Split(',');
        int[] ints = new int[strs.Length];
        for (int i = 0; i < strs.Length; i++)
        {
            ints[i] = int.Parse(strs[i].ToString().Trim());
        }
        int ret = ESP.Media.BusinessLogic.ReporterfavoriteManager.delete(ints, CurrentUserID);
        Response.Redirect("ReporterFavorite.aspx");
    }

    protected void btnReporterSign_Click(object sender, EventArgs e)
    {
        string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        string errmsg = string.Empty;

        //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveSignExcel(Response, dt, filename, out errmsg, CurrentUserID))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成媒体签到表{0}');", errmsg), true);
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //}
    }
    protected void btnReporterContact_Click(object sender, EventArgs e)
    {
        string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        string errmsg = string.Empty;

        //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveCommunicateExcel(Response, dt, filename, out errmsg, CurrentUserID))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成联络表{0}');", errmsg), true);
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //}
    }
}
