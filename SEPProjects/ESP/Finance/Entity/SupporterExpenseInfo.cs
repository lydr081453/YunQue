using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类SupporterExpenseInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SupporterExpenseInfo
    {
        public SupporterExpenseInfo()
        { }
        #region Model
        private int _supporterexpenseid;
        private int? _supporterid;
        private string _supportercode;
        private string _description;
        private decimal? _expense;
        private string _remark;
        /// <summary>
        /// 合同成本明细ID
        /// </summary>
        public int SupporterExpenseID
        {
            set { _supporterexpenseid = value; }
            get { return _supporterexpenseid; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? SupporterID
        {
            set { _supporterid = value; }
            get { return _supporterid; }
        }
        /// <summary>
        /// 项目号
        /// </summary>
        public string SupporterCode
        {
            set { _supportercode = value; }
            get { return _supportercode; }
        }
        /// <summary>
        /// 合同成本明细描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 合同成本
        /// </summary>
        public decimal? Expense
        {
            set { _expense = value; }
            get { return _expense; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}
