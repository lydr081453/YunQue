using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using System.Data;
using ESP.MediaLinq.Utilities;
using ESP.Data;
using ESP.MediaLinq.Entity;
namespace ESP.MediaLinq.BusinessLogic
{
    public class CityManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_CityInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_CityInfo(info);
                mdc.SaveChanges();
                return info.City_ID;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_CityInfo info)
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
                
                    ESP.MediaLinq.Entity.media_CityInfo model = mdc.media_CityInfo.FirstOrDefault(c => c.City_ID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
                
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_CityInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_CityInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_CityInfo> list = q.ToList();
                return list;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_CityInfo> GetListByProvinceID(int provinceId)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_CityInfo
                        where info.Province_ID.Value == provinceId
                        select info;

                List<ESP.MediaLinq.Entity.media_CityInfo> list = q.ToList();
                return list;
            }
        }

        public static IQueryable GetLinkList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from city in mdc.media_CityInfo
                        join province in mdc.media_ProvinceInfo on city.Province_ID equals province.Province_ID
                        join country in mdc.media_CountryInfo on province.Country_ID equals country.CountryID
                        select new
                        {
                            city.City_ID,
                            city.City_Name,
                            city.City_Level,
                            city.Province_ID,
                            province.Province_Name,
                            country.CountryName
                        };
                return q;
            }
        }

        public static ESP.MediaLinq.Entity.media_CityInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in dc.CityInfo where t.City_ID == id select t;
                return mdc.media_CityInfo.FirstOrDefault(c => c.City_ID == id);
            }
        }

        /// <summary>
        /// 根据国家得到省份信息
        /// </summary>
        /// <param name="provinceid">The provinceid.</param>
        /// <returns></returns>
        public static DataTable getAllListByProvince(int provinceid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from c in mdc.media_CityInfo where c.Province_ID == provinceid select c;
                List<ESP.MediaLinq.Entity.media_CityInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_CityInfo>(list).Tables[0];
            }
        }

        /// <summary>
        /// Gets all list by province A.
        /// </summary>
        /// <param name="provinceid">The provinceid.</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<List<string>> getAllListByProvinceA(int provinceid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                List<List<string>> list = new List<List<string>>();

                List<string> cs = new List<string>();
                cs.Add("0");
                cs.Add("请选择");
                list.Add(cs);

                var q = from c in mdc.media_CityInfo where c.Province_ID == provinceid select c;
                List<ESP.MediaLinq.Entity.media_CityInfo> listinfo = q.ToList();

                if (listinfo.Count > 0)
                {
                    for (int i = 0; i < listinfo.Count; i++)
                    {
                        List<string> s = new List<string>();
                        s.Add(listinfo[i].City_ID.ToString());
                        s.Add(listinfo[i].City_Name.ToString());
                        list.Add(s);
                    }
                }
                return list;
            }
        }
    }
}
