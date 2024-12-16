using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.HumanResource.Common;
using ESP.HumanResource.BusinessLogic;

public partial class Transfer_PromotionTransfer : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ListBind();
        }

    }

    private void ListBind()
    {
        string strWhere = "";
        if (txtuserName.Text.Trim() != "")
        {
            strWhere = string.Format(" and sysUserName like '%{0}%'", txtuserName.Text.Trim());
        }

        if (txtBeginTime.Text.Trim() != "")
        {
            strWhere = string.Format(" and datediff(dd, cast('{0}' as smalldatetime), operationDate ) >= 0 ", txtBeginTime.Text.Trim());
        }
        if (txtEndTime.Text.Trim() != "")
        {
            strWhere = string.Format(" and datediff(dd, cast('{0}' as smalldatetime), operationDate ) <= 0", txtEndTime.Text.Trim());
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
            strWhere +=(string.Format(" and companyID in ({0})", empid));
        }

        DataSet ds = PromotionManager.GetList(strWhere);
        gvList.DataSource = ds;
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
        if (ds.Tables[0].Rows.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = ds.Tables[0].Rows.Count.ToString();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int id = int.Parse(e.CommandArgument.ToString());
            ESP.HumanResource.Entity.PromotionInfo model = PromotionManager.GetModel(id);
            PromotionManager.Delete(model, LogManager.GetLogModel(UserInfo.Username + "删除了" + model.sysUserName + "的职位调整登记", UserInfo.UserID, UserInfo.Username, model.sysId, model.sysUserName, Status.Log));

            ListBind();
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

}
