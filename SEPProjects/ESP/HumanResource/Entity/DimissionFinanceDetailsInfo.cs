using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class DimissionFinanceDetailsInfo
    {
        public DimissionFinanceDetailsInfo()
        { }
        #region Model
        private int _dimissionfinancedetailid;
        private int _dimissionid;
        private string _loan;
        private string _businesscard;
        private string _accountspayable;
        private string _salary;
        private string _other;
        private int _financeauditstatus;
        private string _tellerids;
        private string _tellernames;
        private string _accountantids;
        private string _accountantnames;
        private string _businesscardauditids;
        private string _businesscardauditnames;
        private int _directorid;
        private string _directorname;
        private string _salary2;
        private int _salarytellerid;
        private string _salarytellername;
        private int _salarybranch;

        /// <summary>
        /// 编号
        /// </summary>
        public int DimissionFinanceDetailId
        {
            set { _dimissionfinancedetailid = value; }
            get { return _dimissionfinancedetailid; }
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
        /// 借款描述信息
        /// </summary>
        public string Loan
        {
            set { _loan = value; }
            get { return _loan; }
        }
        /// <summary>
        /// 商务卡描述信息
        /// </summary>
        public string BusinessCard
        {
            set { _businesscard = value; }
            get { return _businesscard; }
        }
        /// <summary>
        /// 应收应付款描述信息
        /// </summary>
        public string AccountsPayable
        {
            set { _accountspayable = value; }
            get { return _accountspayable; }
        }
        /// <summary>
        /// 工资描述信息
        /// </summary>
        public string Salary
        {
            set { _salary = value; }
            get { return _salary; }
        }
        /// <summary>
        /// 其他说明信息
        /// </summary>
        public string Other
        {
            set { _other = value; }
            get { return _other; }
        }
        /// <summary>
        /// 财务审批状态
        /// </summary>
        public int FinanceAuditStatus
        {
            set { _financeauditstatus = value; }
            get { return _financeauditstatus; }
        }
        /// <summary>
        /// 出纳ID
        /// </summary>
        public string TellerIds
        {
            set { _tellerids = value; }
            get { return _tellerids; }
        }
        /// <summary>
        /// 出纳姓名
        /// </summary>
        public string TellerNames
        {
            set { _tellernames = value; }
            get { return _tellernames; }
        }
        /// <summary>
        /// 会计ID
        /// </summary>
        public string AccountantIds
        {
            set { _accountantids = value; }
            get { return _accountantids; }
        }
        /// <summary>
        /// 会计姓名
        /// </summary>
        public string AccountantNames
        {
            set { _accountantnames = value; }
            get { return _accountantnames; }
        }
        /// <summary>
        /// 商务卡审批人ID
        /// </summary>
        public string BusinessCardAuditIds
        {
            set { _businesscardauditids = value; }
            get { return _businesscardauditids; }
        }
        /// <summary>
        /// 商务卡审批姓名
        /// </summary>
        public string BusinessCardAuditNames
        {
            set { _businesscardauditnames = value; }
            get { return _businesscardauditnames; }
        }
        /// <summary>
        /// 财务总监ID
        /// </summary>
        public int DirectorId
        {
            set { _directorid = value; }
            get { return _directorid; }
        }
        /// <summary>
        /// 财务总监姓名
        /// </summary>
        public string DirectorName
        {
            set { _directorname = value; }
            get { return _directorname; }
        }
        /// <summary>
        /// 工资结算后，员工如需返还资金给公司，则有出纳负责收款，收款审批信息。
        /// </summary>
        public string Salary2
        {
            set { _salary2 = value; }
            get { return _salary2; }
        }
        /// <summary>
        /// 工资结算后收款出纳ID
        /// </summary>
        public int SalaryTellerId
        {
            set { _salarytellerid = value; }
            get { return _salarytellerid; }
        }
        /// <summary>
        /// 工资结算后收款出纳姓名
        /// </summary>
        public string SalaryTellerName
        {
            set { _salarytellername = value; }
            get { return _salarytellername; }
        }

        /// <summary>
        /// 工资结算后收款出纳所属分公司。
        /// </summary>
        public int SalaryBranch
        {
            set { _salarybranch = value; }
            get { return _salarybranch; }
        }
        #endregion Model
        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["DimissionFinanceDetailId"].ToString() != "")
            {
                _dimissionfinancedetailid = int.Parse(r["DimissionFinanceDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _loan = r["Loan"].ToString();
            _businesscard = r["BusinessCard"].ToString();
            _accountspayable = r["AccountsPayable"].ToString();
            _salary = r["Salary"].ToString();
            _other = r["Other"].ToString();
            if (r["FinanceAuditStatus"].ToString() != "")
            {
                _financeauditstatus = int.Parse(r["FinanceAuditStatus"].ToString());
            }
            _tellerids = r["TellerIds"].ToString();
            _tellernames = r["TellerNames"].ToString();
            _accountantids = r["AccountantIds"].ToString();
            _accountantnames = r["AccountantNames"].ToString();
            _businesscardauditids = r["BusinessCardAuditIds"].ToString();
            _businesscardauditnames = r["BusinessCardAuditNames"].ToString();
            if (r["DirectorId"].ToString() != "")
            {
                _directorid = int.Parse(r["DirectorId"].ToString());
            }
            _directorname = r["DirectorName"].ToString();
            _salary2 = r["Salary2"].ToString();
            if (r["SalaryTellerId"].ToString() != "")
            {
                _salarytellerid = int.Parse(r["SalaryTellerId"].ToString());
            }
            _salarytellername = r["SalaryTellerName"].ToString();
            if (r["SalaryBranch"].ToString() != "")
            {
                _salarybranch = int.Parse(r["SalaryBranch"].ToString());
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["DimissionFinanceDetailId"].ToString() != "")
            {
                _dimissionfinancedetailid = int.Parse(r["DimissionFinanceDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            _loan = r["Loan"].ToString();
            _businesscard = r["BusinessCard"].ToString();
            _accountspayable = r["AccountsPayable"].ToString();
            _salary = r["Salary"].ToString();
            _other = r["Other"].ToString();
            if (r["FinanceAuditStatus"].ToString() != "")
            {
                _financeauditstatus = int.Parse(r["FinanceAuditStatus"].ToString());
            }
            _tellerids = r["TellerIds"].ToString();
            _tellernames = r["TellerNames"].ToString();
            _accountantids = r["AccountantIds"].ToString();
            _accountantnames = r["AccountantNames"].ToString();
            _businesscardauditids = r["BusinessCardAuditIds"].ToString();
            _businesscardauditnames = r["BusinessCardAuditNames"].ToString();
            if (r["DirectorId"].ToString() != "")
            {
                _directorid = int.Parse(r["DirectorId"].ToString());
            }
            _directorname = r["DirectorName"].ToString();
            _salary2 = r["Salary2"].ToString();
            if (r["SalaryTellerId"].ToString() != "")
            {
                _salarytellerid = int.Parse(r["SalaryTellerId"].ToString());
            }
            _salarytellername = r["SalaryTellerName"].ToString();
            if (r["SalaryBranch"].ToString() != "")
            {
                _salarybranch = int.Parse(r["SalaryBranch"].ToString());
            }
        }
    }
}