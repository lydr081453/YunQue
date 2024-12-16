using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类TypeInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class TypeInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInfo"/> class.
        /// </summary>
        public TypeInfo()
        { }

        #region Model
        private int _typeid;
        private string _typename;
        private string _auditorid;
        private int _parentId;
        private int _typelevel;
        private int _shauditorid;
        private int _gzauditorid;
        private int _status;
        private int _operationflow;
        private int _IsNeedHQCheck;

        public int BJPaymentUserID { get; set; }
        public int SHPaymentUserID { get; set; }
        public int GZPaymentUserID { get; set; }
        public int Sort { get; set; }

        /// <summary>
        /// Gets or sets the typeid.
        /// </summary>
        /// <value>The typeid.</value>
        public int typeid
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        
        /// <summary>
        /// Gets or sets the typename.
        /// </summary>
        /// <value>The typename.</value>
        public string typename
        {
            set { _typename = value; }
            get { return _typename; }
        }

        /// <summary>
        /// Gets or sets the auditorid.
        /// </summary>
        /// <value>The auditorid.</value>
        public string auditorid
        {
            set { _auditorid = value; }
            get { return _auditorid; }
        }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        public int parentId
        {
            set { _parentId = value; }
            get { return _parentId; }
        }

        /// <summary>
        /// Gets or sets the typelevel.
        /// </summary>
        /// <value>The typelevel.</value>
        public int typelevel
        {
            set { _typelevel = value; }
            get { return _typelevel; }
        }

        /// <summary>
        /// Gets or sets the shauditorid.
        /// </summary>
        /// <value>The shauditorid.</value>
        public int shauditorid
        {
            set { _shauditorid = value; }
            get { return _shauditorid; }
        }

        /// <summary>
        /// Gets or sets the gzauditorid.
        /// </summary>
        /// <value>The gzauditorid.</value>
        public int gzauditorid
        {
            set { _gzauditorid = value; }
            get { return _gzauditorid; }
        }

        /// <summary>
        /// 物料类别的状态，0为停用，1为可用
        /// </summary>
        /// <value>The gzauditorid.</value>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 物料类别的流转，0为普通第三方，1为媒介稿费报销，2为媒体广告购买
        /// </summary>
        /// <value>The gzauditorid.</value>
        public int operationflow
        {
            set { _operationflow = value; }
            get { return _operationflow; }
        }

        /// <summary>
        /// 是否需要集团物料审核人复查，0为不需要，1为需要
        /// </summary>
        /// <value>The gzauditorid.</value>
        public int IsNeedHQCheck
        {
            set { _IsNeedHQCheck = value; }
            get { return _IsNeedHQCheck; }
        }

        public string PTBId { get; set; }
        #endregion Model

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["typeid"] && r["typeid"].ToString() != "")
            {
                typeid = int.Parse(r["typeid"].ToString());
            }
            typename = r["typename"].ToString();
            auditorid = r["auditorid"].ToString();
            if (null != r["parentId"] && r["parentId"].ToString() != "")
            {
                parentId = int.Parse(r["parentId"].ToString());
            }
            if (null != r["typelevel"] && r["typelevel"].ToString() != "")
            {
                typelevel = int.Parse(r["typelevel"].ToString());
            }
            if (null != r["shauditorid"] && r["shauditorid"].ToString() != "")
            {
                shauditorid = int.Parse(r["shauditorid"].ToString());
            }
            if (null != r["gzauditorid"] && r["gzauditorid"].ToString() != "")
            {
                gzauditorid = int.Parse(r["gzauditorid"].ToString());
            }
            if (null != r["status"] && r["status"].ToString() != "")
            {
                status = int.Parse(r["status"].ToString());
            }
            if (null != r["operationflow"] && r["operationflow"].ToString() != "")
            {
                operationflow = int.Parse(r["operationflow"].ToString());
            }
            if (null != r["IsNeedHQCheck"] && r["IsNeedHQCheck"].ToString() != "")
            {
                IsNeedHQCheck = int.Parse(r["IsNeedHQCheck"].ToString());
            }
            PTBId = r["PTBId"].ToString();
        }
    }
}