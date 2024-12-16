using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class OperationAuditManageInfo
    {
        public OperationAuditManageInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private int _teamleaderid;
        private string _teamleadername;
        private int _hradminid;
        private string _hradminname;
        private int _managerid;
        private string _managername;
        private int _statisticianid;
        private string _statisticianname;
        private int _areaid;
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operatorid;
        private int _operatordept;
        private int _sort;
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
        /// 部门审批人，可能是TeamLeader也可能是总监
        /// </summary>
        public int TeamLeaderID
        {
            set { _teamleaderid = value; }
            get { return _teamleaderid; }
        }
        /// <summary>
        /// 部门审批人姓名
        /// </summary>
        public string TeamLeaderName
        {
            set { _teamleadername = value; }
            get { return _teamleadername; }
        }
        /// <summary>
        /// 人力审批人
        /// </summary>
        public int HRAdminID
        {
            set { _hradminid = value; }
            get { return _hradminid; }
        }
        /// <summary>
        /// 人力审批人姓名
        /// </summary>
        public string HRAdminName
        {
            set { _hradminname = value; }
            get { return _hradminname; }
        }
        /// <summary>
        /// 团队审批人，一般都是团队经理
        /// </summary>
        public int ManagerID
        {
            set { _managerid = value; }
            get { return _managerid; }
        }
        /// <summary>
        /// 团队审批人姓名
        /// </summary>
        public string ManagerName
        {
            set { _managername = value; }
            get { return _managername; }
        }
        /// <summary>
        /// 考勤统计员ID，一般都是前台
        /// </summary>
        public int StatisticianID
        {
            set { _statisticianid = value; }
            get { return _statisticianid; }
        }
        /// <summary>
        /// 考勤统计员姓名
        /// </summary>
        public string StatisticianName
        {
            set { _statisticianname = value; }
            get { return _statisticianname; }
        }
        /// <summary>
        /// 各地区分公司编号，北京19，上海17，广州18
        /// </summary>
        public int AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 有效性标识
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
        public int OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 操作人部门
        /// </summary>
        public int OperatorDept
        {
            set { _operatordept = value; }
            get { return _operatordept; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
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
            if (r["TeamLeaderID"].ToString() != "")
            {
                _teamleaderid = int.Parse(r["TeamLeaderID"].ToString());
            }
            _teamleadername = r["TeamLeaderName"].ToString();
            if (r["HRAdminID"].ToString() != "")
            {
                _hradminid = int.Parse(r["HRAdminID"].ToString());
            }
            _hradminname = r["HRAdminName"].ToString();
            if (r["ManagerID"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerID"].ToString());
            }
            _managername = r["ManagerName"].ToString();
            if (r["StatisticianID"].ToString() != "")
            {
                _statisticianid = int.Parse(r["StatisticianID"].ToString());
            }
            _statisticianname = r["StatisticianName"].ToString();
            if (r["AreaID"].ToString() != "")
            {
                _areaid = int.Parse(r["AreaID"].ToString());
            }
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
            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            if (r["OperatorDept"].ToString() != "")
            {
                _operatordept = int.Parse(r["OperatorDept"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                _sort = int.Parse(r["Sort"].ToString());
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
            if (r["TeamLeaderID"].ToString() != "")
            {
                _teamleaderid = int.Parse(r["TeamLeaderID"].ToString());
            }
            _teamleadername = r["TeamLeaderName"].ToString();
            if (r["HRAdminID"].ToString() != "")
            {
                _hradminid = int.Parse(r["HRAdminID"].ToString());
            }
            _hradminname = r["HRAdminName"].ToString();
            if (r["ManagerID"].ToString() != "")
            {
                _managerid = int.Parse(r["ManagerID"].ToString());
            }
            _managername = r["ManagerName"].ToString();
            if (r["StatisticianID"].ToString() != "")
            {
                _statisticianid = int.Parse(r["StatisticianID"].ToString());
            }
            _statisticianname = r["StatisticianName"].ToString();
            if (r["AreaID"].ToString() != "")
            {
                _areaid = int.Parse(r["AreaID"].ToString());
            }
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
            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            if (r["OperatorDept"].ToString() != "")
            {
                _operatordept = int.Parse(r["OperatorDept"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                _sort = int.Parse(r["Sort"].ToString());
            }
        }
    }
}