using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class ProjectMediaInfo
    {
        public int Id { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 供应商（媒体）ID
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// 预估媒体成本比例
        /// </summary>
        public decimal CostRate { get; set; }

        /// <summary>
        /// 项目充值金额
        /// </summary>
        public decimal Recharge { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
    }

    public partial class ProjectMediaInfo
    {
        /// <summary>
        /// 媒体付款主体名称(只读)
        /// </summary>
        public string MediaName
        {
            get
            {
                if (SupplierId != null)
                {
                    var media = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(SupplierId);
                    if (media != null)
                        return media.supplier_name;
                }
                return "";
            }
        }
    }
}
