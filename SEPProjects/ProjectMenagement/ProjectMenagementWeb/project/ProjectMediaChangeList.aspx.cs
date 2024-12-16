using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Finance.Utility;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.OleDb;

namespace FinanceWeb.project
{
    public partial class ProjectMediaChangeList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
                BindGrid();
            }
        }

        #region 初始化检索信息
        protected void BindInfo()
        {
            BindProjectType(ddlProjectType, "parentid is null");
            ddlTypeLevel2.Items.Insert(0, new ListItem("请选择", "0"));
            ddlTypeLevel3.Items.Insert(0, new ListItem("请选择", "0"));
            BindDepartment(ddlDepartment1, 0);
            ddlDepartment2.Items.Insert(0, new ListItem("请选择", "-1"));
            ddlDepartment3.Items.Insert(0, new ListItem("请选择", "-1"));

            IList<ESP.Finance.Entity.ContractStatusInfo> plist = ESP.Finance.BusinessLogic.ContractStatusManager.GetList(null, null);
            ddlContactStatus.DataSource = plist;
            ddlContactStatus.DataTextField = "ContractStatusName";
            ddlContactStatus.DataValueField = "ContractStatusID";
            ddlContactStatus.DataBind();
            ddlContactStatus.Items.Insert(0, new ListItem("请选择", "0"));

            var branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
            ddlBranch.DataSource = branchList;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("请选择", "0"));
        }

        private void BindDepartment(DropDownList ddl, int parentId)
        {
            ddl.DataSource = ESP.Compatible.DepartmentManager.GetByParent(parentId);
            ddl.DataTextField = "NodeName";
            ddl.DataValueField = "UniqID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        private void BindProjectType(DropDownList ddl, string terms)
        {
            IList<ESP.Finance.Entity.ProjectTypeInfo> typeList = typeList = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList(terms);
            ddl.DataSource = typeList;
            ddl.DataTextField = "ProjectTypeName";
            ddl.DataValueField = "ProjectTypeID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("请选择", "0"));
        }

        protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjectType(ddlTypeLevel2, "parentid =" + ddlProjectType.SelectedValue);

            ddlTypeLevel3.Items.Clear();
            ddlTypeLevel3.Items.Insert(0, new ListItem("请选择", "0"));

        }

        protected void ddlTypeLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTypeLevel3.Items.Clear();
            BindProjectType(ddlTypeLevel3, "parentid =" + ddlTypeLevel2.SelectedValue);
        }

        protected void ddlDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartment(ddlDepartment2, int.Parse(ddlDepartment1.SelectedValue));
            ddlDepartment3.Items.Clear();
            ddlDepartment3.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        protected void ddlDepartment2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDepartment3.Items.Clear();
            BindDepartment(ddlDepartment3, int.Parse(ddlDepartment2.SelectedValue));
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            this.gvG.DataSource = GetList();
            this.gvG.DataBind();
        }

        private IList<ProjectInfo> GetList()
        {
            string term = "";
            List<SqlParameter> paramlist = new List<SqlParameter>();

            if (ddlDepartment3.SelectedValue != "-1")
            {
                term += " and GroupId=@GroupId";
                paramlist.Add(new SqlParameter("@GroupId", ddlDepartment3.SelectedValue));
            }
            else
            {
                if (ddlDepartment2.SelectedValue != "-1")
                {
                    term += @" and GroupId in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                where b.DepartmentID=@GroupID)";
                    paramlist.Add(new SqlParameter("@GroupId", ddlDepartment2.SelectedValue));
                }
                else
                {
                    if (ddlDepartment1.SelectedValue != "-1")
                    {
                        term += @" and GroupId in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                inner join sep_Departments as a on b.ParentID=a.DepartmentID
                                where a.DepartmentID=@GroupID)";
                        paramlist.Add(new SqlParameter("@GroupId", ddlDepartment1.SelectedValue));
                    }
                }
            }
            if (ddlTypeLevel3.SelectedValue != "0")
            {
                term += " and ProjectTypeLevel3Id=@ProjectTypeId";
                paramlist.Add(new SqlParameter("@ProjectTypeId", ddlTypeLevel3.SelectedValue));
            }
            else
            {
                if (ddlTypeLevel2.SelectedValue != "0")
                {
                    term += " and ProjectTypeLevel2Id=@ProjectTypeId";
                    paramlist.Add(new SqlParameter("@ProjectTypeId", ddlTypeLevel2.SelectedValue));
                }
                else
                {
                    if (ddlProjectType.SelectedValue != "0")
                    {
                        term += " and ProjectTypeId=@ProjectTypeId";
                        paramlist.Add(new SqlParameter("@ProjectTypeId", ddlProjectType.SelectedValue));
                    }
                }
            }
            if (ddlBranch.SelectedValue != "0")
            {
                term += " and BranchId=@BranchId";
                paramlist.Add(new SqlParameter("@BranchId", ddlBranch.SelectedValue));
            }
            if (ddlContactStatus.SelectedValue != "0")
            {
                term += " and ContractStatusId=@ContractStatusId";
                paramlist.Add(new SqlParameter("@ContractStatusId", ddlContactStatus.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtApplicant.Text))
            {
                term += " and ApplicantEmployeeName like '%'+@ApplicantEmployeeName+'%'";
                paramlist.Add(new SqlParameter("@ApplicantEmployeeName", txtApplicant.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCN1.Text))
            {
                term += " and CustomerId in (select id from F_Customer where NameCN1 like '%'+@CN1+'%')";
                paramlist.Add(new SqlParameter("@CN1", txtCN1.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtBizDesc.Text))
            {
                term += " and BusinessDescription like '%'+@BusinessDescription+'%'";
                paramlist.Add(new SqlParameter("@BusinessDescription", txtBizDesc.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtMediaName.Text))
            {
                term += " and MediaId in (select id from T_Supplier where supplier_name like '%'+@supplier_name+'%')";
                paramlist.Add(new SqlParameter("@supplier_name", txtMediaName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtProjectCode.Text))
            {
                term += " and ProjectCode like '%'+@ProjectCode+'%'";
                paramlist.Add(new SqlParameter("@ProjectCode", txtProjectCode.Text.Trim()));
            }
            string projectType = System.Web.Configuration.WebConfigurationManager.AppSettings["ProjectType_Recharge"];
            projectType = projectType.TrimStart(',');
            projectType = projectType.TrimEnd(',');

            term += " and projectCode <>'' and ProjectTypeId in(" + projectType + ")";

            return ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
                Literal lblGroupName = (Literal)e.Row.FindControl("litGroup");
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(projectmodel.GroupID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string temppath = "/Tmp/Project/MediaChangeTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            int cellBegin = 3;
            excel.Load(sourceFile);
            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[1];
            excel.CurSheet.Activate();

            IList<ProjectInfo> list = GetList();
            foreach (var item in list)
            {
                ProjectMediaInfo pm = ProjectMediaManager.GetList(" projectId=" + item.ProjectId + " and endDate is null")[0];
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (cellBegin).ToString(), item.ProjectCode);
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (cellBegin).ToString(), item.BusinessDescription);
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (cellBegin).ToString(), item.Recharge);
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (cellBegin).ToString(), pm.MediaName);
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (cellBegin).ToString(), pm.CostRate);
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (cellBegin).ToString(), pm.Recharge);
                ExcelHandle.WriteCell(excel.CurSheet, "G" + (cellBegin).ToString(), pm.Recharge);
                cellBegin++;
            }

            string serverpath = Common.GetLocalPath("/Tmp/Project");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Project/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, this.Response);
            GC.Collect();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (fil.FileName == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择导入的excel文件！');", true);
                return;
            }
            string fileExtenSion;
            fileExtenSion = Path.GetExtension(fil.FileName);
            if (fileExtenSion.ToLower() != ".xlsx" && fileExtenSion.ToLower() != ".xls")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择xlsx、xls格式的excel！');", true);
                return;
            }
            HttpPostedFile file = this.fil.PostedFile;
            string fileName = file.FileName;
            string tempPath = System.IO.Path.GetTempPath(); //获取系统临时文件路径
            fileName = System.IO.Path.GetFileName(fileName); //获取文件名（不带路径）
            string currFileExtension = System.IO.Path.GetExtension(fileName); //获取文件的扩展名
            string currFilePath = tempPath + fileName; //获取上传后的文件路径 记录到前面声明的全局变量
            file.SaveAs(currFilePath); //上传

            ExcelHandle excel = new ExcelHandle();
            int cellBegin = 3;
            excel.Load(currFilePath);
            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[1];
            excel.CurSheet.Activate();
            while (excel.CurSheet.Range["A" + cellBegin].Value != null && excel.CurSheet.Range["A" + cellBegin].Value.ToString().Trim() != "")
            {
                string msg = "";
                string projectCode, oldMediaName, newMediaName, begin;
                try
                {
                    try
                    {
                        projectCode = excel.CurSheet.Range["A" + cellBegin].Value.ToString();//项目号
                        oldMediaName = excel.CurSheet.Range["D" + cellBegin].Value.ToString(); //原媒体主体名称
                        newMediaName = excel.CurSheet.Range["H" + cellBegin].Value.ToString(); //新媒体主体名称
                        begin = excel.CurSheet.Range["K" + cellBegin].Value.ToString(); //起始时间
                    }
                    catch
                    {
                        throw new Exception("数据不全");
                    }
                    ProjectInfo project = ProjectManager.GetModelByProjectCode(projectCode);
                    if (project == null)
                        throw new Exception("项目号错误");
                    ESP.Purchase.Entity.SupplierInfo supplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(oldMediaName, -1);
                    if (supplier == null)
                        throw new Exception("原媒体主体名称错误");
                    decimal oldRecharge = 0m; //原充值金额
                    if (!decimal.TryParse(excel.CurSheet.Range["F" + cellBegin].Value.ToString(), out oldRecharge))
                        throw new Exception("原充值金额错误");
                    IList<ProjectMediaInfo> projectMediaList = ProjectMediaManager.GetList(" projectId=" + project.ProjectId + " and SupplierId=" + supplier.id + " and recharge=" + oldRecharge + " and endDate is null");
                    if (projectMediaList.Count != 1)
                        throw new Exception("原媒体付款主体信息与项目可拆分主体信息不符");
                    decimal oldChangeRecharge = 0m; //拆分后原主体充值金额
                    if (!decimal.TryParse(excel.CurSheet.Range["G" + cellBegin].Value.ToString(), out oldChangeRecharge))
                        throw new Exception("拆分后原主体充值金额错误");
                    ESP.Purchase.Entity.SupplierInfo newSupplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(newMediaName, -1);
                    if (newSupplier == null)
                        throw new Exception("新媒体主体名称错误");
                    decimal newRecharge = 0m; //新充值金额
                    if (!decimal.TryParse(excel.CurSheet.Range["J" + cellBegin].Value.ToString(), out newRecharge))
                        throw new Exception("新充值金额错误");
                    decimal newCostRate = 0m; //新成本比例
                    if (!decimal.TryParse(excel.CurSheet.Range["I" + cellBegin].Value.ToString(), out newCostRate) || newCostRate > 1)
                        throw new Exception("新预估媒体成本比例错误");
                    DateTime beginDate;
                    if (!DateTime.TryParse(begin, out beginDate))
                        throw new Exception("起始时间错误");

                    ProjectMediaInfo oldPM = projectMediaList[0];
                    if (oldPM.Recharge != (oldChangeRecharge + newRecharge))
                        throw new Exception("拆分后两主体金额和不等于原金额");

                    oldPM.Recharge = oldChangeRecharge;
                    oldPM.EndDate = beginDate.AddDays(-1);

                    ProjectMediaInfo newPM = new ProjectMediaInfo();
                    newPM.BeginDate = beginDate;
                    newPM.SupplierId = newSupplier.id;
                    newPM.ProjectId = project.ProjectId;
                    newPM.CostRate = newCostRate;
                    newPM.Recharge = newRecharge;

                    ProjectMediaManager.UpdateAndAdd(oldPM, newPM);
                    ExcelHandle.WriteCell(excel.CurSheet, "L" + (cellBegin).ToString(), "成功");
                }
                catch (Exception ex)
                {
                    ExcelHandle.WriteCell(excel.CurSheet, "L" + (cellBegin).ToString(), ex.Message);
                    ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + cellBegin.ToString(), "L" + cellBegin.ToString(), System.Drawing.Color.Red);
                }
                cellBegin++;
            }

            string serverpath = Common.GetLocalPath("/Tmp/Project");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = "变更媒体主体导入结果.xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Project/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "hideLoading();", true);
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, this.Response);
            GC.Collect();
        }

    }
}