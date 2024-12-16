using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using System.Data;
using ESP.MediaLinq.Utilities;
using System.Transactions;
using ESP.Data;
using ESP.MediaLinq.Entity;
namespace ESP.MediaLinq.BusinessLogic
{
    public class MediaItemsHistManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_MediaItemsHistInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_MediaItemsHistInfo(info);
                mdc.SaveChanges();
                return info.id;
            }

        }

        public static bool Update(ESP.MediaLinq.Entity.media_MediaItemsHistInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                try
                {
                    mdc.SaveChanges();
                    return true;
                }
                catch (Exception ex)
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
                
                    ESP.MediaLinq.Entity.media_MediaItemsHistInfo model = mdc.media_MediaItemsHistInfo.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
               
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_MediaItemsHistInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaItemsHistInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_MediaItemsHistInfo> list = q.ToList();
                return list;
            }

        }

        public static DataSet GetDataSet()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaItemsHistInfo
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_MediaItemsHistInfo>(q.ToList<ESP.MediaLinq.Entity.media_MediaItemsHistInfo>());
                return list;
            }
        }



        public static ESP.MediaLinq.Entity.media_MediaItemsHistInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_MediaItemsHistInfo where t.id == id select t;
                return mdc.media_MediaItemsHistInfo.FirstOrDefault(c => c.id == id);
            }
        }
    }
}
