using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.DataAccessAuthorization.Entity
{
    /// <summary>
    /// 操作权限描述
    /// </summary>
    public class DataAccessAction
    {
        #region Model
        private int _dataaccessactionid;
        private string _actionname;
        private string _action;
        private Guid _actionidentity;
        private int _actiontype;
        private bool _customaction;
        private string _description;
        private string _accessservice;
        private DateTime _createtime;
        private int _creator;
        private string _creatorname;
        private IList<DataAccessMember> _DataAccessMemberList;
        /// <summary>
        /// 许可操作序号
        /// </summary>
        public int DataAccessActionID
        {
            set { _dataaccessactionid = value; }
            get { return _dataaccessactionid; }
        }
        /// <summary>
        /// 操作名称，用于显示
        /// </summary>
        public string ActionName
        {
            set { _actionname = value; }
            get { return _actionname; }
        }
        /// <summary>
        /// 动作代号
        /// </summary>
        public string Action
        {
            set { _action = value; }
            get { return _action; }
        }
        /// <summary>
        /// 操作唯一编码
        /// </summary>
        public Guid ActionIdentity
        {
            set { _actionidentity = value; }
            get { return _actionidentity; }
        }
        /// <summary>
        /// 操作类型：当非自定义操作类型时，（0）查看；（1）更新；（2）删除；（3）新建。自定义操作类型时根据定义来确定。所有>（0）的操作类型均附带（0）的权限。特殊的权限（Int32的MaxValue）代表完全控制。
        /// </summary>
        public int ActionType
        {
            set { _actiontype = value; }
            get { return _actiontype; }
        }
        /// <summary>
        /// 自定义操作类型
        /// </summary>
        public bool CustomAction
        {
            set { _customaction = value; }
            get { return _customaction; }
        }
        /// <summary>
        /// 操作权限说明，将业务逻辑详细描述与此。例如：PR任何人可以创建；自己创建的PR单自己可以查看；等等。“自己”从具体的单据数据中来，不存储在这几张表内，由业务逻辑控制。
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 权限判定服务，提供服务的类名，通过反射可以实例化；需要实现IAccessService接口
        /// </summary>
        public string AccessService
        {
            set { _accessservice = value; }
            get { return _accessservice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorName
        {
            set { _creatorname = value; }
            get { return _creatorname; }
        }

        /// <summary>
        /// 当前Action的Member列表
        /// </summary>
        public IList<DataAccessMember> DataAccessMemberList
        {
            get {
                if (_DataAccessMemberList == null)
                {
                    _DataAccessMemberList = ESP.Configuration.ProviderHelper<DataAccess.IDataAccessAction>.Instance.GetDataAccessMemberList(_dataaccessactionid);
                }
                return _DataAccessMemberList;
            }
        }
        #endregion Model
    }
}
