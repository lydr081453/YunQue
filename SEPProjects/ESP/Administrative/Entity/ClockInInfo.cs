using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 日常打卡记录表
    /// </summary>
    public class ClockInInfo : BaseEntityInfo
    {
        public ClockInInfo()
        { }
        #region Model
        private int _id;
        private string _cardno;
        private DateTime _readtime;
        private bool _inorout;
        private string _doorname;
        private string _usercode;
        private bool _deleted;
        private DateTime _updatetime;
        private int _operatorid;
        private string _operatorname;
        private string _remark;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 门卡号
        /// </summary>
        public string CardNO
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 打卡时间
        /// </summary>
        public DateTime ReadTime
        {
            set { _readtime = value; }
            get { return _readtime; }
        }
        /// <summary>
        /// 是进还是出， 1.表示进门，0.表示出门
        /// </summary>
        public bool InOrOut
        {
            set { _inorout = value; }
            get { return _inorout; }
        }
        /// <summary>
        /// 出入门名称
        /// </summary>
        public string DoorName
        {
            set { _doorname = value; }
            get { return _doorname; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode
        {
            get { return _usercode; }
            set { _usercode = value; }
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
        /// 最后修改时间
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
        /// 操作人姓名
        /// </summary>
        public string OperatorName
        {
            set { _operatorname = value; }
            get { return _operatorname; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
            _cardno = r["CardNO"].ToString();
            var objReadTime = r["ReadTime"];
            if (!(objReadTime is DBNull))
            {
                _readtime = (DateTime)objReadTime;
            }
            if (r["InOrOut"].ToString() != "")
            {
                if ((r["InOrOut"].ToString() == "1") || (r["InOrOut"].ToString().ToLower() == "true"))
                {
                    _inorout = true;
                }
                else
                {
                    _inorout = false;
                }
            }

            _doorname = r["DoorName"].ToString();
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                CreateTime = (DateTime)objCreateTime;
            }
            if (r["UserCode"].ToString() != "")
            {
                _usercode = r["UserCode"].ToString();
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
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                _updatetime = (DateTime)objUpdateTime;
            }
            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            _operatorname = r["OperatorName"].ToString();
            _remark = r["Remark"].ToString();
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
            _cardno = r["CardNO"].ToString();
            var objReadTime = r["ReadTime"];
            if (!(objReadTime is DBNull))
            {
                _readtime = (DateTime)objReadTime;
            }
            if (r["InOrOut"].ToString() != "")
            {
                if ((r["InOrOut"].ToString() == "1") || (r["InOrOut"].ToString().ToLower() == "true"))
                {
                    _inorout = true;
                }
                else
                {
                    _inorout = false;
                }
            }

            _doorname = r["DoorName"].ToString();
            var objCreateTime = r["CreateTime"];
            if (!(objCreateTime is DBNull))
            {
                CreateTime = (DateTime)objCreateTime;
            }
            if (r["UserCode"].ToString() != "")
            {
                _usercode = r["UserCode"].ToString();
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
            var objUpdateTime = r["UpdateTime"];
            if (!(objUpdateTime is DBNull))
            {
                _updatetime = (DateTime)objUpdateTime;
            }
            if (r["OperatorID"].ToString() != "")
            {
                _operatorid = int.Parse(r["OperatorID"].ToString());
            }
            _operatorname = r["OperatorName"].ToString();
            _remark = r["Remark"].ToString();
        }
    }
}