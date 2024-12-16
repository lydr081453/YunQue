using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.Entity;
using ESP.Administrative.Common;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 考勤事宜信息表
    /// </summary>
    public class MattersInfo
    {
        public MattersInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private DateTime _begintime;
        private DateTime _endtime;
        private decimal _totalHours;
        private int _mattertype;
        private string _mattercontent;
        private int _matterstate;
        private int _approveid;
        private string _approvename;
        private string _approvedesc;
        private int _auditingid;
        private string _auditingname;
        private string _auditingdesc;
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operateorid;
        private int _projectid;
        private string _projectno;
        private string _username;
        private string _matterStateName;
        private string _matterTypeName;
        private bool _is;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 考勤事宜开始时间
        /// </summary>
        public DateTime BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 考勤事宜结束时间
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 事宜类型：1.病假、2.事假、3.年假、4.婚假、5.产假、6.丧家、7.出差、8.外出、9.调休
        /// </summary>
        public int MatterType
        {
            set { _mattertype = value; }
            get { return _mattertype; }
        }
        /// <summary>
        /// 事由类型描述
        /// </summary>
        public string MatterTypeName
        {
            get { return _matterTypeName; }
            set { _matterTypeName = value; }
        }
        /// <summary>
        /// 事宜内容
        /// </summary>
        public string MatterContent
        {
            set { _mattercontent = value; }
            get { return _mattercontent; }
        }
        /// <summary>
        /// 事宜状态,1为未提交状态，2为已审批通过，3待总监审批，4待人力审批, 5被驳回状态，6撤销
        /// </summary>
        public int MatterState
        {
            set { _matterstate = value; }
            get { return _matterstate; }
        }
        /// <summary>
        /// 事由审批状态
        /// </summary>
        public string MatterStateName
        {
            get { return _matterStateName; }
            set { _matterStateName = value; }
        }
        /// <summary>
        /// 总监审批备注
        /// </summary>
        public string Approvedesc
        {
            get { return _approvedesc; }
            set { _approvedesc = value; }
        }
        /// <summary>
        /// 总监审批人姓名
        /// </summary>
        public string Approvename
        {
            get { return _approvename; }
            set { _approvename = value; }
        }
        /// <summary>
        /// 总监审批人ID
        /// </summary>
        public int Approveid
        {
            get { return _approveid; }
            set { _approveid = value; }
        }
        /// <summary>
        /// 人力审核人ID
        /// </summary>
        public int Auditingid
        {
            get { return _auditingid; }
            set { _auditingid = value; }
        }
        /// <summary>
        /// 人力审核人姓名
        /// </summary>
        public string Auditingname
        {
            get { return _auditingname; }
            set { _auditingname = value; }
        }
        /// <summary>
        /// 人力审核人备注
        /// </summary>
        public string Auditingdesc
        {
            get { return _auditingdesc; }
            set { _auditingdesc = value; }
        }
        /// <summary>
        /// 是否已经删除
        /// </summary>
        public bool Deleted
        {
            set { _deleted = value; }
            get { return _deleted; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperateorID
        {
            set { _operateorid = value; }
            get { return _operateorid; }
        }
        /// <summary>
        /// 事由小时数，所有的事由都按小时保存
        /// </summary>
        public decimal TotalHours
        {
            get { return _totalHours; }
            set { _totalHours = value; }
        }
        /// <summary>
        /// 项目号ID
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectNo
        {
            set { _projectno = value; }
            get { return _projectno; }
        }

        public decimal AnnualHours { get; set; }
        public decimal AwardHours { get; set; }

        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            var objBeginTime = r["BeginTime"];
            if (!(objBeginTime is DBNull))
            {
                _begintime = (DateTime)objBeginTime;
            }
            var objEndTime = r["EndTime"];
            if (!(objEndTime is DBNull))
            {
                _endtime = (DateTime)objEndTime;
            }
            if (r["TotalHours"].ToString() != "")
            {
                _totalHours = decimal.Parse(r["TotalHours"].ToString());
            }
            if (r["AnnualHours"].ToString() != "")
            {
                AnnualHours = decimal.Parse(r["AnnualHours"].ToString());
            }
            if (r["AwardHours"].ToString() != "")
            {
                AwardHours = decimal.Parse(r["AwardHours"].ToString());
            }
            if (r["MatterType"].ToString() != "")
            {
                _mattertype = int.Parse(r["MatterType"].ToString());
            }
            _mattercontent = r["MatterContent"].ToString();
            if (r["MatterState"].ToString() != "")
            {
                _matterstate = int.Parse(r["MatterState"].ToString());
            }
            if (r["ProjectID"].ToString() != "")
            {
                _projectid = int.Parse(r["ProjectID"].ToString());
            }
            _projectno = r["ProjectNo"].ToString();
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approvename = r["ApproveName"].ToString();
            _approvedesc = r["ApproveDesc"].ToString();
            if (r["AuditingID"].ToString() != "")
            {
                _auditingid = int.Parse(r["AuditingID"].ToString());
            }
            _auditingname = r["AuditingName"].ToString();
            _auditingdesc = r["AuditingDesc"].ToString();
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    _deleted = true;
                }
                else
                {
                    _deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                _updatetime = (DateTime)objUpdateTime;
            }
            if (r["OperateorID"].ToString() != "")
            {
                _operateorid = int.Parse(r["OperateorID"].ToString());
            }
            UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(_userid);
            _username = userInfo.FullNameCN;
            if (_matterstate == Status.MattersState_NoSubmit)
            {
                _matterStateName = "未提交";
            }
            else if (_matterstate == Status.MattersState_Passed)
            {
                _matterStateName = "审批通过";
            }
            else if (_matterstate == Status.MattersState_WaitDirector || _matterstate == Status.MattersState_WaitHR)
            {
                _matterStateName = "等待审批";
            }

            if (_mattertype == Status.MattersType_Annual)
            {
                _matterTypeName = "年假";
            }
            else if (_mattertype == Status.MattersType_Bereavement)
            {
                _matterTypeName = "丧假";
            }
            else if (_mattertype == Status.MattersType_Leave)
            {
                _matterTypeName = "事假";
            }
            else if (_mattertype == Status.MattersType_Marriage)
            {
                _matterTypeName = "婚假";
            }
            else if (_mattertype == Status.MattersType_Maternity)
            {
                _matterTypeName = "产假";
            }
            else if (_mattertype == Status.MattersType_PeiChanJia)
            {
                _matterTypeName = "陪产假";
            }
            else if (_mattertype == Status.MattersType_OffTune)
            {
                _matterTypeName = "调休";
            }
            else if (_mattertype == Status.MattersType_Other)
            {
                _matterTypeName = "其他";
            }
            else if (_mattertype == Status.MattersType_Out)
            {
                _matterTypeName = "外出";
            }
            else if (_mattertype == Status.MattersType_Sick)
            {
                _matterTypeName = "病假";
            }
            else if (_mattertype == Status.MattersType_Travel)
            {
                _matterTypeName = "出差";
            }

        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            var objBeginTime = r["BeginTime"];
            if (!(objBeginTime is DBNull))
            {
                _begintime = (DateTime)objBeginTime;
            }
            var objEndTime = r["EndTime"];
            if (!(objEndTime is DBNull))
            {
                _endtime = (DateTime)objEndTime;
            }
            if (r["TotalHours"].ToString() != "")
            {
                _totalHours = decimal.Parse(r["TotalHours"].ToString());
            }
            if (r["AnnualHours"].ToString() != "")
            {
                AnnualHours = decimal.Parse(r["AnnualHours"].ToString());
            }
            if (r["AwardHours"].ToString() != "")
            {
                AwardHours = decimal.Parse(r["AwardHours"].ToString());
            }
            if (r["MatterType"].ToString() != "")
            {
                _mattertype = int.Parse(r["MatterType"].ToString());
            }
            _mattercontent = r["MatterContent"].ToString();
            if (r["MatterState"].ToString() != "")
            {
                _matterstate = int.Parse(r["MatterState"].ToString());
            }
            if (r["ProjectID"].ToString() != "")
            {
                _projectid = int.Parse(r["ProjectID"].ToString());
            }
            _projectno = r["ProjectNo"].ToString();
            if (r["ApproveID"].ToString() != "")
            {
                _approveid = int.Parse(r["ApproveID"].ToString());
            }
            _approvename = r["ApproveName"].ToString();
            _approvedesc = r["ApproveDesc"].ToString();
            if (r["AuditingID"].ToString() != "")
            {
                _auditingid = int.Parse(r["AuditingID"].ToString());
            }
            _auditingname = r["AuditingName"].ToString();
            _auditingdesc = r["AuditingDesc"].ToString();
            if (r["Deleted"].ToString() != "")
            {
                if ((r["Deleted"].ToString() == "1") || (r["Deleted"].ToString().ToLower() == "true"))
                {
                    _deleted = true;
                }
                else
                {
                    _deleted = false;
                }
            }
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                _updatetime = (DateTime)objUpdateTime;
            }
            if (r["OperateorID"].ToString() != "")
            {
                _operateorid = int.Parse(r["OperateorID"].ToString());
            }
            UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(_userid);
            _username = userInfo.FullNameCN;
            if (_matterstate == Status.MattersState_NoSubmit)
            {
                _matterStateName = "未提交";
            }
            else if (_matterstate == Status.MattersState_Passed)
            {
                _matterStateName = "审批通过";
            }
            else if (_matterstate == Status.MattersState_WaitDirector || _matterstate == Status.MattersState_WaitHR)
            {
                _matterStateName = "等待审批";
            }

            if (_mattertype == Status.MattersType_Annual)
            {
                _matterTypeName = "年假";
            }
            else if (_mattertype == Status.MattersType_Bereavement)
            {
                _matterTypeName = "丧假";
            }
            else if (_mattertype == Status.MattersType_Leave)
            {
                _matterTypeName = "事假";
            }
            else if (_mattertype == Status.MattersType_Marriage)
            {
                _matterTypeName = "婚假";
            }
            else if (_mattertype == Status.MattersType_Maternity)
            {
                _matterTypeName = "产假";
            }
            else if (_mattertype == Status.MattersType_PeiChanJia)
            {
                _matterTypeName = "陪产假";
            }
            else if (_mattertype == Status.MattersType_OffTune)
            {
                _matterTypeName = "调休";
            }
            else if (_mattertype == Status.MattersType_Other)
            {
                _matterTypeName = "其他";
            }
            else if (_mattertype == Status.MattersType_Out)
            {
                _matterTypeName = "外出";
            }
            else if (_mattertype == Status.MattersType_Sick)
            {
                _matterTypeName = "病假";
            }
            else if (_mattertype == Status.MattersType_Travel)
            {
                _matterTypeName = "出差";
            }
        }
    }
}