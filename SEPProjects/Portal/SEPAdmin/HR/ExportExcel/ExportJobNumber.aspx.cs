using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Common;
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

namespace SEPAdmin.HR.ExportExcel
{
    public partial class ExportJobNumber : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Export();

        }

        private void Export()
        {           
                
                try
                {                 

                    DataSet ds = null;
                    //if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
                    //{
                        ds = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetPositionList();
                        ExportStatistics(ds, Server.MapPath("../"));
                    //}
                    
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString(),"HR",ESP.Logging.LogLevel.Error);
                }
                Response.End();
       

        }

        private string ExportStatistics(DataSet ds, string mapPath)
        {
            string filename = mapPath + "ExcelTemplate\\" + "JobNumber.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
                        
            int startRowIndex = 3;//起始行索引
            int rowIndex = 0; //顺序行索引            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sheet.Cells[startRowIndex + rowIndex, 1] = dr["departmentpositionname"].ToString();
                int[] level2id = new int[] { 28,42,48,107};//仅限总部
                int[] level1id = new int[] { 17,18};//没有总部
                int colindex = 2;
                for (int i = 0; i < level2id.Length; i++)
                {
                    int jituan = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetJobNumberByLevel2Id(level2id[i]);
                    int jituanzhiwei = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetJobNumberBylv2ordp(level2id[i], dr["departmentpositionname"].ToString());
                    double perji = (jituanzhiwei * 1.00) / (jituan * 1.00);
                    sheet.Cells[startRowIndex + rowIndex, colindex] = jituanzhiwei.ToString();
                    sheet.Cells[startRowIndex + rowIndex, ++colindex] = (perji * 100).ToString();
                    colindex++;
                }
                for (int j = 0; j < level1id.Length; j++)
                {
                    int gongsi = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetJobNumberByLevel1Id(level1id[j]);
                    int gongsizhiwei = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetJobNumberBylv1ordp(level1id[j], dr["departmentpositionname"].ToString());
                    double pergs = (gongsizhiwei * 1.00) / (gongsi * 1.00);
                    sheet.Cells[startRowIndex + rowIndex, colindex] = gongsizhiwei.ToString();
                    sheet.Cells[startRowIndex + rowIndex, ++colindex] = (pergs * 100).ToString();
                    colindex++;
                }
                    rowIndex++;
            }

            workbook.Saved = true;

            string tmpFileName = "JobNumber" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
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
            FileInfo finfo = new FileInfo(pathandname);
            finfo.Delete();
        }
    }
}
