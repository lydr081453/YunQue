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
    public class AgencyManager : BaseEntityInfo
    {        

        public static int Add(ESP.MediaLinq.Entity.media_AgencyInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_AgencyInfo(info);
                mdc.SaveChanges();
                return info.AgencyID;
            }
           
        }

        public static bool Update(ESP.MediaLinq.Entity.media_AgencyInfo info)
        {
            //using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            //{
            //    Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
            //    try
            //    {
            //        mdc.SaveChanges();
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        return false;
            //    }
            //}

            if (info == null)
                return false;

            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                mdc.GetObjectByKey(info.EntityKey);
                mdc.ApplyPropertyChanges("media_AgencyInfo", info);
                mdc.SaveChanges();
                return true;
            }
        }

        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                
                    ESP.MediaLinq.Entity.media_AgencyInfo model = mdc.media_AgencyInfo.FirstOrDefault(c => c.AgencyID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;                
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_AgencyInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_AgencyInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_AgencyInfo> list = q.ToList();
                return list;
            }
            
        }

        public static DataSet GetDataSet()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_AgencyInfo
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_AgencyInfo>(q.ToList<ESP.MediaLinq.Entity.media_AgencyInfo>());
                return list;
            }
        }

        public static DataSet GetDataSet(string cname,string mid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_AgencyInfo
                        select info;


                if (!string.IsNullOrEmpty(mid))
                {
                    int mediaid = int.Parse(mid);
                    q = q.Where(x => x.MediaID == mediaid);
                }

                if (!string.IsNullOrEmpty(cname))
                {                    
                    q = q.Where(x => x.AgencyCName == cname || x.AgencyEName == cname);
                }

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_AgencyInfo>(q.ToList<ESP.MediaLinq.Entity.media_AgencyInfo>());
                return list;
            }
        }



        public static ESP.MediaLinq.Entity.media_AgencyInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_AgencyInfo where t.AgencyID == id select t;
                return mdc.media_AgencyInfo.FirstOrDefault(c => c.AgencyID == id);
            }
        }

        public static ESP.MediaLinq.Entity.media_AgencyInfo GetModelByMediaID(int mediaid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_AgencyInfo where t.MediaID == mediaid select t;
                return mdc.media_AgencyInfo.FirstOrDefault(c => c.MediaID == mediaid);
            }
        }



    }
    
}
