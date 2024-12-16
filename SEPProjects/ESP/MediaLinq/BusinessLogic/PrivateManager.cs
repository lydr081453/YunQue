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
    public class PrivateManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_PrivateInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_PrivateInfo(info);
                mdc.SaveChanges();
                return info.id;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_PrivateInfo info)
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
                
                    ESP.MediaLinq.Entity.media_PrivateInfo model = mdc.media_PrivateInfo.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
               
            }
        }


        public static IList<ESP.MediaLinq.Entity.media_PrivateInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_PrivateInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_PrivateInfo> list = q.ToList();
                return list;
            }
        }

        public static DataTable GetListByID(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_PrivateInfo
                        where info.id == id
                        select info;

                List<ESP.MediaLinq.Entity.media_PrivateInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_PrivateInfo>(list).Tables[0];
            }
        }

        public static ESP.MediaLinq.Entity.media_PrivateInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_PrivateInfo where t.id == id select t;
                return mdc.media_PrivateInfo.FirstOrDefault(c => c.id == id);
            }
        }


        /// <summary>
        /// 获得记者的私密信息列表
        /// </summary>
        /// <param name="reportId">记者ID</param>
        /// <returns></returns>
        public static DataTable getListByReportId(int reportId)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_PrivateInfo where t.ReporterID == reportId select t;
                List<ESP.MediaLinq.Entity.media_PrivateInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_PrivateInfo>(list).Tables[0];
            }
        }
    }
}
