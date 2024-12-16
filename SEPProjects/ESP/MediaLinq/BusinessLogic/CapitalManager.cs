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
    public class CapitalManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_CapitalInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_CapitalInfo(info);
                mdc.SaveChanges();
                return info.cityID;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_CapitalInfo info)
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
                
                    ESP.MediaLinq.Entity.media_CapitalInfo model = mdc.media_CapitalInfo.FirstOrDefault(c => c.cityID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
               
            }
        }


        public static IList<ESP.MediaLinq.Entity.media_CapitalInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_CapitalInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_CapitalInfo> list = q.ToList();
                return list;
            }
        }

        public static DataTable GetDataTable()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_CapitalInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_CapitalInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_CapitalInfo>(list).Tables[0];
            }
        }

        public static DataTable GetListByID(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_CapitalInfo
                        where info.cityID == id
                        select info;

                List<ESP.MediaLinq.Entity.media_CapitalInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_CapitalInfo>(list).Tables[0];
            }
        }

        public static ESP.MediaLinq.Entity.media_CapitalInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_CapitalInfo where t.cityID == id select t;
                return mdc.media_CapitalInfo.FirstOrDefault(c => c.cityID == id);
            }
        }
    }
}
