using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// 已审批列表信息
/// </summary>
public partial class DimissionAuditedList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    /// <summary>
    /// 绑定待审批的离职单信息
    /// </summary>
    private void ListBind()
    {
        string where = "";
        string strWhere =  " a.auditorid=" + UserID.ToString();

        if (!string.IsNullOrEmpty(this.txtUserCode.Text))
        {
            where += string.Format(" and d.usercode like '%{0}%'", this.txtUserCode.Text);
        }
        if (!string.IsNullOrEmpty(txtuserName.Text))
        {
            where += string.Format(" and d.username like '%{0}%'", this.txtuserName.Text);
        }
        if (!string.IsNullOrEmpty(txtBeginTime.Text) && !string.IsNullOrEmpty(txtEndTime.Text))
        {
            where += string.Format(" and (d.lastday between '{0}' and '{1}')", this.txtBeginTime.Text, DateTime.Parse(this.txtEndTime.Text).AddDays(1).ToString("yyyy-MM-dd"));
        }
        List<System.Data.SqlClient.SqlParameter> parameterList = new List<System.Data.SqlClient.SqlParameter>();


        DataSet ds = null;

        if (System.Configuration.ConfigurationManager.AppSettings["AdministrativeIDs"].IndexOf("," + CurrentUser.GetDepartmentIDs()[0].ToString() + ",") >= 0)
        {
            where = " 1=1 " + where;
            ds = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAllAuditedDimissionList(CurrentUserID,where, parameterList);
        }
        else
        {
            where = strWhere + where;
            ds = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAuditedDimissionList(where, parameterList);
        }
        List<int> dimissionIdList = new List<int>();
        List<ESP.HumanResource.Entity.DimissionFormInfo> dimissionFormList = new List<ESP.HumanResource.Entity.DimissionFormInfo>();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ESP.HumanResource.Entity.DimissionFormInfo dimissionForm = new ESP.HumanResource.Entity.DimissionFormInfo();
                dimissionForm.PopupData(dr);
                if (!dimissionIdList.Contains<int>(dimissionForm.DimissionId))
                {
                    dimissionIdList.Add(dimissionForm.DimissionId);
                    dimissionFormList.Add(dimissionForm);
                }
            }
        }

        gvList.DataSource = dimissionFormList;
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
        if (gvList.Rows.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = gvList.Rows.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
    }

    /// <summary>
    /// 绑定离职单据信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            string status = e.Row.Cells[5].Text;
            ESP.HumanResource.Entity.DimissionFormInfo dr = e.Row.DataItem as ESP.HumanResource.Entity.DimissionFormInfo;
            ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dr.DepartmentId);
            DateTime? lastDay = null;
            if (dr.LastDay != null && !string.IsNullOrEmpty(dr.LastDay.ToString()))
            {
                lastDay = dr.LastDay.Value; // 离职日期
            }
            int statusInt = 1;
            if (!int.TryParse(status, out statusInt))
            {
                statusInt = 1;
            }

            string strStatus = "未提交";
            switch (statusInt)
            {
                case (int)ESP.HumanResource.Common.DimissionFormStatus.NotSubmit:
                    strStatus = "未提交";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector:
                    strStatus = "待总监审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver:
                    strStatus = "待交接人确认";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager:
                    strStatus = "待总经理审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR:
                    strStatus = "待团队行政审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR1:
                    strStatus = "待集团人力资源审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT:
                    if (dr.ITAuditStatus == (int)ESP.HumanResource.Common.AuditStatus.NotAudit && dr.HRAuditStatus == (int)ESP.HumanResource.Common.AuditStatus.Audited)
                        strStatus = "待IT审批";
                    else
                        strStatus = "待集团人力资源审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance:
                    strStatus = "待财务审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitIT:
                    strStatus = "待IT确认";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration:
                    strStatus = "待集团行政审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2:
                    strStatus = "待集团人力资源审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete:
                    strStatus = "审批通过";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule:
                    strStatus = "审批驳回";
                    break;
                default:
                    strStatus = "未提交";
                    break;
            }

            if ((CurrentUserID == manageModel.HRId || CurrentUserID == manageModel.ReceptionId || CurrentUserID==manageModel.Hrattendanceid) &&
    statusInt == (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete)  // 集团人力审批权限
            {
                e.Row.Cells[7].Text = "<a href=\"DimissionFormPrint.aspx?dimissionid=" + dr.DimissionId + "\" title=\"打印离职单\" target=\"_blank\"><img src=\"../../images/printno.gif\" /></a>"
                    + "&nbsp;&nbsp;<a href=\"DimissionCertification.aspx?dimissionid=" + dr.DimissionId + "\" title=\"打印离职证明\" target=\"_blank\"><img src=\"../../images/printno2.gif\" /></a>";
            }

            e.Row.Cells[5].Text = strStatus;
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