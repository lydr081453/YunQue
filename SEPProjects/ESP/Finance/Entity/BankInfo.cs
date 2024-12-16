using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
[Serializable]
    public class BankInfo
    {
        public BankInfo()
        { }
        #region Model
        private int _bankid;
        private string _bankaccount;
        private string _phoneno;
        private string _exchangeno;
        private string _requestphone;
        private string _webbankcode;
        private int _branchid;
        private string _branchcode;
        private string _branchname;
        private string _dbcode;
        private string _dbmanager;
        private string _bankname;
        private string _bankaccountname;
        private string _address;
        /// <summary>
        /// 
        /// </summary>
        public int BankID
        {
            set { _bankid = value; }
            get { return _bankid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankAccount
        {
            set { _bankaccount = value; }
            get { return _bankaccount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNo
        {
            set { _phoneno = value; }
            get { return _phoneno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExchangeNo
        {
            set { _exchangeno = value; }
            get { return _exchangeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestPhone
        {
            set { _requestphone = value; }
            get { return _requestphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WebBankCode
        {
            set { _webbankcode = value; }
            get { return _webbankcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BranchCode
        {
            set { _branchcode = value; }
            get { return _branchcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BranchName
        {
            set { _branchname = value; }
            get { return _branchname; }
        }
        /// <summary>
        /// 数据库代码
        /// </summary>
        public string DBCode
        {
            set { _dbcode = value; }
            get { return _dbcode; }
        }
        /// <summary>
        /// 管理数据库
        /// </summary>
        public string DBManager
        {
            set { _dbmanager = value; }
            get { return _dbmanager; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankName
        {
            set { _bankname = value; }
            get { return _bankname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankAccountName
        {
            set { _bankaccountname = value; }
            get { return _bankaccountname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        public int IsFactoring { get; set; }

        #endregion Model

    }
}
