using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ExpenseAccountInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ExpenseTypeInfo
    {
        public ExpenseTypeInfo()
        { }

        #region Model
        private int _id;
        private string _expensetype;
        private string _expensedesc;
        private int _parentID;
        private int _costDetailID;
        private int _typeLevel;
        private int _status;
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 报销类型
        /// </summary>
        public string ExpenseType
        {
            set { _expensetype = value; }
            get { return _expensetype; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string ExpenseDesc
        {
            set { _expensedesc = value; }
            get { return _expensedesc; }
        }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentID
        {
            set { _parentID = value; }
            get { return _parentID; }
        }
        /// <summary>
        /// 成本明细ID
        /// </summary>
        public int CostDetailID
        {
            set { _costDetailID = value; }
            get { return _costDetailID; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int TypeLevel
        {
            set { _typeLevel = value; }
            get { return _typeLevel; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}
