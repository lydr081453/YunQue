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

namespace SEPAdmin.HR.ExportExcel
{
    public partial class ImportTemporaryMeritPay : ESP.Web.UI.PageBase
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
                    
                    List<TemporaryMeritPayInfo> list1 = new List<TemporaryMeritPayInfo>();
                    List<TemporaryMeritPayInfo> list2 = new List<TemporaryMeritPayInfo>();
                    
                    int startRowIndex = 4;//起始行索引
                    int rowIndex = 0; //顺序行索引  
                    bool bo = false;//判断是否有错误的code

                    int Year = 0,Month = 0;
                    if(obs[1, 2] != null)
                    {
                        string date = obs[1, 2].ToString();
                        string[] dates = date.Split('-');
                        if (dates.Length >= 2)
                        {
                            Year = int.Parse(dates[0].ToString());
                            Month = int.Parse(dates[1].ToString());
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportTemporaryMeritPay.aspx';alert('导入失败！无法识别的变动时间');", true);
                            return;                          
                        }
                    }

                    while (obs[startRowIndex + rowIndex, 1] != null)
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo model = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(obs[startRowIndex + rowIndex, 1].ToString());
                        TemporaryMeritPayInfo tpinfo = new TemporaryMeritPayInfo();
 
                        if (model != null)
                        {                            

                            int t = 0;
                            List<TemporaryMeritPayInfo> tplist = TemporaryMeritPayManager.GetModelList(" userid=" + model.UserID);
                            if (tplist.Count > 0)
                            {
                                foreach (TemporaryMeritPayInfo tp in tplist)
                                {                                    
                                    if (tp.ImplementYear == Year && tp.ImplementMonth == Month)
                                    {
                                        tpinfo = tp;
                                        t = 1;
                                        break;
                                    }                                    
                                }
                            }                            

                            tpinfo.MeritPay = ESP.Salary.Utility.DESEncrypt.Encode(obs[startRowIndex + rowIndex, 3] != null ? obs[startRowIndex + rowIndex, 3].ToString() : "0"); 

                            tpinfo.Creator  = UserInfo.UserID;
                            tpinfo.CreateDate = DateTime.Now;
                            tpinfo.ImplementYear = Year;
                            tpinfo.ImplementMonth = Month;

                            if (t == 0)
                            {
                                tpinfo.UserID = model.UserID;
                                tpinfo.Code = model.Code;
                                
                            }
                            
                            if (t == 0)
                            {
                                list2.Add(tpinfo);
                            }
                            else
                            {
                                list1.Add(tpinfo);
                            }
                            t = 0;
                        }
                        else
                        {

                            bo = true;
                            sheet.get_Range("B" + (startRowIndex + rowIndex).ToString(), "A" + (startRowIndex + rowIndex).ToString()).Interior.ColorIndex = 3;  
                        }
                        rowIndex++;
                        if (obs.GetLength(0) < startRowIndex + rowIndex)
                        {
                            break;
                        }

                    }
                    string tmpFileName = Server.MapPath("../") + "ExcelTemplate\\ImportTemporaryMeritPayError" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
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
                        if ((0 < TemporaryMeritPayManager.AddandUpdate(list1, list2, LogManager.GetLogModel(UserInfo.Username + "导入了绩效变动数据", UserInfo.UserID, UserInfo.Username, 0, "", 0))))
                        {
                            if (bo)
                            {
                                ESP.HumanResource.Common.SymmetricCrypto crypto = new ESP.HumanResource.Common.SymmetricCrypto();
                                string content = System.Web.HttpUtility.UrlEncode(crypto.EncrypString(filename));
                                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), " var win = window.open('/HR/ExportExcel/ExportExcel.aspx?filename=" + content + "',null,'height=10px,width=10px,scrollbars=yes,top=100px,left=100px');", true);

                            }

                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportTemporaryMeritPay.aspx';alert('导入成功！');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportTemporaryMeritPay.aspx';alert('导入失败！');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportTemporaryMeritPay.aspx';alert('没有数据进行导入！');", true);
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportTemporaryMeritPay.aspx';alert('导入失败！');", true);
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
                    string fn = "/HR/ExcelTemplate/ImportTemporaryMeritPay_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
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
