using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public partial class TsLastUpdateInfo
    {
        public TsLastUpdateInfo()
        { }
        #region Model
        private int _id;
        private int _userId;
        private DateTime _lastUpdateDate;

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserId
        {
            set { _userId = value; }
            get { return _userId; }
        }
        /// <summary>
        ///  
        /// </summary>
        public DateTime LastUpdateDate
        {
            set { _lastUpdateDate = value; }
            get { return _lastUpdateDate; }
        }
        #endregion Model

    }
}
