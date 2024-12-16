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
    public class BranchManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_BranchInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_BranchInfo(info);
                mdc.SaveChanges();
                return info.id;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_BranchInfo info)
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
                
                    ESP.MediaLinq.Entity.media_BranchInfo model = mdc.media_BranchInfo.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
               
            }
        }

        public static bool DeleteBranchByMediaItemID(int mediaItemID)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                try
                {
                    Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                    var q = from info in mdc.media_BranchInfo
                            where info.mediaitemid == mediaItemID
                            select info;
                    foreach (object branch in q)
                    {
                        mdc.DeleteObject(branch);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }            
        }


        public static IList<ESP.MediaLinq.Entity.media_BranchInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_BranchInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_BranchInfo> list = q.ToList();
                return list;
            }
        }

        public static DataTable GetListByID(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_BranchInfo
                        where info.id == id
                        select info;

                List<ESP.MediaLinq.Entity.media_BranchInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_BranchInfo>(list).Tables[0];
            }
        }

        public static ESP.MediaLinq.Entity.media_BranchInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_BranchInfo where t.id == id select t;
                return mdc.media_BranchInfo.FirstOrDefault(c => c.id == id);
            }
        }

        public static DataTable GetListByMediaItemID(int mediaItemID)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_BranchInfo
                        where info.mediaitemid == mediaItemID
                        select info;

                List<ESP.MediaLinq.Entity.media_BranchInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_BranchInfo>(list).Tables[0];
            }
        }
    }
}
