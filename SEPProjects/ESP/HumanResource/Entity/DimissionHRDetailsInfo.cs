using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class DimissionHRDetailsInfo
    {
        public DimissionHRDetailsInfo()
        { }
        #region Model
        private int _dimissionhrdetailid;
        private int _dimissionid;
        private DateTime _socialinslastmonth;
        private DateTime _medicalinslastmonth;
        private DateTime _capitalreservelastmonth;
        private DateTime? _addedmedicalinslastmonth;
        private bool _isarchives;
        private DateTime? _turnarounddate;
        private int _principal1id;
        private string _principal1name;
        private int _principal2id;
        private string _principal2name;
        private string _remark;
        private int _branchid;
        private bool _isshowposition;
        private bool _isComplementaryMedical;
        /// <summary>
        /// 
        /// </summary>
        public int DimissionHRDetailId
        {
            set { _dimissionhrdetailid = value; }
            get { return _dimissionhrdetailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DimissionId
        {
            set { _dimissionid = value; }
            get { return _dimissionid; }
        }
        /// <summary>
        /// 社保缴费截止月份
        /// </summary>
        public DateTime SocialInsLastMonth
        {
            set { _socialinslastmonth = value; }
            get { return _socialinslastmonth; }
        }
        /// <summary>
        /// 医保缴费截止月份
        /// </summary>
        public DateTime MedicalInsLastMonth
        {
            set { _medicalinslastmonth = value; }
            get { return _medicalinslastmonth; }
        }
        /// <summary>
        /// 住房公积金缴费截止月份
        /// </summary>
        public DateTime CapitalReserveLastMonth
        {
            set { _capitalreservelastmonth = value; }
            get { return _capitalreservelastmonth; }
        }
        /// <summary>
        /// 补充医疗缴费截止月份
        /// </summary>
        public DateTime? AddedMedicalInsLastMonth
        {
            set { _addedmedicalinslastmonth = value; }
            get { return _addedmedicalinslastmonth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsArchives
        {
            set { _isarchives = value; }
            get { return _isarchives; }
        }
        /// <summary>
        /// 档案办理调转截止日期
        /// </summary>
        public DateTime? TurnAroundDate
        {
            set { _turnarounddate = value; }
            get { return _turnarounddate; }
        }
        /// <summary>
        /// 负责人1ID
        /// </summary>
        public int Principal1ID
        {
            set { _principal1id = value; }
            get { return _principal1id; }
        }
        /// <summary>
        /// 负责人1姓名
        /// </summary>
        public string Principal1Name
        {
            set { _principal1name = value; }
            get { return _principal1name; }
        }
        /// <summary>
        /// 负责人2ID
        /// </summary>
        public int Principal2ID
        {
            set { _principal2id = value; }
            get { return _principal2id; }
        }
        /// <summary>
        /// 负责人2姓名
        /// </summary>
        public string Principal2Name
        {
            set { _principal2name = value; }
            get { return _principal2name; }
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
        /// 分公司编号
        /// </summary>
        public int BranchId
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 离职证明是否显示职位信息
        /// </summary>
        public bool IsShowPosition
        {
            set { _isshowposition = value; }
            get { return _isshowposition; }
        }
        public bool IsComplementaryMedical
        {
            get { return _isComplementaryMedical; }
            set { _isComplementaryMedical = value; }
        }
        #endregion Model
        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["DimissionHRDetailId"].ToString() != "")
            {
                _dimissionhrdetailid = int.Parse(r["DimissionHRDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["SocialInsLastMonth"].ToString() != "")
            {
                _socialinslastmonth = DateTime.Parse(r["SocialInsLastMonth"].ToString());
            }
            if (r["MedicalInsLastMonth"].ToString() != "")
            {
                _medicalinslastmonth = DateTime.Parse(r["MedicalInsLastMonth"].ToString());
            }
            if (r["CapitalReserveLastMonth"].ToString() != "")
            {
                _capitalreservelastmonth = DateTime.Parse(r["CapitalReserveLastMonth"].ToString());
            }
            if (r["AddedMedicalInsLastMonth"].ToString() != "")
            {
                _addedmedicalinslastmonth = DateTime.Parse(r["AddedMedicalInsLastMonth"].ToString());
            }
            if (r["IsArchives"].ToString() != "")
            {
                if ((r["IsArchives"].ToString() == "1") || (r["IsArchives"].ToString().ToLower() == "true"))
                {
                    _isarchives = true;
                }
                else
                {
                    _isarchives = false;
                }
            }
            if (r["TurnAroundDate"].ToString() != "")
            {
                _turnarounddate = DateTime.Parse(r["TurnAroundDate"].ToString());
            }
            if (r["Principal1ID"].ToString() != "")
            {
                _principal1id = int.Parse(r["Principal1ID"].ToString());
            }
            _principal1name = r["Principal1Name"].ToString();
            if (r["Principal2ID"].ToString() != "")
            {
                _principal2id = int.Parse(r["Principal2ID"].ToString());
            }
            _principal2name = r["Principal2Name"].ToString();
            _remark = r["Remark"].ToString();
            if (r["BranchId"].ToString() != "")
            {
                _branchid = int.Parse(r["BranchId"].ToString());
            }
            if (r["IsShowPosition"].ToString() != "")
            {
                if ((r["IsShowPosition"].ToString() == "1") || (r["IsShowPosition"].ToString().ToLower() == "true"))
                {
                    _isshowposition = true;
                }
                else
                {
                    _isshowposition = false;
                }
            }
            if (r["IsComplementaryMedical"].ToString() != "")
            {
                if ((r["IsComplementaryMedical"].ToString() == "1") || (r["IsComplementaryMedical"].ToString().ToLower() == "true"))
                {
                    _isComplementaryMedical = true;
                }
                else
                {
                    _isComplementaryMedical = false;
                }
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["DimissionHRDetailId"].ToString() != "")
            {
                _dimissionhrdetailid = int.Parse(r["DimissionHRDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["SocialInsLastMonth"].ToString() != "")
            {
                _socialinslastmonth = DateTime.Parse(r["SocialInsLastMonth"].ToString());
            }
            if (r["MedicalInsLastMonth"].ToString() != "")
            {
                _medicalinslastmonth = DateTime.Parse(r["MedicalInsLastMonth"].ToString());
            }
            if (r["CapitalReserveLastMonth"].ToString() != "")
            {
                _capitalreservelastmonth = DateTime.Parse(r["CapitalReserveLastMonth"].ToString());
            }
            if (r["AddedMedicalInsLastMonth"].ToString() != "")
            {
                _addedmedicalinslastmonth = DateTime.Parse(r["AddedMedicalInsLastMonth"].ToString());
            }
            if (r["IsArchives"].ToString() != "")
            {
                if ((r["IsArchives"].ToString() == "1") || (r["IsArchives"].ToString().ToLower() == "true"))
                {
                    _isarchives = true;
                }
                else
                {
                    _isarchives = false;
                }
            }
            if (r["TurnAroundDate"].ToString() != "")
            {
                _turnarounddate = DateTime.Parse(r["TurnAroundDate"].ToString());
            }
            if (r["Principal1ID"].ToString() != "")
            {
                _principal1id = int.Parse(r["Principal1ID"].ToString());
            }
            _principal1name = r["Principal1Name"].ToString();
            if (r["Principal2ID"].ToString() != "")
            {
                _principal2id = int.Parse(r["Principal2ID"].ToString());
            }
            _principal2name = r["Principal2Name"].ToString();
            _remark = r["Remark"].ToString();
            if (r["BranchId"].ToString() != "")
            {
                _branchid = int.Parse(r["BranchId"].ToString());
            }
            if (r["IsShowPosition"].ToString() != "")
            {
                if ((r["IsShowPosition"].ToString() == "1") || (r["IsShowPosition"].ToString().ToLower() == "true"))
                {
                    _isshowposition = true;
                }
                else
                {
                    _isshowposition = false;
                }
            }
            if (r["IsComplementaryMedical"].ToString() != "")
            {
                if ((r["IsComplementaryMedical"].ToString() == "1") || (r["IsComplementaryMedical"].ToString().ToLower() == "true"))
                {
                    _isComplementaryMedical = true;
                }
                else
                {
                    _isComplementaryMedical = false;
                }
            }
        }
    }
}