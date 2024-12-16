using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Province
    {
        public SC_Province()
        { }
        #region Model
        private int _provinceid;
        private string _provincename;
        private int _countryid;
        private int _provincestatus;
        /// <summary>
        /// id
        /// </summary>
        public int ProvinceId
        {
            set { _provinceid = value; }
            get { return _provinceid; }
        }
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName
        {
            set { _provincename = value; }
            get { return _provincename; }
        }
        /// <summary>
        /// 国家id

        /// </summary>
        public int CountryId
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProvinceStatus
        {
            set { _provincestatus = value; }
            get { return _provincestatus; }
        }
        #endregion Model
    }
}
