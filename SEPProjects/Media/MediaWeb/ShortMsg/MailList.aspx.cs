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
using System.Collections.Generic;
public partial class ShortMsg_MailList : ESP.Web.UI.PageBase
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
        string strColumn = "id#subject#createdate#body#id#id";
        string strHeader = "选择#主题#创建日期#内容#编辑#删除";
        string strSort = "#subject#createdate#body##";
        string strH = "center####center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.dgList);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
    }
    #region 绑定列表
    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        Hashtable hts = new Hashtable();
              
        //创始人
        if (txtCreaterName.Text.Trim() != string.Empty)
    {
        IList<Employee> dtuser = ESP.Compatible.Employee.GetDataSetByName(txtCreaterName.Text);
        if (dtuser != null && dtuser.Count > 0)
        {
            string uids = string.Empty;
            for (int i = 0; i < dtuser.Count; i++)
            {
                uids += dtuser[i].SysID + ",";
            }
            uids = "(" + uids.Trim(',', ' ') + ")";
            str.Append(" and createid in " + uids);
            //ht.Add("@rname", txtCreateName.Text.Trim());
        }

        else
        {
            this.dgList.DataSource = new DataTable();
            this.dgList.DataBind();
            return;
        }
        }
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
        if (dpBeginDate.Text != "")
        {
            DateTime begintime, endtime;
            begintime = Convert.ToDateTime(dpBeginDate.Text);
            endtime = Convert.ToDateTime(dpEndDate.Text +" "+"23:59:59");
            str.Append(" and createdate >= @btime and createdate <= @etime");
            ht.Add("@btime", begintime);
            ht.Add("@etime", endtime);
        }

      

            this.dgList.HideColumns(new int[] { 0 });
            DataTable dt = ESP.Media.BusinessLogic.MailmsgManager.GetList(str.ToString(), ht);
      if(dt==null){
          dt = new DataTable();
      }
             this.dgList.DataSource = dt.DefaultView;
             dgList.Columns[3].Visible = false;
      
    }
    #endregion
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type=radio id=radNo name=radNo value={0} runat= 'server'  onclick='selectone(this)'/>", e.Row.Cells[0].Text);
            e.Row.Cells[0].Width = 40;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Split(' ')[0];
            if (e.Row.Cells[2].Text.Equals("1900-1-1"))
            {
                e.Row.Cells[2].Text = "";
            }
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[1].Text = string.Format("<a href='MailDisplay.aspx?Sid={0}' >{1}</a>", e.Row.Cells[4].Text, e.Row.Cells[1].Text);
            
            e.Row.Cells[4].Text = string.Format("<a href='MailAddAndEdit.aspx?Operate=EDIT&Sid={0}' ><img src='{1}' /></a>", e.Row.Cells[4].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);
            
            e.Row.Cells[5].Text = string.Format("<a href='MailAddAndEdit.aspx?Operate=DEL&Sid={0}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{1}' /></a>", e.Row.Cells[5].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);
            
        }

    }
    #region 查找
    protected void btnFind_Click(object sender, EventArgs e)
    {

    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("MailAddAndEdit.aspx?Operate=ADD");
    }

    #region 返回总库
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtSubject.Text = string.Empty;
        this.txtBodyKey.Text = string.Empty;
        this.txtCreaterName.Text = string.Empty;
        this.dpBeginDate.Text = "";
        this.dpEndDate.Text = "";

        ListBind();
    }
    #endregion
   
}