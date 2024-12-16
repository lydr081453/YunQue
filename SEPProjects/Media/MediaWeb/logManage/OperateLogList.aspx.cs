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
using ESP.Media.BusinessLogic;
public partial class logManage_OperateLogList : ESP.Web.UI.PageBase
{
    int userId = 0;
    static Hashtable htOperate = new Hashtable();
    static Hashtable htTable = new Hashtable();

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getOperateType();
            getTables();
        }
        ListBind();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtOperateName.Text = string.Empty;
        this.ddlOperateType.SelectedIndex = 0;
        this.ddlTable.SelectedIndex = 0;
        this.dpBeginDate.Text = string.Empty;
        this.dpEndDate.Text = string.Empty;
        ListBind();
    }

    /// <summary>
    /// Gets the type of the operate.
    /// </summary>
    void getOperateType()
    {
        DataTable dtOperate = OperateTypeManager.GetAll();
        if (dtOperate != null && dtOperate.Rows.Count > 0)
        {
            ddlOperateType.DataSource = dtOperate;
            ddlOperateType.DataTextField = "AltName";
            ddlOperateType.DataValueField = "ID";
            ddlOperateType.DataBind();
            if (!htOperate.ContainsKey(0))
            {
                htOperate.Add(0, "operate");
            }
            foreach (DataRow row in dtOperate.Rows)
            {
                if (!htOperate.ContainsKey(Convert.ToInt32(row["ID"])))
                {
                    htOperate.Add(Convert.ToInt32(row["ID"]), row["Name"]);
                }
            }
        }
        ddlOperateType.Items.Insert(0, new ListItem("请选择", "0"));
    }

    /// <summary>
    /// Gets the tables.
    /// </summary>
    void getTables()
    {
        DataTable dtTables = TablesManager.GetAll();
        if (dtTables != null && dtTables.Rows.Count > 0)
        {
            ddlTable.DataSource = dtTables;
            ddlTable.DataTextField = "AltTableName";
            ddlTable.DataValueField = "TableID";
            ddlTable.DataBind();
            if (!htTable.ContainsKey(0))
            {
                htTable.Add(0, "table");
            }
            foreach (DataRow row in dtTables.Rows)
            {
                if (!htTable.ContainsKey(Convert.ToInt32(row["TableID"])))
                {
                    htTable.Add(Convert.ToInt32(row["TableID"]), row["TableName"]);
                }
            }
        }
        ddlTable.Items.Insert(0, new ListItem("请选择", "0"));
    }

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
        int userid = CurrentUserID;
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent()
    {

    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "OperateLogId#UserID#OperateAltName#AltTableName#OperateTime#OperateDes";
        string strHeader = "id#操作人姓名#操作类型#操作表#操作时间#描述";
        string strSort = "OperateLogId#UserID#OperateAltName#AltTableName#OperateTime#";
        string strH = "####center#";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.dgList);
    }
    #endregion

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        StringBuilder strTerms = new StringBuilder();
        Hashtable ht = new Hashtable();
        //操作人姓名
        if (txtOperateName.Text.Length > 0 && hidChkID.Value != "")
        {
            strTerms.Append(" and a.UserID = @UserID");
            if (!ht.ContainsKey("@UserID"))
            {
                ht.Add("@UserID", hidChkID.Value);
            }
        }
        //操作类型
        if (ddlOperateType.SelectedIndex > 0)
        {
            strTerms.Append(" and  a.OperateTypeID=@OperateTypeID ");
            if (!ht.ContainsKey("@OperateTypeID"))
            {
                ht.Add("@OperateTypeID", ddlOperateType.SelectedValue);
            }
        }
        //操作表
        if (ddlTable.SelectedIndex > 0)
        {
            strTerms.Append(" and a.OperateTableID=@OperateTableID ");
            if (!ht.ContainsKey("@OperateTableID"))
            {
                ht.Add("@OperateTableID", ddlTable.SelectedValue);
            }
        }

        //操作时间
        if (dpBeginDate.Text != "")
        {
            DateTime begintime, endtime;
            begintime = Convert.ToDateTime(dpBeginDate.Text);
            endtime = Convert.ToDateTime(dpEndDate.Text + " " + "23:59:59");
            strTerms.Append(" and a.OperateTime  > @btime and a.OperateTime < @etime");
            ht.Add("@btime", begintime);
            ht.Add("@etime", endtime);
        }




        DataTable dt = OperatelogManager.GetList(strTerms.ToString(), ht);
        if (dt == null)
        {
            dt = new DataTable();
        }

        this.dgList.DataSource = dt.DefaultView;
        dgList.HideColumns(new int[] { 0 });
    }
    #endregion

    /// <summary>
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int uid = Convert.ToInt32(e.Row.Cells[1].Text);
            if (uid > 0)
            {
                e.Row.Cells[1].Text = new ESP.Compatible.Employee(uid).Name;
            }
            else
            {
                e.Row.Cells[1].Text = string.Empty;
            }
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Split(' ')[0];
        }
    }

    /// <summary>
    /// Handles the Click event of the btnSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}
