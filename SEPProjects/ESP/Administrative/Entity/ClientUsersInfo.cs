using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 实体类AD_ClientUsers 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class ClientUsersInfo
    {
        public ClientUsersInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _username;
        private int _faid;
        private string _faname;
        private DateTime _begintime;
        private DateTime _endtime;
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operateorid;
        private int _operateordepid;
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
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// FA编号
        /// </summary>
        public int FAID
        {
            set { _faid = value; }
            get { return _faid; }
        }
        /// <summary>
        /// FA姓名
        /// </summary>
        public string FAName
        {
            set { _faname = value; }
            get { return _faname; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
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
        /// 最后一次修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public int OperateorID
        {
            set { _operateorid = value; }
            get { return _operateorid; }
        }
        /// <summary>
        /// 操作人部门
        /// </summary>
        public int OperateorDepId
        {
            set { _operateordepid = value; }
            get { return _operateordepid; }
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
            _username = r["UserName"].ToString();
            if (r["FAID"].ToString() != "")
            {
                _faid = int.Parse(r["FAID"].ToString());
            }
            _faname = r["FAName"].ToString();
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
            if (r["OperateorDepId"].ToString() != "")
            {
                _operateordepid = int.Parse(r["OperateorDepId"].ToString());
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
            _username = r["UserName"].ToString();
            if (r["FAID"].ToString() != "")
            {
                _faid = int.Parse(r["FAID"].ToString());
            }
            _faname = r["FAName"].ToString();
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
            if (r["OperateorDepId"].ToString() != "")
            {
                _operateordepid = int.Parse(r["OperateorDepId"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                _sort = int.Parse(r["Sort"].ToString());
            }
        }
    }
}