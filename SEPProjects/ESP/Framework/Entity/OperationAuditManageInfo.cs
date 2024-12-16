using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Framework.Entity
{
    public class OperationAuditManageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAuditManageInfo"/> class.
        /// </summary>
        public OperationAuditManageInfo()
        { }

        #region Model
        private int _id;
        private int _depid;
        private int _directorid;
        private string _directorname;
        private int _managerid;
        private string _managername;
        private int _ceoid;
        private string _ceoname;
        private int _attendanceid;
        private string _attendancename;
        private int _hrid;
        private string _hrname;
        private int _faid;
        private string _faname;
        private int _hrattendanceid;
        private string _hrattendancename;
        private int _admanagerid;
        private string _admanagername;
        /// <summary>
        /// 机票预定UserId(无此角色暂时不用)
        /// </summary>
        public int ReceptionId { get; set; }
        /// <summary>
        /// 机票预定前台(无此角色暂时不用)
        /// </summary>
        public string Reception { get; set; }
        /// <summary>
        /// 机票批次审批初审人UserId(无此角色暂时不用)
        /// </summary>
        public int TicketPurchaseId { get; set; }
        /// <summary>
        /// 机票批次审批初审人(无此角色暂时不用)
        /// </summary>
        public string TicketPurchase { get; set; }

        private int _dimissionfinanceauditorid;
        private string _dimissionfinanceauditorname;
        private int _dimissionadauditorid;
        private string _dimissionadauditorname;
        private int _dimissionDirectorid;
        private string _dimissionDirectorName;
        private int _dimissionManagerId;
        private string _dimissionManagerName;
        /// <summary>
        /// 无此角色暂时不用
        /// </summary>
        public int FinanceUserId { get; set; }
        /// <summary>
        /// 无此角色暂时不用
        /// </summary>
        public string FinanceUsername { get; set; }
        /// <summary>
        /// 特殊人员专设审批路径
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 特殊项目专设审批路径
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        /// <value>The id.</value>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        /// <value>The dep id.</value>
        public int DepId
        {
            set { _depid = value; }
            get { return _depid; }
        }

        /// <summary>
        /// 总监编号
        /// </summary>
        /// <value>The director id.</value>
        public int DirectorId
        {
            set { _directorid = value; }
            get { return _directorid; }
        }

        /// <summary>
        /// 总监姓名
        /// </summary>
        /// <value>The name of the director.</value>
        public string DirectorName
        {
            set { _directorname = value; }
            get { return _directorname; }
        }

        /// <summary>
        /// 总经理编号
        /// </summary>
        /// <value>The manager id.</value>
        public int ManagerId
        {
            set { _managerid = value; }
            get { return _managerid; }
        }

        /// <summary>
        /// 总经理姓名
        /// </summary>
        /// <value>The name of the manager.</value>
        public string ManagerName
        {
            set { _managername = value; }
            get { return _managername; }
        }

        /// <summary>
        /// CEO编号
        /// </summary>
        /// <value>The CEO id.</value>
        public int CEOId
        {
            set { _ceoid = value; }
            get { return _ceoid; }
        }

        /// <summary>
        /// CEO姓名
        /// </summary>
        /// <value>The name of the CEO.</value>
        public string CEOName
        {
            set { _ceoname = value; }
            get { return _ceoname; }
        }
        /// <summary>
        /// 考勤事由审批总经理编号(考勤默认一级总监审批暂时不用)
        /// </summary>
        /// <value>The manager id.</value>
        public int AttendanceId
        {
            set { _attendanceid = value; }
            get { return _attendanceid; }
        }

        /// <summary>
        /// 考勤事由审批总经理姓名(考勤默认一级总监审批暂时不用)
        /// </summary>
        /// <value>The name of the manager.</value>
        public string AttendanceName
        {
            set { _attendancename = value; }
            get { return _attendancename; }
        }

        /// <summary>
        /// HR审批人编号
        /// </summary>
        /// <value>The CEO id.</value>
        public int HRId
        {
            set { _hrid = value; }
            get { return _hrid; }
        }

        /// <summary>
        /// HR审批人姓名
        /// </summary>
        /// <value>The name of the CEO.</value>
        public string HRName
        {
            set { _hrname = value; }
            get { return _hrname; }
        }

        /// <summary>
        /// 财务助理审批人编号(目前无此角色暂时不用)
        /// </summary>
        /// <value>The CEO id.</value>
        public int FAId
        {
            set { _faid = value; }
            get { return _faid; }
        }

        /// <summary>
        /// 财务助理审批人姓名(目前无此角色暂时不用)
        /// </summary>
        /// <value>The name of the CEO.</value>
        public string FAName
        {
            set { _faname = value; }
            get { return _faname; }
        }

        /// <summary>
        /// 月度考勤审批人ID
        /// </summary>
        public int Hrattendanceid
        {
            get { return _hrattendanceid; }
            set { _hrattendanceid = value; }
        }

        /// <summary>
        /// 月度考勤审批人姓名
        /// </summary>
        public string Hrattendancename
        {
            get { return _hrattendancename; }
            set { _hrattendancename = value; }
        }
        /// <summary>
        /// 考勤行政管理员ID(目前无此角色暂时不用)
        /// </summary>
        public int ADManagerID
        {
            set { _admanagerid = value; }
            get { return _admanagerid; }
        }
        /// <summary>
        /// 考勤行政管理员姓名(目前无此角色暂时不用)
        /// </summary>
        public string ADManagerName
        {
            set { _admanagername = value; }
            get { return _admanagername; }
        }
        /// <summary>
        /// 离职财务审批人ID(目前无此角色暂时不用)
        /// </summary>
        public int DimissionFinanceAuditorId
        {
            set { _dimissionfinanceauditorid = value; }
            get { return _dimissionfinanceauditorid; }
        }
        /// <summary>
        /// 离职财务审批姓名(目前无此角色暂时不用)
        /// </summary>
        public string DimissionFinanceAuditorName
        {
            set { _dimissionfinanceauditorname = value; }
            get { return _dimissionfinanceauditorname; }
        }
        /// <summary>
        /// 离职行政审批人ID
        /// </summary>
        public int DimissionADAuditorId
        {
            set { _dimissionadauditorid = value; }
            get { return _dimissionadauditorid; }
        }
        /// <summary>
        /// 离职行政审批人姓名
        /// </summary>
        public string DimissionADAuditorName
        {
            set { _dimissionadauditorname = value; }
            get { return _dimissionadauditorname; }
        }

        /// <summary>
        /// 离职总监级审批人ID
        /// </summary>
        public int DimissionDirectorid
        {
            set { _dimissionDirectorid = value; }
            get { return _dimissionDirectorid; }
        }
        /// <summary>
        /// 离职总监级审批人姓名
        /// </summary>
        public string DimissionDirectorName
        {
            set { _dimissionDirectorName = value; }
            get { return _dimissionDirectorName; }
        }
        /// <summary>
        /// 离职总经理级审批人
        /// </summary>
        public int DimissionManagerId
        {
            set { _dimissionManagerId = value; }
            get { return _dimissionManagerId; }
        }
        /// <summary>
        /// 离职总经理级审批人
        /// </summary>
        public string DimissionManagerName
        {
            set { _dimissionManagerName = value; }
            get { return _dimissionManagerName; }
        }
        /// <summary>
        /// HeadCount初审人ID
        /// </summary>
        public int HeadCountAuditorId { get; set; }
        /// <summary>
        /// HeadCount初审人
        /// </summary>
        public string HeadCountAuditor { get; set; }
        /// <summary>
        /// HeadCount总监级审核人ID
        /// </summary>
        public int HeadCountDirectorId { get; set; }
        /// <summary>
        /// HeadCount总监级审核人
        /// </summary>
        public string HeadCountDirector { get; set; }
        /// <summary>
        /// 采购物料审核人ID
        /// </summary>
        public int PurchaseAuditorId { get; set; }
        /// <summary>
        /// 采购物料审核人
        /// </summary>
        public string PurchaseAuditor { get; set; }
        /// <summary>
        /// 采购附加收货人ID
        /// </summary>
        public int AppendReceiverId { get; set; }
        /// <summary>
        /// 采购附加收货人
        /// </summary>
        public string AppendReceiver { get; set; }
        /// <summary>
        /// 采购总监级审核人ID
        /// </summary>
        public int PurchaseDirectorId { get; set; }
        /// <summary>
        ///  采购总监级审核人
        /// </summary>
        public string PurchaseDirector { get; set; }
        /// <summary>
        /// 总监级审核人金额权限
        /// </summary>
        public decimal DirectorAmount { get; set; }
        /// <summary>
        /// 总经理级审核人金额权限
        /// </summary>
        public decimal ManagerAmount { get; set; }
        /// <summary>
        /// CEO级审核人金额权限
        /// </summary>
        public decimal CEOAmount { get; set; }
        /// <summary>
        /// (目前无此角色暂时不用)
        /// </summary>
        public int ProjectCEOIndependent { get; set; }
        /// <summary>
        /// 部门应收报表的查阅权限(逗号分隔格式:,1,2,3,)
        /// </summary>
        public string ARReportUsers { get; set; }
        /// <summary>
        /// 项目成本查阅权限(逗号分隔格式:,1,2,3,)
        /// </summary>
        public string CostView { get; set; }

        /// <summary>
        /// 风控中心审核人ID
        /// </summary>
        public int RiskControlAccounter { get; set; }
        /// <summary>
        /// 风控中心审核人
        /// </summary>
        public string RiskControlAccounterName { get; set; }

        /// <summary>
        /// HC终审人
        /// </summary>
        public int HCFinalAuditor { get; set; }
        /// <summary>
        /// 离职业务预审人ID
        /// </summary>
        public int DimissionPreAuditorId{ get; set; }
        /// <summary>
        /// 离职业务预审人
        /// </summary>
        public string DimissionPreAuditor { get; set; }

        public int ITOperatorId { get; set; }
        public string ITOperator { get; set; }
        #endregion Model

        #region method
        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                _id = int.Parse(r["id"].ToString());
            }
            if (null != r["DepId"] && r["DepId"].ToString() != "")
            {
                _depid = int.Parse(r["DepId"].ToString());
            }
            if (null != r["DirectorId"] && r["DirectorId"].ToString() != "")
            {
                _directorid = int.Parse(r["DirectorId"].ToString());
            }
            _directorname = r["DirectorName"].ToString();
            if (null != r["ManagerId"] && r["ManagerId"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerId"].ToString());
            }
            _managername = r["managername"].ToString();
            if (null != r["CEOId"] && r["CEOId"].ToString() != "")
            {
                _ceoid = int.Parse(r["CEOId"].ToString());
            }
            _ceoname = r["ceoname"].ToString();

            if (null != r["AttendanceId"] && r["AttendanceId"].ToString() != "")
            {
                _attendanceid = int.Parse(r["AttendanceId"].ToString());
            }
            _attendancename = r["AttendanceName"].ToString();
            if (null != r["HRId"] && r["HRId"].ToString() != "")
            {
                _hrid = int.Parse(r["HRId"].ToString());
            }
            _hrname = r["HRName"].ToString();
            if (null != r["FAId"] && r["FAId"].ToString() != "")
            {
                _faid = int.Parse(r["FAId"].ToString());
            }
            _faname = r["FAName"].ToString();
            if (null != r["Hrattendanceid"] && r["Hrattendanceid"].ToString() != "")
            {
                _hrattendanceid = int.Parse(r["Hrattendanceid"].ToString());
            }
            _hrattendancename = r["Hrattendancename"].ToString();
            if (r["ADManagerID"].ToString() != "")
            {
                _admanagerid = int.Parse(r["ADManagerID"].ToString());
            }
            _admanagername = r["ADManagerName"].ToString();
            if (null != r["ReceptionId"] && r["ReceptionId"].ToString() != "")
            {
                ReceptionId = int.Parse(r["ReceptionId"].ToString());
            }

            if (null != r["Reception"] && r["Reception"].ToString() != "")
            {
                Reception = r["Reception"].ToString();
            }
            if (null != r["TicketPurchaseId"] && r["TicketPurchaseId"].ToString() != "")
            {
                TicketPurchaseId = int.Parse(r["TicketPurchaseId"].ToString());
            }
            if (null != r["TicketPurchase"] && r["TicketPurchase"].ToString() != "")
            {
                TicketPurchase = r["TicketPurchase"].ToString();
            }
            if (r["DimissionFinanceAuditorId"].ToString() != "")
            {
                _dimissionfinanceauditorid = int.Parse(r["DimissionFinanceAuditorId"].ToString());
            }
            _dimissionfinanceauditorname = r["DimissionFinanceAuditorName"].ToString();
            if (r["DimissionADAuditorId"].ToString() != "")
            {
                _dimissionadauditorid = int.Parse(r["DimissionADAuditorId"].ToString());
            }
            _dimissionadauditorname = r["DimissionADAuditorName"].ToString();
            if (r["DimissionDirectorid"].ToString() != "")
            {
                _dimissionDirectorid = int.Parse(r["DimissionDirectorid"].ToString());
            }
            _dimissionDirectorName = r["DimissionDirectorName"].ToString();
            if (r["DimissionManagerId"].ToString() != "")
            {
                _dimissionManagerId = int.Parse(r["DimissionManagerId"].ToString());
            }
            _dimissionManagerName = r["DimissionManagerName"].ToString();

            if (r["FinanceUserId"].ToString() != "")
            {
                FinanceUserId = int.Parse(r["FinanceUserId"].ToString());
            }
            FinanceUsername = r["FinanceUsername"].ToString();

            if (r["HeadCountAuditorId"].ToString() != "")
            {
                HeadCountAuditorId = int.Parse(r["HeadCountAuditorId"].ToString());
            }
            HeadCountAuditor = r["HeadCountAuditor"].ToString();


            if (r["HeadCountDirectorId"].ToString() != "")
            {
                HeadCountDirectorId = int.Parse(r["HeadCountDirectorId"].ToString());
            }
            HeadCountDirector = r["HeadCountDirector"].ToString();


            if (r["PurchaseAuditorId"].ToString() != "")
            {
                PurchaseAuditorId = int.Parse(r["PurchaseAuditorId"].ToString());
            }
            PurchaseAuditor = r["PurchaseAuditor"].ToString();


            if (r["AppendReceiverId"].ToString() != "")
            {
                AppendReceiverId = int.Parse(r["AppendReceiverId"].ToString());
            }
            AppendReceiver = r["AppendReceiver"].ToString();


            if (r["PurchaseDirectorId"].ToString() != "")
            {
                PurchaseDirectorId = int.Parse(r["PurchaseDirectorId"].ToString());
            }
            PurchaseDirector = r["PurchaseDirector"].ToString();


            if (r["DirectorAmount"].ToString() != "")
            {
                DirectorAmount = decimal.Parse(r["DirectorAmount"].ToString());
            }
            if (r["ManagerAmount"].ToString() != "")
            {
                ManagerAmount = int.Parse(r["ManagerAmount"].ToString());
            }
            if (r["CEOAmount"].ToString() != "")
            {
                CEOAmount = int.Parse(r["CEOAmount"].ToString());
            }

            if (r["UserId"].ToString() != "")
            {
                UserId = int.Parse(r["UserId"].ToString());
            }

            if (r["ProjectId"].ToString() != "")
            {
                ProjectID = int.Parse(r["ProjectId"].ToString());
            }

            if (r["ProjectCEOIndependent"].ToString() != "")
            {
                ProjectCEOIndependent = int.Parse(r["ProjectCEOIndependent"].ToString());
            }


            if (null != r["ARReportUsers"] && r["ARReportUsers"].ToString() != "")
            {
                ARReportUsers = r["ARReportUsers"].ToString();
            }
            if (null != r["CostView"] && r["CostView"].ToString() != "")
            {
                CostView = r["CostView"].ToString();
            }

            if (r["RiskControlAccounter"].ToString() != "")
            {
                RiskControlAccounter = int.Parse(r["RiskControlAccounter"].ToString());
            }
            if (null != r["RiskControlAccounterName"] && r["RiskControlAccounterName"].ToString() != "")
            {
                RiskControlAccounterName = r["RiskControlAccounterName"].ToString();
            }

            if (r["HCFinalAuditor"].ToString() != "")
            {
                HCFinalAuditor = int.Parse(r["HCFinalAuditor"].ToString());
            }

            if (r["DimissionPreAuditorId"].ToString() != "")
            {
                DimissionPreAuditorId = int.Parse(r["DimissionPreAuditorId"].ToString());
            }
            if (null != r["DimissionPreAuditor"] && r["DimissionPreAuditor"].ToString() != "")
            {
                DimissionPreAuditor = r["DimissionPreAuditor"].ToString();
            }
            if (r["ITOperatorId"].ToString() != "")
            {
                ITOperatorId = int.Parse(r["ITOperatorId"].ToString());
            }
            if (null != r["ITOperator"] && r["ITOperator"].ToString() != "")
            {
                ITOperator = r["ITOperator"].ToString();
            }
        }

        public void PopupData(DataRow r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                _id = int.Parse(r["id"].ToString());
            }
            if (null != r["DepId"] && r["DepId"].ToString() != "")
            {
                _depid = int.Parse(r["DepId"].ToString());
            }
            if (null != r["DirectorId"] && r["DirectorId"].ToString() != "")
            {
                _directorid = int.Parse(r["DirectorId"].ToString());
            }
            _directorname = r["DirectorName"].ToString();
            if (null != r["ManagerId"] && r["ManagerId"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerId"].ToString());
            }
            _managername = r["managername"].ToString();
            if (null != r["CEOId"] && r["CEOId"].ToString() != "")
            {
                _ceoid = int.Parse(r["CEOId"].ToString());
            }
            _ceoname = r["ceoname"].ToString();

            if (null != r["AttendanceId"] && r["AttendanceId"].ToString() != "")
            {
                _attendanceid = int.Parse(r["AttendanceId"].ToString());
            }
            _attendancename = r["AttendanceName"].ToString();
            if (null != r["HRId"] && r["HRId"].ToString() != "")
            {
                _hrid = int.Parse(r["HRId"].ToString());
            }
            _hrname = r["HRName"].ToString();
            if (null != r["FAId"] && r["FAId"].ToString() != "")
            {
                _faid = int.Parse(r["FAId"].ToString());
            }
            _faname = r["FAName"].ToString();
            if (null != r["Hrattendanceid"] && r["Hrattendanceid"].ToString() != "")
            {
                _hrattendanceid = int.Parse(r["Hrattendanceid"].ToString());
            }
            _hrattendancename = r["Hrattendancename"].ToString();
            if (r["ADManagerID"].ToString() != "")
            {
                _admanagerid = int.Parse(r["ADManagerID"].ToString());
            }
            _admanagername = r["ADManagerName"].ToString();
            if (null != r["ReceptionId"] && r["ReceptionId"].ToString() != "")
            {
                ReceptionId = int.Parse(r["ReceptionId"].ToString());
            }

            if (null != r["Reception"] && r["Reception"].ToString() != "")
            {
                Reception = r["Reception"].ToString();
            }
            if (null != r["TicketPurchaseId"] && r["TicketPurchaseId"].ToString() != "")
            {
                TicketPurchaseId = int.Parse(r["TicketPurchaseId"].ToString());
            }
            if (null != r["TicketPurchase"] && r["TicketPurchase"].ToString() != "")
            {
                TicketPurchase = r["TicketPurchase"].ToString();
            }
            if (r["DimissionFinanceAuditorId"].ToString() != "")
            {
                _dimissionfinanceauditorid = int.Parse(r["DimissionFinanceAuditorId"].ToString());
            }
            _dimissionfinanceauditorname = r["DimissionFinanceAuditorName"].ToString();
            if (r["DimissionADAuditorId"].ToString() != "")
            {
                _dimissionadauditorid = int.Parse(r["DimissionADAuditorId"].ToString());
            }
            _dimissionadauditorname = r["DimissionADAuditorName"].ToString();
            if (r["DimissionDirectorid"].ToString() != "")
            {
                _dimissionDirectorid = int.Parse(r["DimissionDirectorid"].ToString());
            }
            _dimissionDirectorName = r["DimissionDirectorName"].ToString();
            if (r["DimissionManagerId"].ToString() != "")
            {
                _dimissionManagerId = int.Parse(r["DimissionManagerId"].ToString());
            }
            _dimissionManagerName = r["DimissionManagerName"].ToString();

            if (r["FinanceUserId"].ToString() != "")
            {
                FinanceUserId = int.Parse(r["FinanceUserId"].ToString());
            }
            FinanceUsername = r["FinanceUsername"].ToString();

            if (r["HeadCountAuditorId"].ToString() != "")
            {
                HeadCountAuditorId = int.Parse(r["HeadCountAuditorId"].ToString());
            }
            HeadCountAuditor = r["HeadCountAuditor"].ToString();


            if (r["HeadCountDirectorId"].ToString() != "")
            {
                HeadCountDirectorId = int.Parse(r["HeadCountDirectorId"].ToString());
            }
            HeadCountDirector = r["HeadCountDirector"].ToString();

            if (r["PurchaseAuditorId"].ToString() != "")
            {
                PurchaseAuditorId = int.Parse(r["PurchaseAuditorId"].ToString());
            }
            PurchaseAuditor = r["PurchaseAuditor"].ToString();

            if (r["AppendReceiverId"].ToString() != "")
            {
                AppendReceiverId = int.Parse(r["AppendReceiverId"].ToString());
            }
            AppendReceiver = r["AppendReceiver"].ToString();

            if (r["PurchaseDirectorId"].ToString() != "")
            {
                PurchaseDirectorId = int.Parse(r["PurchaseDirectorId"].ToString());
            }
            PurchaseDirector = r["PurchaseDirector"].ToString();


            if (r["DirectorAmount"].ToString() != "")
            {
                DirectorAmount = decimal.Parse(r["DirectorAmount"].ToString());
            }
            if (r["ManagerAmount"].ToString() != "")
            {
                ManagerAmount = int.Parse(r["ManagerAmount"].ToString());
            }
            if (r["CEOAmount"].ToString() != "")
            {
                CEOAmount = int.Parse(r["CEOAmount"].ToString());
            }

            if (r["UserId"].ToString() != "")
            {
                UserId = int.Parse(r["UserId"].ToString());
            }

            if (r["ProjectId"].ToString() != "")
            {
                ProjectID = int.Parse(r["ProjectId"].ToString());
            }

            if (r["ProjectCEOIndependent"].ToString() != "")
            {
                ProjectCEOIndependent = int.Parse(r["ProjectCEOIndependent"].ToString());
            }

            if (null != r["ARReportUsers"] && r["ARReportUsers"].ToString() != "")
            {
                ARReportUsers = r["ARReportUsers"].ToString();
            }
            if (null != r["CostView"] && r["CostView"].ToString() != "")
            {
                CostView = r["CostView"].ToString();
            }
            if (r["RiskControlAccounter"].ToString() != "")
            {
                RiskControlAccounter = int.Parse(r["RiskControlAccounter"].ToString());
            }
            if (null != r["RiskControlAccounterName"] && r["RiskControlAccounterName"].ToString() != "")
            {
                RiskControlAccounterName = r["RiskControlAccounterName"].ToString();
            }
            if (r["HCFinalAuditor"].ToString() != "")
            {
                HCFinalAuditor = int.Parse(r["HCFinalAuditor"].ToString());
            }

            if (r["DimissionPreAuditorId"].ToString() != "")
            {
                DimissionPreAuditorId = int.Parse(r["DimissionPreAuditorId"].ToString());
            }
            if (null != r["DimissionPreAuditor"] && r["DimissionPreAuditor"].ToString() != "")
            {
                DimissionPreAuditor = r["DimissionPreAuditor"].ToString();
            }
            if (r["ITOperatorId"].ToString() != "")
            {
                ITOperatorId = int.Parse(r["ITOperatorId"].ToString());
            }
            if (null != r["ITOperator"] && r["ITOperator"].ToString() != "")
            {
                ITOperator = r["ITOperator"].ToString();
            }
        }
        #endregion
    }
}
