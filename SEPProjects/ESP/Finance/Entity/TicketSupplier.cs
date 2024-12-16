using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class TicketSupplier
    {
        public int SupplierId { get; set; }
     /// <summary>
     /// 供应商名称
     /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacter { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 开户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string AccountNo { get; set; }
        /// <summary>
        /// 对应的楼层
        /// </summary>
        public string FloorNo { get; set; }
        /// <summary>
        /// 对应各楼层的前台，已逗号分割开
        /// </summary>
        public int ReceptionId { get; set; }
        public int SupplySupplierId { get; set; }
    }
}
