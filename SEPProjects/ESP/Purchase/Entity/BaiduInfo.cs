using System;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_Watch 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class BaiduInfo
    {
        public BaiduInfo()
        { }
        #region Model
        private int _id;
        private string _url;
        private string _title;
        private string _key;
        private int _linkpos;
        private string _urlhash;
        private string _urlfrom;
        private DateTime? _dtime;
        private string _uid;
        private string _namesite;
        private string _vartime;
        private string _mode;
        private string _zid;
        private string _doc;
        private string _ext1;
        private string _ext2;
        private string _ext3;
        private string _ext4;
        private string _ext5;
        private int _status;
        private string _keys;
        private DateTime? _createtime;
        private int _isappoint;
        private int _datatype;
        private string _desc;
        private string _deleted;
        private int _keywordid;

        /// <summary>
        /// 标识数据是否有效
        /// </summary>
        public string Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }
        /// <summary>
        /// 查询结果描述信息
        /// </summary>
        public string desc
        {
            set { _desc = value; }
            get { return _desc; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string key
        {
            set { _key = value; }
            get { return _key; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int linkPos
        {
            set { _linkpos = value; }
            get { return _linkpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string urlHash
        {
            set { _urlhash = value; }
            get { return _urlhash; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string urlFrom
        {
            set { _urlfrom = value; }
            get { return _urlfrom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dTime
        {
            set { _dtime = value; }
            get { return _dtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nameSite
        {
            set { _namesite = value; }
            get { return _namesite; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string varTime
        {
            set { _vartime = value; }
            get { return _vartime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mode
        {
            set { _mode = value; }
            get { return _mode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zid
        {
            set { _zid = value; }
            get { return _zid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string doc
        {
            set { _doc = value; }
            get { return _doc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext1
        {
            set { _ext1 = value; }
            get { return _ext1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext2
        {
            set { _ext2 = value; }
            get { return _ext2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext3
        {
            set { _ext3 = value; }
            get { return _ext3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext4
        {
            set { _ext4 = value; }
            get { return _ext4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ext5
        {
            set { _ext5 = value; }
            get { return _ext5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string keys
        {
            set { _keys = value; }
            get { return _keys; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isAppoint
        {
            set { _isappoint = value; }
            get { return _isappoint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int dataType
        {
            set { _datatype = value; }
            get { return _datatype; }
        }

        public int Keywordid
        {
            get { return _keywordid; }
            set { _keywordid = value; }
        }
        #endregion Model

    }
}
