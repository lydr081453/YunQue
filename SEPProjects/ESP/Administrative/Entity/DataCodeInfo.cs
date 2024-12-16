using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class DataCodeInfo
    {
        public DataCodeInfo()
        { }
        #region Model
        private int _datacodeid;
        private string _name;
        private string _type;
        private string _code;
        private bool _deleted;
        private int _sort;
        /// <summary>
        /// 
        /// </summary>
        public int DataCodeID
        {
            set { _datacodeid = value; }
            get { return _datacodeid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 有效性
        /// </summary>
        public bool Deleted
        {
            set { _deleted = value; }
            get { return _deleted; }
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
            if (r["DataCodeID"].ToString() != "")
            {
                _datacodeid = int.Parse(r["DataCodeID"].ToString());
            }
            _name = r["Name"].ToString();
            _type = r["Type"].ToString();
            _code = r["Code"].ToString();
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
            if (r["DataCodeID"].ToString() != "")
            {
                _datacodeid = int.Parse(r["DataCodeID"].ToString());
            }
            _name = r["Name"].ToString();
            _type = r["Type"].ToString();
            _code = r["Code"].ToString();
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

            if (r["Sort"].ToString() != "")
            {
                _sort = int.Parse(r["Sort"].ToString());
            }
        }
    }
}