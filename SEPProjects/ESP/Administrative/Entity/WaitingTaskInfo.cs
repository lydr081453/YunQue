using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class WaitingTaskInfo
    {
        public WaitingTaskInfo()
        { }
        #region Model
        private int _id;
        private int _tasktype;
        private string _usercode;
        private string _username;
        private int _cardno;
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
        /// 任务类型：1.启动门卡，2.停用门卡
        /// </summary>
        public int TaskType
        {
            set { _tasktype = value; }
            get { return _tasktype; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
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
        /// 门卡号
        /// </summary>
        public int CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 地区ID， 1.北京，2.上海，3.广州，4.成都
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
        /// 操作人
        /// </summary>
        public int OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 操作部门
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
            if (r["TaskType"].ToString() != "")
            {
                _tasktype = int.Parse(r["TaskType"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            if (r["CardNo"].ToString() != "")
            {
                _cardno = int.Parse(r["CardNo"].ToString());
            }
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
            if (r["TaskType"].ToString() != "")
            {
                _tasktype = int.Parse(r["TaskType"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            if (r["CardNo"].ToString() != "")
            {
                _cardno = int.Parse(r["CardNo"].ToString());
            }
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