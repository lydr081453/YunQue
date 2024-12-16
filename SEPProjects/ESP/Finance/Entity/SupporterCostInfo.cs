using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类SupporterCostInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SupporterCostInfo
    {
        public SupporterCostInfo()
        { }
        #region Model
        private int _supportcostid;
        private int _projectid;
        private int _supportid;
        private string _description;
        private decimal _amounts;
        private decimal? _type;
        private byte[] _lastupdatetime;
        private string _remark;
        private int? _costTypeID;

        /// <summary>
        /// 自增编号
        /// </summary>
        public int SupportCostId
        {
            set { _supportcostid = value; }
            get { return _supportcostid; }
        }
        /// <summary>
        /// 项目号申请单编号
        /// </summary>
        public int ProjectId
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 支持方编号

        /// </summary>
        public int SupportId
        {
            set { _supportid = value; }
            get { return _supportid; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amounts
        {
            set { _amounts = value; }
            get { return _amounts; }
        }
        /// <summary>
        /// 金额类型：Fee;Cost;Fee&Cost
        /// </summary>
        public decimal? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// 合同成本明细类型ID
        /// </summary>
        public int? CostTypeID
        {
            get { return _costTypeID; }
            set { _costTypeID = value; }
        }
        #endregion Model

    }
}