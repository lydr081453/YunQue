using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{

        public class CostRecordInfo
        {
            public CostRecordInfo()
            { }
            #region Model
            private int _recordid;
            private int _prid;
            private string _prno;
            private string _requestor;
            private string _groupname;
            private DateTime _appdate;
            private decimal _appamount;
            private int _prtype;
            private decimal _pntotal;
            private string _description;
            private int _typeid;
            private string _typename;
            private int _projectid;
            private string _projectcode;
            private string _suppliername;
            private decimal _costpreamount;
            private decimal _typetotalamount;
            private int _departmentid;
            private string _departmentname;
            private decimal _paidamount;
            private decimal _unpaidamount;
            private int _returntype;
            private int _recordtype;
            public decimal OrderTotal { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int RecordID
            {
                set { _recordid = value; }
                get { return _recordid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int PRID
            {
                set { _prid = value; }
                get { return _prid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string PRNO
            {
                set { _prno = value; }
                get { return _prno; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Requestor
            {
                set { _requestor = value; }
                get { return _requestor; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string GroupName
            {
                set { _groupname = value; }
                get { return _groupname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime AppDate
            {
                set { _appdate = value; }
                get { return _appdate; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal AppAmount
            {
                set { _appamount = value; }
                get { return _appamount; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int PrType
            {
                set { _prtype = value; }
                get { return _prtype; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal PNTotal
            {
                set { _pntotal = value; }
                get { return _pntotal; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Description
            {
                set { _description = value; }
                get { return _description; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int TypeID
            {
                set { _typeid = value; }
                get { return _typeid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string TypeName
            {
                set { _typename = value; }
                get { return _typename; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int ProjectID
            {
                set { _projectid = value; }
                get { return _projectid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string ProjectCode
            {
                set { _projectcode = value; }
                get { return _projectcode; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string SupplierName
            {
                set { _suppliername = value; }
                get { return _suppliername; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal CostPreAmount
            {
                set { _costpreamount = value; }
                get { return _costpreamount; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal TypeTotalAmount
            {
                set { _typetotalamount = value; }
                get { return _typetotalamount; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int DepartmentID
            {
                set { _departmentid = value; }
                get { return _departmentid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string DepartmentName
            {
                set { _departmentname = value; }
                get { return _departmentname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal PaidAmount
            {
                set { _paidamount = value; }
                get { return _paidamount; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal UnPaidAmount
            {
                set { _unpaidamount = value; }
                get { return _unpaidamount; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int ReturnType
            {
                set { _returntype = value; }
                get { return _returntype; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int RecordType
            {
                set { _recordtype = value; }
                get { return _recordtype; }
            }
            #endregion Model

        }
    }



