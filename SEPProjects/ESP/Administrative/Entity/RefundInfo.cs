using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class RefundInfo
    {
        public RefundInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private DateTime? _begintime;
        private DateTime? _endtime;
        private string _beginoperator;
        private string _endoperator;
        private decimal? _cost;
        private int? _type;
        private int? _status;
        private string _creator;
        private string _creatorip;
        private DateTime? _createtime;
        private string _lastupdater;
        private string _lastupdaterip;
        private DateTime? _lastupdatetime;
        private bool _isdeleted;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 开始操作人
        /// </summary>
        public string BeginOperator
        {
            set { _beginoperator = value; }
            get { return _beginoperator; }
        }
        /// <summary>
        /// 结束操作人
        /// </summary>
        public string EndOperator
        {
            set { _endoperator = value; }
            get { return _endoperator; }
        }
        /// <summary>
        /// 报销费用
        /// </summary>
        public decimal? Cost
        {
            set { _cost = value; }
            get { return _cost; }
        }
        /// <summary>
        /// 报销类型（1、笔记本报销）
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 报销状态
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建人IP
        /// </summary>
        public string CreatorIP
        {
            set { _creatorip = value; }
            get { return _creatorip; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastUpdater
        {
            set { _lastupdater = value; }
            get { return _lastupdater; }
        }
        /// <summary>
        /// 最后修改人IP
        /// </summary>
        public string LastUpdaterIP
        {
            set { _lastupdaterip = value; }
            get { return _lastupdaterip; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 有效性标识
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        public string ProductName { get; set; }
        public string ProductNo { get; set; }

        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["Id"].ToString() != "")
            {
                _id = int.Parse(r["Id"].ToString());
            }
            if (r["UserId"].ToString() != "")
            {
                _userid = int.Parse(r["UserId"].ToString());
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
            _beginoperator = r["BeginOperator"].ToString();
            _endoperator = r["EndOperator"].ToString();
            if (r["Cost"].ToString() != "")
            {
                _cost = decimal.Parse(r["Cost"].ToString());
            }
            if (r["Type"].ToString() != "")
            {
                _type = int.Parse(r["Type"].ToString());
            }
            if (r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            _creator = r["Creator"].ToString();
            ProductName = r["ProductName"].ToString();
            ProductNo = r["ProductNo"].ToString();
            Remark = r["Remark"].ToString();
            _creatorip = r["CreatorIP"].ToString();
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            _lastupdater = r["LastUpdater"].ToString();
            _lastupdaterip = r["LastUpdaterIP"].ToString();
            var objLastUpdateTime = r["LastUpdateTime"]; 
            if (!(objLastUpdateTime is DBNull))
            {
                _lastupdatetime = (DateTime)objLastUpdateTime;
            }
            if (r["IsDeleted"].ToString() != "")
            {
                if ((r["IsDeleted"].ToString() == "1") || (r["IsDeleted"].ToString().ToLower() == "true"))
                {
                    _isdeleted = true;
                }
                else
                {
                    _isdeleted = false;
                }
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["Id"].ToString() != "")
            {
                _id = int.Parse(r["Id"].ToString());
            }
            if (r["UserId"].ToString() != "")
            {
                _userid = int.Parse(r["UserId"].ToString());
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
            _beginoperator = r["BeginOperator"].ToString();
            _endoperator = r["EndOperator"].ToString();
            if (r["Cost"].ToString() != "")
            {
                _cost = decimal.Parse(r["Cost"].ToString());
            }
            if (r["Type"].ToString() != "")
            {
                _type = int.Parse(r["Type"].ToString());
            }
            if (r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            _creator = r["Creator"].ToString();
            _creatorip = r["CreatorIP"].ToString();
            ProductName = r["ProductName"].ToString();
            ProductNo = r["ProductNo"].ToString();
            Remark = r["Remark"].ToString();
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                _createtime = (DateTime)objCreateTime;
            }
            _lastupdater = r["LastUpdater"].ToString();
            _lastupdaterip = r["LastUpdaterIP"].ToString();
            var objLastUpdateTime = r["LastUpdateTime"];
            if (!(objLastUpdateTime is DBNull))
            {
                _lastupdatetime = (DateTime)objLastUpdateTime;
            }
            if (r["IsDeleted"].ToString() != "")
            {
                if ((r["IsDeleted"].ToString() == "1") || (r["IsDeleted"].ToString().ToLower() == "true"))
                {
                    _isdeleted = true;
                }
                else
                {
                    _isdeleted = false;
                }
            }
        }
    }
}