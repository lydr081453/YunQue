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
    public class CounterManager : BaseEntityInfo
    {        

        public static int Add(ESP.MediaLinq.Entity.media_Counter info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_Counter(info);
                mdc.SaveChanges();
                return info.IntegralID;
            }
           
        }

        public static bool Update(ESP.MediaLinq.Entity.media_Counter info)
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
                mdc.ApplyPropertyChanges("media_Counter", info);
                mdc.SaveChanges();
                return true;
            }
        }

        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                
                    ESP.MediaLinq.Entity.media_Counter model = mdc.media_Counter.FirstOrDefault(c => c.IntegralID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;                
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_Counter> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_Counter
                        select info;

                List<ESP.MediaLinq.Entity.media_Counter> list = q.ToList();
                return list;
            }

        }

        public static DataTable GetListByUserID(int userid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_Counter
                        where info.UserID == userid
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_Counter>(q.ToList<ESP.MediaLinq.Entity.media_Counter>());
                return list.Tables[0];
            }

        }

        public static DataSet GetDataSet()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_Counter
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_Counter>(q.ToList<ESP.MediaLinq.Entity.media_Counter>());
                return list;
            }
        }

        public static DataTable GetAll()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_Counter
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_Counter>(q.ToList<ESP.MediaLinq.Entity.media_Counter>());
                return list.Tables[0];
            }
        }



        public static ESP.MediaLinq.Entity.media_Counter GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_Counter where t.IntegralID == id select t;
                return mdc.media_Counter.FirstOrDefault(c => c.IntegralID == id);
            }
        }

        //public static ESP.MediaLinq.Entity.media_Counter GetModelByMediaID(int mediaid)
        //{
        //    using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
        //    {
        //        Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
        //        var q = from t in mdc.media_Counter where t.MediaID == mediaid select t;
        //        return mdc.media_Counter.FirstOrDefault(c => c.MediaID == mediaid);
        //    }
        //}



    }
}