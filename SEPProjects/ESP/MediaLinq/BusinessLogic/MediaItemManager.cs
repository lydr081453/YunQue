using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using ESP.MediaLinq.Utilities;
using ESP.Data;
using ESP.MediaLinq.Entity;
using System.Transactions;
using System.Data;
using System.Reflection;
namespace ESP.MediaLinq.BusinessLogic
{
    public struct MediaAttach
    {
        public string LogoFileName;//媒体LOGO文件名
        public byte[] LogoFileData;//媒体LOGO文件内容
        public string PriceFileName;//报价文件名
        public byte[] PriceFileData;//报价文件内容
        public string BriefFileName;//剪报文件名
        public byte[] BriefFileData;//剪报文件内容
    }

    public class MediaItemManager : BaseEntityInfo
    {
        public const int PriceFileType = 1;
        public const int BriefingFileType = 2;

        public static int Add(ESP.MediaLinq.Entity.media_MediaItemsInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_MediaItemsInfo(info);
                mdc.SaveChanges();
                return info.MediaitemID;
            }
        }

        /// <summary>
        /// 添加新媒体
        /// 插入所有选中的行业属性
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="attach">附件</param>
        /// <param name="errmsg">错误信息</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static int Add(media_MediaItemsInfo obj, List<media_BranchInfo> branchs, MediaAttach attach, int userid)
        {

            int ret = 0;
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                var q = from m in mdc.media_MediaItemsInfo
                        where m.MediaCName == obj.MediaCName && m.del == (int)Global.FiledStatus.Del
                        select m;
                if (obj.MediaItemType == Global.MediaItemType_Tv || obj.MediaItemType == Global.MediaItemType_Dab)
                {
                    q = from m in mdc.media_MediaItemsInfo
                        where m.MediaCName == obj.MediaCName && m.ChannelName == obj.ChannelName && m.TopicName == obj.TopicName && m.del == (int)Global.FiledStatus.Del
                        select m;
                }
                else if (obj.MediaItemType == Global.MediaItemType_Web)
                {
                    q = from m in mdc.media_MediaItemsInfo
                        where m.MediaCName == obj.MediaCName && m.ChannelName == obj.ChannelName && m.del == (int)Global.FiledStatus.Del
                        select m;
                }


                if (q.Any())
                {
                    ESP.Logging.Logger.Add("Save a new media is failed.", "Media system", ESP.Logging.LogLevel.Information, null);
                    return -1;
                }


                var mi = from mih in mdc.media_MediaItemsHistInfo
                         where mih.MediaitemID == obj.MediaitemID
                         select mih;
                int? version = mi.Max(x => (int?)x.version);

                if (version == null)
                {
                    version = 0;
                }
                obj.CurrentVersion = (int)version + 1;
                SaveMediaAttach(ref obj, attach);
                obj.del = (int)Global.FiledStatus.Usable;
                ret = ESP.MediaLinq.BusinessLogic.MediaItemManager.Add(obj);
                //obj.MediaitemID = ret;
                BranchManager.DeleteBranchByMediaItemID(obj.MediaitemID);
                if (branchs != null && branchs.Count > 0)
                {
                    for (int i = 0; i < branchs.Count; i++)
                    {
                        branchs[i].mediaitemid = obj.MediaitemID;
                        BranchManager.Add(branchs[i]);
                    }
                }
                obj.LastModifiedByUserID = userid;
                obj.LastModifiedIP = obj.LastModifiedIP;
                obj.LastModifiedDate = DateTime.Now;


                SaveHist(obj, null, userid, (int)version);

                media_OperateLogInfo log = new media_OperateLogInfo();
                log.OperateTypeID = (int)Global.SysOperateType.Add;
                log.OperateTableID = (int)Global.Tables.Media;
                log.UserID = userid;
                log.OperateTime = DateTime.Now;
                log.Del = (int)Global.FiledStatus.Usable;
                //OperateLogManager.Add(log);
                ESP.Logging.Logger.Add("Save a new media is sucess.", "Media system", ESP.Logging.LogLevel.Information);



                return ret;
            }

        }

        public static bool Update(ESP.MediaLinq.Entity.media_MediaItemsInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                if (info != null)
                {
                    mdc.GetObjectByKey(info.EntityKey);
                    mdc.ApplyPropertyChanges("media_MediaItemsInfo", info);
                    mdc.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// Updates the mediaitem obj.
        /// </summary>
        /// <param name="obj">The mediaitem object for update.</param>
        /// <param name="branchs">The branchs.</param>
        /// <param name="attach">The attach.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Update(media_MediaItemsInfo obj, List<media_BranchInfo> branchs, MediaAttach attach, int userid)
        {

            int ret = 0;
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                var q = from m in mdc.media_MediaItemsInfo
                        where m.MediaCName == obj.MediaCName && m.del == (int)Global.FiledStatus.Del
                        select m;
                if (obj.MediaItemType == Global.MediaItemType_Tv || obj.MediaItemType == Global.MediaItemType_Dab)
                {
                    q = from m in mdc.media_MediaItemsInfo
                        where m.MediaCName == obj.MediaCName && m.ChannelName == obj.ChannelName && m.TopicName == obj.TopicName && m.del == (int)Global.FiledStatus.Del
                        select m;
                }
                else if (obj.MediaItemType == Global.MediaItemType_Web)
                {
                    q = from m in mdc.media_MediaItemsInfo
                        where m.MediaCName == obj.MediaCName && m.ChannelName == obj.ChannelName && m.del == (int)Global.FiledStatus.Del
                        select m;
                }


                if (q.Any())
                {
                    ESP.Logging.Logger.Add("Save a new media is failed.", "Media system", ESP.Logging.LogLevel.Information, null);
                    return -1;
                }


                var mi = from mih in mdc.media_MediaItemsHistInfo
                         where mih.MediaitemID == obj.MediaitemID
                         select mih;
                int? version = mi.Max(x => (int?)x.version);

                if (version == null)
                {
                    version = 0;
                }
                obj.CurrentVersion = (int)version + 1;
                SaveMediaAttach(ref obj, attach);
                if (Update(obj))
                {
                    BranchManager.DeleteBranchByMediaItemID(obj.MediaitemID);
                    if (branchs != null && branchs.Count > 0)
                    {
                        for (int i = 0; i < branchs.Count; i++)
                        {
                            branchs[i].mediaitemid = obj.MediaitemID;
                            BranchManager.Add(branchs[i]);
                        }
                    }
                    obj.LastModifiedByUserID = userid;
                    obj.LastModifiedIP = obj.LastModifiedIP;
                    obj.LastModifiedDate = DateTime.Now;

                    SaveHist(obj, null, userid, (int)version);

                    media_OperateLogInfo log = new media_OperateLogInfo();
                    log.OperateTypeID = (int)Global.SysOperateType.Edit;
                    log.OperateTableID = (int)Global.Tables.Media;
                    log.UserID = userid;
                    log.OperateTime = DateTime.Now;
                    log.Del = (int)Global.FiledStatus.Usable;
                    //OperateLogManager.Add(log);
                    ESP.Logging.Logger.Add("Save a new media is sucess.", "Media system", ESP.Logging.LogLevel.Information);
                }



                return 1;
            }
        }


        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                ESP.MediaLinq.Entity.media_MediaItemsInfo model = mdc.media_MediaItemsInfo.FirstOrDefault(c => c.MediaitemID == id);
                if (model != null)
                {
                    mdc.DeleteObject((object)model);
                    return true;
                }
                else
                    return false;

            }
        }

        /// <summary>
        /// 对未审核通过的媒体进行物理删除
        /// </summary>
        /// <param name="id">要删除的媒体ID</param>
        /// <param name="userid">操作人</param>
        /// <param name="errmsg">返回信息</param>
        /// <returns></returns>
        public static bool PhysicalDel(int id, int userid, out string errmsg)
        {
            errmsg = "删除失败";
            if (id <= 0) return false;
            media_MediaItemsInfo item = GetModel(id);
            if (item.Status == (int)Global.MediaAuditStatus.FirstLevelAudit)
            {
                errmsg = "已审核通过的媒体，不能物理删除！";
                ESP.Logging.Logger.Add("Delete a media is failed.", "Media system", ESP.Logging.LogLevel.Information, null, (object)(errmsg + " ID=" + id.ToString()));
                return false;
            }
            return Delete(id);
        }

        public static bool Delete(ESP.MediaLinq.Entity.media_MediaItemsInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                try
                {
                    ///置位为已经删除
                    info.del = (int)Global.FiledStatus.Del;
                    mdc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes the specified obj. logic delete
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Delete(media_MediaItemsInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            try
            {

                using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
                {
                    Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                    obj.del = (int)Global.FiledStatus.Del;

                    Update(obj);

                    MediaIndustryRelationManager.modifyforDelMediaItem(obj.MediaitemID);

                    ESP.Logging.Logger.Add("Delete a media is success.", "Media system", ESP.Logging.LogLevel.Information);
                }
                return 1;
            }
            catch
            {
                errmsg = "删除失败!";
                ESP.Logging.Logger.Add("Delete a media is failed.", "Media system", ESP.Logging.LogLevel.Information, null, (object)(errmsg + " ID=" + obj.MediaitemID.ToString()));
                return -3;

            }


        }

        public static IList<ESP.MediaLinq.Entity.media_MediaItemsInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_MediaItemsInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_MediaItemsInfo> list = q.ToList();
                return list;
            }
        }

        public static ESP.MediaLinq.Entity.media_MediaItemsInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_MediaItemsInfo where t.MediaitemID == id select t;
                return mdc.media_MediaItemsInfo.FirstOrDefault(c => c.MediaitemID == id);
            }
        }

        public static DataTable GetUnAuditList(string MediaType, string Industry, string RegionAttribute, string CountryID, string provinceid, string CityID, string MediaCName)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                var q1 = from t in mdc.media_MediaItemsInfo
                         join mediatype in mdc.media_mediaTypeInfo on t.MediaItemType equals mediatype.id
                         where t.Status == (int)Global.MediaAuditStatus.Submit
                         select new
                         {
                             t.MediaitemID,
                             t.MediaCName,
                             t.MediaEName,
                             t.CShortName,
                             t.EShortName,
                             t.MediaItemType,
                             t.CurrentVersion,
                             t.Status,
                             t.CreatedByUserID,
                             t.CreatedDate,
                             t.LastModifiedByUserID,
                             t.LastModifiedDate,
                             t.MediumSort,
                             t.RegionAttribute,
                             t.IndustryID,
                             t.countryid,
                             t.provinceid,
                             t.cityid,
                             t.MediaType,
                             t.TelephoneExchange,
                             t.ChannelName,
                             t.TopicName,
                             t.IssueRegion,
                             t.PublishPeriods,
                             mediatype.name

                         };
                var q2 = q1.GroupJoin(mdc.media_IndustriesInfo, (mi) => mi.IndustryID, (i) => i.IndustryID,
                    (mi, iCol) => new { MI = mi, I = iCol.FirstOrDefault() });
                var q3 = q2.GroupJoin(mdc.media_CountryInfo, (mi) => mi.MI.countryid, (country) => country.CountryID,
                    (mi, countryCol) => new { mi.MI, mi.I, Country = countryCol.FirstOrDefault() });
                var q4 = q3.GroupJoin(mdc.media_ProvinceInfo, (mi) => mi.MI.provinceid, (province) => province.Province_ID,
                    (mi, provinceCol) => new { mi.MI, mi.I, mi.Country, Province = provinceCol.FirstOrDefault() });
                var q5 = q4.GroupJoin(mdc.media_CityInfo, (mi) => mi.MI.cityid, (city) => city.City_ID,
                    (mi, cityCol) => new { mi.MI, mi.I, mi.Country, mi.Province, City = cityCol.FirstOrDefault() });
                var q = from mi in q5
                        where mi.MI.Status == (int)Global.MediaAuditStatus.Submit
                        select new
                        {
                            mediaitemid = mi.MI.MediaitemID,
                            mediacname = mi.MI.MediaCName,
                            mediaename = mi.MI.MediaEName,
                            cshortname = mi.MI.CShortName,
                            eshortname = mi.MI.EShortName,
                            mediaitemtype = mi.MI.MediaItemType,
                            currentversion = mi.MI.CurrentVersion,
                            status = mi.MI.Status,
                            createdbyuserid = mi.MI.CreatedByUserID,
                            createddate = mi.MI.CreatedDate,
                            lastmodifiedbyuserid = mi.MI.LastModifiedByUserID,
                            lastmodifieddate = mi.MI.LastModifiedDate,
                            mediumsort = mi.MI.MediumSort,
                            RegionAttribute = mi.MI.RegionAttribute,
                            industryid = mi.MI.IndustryID,
                            Countryid = mi.MI.countryid,
                            provinceid = mi.MI.provinceid,
                            cityid = mi.MI.cityid,
                            MediaType = mi.MI.MediaType,
                            TelephoneExchange = mi.MI.TelephoneExchange,
                            medianame = "",
                            issueregion = mi.MI.IssueRegion,
                            industryname = mi.I.IndustryName,
                            mi.Country.CountryName,
                            mi.Province.Province_Name,
                            mi.City.City_Name,
                            headquarter = "",
                            publishPeriods = mi.MI.PublishPeriods,
                            mediatypename = mi.MI.name,
                            ChannelName = mi.MI.ChannelName,
                            TopicName = mi.MI.TopicName
                        };
                if (!string.IsNullOrEmpty(MediaType))
                {
                    int typeID = Convert.ToInt32(MediaType);
                    q = q.Where(x => x.mediaitemtype == typeID);
                }
                if (!string.IsNullOrEmpty(Industry))
                {
                    int ind = int.Parse(Industry);
                    q = q.Where(x => x.industryid == ind);
                }
                if (!string.IsNullOrEmpty(RegionAttribute))
                {
                    int ra = int.Parse(RegionAttribute);
                    q = q.Where(x => x.RegionAttribute == ra);
                    if (!string.IsNullOrEmpty(CountryID))
                    {
                        int cid = int.Parse(CountryID);
                        q = q.Where(x => x.Countryid == cid);
                    }
                    if (!string.IsNullOrEmpty(provinceid))
                    {
                        int pid = int.Parse(provinceid);
                        q = q.Where(x => x.provinceid == pid);
                        if (!string.IsNullOrEmpty(CityID))
                        {
                            int city = int.Parse(CityID);
                            q = q.Where(x => x.cityid == city);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(MediaCName))
                {
                    q = q.Where(x => x.mediacname.Contains(MediaCName) || x.ChannelName.Contains(MediaCName) || x.TopicName.Contains(MediaCName));
                }

                //List<media_MediaItemsInfo> list = q.ToList();
                //return ListToDataSet.ToDataSet<media_MediaItemsInfo>(list).Tables[0];
                var list = q.ToList();

                MethodInfo method = typeof(ListToDataSet).GetMethod("ToDataSets");
                MethodInfo method2 = method.MakeGenericMethod(q.ElementType);
                DataSet ds = (DataSet)method2.Invoke(null, new object[] { list });

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["medianame"] = (dr.IsNull("mediacname") ? " " : dr["mediacname"].ToString()) + ""
                                        + (dr.IsNull("ChannelName") ? " " : dr["ChannelName"].ToString()) + ""
                                        + (dr.IsNull("TopicName") ? " " : dr["TopicName"].ToString());
                    dr["headquarter"] = (dr.IsNull("CountryName") ? " " : dr["CountryName"].ToString()) + ""
                                        + (dr.IsNull("Province_Name") ? " " : dr["Province_Name"].ToString()) + ""
                                        + (dr.IsNull("City_Name") ? " " : dr["City_Name"].ToString());

                }

                return ds.Tables[0];
            }
        }

        public static DataTable GetAuditList(string MediaType, string Industry, string RegionAttribute, string CountryID, string provinceid, string CityID, string MediaCName)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                var q1 = from t in mdc.media_MediaItemsInfo
                         join mediatype in mdc.media_mediaTypeInfo on t.MediaItemType equals mediatype.id
                         where t.Status == (int)Global.MediaAuditStatus.FirstLevelAudit
                         select new
                         {
                             t.MediaitemID,
                             t.MediaCName,
                             t.MediaEName,
                             t.CShortName,
                             t.EShortName,
                             t.MediaItemType,
                             t.CurrentVersion,
                             t.Status,
                             t.CreatedByUserID,
                             t.CreatedDate,
                             t.LastModifiedByUserID,
                             t.LastModifiedDate,
                             t.MediumSort,
                             t.RegionAttribute,
                             t.IndustryID,
                             t.countryid,
                             t.provinceid,
                             t.cityid,
                             t.MediaType,
                             t.TelephoneExchange,
                             t.ChannelName,
                             t.TopicName,
                             t.IssueRegion,
                             t.PublishPeriods,
                             mediatype.name

                         };
                var q2 = q1.GroupJoin(mdc.media_IndustriesInfo, (mi) => mi.IndustryID, (i) => i.IndustryID,
                    (mi, iCol) => new { MI = mi, I = iCol.FirstOrDefault() });
                var q3 = q2.GroupJoin(mdc.media_CountryInfo, (mi) => mi.MI.countryid, (country) => country.CountryID,
                    (mi, countryCol) => new { mi.MI, mi.I, Country = countryCol.FirstOrDefault() });
                var q4 = q3.GroupJoin(mdc.media_ProvinceInfo, (mi) => mi.MI.provinceid, (province) => province.Province_ID,
                    (mi, provinceCol) => new { mi.MI, mi.I, mi.Country, Province = provinceCol.FirstOrDefault() });
                var q5 = q4.GroupJoin(mdc.media_CityInfo, (mi) => mi.MI.cityid, (city) => city.City_ID,
                    (mi, cityCol) => new { mi.MI, mi.I, mi.Country, mi.Province, City = cityCol.FirstOrDefault() });
                var q = from mi in q5
                        where mi.MI.Status == (int)Global.MediaAuditStatus.FirstLevelAudit
                        select new
                        {
                            mediaitemid = mi.MI.MediaitemID,
                            mediacname = mi.MI.MediaCName,
                            mediaename = mi.MI.MediaEName,
                            cshortname = mi.MI.CShortName,
                            eshortname = mi.MI.EShortName,
                            mediaitemtype = mi.MI.MediaItemType,
                            currentversion = mi.MI.CurrentVersion,
                            status = mi.MI.Status,
                            createdbyuserid = mi.MI.CreatedByUserID,
                            createddate = mi.MI.CreatedDate,
                            lastmodifiedbyuserid = mi.MI.LastModifiedByUserID,
                            lastmodifieddate = mi.MI.LastModifiedDate,
                            mediumsort = mi.MI.MediumSort,
                            RegionAttribute = mi.MI.RegionAttribute,
                            industryid = mi.MI.IndustryID,
                            Countryid = mi.MI.countryid,
                            provinceid = mi.MI.provinceid,
                            cityid = mi.MI.cityid,
                            MediaType = mi.MI.MediaType,
                            TelephoneExchange = mi.MI.TelephoneExchange,
                            medianame = "",
                            issueregion = mi.MI.IssueRegion,
                            industryname = mi.I.IndustryName,
                            mi.Country.CountryName,
                            mi.Province.Province_Name,
                            mi.City.City_Name,
                            headquarter = "",
                            publishPeriods = mi.MI.PublishPeriods,
                            mediatypename = mi.MI.name,
                            ChannelName = mi.MI.ChannelName,
                            TopicName = mi.MI.TopicName
                        };
                if (!string.IsNullOrEmpty(MediaType))
                {
                    int typeID = Convert.ToInt32(MediaType);
                    q = q.Where(x => x.mediaitemtype == typeID);
                }
                if (!string.IsNullOrEmpty(Industry))
                {
                    int ind = int.Parse(Industry);
                    q = q.Where(x => x.industryid == ind);
                }
                if (!string.IsNullOrEmpty(RegionAttribute))
                {
                    int ra = int.Parse(RegionAttribute);
                    q = q.Where(x => x.RegionAttribute == ra);
                    if (!string.IsNullOrEmpty(CountryID))
                    {
                        int cid = int.Parse(CountryID);
                        q = q.Where(x => x.Countryid == cid);
                    }
                    if (!string.IsNullOrEmpty(provinceid))
                    {
                        int pid = int.Parse(provinceid);
                        q = q.Where(x => x.provinceid == pid);
                        if (!string.IsNullOrEmpty(CityID))
                        {
                            int city = int.Parse(CityID);
                            q = q.Where(x => x.cityid == city);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(MediaCName))
                {
                    q = q.Where(x => x.mediacname.Contains(MediaCName) || x.ChannelName.Contains(MediaCName) || x.TopicName.Contains(MediaCName));
                }

                //List<media_MediaItemsInfo> list = q.ToList();
                //return ListToDataSet.ToDataSet<media_MediaItemsInfo>(list).Tables[0];
                var list = q.ToList();

                MethodInfo method = typeof(ListToDataSet).GetMethod("ToDataSets");
                MethodInfo method2 = method.MakeGenericMethod(q.ElementType);
                DataSet ds = (DataSet)method2.Invoke(null, new object[] { list });

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["medianame"] = (dr.IsNull("mediacname") ? " " : dr["mediacname"].ToString()) + ""
                                        + (dr.IsNull("ChannelName") ? " " : dr["ChannelName"].ToString()) + ""
                                        + (dr.IsNull("TopicName") ? " " : dr["TopicName"].ToString());
                    dr["headquarter"] = (dr.IsNull("CountryName") ? " " : dr["CountryName"].ToString()) + ""
                                        + (dr.IsNull("Province_Name") ? " " : dr["Province_Name"].ToString()) + ""
                                        + (dr.IsNull("City_Name") ? " " : dr["City_Name"].ToString());

                }
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 保存附件信息
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="attach">The attach.</param>
        private static void SaveMediaAttach(ref media_MediaItemsInfo obj, MediaAttach attach)
        {
            if (!string.IsNullOrEmpty(attach.LogoFileName) && attach.LogoFileName.Length > 0)
            {
                obj.MediaLogo = attach.LogoFileName.ToString();
            }
            if (attach.PriceFileData != null)
            {
                media_MediaAttachmentsInfo mediaAttach = new media_MediaAttachmentsInfo();
                mediaAttach.MediaItemID = obj.MediaitemID;
                mediaAttach.Type = PriceFileType;


                mediaAttach.Version = GetLastMediaAttachVersion(PriceFileType, obj.MediaitemID);

                mediaAttach.AttachmentPath = ESP.MediaLinq.Utilities.SaveFIleHelper.SaveFile(ESP.MediaLinq.Utilities.ConfigManager.MediaBriefPath, attach.PriceFileName, attach.PriceFileData, true);
                mediaAttach.CreatedByUserID = (int)obj.LastModifiedByUserID;
                mediaAttach.CreatedDate = (DateTime)obj.LastModifiedDate;
                mediaAttach.Createdip = obj.CreatedIP;

                obj.AdsPrice = ESP.MediaLinq.BusinessLogic.MediaAttachmentsManager.Add(mediaAttach);
            }
            if (attach.BriefFileData != null)
            {
                media_MediaAttachmentsInfo mediaAttach = new media_MediaAttachmentsInfo();
                mediaAttach.MediaItemID = obj.MediaitemID;
                mediaAttach.Type = BriefingFileType;
                mediaAttach.Version = GetLastMediaAttachVersion(BriefingFileType, obj.MediaitemID);

                mediaAttach.AttachmentPath = ESP.MediaLinq.Utilities.SaveFIleHelper.SaveFile(ESP.MediaLinq.Utilities.ConfigManager.MediaBriefPath, attach.BriefFileName, attach.BriefFileData, true);
                mediaAttach.CreatedByUserID = (int)obj.LastModifiedByUserID;
                mediaAttach.CreatedDate = (DateTime)obj.LastModifiedDate;
                mediaAttach.Createdip = obj.CreatedIP;
                obj.Briefing = ESP.MediaLinq.BusinessLogic.MediaAttachmentsManager.Add(mediaAttach);
            }
        }

        /// <summary>
        /// Gets the last media attach version.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <param name="type">The type.</param>
        /// <param name="mid">The mid.</param>
        /// <returns></returns>
        private static int GetLastMediaAttachVersion(int type, int mid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from ma in mdc.media_MediaAttachmentsInfo
                        where ma.Type == type && ma.MediaItemID == mid
                        select ma;
                int? version = q.Max(x => (int?)x.Version);

                if (version == null)
                {
                    version = 0;
                }
                return (int)version;
            }
        }

        /// <summary>
        /// Saves the hist.
        /// </summary>        
        /// <param name="obj">The obj.</param>
        /// <param name="branchs">The branchs.</param>
        /// <param name="userid">The userid.</param>
        private static void SaveHist(media_MediaItemsInfo obj, List<media_BranchInfo> branchs, int userid, int MaxVersion)
        {
            media_MediaItemsHistInfo hist = new media_MediaItemsHistInfo();
            hist.LastModifiedByUserID = userid;
            hist.LastModifiedIP = obj.LastModifiedIP;
            hist.LastModifiedDate = DateTime.Now;

            hist.MediaitemID = obj.MediaitemID;
            hist.MediaCName = obj.MediaCName;
            hist.MediaEName = obj.MediaEName;
            hist.CShortName = obj.CShortName;
            hist.EShortName = obj.EShortName;
            hist.MediaItemType = obj.MediaItemType;
            hist.MediumSort = obj.MediumSort;
            hist.ReaderSort = obj.ReaderSort;
            hist.GoverningBody = obj.GoverningBody;
            hist.FrontFor = obj.FrontFor;
            hist.Proprieter = obj.Proprieter;
            hist.SubProprieter = obj.SubProprieter;
            hist.ChiefEditor = obj.ChiefEditor;
            hist.AdminEditor = obj.AdminEditor;
            hist.Subeditor = obj.Subeditor;
            hist.Manager = obj.Manager;
            hist.Zhuren = obj.Zhuren;
            hist.Producer = obj.Producer;
            hist.StartPublication = obj.StartPublication;
            hist.PublishDate = obj.PublishDate;
            hist.TelephoneExchange = obj.TelephoneExchange;
            hist.Fax = obj.Fax;
            hist.AddressOne = obj.AddressOne;
            hist.AddressTwo = obj.AddressTwo;
            hist.WebAddress = obj.WebAddress;
            hist.ISSN = obj.ISSN;
            hist.Cooperate = obj.Cooperate;
            //hist.IndustryProperty = obj.IndustryProperty;
            hist.Circulation = obj.Circulation;
            hist.PublishChannels = obj.PublishChannels;
            hist.PhoneOne = obj.PhoneOne;
            hist.PhoneTwo = obj.PhoneTwo;
            hist.EndPublication = obj.EndPublication;
            hist.AdsPhone = obj.AdsPhone;
            hist.AdsPrice = obj.AdsPrice;
            hist.MediaLogo = obj.MediaLogo;
            hist.Briefing = obj.Briefing;
            hist.MediaIntro = obj.MediaIntro;
            hist.EngIntro = obj.EngIntro;
            hist.Remarks = obj.Remarks;
            hist.ChannelName = obj.ChannelName;
            hist.DABFM = obj.DABFM;
            hist.TopicName = obj.TopicName;
            hist.TopicProperty = obj.TopicProperty;
            hist.OverrideRange = obj.OverrideRange;
            hist.Rating = obj.Rating;
            hist.TopicTime = obj.TopicTime;
            hist.ChannelWebAddress = obj.ChannelWebAddress;
            hist.CreatedByUserID = (int)obj.LastModifiedByUserID;
            hist.CreatedDate = (DateTime)obj.LastModifiedDate;
            hist.CreatedIP = obj.LastModifiedIP;
            hist.Status = obj.Status;

            hist.version = MaxVersion;

            hist.PostCode = obj.PostCode;
            hist.RegionAttribute = obj.RegionAttribute;
            hist.Override_Countryid = obj.Override_Countryid;
            hist.Override_Provinceid = obj.Override_Provinceid;
            hist.Override_Cityid = obj.Override_Cityid;
            hist.Override_describe = obj.Override_describe;
            hist.IndustryID = obj.IndustryID;
            hist.IssueRegion = obj.IssueRegion;
            hist.countryid = obj.countryid;
            hist.provinceid = obj.provinceid;
            hist.cityid = obj.cityid;
            hist.addr1_cityid = obj.addr1_cityid;
            hist.addr1_countryid = obj.addr1_countryid;
            hist.addr1_provinceid = obj.addr1_provinceid;

            hist.PublishPeriods = obj.PublishPeriods;
            if (branchs != null && branchs.Count > 0)
            {
                for (int i = 0; i < branchs.Count; i++)
                {
                    hist.branchs += branchs[i].cityname + ",";
                }
                hist.branchs.Trim(',');
            }


            int hisid = ESP.MediaLinq.BusinessLogic.MediaItemsHistManager.Add(hist);
            //if (hisid > 0)
            //    ESP.Logging.Logger.Add("Save a mediahistory is success.", "Media system", ESP.Logging.LogLevel.Information);
            //else
            //    ESP.Logging.Logger.Add("Save a mediahistory is failed.", "Media system", ESP.Logging.LogLevel.Information);

        }

        /// <summary>
        /// 审核媒体
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int AuditMedia(media_MediaItemsInfo obj, int userid, out string errmsg)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                errmsg = string.Empty;
                obj.Status = (int)Global.MediaAuditStatus.FirstLevelAudit;
                if (Update(obj))
                {
                    //   ESP.Logging.Logger.Add("Audit a media is success.", "Media system", ESP.Logging.LogLevel.Information);
                    return 1;
                }
                else
                {
                    errmsg = "修改失败!";
                    //    ESP.Logging.Logger.Add("Submit a media is failed.", "Media system", ESP.Logging.LogLevel.Information, null, (object)(errmsg + " MediaID=" + obj.MediaitemID.ToString()));
                    return -3;
                }
            }
        }



    }
}