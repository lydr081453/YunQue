using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using System.Data;
using ESP.MediaLinq.Utilities;
using ESP.MediaLinq.Entity;
using ESP.Data;
namespace ESP.MediaLinq.BusinessLogic
{
    public class ProvinceManager:BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_ProvinceInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_ProvinceInfo(info);
                mdc.SaveChanges();
                return info.Province_ID;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_ProvinceInfo info)
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
                
                    ESP.MediaLinq.Entity.media_ProvinceInfo model = mdc.media_ProvinceInfo.FirstOrDefault(c => c.Province_ID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
                
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_ProvinceInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_ProvinceInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_ProvinceInfo> list = q.ToList();
                return list;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_ProvinceInfo> GetListByCountryID(int countryID)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_ProvinceInfo
                        where info.Country_ID == countryID
                        select info;

                List<ESP.MediaLinq.Entity.media_ProvinceInfo> list = q.ToList();
                return list;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_ProvinceInfo> GetList(int countryId)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_ProvinceInfo
                        where info.Country_ID.Value == countryId
                        select info;

                List<ESP.MediaLinq.Entity.media_ProvinceInfo> list = q.ToList();
                return list;
            }
        }

        public static ESP.MediaLinq.Entity.media_ProvinceInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_ProvinceInfo where t.Province_ID == id select t;
                return mdc.media_ProvinceInfo.FirstOrDefault(c => c.Province_ID == id);
            }
        }

        /// <summary>
        /// 根据国家得到省份信息
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        public static DataTable getAllListByCountry(int countryid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from p in mdc.media_ProvinceInfo where p.Country_ID == countryid select p;
                List<ESP.MediaLinq.Entity.media_ProvinceInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_ProvinceInfo>(list).Tables[0];
            }
        }

        /// <summary>
        /// Gets all list by country A.
        /// </summary>
        /// <param name="countryid">The countryid.</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<List<string>> getAllListByCountryA()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                int countryid = Global.DefaultCountry;

                List<List<string>> list = new List<List<string>>();

                List<string> c = new List<string>();
                c.Add("0");
                c.Add("请选择");
                list.Add(c);

                var q = from p in mdc.media_ProvinceInfo where p.Country_ID == countryid select p;
                List<ESP.MediaLinq.Entity.media_ProvinceInfo> listinfo = q.ToList();

                if (listinfo.Count > 0)
                {
                    for (int i = 0; i < listinfo.Count; i++)
                    {
                        List<string> s = new List<string>();
                        s.Add(listinfo[i].Province_ID.ToString());
                        s.Add(listinfo[i].Province_Name.ToString());
                        list.Add(s);
                    }
                }
                return list;
            }
        }
    }
}
