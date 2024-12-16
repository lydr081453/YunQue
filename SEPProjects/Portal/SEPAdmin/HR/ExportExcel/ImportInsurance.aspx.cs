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
    public partial class ImportInsurance : ESP.Web.UI.PageBase
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

            if (!IsPostBack)
            {
                initDrp();
            }

        }

        protected void initDrp()
        {
            int year = DateTime.Now.Year - 10;
            for (int i = 0; i < 20; i++)
            {
                drpBeginTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }
            drpBeginTimeY.SelectedValue = DateTime.Now.Year.ToString();
            for (int i = 1; i <= 12; i++)
            {
                drpBeginTimeM.Items.Insert(i - 1, new ListItem((i).ToString(), (i).ToString()));
            }
            drpBeginTimeM.SelectedValue = DateTime.Now.Month.ToString();
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
                    //       object[,] obs = (object[,])sheet.Cells.Value2;


                  //  List<SnapshotsInfo> list = new List<SnapshotsInfo>();
                    List<EmployeeBaseInfo> list2 = new List<EmployeeBaseInfo>();
                    int startRowIndex = 3;//起始行索引
                    int rowIndex = 0; //顺序行索引  
                    bool bo = false;//判断是否有错误的code

                    while (obs[startRowIndex + rowIndex, 1] != null )
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo model = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(obs[startRowIndex + rowIndex, 1].ToString());
                        SnapshotsInfo snap = SnapshotsManager.GetTopModel(obs[startRowIndex + rowIndex, 1].ToString().Trim());
                        if (model != null)
                        {
                            if (snap == null)
                            {
                                snap = new SnapshotsInfo();                               
                                snap.ArchiveInfoOK = model.ArchiveInfoOK;
                                snap.BaseInfoOK = model.BaseInfoOK;
                                snap.BIFirmsCosts = model.EmployeeWelfareInfo.BIFirmsCosts;
                                snap.BIIndividualsCosts = model.EmployeeWelfareInfo.BIIndividualsCosts;
                                snap.BIProportionOfFirms = model.EmployeeWelfareInfo.BIProportionOfFirms;
                                snap.BIProportionOfIndividuals = model.EmployeeWelfareInfo.BIProportionOfIndividuals;
                                snap.Birthday = model.Birthday;
                                snap.birthInsuranceEndTime = model.EmployeeWelfareInfo.birthInsuranceEndTime;
                                snap.birthInsuranceStarTime = model.EmployeeWelfareInfo.birthInsuranceStarTime;                                
                                snap.CIFirmsCosts = model.EmployeeWelfareInfo.CIFirmsCosts;
                                snap.CIIndividualsCosts = model.EmployeeWelfareInfo.CIIndividualsCosts;
                                snap.CIProportionOfFirms = model.EmployeeWelfareInfo.CIProportionOfFirms;
                                snap.CIProportionOfIndividuals = model.EmployeeWelfareInfo.CIProportionOfIndividuals;                                
                                snap.Code = model.Code;
                                snap.compoInsuranceEndTime = model.EmployeeWelfareInfo.compoInsuranceEndTime;
                                snap.compoInsuranceStarTime = model.EmployeeWelfareInfo.compoInsuranceStarTime;
                                snap.contractEndDate = model.EmployeeWelfareInfo.contractEndDate;
                                snap.ContractInfoOK = model.ContractInfoOK;
                                snap.contractSignInfo = model.EmployeeWelfareInfo.contractSignInfo;
                                snap.contractStartDate = model.EmployeeWelfareInfo.contractStartDate;
                                snap.CreatedTime = DateTime.Parse(drpBeginTimeY.SelectedItem.Value + "-" + drpBeginTimeM.SelectedItem.Value + "-01");
                                snap.Creator = UserInfo.UserID;
                                snap.CreatorName = UserInfo.Username;
                                snap.Degree = model.Degree;
                                snap.DomicilePlace = model.DomicilePlace;
                                snap.Education = model.Education;
                                snap.EIFirmsCosts = model.EmployeeWelfareInfo.EIFirmsCosts;
                                snap.EIIndividualsCosts = model.EmployeeWelfareInfo.EIIndividualsCosts;
                                snap.EIProportionOfFirms = model.EmployeeWelfareInfo.EIProportionOfFirms;
                                snap.EIProportionOfIndividuals = model.EmployeeWelfareInfo.EIProportionOfIndividuals;                                
                                snap.EmergencyContact = model.EmergencyContact;
                                snap.EmergencyContactPhone = model.EmergencyContactPhone;
                                snap.endowmentInsuranceEndTime = model.EmployeeWelfareInfo.endowmentInsuranceEndTime;
                                snap.endowmentInsuranceStarTime = model.EmployeeWelfareInfo.endowmentInsuranceStarTime;                                
                                snap.Gender = model.Gender;
                                snap.GraduatedDate = model.GraduatedDate;
                                snap.GraduatedFrom = model.GraduateFrom;
                                snap.InsuranceInfoOK = model.InsuranceInfoOK;
                                snap.InsurancePlace = model.EmployeeWelfareInfo.InsurancePlace;
                                snap.isArchive = model.EmployeeWelfareInfo.isArchive;
                                snap.IsCertification = model.IsCertification;
                                snap.IsForeign = model.IsForeign;
                                snap.Major = model.Major;
                                snap.MaritalStatus = model.MaritalStatus;
                                snap.medicalInsuranceBase = model.EmployeeWelfareInfo.medicalInsuranceBase;
                                snap.medicalInsuranceEndTime = model.EmployeeWelfareInfo.medicalInsuranceEndTime;
                                snap.medicalInsuranceStarTime = model.EmployeeWelfareInfo.medicalInsuranceStarTime;
                                snap.MIBigProportionOfIndividuals = model.EmployeeWelfareInfo.MIBigProportionOfIndividuals;
                                snap.MIFirmsCosts = model.EmployeeWelfareInfo.MIFirmsCosts;
                                snap.MIIndividualsCosts = model.EmployeeWelfareInfo.MIIndividualsCosts;
                                snap.MIProportionOfFirms = model.EmployeeWelfareInfo.MIProportionOfFirms;
                                snap.MIProportionOfIndividuals = model.EmployeeWelfareInfo.MIProportionOfIndividuals;
                                snap.newBasePay = snap.newMeritPay = snap.nowBasePay = snap.nowMeritPay = ESP.Salary.Utility.DESEncrypt.Encode("0");
                                snap.PRFirmsCosts = model.EmployeeWelfareInfo.PRFirmsCosts;
                                snap.PRFProportionOfFirms = model.EmployeeWelfareInfo.PRFProportionOfFirms;
                                snap.PRFProportionOfIndividuals = model.EmployeeWelfareInfo.PRFProportionOfIndividuals;
                                snap.PRIIndividualsCosts = model.EmployeeWelfareInfo.PRIIndividualsCosts;
                                snap.probationEndDate = model.EmployeeWelfareInfo.probationEndDate;
                                snap.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase;
                                snap.publicReserveFundsEndTime = model.EmployeeWelfareInfo.publicReserveFundsEndTime;
                                snap.publicReserveFundsStarTime = model.EmployeeWelfareInfo.publicReserveFundsStarTime;
                                snap.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase;
                                snap.socialInsuranceCompany = model.EmployeeWelfareInfo.socialInsuranceCompany;
                                snap.Status = model.Status;
                                snap.TypeID = model.TypeID;
                                snap.UIFirmsCosts = model.EmployeeWelfareInfo.UIFirmsCosts;
                                snap.UIIndividualsCosts = model.EmployeeWelfareInfo.UIIndividualsCosts;
                                snap.UIProportionOfFirms = model.EmployeeWelfareInfo.UIProportionOfFirms;
                                snap.UIProportionOfIndividuals = model.EmployeeWelfareInfo.UIProportionOfIndividuals;
                                snap.unemploymentInsuranceEndTime = model.EmployeeWelfareInfo.unemploymentInsuranceEndTime;
                                snap.unemploymentInsuranceStarTime = model.EmployeeWelfareInfo.unemploymentInsuranceStarTime;
                                snap.UserID = model.UserID;
                                snap.WageMonths = model.WageMonths;
                                snap.CommonName = model.CommonName;
                                
                            }
                            snap.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode(obs[startRowIndex + rowIndex, 3] != null ? obs[startRowIndex + rowIndex, 3].ToString() : "0");
                            snap.medicalInsuranceBase = model.EmployeeWelfareInfo.medicalInsuranceBase = ESP.Salary.Utility.DESEncrypt.Encode(obs[startRowIndex + rowIndex, 4] != null ? obs[startRowIndex + rowIndex, 4].ToString() : "0");
                            snap.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase = ESP.Salary.Utility.DESEncrypt.Encode(obs[startRowIndex + rowIndex, 5] != null ? obs[startRowIndex + rowIndex, 5].ToString() : "0");
                            snap.ThisYearSalary = model.ThisYearSalary = obs[startRowIndex + rowIndex, 6] != null ? obs[startRowIndex + rowIndex, 6].ToString() : snap.ThisYearSalary != null ? snap.ThisYearSalary : "";
                            snap.socialInsuranceCompany = model.EmployeeWelfareInfo.socialInsuranceCompany = obs[startRowIndex + rowIndex, 7] != null ? obs[startRowIndex + rowIndex, 7].ToString() : snap.socialInsuranceCompany != null ? snap.socialInsuranceCompany : "";
                            snap.Creator = UserInfo.UserID;
                            snap.CreatedTime = DateTime.Parse(drpBeginTimeY.SelectedItem.Value + "-" + drpBeginTimeM.SelectedItem.Value + "-01");
                            snap.CreatorName = UserInfo.Username;                            

                            model.EmployeesCurrentSnapshot = snap;
                            list2.Add(model);

                        }
                        else
                        {
                            
                            bo = true;
                            sheet.get_Range("B" + (startRowIndex + rowIndex).ToString(), "B" + (startRowIndex + rowIndex).ToString()).Interior.ColorIndex = 3;
                            // sheet.get_Range("C" + (startRowIndex + rowIndex).ToString(), "C" + (startRowIndex + rowIndex).ToString()).Font.Color = 3;



                        }
                        rowIndex++;
                        if (obs.GetLength(0) < startRowIndex + rowIndex)
                        {
                            break;
                        }

                    }
                    string tmpFileName = Server.MapPath("../") + "ExcelTemplate\\ImportInsuranceError" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
                    if (bo)
                    {
                        workbook.Saved = true;
                        try
                        {
                            workbook.Save();
                            // workbook.SaveAs(tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            //  app.ActiveWorkbook.SaveCopyAs(filename);                     


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
                                        
                    if ( list2.Count > 0)
                    {
                        if (0 < EmployeeBaseManager.Update(list2, LogManager.GetLogModel(UserInfo.Username + "导入了社保数据", UserInfo.UserID, UserInfo.Username, 0, "", 0)))
                        {
                            if (bo)
                            {
                                ESP.HumanResource.Common.SymmetricCrypto crypto = new ESP.HumanResource.Common.SymmetricCrypto();
                                string content = System.Web.HttpUtility.UrlEncode(crypto.EncrypString(filename));
                                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), " var win = window.open('/HR/ExportExcel/ExportExcel.aspx?filename=" + content + "',null,'height=10px,width=10px,scrollbars=yes,top=100px,left=100px');", true);

                            }

                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportInsurance.aspx';alert('导入成功！');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportInsurance.aspx';alert('导入失败！');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportInsurance.aspx';alert('导入失败！');", true);
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
                    string fn = "/HR/ExcelTemplate/ImportInsurance_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
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
