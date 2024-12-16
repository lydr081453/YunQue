using ESP.Purchase.Entity;
using System;
namespace ESP.Finance.Entity
{
	/// <summary>
	/// 实体类RebateRegistrationInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    /// 
    [Serializable]
    public partial class RebateRegistrationInfo
	{
        public RebateRegistrationInfo()
		{}
		#region Model
        public int Id { get; set; }

        /// <summary>
        /// 项目ID（非必填)
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 媒体ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// 返点金额
        /// </summary>
        public decimal RebateAmount { get; set; }

        /// <summary>
        /// 入账年月
        /// </summary>
        public string CreditedDate { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ESP.Finance.Utility.Common.RebateRegistration_Status Status { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }
		#endregion Model

        /// <summary>
        /// 批次ID
        /// </summary>
        public int BatchId { get; set; }

        /// <summary>
        /// 返点核算信息编号
        /// </summary>
        public string AccountingNum { get; set; }

        /// <summary>
        /// 结算类型
        /// </summary>
        public string SettleType { get; set; }

        /// <summary>
        /// 我方主体名称
        /// </summary>
        public string Branch { get; set; }
	}

    public partial class RebateRegistrationInfo
	{

        /// <summary>
        /// 项目信息（只读）
        /// </summary>
        public ProjectInfo Project
        {
            get
            {
                return ProjectId == null ? new ProjectInfo() { ProjectCode = "" } : ESP.Finance.BusinessLogic.ProjectManager.GetModel(ProjectId.Value);
            }
        }

        /// <summary>
        /// 媒体信息（只读）
        /// </summary>
        public SupplierInfo Supplier
        {
            get
            {
                return ESP.Purchase.BusinessLogic.SupplierManager.GetModel(SupplierId);
            }
        }

        /// <summary>
        /// 导入显示的错误信息
        /// </summary>
        public string ErrorContent { get; set; }
        /// <summary>
        /// 导入显示的行号
        /// </summary>
        public int RowNo { get; set; }
        /// <summary>
        /// 导入用 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 导入用 媒体名称
        /// </summary>
        public string SupplierName { get; set; }

        public string PurchaseBatchCode { get; set; }
    }
}

