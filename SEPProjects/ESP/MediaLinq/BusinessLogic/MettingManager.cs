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
    public class MettingManager : BaseEntityInfo
    {        

        public static int Add(ESP.MediaLinq.Entity.Media_Meetings info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddToMedia_Meetings(info);
                mdc.SaveChanges();
                return info.id;
            }
           
        }

        public static bool Update(ESP.MediaLinq.Entity.Media_Meetings info)
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
                mdc.ApplyPropertyChanges("Media_Meetings", info);
                mdc.SaveChanges();
                return true;
            }
        }

        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                
                    ESP.MediaLinq.Entity.Media_Meetings model = mdc.Media_Meetings.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;                
            }
        }

        public static IList<ESP.MediaLinq.Entity.Media_Meetings> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.Media_Meetings
                        select info;

                List<ESP.MediaLinq.Entity.Media_Meetings> list = q.ToList();
                return list;
            }

        }

        public static DataSet GetDataSet()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.Media_Meetings
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.Media_Meetings>(q.ToList<ESP.MediaLinq.Entity.Media_Meetings>());
                return list;
            }
        }

        public static DataTable GetAll()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.Media_Meetings
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.Media_Meetings>(q.ToList<ESP.MediaLinq.Entity.Media_Meetings>());
                return list.Tables[0];
            }
        }



        public static ESP.MediaLinq.Entity.Media_Meetings GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.Media_Meetings where t.id == id select t;
                return mdc.Media_Meetings.FirstOrDefault(c => c.id == id);
            }
        }

        //public static ESP.MediaLinq.Entity.Media_Meetings GetModelByMediaID(int mediaid)
        //{
        //    using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
        //    {
        //        Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
        //        var q = from t in mdc.Media_Meetings where t.MediaID == mediaid select t;
        //        return mdc.Media_Meetings.FirstOrDefault(c => c.MediaID == mediaid);
        //    }
        //}



    }
}