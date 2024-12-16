using System;

namespace ESP.Purchase.Entity
{

    /// <summary>
    ///WorkItemsExtend 的摘要说明
    /// </summary>
    public class WorkItemsExtend : WorkFlow.Model.WORKITEMS
    {
        public WorkItemsExtend()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        private int _userIdExt;
        public int UserIdExt
        {
            get { return _userIdExt; }
            set { _userIdExt = value; }
        }

        private string _userNameExt;
        public string UserNameExt
        {
            get { return _userNameExt; }
            set { _userNameExt = value; }
        }

        private DateTime _auditTimeExt;
        public DateTime AuditTimeExt
        {
            get { return _auditTimeExt; }
            set { _auditTimeExt = value; }
        }

        private string _auditNoteExt;
        public string AuditNoteExt
        {
            get { return _auditNoteExt; }
            set { _auditNoteExt = value; }
        }

        private string _desExt;
        public string DesExt
        {
            get { return _desExt; }
            set { _desExt = value; }
        }

    }
}
