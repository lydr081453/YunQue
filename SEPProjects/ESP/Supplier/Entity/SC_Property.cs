using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Property
    {
        public SC_Property()
        { }
        #region Model
        private int _propertyid;
        private string _propertydes;
        private int _propertystatus;
        /// <summary>
        /// 
        /// </summary>
        public int PropertyId
        {
            set { _propertyid = value; }
            get { return _propertyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PropertyDes
        {
            set { _propertydes = value; }
            get { return _propertydes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PropertyStatus
        {
            set { _propertystatus = value; }
            get { return _propertystatus; }
        }
        #endregion Model
    }
}
