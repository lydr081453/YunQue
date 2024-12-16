using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class ContractCostTmpInfo
    {
        public ContractCostTmpInfo()
        { }
        #region Model
        private int _contractcostid;
        private int? _contractid;
        private int? _projectid;
        private string _projectcode;
        private string _description;
        private decimal? _cost;
        private string _remark;
        private int? _costTypeID;


        /// <summary>
        /// 合同成本明细ID
        /// </summary>
        public int ContractCostID
        {
            set { _contractcostid = value; }
            get { return _contractcostid; }
        }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int? ContractID
        {
            set { _contractid = value; }
            get { return _contractid; }
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
        public decimal? Cost
        {
            set { _cost = value; }
            get { return _cost; }
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
