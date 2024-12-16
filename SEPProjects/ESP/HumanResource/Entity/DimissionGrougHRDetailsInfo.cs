using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class DimissionGrougHRDetailsInfo
    {
        public DimissionGrougHRDetailsInfo()
        { }
        #region Model
        private int _dimissiongrouphrdetails;
        private int _dimissionid;
        private decimal _remainannual;
        private decimal _advanceannual;
        private string _fixedassets;
        private int _principalid;
        private string _principalname;
        /// <summary>
        /// 编号
        /// </summary>
        public int DimissionGroupHRDetails
        {
            set { _dimissiongrouphrdetails = value; }
            get { return _dimissiongrouphrdetails; }
        }
        /// <summary>
        /// 离职单编号
        /// </summary>
        public int DimissionId
        {
            set { _dimissionid = value; }
            get { return _dimissionid; }
        }
        /// <summary>
        /// 剩余年假数
        /// </summary>
        public decimal RemainAnnual
        {
            set { _remainannual = value; }
            get { return _remainannual; }
        }
        /// <summary>
        /// 预支年假数
        /// </summary>
        public decimal AdvanceAnnual
        {
            set { _advanceannual = value; }
            get { return _advanceannual; }
        }
        /// <summary>
        /// 固定资产描述信息
        /// </summary>
        public string FixedAssets
        {
            set { _fixedassets = value; }
            get { return _fixedassets; }
        }
        /// <summary>
        /// 负责人ID
        /// </summary>
        public int PrincipalID
        {
            set { _principalid = value; }
            get { return _principalid; }
        }
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string PrincipalName
        {
            set { _principalname = value; }
            get { return _principalname; }
        }
        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["DimissionGroupHRDetails"].ToString() != "")
            {
                _dimissiongrouphrdetails = int.Parse(r["DimissionGroupHRDetails"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["RemainAnnual"].ToString() != "")
            {
                _remainannual = decimal.Parse(r["RemainAnnual"].ToString());
            }
            if (r["AdvanceAnnual"].ToString() != "")
            {
                _advanceannual = decimal.Parse(r["AdvanceAnnual"].ToString());
            }
            _fixedassets = r["FixedAssets"].ToString();
            if (r["PrincipalID"].ToString() != "")
            {
                _principalid = int.Parse(r["PrincipalID"].ToString());
            }
            _principalname = r["PrincipalName"].ToString();
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["DimissionGroupHRDetails"].ToString() != "")
            {
                _dimissiongrouphrdetails = int.Parse(r["DimissionGroupHRDetails"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["RemainAnnual"].ToString() != "")
            {
                _remainannual = decimal.Parse(r["RemainAnnual"].ToString());
            }
            if (r["AdvanceAnnual"].ToString() != "")
            {
                _advanceannual = decimal.Parse(r["AdvanceAnnual"].ToString());
            }
            _fixedassets = r["FixedAssets"].ToString();
            if (r["PrincipalID"].ToString() != "")
            {
                _principalid = int.Parse(r["PrincipalID"].ToString());
            }
            _principalname = r["PrincipalName"].ToString();
        }
    }
}