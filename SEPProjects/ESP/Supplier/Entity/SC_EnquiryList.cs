using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_EnquiryList
    {
        public SC_EnquiryList()
        { }
        #region Model
        private int _Id;
        private int _TemplateID;
        private int _TypeID;
        private int _UserID;
        private int _MessageId;
        private string _Note;
        private DateTime _CreateTime;
        private int _PEID;
        
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TemplateID
        {
            set { _TemplateID = value; }
            get { return _TemplateID; }
        }
                /// <summary>
        /// 
        /// </summary>
        public int TypeID
        {
            set { _TypeID = value; }
            get { return _TypeID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _UserID = value; }
            get { return _UserID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MessageId
        {
            set { _MessageId = value; }
            get { return _MessageId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Note
        {
            set { _Note = value; }
            get { return _Note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _CreateTime = value; }
            get { return _CreateTime; }
        }
         /// <summary>
        /// 
        /// </summary>
        public int PEID
        {
            set { _PEID = value; }
            get { return _PEID; }
        }
        
        #endregion Model
    }
}
