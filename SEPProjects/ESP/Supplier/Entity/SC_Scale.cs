using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Scale
    {
        public SC_Scale()
        { }
        #region Model
        private int _scaleid;
        private string _scaledes;
        private int _scalestatus;
        /// <summary>
        /// 
        /// </summary>
        public int ScaleId
        {
            set { _scaleid = value; }
            get { return _scaleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ScaleDes
        {
            set { _scaledes = value; }
            get { return _scaledes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ScaleStatus
        {
            set { _scalestatus = value; }
            get { return _scalestatus; }
        }
        #endregion Model
    }
}
