using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Compatible;
public partial class ShortMsg_SendShortMsgList : ESP.Web.UI.PageBase
{
    int rowNo = 0;
    DataTable dt = null;
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
        string strColumn = "id#subject#createdate#body";
        string strHeader = "选择#主题#创建日期#内容";
        string strH = "center####center#center"; 
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, null,strH, this.dgList);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
        if (Request[RequestName.ProjectID] != null)
        {
            this.hidPjID.Value = Request[RequestName.ProjectID].ToString();
        }
        
    }
    #region 绑定列表
    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        //创始人
        //if (txtCreateName.Text.Trim() != string.Empty)
        //{
        //    str.Append(" and ReporterName like '%'+@rname+'%'");
        //    ht.Add("@rname", txtCreateName.Text.Trim());
        //}
        //主题
        if (txtSubject.Text.Trim() != string.Empty)
        {
            str.Append(" and subject like '%'+@subject+'%'");
            ht.Add("@subject", txtSubject.Text.Trim());
        }
        //关键字
        if (txtBodyKey.Text.Trim() != string.Empty)
        {
            str.Append(" and body like '%'+@body+'%'");
            ht.Add("@body", txtBodyKey.Text.Trim());
        }
        //创建日期
        if (dpBeginDate.Text!="")
        {
            DateTime begintime, endtime;
            begintime =Convert.ToDateTime( dpBeginDate.Text);
            endtime = Convert.ToDateTime(dpEndDate.Text + " " + "23:59:59");
            str.Append(" and createdate > @btime and createdate < @etime");
            ht.Add("@btime", begintime);
            ht.Add("@etime", endtime);
        }
      //  this.dgList.HideColumns(new int[] { 0 });
        dt = ESP.Media.BusinessLogic.ShortmsgManager.GetList(str.ToString(), ht);
        this.dgList.DataSource = dt.DefaultView;
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
            e.Row.Cells[0].Text = string.Format("<input type=radio id=radNo name=radNo value={0} runat= 'server'  onclick='selectone(this)'/>", e.Row.Cells[0].Text + "," + e.Row.Cells[1].Text + "," + e.Row.Cells[3].Text);
            e.Row.Cells[0].Width = 40;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Split(' ')[0];
            if (e.Row.Cells[2].Text.Equals("1900-1-1"))
            {
                e.Row.Cells[2].Text = "";
            }
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;

        }

    }
    #region 查找
    protected void btnFind_Click(object sender, EventArgs e)
    {

    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShortMsgAddAndEdit.aspx?Operate=ADD&Source=Send");
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {

        if (hidChkID.Value.Equals(""))
        {
            Response.Redirect("SendShortMsg.aspx");
        }
        else
        {
            
        int selid = Convert.ToInt32(hidChkID.Value);
        Response.Redirect(string.Format("SendShortMsg.aspx?ShortMsgID={0}",selid));
        }
    }

    #region 返回总库
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtSubject.Text = string.Empty;
        this.txtBodyKey.Text = string.Empty;
    //    this.txtCreaterName.Text = string.Empty;
        this.dpBeginDate.Text = "";
        this.dpEndDate.Text = "";

        ListBind();
    }
    #endregion
}
