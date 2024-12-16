using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Area
    {
        public SC_Area()
        { }
        #region Model
        private int _areaid;
        private string _areades;
        private int _areastatus;
        /// <summary>
        /// 
        /// </summary>
        public int AreaId
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaDes
        {
            set { _areades = value; }
            get { return _areades; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AreaStatus
        {
            set { _areastatus = value; }
            get { return _areastatus; }
        }
        #endregion Model

    }
}
