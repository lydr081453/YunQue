using ESP.Finance.BusinessLogic;
using ESP.Finance.DataAccess;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinanceWeb.Tools
{
    public partial class ProjectExpendReport : System.Web.UI.Page
    {
        string projectIdSql = "";
        string returnIdSql = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string temppath = "/Tmp/Project/ProjectExpendTemplate.xlsx";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[1];
            excel.CurSheet.Activate();

            DataTable projectList = ProjectListBind();
            for (int i = 0; i < projectList.Rows.Count; i++)
            {
                DataRow dr = projectList.Rows[i];
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (i + 2).ToString(), dr["projectcode"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (i + 2).ToString(), dr["applicantemployeename"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (i + 2).ToString(), dr["businessdescription"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (i + 2).ToString(), dr["department"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (i + 2).ToString(), dr["BeginDate"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (i + 2).ToString(), dr["EndDate"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "G" + (i + 2).ToString(), dr["totalamount"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "H" + (i + 2).ToString(), dr["supporter"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "I" + (i + 2).ToString(), dr["cost"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "J" + (i + 2).ToString(), dr["oop"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "K" + (i + 2).ToString(), dr["usedCB"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "L" + (i + 2).ToString(), dr["usedBX"].ToString());
            }

            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[2];
            excel.CurSheet.Activate();
            DataTable prList = PrListBind();
            for (int i = 0; i < prList.Rows.Count; i++)
            {
                DataRow dr = prList.Rows[i];
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (i + 2).ToString(), dr["returncode"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (i + 2).ToString(), dr["expectpaymentprice"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (i + 2).ToString(), dr["totalprice"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (i + 2).ToString(), dr["returnstatus"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (i + 2).ToString(), dr["returnfactdate"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (i + 2).ToString(), dr["status"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "G" + (i + 2).ToString(), dr["id"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "H" + (i + 2).ToString(), dr["prno"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "I" + (i + 2).ToString(), dr["requestorname"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "J" + (i + 2).ToString(), dr["app_date"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "K" + (i + 2).ToString(), dr["dept"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "L" + (i + 2).ToString(), dr["project_code"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "M" + (i + 2).ToString(), dr["supplier_name"].ToString());
            }

            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[3];
            excel.CurSheet.Activate();
            DataTable returnList = ReturnListBind();
            for (int i = 0; i < returnList.Rows.Count; i++)
            {
                DataRow dr = returnList.Rows[i];
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (i + 2).ToString(), dr["returncode"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (i + 2).ToString(), dr["projectcode"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (i + 2).ToString(), dr["prefee"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (i + 2).ToString(), dr["requestemployeename"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (i + 2).ToString(), dr["dept"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (i + 2).ToString(), dr["returnstatus"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "G" + (i + 2).ToString(), dr["returncode2"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "H" + (i + 2).ToString(), dr["prefee2"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "I" + (i + 2).ToString(), dr["returnstatus2"].ToString());
            }

            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[4];
            excel.CurSheet.Activate();
            DataTable edList = ExpenseDetailListBind();
            for (int i = 0; i < edList.Rows.Count; i++)
            {
                DataRow dr = edList.Rows[i];
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (i + 2).ToString(), dr["returncode"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (i + 2).ToString(), dr["projectcode"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (i + 2).ToString(), dr["prefee"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (i + 2).ToString(), dr["requestemployeename"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (i + 2).ToString(), dr["dept"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (i + 2).ToString(), dr["ExpenseDesc"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "G" + (i + 2).ToString(), dr["ExpenseDate"].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "H" + (i + 2).ToString(), dr["ExpenseMoney"].ToString());
            }

            string serverpath = Common.GetLocalPath("/Tmp/Project");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xlsx";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Project/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, this.Response);
            GC.Collect();
            Response.Redirect("projectExpendReport.aspx");
        }

        /// <summary>
        /// 项目信息
        /// </summary>
        private DataTable ProjectListBind()
        {
            string begin = txtBegin.Text.Trim();
            string end = txtEnd.Text.Trim();

            projectIdSql = "select ProjectId from f_project where (EndDate between '"+begin+"' and '"+end+"')and ProjectCode<>''";

            string sql = @"	select projectcode,applicantemployeename,businessdescription,
			    d.level1+'-'+d.level2+'-'+d.level3 as department,
			    f_project.BeginDate,EndDate,
			    totalamount,
			    (select sum(budgetallocation) from f_supporter where projectid=f_project.projectid) 'supporter',
			    (select sum(cost) from f_contractcost where projectid=f_project.projectid) 'cost',
			    (select sum(expense) from f_projectexpense where projectid=f_project.projectid) 'oop',

			    (select sum(totalprice) from t_generalinfo a where a.project_code=f_project.projectcode and departmentid=f_project.groupid and a.prtype not in(1,6,4,8) and a.status not in(-1,0,2,4)) as usedCB,
			    (select sum(prefee) from f_return where projectid=f_project.projectid and returnstatus not in(-1,0,1) and returntype in(30,31,20,32,33,35,37,40) and departmentid=f_project.groupid) as usedBX

			    from f_project 
			    join sep_employeesinpositions b on f_project.applicantuserid =b.userid
			    join v_department d on f_project.groupid =d.level3id
			    where ProjectId in ("+projectIdSql+");";
            return DbHelperSQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 第三方采购信息
        /// </summary>
        private DataTable PrListBind()
        {
            string sql = @"	select * from (
		        select distinct '' returncode ,p.expectpaymentprice,a.totalprice,'' returnstatus,'' returnfactdate,
		        a.status,a.id,a.prno,a.requestorname,a.app_date,d.level1+'-'+d.level2+'-'+d.level3 dept,
		        project_code,a.supplier_name
		        from t_generalinfo a left join t_paymentperiod p on a.id=p.gid
		        left join v_Department d on a.DepartmentId =d.level3id
		        where  
		        p.status=0
		        and a.status in(1,3,5,6,7,8,9,10,12,13,14,15,19) and
		        a.ProjectID in ("+projectIdSql+@")

		        union

		        select b.returncode,b.prefee,a.totalprice,b.returnstatus,b.returnfactdate,
		        a.status,a.id,a.prno,a.requestorname,a.app_date,d.level1+'-'+d.level2+'-'+d.level3 dept,
		        project_code,a.supplier_name
		        from t_generalinfo a 
		        left join f_return b on a.id=b.prid
		        left join v_Department d on a.DepartmentId =d.level3id
		        where a.status not in(0,-1) and b.returntype not in(34,11) and
		        a.ProjectID in (" + projectIdSql + @")
		        ) abc order by project_code, prNo";
            return DbHelperSQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 报销信息
        /// </summary>
        private DataTable ReturnListBind()
        {
            returnIdSql = "select returnId from f_return where  a.returnstatus not in(0,1,-1) and a.returntype in(20,30,31,32,33,35,37,40) and  a.ProjectID in (" + projectIdSql + ")";

            string sql = @"	select distinct a.returncode,a.projectcode,a.prefee,a.requestemployeename,dept.level1+'-'+dept.level2+'-'+dept.level3 as dept,a.returnstatus,
		        c.returncode returncode2,c.prefee prefee2,c.returnstatus as returnstatus2
		        from f_return a

		        left join f_return c on a.returnid=c.parentid
		        left join V_Department dept on a.DepartmentId =dept.level3Id
		        where a.returnId in (" + returnIdSql + @")
		        order by a.ProjectCode;";
            return DbHelperSQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 报销明细信息
        /// </summary>
        private DataTable ExpenseDetailListBind()
        {
            string sql = @"	select  a.returncode,a.projectcode,a.prefee,a.requestemployeename,dept.level1+'-'+dept.level2+'-'+dept.level3 as dept,
		        b.ExpenseDesc,b.ExpenseDate,b.ExpenseMoney
		        from f_return a join F_ExpenseAccountDetail b on a.ReturnID =b.ReturnID 
		        left join f_return c on a.returnid=c.parentid
		        left join v_Department dept on a.DepartmentId =dept.level3Id
		        where a.returnstatus not in(0,1,-1) and a.returntype in(20,30,31,32,33,35,37,40) and 
		        a.returnId in (" + returnIdSql + @")
		        and b.TicketStatus<>1
		        order by a.ProjectCode, a.Returnid;";

            return DbHelperSQL.Query(sql).Tables[0];
        }
    }
}