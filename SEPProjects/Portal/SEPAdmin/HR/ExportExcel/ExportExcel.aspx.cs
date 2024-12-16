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
    public partial class ExportExcel : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var content = Request["content"];
            var type = Request["type"];
            var value = Request["value"];
            var filename = Request["filename"];

            if (!string.IsNullOrEmpty(content) && null != type && !string.IsNullOrEmpty(value))
            {
                SymmetricCrypto crypto = new SymmetricCrypto();
                content = crypto.DecrypString(content);
                value = crypto.DecrypString(value);
                Export(content, type, value);
            }
            else if (!string.IsNullOrEmpty(filename))
            {
                SymmetricCrypto crypto = new SymmetricCrypto();
                filename = crypto.DecrypString(filename);
                Export(filename);
            }

        }

        private void Export(string content, string type, string value)
        {
            try
            {
                string key;
                string val;
                DataSet ds = GetData(content, type, value, out key, out val);

                string mapPath = Server.MapPath("../");
                string tmpFileName = "Statistics" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";

                ExportStatistics(ds, mapPath, tmpFileName, key, val);

                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName);

            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
            }
        }

        private DataSet GetData(string content, string type, string value, out string key, out string val)
        {
            string strWhere = content;
            string typevalue = type;
            string[] str = value.Split('|');
            key = "";
            val = "";
            if (str.Length == 2)
            {
                key = str[0].ToString();
                val = str[1].ToString();
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

            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
            {

                return ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelByDeparmtnetID(depids, strWhere);
            }
            else
            {
                return ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModel( depids, strWhere);
            }

        }

        private void Export(string filename)
        {
            SymmetricCrypto crypto = new SymmetricCrypto();
            string[] name = filename.Split('\\');
            outExcel(filename, name[name.Length - 1]);

        }

        private void ExportStatistics(DataSet ds, string mapPath, string tmpFileName, string key, string value)
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
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value) && value != "-1")
            {
                sheet.Cells[1, 9] = key;
            }
            int startRowIndex = 2;//起始行索引
            int rowIndex = 0; //顺序行索引            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sheet.Cells[startRowIndex + rowIndex, 1] = dr["Code"].ToString();
                sheet.Cells[startRowIndex + rowIndex, 2] = dr["LastNameCN"].ToString() + dr["FirstNameCN"].ToString();
                sheet.Cells[startRowIndex + rowIndex, 3] = dr["FirstNameEN"].ToString() + " " + dr["LastNameEN"].ToString();
                string gender = "";

                sheet.Cells[startRowIndex + rowIndex, 4] = gender;
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eip = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + dr["userid"].ToString());
                if (eip.Count > 0)
                {
                    sheet.Cells[startRowIndex + rowIndex, 5] = eip[0].CompanyName + "-" + eip[0].DepartmentName + "-" + eip[0].GroupName;
                    sheet.Cells[startRowIndex + rowIndex, 6] = eip[0].DepartmentPositionName;
                }
                sheet.Cells[startRowIndex + rowIndex, 7] = DateTime.Parse(dr["joinDate"].ToString()).ToString("yyyy-MM-dd");
                sheet.Cells[startRowIndex + rowIndex, 8] = dr["IDNumber"].ToString();
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value) && value != "-1")
                {
                    sheet.Cells[startRowIndex + rowIndex, 9] = dr[value].ToString();
                }
                //               

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
            //FileStream fin = new FileStream(pathandname, FileMode.Open);
            ////Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
            ////Response.AddHeader("Connection", "Close");
            //Response.AddHeader("Content-Transfer-Encoding", "binary");
            //Response.AddHeader("Content-Length", finfo.Length.ToString());
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
}
