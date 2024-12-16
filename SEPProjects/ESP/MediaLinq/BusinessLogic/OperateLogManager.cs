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
    public class OperateLogManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_OperateLogInfo info)
        {           
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_OperateLogInfo(info);
                mdc.SaveChanges();
                return info.ID;
            }

        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="operatetypeid">操作类型</param>
        /// <param name="operatetableid">操作表</param>
        /// <param name="userid">操作人</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public static int Add(int operatetypeid, int operatetableid, int userid)
        {
            media_OperateLogInfo log = new media_OperateLogInfo();
            log.OperateTypeID = operatetypeid;
            log.OperateTableID = operatetableid;
            log.UserID = userid;
            log.OperateTime = DateTime.Now;
            log.Del = (int)Global.FiledStatus.Usable;
            return Add(log);
        }

        public static bool Update(ESP.MediaLinq.Entity.media_OperateLogInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                try
                {
                    mdc.SaveChanges();
                    return true;
                }
                catch (Exception ex)
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
                
                    ESP.MediaLinq.Entity.media_OperateLogInfo model = mdc.media_OperateLogInfo.FirstOrDefault(c => c.ID == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
               
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_OperateLogInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_OperateLogInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_OperateLogInfo> list = q.ToList();
                return list;
            }

        }

        public static DataSet GetDataSet()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_OperateLogInfo
                        select info;

                DataSet list = ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_OperateLogInfo>(q.ToList<ESP.MediaLinq.Entity.media_OperateLogInfo>());
                return list;
            }
        }



        public static ESP.MediaLinq.Entity.media_OperateLogInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_OperateLogInfo where t.ID == id select t;
                return mdc.media_OperateLogInfo.FirstOrDefault(c => c.ID == id);
            }
        }
    }
}

