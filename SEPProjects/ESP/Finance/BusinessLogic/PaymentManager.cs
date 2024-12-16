using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Linq;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类PaymentBLL 的摘要说明。
    /// </summary>


    public static class PaymentManager
    {
        //private readonly ESP.Finance.DataAccess.PaymentDAL dal=new ESP.Finance.DataAccess.PaymentDAL();

        private static ESP.Finance.IDataAccess.IPaymentDataProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IPaymentDataProvider>.Instance; } }
        //private const string _dalProviderName = "PaymentDALProvider";


        #region  成员方法

        public static string CreateCode(int paymentId)
        {
            if (paymentId == 0) return string.Empty;
            Entity.PaymentInfo model = GetModel(paymentId);
            if (model.BranchID == null || model.BranchID.Value == 0) return string.Empty;
            Entity.BranchInfo branch = BranchManager.GetModel(model.BranchID.Value);
            if (branch == null || string.IsNullOrEmpty(branch.DBCode) || branch.DBCode.Trim().Length < 3) return string.Empty;
            return DataProvider.CreateCode(branch.DBCode.Trim().Substring(0, 3));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>

        public static bool PaymentCodeExist(int paymentId, string paymentCode)
        {
            return DataProvider.PaymentCodeExist(paymentId, paymentCode);
        }

        public static int Add(ESP.Finance.Entity.PaymentInfo model)
        {
            return DataProvider.Add(model);
        }


        /// <summary>
        /// 增加数据
        /// </summary>
        public static int Add(List<ESP.Finance.Entity.PaymentInfo> models)
        {
            if (models == null || models.Count <= 0 || models[0] == null)
            {
                return 0;
            }
            int prjId = models[0].ProjectID;
            List<ESP.Finance.Entity.PaymentInfo> listcount = (List<ESP.Finance.Entity.PaymentInfo>)GetListByProject(prjId, null, null);
            if (listcount != null && listcount.Count > 0)
            {
                if (listcount.Exists(n => n.PaymentStatus != (int)Utility.ReturnStatus.Save))//如果有一条状态不为0的则不插入
                {
                    return 0;
                }
            }
            int counter = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //删除此项目的payment
                    DataProvider.DeleteByProject(prjId, trans);
                    foreach (Entity.PaymentInfo model in models)
                    {
                        int ret = DataProvider.Add(model, trans);
                        if (ret > 0)
                        {
                            counter++;
                        }
                        else
                        {
                            trans.Rollback();
                            return 0;//只要有一条出错就rollback
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
            return counter;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>



        public static UpdateResult Update(ESP.Finance.Entity.PaymentInfo model)
        {
            return Update(model, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>



        public static UpdateResult Update(ESP.Finance.Entity.PaymentInfo model, Entity.InvoiceInfo invoice)
        {
            int res = 0;
            try
            {
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }

            if (res > 0)
            {

                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
        }
        public static int UpdatePaymentBankInfo(int projectId,int bankId)
        {
            return DataProvider.UpdatePaymentBankInfo(projectId, bankId);
        }

        /// <summary>
        /// 根据项目ID更新项目号
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectCode"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>


        public static ESP.Finance.Utility.UpdateResult UpdateProjectCode(int projectId, string projectCode)
        {
            int res = 0;
            try
            {
                //trans//res = DataProvider.UpdateProjectCode(projectId, projectCode, true);
                res = DataProvider.UpdateProjectCode(projectId, projectCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateResult.Failed;
            }
            if (res > 0)
            {
                return UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return UpdateResult.UnExecute;
            }
            return UpdateResult.Failed;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int PaymentID)
        {

            int res = 0;
            try
            {
                res = DataProvider.Delete(PaymentID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return DeleteResult.Failed;
            }
            if (res > 0)
            {
                return DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return DeleteResult.UnExecute;
            }
            return DeleteResult.Failed;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.PaymentInfo GetModel(int PaymentID)
        {

            return DataProvider.GetModel(PaymentID);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<PaymentInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<PaymentInfo> GetList(string term)
        {
            return GetList(term, null);
        }

        public static int UpdatePaymentConfirmMonth(int projectId, int year, int month)
        {
            return DataProvider.UpdatePaymentConfirmMonth(projectId, year, month);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<PaymentInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.PaymentInfo> GetListByProject(int projectID, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetListByProject(projectID, term, param);
        }
        #endregion 获得数据列表

        public static IList<PaymentInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            return DataProvider.GetWaitAuditList(userIds, strTerms, parms);
        }
        #endregion  成员方法

        public static IList<PaymentInfo> GetWaitAuditList(int[] userIds)
        {
            return DataProvider.GetWaitAuditList(userIds);
        }


        public static string ExportPaymentDetail(int paymentId, System.Web.HttpResponse response)
        {
            ESP.Finance.Entity.PaymentInfo paymentModel = ESP.Finance.BusinessLogic.PaymentManager.GetModel(paymentId);
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(paymentModel.ProjectID);

            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModel(projectModel.BranchID.Value);
            IList<ESP.Finance.Entity.PaymentDetailInfo> detailList = ESP.Finance.BusinessLogic.PaymentDetailManager.GetList(" paymentid=" + paymentId.ToString());

            int bankId = 0;
            if (projectModel.BankId != 0)
            {
                bankId = projectModel.BankId;
            }
            if (paymentModel.BankID != null && paymentModel.BankID.Value != 0)
            {
                bankId = paymentModel.BankID.Value;
            }
            ESP.Finance.Entity.BankInfo bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(bankId);

            if (detailList == null)
            {
                detailList = new List<ESP.Finance.Entity.PaymentDetailInfo>();
            }
            if (detailList.Count == 0)
            {
                ESP.Finance.Entity.PaymentDetailInfo item = new PaymentDetailInfo();
                item.PaymentContent = paymentModel.PaymentContent;
                item.PaymentPreAmount = paymentModel.PaymentBudgetConfirm.Value;
                detailList.Add(item);
            }
            string temppath = "/Tmp/Salary/PaymentDetailTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int cellBegin = 21;
            int cellCount = 0;
            if (!string.IsNullOrEmpty(paymentModel.InvoiceTitle))
                ExcelHandle.WriteCell(excel.CurSheet, "A8", paymentModel.InvoiceTitle);

            ExcelHandle.WriteCell(excel.CurSheet, "F10", paymentModel.PaymentPreDate.ToString("yyyy-MM-dd"));
            ExcelHandle.WriteCell(excel.CurSheet, "F12", paymentModel.PaymentCode);
            //Job No 
            ExcelHandle.WriteCell(excel.CurSheet, "A16", "Job No " + projectModel.ProjectCode);


            #region 写明细
            foreach (PaymentDetailInfo item in detailList)
            {
                cellCount++;
                ExcelHandle.WriteCell(excel.CurSheet, "A" + cellBegin, cellCount);
                ExcelHandle.WriteCell(excel.CurSheet, "B" + cellBegin, item.PaymentContent);
                ExcelHandle.WriteCell(excel.CurSheet, "F" + cellBegin, item.PaymentPreAmount.ToString("#,##0.00"));
                cellBegin++;
            }
            #endregion


            if (cellBegin < 37)
            {

                ExcelHandle.SetBorderBottom(excel.CurSheet, "A37", "D37");
                ExcelHandle.SetBorderBottom(excel.CurSheet, "F37");

                cellBegin = 37;
            }
            else
            {
                ExcelHandle.SetBorderBottom(excel.CurSheet, "A" + cellBegin, "D" + cellBegin);
                ExcelHandle.SetBorderBottom(excel.CurSheet, "F" + cellBegin);

            }
            cellBegin = cellBegin + 2;

            ExcelHandle.WriteCell(excel.CurSheet, "D" + (cellBegin).ToString(), "总额:");
            ExcelHandle.SetFormula(excel.CurSheet, "F" + (cellBegin).ToString(), "=ROUND(SUM(F21:F" + (cellBegin - 1).ToString() + "),2)");

            ExcelHandle.SetBold(excel.CurSheet, "D" + (cellBegin).ToString());
            ExcelHandle.SetBorderAll(excel.CurSheet, "F" + (cellBegin).ToString());

            cellBegin = cellBegin + 2;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + cellBegin, "备注: 请在收到帐单日即日付款,并将款项用电汇方式付到如下帐号:");

            cellBegin = cellBegin + 2;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + cellBegin, bankModel == null ? "" : "开户名称:" + bankModel.BankAccountName);
            cellBegin++;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + cellBegin, bankModel == null ? "" : "帐号:" + bankModel.BankAccount);
            cellBegin++;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + cellBegin, bankModel == null ? "" : "银行名称:" + bankModel.BankName);
            cellBegin++;
            ExcelHandle.WriteCell(excel.CurSheet, "A" + cellBegin, bankModel == null ? "" : "银行地址:" + bankModel.Address);

            ExcelHandle.WriteCell(excel.CurSheet, "D" + cellBegin, branchModel == null ? "" : branchModel.BranchName);
            cellBegin++;
            ExcelHandle.WriteCell(excel.CurSheet, "D" + cellBegin, "代表签字");

            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static string ExportPaymentList(string dtsql,bool isFinance, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/Salary/PaymentListTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            int cellBegin = 9;
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            excel.CurSheet.Activate();
            decimal totalAR = 0;

            #region "写应收明细"
            DataTable dt = GetPaymentReportList(dtsql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //付款通知号码
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (cellBegin).ToString(), dt.Rows[i][2].ToString());
                //付款通知日期
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (cellBegin).ToString(), dt.Rows[i][3].ToString());
                //客户名称
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (cellBegin).ToString(), dt.Rows[i][4].ToString().Trim());
                //项目号
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (cellBegin).ToString(), dt.Rows[i][5].ToString());
                //项目名称
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (cellBegin).ToString(), dt.Rows[i][6].ToString());
                //账期描述
                ExcelHandle.WriteCell(excel.CurSheet, "G" + (cellBegin).ToString(), dt.Rows[i][7].ToString());
                //帐龄(天数)
                ExcelHandle.WriteCell(excel.CurSheet, "H" + (cellBegin).ToString(), dt.Rows[i][8].ToString());
                //项目组
                ExcelHandle.WriteCell(excel.CurSheet, "I" + (cellBegin).ToString(), dt.Rows[i][11].ToString());

                //180
                ExcelHandle.WriteCell(excel.CurSheet, "J" + (cellBegin).ToString(), dt.Rows[i][30].ToString());
                // 181到365天 
                ExcelHandle.WriteCell(excel.CurSheet, "K" + (cellBegin).ToString(), dt.Rows[i][31].ToString());
                // 366到730天 
                ExcelHandle.WriteCell(excel.CurSheet, "L" + (cellBegin).ToString(), dt.Rows[i][32].ToString());
                //730天以上
                ExcelHandle.WriteCell(excel.CurSheet, "M" + (cellBegin).ToString(), dt.Rows[i][33].ToString());

                //付款通知金额(RMB)
                ExcelHandle.WriteCell(excel.CurSheet, "N" + (cellBegin).ToString(), dt.Rows[i][29].ToString());

                decimal paymentBudgetConfirm = decimal.Parse(dt.Rows[i][29].ToString());
                totalAR += paymentBudgetConfirm;

               
                //负责人
                ExcelHandle.WriteCell(excel.CurSheet, "O" + (cellBegin).ToString(), dt.Rows[i][13].ToString());
                
                //银行
                ExcelHandle.WriteCell(excel.CurSheet, "P" + (cellBegin).ToString(), dt.Rows[i][15].ToString());
                //备注
                ExcelHandle.WriteCell(excel.CurSheet, "Q" + (cellBegin).ToString(), dt.Rows[i][14].ToString());
                //发票抬头
                ExcelHandle.WriteCell(excel.CurSheet, "R" + (cellBegin).ToString(), dt.Rows[i][16].ToString());
                //开票日期
                ExcelHandle.WriteCell(excel.CurSheet, "S" + (cellBegin).ToString(), dt.Rows[i][17].ToString());
                //发票号
                ExcelHandle.WriteCell(excel.CurSheet, "T" + (cellBegin).ToString(), dt.Rows[i][18].ToString());
                //发票金额
                ExcelHandle.WriteCell(excel.CurSheet, "U" + (cellBegin).ToString(), dt.Rows[i][19].ToString());
                //发票领用人
                ExcelHandle.WriteCell(excel.CurSheet, "V" + (cellBegin).ToString(), dt.Rows[i][21].ToString());
                //签收单
                ExcelHandle.WriteCell(excel.CurSheet, "W" + (cellBegin).ToString(), dt.Rows[i][22].ToString());
                //确认收入状态
                string confirmRemark = string.Empty;
                if ((dt.Rows[i][34] != DBNull.Value && dt.Rows[i][34].ToString() != "0") && (dt.Rows[i][35] != DBNull.Value && dt.Rows[i][35].ToString() != "0"))
                {
                    confirmRemark = dt.Rows[i][34].ToString() + "年" + dt.Rows[i][35].ToString()+"月确认收入";
                    ExcelHandle.WriteCell(excel.CurSheet, "X" + (cellBegin).ToString(), confirmRemark);
                }
                
                //回款日期
                ExcelHandle.WriteCell(excel.CurSheet, "Y" + (cellBegin).ToString(), dt.Rows[i][24].ToString());
                //回款金额
                if (dt.Rows[i][25] != DBNull.Value && dt.Rows[i][25].ToString() != "0.0000")
                    ExcelHandle.WriteCell(excel.CurSheet, "Z" + (cellBegin).ToString(), dt.Rows[i][25].ToString());
                //回款金额（外币）
                if (dt.Rows[i][26] != DBNull.Value && dt.Rows[i][26].ToString() != "0.0000")
                    ExcelHandle.WriteCell(excel.CurSheet, "AA" + (cellBegin).ToString(), dt.Rows[i][26].ToString() + "(" + dt.Rows[i][27].ToString() + ")");
                //汇兑损益（回款）
                if (dt.Rows[i][28] != DBNull.Value && dt.Rows[i][28].ToString() != "0.0000")
                    ExcelHandle.WriteCell(excel.CurSheet, "AB" + (cellBegin).ToString(), dt.Rows[i][28].ToString());
                //
                if (isFinance)
                {
                    if (dt.Rows[i][36].ToString().Trim() == "1")
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "AC" + (cellBegin).ToString(), "法务负责");
                    }
                    if (dt.Rows[i][37].ToString().Trim() == "1")
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "AD" + (cellBegin).ToString(), "内部关联公司");
                    }
                }
                cellBegin++;
            }
            #endregion

            #region "按部门检索"
            cellBegin = 6;
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[2];
            DataTable dtGroup = GetPaymentReportByGroup(dtsql);
            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {
                //项目组
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (cellBegin).ToString(), dtGroup.Rows[i][0].ToString());
                //180天内
                decimal total180 = decimal.Parse(dtGroup.Rows[i][1].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (cellBegin).ToString(), total180.ToString("#,##0.00"));
                //181到365天
                decimal total365 = decimal.Parse(dtGroup.Rows[i][2].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (cellBegin).ToString(), total365.ToString("#,##0.00"));
                //366到730天
                decimal total730 = decimal.Parse(dtGroup.Rows[i][3].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (cellBegin).ToString(), total730.ToString("#,##0.00"));
                //超过731天
                decimal totalOthers = decimal.Parse(dtGroup.Rows[i][4].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (cellBegin).ToString(), totalOthers.ToString("#,##0.00"));
                //金额(RMB)
                decimal subtotal = decimal.Parse(dtGroup.Rows[i][5].ToString()); ;
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (cellBegin).ToString(), subtotal.ToString("#,##0.00"));

                //G空一列
                //H开始计算%
                ExcelHandle.SetFormula(excel.CurSheet, "H" + (cellBegin).ToString(), "=B" + cellBegin + "/$F" + cellBegin);

                ExcelHandle.SetFormula(excel.CurSheet, "I" + (cellBegin).ToString(), "=C" + cellBegin + "/$F" + cellBegin);

                ExcelHandle.SetFormula(excel.CurSheet, "J" + (cellBegin).ToString(), "=D" + cellBegin + "/$F" + cellBegin);

                ExcelHandle.SetFormula(excel.CurSheet, "K" + (cellBegin).ToString(), "=E" + cellBegin + "/$F" + cellBegin);

                decimal subPercent = 0;
                if (totalAR != 0)
                    subPercent = subtotal / totalAR;
                ExcelHandle.WriteCell(excel.CurSheet, "L" + (cellBegin).ToString(), subPercent.ToString());

                cellBegin++;
            }

            if (dtGroup.Rows.Count > 0)
            {
                //写部门合计
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (cellBegin).ToString(), "总计：");
                ExcelHandle.SetFormula(excel.CurSheet, "B" + (cellBegin).ToString(), "=ROUND(SUM(B6:B" + (cellBegin - 1).ToString() + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "C" + (cellBegin).ToString(), "=ROUND(SUM(C6:C" + (cellBegin - 1).ToString() + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "D" + (cellBegin).ToString(), "=ROUND(SUM(D6:D" + (cellBegin - 1).ToString() + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "E" + (cellBegin).ToString(), "=ROUND(SUM(E6:E" + (cellBegin - 1).ToString() + "),2)");
                ExcelHandle.SetFormula(excel.CurSheet, "F" + (cellBegin).ToString(), "=ROUND(SUM(F6:F" + (cellBegin - 1).ToString() + "),2)");
                //=B24/$F24
                ExcelHandle.SetFormula(excel.CurSheet, "H" + (cellBegin).ToString(), "=B" + cellBegin + "/$F" + cellBegin);
                ExcelHandle.SetFormula(excel.CurSheet, "I" + (cellBegin).ToString(), "=C" + cellBegin + "/$F" + cellBegin);
                ExcelHandle.SetFormula(excel.CurSheet, "J" + (cellBegin).ToString(), "=D" + cellBegin + "/$F" + cellBegin);
                ExcelHandle.SetFormula(excel.CurSheet, "K" + (cellBegin).ToString(), "=E" + cellBegin + "/$F" + cellBegin);
                //
                ExcelHandle.SetFormula(excel.CurSheet, "L" + (cellBegin).ToString(), "1");
            }
            #endregion

            #region "按客户检索"
            cellBegin = 2;
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[3];

            excel.CurSheet.Activate();

            DataTable dtCustomer = GetPaymentReportByCustomer(dtsql);

            for (int i = 0; i < dtCustomer.Rows.Count; i++)
            {
                string custName = dtCustomer.Rows[i][0].ToString().Trim();
                //客户名称
                ExcelHandle.WriteCell(excel.CurSheet, "A" + (cellBegin).ToString(), custName);
                //180天内
                decimal total30 = decimal.Parse(dtCustomer.Rows[i][1].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "B" + (cellBegin).ToString(), total30.ToString("#,##0.00"));
                //180到365天
                decimal total60 = decimal.Parse(dtCustomer.Rows[i][2].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "C" + (cellBegin).ToString(), total60.ToString("#,##0.00"));
                //365到730天
                decimal total90 = decimal.Parse(dtCustomer.Rows[i][3].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "D" + (cellBegin).ToString(), total90.ToString("#,##0.00"));
                //超过730天
                decimal total120 = decimal.Parse(dtCustomer.Rows[i][4].ToString());
                ExcelHandle.WriteCell(excel.CurSheet, "E" + (cellBegin).ToString(), total120.ToString("#,##0.00"));
                //金额(RMB)
                decimal subtotal = total30 + total60 + total90 + total120;
                ExcelHandle.WriteCell(excel.CurSheet, "F" + (cellBegin).ToString(), subtotal.ToString("#,##0.00"));
                //占比
                decimal totalPercent = 0;
                if (totalAR != 0)
                    totalPercent = (subtotal / totalAR);
                ExcelHandle.WriteCell(excel.CurSheet, "G" + (cellBegin).ToString(), totalPercent.ToString());
                cellBegin++;
            }
            #endregion

            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static string ExportPaymentListByMonth3(string groupids, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/Salary/PaymentListByMonthTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();
            int cellTotalBegin = 3;
            int cellBegin = 3;
            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];

            #region "写应收明细"
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddMonths(2);
            decimal totalPayment = 0;



            while (dtStart <= dtEnd)
            {
                DataTable dt = GetPaymentReportListByMonth(groupids, dtStart.Year, dtStart.Month);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExcelHandle.WriteCell(excel.CurSheet, "A" + (cellBegin).ToString(), dt.Rows[i]["level3"].ToString());
                        //月份
                        ExcelHandle.WriteCell(excel.CurSheet, "B" + (cellBegin).ToString(), dtStart.Year + "年" + dtStart.Month + "月");
                        //客户
                        ExcelHandle.WriteCell(excel.CurSheet, "C" + (cellBegin).ToString(), dt.Rows[i]["customer"].ToString());
                        //项目号
                        ExcelHandle.WriteCell(excel.CurSheet, "D" + (cellBegin).ToString(), dt.Rows[i]["ProjectCode"].ToString());
                        //项目名称
                        ExcelHandle.WriteCell(excel.CurSheet, "E" + (cellBegin).ToString(), dt.Rows[i]["BusinessDescription"].ToString());
                        //回款金额
                        decimal payment = Decimal.Parse(dt.Rows[i]["PaymentBudgetConfirm"].ToString());
                        totalPayment += payment;
                        ExcelHandle.WriteCell(excel.CurSheet, "F" + (cellBegin).ToString(), payment.ToString("#,##0.00"));
                        cellBegin++;
                    }
                    //回款金额小计
                    ExcelHandle.WriteCell(excel.CurSheet, "A" + (cellBegin).ToString(), dtStart.Year + "年" + dtStart.Month + "月小计");
                    ExcelHandle.SetFormula(excel.CurSheet, "F" + (cellBegin).ToString(), "=ROUND(SUM(F" + cellTotalBegin + ":F" + (cellBegin - 1) + "),2)");
                    ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (cellBegin).ToString(), "F" + (cellBegin).ToString(), System.Drawing.Color.Orange);

                    cellBegin++;
                    cellTotalBegin = cellBegin;
                }
                dtStart = dtStart.AddMonths(1);
            }

            //回款金额总计
            ExcelHandle.WriteCell(excel.CurSheet, "A" + (cellBegin).ToString(), "总计");
            ExcelHandle.WriteCell(excel.CurSheet, "F" + (cellBegin).ToString(), totalPayment.ToString("#,##0.00"));
            ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + (cellBegin).ToString(), "F" + (cellBegin).ToString(), System.Drawing.Color.Green);

            ExcelHandle.WriteCell(excel.CurSheet, "A1", DateTime.Now.Year + "年" + DateTime.Now.Month + "月 - " + dtEnd.Year + "年" + dtEnd.Month + "月预计收款");

            #endregion

            string serverpath = Common.GetLocalPath("/Tmp/Salary");
            if (!System.IO.Directory.Exists(serverpath))
                System.IO.Directory.CreateDirectory(serverpath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = serverpath + "/" + desFilename;
            string desPath = "/Tmp/Salary/" + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();
            ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            return desPath;
        }

        public static DataTable GetPaymentReportList(string term)
        {
            return DataProvider.GetPaymentReportList(term);
        }

        public static DataTable GetPaymentReportListByMonth(string groupids, int year, int month)
        {
            return DataProvider.GetPaymentReportListByMonth(groupids, year, month);
        }

        public static DataTable GetPaymentReportByGroup(string term)
        {
            return DataProvider.GetPaymentReportByGroup(term);
        }

        public static DataTable GetPaymentReportByCustomer(string term)
        {
            return DataProvider.GetPaymentReportByCustomer(term);
        }
    }
}

