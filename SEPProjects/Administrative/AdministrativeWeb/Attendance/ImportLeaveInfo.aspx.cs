using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using ESP.Administrative.BusinessLogic;
using System.IO;

namespace AdministrativeWeb.Attendance
{
    public partial class ImportLeaveInfo : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导入年假和奖励假信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            ImportData();
        }

        /// <summary>
        /// 导入年假信息
        /// </summary>
        private void ImportData()
        {
            string filename = "";
            if (fileARLeave.PostedFile != null && fileARLeave.PostedFile.ContentLength > 0)
            {
                filename = SaveFile();
                ImportData(filename);
            }
        }

        /// <summary>
        /// 导入年假和奖励假信息
        /// </summary>
        /// <param name="filename"></param>
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

                    List<ALAndRLInfo> list = new List<ALAndRLInfo>();
                    int startRowIndex = 3;   // 起始行索引
                    int rowIndex = 0;        // 顺序行索引  
                    bool bo = false;         // 判断是否有错误的code

                    while (obs[startRowIndex + rowIndex, 2] != null)
                    {
                        try
                        {
                            // 通过员工编号获得用户信息
                            string userCode = obs[startRowIndex + rowIndex, 2].ToString();
                            ESP.HumanResource.Entity.EmployeeBaseInfo employeeBaseModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userCode);
                            if (employeeBaseModel != null)
                            {
                                ALAndRLInfo model = new ALAndRLInfo();
                                model.UserID = employeeBaseModel.UserID;
                                model.UserCode = userCode;
                                model.UserName = employeeBaseModel.Username;
                                // 中文名
                                model.EmployeeName = obs[startRowIndex + rowIndex, 3] == null ? "" : obs[startRowIndex + rowIndex, 3].ToString().Replace(" ", "");
                                // 年假年份
                                model.LeaveYear = obs[startRowIndex + rowIndex, 4] == null ? 0 : int.Parse(obs[startRowIndex + rowIndex, 4].ToString().Replace(" ", ""));
                                // 年假总天数
                                model.LeaveNumber = obs[startRowIndex + rowIndex, 5] == null ? 0 : decimal.Parse(obs[startRowIndex + rowIndex, 5].ToString().Replace(" ", ""));
                                // 剩余年假数
                                model.RemainingNumber = obs[startRowIndex + rowIndex, 6] == null ? 0 : decimal.Parse(obs[startRowIndex + rowIndex, 6].ToString().Replace(" ", ""));
                                // 年假类型
                                model.LeaveType = (int)AAndRLeaveType.AnnualType;
                                model.ValidTo = new DateTime(model.LeaveYear, Status.AnnualLeaveLastMonth, Status.AnnualLeaveLastDay);
                                model.CreateTime = DateTime.Now;
                                model.UpdateTime = DateTime.Now;
                                model.Deleted = false;
                                model.OperatorID = UserID;
                                model.OperatorDept = 0;
                                list.Add(model);

                                // 判断是否有奖励假信息
                                if (obs[startRowIndex + rowIndex, 7] != null && obs[startRowIndex + rowIndex, 7].ToString().Trim() != "0")
                                {
                                    ALAndRLInfo rlModel = new ALAndRLInfo();
                                    rlModel.UserID = employeeBaseModel.UserID;
                                    rlModel.UserCode = userCode;
                                    rlModel.UserName = employeeBaseModel.Username;
                                    // 中文名
                                    rlModel.EmployeeName = obs[startRowIndex + rowIndex, 3] == null ? "" : obs[startRowIndex + rowIndex, 3].ToString().Replace(" ", "");
                                    // 奖励假年份
                                    rlModel.LeaveYear = obs[startRowIndex + rowIndex, 4] == null ? 0 : int.Parse(obs[startRowIndex + rowIndex, 4].ToString().Replace(" ", ""));
                                    // 奖励假天数
                                    rlModel.LeaveNumber = decimal.Parse(obs[startRowIndex + rowIndex, 7].ToString().Replace(" ", ""));
                                    // 剩余奖励假天数
                                    rlModel.RemainingNumber = obs[startRowIndex + rowIndex, 7] == null ? 0 : decimal.Parse(obs[startRowIndex + rowIndex, 7].ToString().Replace(" ", ""));
                                    // 奖励假类型
                                    rlModel.LeaveType = (int)AAndRLeaveType.RewardType;
                                    rlModel.ValidTo = DateTime.Parse(obs[startRowIndex + rowIndex, 8].ToString().Trim());
                                    rlModel.CreateTime = DateTime.Now;
                                    rlModel.UpdateTime = DateTime.Now;
                                    rlModel.Deleted = false;
                                    rlModel.OperatorID = UserID;
                                    rlModel.OperatorDept = 0;
                                    list.Add(rlModel);
                                }
                            }
                            else
                            {
                                bo = true;
                                sheet.get_Range("A" + (startRowIndex + rowIndex).ToString(), "H" + (startRowIndex + rowIndex).ToString()).Interior.ColorIndex = 3;
                            }
                        }
                        catch (Exception)
                        {
                            bo = true;
                            sheet.get_Range("A" + (startRowIndex + rowIndex).ToString(), "H" + (startRowIndex + rowIndex).ToString()).Interior.ColorIndex = 3;
                        }
                        
                        rowIndex++;
                    }

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

                    ALAndRLManager manager = new ALAndRLManager();

                    if (manager.SaveAnnualLeaveInfo(list))
                    {
                        if (bo)
                        {
                            FileHelper.outExcel(filename, "", "年假导入异常信息.xls", Response);
                        }
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportLeaveInfo.aspx';alert('年假信息导入成功！');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportLeaveInfo.aspx';alert('年假信息导入失败，请与系统管理员联系！');", true);
                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportLeaveInfo.aspx';alert('年假信息导入失败，请与系统管理员联系！');", true);
                }
            }
        }

        /// <summary>
        /// 将年假信息保存在服务器上
        /// </summary>
        /// <returns></returns>
        private string SaveFile()
        {
            HttpPostedFile myFile = fileARLeave.PostedFile;
            try
            {
                if (myFile.FileName != null && myFile.ContentLength > 0)
                {
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string fn = "/ExcelTemplate/AnnualLeave_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileARLeave.FileName;
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
    }
}
