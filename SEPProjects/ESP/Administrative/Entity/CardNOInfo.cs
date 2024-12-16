using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 门卡信息实体类
    /// </summary>
    public class CardNOInfo : BaseEntityInfo
    {
        public CardNOInfo()
        { }
        #region Model
        private int _id;
        private string _cardno;
        private int _userid;
        private string _usercode;
        private string _username;
        private int _departmentid;
        private string _departmentname;
        private bool _isenable;
        private DateTime? _enabletime;
        private DateTime? _unenabletime;
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operateorid;
        private int _operateordept;
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
        /// 
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 用户系统编号
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
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
        /// 部门编号
        /// </summary>
        public int DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool ISEnable
        {
            set { _isenable = value; }
            get { return _isenable; }
        }
        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime? EnableTime
        {
            set { _enabletime = value; }
            get { return _enabletime; }
        }
        /// <summary>
        /// 停用时间
        /// </summary>
        public DateTime? UnEnableTime
        {
            set { _unenabletime = value; }
            get { return _unenabletime; }
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
            _cardno = r["CardNo"].ToString();
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            if (r["DepartmentID"].ToString() != "")
            {
                _departmentid = int.Parse(r["DepartmentID"].ToString());
            }
            _departmentname = r["DepartmentName"].ToString();
            if (r["ISEnable"].ToString() != "")
            {
                if ((r["ISEnable"].ToString() == "1") || (r["ISEnable"].ToString().ToLower() == "true"))
                {
                    _isenable = true;
                }
                else
                {
                    _isenable = false;
                }
            }
            var objEnableTime = r["EnableTime"];
            if (!(objEnableTime is DBNull))
            {
                _enabletime = (DateTime)objEnableTime;
            }
            var objUnEnableTime = r["UnEnableTime"];
            if (!(objEnableTime is DBNull))
            {
                _unenabletime = (DateTime)objUnEnableTime;
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
            if (r["OperateorDept"].ToString() != "")
            {
                _operateordept = int.Parse(r["OperateorDept"].ToString());
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
            _cardno = r["CardNo"].ToString();
            if (r["UserID"].ToString() != "")
            {
                _userid = int.Parse(r["UserID"].ToString());
            }
            _usercode = r["UserCode"].ToString();
            _username = r["UserName"].ToString();
            if (r["DepartmentID"].ToString() != "")
            {
                _departmentid = int.Parse(r["DepartmentID"].ToString());
            }
            _departmentname = r["DepartmentName"].ToString();
            if (r["ISEnable"].ToString() != "")
            {
                if ((r["ISEnable"].ToString() == "1") || (r["ISEnable"].ToString().ToLower() == "true"))
                {
                    _isenable = true;
                }
                else
                {
                    _isenable = false;
                }
            }
            var objEnableTime = r["EnableTime"];
            if (!(objEnableTime is DBNull))
            {
                _enabletime = (DateTime)objEnableTime;
            }
            var objUnEnableTime = r["UnEnableTime"];
            if (!(objEnableTime is DBNull))
            {
                _unenabletime = (DateTime)objUnEnableTime;
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
            if (r["OperateorDept"].ToString() != "")
            {
                _operateordept = int.Parse(r["OperateorDept"].ToString());
            }
            if (r["Sort"].ToString() != "")
            {
                _sort = int.Parse(r["Sort"].ToString());
            }
        }
	}
}