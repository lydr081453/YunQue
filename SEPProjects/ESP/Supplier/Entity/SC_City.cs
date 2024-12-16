using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_City
    {
        public SC_City()
        { }
        #region Model
        private int _cityid;
        private string _cityname;
        private string _citylevel;
        private int _provinceid;
        private int _citystatus;
        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId
        {
            set { _cityid = value; }
            get { return _cityid; }
        }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            set { _cityname = value; }
            get { return _cityname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CityLevel
        {
            set { _citylevel = value; }
            get { return _citylevel; }
        }
        /// <summary>
        /// 省id

        /// </summary>
        public int ProvinceId
        {
            set { _provinceid = value; }
            get { return _provinceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CityStatus
        {
            set { _citystatus = value; }
            get { return _citystatus; }
        }
        #endregion Model
    }
}
