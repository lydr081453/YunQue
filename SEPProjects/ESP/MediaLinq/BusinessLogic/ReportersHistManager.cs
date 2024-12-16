using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using System.Data;
using ESP.MediaLinq.Utilities;
using ESP.Data;
using ESP.MediaLinq.Entity;
using System.Reflection;

namespace ESP.MediaLinq.BusinessLogic
{
    public class ReportersHistManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_reportersHistInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_reportersHistInfo(info);
                mdc.SaveChanges();
                return info.id;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_reportersHistInfo info)
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
                
                    ESP.MediaLinq.Entity.media_reportersHistInfo model = mdc.media_reportersHistInfo.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
               
            }
        }

        public static DataTable GetListByReporterID(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_reportersHistInfo where info.ReporterID == id
                        select info;

                List<ESP.MediaLinq.Entity.media_reportersHistInfo> list = q.ToList();

                MethodInfo method = typeof(ListToDataSet).GetMethod("ToDataSets");
                MethodInfo method2 = method.MakeGenericMethod(q.ElementType);
                DataSet ds = (DataSet)method2.Invoke(null, new object[] { list });

                return ds.Tables[0];
                //return list;
            }
        }

        public static ESP.MediaLinq.Entity.media_reportersHistInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_reportersHistInfo where t.id == id select t;
                return mdc.media_reportersHistInfo.FirstOrDefault(c => c.id == id);
            }
        }

    }
}
