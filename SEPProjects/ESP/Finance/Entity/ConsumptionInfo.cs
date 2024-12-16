using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class ConsumptionInfo
    {
        public ConsumptionInfo()
        { }
        /// <summary>
        /// Auto ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 导入批次的ID
        /// </summary>
        public int BatchId { get; set; }
        /// <summary>
        /// 费用发生年月
        /// </summary>
        public string OrderYM { get; set; }

        public int ProjectId { get; set; }
        /// <summary>
        /// 关联项目号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 费用描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 发生金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 媒体主体
        /// </summary>
        public string Media { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 导入时记录错误问题
        /// </summary>
        public string ErrorContent { get; set; }

        /// <summary>
        /// JS编号来自媒体主体平台
        /// </summary>
        public string JSCode { get; set; }
        /// <summary>
        /// XM编号来自媒体主体平台
        /// </summary>
        public string XMCode { get; set; }
        /// <summary>
        /// 对应excel的行号，便于检查
        /// </summary>
        public int RowNo { get; set; }

        /// <summary>
        /// 关联付款申请ID
        /// </summary>
        public int? ReturnId { get; set; }

        public string PurchaseBatchCode { get; set; }

    }
}
