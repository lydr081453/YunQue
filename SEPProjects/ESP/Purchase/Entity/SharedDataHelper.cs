using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    ///SharedDataHelper 的摘要说明
    /// </summary>
    public class SharedDataHelper
    {
        public SharedDataHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
    }

    public class V_GetProjectList
    {
        #region Model
        private int _projectId;
        private string _serialCode;
        private string _projectCode;
        private string _businessDescription;
        private int _groupID;
        private string _groupName;
        private decimal _totalAmount;
        private DateTime _createDate;
        private DateTime _submitDate;
        private int _status;
        private int _memberUserID;
        private string _memberUserName;

        /// <summary>
        /// 流水号
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId
        {
            set { _projectId = value; }
            get { return _projectId; }
        }

        /// <summary>
        /// PA号
        /// </summary>
        /// <value>The serial code.</value>
        public string SerialCode
        {
            set { _serialCode = value; }
            get { return _serialCode; }
        }

        /// <summary>
        /// 项目号
        /// </summary>
        /// <value>The project code.</value>
        public string ProjectCode
        {
            set { _projectCode = value; }
            get { return _projectCode; }
        }

        /// <summary>
        /// 项目描述
        /// </summary>
        /// <value>The business description.</value>
        public string BusinessDescription
        {
            set { _businessDescription = value; }
            get { return _businessDescription; }
        }

        /// <summary>
        /// 申请方组别ID
        /// </summary>
        /// <value>The group ID.</value>
        public int GroupID
        {
            set { _groupID = value; }
            get { return _groupID; }
        }

        /// <summary>
        /// 申请方组别
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            set { _groupName = value; }
            get { return _groupName; }
        }

        /// <summary>
        /// 总成本
        /// </summary>
        /// <value>The total amount.</value>
        public decimal TotalAmount
        {
            set { _totalAmount = value; }
            get { return _totalAmount; }
        }

        /// <summary>
        /// 项目创建时间
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        }

        /// <summary>
        /// 项目提交时间
        /// </summary>
        /// <value>The submit date.</value>
        public DateTime SubmitDate
        {
            set { _submitDate = value; }
            get { return _submitDate; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <value>The status.</value>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 项目成员id
        /// </summary>
        /// <value>The member user ID.</value>
        public int MemberUserID
        {
            set { _memberUserID = value; }
            get { return _memberUserID; }
        }

        /// <summary>
        /// 项目成员
        /// </summary>
        /// <value>The name of the member user.</value>
        public string MemberUserName
        {
            set { _memberUserName = value; }
            get { return _memberUserName; }
        }
        #endregion Model

        #region method
        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["ProjectId"] && r["ProjectId"].ToString() != "")
            {
                _projectId = int.Parse(r["ProjectId"].ToString());
            }
            _serialCode = r["SerialCode"].ToString();
            _projectCode = r["ProjectCode"].ToString();
            _businessDescription = r["BusinessDescription"].ToString();
            if (null != r["GroupID"] && r["GroupID"].ToString() != "")
            {
                _groupID = int.Parse(r["GroupID"].ToString());
            }
            _groupName = r["GroupName"].ToString();
            if (null != r["TotalAmount"] && r["TotalAmount"].ToString() != "")
            {
                _totalAmount = decimal.Parse(r["TotalAmount"].ToString());
            }
            _createDate = r["CreateDate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(r["CreateDate"].ToString());
            _submitDate = r["SubmitDate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(r["SubmitDate"].ToString());
            if (null != r["Status"] && r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            //if (null != r["MemberUserID"] && r["MemberUserID"].ToString() != "")
            //{
            //    _memberUserID = int.Parse(r["MemberUserID"].ToString());
            //}
            //_memberUserName = r["MemberUserName"].ToString();
        }
        #endregion
    }

    public class V_GetProjectGroupList
    {
        #region Model
        private int _projectId;
        private int _groupID;
        private string _groupName;
        private decimal _cost;

        /// <summary>
        /// 流水号
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId
        {
            set { _projectId = value; }
            get { return _projectId; }
        }

        /// <summary>
        /// 组别ID
        /// </summary>
        /// <value>The group ID.</value>
        public int GroupID
        {
            set { _groupID = value; }
            get { return _groupID; }
        }

        /// <summary>
        /// 组别
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            set { _groupName = value; }
            get { return _groupName; }
        }

        /// <summary>
        /// 每个组别可以使用的费用
        /// </summary>
        /// <value>The total amount.</value>
        public decimal COST
        {
            set { _cost = value; }
            get { return _cost; }
        }

        #endregion Model

        #region method
        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["ProjectId"] && r["ProjectId"].ToString() != "")
            {
                _projectId = int.Parse(r["ProjectId"].ToString());
            }
            if (null != r["GroupID"] && r["GroupID"].ToString() != "")
            {
                _groupID = int.Parse(r["GroupID"].ToString());
            }
            _groupName = r["GroupName"].ToString();
            if (null != r["COST"] && r["COST"].ToString() != "")
            {
                _cost = decimal.Parse(r["COST"].ToString());
            }
        }
        #endregion
    }

    public class V_GetProjectTypeList
    {
        #region Model
        private int _projectId;
        private int _groupID;
        private int _typeID;
        private decimal _amounts;
        private int _parentId;
        private string _typeName;

        /// <summary>
        /// 流水号
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId
        {
            set { _projectId = value; }
            get { return _projectId; }
        }

        /// <summary>
        /// 组别ID
        /// </summary>
        /// <value>The group ID.</value>
        public int GroupID
        {
            set { _groupID = value; }
            get { return _groupID; }
        }

        /// <summary>
        /// 物料类别ID
        /// </summary>
        /// <value>The name of the group.</value>
        public int TypeID
        {
            set { _typeID = value; }
            get { return _typeID; }
        }

        /// <summary>
        /// 每个物料类别可以使用的费用
        /// </summary>
        /// <value>The total amount.</value>
        public decimal Amounts
        {
            set { _amounts = value; }
            get { return _amounts; }
        }

        public int parentId
        {
            set { _parentId = value; }
            get { return _parentId; }
        }

        public string typeName
        {
            set { _typeName = value; }
            get { return _typeName; }
        }

        #endregion Model

        #region method
        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["ProjectId"] && r["ProjectId"].ToString() != "")
            {
                _projectId = int.Parse(r["ProjectId"].ToString());
            }
            if (null != r["GroupID"] && r["GroupID"].ToString() != "")
            {
                _groupID = int.Parse(r["GroupID"].ToString());
            }
            if (null != r["TypeID"] && r["TypeID"].ToString() != "")
            {
                _typeID = int.Parse(r["TypeID"].ToString());
            }
            if (null != r["Amounts"] && r["Amounts"].ToString() != "")
            {
                _amounts = decimal.Parse(r["Amounts"].ToString());
            }
            if (null != r["parentId"] && r["parentId"].ToString() != "")
            {
                _parentId = int.Parse(r["parentId"].ToString());
            }
            if (null != r["typeName"] && r["typeName"].ToString() != "")
            {
                _typeName = r["typeName"].ToString();
            }
        }
        #endregion
    }

    public class V_GetCostType
    {
        #region Model
        private int _typeid;
        private string _typename;
        private int _parentId;
        private int _typelevel;

        /// <summary>
        /// 物料类别ID
        /// </summary>
        /// <value>The project id.</value>
        public int typeid
        {
            set { _typeid = value; }
            get { return _typeid; }
        }

        /// <summary>
        /// 物料类别名称
        /// </summary>
        /// <value>The group ID.</value>
        public string typename
        {
            set { _typename = value; }
            get { return _typename; }
        }

        /// <summary>
        /// 物料类别父ID
        /// </summary>
        /// <value>The name of the group.</value>
        public int parentId
        {
            set { _parentId = value; }
            get { return _parentId; }
        }

        /// <summary>
        /// 物料级别
        /// </summary>
        /// <value>The typelevel.</value>
        public int typelevel
        {
            set { _typelevel = value; }
            get { return _typelevel; }
        }

        #endregion Model

        #region method
        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["typeid"] && r["typeid"].ToString() != "")
            {
                _typeid = int.Parse(r["typeid"].ToString());
            }
            _typename = r["typename"].ToString();
            if (null != r["parentId"] && r["parentId"].ToString() != "")
            {
                _parentId = int.Parse(r["parentId"].ToString());
            }
            if (null != r["typelevel"] && r["typelevel"].ToString() != "")
            {
                _typelevel = int.Parse(r["typelevel"].ToString());
            }
        }
        #endregion
    }
}