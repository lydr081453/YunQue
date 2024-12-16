using System;
using System.Web;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class DataInfo
    {
        private int _id;
        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _dataType;
        /// <summary>
        /// T_DataType自增ID
        /// </summary>
        public int DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        private int _dataId;
        /// <summary>
        /// 数据ID（如：T_GeneralInfo的ID、T_Recipient的ID）
        /// </summary>
        public int DataId
        {
            get { return _dataId; }
            set { _dataId = value; }
        }
    }

    public class DataTypeInfo
    {
        private int _id;
        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _dataTypeName;
        /// <summary>
        /// 数据类型名称(如：PR,GR)
        /// </summary>
        public string DataTypeName
        {
            get { return _dataTypeName; }
            set { _dataTypeName = value; }
        }
    }

    public class DataPermissionInfo
    {
        private int _id;
        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _dataInfoId;
        /// <summary>
        /// T_DataInfo的自增ID
        /// </summary>
        public int DataInfoId
        {
            get { return _dataInfoId; }
            set { _dataInfoId = value; }
        }

        private int _userId;
        /// <summary>
        /// 人员ID
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private bool _isEditor;
        /// <summary>
        /// 是否为编辑者
        /// </summary>
        public bool IsEditor
        {
            get { return _isEditor; }
            set { _isEditor = value; }
        }

        private bool _isViewer;
        /// <summary>
        /// 是否为查看者
        /// </summary>
        public bool IsViewer
        {
            get { return _isViewer; }
            set { _isViewer = value; }
        }
    }
}
