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
using ESP.Media.BusinessLogic;
public partial class logManage_LogUserList : ESP.Web.UI.PageBase
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        ListBind();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtInput.Text = string.Empty;
        ListBind();
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
        string strColumn = "SysUserID#UserCode#EMail#UserName#Telephone#Mobile#PositionDescription";
        string strHeader = "选择#用户编号#邮箱#用户名#电话号码#手机号码#描述";
        string strSort = "SysUserID#UserCode#EMail#UserName#Telephone#Mobile#";
        string strH = "center######";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.dgEmployeeList);
    }
    #endregion

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        //操作人姓名
        IList<Employee> dt = ESP.Media.BusinessLogic.UsersManager.GetList();
        if (txtInput.Text.Length > 0)
        {

            dt = UsersManager.GetListByName(txtInput.Text.Trim());
        }



        if (dt == null)
        {
            dt = new List<Employee>();
        }

        this.dgEmployeeList.DataSource = dt;
    }
    #endregion

    /// <summary>
    /// Handles the RowDataBound event of the dgEmployeeList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type='radio' id='radNo' name='radNo' value={0} runat= 'server' onclick='selectone(this)' />", e.Row.Cells[0].Text + "," + e.Row.Cells[3].Text);

        }
    }

    /// <summary>
    /// Handles the Click event of the btnFind control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnFind_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Handles the Click event of the btnSel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSel_Click(object sender, EventArgs e)
    {

        Response.Redirect(string.Format("../logManage/OperateLogList.aspx?userId={0}", hidChkID.Value));
    }
}
