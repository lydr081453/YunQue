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
    public class TableManager : BaseEntityInfo
    {

        public static int Add(ESP.MediaLinq.Entity.media_Tables info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_Tables(info);
                mdc.SaveChanges();
                return info.TableID;
            }

        }

        public static bool Update(ESP.MediaLinq.Entity.media_Tables info)
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
                mdc.ApplyPropertyChanges("media_Tables", info);
                mdc.SaveChanges();
                return true;
            }
        }

        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                ESP.MediaLinq.Entity.media_Tables model = mdc.media_Tables.FirstOrDefault(c => c.TableID == id);
                if (model != null)
                {
                    mdc.DeleteObject((object)model);
                    return true;
                }
                else
                    return false;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_Tables> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_Tables
                        select info;

                List<ESP.MediaLinq.Entity.media_Tables> list = q.ToList();
                return list;
            }

        }

        public static DataSet GetDataSet()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_Tables
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_Tables>(q.ToList<ESP.MediaLinq.Entity.media_Tables>());
                return list;
            }
        }

        public static DataTable GetAll()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_Tables
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_Tables>(q.ToList<ESP.MediaLinq.Entity.media_Tables>());
                return list.Tables[0];
            }
        }



        public static ESP.MediaLinq.Entity.media_Tables GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_Tables where t.TableID == id select t;
                return mdc.media_Tables.FirstOrDefault(c => c.TableID == id);
            }
        }

        //public static ESP.MediaLinq.Entity.media_Tables GetModelByMediaID(int mediaid)
        //{
        //    using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
        //    {
        //        Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
        //        var q = from t in mdc.media_Tables where t.MediaID == mediaid select t;
        //        return mdc.media_Tables.FirstOrDefault(c => c.MediaID == mediaid);
        //    }
        //}



    }
}