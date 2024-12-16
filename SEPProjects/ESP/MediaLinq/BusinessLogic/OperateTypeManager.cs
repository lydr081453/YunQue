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
    public class OperateTypeManager : BaseEntityInfo
    {

        public static int Add(ESP.MediaLinq.Entity.media_OperateType info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_OperateType(info);
                mdc.SaveChanges();
                return info.ID;
            }

        }

        public static bool Update(ESP.MediaLinq.Entity.media_OperateType info)
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
                mdc.ApplyPropertyChanges("media_OperateType", info);
                mdc.SaveChanges();
                return true;
            }
        }

        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                ESP.MediaLinq.Entity.media_OperateType model = mdc.media_OperateType.FirstOrDefault(c => c.ID == id);
                if (model != null)
                {
                    mdc.DeleteObject((object)model);
                    return true;
                }
                else
                    return false;
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_OperateType> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_OperateType
                        select info;

                List<ESP.MediaLinq.Entity.media_OperateType> list = q.ToList();
                return list;
            }

        }

        public static DataSet GetDataSet()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_OperateType
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_OperateType>(q.ToList<ESP.MediaLinq.Entity.media_OperateType>());
                return list;
            }
        }

        public static DataTable GetAll()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_OperateType
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_OperateType>(q.ToList<ESP.MediaLinq.Entity.media_OperateType>());
                return list.Tables[0];
            }
        }



        public static ESP.MediaLinq.Entity.media_OperateType GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_OperateType where t.ID == id select t;
                return mdc.media_OperateType.FirstOrDefault(c => c.ID == id);
            }
        }

        //public static ESP.MediaLinq.Entity.media_OperateType GetModelByMediaID(int mediaid)
        //{
        //    using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
        //    {
        //        Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
        //        var q = from t in mdc.media_OperateType where t.MediaID == mediaid select t;
        //        return mdc.media_OperateType.FirstOrDefault(c => c.MediaID == mediaid);
        //    }
        //}



    }
}