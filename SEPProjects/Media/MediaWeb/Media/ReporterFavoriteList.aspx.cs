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

public partial class Media_ReporterFavoriteList : ESP.Web.UI.PageBase
{
    int rowsNo = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
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
        
    }
    #endregion

    #region 绑定列表
    private void ListBind()
    {
        Hashtable ht = new Hashtable();

        //  this.dgList.HideColumns(new int[] { 0 });
        DataTable dt = null;



        
            dt = ESP.Media.BusinessLogic.ReportersManager.GetList("", ht);
        
        if (dt != null)
        {
            this.dgList.DataSource = dt.DefaultView;
        }
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }
    }
  
    #endregion

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input  id='cbNo' type='checkbox' runat='server' value='{0}' onclick='selectone(this)'/>", rowsNo++);
        }

    }

    //搜索记者
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string reporter = txtReporter.Text.Trim();
        string media = txtMedia.Text.Trim();
        StringBuilder tems = new StringBuilder();
        DataTable dt2 = null;
        Hashtable ht = new Hashtable();

        if (txtReporter.Text.Trim() != string.Empty)
        {
            tems.Append(" and a.reportername like '%'+@reporter+'%'");
            ht.Add("@reporter", txtReporter.Text.Trim());
        }
        if (txtMedia.Text.Trim() != string.Empty)
        {
            tems.Append(" and (media.mediacname like '%'+@media+'%' or media.ChannelName like '%'+@media+'%' or media.TopicName like '%'+@media+'%') ");
            ht.Add("@media", txtMedia.Text.Trim());
        }
        dt2 = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);
        if (dt2 != null)
        {
            this.dgList.DataSource = dt2.DefaultView;
        }
        for (int i = 0; i < dgList.Columns.Count; i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    { }
}
