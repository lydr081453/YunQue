using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Common;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using Microsoft.Office.Interop.Excel;
using ESP.Administrative.BusinessLogic;

namespace AdministrativeWeb.ExportExcel
{
    public partial class ExportExcel : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Export();

        }

        private void Export()
        {
            if (!string.IsNullOrEmpty(Request["content"]) && !(null == Request["type"]))
            {
                SymmetricCrypto crypto = new SymmetricCrypto();
                try
                {
                    string strWhere = crypto.DecrypString(Request["content"].ToString());
                    string typevalue = crypto.DecrypString(Request["type"].ToString());
                    int status = 0;
                    switch (typevalue)
                    {
                        case "3": status = Status.MonthStatAppState_WaitStatisticians; break;
                        case "4": status = Status.MonthStatAppState_WaitHRAdmin; break;
                        case "6": status = Status.MonthStatAppState_WaitADAdmin; break;
                    }

                    string[] str = strWhere.Split('|');
                    string year = "";
                    string month = "";
                    if (str.Length == 2)
                    {
                        year = str[0].ToString();
                        month = str[1].ToString();
                    }
                    DataSet ds = new ApproveLogManager().GetApproveList(UserInfo.UserID.ToString(), status, int.Parse(year), int.Parse(month), "", new List<System.Data.SqlClient.SqlParameter>());
                    //int[] depids = null;
                    //if (!string.IsNullOrEmpty(typevalue) && ESP.HumanResource.Utilities.StringHelper.IsConvertInt(typevalue))
                    //{
                    //    IList<ESP.Framework.Entity.DepartmentInfo> dlist;
                    //    int selectedDep = int.Parse(typevalue);
                    //    dlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(selectedDep);
                    //    if (dlist != null && dlist.Count > 0)
                    //    {
                    //        depids = new int[dlist.Count];
                    //        for (int i = 0; i < dlist.Count; i++)
                    //        {
                    //            depids[i] = dlist[i].DepartmentID;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        depids = new int[] { selectedDep };
                    //    }
                    //}
                    //else
                    //{
                    //    depids = null;
                    //}

                    //DataSet ds = null;
                    //if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
                    //{

                    //    ds = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelByDeparmtnetID(depids, strWhere);
                    //}
                    //else
                    //{
                    //    ds = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModel(UserInfo, depids, strWhere);
                    //}
                    ExportStatistics(ds, Server.MapPath("../"), year, month);
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                }
            }
        }

        private string ExportStatistics(DataSet ds, string mapPath, string year, string month)
        {
            string filename = mapPath + "ExcelTemplate\\" + "MonthStat.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            sheet.Cells[1, 1] = "部门月考勤表";
            sheet.Cells[2, 1] = "时间: "+year+" 年"+month+"月";
            sheet.Cells[3, 1] = "序号";
            sheet.Cells[3, 2] = "员工编号";
            sheet.Cells[3, 3] = "姓名";
            sheet.Cells[3, 4] = "部门";
            sheet.Cells[3, 5] = "迟到(次)";
            sheet.Cells[3, 6] = "早退(次)";
            sheet.Cells[3, 7] = "旷工（天）";
            sheet.Cells[3, 8] = "病假";
            sheet.Cells[3, 9] = "年度累计病假";
            sheet.Cells[3, 10] = "事假";
            sheet.Cells[3, 11] = "年休假";
            sheet.Cells[3, 12] = "婚假";
            sheet.Cells[3, 13] = "产假";
            sheet.Cells[3, 14] = "丧假";
            sheet.Cells[3, 15] = "倒休";
            sheet.Cells[3, 16] = "其它";
            int startRowIndex = 4;//起始行索引
            int rowIndex = 0; //顺序行索引            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sheet.Cells[startRowIndex + rowIndex, 1] = rowIndex + 1;
                sheet.Cells[startRowIndex + rowIndex, 2] = dr["usercode"].ToString();
                sheet.Cells[startRowIndex + rowIndex, 3] = dr["ApplicantName"].ToString();
                sheet.Cells[startRowIndex + rowIndex, 4] = dr["level3"].ToString();
                sheet.Cells[startRowIndex + rowIndex, 5] = dr["LateCount"].ToString();
                sheet.Cells[startRowIndex + rowIndex, 6] = dr["LeaveEarlyCount"].ToString();
                sheet.Cells[startRowIndex + rowIndex, 7] = (decimal.Parse(dr["AbsentDays"].ToString()) / Status.WorkingHours).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 8] = decimal.Parse(dr["SickLeaveHours"].ToString()).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 9] = new MonthStatManager().GetUserAllYearSickLeaveDay(int.Parse(dr["ApplicantID"].ToString()),int.Parse(year), Status.AttendanceStatItem.SickLeaveHours);
                sheet.Cells[startRowIndex + rowIndex, 10] = decimal.Parse(dr["AffairLeaveHours"].ToString()).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 11] = (decimal.Parse(dr["AnnualLeaveDays"].ToString()) / Status.WorkingHours).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 12] = (decimal.Parse(dr["MarriageLeaveHours"].ToString()) / Status.WorkingHours).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 13] = (decimal.Parse(dr["MaternityLeaveHours"].ToString()) / Status.WorkingHours).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 14] = (decimal.Parse(dr["FuneralLeaveHours"].ToString()) / Status.WorkingHours).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 15] = (decimal.Parse(dr["OffTuneHours"].ToString())/ Status.WorkingHours).ToString("#.##");
                sheet.Cells[startRowIndex + rowIndex, 16] = dr["Other"].ToString();
                
                rowIndex++;
            }

            workbook.Saved = true;

            string tmpFileName = "MonthStat" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                sheet = null;
                workbook = null;
                app = null;
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }

        private void outExcel(string pathandname, string filename)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            FileStream fin = new FileStream(pathandname, FileMode.Open);
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
            Response.AddHeader("Connection", "Close");
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Length", fin.Length.ToString());

            this.EnableViewState = false;
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
            FileInfo finfo = new FileInfo(pathandname);
            finfo.Delete();
        }
    }
}
