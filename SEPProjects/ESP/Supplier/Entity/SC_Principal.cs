using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Principal
    {
        #region Model
        private int _principalid;
        private string _principaldes;
        private int _principalstatus;
        /// <summary>
        /// 
        /// </summary>
        public int PrincipalId
        {
            set { _principalid = value; }
            get { return _principalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrincipalDes
        {
            set { _principaldes = value; }
            get { return _principaldes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PrincipalStatus
        {
            set { _principalstatus = value; }
            get { return _principalstatus; }
        }
        #endregion Model
    }
}
