using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;

public partial class Employees_PassedCheckInList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    /// <summary>
    /// 绑定列表
    /// </summary>
    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        StringBuilder strWhere = new StringBuilder();

        if (txtuserName.Text.Trim() != "")
        {
            strWhere.Append(" and sysusername like '%'+@username+'%'");
            parms.Add(new SqlParameter("@username", txtuserName.Text.Trim()));
        }

        if (txtDepartments.Text.Trim() != "")
        {
            strWhere.Append(" and departmentName like '%'+@departmentname+'%'");
            parms.Add(new SqlParameter("@departmentname", txtDepartments.Text.Trim()));
        }

        if (txtBeginTime.Text.Trim() != "")
        {
            strWhere.Append(" and datediff(dd, cast('").Append(txtBeginTime.Text.Trim()).Append("' as smalldatetime), operationDate ) >= 0 ");
        }
        if (txtEndTime.Text.Trim() != "")
        {
            strWhere.Append(" and datediff(dd, cast('").Append(txtEndTime.Text.Trim()).Append("' as smalldatetime), operationDate ) <= 0");
        }
        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + UserInfo.UserID);
        string empid = "";
        
        for (int i = 0; i < empinpos.Count; i++)
        {
            empid += empinpos[i].CompanyID.ToString() + ",";
        }
        if (empid.Length > 0)
        {
            empid = empid.Substring(0, empid.Length - 1);
            strWhere.Append(string.Format(" and companyID in ({0})", empid));
        }
        List<ESP.HumanResource.Entity.PassedInfo> list = PassedManager.GetModelList(strWhere.ToString(), parms);

        gvList.DataSource = list;
        gvList.DataBind();

        if (gvList.PageCount > 1)
        {
            PageBottom.Visible = true;
            PageTop.Visible = true;
        }
        else
        {
            PageBottom.Visible = false;
            PageTop.Visible = false;
        }
        if (list.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = list.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
        if (gvList.PageCount > 0)
        {
            if (gvList.PageIndex + 1 == gvList.PageCount)
                disButton("last");
            else if (gvList.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    #region 分页设置
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvList.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvList.PageIndex + 1) > gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvList.PageIndex - 1) < 0 ? 0 : (gvList.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvList_PageIndexChanging(new object(), e);
    }

    /// <summary>
    /// 分页按钮的显示设置
    /// </summary>
    /// <param name="page"></param>
    private void disButton(string page)
    {
        switch (page)
        {
            case "first":
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = false;
                btnPrevious2.Enabled = false;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
            case "last":
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = false;
                btnLast2.Enabled = false;
                break;
            default:
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
        }
    }

    #endregion

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int id = int.Parse(e.CommandArgument.ToString());
            ESP.HumanResource.Entity.PassedInfo model = PassedManager.GetModel(id);
            PassedManager.Delete(model, LogManager.GetLogModel(UserInfo.Username + "删除了" + model.sysUserName + "的转正登记", UserInfo.UserID, UserInfo.Username, model.sysid, model.sysUserName, Status.Log));
            ListBind();
        }
    }

    /// <summary>
    /// 检索按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }
}
