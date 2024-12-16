using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using Excel = Microsoft.Office.Interop.Excel;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using System.Data.SqlClient;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using System.Text;
namespace ESP.Finance.BusinessLogic
{
    /// <summary>
    /// 业务逻辑类 ExpressScan 的摘要说明。
    /// </summary>
    public static class ExpressCheckManager
    {
        private static ESP.Finance.IDataAccess.IExpressCheckProvider DataProvider { get { return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.IExpressCheckProvider>.Instance; } }

        #region  成员方法


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ESP.Finance.Entity.ExpressCheckInfo model)
        {
            return DataProvider.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static UpdateResult Update(ESP.Finance.Entity.ExpressCheckInfo model)
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static DeleteResult Delete(int id)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(id);
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

        public static DeleteResult Delete(int year, int month)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(year, month);
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
        public static ESP.Finance.Entity.ExpressCheckInfo GetModel(int id)
        {

            return DataProvider.GetModel(id);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ExpressCheckInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ExpressCheckInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ExpressCheckInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }
        #endregion 获得数据列表
        #endregion  成员方法


        public static string ExportOrders(int year, int month, System.Web.HttpResponse response)
        {
            string temppath = "/Tmp/Salary/ExpressCheckTemplate.xls";
            string sourceFile = Common.GetLocalPath(temppath);
            ExcelHandle excel = new ExcelHandle();

            excel.Load(sourceFile);
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            int rownum = 2;
            string cell = "A2";

            IList<ESP.Finance.Entity.ExpressCheckInfo> checkList = ESP.Finance.BusinessLogic.ExpressCheckManager.GetList(" expYear=" + year + " and expMonth=" + month);

            #region 数据写入
            if (checkList == null || checkList.Count == 0)
                return "";

            foreach (ESP.Finance.Entity.ExpressCheckInfo chk in checkList)
            {
                //组织架构	费用发生日期	申请人	员工编号	费用描述	快递金额	包装费	保价费	单号

                DataTable emptb = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetListForExpress(" and (b.LastNameCN+b.FirstNameCN)='" + chk.Sender + "'").Tables[0];

                ESP.Finance.Entity.ExpressScanInfo scan = ESP.Finance.BusinessLogic.ExpressScanManager.GetModel(chk.ExpressNo);

                if (emptb != null && emptb.Rows.Count > 0)
                {
                    //组织架构
                    cell = string.Format("A{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, emptb.Rows[0]["GroupName"].ToString());
                    //员工编号
                    cell = string.Format("D{0}", rownum);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, "'"+emptb.Rows[0]["code"].ToString());
                }
                //费用发生日期
                cell = string.Format("B{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, chk.SendTime.ToString("yyyy-MM-dd"));
                //申请人
                cell = string.Format("C{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, chk.Sender);

                string desc = string.Empty;

                if (scan != null)
                {
                    desc = scan.Company + ":" + chk.City;
                }
                else
                {
                    desc = "未找到扫描单号:" + chk.City;
                }
                //费用描述
                cell = string.Format("E{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, desc);
                //快递金额
                cell = string.Format("F{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, chk.ExpPrice.ToString("f2"));
                //	包装费	
                cell = string.Format("G{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, chk.PackPrice.ToString("f2"));
                //保价费
                cell = string.Format("H{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, chk.InsureFee.ToString("f2"));

                //单号
                cell = string.Format("I{0}", rownum);
                ExcelHandle.SetFormula(excel.CurSheet, cell, "'" + chk.ExpressNo);

                rownum++;
            }
            #endregion


            rownum += 5;

            cell = string.Format("A{0}", rownum);
            ExcelHandle.WriteCell(excel.CurSheet, cell, "已扫描未登记单号：");
            rownum += 2;

            IList<ESP.Finance.Entity.ExpressScanInfo> scanList = ESP.Finance.BusinessLogic.ExpressScanManager.GetList(" expYear=" + year + " and expMonth=" + month + " and expressno not in(select expressno from f_expresscheck where expYear=" + year + " and expMonth=" + month + ")");

            foreach (ESP.Finance.Entity.ExpressScanInfo scan in scanList)
            {
                cell = string.Format("I{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + scan.ExpressNo);
                rownum++;
            }

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

    }
}
