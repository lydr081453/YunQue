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
    public partial class ImportEmployeeBase : ESP.Web.UI.PageBase
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

                    int startRowIndex = 2;//起始行索引
                    int rowIndex = 0; //顺序行索引  
                    bool bo = false;//判断是否有错误的code

                    List<ESP.HumanResource.Entity.SnapshotsInfo> list1 = new List<SnapshotsInfo>();
                    List<ESP.HumanResource.Entity.EmployeeBaseInfo> list2 = new List<ESP.HumanResource.Entity.EmployeeBaseInfo>();

                    //根据第一个是否有数据判断循环
                    while (obs[startRowIndex + rowIndex, 1] != null)
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo model = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(obs[startRowIndex + rowIndex, 1].ToString());

                        if (model != null)
                        {
                            ESP.HumanResource.Entity.SnapshotsInfo snap = ESP.HumanResource.BusinessLogic.SnapshotsManager.GetTopModel(model.UserID);
                            if (snap != null)
                            {
                                try
                                {
                                    string phone = obs[startRowIndex + rowIndex, 3].ToString();
                                    string[] phones = phone.Split('-');
                                    //判断‘-’的个数，不足的补齐到3个
                                    for (int i = phones.Length; i < 4; i++)
                                    {
                                        phone += "-";
                                    }
                                    model.Phone1 = snap.Phone1 = phone;
                                    model.MobilePhone = snap.MobilePhone = obs[startRowIndex + rowIndex, 4].ToString();
                                    model.IDNumber = snap.IDNumber = obs[startRowIndex + rowIndex, 4].ToString();
                                    model.Birthday = snap.Birthday = DateTime.Parse(obs[startRowIndex + rowIndex, 6].ToString());

                                    list1.Add(snap);
                                    list2.Add(model);
                                }
                                catch
                                {
                                    bo = true;
                                    //模版第一行有空行，标记是下移一位
                                    sheet.get_Range("B" + (startRowIndex + rowIndex + 1).ToString(), "A" + (startRowIndex + rowIndex + 1).ToString()).Interior.ColorIndex = 3;
                                }
                            }
                            else
                            {
                                bo = true;
                                //模版第一行有空行，标记是下移一位
                                sheet.get_Range("B" + (startRowIndex + rowIndex + 1).ToString(), "A" + (startRowIndex + rowIndex + 1).ToString()).Interior.ColorIndex = 3;
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
                    string tmpFileName = Server.MapPath("../") + "ExcelTemplate\\ImportEmployeeBaseError" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
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

                    if (list2.Count > 0)
                    {
                        if ((0 < ESP.HumanResource.BusinessLogic.EmployeeBaseManager.UpdateEmployees(list2, list1, UserInfo.Username + "Import EmployeeBase Data")))
                        {
                            if (bo)
                            {
                                ESP.HumanResource.Common.SymmetricCrypto crypto = new ESP.HumanResource.Common.SymmetricCrypto();
                                string content = System.Web.HttpUtility.UrlEncode(crypto.EncrypString(filename));
                                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), " var win = window.open('/HR/ExportExcel/ExportExcel.aspx?filename=" + content + "',null,'height=10px,width=10px,scrollbars=yes,top=100px,left=100px');", true);
                            }

                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportEmployeeBase.aspx';alert('导入成功！');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportEmployeeBase.aspx';alert('导入失败！');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportEmployeeBase.aspx';alert('没有数据进行导入！');", true);
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportEmployeeBase.aspx';alert('导入失败！');", true);
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
                    string fn = "/HR/ExcelTemplate/ImportEmployeeBase_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
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
