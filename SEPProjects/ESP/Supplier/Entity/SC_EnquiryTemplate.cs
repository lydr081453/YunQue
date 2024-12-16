using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_EnquiryTemplate
    {
        public SC_EnquiryTemplate()
        { }
        #region Model
        private int _ID;
        private string _Name;
        private string _Xml;
        private int _TypeID;
        private int _UserID;
        private DateTime _CreateTime;
        private DateTime _UpdateTime;
        private int _IsDelete;
        private int _MessageId;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Xml
        {
            set { _Xml = value; }
            get { return _Xml; }
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
        public DateTime CreateTime
        {
            set { _CreateTime = value; }
            get { return _CreateTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _UpdateTime = value; }
            get { return _UpdateTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsDelete
        {
            set { _IsDelete = value; }
            get { return _IsDelete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MessageId
        {
            set { _MessageId = value; }
            get { return _MessageId; }
        }
        #endregion Model
    }
}
