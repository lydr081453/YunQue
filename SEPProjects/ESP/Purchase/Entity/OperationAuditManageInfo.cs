//using System.Data;

//namespace ESP.Purchase.Entity
//{
//    /// <summary>
//    /// 实体类T_OperationAuditManage 。(属性说明自动提取数据库字段的描述信息)
//    /// </summary>
//    public class OperationAuditManageInfo
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="OperationAuditManageInfo"/> class.
//        /// </summary>
//        public OperationAuditManageInfo()
//        { }

//        #region Model
//        private int _id;
//        private int _depid;
//        private int _directorid;
//        private string _directorname;
//        private int _managerid;
//        private string _managername;
//        private int _ceoid;
//        private string _ceoname;

//        /// <summary>
//        /// 流水号
//        /// </summary>
//        /// <value>The id.</value>
//        public int Id
//        {
//            set { _id = value; }
//            get { return _id; }
//        }

//        /// <summary>
//        /// 部门编号
//        /// </summary>
//        /// <value>The dep id.</value>
//        public int DepId
//        {
//            set { _depid = value; }
//            get { return _depid; }
//        }

//        /// <summary>
//        /// 总监编号
//        /// </summary>
//        /// <value>The director id.</value>
//        public int DirectorId
//        {
//            set { _directorid = value; }
//            get { return _directorid; }
//        }

//        /// <summary>
//        /// 总监姓名
//        /// </summary>
//        /// <value>The name of the director.</value>
//        public string DirectorName
//        {
//            set { _directorname = value; }
//            get { return _directorname; }
//        }

//        /// <summary>
//        /// 总经理编号
//        /// </summary>
//        /// <value>The manager id.</value>
//        public int ManagerId
//        {
//            set { _managerid = value; }
//            get { return _managerid; }
//        }

//        /// <summary>
//        /// 总经理姓名
//        /// </summary>
//        /// <value>The name of the manager.</value>
//        public string ManagerName
//        {
//            set { _managername = value; }
//            get { return _managername; }
//        }

//        /// <summary>
//        /// CEO编号
//        /// </summary>
//        /// <value>The CEO id.</value>
//        public int CEOId
//        {
//            set { _ceoid = value; }
//            get { return _ceoid; }
//        }

//        /// <summary>
//        /// CEO姓名
//        /// </summary>
//        /// <value>The name of the CEO.</value>
//        public string CEOName
//        {
//            set { _ceoname = value; }
//            get { return _ceoname; }
//        }

//        private int _FAId;
//        /// <summary>
//        /// FA编号
//        /// </summary>
//        public int FAId
//        {
//            get { return _FAId; }
//            set { _FAId = value; }
//        }

//        private string _FAName;
//        /// <summary>
//        /// FA姓名
//        /// </summary>
//        public string FAName
//        {
//            get { return _FAName; }
//            set { _FAName = value; }
//        }

//        #endregion Model

//        #region method
//        /// <summary>
//        /// Popups the data.
//        /// </summary>
//        /// <param name="r">The r.</param>
//        public void PopupData(IDataReader r)
//        {
//            if (null != r["id"] && r["id"].ToString() != "")
//            {
//                _id = int.Parse(r["id"].ToString());
//            }
//            if (null != r["DepId"] && r["DepId"].ToString() != "")
//            {
//                _depid = int.Parse(r["DepId"].ToString());
//            }
//            if (null != r["DirectorId"] && r["DirectorId"].ToString() != "")
//            {
//                _directorid = int.Parse(r["DirectorId"].ToString());
//            }
//            _directorname = r["DirectorName"].ToString();
//            if (null != r["ManagerId"] && r["ManagerId"].ToString() != "")
//            {
//                _managerid = int.Parse(r["ManagerId"].ToString());
//            }
//            _managername = r["managername"].ToString();
//            if (null != r["CEOId"] && r["CEOId"].ToString() != "")
//            {
//                _ceoid = int.Parse(r["CEOId"].ToString());
//            }
//            _ceoname = r["ceoname"].ToString();
//            if (null != r["FAId"] && r["FAId"].ToString() != "")
//            {
//                _FAId = int.Parse(r["FAId"].ToString());
//            }
//            _FAName = r["FAName"].ToString();
//        }
//        #endregion
//    }
//}