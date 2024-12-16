using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using ESP.MediaLinq.Entity;
using System.Data;
using ESP.MediaLinq.Utilities;
using ESP.Data;
namespace ESP.MediaLinq.BusinessLogic
{
    public class CountryManager : BaseEntityInfo
    {
        /// <summary>
        /// 添加Country
        /// </summary>
        /// <param name="info">ESP.MediaLinq.Entity.media_CountryInfo info</param>
        /// <returns>Media_CountryInfo.CountryID</returns>
        public static int Add(ESP.MediaLinq.Entity.media_CountryInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_CountryInfo(info);
                mdc.SaveChanges();
                return info.CountryID;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_CountryInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                if(info != null)
                {                   
                    mdc.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                
                    ESP.MediaLinq.Entity.media_CountryInfo model = mdc.media_CountryInfo.FirstOrDefault(c => c.CountryID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
                
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_CountryInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_CountryInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_CountryInfo> list = q.ToList();
                return list;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_CountryInfo> GetListByCountryID(int countryID)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_CountryInfo
                        where info.CountryID == countryID
                        select info;

                List<ESP.MediaLinq.Entity.media_CountryInfo> list = q.ToList();
                return list;
            }
        }

        public static ESP.MediaLinq.Entity.media_CountryInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in dc.CountryInfo where t.CountryID == id select t;
                return mdc.media_CountryInfo.FirstOrDefault(c => c.CountryID == id);
            }
        }

        /// <summary>
        /// Gets the list by region attribute ID.
        /// </summary>
        /// <param name="regionattributeid">The regionattributeid.</param>
        /// <returns></returns>
        public static DataTable getListByRegionAttributeID(int regionattributeid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from c in mdc.media_CountryInfo where c.RegionAttributeID == regionattributeid select c;
                List<ESP.MediaLinq.Entity.media_CountryInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_CountryInfo>(list).Tables[0];
            }
        }

        /// <summary>
        /// Gets the list by region attribute IDA.
        /// </summary>
        /// <param name="regionattributeid">The regionattributeid.</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<List<string>> getListByRegionAttributeIDA(int regionattributeid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                List<List<string>> list = new List<List<string>>();
                List<string> cs = new List<string>();
                cs.Add("0");
                cs.Add("请选择");
                list.Add(cs);

                var q = from c in mdc.media_CountryInfo where c.RegionAttributeID == regionattributeid select c;
                List<ESP.MediaLinq.Entity.media_CountryInfo> listinfo = q.ToList();

                if (listinfo.Count > 0)
                {
                    for (int i = 0; i < listinfo.Count; i++)
                    {
                        List<string> s = new List<string>();
                        s.Add(listinfo[i].CountryID.ToString());
                        s.Add(listinfo[i].CountryName.ToString());
                        list.Add(s);
                    }
                }
                return list;
            }
        }
    }
}
