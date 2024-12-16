using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Country
    {
        public SC_Country()
        { }
        #region Model
        private int _countryid;
        private string _countrynum;
        private string _countryname;
        private int _countrystatus;
        /// <summary>
        /// 
        /// </summary>
        public int CountryID
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CountryNum
        {
            set { _countrynum = value; }
            get { return _countrynum; }
        }
        /// <summary>
        /// 国家名字
        /// </summary>
        public string CountryName
        {
            set { _countryname = value; }
            get { return _countryname; }
        }
        /// <summary>
        /// 地域属性ID
        /// </summary>
        public int CountryStatus
        {
            set { _countrystatus = value; }
            get { return _countrystatus; }
        }
        #endregion Model
    }
}
