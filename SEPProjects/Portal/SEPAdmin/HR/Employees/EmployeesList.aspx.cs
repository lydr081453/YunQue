using System;
using System.Collections.Generic;
using ESP.HumanResource.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.WebPages;

public partial class Employees_EmployeesList : ViewPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            listBind();
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        listBind();
    }

    protected void listBind()
    {
        string strCondition = string.Empty;
        strCondition += string.Format(" and (a.status >= {0} and a.status <= {1}) and b.IsDeleted='False' ", Status.Entry,Status.WaitDimission);
        if (!string.IsNullOrEmpty(txtName.Text.Trim()))
        {
            strCondition += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%' or b.username like '%{0}%' )  ", txtName.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtCode.Text.Trim()))
        {
            strCondition += string.Format(" and a.Code like '%{0}%' ",txtCode.Text.Trim());
        }
        List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = null;
        if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
        {
            list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(strCondition);
        }
        else
        {
            list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(UserInfo, strCondition);
        }
        gvE.DataSource = list;
        gvE.DataBind();

        this.ddlCurrentPage2.Items.Clear();
        for (int i = 1; i <= this.gvE.PageCount; i++)
        {
            this.ddlCurrentPage2.Items.Add(i.ToString());
        }
        if (this.gvE.PageCount > 0)
        {
            this.ddlCurrentPage2.SelectedIndex = this.gvE.PageIndex;
        }
        if (gvE.PageCount > 1)
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
        labPageCount.Text = labPageCountT.Text = (gvE.PageIndex + 1).ToString() + "/" + gvE.PageCount.ToString();
        if (gvE.PageCount > 0)
        {
            if (gvE.PageIndex + 1 == gvE.PageCount)
                disButton("last");
            else if (gvE.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    protected void gvE_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Repeater rep = (Repeater)e.Row.FindControl("repJob");
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = (List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>)gvE.DataKeys[e.Row.RowIndex].Values[1];
            if (list.Count > 0 && list != null)
            {
                rep.DataSource = list;
                rep.DataBind();
            }
        }
    }

    #region 翻页相关
    protected void gvE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       gvE.PageIndex = e.NewPageIndex;
       listBind();
    }


    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvE.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvE.PageIndex + 2) >= gvE.PageCount ? gvE.PageCount : (gvE.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvE.PageIndex - 1) < 1 ? 0 : (gvE.PageIndex - 1));
    }
    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvE_PageIndexChanging(new object(), e);
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gvE.PageIndex = this.ddlCurrentPage2.SelectedIndex;
        listBind();
    } 

    //翻页判断
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
}
