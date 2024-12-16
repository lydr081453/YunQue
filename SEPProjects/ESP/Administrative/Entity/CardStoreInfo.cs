using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class CardStoreInfo
    {
        public CardStoreInfo()
        { }
        #region Model
        private int _id;
        private int _cardno;
        private int _state;
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
        /// 卡号
        /// </summary>
        public int CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 状态,0未使用,1已使用,2作废
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 地区ID，北京、上海、广州（分别用分公司的ID来表示）
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
        public int sort
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
            if (r["CardNo"].ToString() != "")
            {
                _cardno = int.Parse(r["CardNo"].ToString());
            }
            if (r["State"].ToString() != "")
            {
                _state = int.Parse(r["State"].ToString());
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
            if (r["sort"].ToString() != "")
            {
                _sort = int.Parse(r["sort"].ToString());
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
            if (r["CardNo"].ToString() != "")
            {
                _cardno = int.Parse(r["CardNo"].ToString());
            }
            if (r["State"].ToString() != "")
            {
                _state = int.Parse(r["State"].ToString());
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
            if (r["sort"].ToString() != "")
            {
                _sort = int.Parse(r["sort"].ToString());
            }
        }
    }
}