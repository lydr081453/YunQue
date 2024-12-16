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
using Microsoft.Office.Interop.Excel;
using ESP.Administrative.BusinessLogic;
using ESP.Framework.Entity;
using ESP.Administrative.Entity;

public partial class AddressBookList : ESP.Web.UI.PageBase
{
    private string clientId = string.Empty;
    private string searchType = string.Empty;
    private string deptName = string.Empty;
    protected string userCode = "";
    protected string userName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Employees_EmployeesAllList));
        #endregion

        if (!IsPostBack)
        {
            DepartmentDataBind();
            DrpBataBind();
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                int pageIndex = 0;
                int.TryParse(Request["pageIndex"].Trim(), out pageIndex);
                Paging(pageIndex);
            }
            else
            {
                ListBind();
            }
        }
    }

    private void DrpBataBind()
    {
        DataSet ds = StatisticsTypeManager.GetList(" IsDeleted=0");
        //drpPrint.DataSource = ds;
        //drpPrint.DataTextField = "StatisticsTypeName";
        //drpPrint.DataValueField = "StatisticsTypeValue";
        //drpPrint.DataBind();
        //drpPrint.Items.Insert(0, new ListItem("无条件...", "-1"));
    }

    private void ListBind()
    {
        string strWhere = " and (a.status = 1 or a.status = 3) ";
        if (txtITCode.Text.Trim() != "")
        {
            userCode = txtITCode.Text.Trim();
            strWhere += string.Format(" and (a.code like '%{0}%')", txtITCode.Text.Trim());
        }
        else
        {
            if (this.Request.QueryString["userCode"] != "" && this.Request.QueryString["userCode"] != null)
            {
                txtITCode.Text = this.Request.QueryString["userCode"];
                strWhere += string.Format(" and (a.code like '%{0}%')", this.Request.QueryString["userCode"].Trim());
            }
        }
        if (txtuserName.Text.Trim() != "")
        {
            userName = txtuserName.Text.Trim();
            strWhere += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%'  or b.username like '%{0}%' )", txtuserName.Text.Trim());
        }
        else
        {
            if (this.Request.QueryString["userName"] != "" && this.Request.QueryString["userName"] != null)
            {
                txtuserName.Text = this.Request.QueryString["userName"];
                strWhere += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%'  or b.username like '%{0}%' )", this.Request.QueryString["userName"].Trim());
            }
        }

        List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = new List<EmployeeBaseInfo>();

        string userList = ESP.Configuration.ConfigurationManager.SafeAppSettings["HRDepartmentId"];
        if (userList.IndexOf("," + CurrentUser.SysID + ",") >= 0)
        {
            list = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere);
        }
        else
        {
            //13800	张毅	
            //14336	陈炼	
            int currentuser = this.UserID;
            if (this.UserID == 15129)
                currentuser = 13800;
            list = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere + " and a.UserId in (select userid from ad_operationAuditManage where hradminid=" + currentuser + ")");
        }

        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eips = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + list[i].UserID);
                list[i].EmployeesInPositionsList = eips;
            }

            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                List<ESP.HumanResource.Entity.EmployeeBaseInfo> items = new List<EmployeeBaseInfo>();
                foreach (var item in list)
                {
                    foreach (var p in item.EmployeesInPositionsList)
                    {
                        if (p.GroupID.ToString() == hidtype2.Value)
                        {
                            items.Add(item);
                        }
                    }
                }
                list = items;
            }
            else if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                List<ESP.HumanResource.Entity.EmployeeBaseInfo> items = new List<EmployeeBaseInfo>();
                foreach (var item in list)
                {
                    foreach (var p in item.EmployeesInPositionsList)
                    {
                        if (p.DepartmentID.ToString() == hidtype1.Value)
                        {
                            items.Add(item);
                        }
                    }
                }
                list = items;
            }
            else if (hidtype.Value != "" && hidtype.Value != "-1")
            {
                List<ESP.HumanResource.Entity.EmployeeBaseInfo> items = new List<EmployeeBaseInfo>();
                foreach (var item in list)
                {
                    foreach (var p in item.EmployeesInPositionsList)
                    {
                        if (p.CompanyID.ToString() == hidtype.Value)
                        {
                            items.Add(item);
                        }
                    }
                }
                list = items;
            }
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
            e.Row.Cells[1].Text = string.Format("<a href='/HR/Statistics/EmployeesChangeDetail.aspx?userid={0}'>{1}</a>", gvList.DataKeys[e.Row.RowIndex].Values[0].ToString(), e.Row.Cells[1].Text);
            Repeater rep = (Repeater)e.Row.FindControl("rep");
            Repeater Job = (Repeater)e.Row.FindControl("Job");
            //Repeater dep = (Repeater)e.Row.FindControl("dep");
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> list = (List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>)gvList.DataKeys[e.Row.RowIndex].Values[2];

            if (list.Count > 0 && list != null)
            {
                rep.DataSource = list;
                rep.DataBind();

                Job.DataSource = list;
                Job.DataBind();

                //dep.DataSource = list;
                //dep.DataBind();
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
        Export();
    }

    private void Export()
    {
        try
        {
                DataSet ds = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetAddressBookList(CurrentUserID);
            string mapPath = Server.MapPath("../");
            string tmpFileName = "AddressBook" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
            ExportStatistics(ds, mapPath, tmpFileName);

            outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName);
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(ex.ToString());
            throw;
        }
    }

    private void ExportStatistics(DataSet ds, string mapPath, string tmpFileName)
    {
        string filename = mapPath + "ExcelTemplate\\" + "AddressBookTemplate.xls";
        Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
        Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
        Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
        Microsoft.Office.Interop.Excel.Worksheet sheetBeijing = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];


        int startRowIndex = 8;//起始行索引
        int rowIndex = 0; //顺序行索引

        int userNumber = 1;
        sheetBeijing.Cells[1, 2] =   "星  言  云  汇        " + DateTime.Now.ToString("yyyy-MM-dd");


        int depLevel1Id = 0;
        int depLevel2Id = 0;
        int depLevel3Id = 0;

        int curdepLevel1Id = 0;
        int curdepLevel2Id = 0;
        int curdepLevel3Id = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            curdepLevel1Id = int.Parse(dr["level1id"].ToString());
            curdepLevel2Id = int.Parse(dr["level2id"].ToString());
            curdepLevel3Id = int.Parse(dr["level3id"].ToString());

            depLevel1Id = curdepLevel1Id;

            CreateExcelRow(startRowIndex, ref rowIndex, ref userNumber, ref depLevel2Id,
                    ref depLevel3Id, curdepLevel2Id, curdepLevel3Id, sheetBeijing, dr);

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
            sheetBeijing = null;
            workbook = null;
            app = null;
        }
    }

    private static void CreateExcelRow(int startRowIndex, ref int rowIndex, ref int userNumber, ref int depLevel2Id, ref int depLevel3Id, int curdepLevel2Id, int curdepLevel3Id, Microsoft.Office.Interop.Excel.Worksheet sheetTemp, DataRow dr)
    {
        if (curdepLevel2Id != depLevel2Id)  // 判断二级部门
        {
            depLevel2Id = curdepLevel2Id;
            sheetTemp.Cells[startRowIndex + rowIndex, 2] = dr["Level2"].ToString();
            Microsoft.Office.Interop.Excel.Range r1 = sheetTemp.get_Range(sheetTemp.Cells[startRowIndex + rowIndex, 2], sheetTemp.Cells[startRowIndex + rowIndex, 11]);
            r1.MergeCells = true;
            r1.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
            r1.Font.Bold = true;
            r1.RowHeight = 30;
            rowIndex++;

            Microsoft.Office.Interop.Excel.Range r2 = sheetTemp.get_Range(sheetTemp.Cells[startRowIndex + rowIndex, 2], sheetTemp.Cells[startRowIndex + rowIndex, 11]);
            r2.MergeCells = true;
            r2.Interior.ColorIndex = 49;
            r2.RowHeight = 4.5;
            rowIndex++;

            sheetTemp.Cells[startRowIndex + rowIndex, 2] = "部门";
            sheetTemp.Cells[startRowIndex + rowIndex, 3] = "序号";
            sheetTemp.Cells[startRowIndex + rowIndex, 4] = "姓名";
            sheetTemp.Cells[startRowIndex + rowIndex, 5] = "英文名";
            sheetTemp.Cells[startRowIndex + rowIndex, 6] = "职位";
            sheetTemp.Cells[startRowIndex + rowIndex, 7] = "手机";
            sheetTemp.Cells[startRowIndex + rowIndex, 8] = "分机";
            sheetTemp.Cells[startRowIndex + rowIndex, 9] = "电子邮箱";
            sheetTemp.Cells[startRowIndex + rowIndex, 10] = "员工编号";
            sheetTemp.Cells[startRowIndex + rowIndex, 11] = "工作地点";
            rowIndex++;
        }
        if (curdepLevel3Id != depLevel3Id)  // 判断三级部门
        {
            depLevel3Id = curdepLevel3Id;
            sheetTemp.Cells[startRowIndex + rowIndex, 2] = dr["Level3"].ToString();
            userNumber = 1;
        }

        sheetTemp.Cells[startRowIndex + rowIndex, 3] = userNumber;
        sheetTemp.Cells[startRowIndex + rowIndex, 4] = dr["cnname"].ToString();
        sheetTemp.Cells[startRowIndex + rowIndex, 5] = dr["username"].ToString();
        sheetTemp.Cells[startRowIndex + rowIndex, 6] = dr["DepartmentPositionName"].ToString();
        sheetTemp.Cells[startRowIndex + rowIndex, 7] = dr["MobilePhone"].ToString();
        sheetTemp.Cells[startRowIndex + rowIndex, 8] = dr["Phone1"].ToString();
        sheetTemp.Cells[startRowIndex + rowIndex, 9] = dr["Email"].ToString();
        sheetTemp.Cells[startRowIndex + rowIndex, 10] ="'"+ dr["code"].ToString();
        sheetTemp.Cells[startRowIndex + rowIndex, 11] = dr["workCity"].ToString();
        userNumber++;

        rowIndex++;
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
