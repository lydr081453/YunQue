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
using ESP.Compatible;
using System.Collections.Generic;
public partial class Media_MediaLinkReporterList : ESP.Web.UI.PageBase
{
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
        //string strColumn = "ReporterID#ReporterName#Sex#Birthday#UsualMobile#Tel_O#QQ#MSN#Experience#ReporterID#ReporterID#ReporterID#ReporterID";
        //string strHeader = "选择#姓名#性别#出生日期#手机#固话#QQ#MSN#工作单位#查看#编辑#删除#id";
        //string sort = "#ReporterName#Sex#Birthday#########";
        //MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        string strColumn = "ReporterID#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
        string strHeader = "选择#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
        string sort = "#ReporterName#medianame#sex#ReporterPosition#responsibledomain###";
        string strH = "center########";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.MediaLinkReporterList + "?Mid=" + Request["Mid"];
        ListBind();

        if (!IsPostBack)
        {
            if (Request["Sou"] != null) {
                btnAddReporter.Visible = false;
            
            }
        }
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtReporterName.Text = string.Empty;
        this.txtMedia.Text = string.Empty;
        this.txtMobile.Text = string.Empty;
        this.txtIdCard.Text = string.Empty;
        this.txtEmail.Text = string.Empty;
        ListBind();
    }


    #region 绑定列表
    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        //记者名称
        if (txtReporterName.Text.Trim() != string.Empty)
        {
            str.Append(" and ReporterName like '%'+@rname+'%'");
            ht.Add("@rname", txtReporterName.Text.Trim());
        }
        //手机号
        if (txtMobile.Text.Trim() != string.Empty)
        {
            str.Append(" and (UsualMobile like '%'+@mobile+'%' or BackupMobile like '%'+@mobile+'%' )");
            ht.Add("@mobile", txtMobile.Text.Trim());
        }

        //邮箱
        if (txtEmail.Text.Trim() != string.Empty)
        {
            str.Append(" and (EmailOne like '%'+@email+'%' or EmailTwo like '%'+@email+'%'or EmailThree like '%'+@email+'%') ");
            ht.Add("@email", txtEmail.Text.Trim());
        }
        //身份证号
        if (txtIdCard.Text.Trim() != string.Empty)
        {
            str.Append(" and CardNumber like '%'+@idcard+'%'");
            ht.Add("@idcard", txtIdCard.Text.Trim());
        }
        //所属媒体
        if (txtMedia.Text.Trim() != "")
        {
            str.Append(" and (media.mediacname like '%'+@MediaCName+'%' or media.ChannelName like '%'+@MediaCName+'%' or media.TopicName like '%'+@MediaCName+'%') ");
            if (!ht.ContainsKey("@MediaCName"))
            {
                ht.Add("@MediaCName", txtMedia.Text.Trim());
            }
        }

        int mid = 0;

        str.Append(" and media_id = 0");//sxc 只能显示未关联的记者

        DataTable dt;
       if (Request["Mid"] != null)
        {
            dt = ESP.Media.BusinessLogic.ReportersManager.GetList(str.ToString(), ht);
            this.dgList.DataSource = dt.DefaultView;
        }

    }
    #endregion

    #region 绑定下拉框
    private void ddlBind()
    {

    }
    #endregion

    #region 查找
    protected void btnFind_Click(object sender, EventArgs e)
    {
    }
    #endregion

    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        //int mid = 0;
        //if (Request["Mid"] != null)
        //    mid = Convert.ToInt32(Request["Mid"]);
        //Response.Redirect("MediaReporterAdd.aspx?Operate=ADD&Mid=" + mid.ToString());

        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
        if (Request["Mid"] != null)
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Mid", Request["Mid"]);
        if (!string.IsNullOrEmpty(Request["alert"]))
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (Convert.ToInt32(Request["alert"]) + 1).ToString());
        string sname = Guid.NewGuid().ToString();//DateTime.Now.ToShortTimeString();
        List<string> trunto = new List<string>();
        trunto.Add("MediaLinkReporterList.aspx");

        Session[sname] = trunto;

        if (Request["backurl"] != null)
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "backurl", Request["backurl"]);

        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "ADD");
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "sname", sname);
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "truntocount", "0");
        Response.Redirect(string.Format("MediaReporterAdd.aspx?{0}", param));
    }
    #endregion

    #region 关联到媒体
    protected void btnLink_Click(object sender, EventArgs e)
    {
        string[] ss = hidChecked.Value.Trim(',',' ').Split(',');
        int[] ls = new int[ss.Length];
        for(int i=0;i<ss.Length;i++)
        {
            ls[i] = Convert.ToInt32(ss[i]);
        }
        if (Request["Mid"] != null)
        {
            string errmsg;
            int ret = ESP.Media.BusinessLogic.ReportersManager.LinkToMedia(ls, Convert.ToInt32(Request["Mid"]), out errmsg);//
            if (ret > 0)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("opener.location = opener.location;window.location=window.location;alert('{0}');", "添加成功"), true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "opener.location = opener.location;alert('保存成功！');window.close();", true);
          
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
        }
    }
    #endregion

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int mid = 0;
        if (Request["Mid"] != null)
            mid = Convert.ToInt32(Request["Mid"]);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type='checkbox' onclick='selected(this)' value='{0}'/>", e.Row.Cells[0].Text);
            e.Row.Cells[0].Width = 40;


            int reporterId = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.Cells[1].Text = string.Format("<a href='ReporterDisplay.aspx?backurl=/Media/MediaLinkReporterList.aspx&visible=false&alert=2&Rid={0}&Mid={2}');\">{1}</a>", reporterId, e.Row.Cells[1].Text, Request["Mid"]);
        }
    }

    protected void btnMedia_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReporterSelectMediaList.aspx");
    }

    private string GetWorkString(string xml)
    {
        xml = Server.HtmlDecode(xml);
        Media_skins_Experience.InitExperienceTable();
        DataTable dt = Media_skins_Experience.ExperienceTable.Clone();
        System.IO.StringReader sr = new System.IO.StringReader(xml);
        dt.ReadXml(sr);
        if (dt.Rows.Count > 0)
            return dt.Rows[0]["单位名称"].ToString();
        else
            return xml;
    }

    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortDirection == SortDirection.Ascending)
        {
            e.SortDirection = SortDirection.Descending;
        }
        else if (e.SortDirection == SortDirection.Descending)
        {
            e.SortDirection = SortDirection.Ascending;
        }
    }

}
