using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class BaseEntityInfo
    {
        private bool _deleted;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int _operateorid;
        private int _operateordept;
        private int _sort;

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
        public int OperateorID
        {
            set { _operateorid = value; }
            get { return _operateorid; }
        }
        /// <summary>
        /// 操作部门
        /// </summary>
        public int OperateorDept
        {
            set { _operateordept = value; }
            get { return _operateordept; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
    }
}
