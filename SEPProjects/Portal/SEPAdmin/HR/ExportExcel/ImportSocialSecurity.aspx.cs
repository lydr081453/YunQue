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
    public partial class ImportSocialSecurity : ESP.Web.UI.PageBase
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

                    //  List<SnapshotsInfo> list = new List<SnapshotsInfo>();
                    List<EmployeeBaseInfo> list2 = new List<EmployeeBaseInfo>();
                    int startRowIndex = 3;//起始行索引
                    int rowIndex = 0; //顺序行索引  
                    bool bo = false;//判断是否有错误的code

                    while (obs[startRowIndex + rowIndex, 1] != null)
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
                                
                                snap.BIProportionOfFirms = model.EmployeeWelfareInfo.BIProportionOfFirms;
                                snap.BIProportionOfIndividuals = model.EmployeeWelfareInfo.BIProportionOfIndividuals;
                                snap.Birthday = model.Birthday;
                                snap.birthInsuranceEndTime = model.EmployeeWelfareInfo.birthInsuranceEndTime;
                                snap.birthInsuranceStarTime = model.EmployeeWelfareInfo.birthInsuranceStarTime;
                                
                                
                                snap.Code = model.Code;
                                snap.compoInsuranceEndTime = model.EmployeeWelfareInfo.compoInsuranceEndTime;
                                snap.compoInsuranceStarTime = model.EmployeeWelfareInfo.compoInsuranceStarTime;
                                snap.contractEndDate = model.EmployeeWelfareInfo.contractEndDate;
                                snap.ContractInfoOK = model.ContractInfoOK;
                                snap.contractSignInfo = model.EmployeeWelfareInfo.contractSignInfo;
                                snap.contractStartDate = model.EmployeeWelfareInfo.contractStartDate;
                                snap.CreatedTime = DateTime.Now;
                                snap.Creator = UserInfo.UserID;
                                snap.CreatorName = UserInfo.Username;
                                snap.Degree = model.Degree;
                                snap.DomicilePlace = model.DomicilePlace;
                                snap.Education = model.Education;
                                
                                
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
                                
                                snap.newBasePay = snap.newMeritPay = snap.nowBasePay = snap.nowMeritPay = ESP.Salary.Utility.DESEncrypt.Encode("0");
                                
                                                                
                                snap.probationEndDate = model.EmployeeWelfareInfo.probationEndDate;
                                snap.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase;
                                snap.publicReserveFundsEndTime = model.EmployeeWelfareInfo.publicReserveFundsEndTime;
                                snap.publicReserveFundsStarTime = model.EmployeeWelfareInfo.publicReserveFundsStarTime;
                                snap.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase;
                                snap.socialInsuranceCompany = model.EmployeeWelfareInfo.socialInsuranceCompany;
                                snap.Status = model.Status;
                                snap.TypeID = model.TypeID;
                                
                                
                                snap.unemploymentInsuranceEndTime = model.EmployeeWelfareInfo.unemploymentInsuranceEndTime;
                                snap.unemploymentInsuranceStarTime = model.EmployeeWelfareInfo.unemploymentInsuranceStarTime;
                                snap.UserID = model.UserID;
                                snap.WageMonths = model.WageMonths;
                                snap.CommonName = model.CommonName;

                                snap.socialInsuranceBase = model.EmployeeWelfareInfo.socialInsuranceBase;
                                snap.medicalInsuranceBase = model.EmployeeWelfareInfo.medicalInsuranceBase;
                                snap.publicReserveFundsBase = model.EmployeeWelfareInfo.publicReserveFundsBase;
                                snap.ThisYearSalary = model.ThisYearSalary;
                                snap.socialInsuranceCompany = model.EmployeeWelfareInfo.socialInsuranceCompany;
                            }                             
                             
                             
                             
                            snap.Creator = UserInfo.UserID;
                            snap.CreatedTime = DateTime.Now;
                            snap.CreatorName = UserInfo.Username;

                            

                            string sBase = ESP.Salary.Utility.DESEncrypt.Decode(model.EmployeeWelfareInfo.socialInsuranceBase);// 养老/失业/工伤/生育缴费基数

                            //生育险
                            snap.BIProportionOfFirms = model.EmployeeWelfareInfo.BIProportionOfFirms = Convert.ToInt32(obs[startRowIndex + rowIndex, 21] != null ? obs[startRowIndex + rowIndex, 21].ToString() : "0");
                            snap.BIProportionOfIndividuals = model.EmployeeWelfareInfo.BIProportionOfIndividuals = Convert.ToInt32("0");
                            snap.BIIndividualsCosts = model.EmployeeWelfareInfo.BIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                            
                            try
                            {
                                if (model.EmployeeWelfareInfo.InsurancePlace == "外阜城镇户口" || model.EmployeeWelfareInfo.InsurancePlace == "外阜农业户口")
                                {
                                    decimal? fcost = Convert.ToDecimal(sBase) * (snap.BIProportionOfFirms / 100);
                                    snap.BIFirmsCosts = model.EmployeeWelfareInfo.BIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(fcost.ToString());
                                }
                                else
                                {
                                    snap.BIFirmsCosts = model.EmployeeWelfareInfo.BIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                                }

                            }
                            catch (Exception ex)
                            {
                                ESP.Logging.Logger.Add(ex.ToString());
                            }
                           

                            //工伤险
                            snap.CIProportionOfFirms = model.EmployeeWelfareInfo.CIProportionOfFirms = Convert.ToInt32(obs[startRowIndex + rowIndex, 17] != null ? obs[startRowIndex + rowIndex, 17].ToString() : "0");
                            snap.CIProportionOfIndividuals = model.EmployeeWelfareInfo.CIProportionOfIndividuals = Convert.ToInt32("0");
                            snap.CIIndividualsCosts = model.EmployeeWelfareInfo.CIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                           
                            try
                            {
                                decimal? fcost = Convert.ToDecimal(sBase) * (snap.CIProportionOfFirms / 100);
                                snap.CIFirmsCosts = model.EmployeeWelfareInfo.CIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(fcost.ToString());
                            }
                            catch (Exception ex)
                            {
                                ESP.Logging.Logger.Add(ex.ToString());
                            }

                            

                            //养老险
                            snap.EIProportionOfFirms = model.EmployeeWelfareInfo.EIProportionOfFirms = Convert.ToInt32(obs[startRowIndex + rowIndex, 8] != null ? obs[startRowIndex + rowIndex, 8].ToString() : "0");
                            snap.EIProportionOfIndividuals = model.EmployeeWelfareInfo.EIProportionOfIndividuals = Convert.ToInt32(obs[startRowIndex + rowIndex, 6] != null ? obs[startRowIndex + rowIndex, 6].ToString() : "0");                                                        
                            
                            try
                            {
                                decimal? fcost = Convert.ToDecimal(sBase) * (snap.EIProportionOfFirms / 100);
                                snap.EIFirmsCosts = model.EmployeeWelfareInfo.EIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(fcost.ToString());
                                decimal? icost = Convert.ToDecimal(sBase) * (snap.EIProportionOfIndividuals / 100);
                                snap.EIIndividualsCosts = model.EmployeeWelfareInfo.EIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(icost.ToString());
                            }
                            catch (Exception ex)
                            {
                                ESP.Logging.Logger.Add(ex.ToString());
                            }
                                
                            //失业险
                            snap.UIProportionOfFirms = model.EmployeeWelfareInfo.UIProportionOfFirms = Convert.ToInt32(obs[startRowIndex + rowIndex, 13] != null ? obs[startRowIndex + rowIndex, 13].ToString() : "0");
                            snap.UIProportionOfIndividuals = model.EmployeeWelfareInfo.UIProportionOfIndividuals = Convert.ToInt32(obs[startRowIndex + rowIndex, 11] != null ? obs[startRowIndex + rowIndex, 11].ToString() : "0");

                            try
                            {
                                decimal? fcost = Convert.ToDecimal(sBase) * (snap.UIProportionOfFirms / 100);
                                decimal? icost = Convert.ToDecimal(sBase) * (snap.UIProportionOfIndividuals / 100);
                                snap.UIFirmsCosts = model.EmployeeWelfareInfo.UIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(fcost.ToString());
                                if (model.EmployeeWelfareInfo.InsurancePlace == "外阜农业户口")
                                {
                                    snap.UIIndividualsCosts = model.EmployeeWelfareInfo.UIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(icost.ToString());
                                }
                                else
                                {
                                    snap.UIIndividualsCosts = model.EmployeeWelfareInfo.UIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode("0");
                                }
                            }
                            catch (Exception ex)
                            {
                                ESP.Logging.Logger.Add(ex.ToString());
                            }

                            //医疗险
                            snap.MIBigProportionOfIndividuals = model.EmployeeWelfareInfo.MIBigProportionOfIndividuals = Convert.ToInt32(obs[startRowIndex + rowIndex, 25] != null ? obs[startRowIndex + rowIndex, 25].ToString() : "0");
                            snap.MIProportionOfFirms = model.EmployeeWelfareInfo.MIProportionOfFirms = Convert.ToInt32(obs[startRowIndex + rowIndex, 27] != null ? obs[startRowIndex + rowIndex, 27].ToString() : "0");
                            snap.MIProportionOfIndividuals = model.EmployeeWelfareInfo.MIProportionOfIndividuals = Convert.ToInt32(obs[startRowIndex + rowIndex, 24] != null ? obs[startRowIndex + rowIndex, 24].ToString() : "0");

                            string mBase = ESP.Salary.Utility.DESEncrypt.Decode(model.EmployeeWelfareInfo.medicalInsuranceBase);// 医疗基数

                            try
                            {
                                decimal? fcost = Convert.ToDecimal(mBase) * (snap.MIProportionOfFirms / 100);
                                decimal? icost = Convert.ToDecimal(mBase) * (snap.MIProportionOfIndividuals / 100);
                                snap.MIFirmsCosts = model.EmployeeWelfareInfo.MIFirmsCosts = ESP.Salary.Utility.DESEncrypt.Encode(fcost.ToString());
                                snap.MIIndividualsCosts = model.EmployeeWelfareInfo.MIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(icost.ToString());
                            }
                            catch (Exception ex)
                            {
                                ESP.Logging.Logger.Add(ex.ToString());
                            }

                            //公积金
                            snap.PRFProportionOfFirms = model.EmployeeWelfareInfo.PRFProportionOfFirms = Convert.ToInt32(obs[startRowIndex + rowIndex, 30] != null ? obs[startRowIndex + rowIndex, 30].ToString() : "0");
                            snap.PRFProportionOfIndividuals = model.EmployeeWelfareInfo.PRFProportionOfIndividuals = Convert.ToInt32(obs[startRowIndex + rowIndex, 30] != null ? obs[startRowIndex + rowIndex, 30].ToString() : "0");

                            string pBase = ESP.Salary.Utility.DESEncrypt.Decode(model.EmployeeWelfareInfo.publicReserveFundsBase);// 公积金基数

                            try
                            {
                                decimal? cost = Convert.ToDecimal(pBase) * (snap.PRFProportionOfFirms / 100);
                                snap.PRFirmsCosts = model.EmployeeWelfareInfo.PRFirmsCosts = snap.PRIIndividualsCosts = model.EmployeeWelfareInfo.PRIIndividualsCosts = ESP.Salary.Utility.DESEncrypt.Encode(cost.ToString());
                            }
                            catch (Exception ex)
                            {
                                ESP.Logging.Logger.Add(ex.ToString());
                            }

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
                    string tmpFileName = Server.MapPath("../") + "ExcelTemplate\\ImportSocialSecurityError" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";
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

                    if (list2.Count > 0)
                    {
                        if (0 < EmployeeBaseManager.Update(list2, LogManager.GetLogModel(UserInfo.Username + "导入了保险比例数据", UserInfo.UserID, UserInfo.Username, 0, "", 0)))
                        {
                            if (bo)
                            {
                                ESP.HumanResource.Common.SymmetricCrypto crypto = new ESP.HumanResource.Common.SymmetricCrypto();
                                string content = System.Web.HttpUtility.UrlEncode(crypto.EncrypString(filename));
                                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), " var win = window.open('/HR/ExportExcel/ExportExcel.aspx?filename=" + content + "',null,'height=10px,width=10px,scrollbars=yes,top=100px,left=100px');", true);

                            }

                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportSocialSecurity.aspx';alert('导入成功！');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportSocialSecurity.aspx';alert('导入失败！');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportSocialSecurity.aspx';alert('导入失败！');", true);
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
                    string fn = "/HR/ExcelTemplate/ImportSocialSecurity_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileCV.FileName;
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
