
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public partial class ApplyForInvioceInfo
    {
        public int Id { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        public int Status { get; set; }
        public int CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal InviocePrice { get; set; }

        /// <summary>
        /// 媒体ID
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// 流向
        /// </summary>
        public ESP.Finance.Utility.Common.ApplyForInvioce_FlowTo FlowTo { get; set; }

        /// <summary>
        /// 发票类型
        /// </summary>
        public ESP.Finance.Utility.Common.Invoice_Type InvoiceType { get; set; }

        /// <summary>
        /// 开票单位名称
        /// </summary>
        public string InvoiceTitle { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string BankNum { get; set; }

        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string TIN { get; set; }

        /// <summary>
        /// 开户地址及电话
        /// </summary>
        public string AddressPhone { get; set; }
    }

    public partial class ApplyForInvioceInfo
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
                    var media = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(SupplierId.Value);
                    if (media != null)
                        return media.supplier_name;
                }
                return "";
            }
        }
    }
}
