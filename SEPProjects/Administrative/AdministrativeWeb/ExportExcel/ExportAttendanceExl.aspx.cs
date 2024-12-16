using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Common;
using System.Data;
using ESP.Administrative.BusinessLogic;
using System.IO;
using System.Collections;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.ExportExcel
{
    public partial class ExportAttendanceExl : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                    //ExportStatistics(ds, Server.MapPath("../"), year, month);
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                }
            }
        }

        private string ExportStatistics(DataSet ds, string mapPath, string year, string month, int userId)
        {
            string filename = mapPath + "ExcelTemplate\\" + "UserMonthStat.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            ESP.Framework.Entity.EmployeeInfo employeeInfo = ESP.Framework.BusinessLogic.EmployeeManager.Get(userId);
            IList<ESP.Framework.Entity.EmployeePositionInfo> empPositionlist = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userId);
            string depstr = "";
            if (empPositionlist != null && empPositionlist.Count > 0)
            {
                foreach (ESP.Framework.Entity.EmployeePositionInfo empPosition in empPositionlist)
                {
                    depstr += empPosition.DepartmentName + ",";
                }
            }
            sheet.Cells[2, 2] = year + "年" + month + "月";
            sheet.Cells[2, 4] = employeeInfo.FullNameCN;
            sheet.Cells[2, 6] = depstr.TrimEnd(',');

            Hashtable clockInTimes = new ClockInManager().GetClockInTimesOfMonth(int.Parse(year), int.Parse(month), this.UserID);
            HashSet<int> holidays = new HolidaysInfoManager().GetHolidayListByMonth(int.Parse(year), int.Parse(month));
            IList<MattersInfo> matters = new MattersManager().GetModelListByMonth(this.UserID, int.Parse(year), int.Parse(month));
            IList<SingleOvertimeInfo> overtimes = new SingleOvertimeManager().GetModelListByMonth(this.UserID, int.Parse(year), int.Parse(month));
            //userBasicModel = userBasicManager.GetModelByUserid(UserID);
            //employeeJobModel = ESP.HumanResource.BusinessLogic.EmployeeJobManager.getModelBySysId(UserID);

            string[] weekName = new string[] { "一", "二", "三", "四", "五", "六", "日" };
            DateTime beginDate = new DateTime(int.Parse(year), int.Parse(month), 1);
            DateTime endDate = new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month)));
            while(beginDate <= endDate)
            {
                sheet.Cells[3 + beginDate.Day, 2] = weekName[(int)beginDate.DayOfWeek];
                object objin = clockInTimes[(long)beginDate.Date.Day];
                object objout = clockInTimes[(long)-beginDate.Date.Day];
                sheet.Cells[3 + beginDate.Day, 3] = objin == null ? "" : ((DateTime)objin).ToString("HH:mm");
                sheet.Cells[3 + beginDate.Day, 4] = objout == null ? "" : ((DateTime)objout).ToString("HH:mm");
                

                beginDate.AddDays(1);
            }

            workbook.Saved = true;

            string tmpFileName = employeeInfo.FullNameCN + year + "年"+ month + "月考勤.xls";
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
