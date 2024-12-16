using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Data;
using System.IO;
using ESP.HumanResource.Common;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;

namespace SEPAdmin.HR.ExportExcel
{
    public partial class ImportDeduct : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["content"]))
            {
                SymmetricCrypto crypto = new SymmetricCrypto();
                string filename = crypto.DecrypString(Request["content"].ToString());
                string[] name = filename.Split('\\');
                outExcel(filename, name[name.Length - 1]);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ImportData();
        }

        private void ImportData()
        {
            string filename = "";
            if (fileCV.PostedFile != null && fileCV.PostedFile.ContentLength > 0)
            {
                filename = SaveFile();
                ImportData(filename);
            }
        }
        private void ImportData(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {

                try
                {
                    Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
                    Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
                    Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
                    Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
                    int s = sheet.UsedRange.Count;
                    object[,] obs = (object[,])sheet.UsedRange.Value2;

                    List<ESP.Administrative.Entity.MonthStatInfo> list1 = new List<ESP.Administrative.Entity.MonthStatInfo>();
                    List<ESP.Administrative.Entity.MonthStatInfo> list2 = new List<ESP.Administrative.Entity.MonthStatInfo>();

                    int startRowIndex = 5;//起始行索引
                    int rowIndex = 0; //顺序行索引  
                    bool bo = false;//判断是否有错误的code

                    int Year = 0, Month = 0;
                    if (obs[1, 2] != null && obs[1,5] != null)
                    {
                        try
                        {
                            Year = int.Parse(obs[1, 2].ToString());
                            Month = int.Parse(obs[1, 5].ToString());
                        }
                        catch
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportDeduct.aspx';alert('导入失败！无法识别的对应时间');", true);
                            return;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportDeduct.aspx';alert('导入失败！无法识别的对应时间');", true);
                        return;
                    }
                    

                    while (obs[startRowIndex + rowIndex, 1] != null)
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo model = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(obs[startRowIndex + rowIndex, 1].ToString());
                       MonthStatInfo monthstat = null;

                        if (model != null)
                        {
                            ESP.HumanResource.Entity.UsersInfo userinfo = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(model.UserID);
                            MonthStatInfo stat = new MonthStatManager().GetMonthStatInfo(model.UserID,Year,Month);
                            if (stat == null)
                            {
                                try
                                {
                                    monthstat = new MonthStatInfo();
                                    monthstat.UserID = model.UserID;
                                    monthstat.UserCode = model.Code;
                                    monthstat.EmployeeName = userinfo.LastNameCN + userinfo.FirstNameCN;
                                    monthstat.Year = Year;
                                    monthstat.Month = Month;
                                    monthstat.WorkHours = 0;
                                    monthstat.LateCount = 0;
                                    monthstat.LeaveEarlyCount = 0;
                                    monthstat.SickLeaveHours = 0;
                                    monthstat.AffairLeaveHours = 0;
                                    monthstat.AnnualLeaveDays = 0;
                                    monthstat.Overannualleavedays = 0;
                                    monthstat.MaternityLeaveHours = 0;
                                    monthstat.MarriageLeaveHours = 0;
                                    monthstat.FuneralLeaveHours = 0;
                                    monthstat.AbsentDays = 0;
                                    monthstat.OverTimeHours = 0;
                                    monthstat.EvectionDays = 0;
                                    monthstat.EgressHours = 0;
                                    monthstat.Offtunehours = 0;
                                    monthstat.State = ESP.Administrative.Common.Status.MonthStatAppState_Passed;
                                    monthstat.Deleted = false;
                                    monthstat.Sort = 0;
                                    monthstat.DeductSum = Convert.ToDecimal(obs[startRowIndex + rowIndex, 3] != null ? obs[startRowIndex + rowIndex, 3].ToString() : "0");
                                    monthstat.CreateTime = monthstat.UpdateTime = DateTime.Now;
                                    list2.Add(monthstat);
                                }
                                catch
                                {
                                    bo = true;
                                    //模版第一行有空行，标记是下移一位
                                    sheet.get_Range("B" + (startRowIndex + rowIndex+1).ToString(), "A" + (startRowIndex + rowIndex+1).ToString()).Interior.ColorIndex = 3;
                                }

                            }
                            else
                            {
                                try
                                {
                                    stat.DeductSum = Convert.ToDecimal(obs[startRowIndex + rowIndex, 3] != null ? obs[startRowIndex + rowIndex, 3].ToString() : "0");
                                    stat.UpdateTime = DateTime.Now;
                                    list1.Add(stat);
                                }
                                catch
                                {
                                    bo = true;
                                    //模版第一行有空行，标记是下移一位
                                    sheet.get_Range("B" + (startRowIndex + rowIndex + 1).ToString(), "A" + (startRowIndex + rowIndex + 1).ToString()).Interior.ColorIndex = 3;
                                }
                            }
                        }
                        else
                        {

                            bo = true;
                            //模版第一行有空行，标记是下移一位
                            sheet.get_Range("B" + (startRowIndex + rowIndex + 1).ToString(), "A" + (startRowIndex + rowIndex + 1).ToString()).Interior.ColorIndex = 3;
                        }
                        rowIndex++;
                        if (obs.GetLength(0) < startRowIndex + rowIndex)
                        {
                            break;
                        }

                    }
                    string tmpFileName = Server.MapPath("../") + "ExcelTemplate\\ImportDeductError" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
                    if (bo)
                    {
                        workbook.Saved = true;
                        try
                        {
                            workbook.Save();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {

                        }
                    }
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

                    if (list2.Count > 0 || list1.Count > 0)
                    {
                        if ((0 < new ESP.Administrative.BusinessLogic.MonthStatManager().AddandUpdate(list1, list2, UserInfo.Username + "Import Deduct data")))
                        {
                            if (bo)
                            {
                                ESP.HumanResource.Common.SymmetricCrypto crypto = new ESP.HumanResource.Common.SymmetricCrypto();
                                string content = System.Web.HttpUtility.UrlEncode(crypto.EncrypString(filename));
                                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), " var win = window.open('/HR/ExportExcel/ExportExcel.aspx?filename=" + content + "',null,'height=10px,width=10px,scrollbars=yes,top=100px,left=100px');", true);

                            }

                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportDeduct.aspx';alert('导入成功！');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportDeduct.aspx';alert('导入失败！');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportDeduct.aspx';alert('没有数据进行导入！');", true);
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportDeduct.aspx';alert('导入失败！');", true);
                }
            }
        }
        private string SaveFile()
        {
            HttpPostedFile myFile = fileCV.PostedFile;
            try
            {
                if (myFile.FileName != null && myFile.ContentLength > 0)//&& theFile.ContentLength <= Config.PHOTO_CONTENT_LENGTH)
                {
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string fn = "/HR/ExcelTemplate/ImportDeduct_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
                    myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(fn));

                    return System.Web.HttpContext.Current.Server.MapPath(fn);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString());
                return "";
            }

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
