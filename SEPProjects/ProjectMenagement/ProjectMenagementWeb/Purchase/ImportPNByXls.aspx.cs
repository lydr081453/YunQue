using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Purchase
{
    public partial class ImportPNByXls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string SaveFile()
        {
            HttpPostedFile myFile = fileUp.PostedFile;
            try
            {
                if (myFile.FileName != null && myFile.ContentLength > 0)
                {
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string fn = "/Templates/ImportPN_" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fileUp.FileName;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
             string filename = "";
            if (this.fileUp.PostedFile != null && fileUp.PostedFile.ContentLength > 0)
            {
                filename = SaveFile();
            }
            List<ESP.Finance.Entity.ReturnInfo> returnList = GetData(filename);
            ViewState["ReturnList"] = returnList;
            this.gvG.DataSource = returnList;
            this.gvG.DataBind();
        }

        private List<ESP.Finance.Entity.ReturnInfo> GetData(string filename)
        {
            List<ESP.Finance.Entity.ReturnInfo> returnList = new List<ESP.Finance.Entity.ReturnInfo>();
          
                Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
                int s = sheet.UsedRange.Count;
                object[,] obs = (object[,])sheet.UsedRange.Value2;
                int startRowIndex = 10;//起始行索引
                int rowIndex = 0; //顺序行索引  
                bool bo = false;//判断是否有错误的code 
                bool IsValidDept = false;
                try
                {
                    if (obs[5, 7] == null || obs[6, 7] == null || obs[7, 7] == null)
                        return null;
                    string SupplierName = obs[5, 7].ToString();
                    string SupplierBank = obs[6, 7].ToString();
                    string SupplierAccount = obs[7, 7].ToString();
                    ESP.Finance.Entity.ProjectInfo projectModel = null;

                    while (obs[startRowIndex + rowIndex, 1] != null)
                    {
                        //序号	项目号	费用发生日期	申请人	 员工编号 	费用所属组	费用明细描述	申请金额	时间	单号

                        ESP.Finance.Entity.ReturnInfo returnModel = new ESP.Finance.Entity.ReturnInfo();
                        string projectcode = obs[startRowIndex + rowIndex, 2].ToString();
                        projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(projectcode);
                        if (projectModel == null)
                        {
                            rowIndex++;
                            continue;
                        }
                        else
                        {
                            returnModel.ProjectID = projectModel.ProjectId;
                            returnModel.ProjectCode = projectModel.ProjectCode;
                            if (obs[startRowIndex + rowIndex, 6].ToString().Trim() == projectModel.GroupName)
                            {
                                returnModel.DepartmentID = projectModel.GroupID;
                                returnModel.DepartmentName = projectModel.GroupName;
                                IsValidDept = true;
                            }
                            else
                            {
                                IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(projectModel.ProjectId, null, null);
                                foreach (ESP.Finance.Entity.SupporterInfo supModel in supporterList)
                                {
                                    if (obs[startRowIndex + rowIndex, 6].ToString().Trim() == supModel.GroupName)
                                    {
                                        returnModel.DepartmentID = supModel.GroupID;
                                        returnModel.DepartmentName = supModel.GroupName;
                                        IsValidDept=true;
                                    }
                                }
                            }
                        }
                        if (IsValidDept == false)
                        {
                            rowIndex++;
                            continue;
                        }
                        IsValidDept = false;

                        DateTime begindate = DateTime.Now;
                        try
                        {
                            begindate = Convert.ToDateTime(obs[startRowIndex + rowIndex, 3].ToString());
                        }
                        catch
                        {
                            begindate = DateTime.Now;
                        }
                        returnModel.PreBeginDate = begindate;
                        ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(obs[startRowIndex + rowIndex, 5].ToString());
                        
                        if (emp != null)
                        {
                            ESP.Compatible.Employee emp2 = new ESP.Compatible.Employee(emp.UserID);
                            returnModel.RequestorID = emp.UserID;
                            returnModel.RequestUserCode = emp.Code;
                            returnModel.RequestUserName = emp2.ITCode;
                            returnModel.RequestEmployeeName = emp2.Name;
                        }
                        else
                        {
                            rowIndex++;
                            continue;
                        }
                        string desc = string.Empty;
                        desc=obs[startRowIndex + rowIndex, 7] == null ? "" : obs[startRowIndex + rowIndex, 7].ToString();
                        desc+=obs[startRowIndex + rowIndex, 9] == null ? "" : obs[startRowIndex + rowIndex, 9].ToString();
                        returnModel.ReturnContent = desc;
                        returnModel.PRNo = obs[startRowIndex + rowIndex, 10] == null ? "" : obs[startRowIndex + rowIndex, 10].ToString();
                        if (obs[startRowIndex + rowIndex, 8] != null)
                        {
                            returnModel.PreFee = Convert.ToDecimal(obs[startRowIndex + rowIndex, 8].ToString());
                            returnModel.FactFee = Convert.ToDecimal(obs[startRowIndex + rowIndex, 8].ToString());
                        }
                        else
                        {
                            rowIndex++;
                            continue;
                        }
                        returnModel.SupplierName = SupplierName;
                        returnModel.SupplierBankName = SupplierBank;
                        returnModel.SupplierBankAccount = SupplierAccount;
                        returnModel.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                        returnModel.PaymentTypeID = 3;
                        returnModel.PaymentTypeName = "银行转帐";
                        string CostType = this.ddlCostType.SelectedItem.Text;
                        //机票、复印装订、快递、酒店
                        switch (CostType)
                        {
                            case "机票":
                                returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket;
                                break;
                            case "复印装订":
                                returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_Print;
                                break;
                            case "快递":
                                returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_Express;
                                break;
                            case "酒店":
                                returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_Hotel;
                                break;
                        }
                        rowIndex++;
                        returnList.Add(returnModel);
                    }
                    string tmpFileName = Server.MapPath("/") + "Templates\\ImportPNError" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Ticks.ToString("###") + ".xls";

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

                }
                catch(Exception ex)
                { }
                finally
                {
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
            
                }
            return returnList;
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                lblNo.Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (ViewState["ReturnList"] == null)
            {
                return;
            }
            List<ESP.Finance.Entity.ReturnInfo> returnList = (List<ESP.Finance.Entity.ReturnInfo>)ViewState["ReturnList"];
           int ret= ESP.Finance.BusinessLogic.ReturnManager.Add(returnList);
           if (ret > 0)
           {
               this.gvG.DataSource = null;
               this.gvG.DataBind();
               ViewState["ReturnList"] = null;
               this.ddlCostType.SelectedIndex = 0;
               ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('PN付款导入成功！');", true);
      
           }
           else
           {
               ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('PN付款导入失败！');", true);
      
           }
        }
    }
}
