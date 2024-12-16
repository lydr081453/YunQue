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
public partial class NewMedia_System_SystemUserList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        ListBind();
    }
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
        int userid = CurrentUserID;
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
        string strColumn = "SysUserID#UserID#UserCode#EMail#UserName#Telephone#Mobile#PositionDescription";
        string strHeader = "选择#用户编号#系统名称#邮箱#用户名#电话号码#手机号码#描述";
        string strSort = "SysUserID#UserID#UserCode#EMail#UserName#Telephone#Mobile#";
        string strH = "center#######";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort,strH, this.dgEmployeeList);
    }
    #endregion
    #region 绑定列表
    private void ListBind()
    {
        //操作人姓名
        IList<Employee> dt = ESP.Media.BusinessLogic.UsersManager.GetList();
        if (txtInput.Text.Length > 0)
        {
            
            dt = ESP.MediaLinq.BusinessLogic.UsersManager.GetListByName(txtInput.Text.Trim());
        }
    

       
        if (dt == null)
        {
            dt = new List<Employee>();
        }

        this.dgEmployeeList.DataSource = dt;
        dgEmployeeList.HideColumns(new int[] { 0 });
    }
    #endregion


    protected void dgEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type='radio' id='radNo' name='radNo' value={0} runat= 'server'  />", e.Row.Cells[0].Text);
                    
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {

    }

    #region 返回总库
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtInput.Text = string.Empty;       

        ListBind();
    }
    #endregion
   
}
