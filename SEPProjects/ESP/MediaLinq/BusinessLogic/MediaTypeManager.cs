using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using ESP.Data;
using ESP.MediaLinq.Entity;
using System.Data;
using ESP.MediaLinq.Utilities;
namespace ESP.MediaLinq.BusinessLogic
{
    public class MediaTypeManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_mediaTypeInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_mediaTypeInfo(info);
                mdc.SaveChanges();
                return info.id;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_mediaTypeInfo info)
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
               
                    ESP.MediaLinq.Entity.media_mediaTypeInfo model = mdc.media_mediaTypeInfo.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;  
                
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_mediaTypeInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_mediaTypeInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_mediaTypeInfo> list = q.ToList();
                return list;
            }
        }

        public static ESP.MediaLinq.Entity.media_mediaTypeInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_mediaTypeInfo where t.id == id select t;
                return mdc.media_mediaTypeInfo.FirstOrDefault(c => c.id == id);
            }
        }

        public static DataTable GetDataTable()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_mediaTypeInfo
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_mediaTypeInfo>(q.ToList<ESP.MediaLinq.Entity.media_mediaTypeInfo>());
                return list.Tables[0];
            }
        }
    }
}
