using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class PaymentNotifyReporterInfo : ESP.Finance.Entity.PaymentInfo
    {
        //private ESP.Finance.Entity.ProjectInfo _projectModel;
        //private ESP.Finance.Entity.InvoiceInfo _invoiceModel;
        //private ESP.Finance.Entity.PaymentInfo paymentMode;

        //public ESP.Finance.Entity.PaymentInfo PaymentMode
        //{
        //    get { return paymentMode; }
        //    set { paymentMode = value; }
        //}

        //public ESP.Finance.Entity.ProjectInfo ProjectModel
        //{
        //    get { return _projectModel; }
        //    set { _projectModel = value; }
        //}
        //public ESP.Finance.Entity.InvoiceInfo InvoiceModel
        //{
        //    get { return _invoiceModel; }
        //    set { _invoiceModel = value; }
        //}



        private int _invoicedetailid;
        private int? _invoiceid;
        private decimal? _amounts;
        private int? _responseuserid;
        private string _responseusername;
        private string _responsecode;
        private string _responseemployeename;
        private DateTime? _createdate;
        private string _description;
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int InvoiceDetailID
        {
            set { _invoicedetailid = value; }
            get { return _invoicedetailid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? InvoiceID
        {
            set { _invoiceid = value; }
            get { return _invoiceid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Amounts
        {
            set { _amounts = value; }
            get { return _amounts; }
        }

        /// <summary>
        /// 负责人ID自增长
        /// </summary>
        public int? ResponseUserID
        {
            set { _responseuserid = value; }
            get { return _responseuserid; }
        }
        /// <summary>
        /// 负责人登录帐号
        /// </summary>
        public string ResponseUserName
        {
            set { _responseusername = value; }
            get { return _responseusername; }
        }
        /// <summary>
        /// 负责人内部编号
        /// </summary>
        public string ResponseCode
        {
            set { _responsecode = value; }
            get { return _responsecode; }
        }
        /// <summary>
        /// 负责人真实姓名
        /// </summary>
        public string ResponseEmployeeName
        {
            set { _responseemployeename = value; }
            get { return _responseemployeename; }
        }


        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }


        #region 客户

        private string _custareacode;
        private string _custareaname;
        private string _custindustrycode;
        private string _custindustryname;


        private string _custnamecn1;
        private string _custnamecn2;
        private string _custnameen1;
        private string _custnameen2;
        private string _custshortcn;
        private string _custshorten;

        /// <summary>
        /// 客户中文名称
        /// </summary>
        public string CustNameCN1
        {
            set { _custnamecn1 = value; }
            get { return _custnamecn1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustNameCN2
        {
            set { _custnamecn2 = value; }
            get { return _custnamecn2; }
        }
        /// <summary>
        /// 客户英文名称
        /// </summary>
        public string CustNameEN1
        {
            set { _custnameen1 = value; }
            get { return _custnameen1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustNameEN2
        {
            set { _custnameen2 = value; }
            get { return _custnameen2; }
        }
        /// <summary>
        /// 客户中文简称

        /// </summary>
        public string CustShortCN
        {
            set { _custshortcn = value; }
            get { return _custshortcn; }
        }
        /// <summary>
        /// 客户英文简称
        /// </summary>
        public string CustShortEN
        {
            set { _custshorten = value; }
            get { return _custshorten; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string CustAreaCode
        {
            set { _custareacode = value; }
            get { return _custareacode; }
        }
        /// <summary>
        /// 所在地区名称
        /// </summary>
        public string CustAreaName
        {
            set { _custareaname = value; }
            get { return _custareaname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustIndustryCode
        {
            set { _custindustrycode = value; }
            get { return _custindustrycode; }
        }
        /// <summary>
        /// 所在行业名称
        /// </summary>
        public string CustIndustryName
        {
            set { _custindustryname = value; }
            get { return _custindustryname; }
        }

        public string CustFullNameCN
        {
            get { return CustNameCN1 + " " + CustNameCN2; }
        }

        public string CustFullNameEN
        {
            get { return CustNameEN1 + " " + CustNameEN2; }
        }

        public string CustFullAreaName
        {
            get { return CustAreaCode + " " + CustAreaName; }
        }

        public string CustFullIndustryName
        {
            get { return CustIndustryCode + " " + CustIndustryName; }
        }

        #endregion

        #region 分公司

        //private string _branchcode;
        //private string _branchname;
        ///// <summary>
        ///// 
        ///// </summary>
        //public string BranchCode
        //{
        //    set { _branchcode = value; }
        //    get { return _branchcode; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string BranchName
        //{
        //    set { _branchname = value; }
        //    get { return _branchname; }
        //}
        #endregion

        int _groupID;

        public int GroupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }
        string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }


        string _projecttypecode;
        /// <summary>
        /// 项目类型代码
        /// </summary>
        public string ProjectTypeCode
        {
            set { _projecttypecode = value; }
            get { return _projecttypecode; }
        }

    }
}
