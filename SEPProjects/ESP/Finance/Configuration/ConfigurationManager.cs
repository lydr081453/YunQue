using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.Collections;

namespace ESP.Finance.Configuration
{
    public class ConfigurationManager
    {
        public static string ConnectionString
        {
            get { return ESP.Configuration.ConfigurationManager.SafeConnectionStrings["CustomerSqlConnection"].ConnectionString; }
        }

        public static string NewFinanceServer
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["NewFinanceServer"]; }
        }
        public static int  TreeRoot
        {
            get { return Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceTreeRoot"]); }
        }

        public static string BizAssemblyPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["BizAssemblyPath"]; }
        }
    
        /// <summary>
        /// default value is 50000
        /// </summary>
        public static string FinancialAmount
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["FinancialAmount"]; }
        }

        /// <summary>
        /// 项目可超出范围比例
        /// </summary>
        public static double ProjectAmountOverRate
        {
            get { return Convert.ToDouble( ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAmountOverRate"]); }
        }

        public static string CustomerAttachPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["CustomerAttachPath"]; }
        }

        public  static string FADeptID
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["FADeptID"]; }
        }

        public static IList<ESP.Framework.Entity.DepartmentInfo> GetFinanceDept()
        {
            IList < ESP.Framework.Entity.DepartmentInfo > DeptList= new List<ESP.Framework.Entity.DepartmentInfo>();

            string[] DeptS = FADeptID.Split(',');
            for (int i = 0; i < DeptS.Length; i++)
            {
                if (!string.IsNullOrEmpty(DeptS[i]))
                {
                    string[] deptModel = DeptS[i].Split('/');
                    ESP.Framework.Entity.DepartmentInfo d = new ESP.Framework.Entity.DepartmentInfo();
                    d.DepartmentID = Convert.ToInt32(deptModel[0]);
                    d.DepartmentName = deptModel[1];
                    DeptList.Add(d);
                }
            }
            return DeptList;
        }

        public static string MediaOrderPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MediaOrderPath"]; }
        }
        public static string ContractPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ContractPath"]; }
        }
        public static string ShunYaShortEN
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ShunYa"]; }
        }
        public static string AdvanceID
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["AdvanceID"]; }
        }
        /// <summary>
        /// DavidZhangID
        /// </summary>
        public static string DavidZhangID
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DavidZhangID"]; }
        }
        public static int DavidZhangAudit
        {
            get { return Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["DavidZhangAudit"]); }
        }

        public static string CAStatus
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["CAStatus"]; }
        }

        public static string FCAStatus
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["FCAStatus"]; }
        }

        public static string PurchaseServer
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"]; }
        }

        public static string PaymentNotify
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["PaymentNotify"]; }
        }

        public static string ProjectNotNeedAudit
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectNotNeedAudit"]; }
        }
        /// <summary>
        /// 来自采购T_Type表中的二级物料ID
        /// 正常项目：税金=总金额*税率
        /// 选择税率是广告费的项目：税金=（总金额-这些二级物料的成本金额）*税率
        /// </summary>
        public static string ExcludeTaxFee
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ExcludeTaxFee"]; }
        }


        public static List<int> ExcludeTaxFeeList
        {
            get {
                List<int> list = new List<int>();
                string ids = ExcludeTaxFee;
                if (!string.IsNullOrEmpty(ids.Trim()))
                { 
                    string[] tmp = ids.Trim().Split(',');
                    foreach (string i in tmp)
                    {
                        list.Add(Convert.ToInt32(i));
                    }
                }
                return list;
            }
        }
    }
}
