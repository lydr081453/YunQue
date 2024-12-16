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
    public class MediaAttachmentsManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_MediaAttachmentsInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_MediaAttachmentsInfo(info);
                mdc.SaveChanges();
                return info.id;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_MediaAttachmentsInfo info)
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
               
                    ESP.MediaLinq.Entity.media_MediaAttachmentsInfo model = mdc.media_MediaAttachmentsInfo.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;                  
            }
        }


        public static IList<ESP.MediaLinq.Entity.media_MediaAttachmentsInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaAttachmentsInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_MediaAttachmentsInfo> list = q.ToList();
                return list;
            }
        }

        public static DataTable GetListByID(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaAttachmentsInfo
                        where info.id == id
                        select info;

                List<ESP.MediaLinq.Entity.media_MediaAttachmentsInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_MediaAttachmentsInfo>(list).Tables[0];
            }
        }

        public static ESP.MediaLinq.Entity.media_MediaAttachmentsInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_MediaAttachmentsInfo where t.id == id select t;
                return mdc.media_MediaAttachmentsInfo.FirstOrDefault(c => c.id == id);
            }
        }
    }
}