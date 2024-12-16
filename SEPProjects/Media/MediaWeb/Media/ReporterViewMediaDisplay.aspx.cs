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
using ESP.Compatible;
using ESP.Media.Entity;
public partial class Media_ReporterViewMediaDisplay : ESP.Web.UI.PageBase
{
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Mid"] != null)
        {
            MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"]));
            UserControl uc;

            if (media != null)
            {
                if (media.Mediaitemtype == 1)
                {
                    uc = (UserControl)this.LoadControl("skins/PlaneMediaDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_PlaneMediaDisplay)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 3)
                {
                    uc = (UserControl)this.LoadControl("skins/TvMediaDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_TvMediaDisplay)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 2)
                {
                    uc = (UserControl)this.LoadControl("skins/WebMediaDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_WebMediaDisplay)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 4)
                {
                    uc = (UserControl)this.LoadControl("skins/DABMediaDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_DABMediaDisplay)uc).InitPage(media);
                }
                ListBind(media.Mediaitemid);
            }
        }
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
        string strColumn = "ReporterID#ReporterName#Sex#Birthday#UsualMobile#Tel_O#QQ#MSN#Experience";
        string strHeader = "选择#姓名#性别#出生日期#手机#固话#QQ#MSN#工作单位";
        string sort = "#ReporterName#Sex#Birthday######";
        string strH = "center###center#####";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion

    #region 绑定列表
    private void ListBind(int mid)
    {
        DataTable dt = ESP.Media.BusinessLogic.ReportersManager.GetList("and Media_ID=" + mid.ToString(), null);
        this.dgList.DataSource = dt.DefaultView;
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }
       this.dgList.Columns[0].Visible = false;
    }

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int mid = 0;
        if (Request["Mid"] != null)
            mid = Convert.ToInt32(Request["Mid"]);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type='checkbox'  value='{0}'/>", e.Row.Cells[0].Text);
            e.Row.Cells[0].Width = 40;

            e.Row.Cells[1].Wrap = false;

            if (e.Row.Cells[2].Text == "1")
                e.Row.Cells[2].Text = "男";
            else if (e.Row.Cells[2].Text == "2")
                e.Row.Cells[2].Text = "女";
            else
                e.Row.Cells[2].Text = "未知";
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Split(' ')[0];
            if (e.Row.Cells[3].Text.Equals("1900-1-1"))
            {
                e.Row.Cells[3].Text = "";
            }
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;

            e.Row.Cells[8].Text = GetWorkString(e.Row.Cells[8].Text);
            e.Row.Cells[8].Wrap = false;

            //e.Row.Cells[9].Text = string.Format("<a href='ReporterDisplay.aspx?Rid={0}&Mid={1}' ><img src='{2}' /></a>", e.Row.Cells[9].Text, Request["Mid"], ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);
            //e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[9].Width = 0;
        }
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
    #endregion

    protected void btnWatch_Click(object sender, EventArgs e)
    {
        if (Request["Mid"] != null)
        {
            MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"]));
           
            if (media != null)
            {
                if (media.Mediaitemtype == 1)
                {
                    Response.Redirect(string.Format("PlaneMediaContentsList.aspx?Mid={0}", Request["Mid"]));
         
                }
                else if (media.Mediaitemtype == 3)
                {
                    Response.Redirect(string.Format("TvMediaContentsList.aspx?Mid={0}", Request["Mid"]));

                }
                else if (media.Mediaitemtype == 2)
                {
                    Response.Redirect(string.Format("WebMediaContentsList.aspx?Mid={0}", Request["Mid"]));

                }
                else if (media.Mediaitemtype == 4)
                {
                    Response.Redirect(string.Format("DABMediaContentsList.aspx?Mid={0}", Request["Mid"]));

                }
            }
        }
    }
}
