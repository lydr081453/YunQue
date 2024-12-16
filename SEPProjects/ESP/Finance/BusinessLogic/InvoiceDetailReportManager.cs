using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class InvoiceDetailReportManager
    {

        private static ESP.Finance.IDataAccess.IInvoiceDetailReportDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IInvoiceDetailReportDataProvider>.Instance;}}
        //private const string _dalProviderName = "InvoiceDetailDALProvider";

        

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.InvoiceDetailReporterInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.InvoiceDetailReporterInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.InvoiceDetailReporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static string GetInvoiceReport(IList<ESP.Finance.Entity.InvoiceDetailReporterInfo> InvoiceList,string BranchName,string BeginDate,string EndDate,System.Web.HttpResponse response)
        {
            int counter = 0;//计数器
            int lineoffset = 4;//行数索引
            decimal TotalAmount=0;
            string sourceFileName = "/Tmp/Invoice/InvoiceReports.xls";
            string sourceFile = Common.GetLocalPath(sourceFileName);
            if (InvoiceList == null || InvoiceList.Count == 0) return sourceFileName;
            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

            ExcelHandle.WriteCell(excel.CurSheet, "B1", BranchName);
            ExcelHandle.WriteCell(excel.CurSheet, "B2", "发票登记记录(" + BeginDate + "~"+EndDate+")");

            foreach (InvoiceDetailReporterInfo model in InvoiceList)
            {
                string InvoiceNo_Cell = "B" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, InvoiceNo_Cell, model.InvoiceNo);
                string InvoiceDate_Cell = "C" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, InvoiceDate_Cell, model.CreateDate.Value.ToString("yyyy-MM-dd"));
                string InvoiceAmount_Cell = "D" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, InvoiceAmount_Cell, model.Amounts.Value.ToString("#,##0.00"));
                string USDDiffer_Cell = "E" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, USDDiffer_Cell, model.USDDiffer.Value.ToString("#,##0.00"));
                string Customer_Cell = "F" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, Customer_Cell, model.CustomerName);
                string ProjectCode_Cell = "H" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, ProjectCode_Cell, model.ProjectCode);
                //S-ZJKJ-E-202310073-D
                ExcelHandle.WriteCell(excel.CurSheet, "I"+(lineoffset + 1).ToString(), model.ProjectCode.Substring(0,1));
                ExcelHandle.WriteCell(excel.CurSheet, "J" + (lineoffset + 1).ToString(), model.ProjectCode.Substring(2, 4));
                ExcelHandle.WriteCell(excel.CurSheet, "K" + (lineoffset + 1).ToString(), model.ProjectCode.Substring(7, 1));
                ExcelHandle.WriteCell(excel.CurSheet, "L" + (lineoffset + 1).ToString(), model.ProjectCode.Substring(9));
                string Remark_Cell = "M" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, Remark_Cell, model.Remark);
                string PaymentCode_Cell = "N" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, PaymentCode_Cell, model.PaymentCode);
                string Description_Cell = "O" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, Description_Cell, model.Description);
                string Responser_Cell = "P" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, Responser_Cell, model.ResponseEmployeeName);
                lineoffset ++;
                counter++;
                TotalAmount+=model.Amounts.Value;
            }
            ExcelHandle.WriteCell(excel.CurSheet, "B"+(lineoffset+3).ToString(), BeginDate+"~"+EndDate+"开票合计:");
            ExcelHandle.WriteCell(excel.CurSheet, "D" + (lineoffset + 3).ToString(), TotalAmount.ToString("#,##0.00"));

            string serverpath = Common.GetLocalPath("/Tmp/Invoice");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Invoice/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(serverpath + desPath, desFilename, response);
            return desPath;
        }


        #endregion 获得数据列表
    }
}
