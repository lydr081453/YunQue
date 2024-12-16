using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ESP.HumanResource.Entity;
using Microsoft.Office.Interop.Excel;

namespace SEPAdmin.HR.ExportExcel
{
    public partial class AddressBookExcel : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["id"] != "" && this.Request.QueryString["id"] != null)
            {
                try
                {
                    Export(Convert.ToInt32(this.Request.QueryString["id"]));
                    this.Response.End();
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString(), "HR", ESP.Logging.LogLevel.Error);
                    ShowCompleteMessage("导出失败！", "/HR/Employees/AddressBookHistoryList.aspx");
                }
            }
            else
            {
                ShowCompleteMessage("导出失败！", "/HR/Employees/AddressBookHistoryList.aspx");
            }
        }
        private string Export(int id)
        {
            string mapPath = Server.MapPath("../");
            string filename = mapPath + "ExcelTemplate\\" + "AddressBookTemplate.xls";
            IList<ESP.Framework.Entity.DepartmentInfo> deps = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(0);
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Sheets sheets = workbook.Sheets;

            for (int i = 1; i <= sheets.Count; i++)
            {
                Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[i];
                foreach (var dep in deps)
                {
                    if (dep.DepartmentName.Contains(sheet.Name))
                    {
                        IList<AddressItemInfo> items = ESP.HumanResource.BusinessLogic.AddressBookManager.GetData(id, dep.DepartmentID);
                        int num = 1;
                        string depName = "";
                        int rowIndex = 8;
                        foreach (var item in items)
                        {
                            if (depName == item.DepartmentName)
                            {
                                sheet.Cells[rowIndex, 2] = "";
                                num++;
                            }
                            else
                            {
                                sheet.Cells[rowIndex, 2] = item.DepartmentName;
                                depName = item.DepartmentName;
                                num = 1;
                            }

                            sheet.Cells[rowIndex, 3] = num;
                            Range range = (Range)sheet.Cells[rowIndex, 3];
                            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            sheet.Cells[rowIndex, 4] = item.UserName;
                            sheet.Cells[rowIndex, 5] = item.ENName;
                            sheet.Cells[rowIndex, 6] = item.Position;
                            sheet.Cells[rowIndex, 7] = item.Mobile;
                            sheet.Cells[rowIndex, 8] = item.Tel;
                            sheet.Cells[rowIndex, 9] = item.Email;
                            rowIndex++;
                        }
                    }
                }
            }


            string tmpFileName = "AddressBook" + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Ticks.ToString("###") + ".xls";
            try
            {
                workbook.Saved = true;
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();
                GC.Collect();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                workbook = null;
                app = null;

                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName);

            }
            catch (Exception ex)
            {
                workbook.Saved = false;
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
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + Server.UrlEncode(filename));
            Response.AddHeader("Connection", "Close");
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Length", fin.Length.ToString());
            Response.Charset = "gb2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

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
