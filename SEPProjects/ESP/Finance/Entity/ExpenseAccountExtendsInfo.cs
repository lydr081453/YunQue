using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ExpenseAccountInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ExpenseAccountExtendsInfo : ReturnInfo
    {
        public ExpenseAccountExtendsInfo()
        { }
        #region Model

        //private int? _workItemId;
        //private string _workItemName;
        private string _participantName;
        private string _webPage;
        

        /// <summary>
        /// 工作项ID
        /// </summary>
        //public int? WorkItemId
        //{
        //    set { _workItemId = value; }
        //    get { return _workItemId; }
        //}
        /// <summary>
        /// 工作项名称
        /// </summary>
        //public string WorkItemName
        //{
        //    set { _workItemName = value; }
        //    get { return _workItemName; }
        //}
        /// <summary>
        /// 角色名称
        /// </summary>
        public string ParticipantName
        {
            set { _participantName = value; }
            get { return _participantName; }
        }
        /// <summary>
        /// 转向页面路径
        /// </summary>
        public string WebPage
        {
            set { _webPage = value; }
            get { return _webPage; }
        }
        
        #endregion Model

    }
}
