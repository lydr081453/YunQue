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
    public class IndustriesManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_IndustriesInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_IndustriesInfo(info);
                mdc.SaveChanges();
                return info.IndustryID;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_IndustriesInfo info)
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
                
                    ESP.MediaLinq.Entity.media_IndustriesInfo model = mdc.media_IndustriesInfo.FirstOrDefault(c => c.IndustryID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
                
            }
        }


        public static IList<ESP.MediaLinq.Entity.media_IndustriesInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_IndustriesInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_IndustriesInfo> list = q.ToList();
                return list;
            }
        }

        public static DataTable GetDataTable()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_IndustriesInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_IndustriesInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_IndustriesInfo>(list).Tables[0];
            }
        }

        public static DataTable GetDataTable(string industryname)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_IndustriesInfo
                        select info;

                if (!string.IsNullOrEmpty(industryname))
                {
                    q = q.Where(x => x.IndustryName.Contains(industryname));
                }
                List<ESP.MediaLinq.Entity.media_IndustriesInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_IndustriesInfo>(list).Tables[0];
            }
        }

        public static DataTable GetListByID(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_IndustriesInfo
                        where info.IndustryID == id
                        select info;

                List<ESP.MediaLinq.Entity.media_IndustriesInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_IndustriesInfo>(list).Tables[0];
            }
        }

        public static ESP.MediaLinq.Entity.media_IndustriesInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_IndustriesInfo where t.IndustryID == id select t;
                return mdc.media_IndustriesInfo.FirstOrDefault(c => c.IndustryID == id);
            }
        }
    }
}
