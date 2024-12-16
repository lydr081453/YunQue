using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.BusinessLogic
{
    public class CapitalManager
    {

        /// <summary>
        /// Gets all list.
        /// </summary>
        /// <returns></returns>
        public static DataTable getAllList()
        {
            return ESP.Media.DataAccess.CapitalDataProvider.QueryInfo(" and 1=1 order by cityid ", new Hashtable());
        }

        public static CapitalInfo GetModel(int cityid)
        {
            if (cityid <= 0)
            {
                CapitalInfo city = new ESP.Media.Entity.CapitalInfo();
                city.Cityname = string.Empty;
                city.Cityid = 0;
                city.Province = string.Empty;
                return city;
            }
            return ESP.Media.DataAccess.CapitalDataProvider.Load(cityid);
        }
    }
}
