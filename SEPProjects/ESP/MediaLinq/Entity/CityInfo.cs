using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.MediaLinq.Entity
{
    public class CityInfo : SqlDataAccess.CityInfo
    {
        //private int _city_ID;
        //private string _city_Name;
        //private int _city_Level;
        //private int _province_ID;
        //private SqlDataAccess.CityInfo _cityInfo;
        private string _province_Name;
        private string _country_Name;

        //public int City_ID
        //{
        //    get { return _city_ID; }
        //    set { _city_ID = value; }
        //}

        //public string City_Name
        //{
        //    get { return _city_Name; }
        //    set { _city_Name = value; }
        //}

        //public int City_Level
        //{
        //    get { return _city_Level; }
        //    set { _city_Level = value; }
        //}

        //public SqlDataAccess.CityInfo CityInfo
        //{
        //    get { return _cityInfo; }
        //    set { _cityInfo = value; }
        //}

        public string Province_Name
        {
            get { return _province_Name; }
            set { _province_Name = value; }
        }

        public string Country_Name
        {
            get { return _country_Name; }
            set { _country_Name = value; }
        }
    }
}
