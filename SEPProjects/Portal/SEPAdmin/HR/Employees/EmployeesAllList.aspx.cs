using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Compatible;
using System.IO;

public partial class Employees_EmployeesAllList : ESP.Web.UI.PageBase
{
    private string clientId = string.Empty;
    private string searchType = string.Empty;
    private int DeptID = 0;
    private string deptName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Employees_EmployeesAllList));
        #endregion

        if (!IsPostBack)
        {
            DepartmentDataBind();
            DrpBataBind();
            ListBind();
        }
    }

    private void DrpBataBind()
    {
        DataSet ds = StatisticsTypeManager.GetList(" IsDeleted=0");
    }

    private void ListBind()
    {
        string strWhere = " and (a.status = 1 or a.status = 3) ";
        if (txtITCode.Text.Trim() != "")
        {
            strWhere += string.Format(" and (a.code like '%{0}%')", txtITCode.Text.Trim());
        }
        if (txtuserName.Text.Trim() != "")
        {
            strWhere += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%'  or b.username like '%{0}%' )", txtuserName.Text.Trim());
        }
        if (txtContractCompany.Text.Trim() != "")
        {
            strWhere += string.Format(" and (c.ContractCompany like '%{0}%') ", txtContractCompany.Text.Trim());
        }
        if (txtFinishSchool.Text.Trim() != "")
        {
            strWhere += string.Format(" and (a.GraduatedFrom like '%{0}%')", txtFinishSchool.Text.Trim());
        }
        if (txtThisYearSalary.Text.Trim() != "")
        {
            strWhere += string.Format(" and (a.ThisYearSalary like '%{0}%') ", txtThisYearSalary.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtContractBegin.Text) && !string.IsNullOrEmpty(txtContractEnd.Text))
        {
            strWhere += string.Format(" and (a.ContractEndDate between '{0}' and '{1}')", txtContractBegin.Text, txtContractEnd.Text);
        }

        if (!string.IsNullOrEmpty(txtBirthdayBegin.Text) && !string.IsNullOrEmpty(txtBirthdayEnd.Text))
        {
            strWhere += string.Format(" and (a.birthday between '{0}' and '{1}')", txtBirthdayBegin.Text, txtBirthdayEnd.Text);
        }

        if (!string.IsNullOrEmpty(txtProbationBegin.Text) && !string.IsNullOrEmpty(txtProbationEnd.Text))
        {
            string dateBegin = DateTime.Parse(txtProbationBegin.Text).AddMonths(-6).AddDays(1).ToString("yyyy-MM-dd");
            string dateEnd = DateTime.Parse(txtProbationEnd.Text).AddMonths(-6).AddDays(1).ToString("yyyy-MM-dd");

            strWhere += string.Format(" and (a.joinDate between '{0}' and '{1}') ", dateBegin, dateEnd);
        }

        string typevalue = "";

        if (hidtype2.Value != "" && hidtype2.Value != "-1")
        {
            typevalue = hidtype2.Value;
        }
        else if (hidtype1.Value != "" && hidtype1.Value != "-1")
        {
            typevalue = hidtype1.Value;
        }
        else if (hidtype.Value != "" && hidtype.Value != "-1")
        {
            typevalue = hidtype.Value;
        }
        else
        {
        }

        int[] depids = null;
        if (!string.IsNullOrEmpty(typevalue) && ESP.HumanResource.Utilities.StringHelper.IsConvertInt(typevalue))
        {
            IList<ESP.Framework.Entity.DepartmentInfo> dlist;
            int selectedDep = int.Parse(typevalue);
            dlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(selectedDep);
            if (dlist != null && dlist.Count > 0)
            {
                depids = new int[dlist.Count];
                for (int i = 0; i < dlist.Count; i++)
                {
                    depids[i] = dlist[i].DepartmentID;
                }
            }
            else
            {
                depids = new int[] { selectedDep };
            }
        }
        else
        {
            depids = null;
        }

        // List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeesInPositionsManager.GetUserModelList(UserInfo,depids, strWhere);
        List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = null;


        // if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
        if (ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRId().IndexOf(this.UserID.ToString()) >= 0 || ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
        {

            list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelListByDeparmtnetID(depids, strWhere);
        }
        else
        {
            list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(depids, strWhere);
        }

        gvList.DataSource = list;
        gvList.DataBind();

        this.ddlCurrentPage2.Items.Clear();
        for (int i = 1; i <= this.gvList.PageCount; i++)
        {
            this.ddlCurrentPage2.Items.Add(i.ToString());
        }
        if (this.gvList.PageCount > 0)
        {
            this.ddlCurrentPage2.SelectedIndex = this.gvList.PageIndex;
        }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int id = int.Parse(e.CommandArgument.ToString());
            ListBind();
        }
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = string.Format("<a href='/Management/UserManagement/EmpMgt.aspx?backurl=/HR/Employees/EmployeesAllList.aspx&userid={0}'>{1}</a>", gvList.DataKeys[e.Row.RowIndex].Values[0].ToString(), e.Row.Cells[1].Text);
            e.Row.Cells[5].Text = string.Format("<a href='/Management/UserManagement/EmpMgt.aspx?backurl=/HR/Employees/EmployeesAllList.aspx&userid={0}'><img src='../../images/edit.gif' border='0px;'></a>", gvList.DataKeys[e.Row.RowIndex].Values[0].ToString());
            Repeater rep = (Repeater)e.Row.FindControl("repJob");
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = (List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>)gvList.DataKeys[e.Row.RowIndex].Values[2];
            if (list != null && list.Count > 0)
            {
                rep.DataSource = list;
                rep.DataBind();
            }
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

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gvList.PageIndex = this.ddlCurrentPage2.SelectedIndex;
        ListBind();
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

    [AjaxPro.AjaxMethod]
    public static List<List<string>> getalist(int parentId)
    {
        List<List<string>> list = new List<List<string>>();
        try
        {
            list = ESP.Compatible.DepartmentManager.GetListForAJAX(parentId);
        }
        catch (Exception e)
        {
            e.ToString();
        }

        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);

        return list;
    }

    private void DepartmentDataBind()
    {
        string hradmin = "";
        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = new List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>();
        if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
        {
            object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
            ddltype.DataSource = dt;
            ddltype.DataTextField = "NodeName";
            ddltype.DataValueField = "UniqID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype1.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype2.Items.Insert(0, new ListItem("请选择...", "-1"));
        }
        if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("004", this.ModuleInfo.ModuleID, this.UserID))
        {
            empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            ddltype.Items.Insert(0, new ListItem(empinposlist[0].CompanyName, empinposlist[0].CompanyID.ToString()));
            hidtype.Value = empinposlist[0].CompanyID.ToString();
        }
        if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("002", this.ModuleInfo.ModuleID, this.UserID))
        {
            hradmin = "培恩公关";
            empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            if (empinposlist != null && empinposlist.Count > 0)
            {
                IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[0].CompanyID);
                int depid = IsStringInArray(hradmin, deplist);
                if (depid > 0)
                {
                    ddltype1.Items.Insert(0, new ListItem(hradmin, depid.ToString()));
                    hidtype1.Value = depid.ToString();
                    ddltype1.Items.Insert(0, new ListItem("DPH", "250"));
                }
            }
        }
        else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("003", this.ModuleInfo.ModuleID, this.UserID))
        {
            hradmin = "TCG";
            empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            if (empinposlist != null && empinposlist.Count > 0)
            {
                IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[0].CompanyID);
                int depid = IsStringInArray(hradmin, deplist);
                if (depid > 0)
                {
                    ddltype1.Items.Insert(0, new ListItem(hradmin, depid.ToString()));
                    hidtype1.Value = depid.ToString();
                }
            }


        }
        else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("005", this.ModuleInfo.ModuleID, this.UserID))
        {
            hradmin = "国际公关";
            empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            if (empinposlist != null && empinposlist.Count > 0)
            {
                IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[0].CompanyID);
                int depid = IsStringInArray(hradmin, deplist);
                if (depid > 0)
                {
                    ddltype1.Items.Insert(0, new ListItem(hradmin, depid.ToString()));
                    hidtype1.Value = depid.ToString();
                    ddltype1.Items.Insert(1, new ListItem("DPH", "251"));
                }
            }
        }
        else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("006", this.ModuleInfo.ModuleID, this.UserID))
        {
            hradmin = "集团";
            empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            if (empinposlist != null && empinposlist.Count > 0)
            {
                IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[0].CompanyID);
                int depid = IsStringInArray(hradmin, deplist);
                if (depid > 0)
                {
                    ddltype1.Items.Insert(0, new ListItem(hradmin, depid.ToString()));
                    hidtype1.Value = depid.ToString();
                }
            }
        }
    }

    private static int IsStringInArray(string s, IList<ESP.Framework.Entity.DepartmentInfo> array)
    {
        if (array == null || s == null)
            return 0;

        for (int i = 0; i < array.Count; i++)
        {
            if (string.Compare(s, array[i].DepartmentName, true) == 0)
            {
                return array[i].DepartmentID;
            }
        }
        return 0;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        string strWhere = " and (a.status = 1 or a.status = 3) ";
        if (txtITCode.Text.Trim() != "")
        {
            strWhere += string.Format(" and (a.code like '%{0}%')", txtITCode.Text.Trim());
        }
        if (txtuserName.Text.Trim() != "")
        {
            strWhere += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%'  or b.username like '%{0}%' )", txtuserName.Text.Trim());
        }
        if (txtContractCompany.Text.Trim() != "")
        {
            strWhere += string.Format(" and (c.ContractCompany like '%{0}%') ", txtContractCompany.Text.Trim());
        }
        if (txtFinishSchool.Text.Trim() != "")
        {
            strWhere += string.Format(" and (a.GraduatedFrom like '%{0}%')", txtFinishSchool.Text.Trim());
        }
        if (txtThisYearSalary.Text.Trim() != "")
        {
            strWhere += string.Format(" and (a.ThisYearSalary like '%{0}%') ", txtThisYearSalary.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtContractBegin.Text) && !string.IsNullOrEmpty(txtContractEnd.Text))
        {
            strWhere += string.Format(" and (a.ContractEndDate between '{0}' and '{1}')", txtContractBegin.Text, txtContractEnd.Text);
        }

        if (!string.IsNullOrEmpty(txtProbationBegin.Text) && !string.IsNullOrEmpty(txtProbationEnd.Text))
        {
            string dateBegin = DateTime.Parse(txtProbationBegin.Text).AddMonths(-6).AddDays(1).ToString("yyyy-MM-dd");
            string dateEnd = DateTime.Parse(txtProbationEnd.Text).AddMonths(-6).AddDays(1).ToString("yyyy-MM-dd");

            strWhere += string.Format(" and (a.joinDate between '{0}' and '{1}') ", dateBegin, dateEnd);
        }

        string typevalue = "";

        if (hidtype2.Value != "" && hidtype2.Value != "-1")
        {
            typevalue = hidtype2.Value;
        }
        else if (hidtype1.Value != "" && hidtype1.Value != "-1")
        {
            typevalue = hidtype1.Value;
        }
        else if (hidtype.Value != "" && hidtype.Value != "-1")
        {
            typevalue = hidtype.Value;
        }
        else
        {
        }

        int[] depids = null;
        if (!string.IsNullOrEmpty(typevalue) && ESP.HumanResource.Utilities.StringHelper.IsConvertInt(typevalue))
        {
            IList<ESP.Framework.Entity.DepartmentInfo> dlist;
            int selectedDep = int.Parse(typevalue);
            dlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(selectedDep);
            if (dlist != null && dlist.Count > 0)
            {
                depids = new int[dlist.Count];
                for (int i = 0; i < dlist.Count; i++)
                {
                    depids[i] = dlist[i].DepartmentID;
                }
            }
            else
            {
                depids = new int[] { selectedDep };
            }
        }
        else
        {
            depids = null;
        }

        string content = strWhere;

        ExportExcel(content, depids);

    }

    private void ExportExcel(string content, int[] depids)
    {
        Export(content, depids);
    }

    private void Export(string content, int[] depids)
    {
        try
        {

            DataSet ds = GetData(content, depids);

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

    private DataSet GetData(string content, int[] depids)
    {
        string strWhere = content;
        return ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModel(depids, strWhere);
    }

    private void ExportStatistics(DataSet ds, string mapPath, string tmpFileName)
    {
        string filename = mapPath + "ExcelTemplate\\" + "Statistics.xls";
        Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
        Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
        Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
        Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

        int startRowIndex = 4;//起始行索引
        int rowIndex = 0; //顺序行索引            
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sheet.Cells[startRowIndex + rowIndex, 1] = (rowIndex + 1).ToString();

            string workCity = string.Empty;

            if (dr["WorkCity"] != DBNull.Value)
            {
                if (dr["WorkCity"].ToString().Trim() == "19")
                    workCity = "北京";
                else if (dr["WorkCity"].ToString().Trim() == "17")
                    workCity = "上海";
                else if (dr["WorkCity"].ToString().Trim() == "18")
                    workCity = "广州";
                else
                    workCity = "北京";

                sheet.Cells[startRowIndex + rowIndex, 2] = workCity;//属地 workcity
            }
            sheet.Cells[startRowIndex + rowIndex, 3] = dr["Code"].ToString();
            sheet.Cells[startRowIndex + rowIndex, 4] = dr["LastNameCN"].ToString() + dr["FirstNameCN"].ToString();
            sheet.Cells[startRowIndex + rowIndex, 5] = dr["FirstNameEN"].ToString() + " " + dr["LastNameEN"].ToString();

            sheet.Cells[startRowIndex + rowIndex, 6] = ESP.HumanResource.Common.Status.Gender_Names[int.Parse(dr["Gender"].ToString())];
            //7 工资所属 签合同选择的公司
            ESP.HumanResource.Entity.EmployeeWelfareInfo welfare = ESP.HumanResource.BusinessLogic.EmployeeWelfareManager.getModelBySysId(int.Parse(dr["userid"].ToString()));
            if (welfare != null)
            {
                sheet.Cells[startRowIndex + rowIndex, 7] = welfare.socialInsuranceCompany;
            }
            //8 社保所在公司
            sheet.Cells[startRowIndex + rowIndex, 8] = dr["BranchCode"].ToString();
           

            //9 在职/离职
            sheet.Cells[startRowIndex + rowIndex, 9] = "Active在职";

            sheet.Cells[startRowIndex + rowIndex, 10] = dr["DepartmentName"].ToString();
            sheet.Cells[startRowIndex + rowIndex, 11] = dr["GroupName"].ToString();
            sheet.Cells[startRowIndex + rowIndex, 12] = dr["joinJob"].ToString(); 

            //13 工作地点
            sheet.Cells[startRowIndex + rowIndex, 13] = workCity;
            DateTime joinDate = new DateTime(1900,1,1) ;
            DateTime workBegin = new DateTime(1900, 1, 1);
            decimal workTime = 0;
            if (dr["joinDate"] != DBNull.Value && !string.IsNullOrEmpty(dr["joinDate"].ToString()))
            {
                joinDate = DateTime.Parse(dr["joinDate"].ToString());

                sheet.Cells[startRowIndex + rowIndex, 14] = joinDate.ToString("yyyy-MM-dd");
                TimeSpan ts =(DateTime.Now - joinDate);
                decimal ts2 = (decimal)ts.Days / (decimal)365;
                //15 服务星言云汇年限
                sheet.Cells[startRowIndex + rowIndex, 15] = Math.Round(ts2,1);
                workTime += Math.Round(ts2, 1);
            }
            //16 工作年限
            if (dr["workbegin"] != DBNull.Value && !string.IsNullOrEmpty(dr["workbegin"].ToString()))
            {
                workBegin = DateTime.Parse(dr["workbegin"].ToString());

                TimeSpan tswork = (joinDate - workBegin);
                decimal tswork2 = (decimal)tswork.Days / (decimal)365;

                sheet.Cells[startRowIndex + rowIndex, 16] = Math.Round(tswork2, 1);
                workTime += Math.Round(tswork2, 1);
            }

            //17 时时工作年限
            sheet.Cells[startRowIndex + rowIndex, 17] = workTime;

            //18 合同期限
            sheet.Cells[startRowIndex + rowIndex, 18] = dr["ContractYear"].ToString();
            //19 合同起始日期(YY/MM/DD)
            if (dr["ContractBeginDate"] != DBNull.Value && dr["ContractBeginDate"].ToString() != "")
            {
                sheet.Cells[startRowIndex + rowIndex, 19] = DateTime.Parse(dr["ContractBeginDate"].ToString()).ToString("yyyy-MM-dd");
            }
            //20 合同结束日期(YY/MM/DD)
            if (dr["ContractEndDate"] != DBNull.Value && dr["ContractEndDate"].ToString() != "")
            {
                sheet.Cells[startRowIndex + rowIndex, 20] = DateTime.Parse(dr["ContractEndDate"].ToString()).ToString("yyyy-MM-dd");
            }
            //21 户口所在地
            sheet.Cells[startRowIndex + rowIndex, 21] = dr["DomicilePlace"].ToString();
            //22 户口性质
            sheet.Cells[startRowIndex + rowIndex, 22] = dr["Residence"].ToString();
            //23 社保类别

            //24 婚姻状况
            if (dr["MaritalStatus"] != DBNull.Value && dr["MaritalStatus"].ToString() != "")
            {
                int marital = int.Parse(dr["MaritalStatus"].ToString());
                if (marital == 1)
                    sheet.Cells[startRowIndex + rowIndex, 24] = "已婚";
                else if (marital == 2)
                    sheet.Cells[startRowIndex + rowIndex, 24] = "未婚";
                else if (marital == 3)
                    sheet.Cells[startRowIndex + rowIndex, 24] = "已婚有子";
                else if (marital == 4)
                    sheet.Cells[startRowIndex + rowIndex, 24] = "离异";
                else
                    sheet.Cells[startRowIndex + rowIndex, 24] = "";
            }
            //25 身份证号码
            sheet.Cells[startRowIndex + rowIndex, 25] = "'" + dr["IDNumber"].ToString();

            //26 Date of Birth
            //27 年龄
            if (dr["Birthday"] != DBNull.Value && dr["Birthday"].ToString() != "")
            {
                sheet.Cells[startRowIndex + rowIndex, 26] = DateTime.Parse(dr["Birthday"].ToString()).ToString("yyyy-MM-dd");
                sheet.Cells[startRowIndex + rowIndex, 27] = (DateTime.Now.Year - DateTime.Parse(dr["Birthday"].ToString()).Year).ToString();
            }
            //28 教育程度（中文）
            sheet.Cells[startRowIndex + rowIndex, 28] = dr["Education"].ToString();
            //29 毕业院校
            sheet.Cells[startRowIndex + rowIndex, 29] = dr["GraduatedFrom"].ToString();
            //30 专业
            sheet.Cells[startRowIndex + rowIndex, 30] = dr["Major"].ToString();
            //31 入职职位
            ESP.HumanResource.Entity.EmployeeJobInfo jobInfo = ESP.HumanResource.BusinessLogic.EmployeeJobManager.GetModel(int.Parse(dr["userid"].ToString()));
            if (jobInfo != null)
                sheet.Cells[startRowIndex + rowIndex, 31] = jobInfo.joinJob;
            //32 手机
            sheet.Cells[startRowIndex + rowIndex, 32] = "'" + dr["MobilePhone"].ToString();
            //33 民族
            sheet.Cells[startRowIndex + rowIndex, 33] = dr["Nation"].ToString();
            //34 通讯地址
            sheet.Cells[startRowIndex + rowIndex, 34] = dr["Address"].ToString();
            //35 升（降）职1
            //36 升（降）职2
            //37 升（降）职3
            //38 升（降）职4
            //39 升（降）职5
            //40 null
            //41 null
            //42 null



            if (dr["LastDay"] != DBNull.Value && !string.IsNullOrEmpty(dr["LastDay"].ToString()))
            {
                sheet.Cells[startRowIndex + rowIndex, 43] = DateTime.Parse(dr["LastDay"].ToString()).ToString("yyyy-MM-dd");
            }

            //44 离职原因	
            //45 离职注意事项	
            //46 生日月份	
            //47 应转正日期	
            //48 实际转正生效日期

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
