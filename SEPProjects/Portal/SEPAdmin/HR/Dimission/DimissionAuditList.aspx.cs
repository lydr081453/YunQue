using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Compatible;
using System.IO;

public partial class DimissionAuditList : ESP.Web.UI.PageBase
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
        string terms = string.Empty;
        if (!string.IsNullOrEmpty(txtUserCode.Text))
        {
            terms += " and a.usercode like '%" + txtUserCode.Text + "%'";
        }
        if (!string.IsNullOrEmpty(txtuserName.Text))
        {
            terms += " and a.username like '%" + txtuserName.Text + "%'";
        }
        if (!string.IsNullOrEmpty(txtDepartments.Text))
        {
            terms += " and a.departmentname like '%" + txtDepartments.Text + "%'";
        }
        if (!string.IsNullOrEmpty(txtBeginTime.Text) && !string.IsNullOrEmpty(txtEndTime.Text))
        {
            terms += " and (a.lastday between '" + txtBeginTime.Text + "' and '" + txtEndTime.Text + "') ";
        }

        DataSet ds = null;
        string hrIds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRId() + "," + System.Configuration.ConfigurationManager.AppSettings["HRAdminID"];
        string hrattendance = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRAttendanceId();
        string reception = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetReceptionIds();
        hrIds += "," + hrattendance + "," + reception;

        if (hrIds.IndexOf(CurrentUserID.ToString()) >= 0)
            ds = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetSubmitDimissionList(terms, new List<System.Data.SqlClient.SqlParameter>());
        else
            ds = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetWaitAuditList(UserID, terms, new List<System.Data.SqlClient.SqlParameter>());

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
            DataRowView dr = e.Row.DataItem as DataRowView;
            Label lblOperation = (Label)e.Row.FindControl("lblOperation");

            ESP.HumanResource.Entity.DimissionFormInfo dimissionModel = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(int.Parse(dr["DimissionId"].ToString()));
            ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionModel.DepartmentId);

            lblOperation.Text = "<a href='DimissionAuditEdit.aspx?dimissionid=" + dr["DimissionId"].ToString() + "' title='审批'><img src='../../images/edit.gif' /></a>";

            DateTime? lastDay = null;
            if (dr["LastDay"] != null && !string.IsNullOrEmpty(dr["LastDay"].ToString()))
            {
                lastDay = DateTime.Parse(dr["LastDay"].ToString()); // 离职日期
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
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitPreAuditor:
                    strStatus = "待预审";
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
                    strStatus = "待行政审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR1:
                    strStatus = "待人力资源审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT:
                    if (dimissionModel.ITAuditStatus == (int)ESP.HumanResource.Common.AuditStatus.NotAudit && dimissionModel.HRAuditStatus == (int)ESP.HumanResource.Common.AuditStatus.Audited)
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
                    strStatus = "待行政审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2:
                    strStatus = "待人力资源审批";
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

            // 判断该离职单是否是处于当前登录人审批
            bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, int.Parse(dr["DimissionId"].ToString()));

            if (!b)
            {
                e.Row.Cells[7].Text = "";
            }
            if (statusInt == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR && lastDay.Value.Date > DateTime.Now)
            {
                e.Row.Cells[7].Text = "";
            }

            if ((CurrentUserID == manageModel.HRId || CurrentUserID == manageModel.ReceptionId || CurrentUserID == manageModel.Hrattendanceid) &&
    statusInt == (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete)  // 集团人力审批权限
            {
                e.Row.Cells[7].Text = "<a href=\"DimissionFormPrint.aspx?dimissionid=" + dimissionModel.DimissionId + "\" title=\"打印离职单\" target=\"_blank\"><img src=\"../../images/printno.gif\" /></a>"
                    + "&nbsp;&nbsp;<a href=\"DimissionCertification.aspx?dimissionid=" + dimissionModel.DimissionId + "\" title=\"打印离职证明\" target=\"_blank\"><img src=\"../../images/printno2.gif\" /></a>";
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strWhere = " and a.code<>'' ";
        if (!string.IsNullOrEmpty(txtBeginTime.Text) && !string.IsNullOrEmpty(txtEndTime.Text))
        {
            strWhere += " and (di.lastday between '" + txtBeginTime.Text + "' and '" + txtEndTime.Text + "') ";
        }
        else
        {
            strWhere += " and (di.status in(2,3,4,5,6,7,8,9,10,11,12,14,15)) ";
        }

        Export(strWhere);

    }


    private void Export(string content)
    {
        try
        {

            DataSet ds = GetData(content);

            string mapPath = Server.MapPath("../");
            string tmpFileName = "Statistics" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";

            ExportStatistics(ds, mapPath, tmpFileName);

            outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName);
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(ex.ToString());
        }
    }

    private DataSet GetData(string content)
    {
        string strWhere = content;
        int[] depids = null;

        if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
        {

            return ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelByDeparmtnetID(depids, strWhere);
        }
        else
        {
            return ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModel(depids, strWhere);
        }
    }

    private void ExportStatistics(DataSet ds, string mapPath, string tmpFileName)
    {
        string filename = mapPath + "ExcelTemplate\\" + "Statistics.xls";
        Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
        Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
        Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
        Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

        sheet.Cells[1, 1] = "员工编号";
        sheet.Cells[1, 2] = "员工姓名";
        sheet.Cells[1, 3] = "英文名";
        sheet.Cells[1, 4] = "性别";
        sheet.Cells[1, 5] = "部门";
        sheet.Cells[1, 6] = "职位";
        sheet.Cells[1, 7] = "入职日期";
        sheet.Cells[1, 8] = "身份证号";
        sheet.Cells[1, 9] = "导出条件";
        sheet.Cells[1, 10] = "Last Day";

        int startRowIndex = 2;//起始行索引
        int rowIndex = 0; //顺序行索引            
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            // ESP.HumanResource.Entity.DimissionFormInfo dimission = DimissionFormManager.GetModelByUserid(int.Parse(dr["UserId"].ToString()));

            sheet.Cells[startRowIndex + rowIndex, 1] = dr["Code"].ToString();
            sheet.Cells[startRowIndex + rowIndex, 2] = dr["LastNameCN"].ToString() + dr["FirstNameCN"].ToString();
            sheet.Cells[startRowIndex + rowIndex, 3] = dr["FirstNameEN"].ToString() + " " + dr["LastNameEN"].ToString();

            sheet.Cells[startRowIndex + rowIndex, 4] = ESP.HumanResource.Common.Status.Gender_Names[int.Parse(dr["Gender"].ToString())];

            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eip = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + dr["userid"].ToString());
            if (eip.Count > 0)
            {
                sheet.Cells[startRowIndex + rowIndex, 5] = eip[0].CompanyName + "-" + eip[0].DepartmentName + "-" + eip[0].GroupName;
                sheet.Cells[startRowIndex + rowIndex, 6] = eip[0].DepartmentPositionName;
            }
            if (dr["joinDate"] != DBNull.Value && !string.IsNullOrEmpty(dr["joinDate"].ToString()))
            {
                sheet.Cells[startRowIndex + rowIndex, 7] = DateTime.Parse(dr["joinDate"].ToString()).ToString("yyyy-MM-dd");
            }
            sheet.Cells[startRowIndex + rowIndex, 8] = dr["IDNumber"].ToString().Replace("$", "");

            if (dr["LastDay"] != DBNull.Value && !string.IsNullOrEmpty(dr["LastDay"].ToString()))
            {
                sheet.Cells[startRowIndex + rowIndex, 10] = DateTime.Parse(dr["LastDay"].ToString()).ToString("yyyy-MM-dd");
            }
            rowIndex++;
        }

        workbook.Saved = true;

        try
        {
            workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            workbook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            sheet = null;
            workbook = null;
            app = null;
        }
    }

    private void outExcel(string pathandname, string filename)
    {
        Response.Clear();
        FileStream fin = new FileStream(pathandname, FileMode.Open);
        Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
        Response.AddHeader("Connection", "Close");
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.AddHeader("Content-Length", fin.Length.ToString());
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        FileInfo finfo = new FileInfo(pathandname);
        Response.ContentType = "application/ms-excel";

        byte[] buf = new byte[1024];
        while (true)
        {
            int length = fin.Read(buf, 0, buf.Length);
            if (length > 0)
                Response.OutputStream.Write(buf, 0, length);
            if (length < buf.Length)
                break;
        }
        fin.Close();
        Response.Flush();
        Response.Close();
        finfo.Delete();
    }
}
