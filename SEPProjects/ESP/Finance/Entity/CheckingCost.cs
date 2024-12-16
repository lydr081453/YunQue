using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class CheckingCost
    {
        public string ID { get;set;}
        public string PRNo {get;set;}
        public string Requestor{get;set;}
        public string GroupName { get; set; }
        public DateTime AppDate { get; set; }
        public string AppAmount { get; set; }
        public int PrType { get; set; }
        public string PNTotal { get; set; }
        public string Description { get; set; }
        public int TypeID { get { return MaterialType.TypeID; } }
        public string TypeName { get { return MaterialType.TypeName; } }
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string SupplierName { get; set; }
        /// <summary>
        /// 项目号内成本项申请的预算
        /// </summary>
        public string CostPreAmount { get { return MaterialType.CostPreAmount.ToString("#,##0.00"); } }
        /// <summary>
        /// 某一成本项已经使用的总额
        /// </summary>
        public string TypeTotalAmount { get { return MaterialType.TypeTotalAmount.ToString("#,##0.00"); } }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        /// <summary>
        ///单据已支付金额 
        /// </summary>
        public string PaidAmount {get;set; }
        /// <summary>
        /// 单据未支付金额
        /// </summary>
        public string UnPaidAmount { get; set; }
        public int ReturnType { get; set; }
        public MaterialType MaterialType { get; set; }
    }

    public class MaterialType
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public decimal CostPreAmount { get; set; }
        public decimal TypeTotalAmount { get; set; }
    }
}
