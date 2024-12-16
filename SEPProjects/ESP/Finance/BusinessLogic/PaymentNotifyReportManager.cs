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
     
     
    public static class PaymentNotifyReportManager
    {

        private static ESP.Finance.IDataAccess.IPaymentNotifyReportDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPaymentNotifyReportDataProvider>.Instance;}}
        //private const string _dalProviderName = "PaymentNotifyDALProvider";

        

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.PaymentNotifyReporterInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.PaymentNotifyReporterInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.PaymentNotifyReporterInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }


        /// <summary>
        /// 根据分公司名称取得下属分公司列表
        /// </summary>
        /// <param name="branchname">分公司名称</param>
        /// <returns>下属分公司列表</returns>
        private static IDictionary<int, string> GetDeparts(string branchname)
        {
            //根据分公司名称得到在部门表中的ID
            IList<ESP.Framework.Entity.DepartmentInfo> parentdep = ESP.Framework.BusinessLogic.DepartmentManager.Get(branchname);
            IDictionary<int, string> Departs = new Dictionary<int, string>();
            if (parentdep == null || parentdep.Count == 0) return Departs;
            //获取分公司下部门列表
            IList<ESP.Framework.Entity.DepartmentInfo> depList = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(parentdep[0].DepartmentID);

            foreach (ESP.Framework.Entity.DepartmentInfo dep in depList)
            {
                int id = dep.DepartmentID;
                string name = dep.DepartmentName;
                Departs.Add(id, name);
            }
            return Departs;
        }

        public static string GetPaymentReport(IList<ESP.Finance.Entity.PaymentNotifyReporterInfo> PaymentList, string BranchName)
        {
            int counter = 0;//计数器
            int lineoffset = 6;//行数索引

            string sourceFileName = "/Tmp/PaymentNotify/PaymentNotify.xls";
            string sourceFile = Common.GetLocalPath(sourceFileName);
            if (PaymentList == null || PaymentList.Count == 0) return sourceFileName;


            IDictionary<int, string> Departs = null;
            if (PaymentList[0].BranchID != null && PaymentList[0].BranchID.Value > 0)
            {
                Departs = GetDeparts(BranchName);
            }
            if (Departs == null || Departs.Count == 0) return sourceFileName;

            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

            ExcelHandle.WriteCell(excel.CurSheet,"A1",BranchName);

            //to do: 动态生成Fee\Cost Header

            //L6-9
            int feestartColindex = 12;//L
            int feestopColindex = (feestartColindex-1) + Departs.Count+2;

            int coststartColindex = feestopColindex +1;
            int coststopColindex = (coststartColindex-1) + Departs.Count +2;

            int coloffset = 0;

            System.Drawing.Color darkpink =  System.Drawing.Color.FromArgb(255,0,255);
            System.Drawing.Color lightgreen = System.Drawing.Color.FromArgb(204,255,204);

            
            


            lineoffset = 6;

            //set border
            ExcelHandle.SetBorderAll(excel.CurSheet, ExcelHandle.NtoL(feestartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex+5) + (lineoffset + 3).ToString());

            ExcelHandle.WriteAfterMerge(excel.CurSheet,ExcelHandle.NtoL(coststopColindex+1)+(lineoffset+1).ToString(),ExcelHandle.NtoL(coststopColindex+5)+(lineoffset+2).ToString() ,string.Empty);

            ExcelHandle.WriteCell(excel.CurSheet,ExcelHandle.NtoL(coststopColindex+1)+lineoffset.ToString(),"付款通知");
            ExcelHandle.WriteCell(excel.CurSheet, ExcelHandle.NtoL(coststopColindex + 1) + (lineoffset+3).ToString(), "金额合计");

            ExcelHandle.WriteCell(excel.CurSheet, ExcelHandle.NtoL(coststopColindex + 2) + lineoffset.ToString(), "付款通知");
            ExcelHandle.WriteCell(excel.CurSheet, ExcelHandle.NtoL(coststopColindex + 2) + (lineoffset + 3).ToString(), "金额差异");

            ExcelHandle.WriteCell(excel.CurSheet,ExcelHandle.NtoL(coststopColindex+3)+(lineoffset + 3).ToString(), "发票号码");

            ExcelHandle.WriteCell(excel.CurSheet, ExcelHandle.NtoL(coststopColindex + 4) + (lineoffset + 3).ToString(), "开票日期");

            ExcelHandle.WriteCell(excel.CurSheet, ExcelHandle.NtoL(coststopColindex + 5) + (lineoffset + 3).ToString(), "项目负责人");


            
            //fee
            ExcelHandle.SetBackGroundColor(excel.CurSheet, ExcelHandle.NtoL(feestartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(feestopColindex) + lineoffset.ToString(),darkpink);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(feestartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(feestopColindex) + lineoffset.ToString(), "Fee");

            //cost
            ExcelHandle.SetBackGroundColor(excel.CurSheet, ExcelHandle.NtoL(coststartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex) + lineoffset.ToString(), System.Drawing.Color.LimeGreen);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(coststartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex) + lineoffset.ToString(), "Cost");


            lineoffset = 7;

            //各个部门 set color
            ExcelHandle.SetBackGroundColor(excel.CurSheet,ExcelHandle.NtoL(feestartColindex)+lineoffset.ToString(),ExcelHandle.NtoL(feestopColindex-2)+(lineoffset+2).ToString(),System.Drawing.Color.Pink);
            ExcelHandle.SetBackGroundColor(excel.CurSheet, ExcelHandle.NtoL(coststartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex-2) + (lineoffset + 2).ToString(), System.Drawing.Color.LightGreen);

            //当月

            //fee当月
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(feestartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(feestopColindex-2) + lineoffset.ToString(), "Fee当月");
            //cost 当月
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(coststartColindex) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex-2) + lineoffset.ToString(), "Cost当月");

            //fee

            //defer fee
            ExcelHandle.SetBackGroundColor(excel.CurSheet, ExcelHandle.NtoL(feestopColindex - 1) + lineoffset.ToString(), ExcelHandle.NtoL(feestopColindex - 1) + (lineoffset + 2).ToString(), System.Drawing.Color.LightYellow);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(feestopColindex - 1) + lineoffset.ToString(), ExcelHandle.NtoL(feestopColindex - 1) + (lineoffset + 2).ToString(), "Defer Fee");
            //fee合计
            ExcelHandle.SetBackGroundColor(excel.CurSheet, ExcelHandle.NtoL(feestopColindex) + lineoffset.ToString(), ExcelHandle.NtoL(feestopColindex) + (lineoffset + 2).ToString(), darkpink);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(feestopColindex) + lineoffset.ToString(), ExcelHandle.NtoL(feestopColindex) + (lineoffset + 2).ToString(), "Fee 合计");


            //cost

            //defer cost
            ExcelHandle.SetBackGroundColor(excel.CurSheet, ExcelHandle.NtoL(coststopColindex - 1) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex - 1) + (lineoffset + 2).ToString(), System.Drawing.Color.LightBlue);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(coststopColindex - 1) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex - 1) + (lineoffset + 2).ToString(), "Defer Cost");
            //cost合计
            ExcelHandle.SetBackGroundColor(excel.CurSheet, ExcelHandle.NtoL(coststopColindex) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex) + (lineoffset + 2).ToString(), System.Drawing.Color.LimeGreen);
            ExcelHandle.WriteAfterMerge(excel.CurSheet, ExcelHandle.NtoL(coststopColindex) + lineoffset.ToString(), ExcelHandle.NtoL(coststopColindex) + (lineoffset + 2).ToString(), "Cost 合计");

            
            

            
            

            lineoffset = 8;//sun接口代码

            lineoffset = 9;//部门名称

            IDictionary<int,string> feeDepartCells = new Dictionary<int,string>();
            IDictionary<int,string> costDepartCells = new Dictionary<int,string>();
            foreach(int depid in Departs.Keys)
            {
                string feeCellName = ExcelHandle.NtoL(feestartColindex+coloffset) + lineoffset.ToString();
                string costCellName = ExcelHandle.NtoL(coststartColindex+coloffset) + lineoffset.ToString();

                ExcelHandle.WriteCell(excel.CurSheet, feeCellName, Departs[depid]);
                ExcelHandle.WriteCell(excel.CurSheet, costCellName, Departs[depid]);

                feeDepartCells.Add(depid,ExcelHandle.NtoL(feestartColindex + coloffset));
                costDepartCells.Add(depid, ExcelHandle.NtoL(coststartColindex + coloffset));

                coloffset++;
            }



            lineoffset = 10;
            counter = 0;
            foreach (PaymentNotifyReporterInfo model in PaymentList)
            {
                //编号 B
                if (!string.IsNullOrEmpty(model.PaymentCode))
                {
                    string paymentcode_cell = "B" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, paymentcode_cell, model.PaymentCode);
                }

                //日期 pre
                if (model.PaymentPreDate!=null && model.PaymentPreDate > new DateTime(1900,1,1))
                {
                    string paymentdate_cell = "C" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, paymentdate_cell, model.PaymentPreDate);
                }

                //客户名称 D
                if (!string.IsNullOrEmpty(model.CustFullNameCN))
                {
                    string custfullname_cell = "D" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, custfullname_cell, model.CustFullNameCN);
                }

                //项目号
                string projectcode = model.ProjectCode;
                if (!string.IsNullOrEmpty(projectcode))
                {
                    string projectcode_cell = "E" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, projectcode_cell, projectcode);
                }

                //公司代码
                if (!string.IsNullOrEmpty(model.BranchCode))
                {
                    string BranchCode = model.BranchCode;

                    string BranchCode_cell = "F" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, BranchCode_cell, BranchCode);
                }

                //客户简称
                if (!string.IsNullOrEmpty(model.CustShortEN))
                {
                    string CustShortEn = model.CustShortEN;

                    string CustShortEn_cell = "G" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, CustShortEn_cell, CustShortEn);
                }

                //项目类型
                if (!string.IsNullOrEmpty(model.ProjectTypeCode))
                {
                    string ProjectTypeCode = model.ProjectTypeCode;

                    string ProjectTypeCode_cell = "H" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, ProjectTypeCode_cell, ProjectTypeCode);
                }


                //年/月/流水号
                if (!string.IsNullOrEmpty(projectcode))
                {
                    string[] temp = projectcode.Split('-');
                    if (temp != null && temp.Length >= 4)
                    {
                        if (temp[3].Length >= 7)
                        {
                            string scode = "'" + temp[3].Substring(0, 7);

                            string YMS_cell = "I" + (lineoffset + counter).ToString();
                            ExcelHandle.WriteCell(excel.CurSheet, YMS_cell, scode);
                        }
                    }
                }

                //项目名称
                if (!string.IsNullOrEmpty(model.ProjectName))
                {
                    string ProjectName = model.ProjectName;

                    string ProjectName_cell = "J" + (lineoffset + counter).ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, ProjectName_cell, ProjectName);
                }


                //金额
                string paymentfee = model.PaymentFee.ToString("#,##0.00");



                //fee合计
                string express = "=SUM({0}{1}:{2}{1})";
                ExcelHandle.SetFormula(excel.CurSheet,ExcelHandle.NtoL(feestopColindex)+(lineoffset+counter).ToString(),string.Format(express,ExcelHandle.NtoL(feestartColindex),(lineoffset+counter).ToString(),ExcelHandle.NtoL(feestopColindex-1)));
                //cost合计
                ExcelHandle.SetFormula(excel.CurSheet, ExcelHandle.NtoL(coststopColindex) + (lineoffset + counter).ToString(), string.Format(express, ExcelHandle.NtoL(coststartColindex), (lineoffset + counter).ToString(), ExcelHandle.NtoL(coststopColindex - 1)));

                

                
                counter++;
            }

            string serverpath = Common.GetLocalPath("/Tmp/PaymentNotify");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/PaymentNotify/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            return desPath;
        }



        #endregion 获得数据列表
    }
}
