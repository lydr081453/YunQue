using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using ESP.MediaLinq.Entity;
using ESP.Data;
namespace ESP.MediaLinq.BusinessLogic
{
    public class MediaReporterRelationManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_MediaReporterRelationInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_MediaReporterRelationInfo(info);
                mdc.SaveChanges();
                return info.RelationID;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_MediaReporterRelationInfo info)
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
                
                    ESP.MediaLinq.Entity.media_MediaReporterRelationInfo model = mdc.media_MediaReporterRelationInfo.FirstOrDefault(c => c.RelationID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
                
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_MediaReporterRelationInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaReporterRelationInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_MediaReporterRelationInfo> list = q.ToList();
                return list;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_MediaReporterRelationInfo> GetListByMediaID(int mediaID)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaReporterRelationInfo
                        where info.MediaItemID == mediaID
                        select info;

                List<ESP.MediaLinq.Entity.media_MediaReporterRelationInfo> list = q.ToList();
                return list;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_MediaReporterRelationInfo> GetListByReporterID(int reporterID)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaReporterRelationInfo
                        where info.ReporterID == reporterID
                        select info;

                List<ESP.MediaLinq.Entity.media_MediaReporterRelationInfo> list = q.ToList();
                return list;
            }
        }

        public static ESP.MediaLinq.Entity.media_MediaReporterRelationInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_MediaReporterRelationInfo where t.RelationID == id select t;
                return mdc.media_MediaReporterRelationInfo.FirstOrDefault(c => c.RelationID == id);
            }
        }
    }
}