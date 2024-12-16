using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ProjectExpenseInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ProjectExpenseInfo
    {
        public ProjectExpenseInfo()
        { }
        #region Model
        private int _projectexpenseid;
        private int? _projectid;
        private string _projectcode;
        private string _description;
        private decimal? _expense;
        private string _remark;
        /// <summary>
        /// 合同成本明细ID
        /// </summary>
        public int ProjectExpenseID
        {
            set { _projectexpenseid = value; }
            get { return _projectexpenseid; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
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