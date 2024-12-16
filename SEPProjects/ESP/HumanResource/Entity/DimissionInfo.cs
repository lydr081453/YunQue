using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.HumanResource.Entity
{
    public class DimissionInfo
    {
         public DimissionInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _username;
        private DateTime _joinjobdate = DateTime.Parse("1900-1-1 0:00:00");
        private int _companyid;
        private string _companyname;
        private int _departmentid;
        private string _departmentname;
        private int _groupid;
        private string _groupname;
        private DateTime _dimissiondate = DateTime.Parse("1900-1-1 0:00:00");
        private string _dimissioncause;
        private DateTime _createdate = DateTime.Parse("1900-1-1 0:00:00");
        private int _creater;
        private int _departmentmajordomo;
        private string _departmentmajordomoname;
        private int _departmentmajordomostatus;
        private string _departmentmajordomomemo;
        private DateTime _departmentmajordomodate = DateTime.Parse("1900-1-1 0:00:00");
        private int _groupmanager;
        private string _groupmanagername;
        private int _groupmanagerstatus;
        private string _groupmanagermemo;
        private DateTime _groupmanagerdate = DateTime.Parse("1900-1-1 0:00:00");
        private int _hrauditer;
        private string _hrauditername;
        private int _hrauditstatus;
        private string _hrauditmemo;
        private DateTime _hrauditdate = DateTime.Parse("1900-1-1 0:00:00");
        private int _snapshotsId;
        private bool _isFinish = true;
        private int _status=0;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 申请人id
        /// </summary>
        public int userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime joinJobDate
        {
            set { _joinjobdate = value; }
            get { return _joinjobdate; }
        }
        /// <summary>
        /// 公司
        /// </summary>
        public int companyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 公司
        /// </summary>
        public string companyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 所在部门
        /// </summary>
        public int departmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 所在部门
        /// </summary>
        public string departmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 所属团队
        /// </summary>
        public int groupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 所属团队
        /// </summary>
        public string groupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 拟定离职日期
        /// </summary>
        public DateTime dimissionDate
        {
            set { _dimissiondate = value; }
            get { return _dimissiondate; }
        }
        /// <summary>
        /// 离职申请说明
        /// </summary>
        public string dimissionCause
        {
            set { _dimissioncause = value; }
            get { return _dimissioncause; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int creater
        {
            set { _creater = value; }
            get { return _creater; }
        }
        /// <summary>
        /// 部门总监
        /// </summary>
        public int departmentMajordomo
        {
            set { _departmentmajordomo = value; }
            get { return _departmentmajordomo; }
        }
        /// <summary>
        /// 部门总监名称
        /// </summary>
        public string departmentMajordomoName
        {
            set { _departmentmajordomoname = value; }
            get { return _departmentmajordomoname; }
        }
        /// <summary>
        /// 部门总监批示
        /// </summary>
        public int departmentMajordomoStatus
        {
            set { _departmentmajordomostatus = value; }
            get { return _departmentmajordomostatus; }
        }
        /// <summary>
        /// 部门总监批示备注
        /// </summary>
        public string departmentMajordomoMemo
        {
            set { _departmentmajordomomemo = value; }
            get { return _departmentmajordomomemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime departmentMajordomoDate
        {
            set { _departmentmajordomodate = value; }
            get { return _departmentmajordomodate; }
        }
        /// <summary>
        /// 团队经理
        /// </summary>
        public int groupManager
        {
            set { _groupmanager = value; }
            get { return _groupmanager; }
        }
        /// <summary>
        /// 团队经理名称
        /// </summary>
        public string groupManagerName
        {
            set { _groupmanagername = value; }
            get { return _groupmanagername; }
        }
        /// <summary>
        /// 团队经理批示
        /// </summary>
        public int groupManagerStatus
        {
            set { _groupmanagerstatus = value; }
            get { return _groupmanagerstatus; }
        }
        /// <summary>
        /// 团队经理批示备注
        /// </summary>
        public string groupManagerMemo
        {
            set { _groupmanagermemo = value; }
            get { return _groupmanagermemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime groupManagerDate
        {
            set { _groupmanagerdate = value; }
            get { return _groupmanagerdate; }
        }
        /// <summary>
        /// 人力行政部
        /// </summary>
        public int hrAuditer
        {
            set { _hrauditer = value; }
            get { return _hrauditer; }
        }
        /// <summary>
        /// 人力行政部名称
        /// </summary>
        public string hrAuditerName
        {
            set { _hrauditername = value; }
            get { return _hrauditername; }
        }
        /// <summary>
        /// 人力行政部核准
        /// </summary>
        public int hrAuditStatus
        {
            set { _hrauditstatus = value; }
            get { return _hrauditstatus; }
        }
        /// <summary>
        /// 人力行政部核准备注
        /// </summary>
        public string hrAuditMemo
        {
            set { _hrauditmemo = value; }
            get { return _hrauditmemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime hrAuditDate
        {
            set { _hrauditdate = value; }
            get { return _hrauditdate; }
        }

        private string _userCode;
        public string userCode 
        {
            set { _userCode = value; }
            get { return _userCode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int snapshotsId
        {
            set { _snapshotsId = value; }
            get { return _snapshotsId; }
        }

        /// <summary>
        /// 是否离职手续完整
        /// </summary>
        public bool isFinish
        {
            set { _isFinish = value; }
            get { return _isFinish; }
        }

        /// <summary>
        /// 重新启用状态（0：离职；1：已重新启用）
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model


        public void PopupData(IDataReader r)
        {
            _id = int.Parse(r["id"].ToString());
            if (r["userId"].ToString() != "")
            {
                _userid = int.Parse(r["userId"].ToString());
            }
            _username = r["userName"].ToString();
            if (r["joinJobDate"].ToString() != "")
            {
                _joinjobdate = DateTime.Parse(r["joinJobDate"].ToString());
            }
            if (r["companyID"].ToString() != "")
            {
                _companyid = int.Parse(r["companyID"].ToString());
            }
            _companyname = r["companyName"].ToString();
            if (r["departmentID"].ToString() != "")
            {
                _departmentid = int.Parse(r["departmentID"].ToString());
            }
            _departmentname = r["departmentName"].ToString();
            if (r["groupID"].ToString() != "")
            {
                _groupid = int.Parse(r["groupID"].ToString());
            }
            _groupname = r["groupName"].ToString();
            if (r["dimissionDate"].ToString() != "")
            {
                _dimissiondate = DateTime.Parse(r["dimissionDate"].ToString());
            }
            _dimissioncause = r["dimissionCause"].ToString();
            if (r["createDate"].ToString() != "")
            {
                _createdate = DateTime.Parse(r["createDate"].ToString());
            }
            if (r["creater"].ToString() != "")
            {
                _creater = int.Parse(r["creater"].ToString());
            }
            if (r["departmentMajordomo"].ToString() != "")
            {
                _departmentmajordomo = int.Parse(r["departmentMajordomo"].ToString());
            }
            _departmentmajordomoname = r["departmentMajordomoName"].ToString();
            if (r["departmentMajordomoStatus"].ToString() != "")
            {
                _departmentmajordomostatus = int.Parse(r["departmentMajordomoStatus"].ToString());
            }
            _departmentmajordomomemo = r["departmentMajordomoMemo"].ToString();
            if (r["departmentMajordomoDate"].ToString() != "")
            {
                _departmentmajordomodate = DateTime.Parse(r["departmentMajordomoDate"].ToString());
            }
            if (r["groupManager"].ToString() != "")
            {
                _groupmanager = int.Parse(r["groupManager"].ToString());
            }
            _groupmanagername = r["groupManagerName"].ToString();
            if (r["groupManagerStatus"].ToString() != "")
            {
                _groupmanagerstatus = int.Parse(r["groupManagerStatus"].ToString());
            }
            _groupmanagermemo = r["groupManagerMemo"].ToString();
            if (r["groupManagerDate"].ToString() != "")
            {
                _groupmanagerdate = DateTime.Parse(r["groupManagerDate"].ToString());
            }
            if (r["hrAuditer"].ToString() != "")
            {
                _hrauditer = int.Parse(r["hrAuditer"].ToString());
            }
            _hrauditername = r["hrAuditerName"].ToString();
            if (r["hrAuditStatus"].ToString() != "")
            {
                _hrauditstatus = int.Parse(r["hrAuditStatus"].ToString());
            }
            _hrauditmemo = r["hrAuditMemo"].ToString();
            if (r["hrAuditDate"].ToString() != "")
            {
                _hrauditdate = DateTime.Parse(r["hrAuditDate"].ToString());
            }
            _userCode = r["userCode"].ToString();
            if (r["snapshotsId"].ToString() != "")
            {
                _snapshotsId = int.Parse(r["snapshotsId"].ToString());
            }
            if (r["isFinish"].ToString() != "")
            {
                if ((r["isFinish"].ToString() == "1") || (r["isFinish"].ToString().ToLower() == "true"))
                {
                    _isFinish = true;
                }
                else
                {
                    _isFinish = false;
                }
            }
            if (r["status"].ToString() != "")
            {
                _status = int.Parse(r["status"].ToString());
            }
        }
    }
}
