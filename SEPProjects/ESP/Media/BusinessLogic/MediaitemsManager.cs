using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.BusinessLogic
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

    public class MediaitemsManager
    {
        public const int PriceFileType = 1;
        public const int BriefingFileType = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Media_mediaitems"/> class.
        /// </summary>
        public MediaitemsManager()
        {
        }

        public static List<QueryMediaItemInfo> GetAllObjectList(string term, List<SqlParameter> param)
        {
            Hashtable ht = new Hashtable();
            if (param != null)
            {
                for (int i = 0; i < param.Count; i++)
                {
                    ht.Add(param[i].ParameterName, param[i].Value);
                }
            }
            DataTable dt = GetAuditList(term, ht);
            var query = from querymediaitem in dt.AsEnumerable() select new QueryMediaItemInfo(querymediaitem);
            List<QueryMediaItemInfo> items = new List<QueryMediaItemInfo>();
            foreach (QueryMediaItemInfo item in query)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Gets the media model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static MediaitemsInfo GetModel(int id)
        {
            return ESP.Media.DataAccess.MediaitemsDataProvider.Load(id);
        }

        /// <summary>
        /// Gets all media names.
        /// </summary>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<string> GetAllNames()
        {
            DataTable dt = GetAutoList(null, null);
            List<string> names = new List<string>();
            if (dt == null || dt.Rows.Count == 0)
                return names;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                names.Add(dt.Rows[i]["MediaCName"] == DBNull.Value ? string.Empty : dt.Rows[i]["MediaCName"].ToString());
            }
            return names;
        }

        /// <summary>
        /// get the channel name by mediacname
        /// </summary>
        /// <param name="strCnName">mediacname</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<string> GetChannelNameByCnName(string strCnName)
        {
            List<string> channelName= new List<string>();

            string term = string.Empty;
            Hashtable ht = new Hashtable();

            term += " and a.MediaCName = @MediaCName";
            if (!ht.ContainsKey("@MediaCName"))
            {
                ht.Add("@MediaCName", strCnName);
            }
            //ht.Add("@del", (int)Global.FiledStatus.Usable);

            DataTable dt = GetAutoList(term, ht);
            if (dt == null || dt.Rows.Count == 0)
                return channelName;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                channelName.Add(dt.Rows[i]["ChanName"] == DBNull.Value ? string.Empty : dt.Rows[i]["ChanName"].ToString());
            }
            return channelName;
        }

        /// <summary>
        /// get topic name by mediacname
        /// </summary>
        /// <param name="strCnName">media cname</param>
        /// <param name="strChannelName">media channel name</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<string> GetTopicNameByCnName(string strCnName, string strChannelName)
        {
            List<string> topicName = new List<string>();

            string term = string.Empty;
            Hashtable ht = new Hashtable();

            term += " and a.MediaCName = @MediaCName and a.ChannelName = @ChannelName";
            if (!ht.ContainsKey("@MediaCName"))
            {
                ht.Add("@MediaCName", strCnName);
            }
            if (!ht.ContainsKey("@ChannelName"))
            {
                ht.Add("@ChannelName", strChannelName);
            }

            DataTable dt = GetAutoList(term, ht);
            if (dt == null || dt.Rows.Count == 0)
                return topicName;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                topicName.Add(dt.Rows[i]["ToName"] == DBNull.Value ? string.Empty : dt.Rows[i]["ToName"].ToString());
            }
            return topicName;
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
        public static int Add(MediaitemsInfo obj, List<BranchInfo> branchs, MediaAttach attach, out string errmsg, int userid)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                string term = "";
                List<SqlParameter> parms = new List<SqlParameter>();

                if (obj.Mediaitemtype == Global.MediaItemType_Tv || obj.Mediaitemtype == Global.MediaItemType_Dab)
                {
                    //中文名称、频道名称、栏目名称联合查重
                    term += "MediaCName=@MediaCName and ChannelName=@ChannelName and TopicName=@TopicName AND del!=@del ";
                    parms.Add(new SqlParameter("@MediaCName", obj.Mediacname));
                    parms.Add(new SqlParameter("@ChannelName", obj.Channelname));
                    parms.Add(new SqlParameter("@TopicName", obj.Topicname));
                    errmsg = obj.Mediacname + " " + obj.Channelname + " " + obj.Topicname + "已经存在!";
                }
                else if (obj.Mediaitemtype == Global.MediaItemType_Web)
                {
                    term += "MediaCName=@MediaCName and ChannelName=@ChannelName AND del!=@del ";
                    parms.Add(new SqlParameter("@MediaCName", obj.Mediacname));
                    parms.Add(new SqlParameter("@ChannelName", obj.Channelname));
                    errmsg = obj.Mediacname + " " + obj.Channelname + "已经存在!";
                }
                else
                {
                    term += "MediaCName=@MediaCName AND del!=@del ";
                    parms.Add(new SqlParameter("@MediaCName", obj.Mediacname));
                    errmsg = obj.Mediacname + "已经存在!";
                }
                parms.Add(new SqlParameter("@del", (int)Global.FiledStatus.Del));

                DataTable dt = ESP.Media.DataAccess.MediaitemsDataProvider.QueryInfo(trans,term,parms.ToArray());
                if (dt.Rows.Count > 0)
                {
                    //errmsg = "媒体名称已存在!";
                    trans.Rollback();
                    ESP.Logging.Logger.Add("Save a new media is failed.", "Media system", ESP.Logging.LogLevel.Information,null,(object)errmsg); 
                    conn.Close();
                    return -1;
                }
                try
                {
                    errmsg = string.Empty;
                    obj.Currentversion = CommonManager.GetLastVersion("MediaItem", obj.Mediaitemid, trans) + 1;
                    SaveMediaAttach(trans, ref obj, attach);
                    int ret = ESP.Media.DataAccess.MediaitemsDataProvider.insertinfo(obj,trans);
                    obj.Mediaitemid = ret;
                    BranchManager.DeleteBranchByMediaItemID(obj.Mediaitemid, trans);
                    if (branchs != null && branchs.Count > 0)
                    {
                        for (int i = 0; i < branchs.Count; i++)
                        {
                            branchs[i].Mediaitemid = obj.Mediaitemid;
                            BranchManager.add(branchs[i], userid);
                        }
                    }
                    obj.Lastmodifiedbyuserid = userid;
                    obj.Lastmodifiedip = obj.Lastmodifiedip;
                    obj.Lastmodifieddate = DateTime.Now.ToString();
                    SaveHist(trans, obj,null, userid);
                    //OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.Media, userid, trans);//媒体信息
                    trans.Commit();
                    conn.Close();
                    ESP.Logging.Logger.Add("Save a new media is sucess.", "Media system", ESP.Logging.LogLevel.Information); 
                    return ret;
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    ESP.Logging.Logger.Add("Save a new media is error.", "Media system", ESP.Logging.LogLevel.Information,exception); 
                    errmsg = exception.Message;
                    return -2;
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
        public static int Update(MediaitemsInfo obj,List<BranchInfo> branchs,MediaAttach attach,int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string term = "";
                    List<SqlParameter> parms = new List<SqlParameter>();

                    if (obj.Mediaitemtype == Global.MediaItemType_Tv || obj.Mediaitemtype == Global.MediaItemType_Dab)
                    {
                        //中文名称、频道名称、栏目名称联合查重
                        term += "MediaCName=@MediaCName and ChannelName=@ChannelName and TopicName=@TopicName AND MediaitemID!=@MediaitemID AND del!=@del ";
                        parms.Add(new SqlParameter("@MediaCName", obj.Mediacname));
                        parms.Add(new SqlParameter("@ChannelName", obj.Channelname));
                        parms.Add(new SqlParameter("@TopicName", obj.Topicname));
                        errmsg = obj.Mediacname + " " + obj.Channelname + " " + obj.Topicname + "已经存在!";
                    }
                    else if (obj.Mediaitemtype == Global.MediaItemType_Web)
                    {
                        term += "MediaCName=@MediaCName and ChannelName=@ChannelName AND MediaitemID!=@MediaitemID AND del!=@del ";
                        parms.Add(new SqlParameter("@MediaCName", obj.Mediacname));
                        parms.Add(new SqlParameter("@ChannelName", obj.Channelname));
                        errmsg = obj.Mediacname + " " + obj.Channelname + "已经存在!";
                    }
                    else
                    {
                        term += "MediaCName=@MediaCName AND MediaitemID!=@MediaitemID AND del!=@del ";
                        parms.Add(new SqlParameter("@MediaCName", obj.Mediacname));
                        errmsg = obj.Mediacname + "已经存在!";
                    }
                    parms.Add(new SqlParameter("@del", (int)Global.FiledStatus.Del));
                    parms.Add(new SqlParameter("@MediaitemID", obj.Mediaitemid));

                    DataTable dt = ESP.Media.DataAccess.MediaitemsDataProvider.QueryInfo(trans, term, parms.ToArray());
                    if (dt.Rows.Count > 0)
                    {
                        //errmsg = "媒体名称已存在!";
                        trans.Rollback();
                        conn.Close();
                        ESP.Logging.Logger.Add("Update a media is failed.", "Media system", ESP.Logging.LogLevel.Information,null,(object)errmsg); 
                        return -1;
                    }

                    errmsg = string.Empty;
                    obj.Currentversion = CommonManager.GetLastVersion("MediaItem", obj.Mediaitemid, trans) + 1;
                    SaveMediaAttach(trans, ref obj, attach);
                    //Media_mediaindustryrelation.modifyforAddMediaItem(obj.Mediaitemid, industries, trans);
                    if (ESP.Media.DataAccess.MediaitemsDataProvider.updateInfo(trans,null, obj, string.Empty, null))
                    {
                        obj.Lastmodifiedbyuserid = userid;
                        obj.Lastmodifiedip = obj.Lastmodifiedip;
                        obj.Lastmodifieddate = DateTime.Now.ToString();
                        BranchManager.DeleteBranchByMediaItemID(obj.Mediaitemid, trans);
                        if (branchs != null && branchs.Count > 0)
                        {  
                            for (int i = 0; i < branchs.Count; i++)
                            {
                                BranchManager.add(branchs[i], userid);
                            }
                        }
                        SaveHist(trans, obj,branchs, userid);
                        //OperatelogManager.add((int)Global.SysOperateType.Edit, (int)Global.Tables.Media, userid, trans);//媒体信息
                        trans.Commit();
                        conn.Close();
                        ESP.Logging.Logger.Add("Update a media is sucess.", "Media system", ESP.Logging.LogLevel.Information); 
                        return 1;
                    }
                    else
                    {
                        errmsg = "修改失败!";
                        trans.Rollback();
                        conn.Close();
                        ESP.Logging.Logger.Add("Update a media is failed.", "Media system", ESP.Logging.LogLevel.Information,null, (object)(errmsg + " ID=" + obj.Mediaitemid.ToString())); 
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    ESP.Logging.Logger.Add("Update a media is error.", "Media system", ESP.Logging.LogLevel.Information, exception, (object)(errmsg + " ID=" + obj.Mediaitemid.ToString())); 
                    return -2;
                }
            }
        }

        /// <summary>
        /// Deletes the specified obj. logic delete
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Delete(MediaitemsInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";

            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    obj.Del = (int)Global.FiledStatus.Del;
                  
                   if (ESP.Media.DataAccess.MediaitemsDataProvider.updateInfo(trans,null,obj, null, null))
                    {
                        MediaindustryrelationManager.modifyforDelMediaItem(obj.Mediaitemid, trans);
                       trans.Commit();
                       conn.Close();
                       ESP.Logging.Logger.Add("Delete a media is success.", "Media system", ESP.Logging.LogLevel.Information); 
                        return 1;
                    }
                    else
                    {
                        errmsg = "删除失败!";
                        trans.Rollback();
                        conn.Close();
                        ESP.Logging.Logger.Add("Delete a media is failed.", "Media system", ESP.Logging.LogLevel.Information, null, (object)(errmsg + " ID=" + obj.Mediaitemid.ToString())); 
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    errmsg = exception.Message;
                    ESP.Logging.Logger.Add("Delete a media is error.", "Media system", ESP.Logging.LogLevel.Information, exception, (object)(errmsg + " ID=" + obj.Mediaitemid.ToString())); 
                    return -2;
                }
            }
        }

        /// <summary>
        /// 对未审核通过的媒体进行物理删除
        /// </summary>
        /// <param name="id">要删除的媒体ID</param>
        /// <param name="userid">操作人</param>
        /// <param name="errmsg">返回信息</param>
        /// <returns></returns>
        public static bool PhysicalDel(int id, int userid,out string errmsg)
        { 
            errmsg = "删除失败";
            if (id <= 0) return false;
            MediaitemsInfo item = GetModel(id);
            if (item.Status == (int)Global.MediaAuditStatus.FirstLevelAudit)
            {
                errmsg = "已审核通过的媒体，不能物理删除！";
                ESP.Logging.Logger.Add("Delete a media is failed.", "Media system", ESP.Logging.LogLevel.Information, null, (object)(errmsg + " ID=" + id.ToString())); 
                return false;
            }
            return ESP.Media.DataAccess.MediaitemsDataProvider.DeleteInfo(id);
        }

        /// <summary>
        /// Gets the hist list by mediaitem ID.
        /// </summary>
        /// <param name="mediaitemID">The mediaitem ID.</param>
        /// <returns></returns>
        public static DataTable GetHistListByMediaitemID(int mediaitemID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@id",mediaitemID);
            ht.Add("@del", (int)Global.FiledStatus.Del);

            return getHistList(" and a.del!=@del and a.MediaItemID=@id ", ht);
        }

        /// <summary>
        /// Gets the hist model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static MediaitemshistInfo GetHistModel(int id)
        {
            if (id <= 0) return new MediaitemshistInfo();
            return ESP.Media.DataAccess.MediaitemshistDataProvider.Load(id);
        }

        /// <summary>
        /// 保存附件信息
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="attach">The attach.</param>
        private static void SaveMediaAttach(SqlTransaction ts, ref MediaitemsInfo obj, MediaAttach attach)
        {
            if (!string.IsNullOrEmpty(attach.LogoFileName) && attach.LogoFileName.Length > 0)
            {
                obj.Medialogo = attach.LogoFileName.ToString();
            }
            if (attach.PriceFileData != null)
            {
                MediaattachmentsInfo mediaAttach = new MediaattachmentsInfo();
                mediaAttach.Mediaitemid = obj.Mediaitemid;
                mediaAttach.Type = PriceFileType;
                mediaAttach.Version = GetLastMediaAttachVersion(ts, PriceFileType, obj.Mediaitemid);

                mediaAttach.Attachmentpath = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.MediaBriefPath,attach.PriceFileName, attach.PriceFileData, true);
                mediaAttach.Createdbyuserid = obj.Lastmodifiedbyuserid;
                mediaAttach.Createddate = obj.Lastmodifieddate;
                mediaAttach.Createdip = obj.Createdip;
                obj.Adsprice = ESP.Media.DataAccess.MediaattachmentsDataProvider.insertinfo(mediaAttach,ts);
            }
            if (attach.BriefFileData != null)
            {
                MediaattachmentsInfo mediaAttach = new MediaattachmentsInfo();
                mediaAttach.Mediaitemid = obj.Mediaitemid;
                mediaAttach.Type = BriefingFileType;
                mediaAttach.Version = GetLastMediaAttachVersion(ts, BriefingFileType, obj.Mediaitemid);

                mediaAttach.Attachmentpath = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.MediaBriefPath,attach.BriefFileName, attach.BriefFileData, true);
                mediaAttach.Createdbyuserid = obj.Lastmodifiedbyuserid;
                mediaAttach.Createddate = obj.Lastmodifieddate;
                mediaAttach.Createdip = obj.Createdip;
                obj.Briefing = ESP.Media.DataAccess.MediaattachmentsDataProvider.insertinfo(mediaAttach,ts);
            }
        }

        /// <summary>
        /// Gets the last media attach version.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <param name="type">The type.</param>
        /// <param name="mid">The mid.</param>
        /// <returns></returns>
        private static int GetLastMediaAttachVersion( SqlTransaction ts,int type,int mid)
        {
            string sql = string.Format("select Max(Version) from media_MediaAttachments where Type={0} AND MediaItemID={1}", type, mid);
            DataTable dt;
            if (ts == null)
                dt = clsSelect.QueryBySql(sql);
            else
                dt = clsSelect.QueryBySql(sql, ts);
            if (dt.Rows[0][0] != DBNull.Value)
                return (int)(dt.Rows[0][0]) + 1;
            else
                return 0;
        }

        /// <summary>
        /// Saves the hist.
        /// </summary>
        /// <param name="trans">The trans.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="branchs">The branchs.</param>
        /// <param name="userid">The userid.</param>
        private static void SaveHist(SqlTransaction trans, MediaitemsInfo obj,List<BranchInfo> branchs,int userid)
        {
            MediaitemshistInfo hist = new  MediaitemshistInfo();
            hist.Lastmodifiedbyuserid = userid;
            hist.Lastmodifiedip = obj.Lastmodifiedip;
            hist.Lastmodifieddate = DateTime.Now.ToString();

            hist.Mediaitemid = obj.Mediaitemid;
            hist.Mediacname = obj.Mediacname;
            hist.Mediaename = obj.Mediaename;
            hist.Cshortname = obj.Cshortname;
            hist.Eshortname = obj.Eshortname;
            hist.Mediaitemtype = obj.Mediaitemtype;
            hist.Mediumsort = obj.Mediumsort;
            hist.Readersort = obj.Readersort;
            hist.Governingbody = obj.Governingbody;
            hist.Frontfor = obj.Frontfor;
            hist.Proprieter = obj.Proprieter;
            hist.Subproprieter = obj.Subproprieter;
            hist.Chiefeditor = obj.Chiefeditor;
            hist.Admineditor = obj.Admineditor;
            hist.Subeditor = obj.Subeditor;
            hist.Manager = obj.Manager;
            hist.Zhuren = obj.Zhuren;
            hist.Producer = obj.Producer;
            hist.Startpublication = obj.Startpublication;
            hist.Publishdate = obj.Publishdate;
            hist.Telephoneexchange = obj.Telephoneexchange;
            hist.Fax = obj.Fax;
            hist.Addressone = obj.Addressone;
            hist.Addresstwo = obj.Addresstwo;
            hist.Webaddress = obj.Webaddress;
            hist.Issn = obj.Issn;
            hist.Cooperate = obj.Cooperate;
            //hist.IndustryProperty = obj.IndustryProperty;
            hist.Circulation = obj.Circulation;
            hist.Publishchannels = obj.Publishchannels;
            hist.Phoneone = obj.Phoneone;
            hist.Phonetwo = obj.Phonetwo;
            hist.Endpublication = obj.Endpublication;
            hist.Adsphone = obj.Adsphone;
            hist.Adsprice = obj.Adsprice;
            hist.Medialogo = obj.Medialogo;
            hist.Briefing = obj.Briefing;
            hist.Mediaintro = obj.Mediaintro;
            hist.Engintro = obj.Engintro;
            hist.Remarks = obj.Remarks;
            hist.Channelname = obj.Channelname;
            hist.Dabfm = obj.Dabfm;
            hist.Topicname = obj.Topicname;
            hist.Topicproperty = obj.Topicproperty;
            hist.Overriderange = obj.Overriderange;
            hist.Rating = obj.Rating;
            hist.Topictime = obj.Topictime;
            hist.Channelwebaddress = obj.Channelwebaddress;
            hist.Createdbyuserid = obj.Lastmodifiedbyuserid;
            hist.Createddate = obj.Lastmodifieddate;
            hist.Createdip = obj.Lastmodifiedip;
            hist.Status = obj.Status;
            hist.Version = CommonManager.GetLastVersion("MediaItem", obj.Mediaitemid, trans);

            hist.Postcode = obj.Postcode;
            hist.Regionattribute = obj.Regionattribute;
            hist.Override_countryid = obj.Override_countryid;
            hist.Override_provinceid = obj.Override_provinceid;
            hist.Override_cityid = obj.Override_cityid;
            hist.Override_describe = obj.Override_describe;
            hist.Industryid = obj.Industryid;
            hist.Issueregion = obj.Issueregion;
            hist.Countryid = obj.Countryid;
            hist.Provinceid = obj.Provinceid;
            hist.Cityid = obj.Cityid;
            hist.Addr1_cityid = obj.Addr1_cityid;
            hist.Addr1_countryid = obj.Addr1_countryid;
            hist.Addr1_provinceid = obj.Addr1_provinceid;

            hist.Publishperiods = obj.Publishperiods;
            if (branchs != null && branchs.Count > 0)
            {
                for (int i = 0; i < branchs.Count; i++)
                {
                    hist.Branchs += branchs[i].Cityname + ",";
                }
                hist.Branchs.Trim(',');
            }

            try
            {
                int hisid = ESP.Media.DataAccess.MediaitemshistDataProvider.insertinfo(hist, trans);
                if(hisid > 0)
                    ESP.Logging.Logger.Add("Save a mediahistory is success.", "Media system", ESP.Logging.LogLevel.Information);
                else
                    ESP.Logging.Logger.Add("Save a mediahistory is failed.", "Media system", ESP.Logging.LogLevel.Information); 
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("Save a mediahistory is error.", "Media system", ESP.Logging.LogLevel.Information, null);  
            }
        }

        /// <summary>
        /// Submits the media.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int SubmitMedia(MediaitemsInfo obj, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    obj.Status = (int)Global.MediaAuditStatus.Submit;
                    if (ESP.Media.DataAccess.MediaitemsDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        trans.Commit();
                        conn.Close();
                        ESP.Logging.Logger.Add("Submit a media is success.", "Media system", ESP.Logging.LogLevel.Information);
                        return 1;
                    }
                    else
                    {
                        errmsg = "修改失败!";
                        trans.Rollback();
                        conn.Close();
                        ESP.Logging.Logger.Add("Submit a media is failed.", "Media system", ESP.Logging.LogLevel.Information, null, (object)(errmsg + " MediaID=" + obj.Mediaitemid.ToString()));
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    ESP.Logging.Logger.Add("Submit a media is error.", "Media system", ESP.Logging.LogLevel.Information, exception, (object)(errmsg + " MediaID=" + obj.Mediaitemid.ToString()));
                    return -2;
                }
            }
        }

        /// <summary>
        /// 审核媒体
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int AuditMedia(MediaitemsInfo obj, int userid, out string errmsg)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    errmsg = string.Empty;
                    obj.Status = (int)Global.MediaAuditStatus.FirstLevelAudit;
                    if (ESP.Media.DataAccess.MediaitemsDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                    {
                        trans.Commit();
                        conn.Close();
                        ESP.Logging.Logger.Add("Audit a media is success.", "Media system", ESP.Logging.LogLevel.Information);
                        return 1;
                    }
                    else
                    {
                        errmsg = "修改失败!";
                        trans.Rollback();
                        conn.Close();
                        ESP.Logging.Logger.Add("Submit a media is failed.", "Media system", ESP.Logging.LogLevel.Information, null, (object)(errmsg + " MediaID=" + obj.Mediaitemid.ToString()));
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    conn.Close();
                    errmsg = exception.Message;
                    ESP.Logging.Logger.Add("Submit a media is error.", "Media system", ESP.Logging.LogLevel.Information, exception, (object)(errmsg + " MediaID=" + obj.Mediaitemid.ToString()));
                    return -2;
                }
            }
        }

        /// <summary>
        /// Gets all list.
        /// </summary>
        /// <returns></returns>
        private static DataTable GetAllList()
        {
            return GetList(null, null);
        }

        /// <summary>
        /// 已经保存,未提交列表
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetSaveList(string term, Hashtable ht)
        {
            if (term == null) 
                term = string.Empty;
            if (ht == null) 
                ht = new Hashtable();
            term += " and a.status = @status";
            if (!ht.ContainsKey("@status"))
            {
                ht.Add("@status", (int)Global.MediaAuditStatus.Save);
            }
            return GetList(term, ht);
        }

        /// <summary>
        /// 等待审批列表(submit list)
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetUnAuditList(string term, Hashtable ht)
        {
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            term +=  " and a.status = @status";
            if (!ht.ContainsKey("@status"))
            {
                ht.Add("@status", (int)Global.MediaAuditStatus.Submit);
            }
            return GetList(term, ht);
        }

        /// <summary>
        /// 已审核通过媒体列表.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        public static DataTable GetAuditList(string term, Hashtable ht)
        {
            if (term == null) term = string.Empty;
            if (ht == null) ht = new Hashtable();
            term += " and a.status = @status";
            if (!ht.ContainsKey("@status"))
            {
                ht.Add("@status", (int)Global.MediaAuditStatus.FirstLevelAudit);
            }
            return GetList(term, ht);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        private static DataTable GetList(string term, Hashtable ht)
        {
            string sql = @"select a.mediaitemid as mediaitemid,
                            a.mediacname as mediacname,a.mediaename as mediaename,
                            a.cshortname as cshortname,a.eshortname as eshortname,
                            a.mediaitemtype as mediaitemtype,a.currentversion as currentversion,
                            a.status as status,a.createdbyuserid as createdbyuserid,
                            a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,
                            a.lastmodifieddate as lastmodifieddate,a.mediumsort as mediumsort,a.RegionAttribute as RegionAttribute,a.industryid as industryid,
                            a.Countryid as Countryid,TelephoneExchange,
                            medianame = isnull(a.mediacname,'') + ' '+ isnull(a.ChannelName,'')+' '+isnull(a.TopicName,''),
                            a.issueregion as issueregion,indust.industryname as industryname,
                            headquarter = isnull(country.countryname,'') + ' '+ isnull(province.province_name,'') + ' '+isnull(city.city_name,''),a.publishPeriods as publishPeriods
                            
                            {0} from Media_mediaitems as a {1} where 1=1 {2}";
            string newcol = ",mediatype.name as mediatypename ";
            string jointable = @"left join Media_mediaindustryrelation as industryrelation on a.mediaitemid = industryrelation.mediaitemid
                                 left join media_Industries as industry on industryrelation.industryid = industry.industryid
                                 left join Media_ProjectMediaRelation as project on a.MediaitemID = project.MediaitemID
                                left join media_industries as indust on a.IndustryID = indust.IndustryID 
                                left join media_country as country on a.countryid = country.countryid
                                left join Media_province as province on province.province_id = a.provinceid
                                left join media_city as city on city.city_id = a.cityid
                                inner join media_mediatype as mediatype on a.mediaitemtype = mediatype.id
                                    

                                 ";
            if (term == null)
            {
                term = string.Empty;
            }
            term += @" and a.del!=@del  group by a.mediaitemid,mediacname,mediaename,cshortname,
                            eshortname,mediaitemtype,currentversion,
                            status,createdbyuserid,createddate,lastmodifiedbyuserid,
                            lastmodifieddate,mediumsort,mediatype.name,a.RegionAttribute,a.industryid,a.Countryid,TelephoneExchange,
                            a.channelname,a.topicname,a.issueregion,
                            indust.industryname,country.countryname,province.province_name,city.city_name,a.publishPeriods
                            order by a.Mediaitemid desc
                            ";
            if (ht == null)
            {
                ht = new Hashtable();
            }
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }


            sql = string.Format(sql, newcol, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// Gets the list for auto complete.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        private static DataTable GetAutoList(string term, Hashtable ht)
        {
            string sql = @"select a.mediaitemid as mediaitemid,
                            a.mediacname as mediacname,a.mediaename as mediaename,
                            a.ChannelName as channame,a.TopicName as toname,
                            a.cshortname as cshortname,a.eshortname as eshortname,
                            a.mediaitemtype as mediaitemtype,a.currentversion as currentversion,
                            a.status as status,a.createdbyuserid as createdbyuserid,
                            a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,
                            a.lastmodifieddate as lastmodifieddate,a.mediumsort as mediumsort,a.RegionAttribute as RegionAttribute,a.industryid as industryid,
                            a.Countryid as Countryid,TelephoneExchange,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName,
                            a.issueregion as issueregion,indust.industryname as industryname,
                            headquarter = country.countryname + ' '+ province.province_name + ' '+city.city_name,a.publishPeriods as publishPeriods
                            
                            {0} from Media_mediaitems as a {1} where 1=1 {2}";
            string newcol = ",mediatype.name as mediatypename ";
            string jointable = @"left join Media_mediaindustryrelation as industryrelation on a.mediaitemid = industryrelation.mediaitemid
                                 left join media_Industries as industry on industryrelation.industryid = industry.industryid
                                 left join Media_ProjectMediaRelation as project on a.MediaitemID = project.MediaitemID
                                left join media_industries as indust on a.IndustryID = indust.IndustryID 
                                left join media_country as country on a.countryid = country.countryid
                                left join Media_province as province on province.province_id = a.provinceid
                                left join media_city as city on city.city_id = a.cityid
                                inner join media_mediatype as mediatype on a.mediaitemtype = mediatype.id
                                    

                                 ";
            if (term == null)
            {
                term = string.Empty;
            }
            term += @" and a.del!=@del  group by a.mediaitemid,mediacname,mediaename,cshortname,
                            eshortname,mediaitemtype,currentversion,
                            status,createdbyuserid,createddate,lastmodifiedbyuserid,
                            lastmodifieddate,mediumsort,mediatype.name,a.RegionAttribute,a.industryid,a.Countryid,TelephoneExchange,
                            a.channelname,a.topicname,a.issueregion,
                            indust.industryname,country.countryname,province.province_name,city.city_name,a.publishPeriods
                            order by a.Mediaitemid desc
                            ";
            if (ht == null)
            {
                ht = new Hashtable();
            }
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }


            sql = string.Format(sql, newcol, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }

        /// <summary>
        /// Gets the hist list.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        private static DataTable getHistList(string term, Hashtable ht)
        {
            string sql = @"select a.id as id,a.version as version,a.mediaitemid as mediaitemid,
                            a.mediacname as mediacname,a.mediaename as mediaename,
                            a.cshortname as cshortname,a.eshortname as eshortname,
                            a.mediaitemtype as mediaitemtype,a.currentversion as currentversion,
                            a.status as status,a.createdbyuserid as createdbyuserid,
                            a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,
                            a.lastmodifieddate as lastmodifieddate,a.mediumsort as mediumsort,a.RegionAttribute as RegionAttribute,a.industryid as industryid,
                            a.Countryid as Countryid,TelephoneExchange,
                            medianame = a.mediacname + ' '+a.ChannelName+' '+a.TopicName,
                            a.issueregion as issueregion,indust.industryname as industryname,
                            headquarter = country.countryname + ' '+ province.province_name + ' '+city.city_name,a.publishPeriods as publishPeriods
                            
                            {0} from Media_mediaitemshist as a {1} where 1=1 {2}";
            string newcol = ",mediatype.name as mediatypename ";
            string jointable = @"left join Media_mediaindustryrelation as industryrelation on a.mediaitemid = industryrelation.mediaitemid
                                 left join media_Industries as industry on industryrelation.industryid = industry.industryid
                                 left join Media_ProjectMediaRelation as project on a.MediaitemID = project.MediaitemID
                                left join media_industries as indust on a.IndustryID = indust.IndustryID 
                                left join media_country as country on a.countryid = country.countryid
                                left join Media_province as province on province.province_id = a.provinceid
                                left join media_city as city on city.city_id = a.cityid
                                inner join media_mediatype as mediatype on a.mediaitemtype = mediatype.id
                                    

                                 ";
            if (term == null)
            {
                term = string.Empty;
            }
            term += @" and a.del!=@del  group by 
                            a.id,a.version,
                            a.mediaitemid,mediacname,mediaename,cshortname,
                            eshortname,mediaitemtype,currentversion,
                            status,createdbyuserid,createddate,lastmodifiedbyuserid,
                            lastmodifieddate,mediumsort,mediatype.name,a.RegionAttribute,a.industryid,a.Countryid,TelephoneExchange,
                            a.channelname,a.topicname,a.issueregion,
                            indust.industryname,country.countryname,province.province_name,city.city_name,a.publishPeriods
                            order by a.Mediaitemid desc
                            ";
            if (ht == null)
            {
                ht = new Hashtable();
            }
            if (!ht.ContainsKey("@del"))
            {
                ht.Add("@del", (int)Global.FiledStatus.Del);
            }


            sql = string.Format(sql, newcol, jointable, term);
            SqlParameter[] param = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return clsSelect.QueryBySql(sql, param);
        }
    }
}
