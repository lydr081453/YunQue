using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using System.Data;
using ESP.MediaLinq.Utilities;
using ESP.MediaLinq.Entity;
using System.Reflection;
using ESP.Data;
namespace ESP.MediaLinq.BusinessLogic
{
    public class MediaIndustryRelationManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_mediaIndustryRelationInfo(info);
                mdc.SaveChanges();
                return info.id;
            }
        }

        public static bool Update(ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo info)
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
                
                    ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo model = mdc.media_mediaIndustryRelationInfo.FirstOrDefault(c => c.id == id);
                    if (model != null)
                    {
                        mdc.DeleteObject((object)model);
                        return true;
                    }
                    else
                        return false;   
               
            }
        }
        // <summary>
        /// 删除行业属性(置位)
        /// </summary>
        /// <param name="industries"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int modifyforDelMediaItem(int itemid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                if (itemid <= 0) return -1;//媒体id错误
                DataTable dt = GetAllListByMediaid(itemid);
                if (dt == null || dt.Rows.Count <= 0) return 0;
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    media_mediaIndustryRelationInfo industry = GetModel(Convert.ToInt32(dt.Rows[i]["Id"]));
                    industry.industryid = dt.Rows[i]["Industryid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["Industryid"]);
                    industry.del = (int)Global.FiledStatus.Del;
                    if (Update(industry))
                    {
                        count++;
                    }
                }
                return count;
            }
        }


        public static IList<ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_mediaIndustryRelationInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo> list = q.ToList();
                return list;
            }
        }

        public static DataTable GetListByID(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_mediaIndustryRelationInfo
                        where info.id == id
                        select info;

                List<ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo> list = q.ToList();
                return ListToDataSet.ToDataSet<ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo>(list).Tables[0];
            }
        }

        public static ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_mediaIndustryRelationInfo where t.id == id select t;
                return mdc.media_mediaIndustryRelationInfo.FirstOrDefault(c => c.id == id);
            }
        }

        public static DataTable GetAllListByMediaid(int mediaitemid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from m in mdc.media_mediaIndustryRelationInfo
                        join i in mdc.media_IndustriesInfo on m.industryid equals i.IndustryID
                        where m.mediaitemid == mediaitemid && m.del != (int)Global.FiledStatus.Del && i.del != (int)Global.FiledStatus.Del
                        orderby m.id
                        select new
                        {
                            ID = m.id,
                            IndustryID = m.industryid,
                            MediaitemID = m.mediaitemid,
                            RelationID = m.id,
                            Del = m.del,
                            IndustryName = i.IndustryName

                        };

                var list = q.ToList();

                MethodInfo method = typeof(ListToDataSet).GetMethod("ToDataSets");
                MethodInfo method2 = method.MakeGenericMethod(q.ElementType);
                DataSet ds = (DataSet)method2.Invoke(null, new object[] { list });

                return ds.Tables[0];
            }
        }


        public static ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo[] GetAllIndustryByMedia(int mediaitemid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                DataTable dt = GetAllListByMediaid(mediaitemid);
                if (dt == null || dt.Rows.Count <= 0) return null;
                ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo[] industries = new ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo[dt.Rows.Count];
                for (int i = 0; i < industries.Length; i++)
                {
                    industries[i] = GetModel(Convert.ToInt32(dt.Rows[i]["ID"]));
                }
                return industries;
            }
        }
    }
}