using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    [Serializable]
    public partial class ExpenseAuditerListInfo
    {
        public ExpenseAuditerListInfo()
        { }
        #region Model
        private int _id;
        private int? _returnid;
        private int? _auditer;
        private string _auditername;
        private int? _audittype;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReturnID
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Auditer
        {
            set { _auditer = value; }
            get { return _auditer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditerName
        {
            set { _auditername = value; }
            get { return _auditername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditType
        {
            set { _audittype = value; }
            get { return _audittype; }
        }
        #endregion Model
    }
}
