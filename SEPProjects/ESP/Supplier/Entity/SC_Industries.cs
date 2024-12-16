using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Industries
    {
        public SC_Industries()
        { }
        #region Model
        private int _industryid;
        private string _industryname;
        private int _industrystatus;
        /// <summary>
        /// 
        /// </summary>
        public int IndustryID
        {
            set { _industryid = value; }
            get { return _industryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IndustryName
        {
            set { _industryname = value; }
            get { return _industryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IndustryStatus
        {
            set { _industrystatus = value; }
            get { return _industrystatus; }
        }
        #endregion Model
    }
}
