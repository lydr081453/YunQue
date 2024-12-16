using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Access.Utilities;
using ESP.Media.Entity;

namespace ESP.Media.DataAccess
{
    public class MediaitemshistDataProvider
    {
        #region 构造函数
        public MediaitemshistDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(MediaitemshistInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_mediaitemshist (version,mediaitemid,mediacname,mediaename,cshortname,eshortname,mediaitemtype,currentversion,status,createdbyuserid,createdip,createddate,lastmodifiedbyuserid,lastmodifieddate,lastmodifiedip,mediumsort,readersort,governingbody,frontfor,proprieter,subproprieter,chiefeditor,admineditor,subeditor,manager,zhuren,producer,startpublication,publishdate,telephoneexchange,fax,addressone,addresstwo,webaddress,issn,cooperate,circulation,publishchannels,phoneone,phonetwo,endpublication,adsphone,adsprice,medialogo,briefing,mediaintro,engintro,remarks,channelname,dabfm,topicname,topicproperty,overriderange,rating,topictime,channelwebaddress,countryid,provinceid,cityid,del,mediatype,addr1_provinceid,addr1_cityid,addr1_countryid,addr2_provinceid,addr2_cityid,addr2_countryid,postcode,regionattribute,override_countryid,override_provinceid,override_cityid,override_describe,industryid,issueregion,branchs,publishperiods) values (@version,@mediaitemid,@mediacname,@mediaename,@cshortname,@eshortname,@mediaitemtype,@currentversion,@status,@createdbyuserid,@createdip,@createddate,@lastmodifiedbyuserid,@lastmodifieddate,@lastmodifiedip,@mediumsort,@readersort,@governingbody,@frontfor,@proprieter,@subproprieter,@chiefeditor,@admineditor,@subeditor,@manager,@zhuren,@producer,@startpublication,@publishdate,@telephoneexchange,@fax,@addressone,@addresstwo,@webaddress,@issn,@cooperate,@circulation,@publishchannels,@phoneone,@phonetwo,@endpublication,@adsphone,@adsprice,@medialogo,@briefing,@mediaintro,@engintro,@remarks,@channelname,@dabfm,@topicname,@topicproperty,@overriderange,@rating,@topictime,@channelwebaddress,@countryid,@provinceid,@cityid,@del,@mediatype,@addr1_provinceid,@addr1_cityid,@addr1_countryid,@addr2_provinceid,@addr2_cityid,@addr2_countryid,@postcode,@regionattribute,@override_countryid,@override_provinceid,@override_cityid,@override_describe,@industryid,@issueregion,@branchs,@publishperiods);select @@IDENTITY as rowNum;";
            SqlParameter param_Version = new SqlParameter("@Version", SqlDbType.Int, 4);
            param_Version.Value = obj.Version;
            ht.Add(param_Version);
            SqlParameter param_Mediaitemid = new SqlParameter("@Mediaitemid", SqlDbType.Int, 4);
            param_Mediaitemid.Value = obj.Mediaitemid;
            ht.Add(param_Mediaitemid);
            SqlParameter param_Mediacname = new SqlParameter("@Mediacname", SqlDbType.NVarChar, 512);
            param_Mediacname.Value = obj.Mediacname;
            ht.Add(param_Mediacname);
            SqlParameter param_Mediaename = new SqlParameter("@Mediaename", SqlDbType.NVarChar, 512);
            param_Mediaename.Value = obj.Mediaename;
            ht.Add(param_Mediaename);
            SqlParameter param_Cshortname = new SqlParameter("@Cshortname", SqlDbType.NVarChar, 512);
            param_Cshortname.Value = obj.Cshortname;
            ht.Add(param_Cshortname);
            SqlParameter param_Eshortname = new SqlParameter("@Eshortname", SqlDbType.NVarChar, 512);
            param_Eshortname.Value = obj.Eshortname;
            ht.Add(param_Eshortname);
            SqlParameter param_Mediaitemtype = new SqlParameter("@Mediaitemtype", SqlDbType.SmallInt, 2);
            param_Mediaitemtype.Value = obj.Mediaitemtype;
            ht.Add(param_Mediaitemtype);
            SqlParameter param_Currentversion = new SqlParameter("@Currentversion", SqlDbType.Int, 4);
            param_Currentversion.Value = obj.Currentversion;
            ht.Add(param_Currentversion);
            SqlParameter param_Status = new SqlParameter("@Status", SqlDbType.Int, 4);
            param_Status.Value = obj.Status;
            ht.Add(param_Status);
            SqlParameter param_Createdbyuserid = new SqlParameter("@Createdbyuserid", SqlDbType.Int, 4);
            param_Createdbyuserid.Value = obj.Createdbyuserid;
            ht.Add(param_Createdbyuserid);
            SqlParameter param_Createdip = new SqlParameter("@Createdip", SqlDbType.NVarChar, 100);
            param_Createdip.Value = obj.Createdip;
            ht.Add(param_Createdip);
            SqlParameter param_Createddate = new SqlParameter("@Createddate", SqlDbType.DateTime, 8);
            param_Createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
            ht.Add(param_Createddate);
            SqlParameter param_Lastmodifiedbyuserid = new SqlParameter("@Lastmodifiedbyuserid", SqlDbType.Int, 4);
            param_Lastmodifiedbyuserid.Value = obj.Lastmodifiedbyuserid;
            ht.Add(param_Lastmodifiedbyuserid);
            SqlParameter param_Lastmodifieddate = new SqlParameter("@Lastmodifieddate", SqlDbType.DateTime, 8);
            param_Lastmodifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastmodifieddate);
            ht.Add(param_Lastmodifieddate);
            SqlParameter param_Lastmodifiedip = new SqlParameter("@Lastmodifiedip", SqlDbType.NVarChar, 100);
            param_Lastmodifiedip.Value = obj.Lastmodifiedip;
            ht.Add(param_Lastmodifiedip);
            SqlParameter param_Mediumsort = new SqlParameter("@Mediumsort", SqlDbType.NVarChar, 100);
            param_Mediumsort.Value = obj.Mediumsort;
            ht.Add(param_Mediumsort);
            SqlParameter param_Readersort = new SqlParameter("@Readersort", SqlDbType.NVarChar, 4000);
            param_Readersort.Value = obj.Readersort;
            ht.Add(param_Readersort);
            SqlParameter param_Governingbody = new SqlParameter("@Governingbody", SqlDbType.NVarChar, 100);
            param_Governingbody.Value = obj.Governingbody;
            ht.Add(param_Governingbody);
            SqlParameter param_Frontfor = new SqlParameter("@Frontfor", SqlDbType.NVarChar, 2000);
            param_Frontfor.Value = obj.Frontfor;
            ht.Add(param_Frontfor);
            SqlParameter param_Proprieter = new SqlParameter("@Proprieter", SqlDbType.NVarChar, 100);
            param_Proprieter.Value = obj.Proprieter;
            ht.Add(param_Proprieter);
            SqlParameter param_Subproprieter = new SqlParameter("@Subproprieter", SqlDbType.NVarChar, 100);
            param_Subproprieter.Value = obj.Subproprieter;
            ht.Add(param_Subproprieter);
            SqlParameter param_Chiefeditor = new SqlParameter("@Chiefeditor", SqlDbType.NVarChar, 100);
            param_Chiefeditor.Value = obj.Chiefeditor;
            ht.Add(param_Chiefeditor);
            SqlParameter param_Admineditor = new SqlParameter("@Admineditor", SqlDbType.NVarChar, 100);
            param_Admineditor.Value = obj.Admineditor;
            ht.Add(param_Admineditor);
            SqlParameter param_Subeditor = new SqlParameter("@Subeditor", SqlDbType.NVarChar, 100);
            param_Subeditor.Value = obj.Subeditor;
            ht.Add(param_Subeditor);
            SqlParameter param_Manager = new SqlParameter("@Manager", SqlDbType.NVarChar, 100);
            param_Manager.Value = obj.Manager;
            ht.Add(param_Manager);
            SqlParameter param_Zhuren = new SqlParameter("@Zhuren", SqlDbType.NVarChar, 100);
            param_Zhuren.Value = obj.Zhuren;
            ht.Add(param_Zhuren);
            SqlParameter param_Producer = new SqlParameter("@Producer", SqlDbType.NVarChar, 100);
            param_Producer.Value = obj.Producer;
            ht.Add(param_Producer);
            SqlParameter param_Startpublication = new SqlParameter("@Startpublication", SqlDbType.DateTime, 8);
            param_Startpublication.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Startpublication);
            ht.Add(param_Startpublication);
            SqlParameter param_Publishdate = new SqlParameter("@Publishdate", SqlDbType.NVarChar, 200);
            param_Publishdate.Value = obj.Publishdate;
            ht.Add(param_Publishdate);
            SqlParameter param_Telephoneexchange = new SqlParameter("@Telephoneexchange", SqlDbType.NVarChar, 100);
            param_Telephoneexchange.Value = obj.Telephoneexchange;
            ht.Add(param_Telephoneexchange);
            SqlParameter param_Fax = new SqlParameter("@Fax", SqlDbType.NVarChar, 100);
            param_Fax.Value = obj.Fax;
            ht.Add(param_Fax);
            SqlParameter param_Addressone = new SqlParameter("@Addressone", SqlDbType.NVarChar, 1000);
            param_Addressone.Value = obj.Addressone;
            ht.Add(param_Addressone);
            SqlParameter param_Addresstwo = new SqlParameter("@Addresstwo", SqlDbType.NVarChar, 1000);
            param_Addresstwo.Value = obj.Addresstwo;
            ht.Add(param_Addresstwo);
            SqlParameter param_Webaddress = new SqlParameter("@Webaddress", SqlDbType.NVarChar, 512);
            param_Webaddress.Value = obj.Webaddress;
            ht.Add(param_Webaddress);
            SqlParameter param_Issn = new SqlParameter("@Issn", SqlDbType.NVarChar, 100);
            param_Issn.Value = obj.Issn;
            ht.Add(param_Issn);
            SqlParameter param_Cooperate = new SqlParameter("@Cooperate", SqlDbType.NVarChar, 4000);
            param_Cooperate.Value = obj.Cooperate;
            ht.Add(param_Cooperate);
            SqlParameter param_Circulation = new SqlParameter("@Circulation", SqlDbType.Int, 4);
            param_Circulation.Value = obj.Circulation;
            ht.Add(param_Circulation);
            SqlParameter param_Publishchannels = new SqlParameter("@Publishchannels", SqlDbType.NVarChar, 512);
            param_Publishchannels.Value = obj.Publishchannels;
            ht.Add(param_Publishchannels);
            SqlParameter param_Phoneone = new SqlParameter("@Phoneone", SqlDbType.NVarChar, 100);
            param_Phoneone.Value = obj.Phoneone;
            ht.Add(param_Phoneone);
            SqlParameter param_Phonetwo = new SqlParameter("@Phonetwo", SqlDbType.NVarChar, 100);
            param_Phonetwo.Value = obj.Phonetwo;
            ht.Add(param_Phonetwo);
            SqlParameter param_Endpublication = new SqlParameter("@Endpublication", SqlDbType.NVarChar, 200);
            param_Endpublication.Value = obj.Endpublication;
            ht.Add(param_Endpublication);
            SqlParameter param_Adsphone = new SqlParameter("@Adsphone", SqlDbType.NVarChar, 100);
            param_Adsphone.Value = obj.Adsphone;
            ht.Add(param_Adsphone);
            SqlParameter param_Adsprice = new SqlParameter("@Adsprice", SqlDbType.Int, 4);
            param_Adsprice.Value = obj.Adsprice;
            ht.Add(param_Adsprice);
            SqlParameter param_Medialogo = new SqlParameter("@Medialogo", SqlDbType.NVarChar, 1000);
            param_Medialogo.Value = obj.Medialogo;
            ht.Add(param_Medialogo);
            SqlParameter param_Briefing = new SqlParameter("@Briefing", SqlDbType.Int, 4);
            param_Briefing.Value = obj.Briefing;
            ht.Add(param_Briefing);
            SqlParameter param_Mediaintro = new SqlParameter("@Mediaintro", SqlDbType.NVarChar, 4000);
            param_Mediaintro.Value = obj.Mediaintro;
            ht.Add(param_Mediaintro);
            SqlParameter param_Engintro = new SqlParameter("@Engintro", SqlDbType.NVarChar, 4000);
            param_Engintro.Value = obj.Engintro;
            ht.Add(param_Engintro);
            SqlParameter param_Remarks = new SqlParameter("@Remarks", SqlDbType.NVarChar, 4000);
            param_Remarks.Value = obj.Remarks;
            ht.Add(param_Remarks);
            SqlParameter param_Channelname = new SqlParameter("@Channelname", SqlDbType.NVarChar, 100);
            param_Channelname.Value = obj.Channelname;
            ht.Add(param_Channelname);
            SqlParameter param_Dabfm = new SqlParameter("@Dabfm", SqlDbType.NVarChar, 100);
            param_Dabfm.Value = obj.Dabfm;
            ht.Add(param_Dabfm);
            SqlParameter param_Topicname = new SqlParameter("@Topicname", SqlDbType.NVarChar, 100);
            param_Topicname.Value = obj.Topicname;
            ht.Add(param_Topicname);
            SqlParameter param_Topicproperty = new SqlParameter("@Topicproperty", SqlDbType.Int, 4);
            param_Topicproperty.Value = obj.Topicproperty;
            ht.Add(param_Topicproperty);
            SqlParameter param_Overriderange = new SqlParameter("@Overriderange", SqlDbType.NVarChar, 100);
            param_Overriderange.Value = obj.Overriderange;
            ht.Add(param_Overriderange);
            SqlParameter param_Rating = new SqlParameter("@Rating", SqlDbType.NVarChar, 100);
            param_Rating.Value = obj.Rating;
            ht.Add(param_Rating);
            SqlParameter param_Topictime = new SqlParameter("@Topictime", SqlDbType.NVarChar, 100);
            param_Topictime.Value = obj.Topictime;
            ht.Add(param_Topictime);
            SqlParameter param_Channelwebaddress = new SqlParameter("@Channelwebaddress", SqlDbType.NVarChar, 512);
            param_Channelwebaddress.Value = obj.Channelwebaddress;
            ht.Add(param_Channelwebaddress);
            SqlParameter param_Countryid = new SqlParameter("@Countryid", SqlDbType.Int, 4);
            param_Countryid.Value = obj.Countryid;
            ht.Add(param_Countryid);
            SqlParameter param_Provinceid = new SqlParameter("@Provinceid", SqlDbType.Int, 4);
            param_Provinceid.Value = obj.Provinceid;
            ht.Add(param_Provinceid);
            SqlParameter param_Cityid = new SqlParameter("@Cityid", SqlDbType.Int, 4);
            param_Cityid.Value = obj.Cityid;
            ht.Add(param_Cityid);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Mediatype = new SqlParameter("@Mediatype", SqlDbType.NVarChar, 100);
            param_Mediatype.Value = obj.Mediatype;
            ht.Add(param_Mediatype);
            SqlParameter param_Addr1_provinceid = new SqlParameter("@Addr1_provinceid", SqlDbType.Int, 4);
            param_Addr1_provinceid.Value = obj.Addr1_provinceid;
            ht.Add(param_Addr1_provinceid);
            SqlParameter param_Addr1_cityid = new SqlParameter("@Addr1_cityid", SqlDbType.Int, 4);
            param_Addr1_cityid.Value = obj.Addr1_cityid;
            ht.Add(param_Addr1_cityid);
            SqlParameter param_Addr1_countryid = new SqlParameter("@Addr1_countryid", SqlDbType.Int, 4);
            param_Addr1_countryid.Value = obj.Addr1_countryid;
            ht.Add(param_Addr1_countryid);
            SqlParameter param_Addr2_provinceid = new SqlParameter("@Addr2_provinceid", SqlDbType.Int, 4);
            param_Addr2_provinceid.Value = obj.Addr2_provinceid;
            ht.Add(param_Addr2_provinceid);
            SqlParameter param_Addr2_cityid = new SqlParameter("@Addr2_cityid", SqlDbType.Int, 4);
            param_Addr2_cityid.Value = obj.Addr2_cityid;
            ht.Add(param_Addr2_cityid);
            SqlParameter param_Addr2_countryid = new SqlParameter("@Addr2_countryid", SqlDbType.Int, 4);
            param_Addr2_countryid.Value = obj.Addr2_countryid;
            ht.Add(param_Addr2_countryid);
            SqlParameter param_Postcode = new SqlParameter("@Postcode", SqlDbType.NVarChar, 100);
            param_Postcode.Value = obj.Postcode;
            ht.Add(param_Postcode);
            SqlParameter param_Regionattribute = new SqlParameter("@Regionattribute", SqlDbType.Int, 4);
            param_Regionattribute.Value = obj.Regionattribute;
            ht.Add(param_Regionattribute);
            SqlParameter param_Override_countryid = new SqlParameter("@Override_countryid", SqlDbType.Int, 4);
            param_Override_countryid.Value = obj.Override_countryid;
            ht.Add(param_Override_countryid);
            SqlParameter param_Override_provinceid = new SqlParameter("@Override_provinceid", SqlDbType.Int, 4);
            param_Override_provinceid.Value = obj.Override_provinceid;
            ht.Add(param_Override_provinceid);
            SqlParameter param_Override_cityid = new SqlParameter("@Override_cityid", SqlDbType.Int, 4);
            param_Override_cityid.Value = obj.Override_cityid;
            ht.Add(param_Override_cityid);
            SqlParameter param_Override_describe = new SqlParameter("@Override_describe", SqlDbType.NVarChar, 200);
            param_Override_describe.Value = obj.Override_describe;
            ht.Add(param_Override_describe);
            SqlParameter param_Industryid = new SqlParameter("@Industryid", SqlDbType.Int, 4);
            param_Industryid.Value = obj.Industryid;
            ht.Add(param_Industryid);
            SqlParameter param_Issueregion = new SqlParameter("@Issueregion", SqlDbType.NVarChar, 1000);
            param_Issueregion.Value = obj.Issueregion;
            ht.Add(param_Issueregion);
            SqlParameter param_Branchs = new SqlParameter("@Branchs", SqlDbType.NVarChar, 4000);
            param_Branchs.Value = obj.Branchs;
            ht.Add(param_Branchs);
            SqlParameter param_Publishperiods = new SqlParameter("@Publishperiods", SqlDbType.NVarChar, 100);
            param_Publishperiods.Value = obj.Publishperiods;
            ht.Add(param_Publishperiods);
            return sql;
        }

        //插入一条记录
        public static int insertinfo(MediaitemshistInfo obj, SqlTransaction trans)
        {
            if (obj == null)
            {
                return 0;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = strinsert(obj, ref ht);
            SqlParameter[] param = ht.ToArray();
            int rowNum = 0;
            try
            {
                rowNum = Convert.ToInt32(SqlHelper.ExecuteDataset(trans, CommandType.Text, sql, param).Tables[0].Rows[0]["rowNum"]);
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("Save a new media history is error.", "Media system", ESP.Logging.LogLevel.Information,ex);
            }
            return rowNum;
        }

        //插入一条记录
        public static int insertinfo(MediaitemshistInfo obj)
        {
            if (obj == null)
            {
                return 0;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = strinsert(obj, ref ht);
            SqlParameter[] param = ht.ToArray();
            int rowNum = 0;
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    rowNum = Convert.ToInt32(SqlHelper.ExecuteDataset(trans, CommandType.Text, sql, param).Tables[0].Rows[0]["rowNum"]);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add("Save a new media history is error.", "Media system", ESP.Logging.LogLevel.Information, ex);
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return rowNum;
        }
        #endregion
        #region 删除
        //删除操作
        public static bool DeleteInfo(int id, SqlTransaction trans)
        {
            int rows = 0;
            string sql = "delete media_mediaitemshist where id=@id";
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
            param.Value = id;
            try
            {
                rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                if (rows > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("Delete a media history is error.", "Media system", ESP.Logging.LogLevel.Information, ex); 
                    throw new Exception(ex.Message);
            }
            return false;
        }

        //删除操作
        public static bool DeleteInfo(int id)
        {
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    if (DeleteInfo(id, trans))
                    {
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }
        #endregion
        #region 更新
        //更新sql
        public static string getUpdateString(MediaitemshistInfo objTerm, MediaitemshistInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_mediaitemshist set version=@version,mediaitemid=@mediaitemid,mediacname=@mediacname,mediaename=@mediaename,cshortname=@cshortname,eshortname=@eshortname,mediaitemtype=@mediaitemtype,currentversion=@currentversion,status=@status,createdbyuserid=@createdbyuserid,createdip=@createdip,createddate=@createddate,lastmodifiedbyuserid=@lastmodifiedbyuserid,lastmodifieddate=@lastmodifieddate,lastmodifiedip=@lastmodifiedip,mediumsort=@mediumsort,readersort=@readersort,governingbody=@governingbody,frontfor=@frontfor,proprieter=@proprieter,subproprieter=@subproprieter,chiefeditor=@chiefeditor,admineditor=@admineditor,subeditor=@subeditor,manager=@manager,zhuren=@zhuren,producer=@producer,startpublication=@startpublication,publishdate=@publishdate,telephoneexchange=@telephoneexchange,fax=@fax,addressone=@addressone,addresstwo=@addresstwo,webaddress=@webaddress,issn=@issn,cooperate=@cooperate,circulation=@circulation,publishchannels=@publishchannels,phoneone=@phoneone,phonetwo=@phonetwo,endpublication=@endpublication,adsphone=@adsphone,adsprice=@adsprice,medialogo=@medialogo,briefing=@briefing,mediaintro=@mediaintro,engintro=@engintro,remarks=@remarks,channelname=@channelname,dabfm=@dabfm,topicname=@topicname,topicproperty=@topicproperty,overriderange=@overriderange,rating=@rating,topictime=@topictime,channelwebaddress=@channelwebaddress,countryid=@countryid,provinceid=@provinceid,cityid=@cityid,del=@del,mediatype=@mediatype,addr1_provinceid=@addr1_provinceid,addr1_cityid=@addr1_cityid,addr1_countryid=@addr1_countryid,addr2_provinceid=@addr2_provinceid,addr2_cityid=@addr2_cityid,addr2_countryid=@addr2_countryid,postcode=@postcode,regionattribute=@regionattribute,override_countryid=@override_countryid,override_provinceid=@override_provinceid,override_cityid=@override_cityid,override_describe=@override_describe,industryid=@industryid,issueregion=@issueregion,branchs=@branchs,publishperiods=@publishperiods where 1=1 ";
            SqlParameter param_version = new SqlParameter("@version", SqlDbType.Int, 4);
            param_version.Value = Objupdate.Version;
            ht.Add(param_version);
            SqlParameter param_mediaitemid = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
            param_mediaitemid.Value = Objupdate.Mediaitemid;
            ht.Add(param_mediaitemid);
            SqlParameter param_mediacname = new SqlParameter("@mediacname", SqlDbType.NVarChar, 512);
            param_mediacname.Value = Objupdate.Mediacname;
            ht.Add(param_mediacname);
            SqlParameter param_mediaename = new SqlParameter("@mediaename", SqlDbType.NVarChar, 512);
            param_mediaename.Value = Objupdate.Mediaename;
            ht.Add(param_mediaename);
            SqlParameter param_cshortname = new SqlParameter("@cshortname", SqlDbType.NVarChar, 512);
            param_cshortname.Value = Objupdate.Cshortname;
            ht.Add(param_cshortname);
            SqlParameter param_eshortname = new SqlParameter("@eshortname", SqlDbType.NVarChar, 512);
            param_eshortname.Value = Objupdate.Eshortname;
            ht.Add(param_eshortname);
            SqlParameter param_mediaitemtype = new SqlParameter("@mediaitemtype", SqlDbType.SmallInt, 2);
            param_mediaitemtype.Value = Objupdate.Mediaitemtype;
            ht.Add(param_mediaitemtype);
            SqlParameter param_currentversion = new SqlParameter("@currentversion", SqlDbType.Int, 4);
            param_currentversion.Value = Objupdate.Currentversion;
            ht.Add(param_currentversion);
            SqlParameter param_status = new SqlParameter("@status", SqlDbType.Int, 4);
            param_status.Value = Objupdate.Status;
            ht.Add(param_status);
            SqlParameter param_createdbyuserid = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
            param_createdbyuserid.Value = Objupdate.Createdbyuserid;
            ht.Add(param_createdbyuserid);
            SqlParameter param_createdip = new SqlParameter("@createdip", SqlDbType.NVarChar, 100);
            param_createdip.Value = Objupdate.Createdip;
            ht.Add(param_createdip);
            SqlParameter param_createddate = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
            param_createddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Createddate);
            ht.Add(param_createddate);
            SqlParameter param_lastmodifiedbyuserid = new SqlParameter("@lastmodifiedbyuserid", SqlDbType.Int, 4);
            param_lastmodifiedbyuserid.Value = Objupdate.Lastmodifiedbyuserid;
            ht.Add(param_lastmodifiedbyuserid);
            SqlParameter param_lastmodifieddate = new SqlParameter("@lastmodifieddate", SqlDbType.DateTime, 8);
            param_lastmodifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Lastmodifieddate);
            ht.Add(param_lastmodifieddate);
            SqlParameter param_lastmodifiedip = new SqlParameter("@lastmodifiedip", SqlDbType.NVarChar, 100);
            param_lastmodifiedip.Value = Objupdate.Lastmodifiedip;
            ht.Add(param_lastmodifiedip);
            SqlParameter param_mediumsort = new SqlParameter("@mediumsort", SqlDbType.NVarChar, 100);
            param_mediumsort.Value = Objupdate.Mediumsort;
            ht.Add(param_mediumsort);
            SqlParameter param_readersort = new SqlParameter("@readersort", SqlDbType.NVarChar, 4000);
            param_readersort.Value = Objupdate.Readersort;
            ht.Add(param_readersort);
            SqlParameter param_governingbody = new SqlParameter("@governingbody", SqlDbType.NVarChar, 100);
            param_governingbody.Value = Objupdate.Governingbody;
            ht.Add(param_governingbody);
            SqlParameter param_frontfor = new SqlParameter("@frontfor", SqlDbType.NVarChar, 2000);
            param_frontfor.Value = Objupdate.Frontfor;
            ht.Add(param_frontfor);
            SqlParameter param_proprieter = new SqlParameter("@proprieter", SqlDbType.NVarChar, 100);
            param_proprieter.Value = Objupdate.Proprieter;
            ht.Add(param_proprieter);
            SqlParameter param_subproprieter = new SqlParameter("@subproprieter", SqlDbType.NVarChar, 100);
            param_subproprieter.Value = Objupdate.Subproprieter;
            ht.Add(param_subproprieter);
            SqlParameter param_chiefeditor = new SqlParameter("@chiefeditor", SqlDbType.NVarChar, 100);
            param_chiefeditor.Value = Objupdate.Chiefeditor;
            ht.Add(param_chiefeditor);
            SqlParameter param_admineditor = new SqlParameter("@admineditor", SqlDbType.NVarChar, 100);
            param_admineditor.Value = Objupdate.Admineditor;
            ht.Add(param_admineditor);
            SqlParameter param_subeditor = new SqlParameter("@subeditor", SqlDbType.NVarChar, 100);
            param_subeditor.Value = Objupdate.Subeditor;
            ht.Add(param_subeditor);
            SqlParameter param_manager = new SqlParameter("@manager", SqlDbType.NVarChar, 100);
            param_manager.Value = Objupdate.Manager;
            ht.Add(param_manager);
            SqlParameter param_zhuren = new SqlParameter("@zhuren", SqlDbType.NVarChar, 100);
            param_zhuren.Value = Objupdate.Zhuren;
            ht.Add(param_zhuren);
            SqlParameter param_producer = new SqlParameter("@producer", SqlDbType.NVarChar, 100);
            param_producer.Value = Objupdate.Producer;
            ht.Add(param_producer);
            SqlParameter param_startpublication = new SqlParameter("@startpublication", SqlDbType.DateTime, 8);
            param_startpublication.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Startpublication);
            ht.Add(param_startpublication);
            SqlParameter param_publishdate = new SqlParameter("@publishdate", SqlDbType.NVarChar, 200);
            param_publishdate.Value = Objupdate.Publishdate;
            ht.Add(param_publishdate);
            SqlParameter param_telephoneexchange = new SqlParameter("@telephoneexchange", SqlDbType.NVarChar, 100);
            param_telephoneexchange.Value = Objupdate.Telephoneexchange;
            ht.Add(param_telephoneexchange);
            SqlParameter param_fax = new SqlParameter("@fax", SqlDbType.NVarChar, 100);
            param_fax.Value = Objupdate.Fax;
            ht.Add(param_fax);
            SqlParameter param_addressone = new SqlParameter("@addressone", SqlDbType.NVarChar, 1000);
            param_addressone.Value = Objupdate.Addressone;
            ht.Add(param_addressone);
            SqlParameter param_addresstwo = new SqlParameter("@addresstwo", SqlDbType.NVarChar, 1000);
            param_addresstwo.Value = Objupdate.Addresstwo;
            ht.Add(param_addresstwo);
            SqlParameter param_webaddress = new SqlParameter("@webaddress", SqlDbType.NVarChar, 512);
            param_webaddress.Value = Objupdate.Webaddress;
            ht.Add(param_webaddress);
            SqlParameter param_issn = new SqlParameter("@issn", SqlDbType.NVarChar, 100);
            param_issn.Value = Objupdate.Issn;
            ht.Add(param_issn);
            SqlParameter param_cooperate = new SqlParameter("@cooperate", SqlDbType.NVarChar, 4000);
            param_cooperate.Value = Objupdate.Cooperate;
            ht.Add(param_cooperate);
            SqlParameter param_circulation = new SqlParameter("@circulation", SqlDbType.Int, 4);
            param_circulation.Value = Objupdate.Circulation;
            ht.Add(param_circulation);
            SqlParameter param_publishchannels = new SqlParameter("@publishchannels", SqlDbType.NVarChar, 512);
            param_publishchannels.Value = Objupdate.Publishchannels;
            ht.Add(param_publishchannels);
            SqlParameter param_phoneone = new SqlParameter("@phoneone", SqlDbType.NVarChar, 100);
            param_phoneone.Value = Objupdate.Phoneone;
            ht.Add(param_phoneone);
            SqlParameter param_phonetwo = new SqlParameter("@phonetwo", SqlDbType.NVarChar, 100);
            param_phonetwo.Value = Objupdate.Phonetwo;
            ht.Add(param_phonetwo);
            SqlParameter param_endpublication = new SqlParameter("@endpublication", SqlDbType.NVarChar, 200);
            param_endpublication.Value = Objupdate.Endpublication;
            ht.Add(param_endpublication);
            SqlParameter param_adsphone = new SqlParameter("@adsphone", SqlDbType.NVarChar, 100);
            param_adsphone.Value = Objupdate.Adsphone;
            ht.Add(param_adsphone);
            SqlParameter param_adsprice = new SqlParameter("@adsprice", SqlDbType.Int, 4);
            param_adsprice.Value = Objupdate.Adsprice;
            ht.Add(param_adsprice);
            SqlParameter param_medialogo = new SqlParameter("@medialogo", SqlDbType.NVarChar, 1000);
            param_medialogo.Value = Objupdate.Medialogo;
            ht.Add(param_medialogo);
            SqlParameter param_briefing = new SqlParameter("@briefing", SqlDbType.Int, 4);
            param_briefing.Value = Objupdate.Briefing;
            ht.Add(param_briefing);
            SqlParameter param_mediaintro = new SqlParameter("@mediaintro", SqlDbType.NVarChar, 4000);
            param_mediaintro.Value = Objupdate.Mediaintro;
            ht.Add(param_mediaintro);
            SqlParameter param_engintro = new SqlParameter("@engintro", SqlDbType.NVarChar, 4000);
            param_engintro.Value = Objupdate.Engintro;
            ht.Add(param_engintro);
            SqlParameter param_remarks = new SqlParameter("@remarks", SqlDbType.NVarChar, 4000);
            param_remarks.Value = Objupdate.Remarks;
            ht.Add(param_remarks);
            SqlParameter param_channelname = new SqlParameter("@channelname", SqlDbType.NVarChar, 100);
            param_channelname.Value = Objupdate.Channelname;
            ht.Add(param_channelname);
            SqlParameter param_dabfm = new SqlParameter("@dabfm", SqlDbType.NVarChar, 100);
            param_dabfm.Value = Objupdate.Dabfm;
            ht.Add(param_dabfm);
            SqlParameter param_topicname = new SqlParameter("@topicname", SqlDbType.NVarChar, 100);
            param_topicname.Value = Objupdate.Topicname;
            ht.Add(param_topicname);
            SqlParameter param_topicproperty = new SqlParameter("@topicproperty", SqlDbType.Int, 4);
            param_topicproperty.Value = Objupdate.Topicproperty;
            ht.Add(param_topicproperty);
            SqlParameter param_overriderange = new SqlParameter("@overriderange", SqlDbType.NVarChar, 100);
            param_overriderange.Value = Objupdate.Overriderange;
            ht.Add(param_overriderange);
            SqlParameter param_rating = new SqlParameter("@rating", SqlDbType.NVarChar, 100);
            param_rating.Value = Objupdate.Rating;
            ht.Add(param_rating);
            SqlParameter param_topictime = new SqlParameter("@topictime", SqlDbType.NVarChar, 100);
            param_topictime.Value = Objupdate.Topictime;
            ht.Add(param_topictime);
            SqlParameter param_channelwebaddress = new SqlParameter("@channelwebaddress", SqlDbType.NVarChar, 512);
            param_channelwebaddress.Value = Objupdate.Channelwebaddress;
            ht.Add(param_channelwebaddress);
            SqlParameter param_countryid = new SqlParameter("@countryid", SqlDbType.Int, 4);
            param_countryid.Value = Objupdate.Countryid;
            ht.Add(param_countryid);
            SqlParameter param_provinceid = new SqlParameter("@provinceid", SqlDbType.Int, 4);
            param_provinceid.Value = Objupdate.Provinceid;
            ht.Add(param_provinceid);
            SqlParameter param_cityid = new SqlParameter("@cityid", SqlDbType.Int, 4);
            param_cityid.Value = Objupdate.Cityid;
            ht.Add(param_cityid);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_mediatype = new SqlParameter("@mediatype", SqlDbType.NVarChar, 100);
            param_mediatype.Value = Objupdate.Mediatype;
            ht.Add(param_mediatype);
            SqlParameter param_addr1_provinceid = new SqlParameter("@addr1_provinceid", SqlDbType.Int, 4);
            param_addr1_provinceid.Value = Objupdate.Addr1_provinceid;
            ht.Add(param_addr1_provinceid);
            SqlParameter param_addr1_cityid = new SqlParameter("@addr1_cityid", SqlDbType.Int, 4);
            param_addr1_cityid.Value = Objupdate.Addr1_cityid;
            ht.Add(param_addr1_cityid);
            SqlParameter param_addr1_countryid = new SqlParameter("@addr1_countryid", SqlDbType.Int, 4);
            param_addr1_countryid.Value = Objupdate.Addr1_countryid;
            ht.Add(param_addr1_countryid);
            SqlParameter param_addr2_provinceid = new SqlParameter("@addr2_provinceid", SqlDbType.Int, 4);
            param_addr2_provinceid.Value = Objupdate.Addr2_provinceid;
            ht.Add(param_addr2_provinceid);
            SqlParameter param_addr2_cityid = new SqlParameter("@addr2_cityid", SqlDbType.Int, 4);
            param_addr2_cityid.Value = Objupdate.Addr2_cityid;
            ht.Add(param_addr2_cityid);
            SqlParameter param_addr2_countryid = new SqlParameter("@addr2_countryid", SqlDbType.Int, 4);
            param_addr2_countryid.Value = Objupdate.Addr2_countryid;
            ht.Add(param_addr2_countryid);
            SqlParameter param_postcode = new SqlParameter("@postcode", SqlDbType.NVarChar, 100);
            param_postcode.Value = Objupdate.Postcode;
            ht.Add(param_postcode);
            SqlParameter param_regionattribute = new SqlParameter("@regionattribute", SqlDbType.Int, 4);
            param_regionattribute.Value = Objupdate.Regionattribute;
            ht.Add(param_regionattribute);
            SqlParameter param_override_countryid = new SqlParameter("@override_countryid", SqlDbType.Int, 4);
            param_override_countryid.Value = Objupdate.Override_countryid;
            ht.Add(param_override_countryid);
            SqlParameter param_override_provinceid = new SqlParameter("@override_provinceid", SqlDbType.Int, 4);
            param_override_provinceid.Value = Objupdate.Override_provinceid;
            ht.Add(param_override_provinceid);
            SqlParameter param_override_cityid = new SqlParameter("@override_cityid", SqlDbType.Int, 4);
            param_override_cityid.Value = Objupdate.Override_cityid;
            ht.Add(param_override_cityid);
            SqlParameter param_override_describe = new SqlParameter("@override_describe", SqlDbType.NVarChar, 200);
            param_override_describe.Value = Objupdate.Override_describe;
            ht.Add(param_override_describe);
            SqlParameter param_industryid = new SqlParameter("@industryid", SqlDbType.Int, 4);
            param_industryid.Value = Objupdate.Industryid;
            ht.Add(param_industryid);
            SqlParameter param_issueregion = new SqlParameter("@issueregion", SqlDbType.NVarChar, 1000);
            param_issueregion.Value = Objupdate.Issueregion;
            ht.Add(param_issueregion);
            SqlParameter param_branchs = new SqlParameter("@branchs", SqlDbType.NVarChar, 4000);
            param_branchs.Value = Objupdate.Branchs;
            ht.Add(param_branchs);
            SqlParameter param_publishperiods = new SqlParameter("@publishperiods", SqlDbType.NVarChar, 100);
            param_publishperiods.Value = Objupdate.Publishperiods;
            ht.Add(param_publishperiods);
            if (objTerm == null && (term == null || term.Trim().Length <= 0))
            {
                sql += " and id=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = Objupdate.Id;
                    ht.Add(p);
                }

            }
            if (objTerm != null)
            {
                sql += getTerms(objTerm, ref ht);
            }
            if (term != null && term.Trim().Length > 0)
            {
                sql += term;
            }
            if (param != null && param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (ESP.Media.Access.Utilities.Common.Find(ht, param[i].ParameterName) == -1)
                    {
                        ht.Add(param[i]);
                    }
                }
            }
            return sql;
        }
        
        //更新操作
        public static bool updateInfo(SqlTransaction trans, MediaitemshistInfo objterm, MediaitemshistInfo Objupdate, string term, params SqlParameter[] param)
        {
            if (Objupdate == null)
            {
                return false;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = getUpdateString(objterm, Objupdate, term, ref ht, param);
            SqlParameter[] para = ht.ToArray();
            int rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, para);
            if (rows >= 0)
            {
                return true;
            }
            return false;
        }

        //更新操作
        public static bool updateInfo(MediaitemshistInfo objterm, MediaitemshistInfo Objupdate, string term, params SqlParameter[] param)
        {
            if (Objupdate == null)
            {
                return false;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = getUpdateString(objterm, Objupdate, term, ref ht, param);
            SqlParameter[] para = ht.ToArray();
            int rowNum = 0;
            using (SqlConnection conn = new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    rowNum = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, para);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (rowNum >= 0)
            {
                return true;
            }
            return false;
        }

        private static string getTerms(MediaitemshistInfo obj, ref List<SqlParameter> ht)
        {
            string term = string.Empty;
            if (obj.Id > 0)//id
            {
                term += " and id=@id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@id") == -1)
                {
                    SqlParameter p = new SqlParameter("@id", SqlDbType.Int, 4);
                    p.Value = obj.Id;
                    ht.Add(p);
                }
            }
            if (obj.Version > 0)//版本号
            {
                term += " and version=@version ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@version") == -1)
                {
                    SqlParameter p = new SqlParameter("@version", SqlDbType.Int, 4);
                    p.Value = obj.Version;
                    ht.Add(p);
                }
            }
            if (obj.Mediaitemid > 0)//MediaitemID
            {
                term += " and mediaitemid=@mediaitemid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaitemid") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaitemid", SqlDbType.Int, 4);
                    p.Value = obj.Mediaitemid;
                    ht.Add(p);
                }
            }
            if (obj.Mediacname != null && obj.Mediacname.Trim().Length > 0)
            {
                term += " and mediacname=@mediacname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediacname") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediacname", SqlDbType.NVarChar, 512);
                    p.Value = obj.Mediacname;
                    ht.Add(p);
                }
            }
            if (obj.Mediaename != null && obj.Mediaename.Trim().Length > 0)
            {
                term += " and mediaename=@mediaename ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaename") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaename", SqlDbType.NVarChar, 512);
                    p.Value = obj.Mediaename;
                    ht.Add(p);
                }
            }
            if (obj.Cshortname != null && obj.Cshortname.Trim().Length > 0)
            {
                term += " and cshortname=@cshortname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cshortname") == -1)
                {
                    SqlParameter p = new SqlParameter("@cshortname", SqlDbType.NVarChar, 512);
                    p.Value = obj.Cshortname;
                    ht.Add(p);
                }
            }
            if (obj.Eshortname != null && obj.Eshortname.Trim().Length > 0)
            {
                term += " and eshortname=@eshortname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@eshortname") == -1)
                {
                    SqlParameter p = new SqlParameter("@eshortname", SqlDbType.NVarChar, 512);
                    p.Value = obj.Eshortname;
                    ht.Add(p);
                }
            }
            if (obj.Mediaitemtype > 0)//MediaItemType
            {
                term += " and mediaitemtype=@mediaitemtype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaitemtype") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaitemtype", SqlDbType.SmallInt, 2);
                    p.Value = obj.Mediaitemtype;
                    ht.Add(p);
                }
            }
            if (obj.Currentversion > 0)//CurrentVersion
            {
                term += " and currentversion=@currentversion ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@currentversion") == -1)
                {
                    SqlParameter p = new SqlParameter("@currentversion", SqlDbType.Int, 4);
                    p.Value = obj.Currentversion;
                    ht.Add(p);
                }
            }
            if (obj.Status > 0)//Status
            {
                term += " and status=@status ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@status") == -1)
                {
                    SqlParameter p = new SqlParameter("@status", SqlDbType.Int, 4);
                    p.Value = obj.Status;
                    ht.Add(p);
                }
            }
            if (obj.Createdbyuserid > 0)//CreatedByUserID
            {
                term += " and createdbyuserid=@createdbyuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdbyuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdbyuserid", SqlDbType.Int, 4);
                    p.Value = obj.Createdbyuserid;
                    ht.Add(p);
                }
            }
            if (obj.Createdip != null && obj.Createdip.Trim().Length > 0)
            {
                term += " and createdip=@createdip ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createdip") == -1)
                {
                    SqlParameter p = new SqlParameter("@createdip", SqlDbType.NVarChar, 100);
                    p.Value = obj.Createdip;
                    ht.Add(p);
                }
            }
            if (obj.Createddate != null && obj.Createddate.Trim().Length > 0)
            {
                term += " and createddate=@createddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@createddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@createddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Createddate);
                    ht.Add(p);
                }
            }
            if (obj.Lastmodifiedbyuserid > 0)//LastModifiedByUserID
            {
                term += " and lastmodifiedbyuserid=@lastmodifiedbyuserid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastmodifiedbyuserid") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastmodifiedbyuserid", SqlDbType.Int, 4);
                    p.Value = obj.Lastmodifiedbyuserid;
                    ht.Add(p);
                }
            }
            if (obj.Lastmodifieddate != null && obj.Lastmodifieddate.Trim().Length > 0)
            {
                term += " and lastmodifieddate=@lastmodifieddate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastmodifieddate") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastmodifieddate", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastmodifieddate);
                    ht.Add(p);
                }
            }
            if (obj.Lastmodifiedip != null && obj.Lastmodifiedip.Trim().Length > 0)
            {
                term += " and lastmodifiedip=@lastmodifiedip ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@lastmodifiedip") == -1)
                {
                    SqlParameter p = new SqlParameter("@lastmodifiedip", SqlDbType.NVarChar, 100);
                    p.Value = obj.Lastmodifiedip;
                    ht.Add(p);
                }
            }
            if (obj.Mediumsort != null && obj.Mediumsort.Trim().Length > 0)
            {
                term += " and mediumsort=@mediumsort ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediumsort") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediumsort", SqlDbType.NVarChar, 100);
                    p.Value = obj.Mediumsort;
                    ht.Add(p);
                }
            }
            if (obj.Readersort != null && obj.Readersort.Trim().Length > 0)
            {
                term += " and readersort=@readersort ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@readersort") == -1)
                {
                    SqlParameter p = new SqlParameter("@readersort", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Readersort;
                    ht.Add(p);
                }
            }
            if (obj.Governingbody != null && obj.Governingbody.Trim().Length > 0)
            {
                term += " and governingbody=@governingbody ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@governingbody") == -1)
                {
                    SqlParameter p = new SqlParameter("@governingbody", SqlDbType.NVarChar, 100);
                    p.Value = obj.Governingbody;
                    ht.Add(p);
                }
            }
            if (obj.Frontfor != null && obj.Frontfor.Trim().Length > 0)
            {
                term += " and frontfor=@frontfor ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@frontfor") == -1)
                {
                    SqlParameter p = new SqlParameter("@frontfor", SqlDbType.NVarChar, 2000);
                    p.Value = obj.Frontfor;
                    ht.Add(p);
                }
            }
            if (obj.Proprieter != null && obj.Proprieter.Trim().Length > 0)
            {
                term += " and proprieter=@proprieter ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@proprieter") == -1)
                {
                    SqlParameter p = new SqlParameter("@proprieter", SqlDbType.NVarChar, 100);
                    p.Value = obj.Proprieter;
                    ht.Add(p);
                }
            }
            if (obj.Subproprieter != null && obj.Subproprieter.Trim().Length > 0)
            {
                term += " and subproprieter=@subproprieter ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@subproprieter") == -1)
                {
                    SqlParameter p = new SqlParameter("@subproprieter", SqlDbType.NVarChar, 100);
                    p.Value = obj.Subproprieter;
                    ht.Add(p);
                }
            }
            if (obj.Chiefeditor != null && obj.Chiefeditor.Trim().Length > 0)
            {
                term += " and chiefeditor=@chiefeditor ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@chiefeditor") == -1)
                {
                    SqlParameter p = new SqlParameter("@chiefeditor", SqlDbType.NVarChar, 100);
                    p.Value = obj.Chiefeditor;
                    ht.Add(p);
                }
            }
            if (obj.Admineditor != null && obj.Admineditor.Trim().Length > 0)
            {
                term += " and admineditor=@admineditor ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@admineditor") == -1)
                {
                    SqlParameter p = new SqlParameter("@admineditor", SqlDbType.NVarChar, 100);
                    p.Value = obj.Admineditor;
                    ht.Add(p);
                }
            }
            if (obj.Subeditor != null && obj.Subeditor.Trim().Length > 0)
            {
                term += " and subeditor=@subeditor ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@subeditor") == -1)
                {
                    SqlParameter p = new SqlParameter("@subeditor", SqlDbType.NVarChar, 100);
                    p.Value = obj.Subeditor;
                    ht.Add(p);
                }
            }
            if (obj.Manager != null && obj.Manager.Trim().Length > 0)
            {
                term += " and manager=@manager ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@manager") == -1)
                {
                    SqlParameter p = new SqlParameter("@manager", SqlDbType.NVarChar, 100);
                    p.Value = obj.Manager;
                    ht.Add(p);
                }
            }
            if (obj.Zhuren != null && obj.Zhuren.Trim().Length > 0)
            {
                term += " and zhuren=@zhuren ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@zhuren") == -1)
                {
                    SqlParameter p = new SqlParameter("@zhuren", SqlDbType.NVarChar, 100);
                    p.Value = obj.Zhuren;
                    ht.Add(p);
                }
            }
            if (obj.Producer != null && obj.Producer.Trim().Length > 0)
            {
                term += " and producer=@producer ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@producer") == -1)
                {
                    SqlParameter p = new SqlParameter("@producer", SqlDbType.NVarChar, 100);
                    p.Value = obj.Producer;
                    ht.Add(p);
                }
            }
            if (obj.Startpublication != null && obj.Startpublication.Trim().Length > 0)
            {
                term += " and startpublication=@startpublication ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@startpublication") == -1)
                {
                    SqlParameter p = new SqlParameter("@startpublication", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Startpublication);
                    ht.Add(p);
                }
            }
            if (obj.Publishdate != null && obj.Publishdate.Trim().Length > 0)
            {
                term += " and publishdate=@publishdate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@publishdate") == -1)
                {
                    SqlParameter p = new SqlParameter("@publishdate", SqlDbType.NVarChar, 200);
                    p.Value = obj.Publishdate;
                    ht.Add(p);
                }
            }
            if (obj.Telephoneexchange != null && obj.Telephoneexchange.Trim().Length > 0)
            {
                term += " and telephoneexchange=@telephoneexchange ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@telephoneexchange") == -1)
                {
                    SqlParameter p = new SqlParameter("@telephoneexchange", SqlDbType.NVarChar, 100);
                    p.Value = obj.Telephoneexchange;
                    ht.Add(p);
                }
            }
            if (obj.Fax != null && obj.Fax.Trim().Length > 0)
            {
                term += " and fax=@fax ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@fax") == -1)
                {
                    SqlParameter p = new SqlParameter("@fax", SqlDbType.NVarChar, 100);
                    p.Value = obj.Fax;
                    ht.Add(p);
                }
            }
            if (obj.Addressone != null && obj.Addressone.Trim().Length > 0)
            {
                term += " and addressone=@addressone ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addressone") == -1)
                {
                    SqlParameter p = new SqlParameter("@addressone", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Addressone;
                    ht.Add(p);
                }
            }
            if (obj.Addresstwo != null && obj.Addresstwo.Trim().Length > 0)
            {
                term += " and addresstwo=@addresstwo ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addresstwo") == -1)
                {
                    SqlParameter p = new SqlParameter("@addresstwo", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Addresstwo;
                    ht.Add(p);
                }
            }
            if (obj.Webaddress != null && obj.Webaddress.Trim().Length > 0)
            {
                term += " and webaddress=@webaddress ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@webaddress") == -1)
                {
                    SqlParameter p = new SqlParameter("@webaddress", SqlDbType.NVarChar, 512);
                    p.Value = obj.Webaddress;
                    ht.Add(p);
                }
            }
            if (obj.Issn != null && obj.Issn.Trim().Length > 0)
            {
                term += " and issn=@issn ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@issn") == -1)
                {
                    SqlParameter p = new SqlParameter("@issn", SqlDbType.NVarChar, 100);
                    p.Value = obj.Issn;
                    ht.Add(p);
                }
            }
            if (obj.Cooperate != null && obj.Cooperate.Trim().Length > 0)
            {
                term += " and cooperate=@cooperate ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cooperate") == -1)
                {
                    SqlParameter p = new SqlParameter("@cooperate", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Cooperate;
                    ht.Add(p);
                }
            }
            if (obj.Circulation > 0)//Circulation
            {
                term += " and circulation=@circulation ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@circulation") == -1)
                {
                    SqlParameter p = new SqlParameter("@circulation", SqlDbType.Int, 4);
                    p.Value = obj.Circulation;
                    ht.Add(p);
                }
            }
            if (obj.Publishchannels != null && obj.Publishchannels.Trim().Length > 0)
            {
                term += " and publishchannels=@publishchannels ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@publishchannels") == -1)
                {
                    SqlParameter p = new SqlParameter("@publishchannels", SqlDbType.NVarChar, 512);
                    p.Value = obj.Publishchannels;
                    ht.Add(p);
                }
            }
            if (obj.Phoneone != null && obj.Phoneone.Trim().Length > 0)
            {
                term += " and phoneone=@phoneone ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@phoneone") == -1)
                {
                    SqlParameter p = new SqlParameter("@phoneone", SqlDbType.NVarChar, 100);
                    p.Value = obj.Phoneone;
                    ht.Add(p);
                }
            }
            if (obj.Phonetwo != null && obj.Phonetwo.Trim().Length > 0)
            {
                term += " and phonetwo=@phonetwo ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@phonetwo") == -1)
                {
                    SqlParameter p = new SqlParameter("@phonetwo", SqlDbType.NVarChar, 100);
                    p.Value = obj.Phonetwo;
                    ht.Add(p);
                }
            }
            if (obj.Endpublication != null && obj.Endpublication.Trim().Length > 0)
            {
                term += " and endpublication=@endpublication ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@endpublication") == -1)
                {
                    SqlParameter p = new SqlParameter("@endpublication", SqlDbType.NVarChar, 200);
                    p.Value = obj.Endpublication;
                    ht.Add(p);
                }
            }
            if (obj.Adsphone != null && obj.Adsphone.Trim().Length > 0)
            {
                term += " and adsphone=@adsphone ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@adsphone") == -1)
                {
                    SqlParameter p = new SqlParameter("@adsphone", SqlDbType.NVarChar, 100);
                    p.Value = obj.Adsphone;
                    ht.Add(p);
                }
            }
            if (obj.Adsprice > 0)//AdsPrice
            {
                term += " and adsprice=@adsprice ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@adsprice") == -1)
                {
                    SqlParameter p = new SqlParameter("@adsprice", SqlDbType.Int, 4);
                    p.Value = obj.Adsprice;
                    ht.Add(p);
                }
            }
            if (obj.Medialogo != null && obj.Medialogo.Trim().Length > 0)
            {
                term += " and medialogo=@medialogo ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@medialogo") == -1)
                {
                    SqlParameter p = new SqlParameter("@medialogo", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Medialogo;
                    ht.Add(p);
                }
            }
            if (obj.Briefing > 0)//Briefing
            {
                term += " and briefing=@briefing ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@briefing") == -1)
                {
                    SqlParameter p = new SqlParameter("@briefing", SqlDbType.Int, 4);
                    p.Value = obj.Briefing;
                    ht.Add(p);
                }
            }
            if (obj.Mediaintro != null && obj.Mediaintro.Trim().Length > 0)
            {
                term += " and mediaintro=@mediaintro ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediaintro") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediaintro", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Mediaintro;
                    ht.Add(p);
                }
            }
            if (obj.Engintro != null && obj.Engintro.Trim().Length > 0)
            {
                term += " and engintro=@engintro ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@engintro") == -1)
                {
                    SqlParameter p = new SqlParameter("@engintro", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Engintro;
                    ht.Add(p);
                }
            }
            if (obj.Remarks != null && obj.Remarks.Trim().Length > 0)
            {
                term += " and remarks=@remarks ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@remarks") == -1)
                {
                    SqlParameter p = new SqlParameter("@remarks", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Remarks;
                    ht.Add(p);
                }
            }
            if (obj.Channelname != null && obj.Channelname.Trim().Length > 0)
            {
                term += " and channelname=@channelname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@channelname") == -1)
                {
                    SqlParameter p = new SqlParameter("@channelname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Channelname;
                    ht.Add(p);
                }
            }
            if (obj.Dabfm != null && obj.Dabfm.Trim().Length > 0)
            {
                term += " and dabfm=@dabfm ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@dabfm") == -1)
                {
                    SqlParameter p = new SqlParameter("@dabfm", SqlDbType.NVarChar, 100);
                    p.Value = obj.Dabfm;
                    ht.Add(p);
                }
            }
            if (obj.Topicname != null && obj.Topicname.Trim().Length > 0)
            {
                term += " and topicname=@topicname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@topicname") == -1)
                {
                    SqlParameter p = new SqlParameter("@topicname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Topicname;
                    ht.Add(p);
                }
            }
            if (obj.Topicproperty > 0)//TopicProperty
            {
                term += " and topicproperty=@topicproperty ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@topicproperty") == -1)
                {
                    SqlParameter p = new SqlParameter("@topicproperty", SqlDbType.Int, 4);
                    p.Value = obj.Topicproperty;
                    ht.Add(p);
                }
            }
            if (obj.Overriderange != null && obj.Overriderange.Trim().Length > 0)
            {
                term += " and overriderange=@overriderange ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@overriderange") == -1)
                {
                    SqlParameter p = new SqlParameter("@overriderange", SqlDbType.NVarChar, 100);
                    p.Value = obj.Overriderange;
                    ht.Add(p);
                }
            }
            if (obj.Rating != null && obj.Rating.Trim().Length > 0)
            {
                term += " and rating=@rating ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@rating") == -1)
                {
                    SqlParameter p = new SqlParameter("@rating", SqlDbType.NVarChar, 100);
                    p.Value = obj.Rating;
                    ht.Add(p);
                }
            }
            if (obj.Topictime != null && obj.Topictime.Trim().Length > 0)
            {
                term += " and topictime=@topictime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@topictime") == -1)
                {
                    SqlParameter p = new SqlParameter("@topictime", SqlDbType.NVarChar, 100);
                    p.Value = obj.Topictime;
                    ht.Add(p);
                }
            }
            if (obj.Channelwebaddress != null && obj.Channelwebaddress.Trim().Length > 0)
            {
                term += " and channelwebaddress=@channelwebaddress ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@channelwebaddress") == -1)
                {
                    SqlParameter p = new SqlParameter("@channelwebaddress", SqlDbType.NVarChar, 512);
                    p.Value = obj.Channelwebaddress;
                    ht.Add(p);
                }
            }
            if (obj.Countryid > 0)//国家id
            {
                term += " and countryid=@countryid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@countryid") == -1)
                {
                    SqlParameter p = new SqlParameter("@countryid", SqlDbType.Int, 4);
                    p.Value = obj.Countryid;
                    ht.Add(p);
                }
            }
            if (obj.Provinceid > 0)//省id
            {
                term += " and provinceid=@provinceid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@provinceid") == -1)
                {
                    SqlParameter p = new SqlParameter("@provinceid", SqlDbType.Int, 4);
                    p.Value = obj.Provinceid;
                    ht.Add(p);
                }
            }
            if (obj.Cityid > 0)//城市id
            {
                term += " and cityid=@cityid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cityid") == -1)
                {
                    SqlParameter p = new SqlParameter("@cityid", SqlDbType.Int, 4);
                    p.Value = obj.Cityid;
                    ht.Add(p);
                }
            }
            if (obj.Del > 0)//删除标记
            {
                term += " and del=@del ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@del") == -1)
                {
                    SqlParameter p = new SqlParameter("@del", SqlDbType.SmallInt, 2);
                    p.Value = obj.Del;
                    ht.Add(p);
                }
            }
            if (obj.Mediatype != null && obj.Mediatype.Trim().Length > 0)
            {
                term += " and mediatype=@mediatype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@mediatype") == -1)
                {
                    SqlParameter p = new SqlParameter("@mediatype", SqlDbType.NVarChar, 100);
                    p.Value = obj.Mediatype;
                    ht.Add(p);
                }
            }
            if (obj.Addr1_provinceid > 0)//地址1省id
            {
                term += " and addr1_provinceid=@addr1_provinceid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addr1_provinceid") == -1)
                {
                    SqlParameter p = new SqlParameter("@addr1_provinceid", SqlDbType.Int, 4);
                    p.Value = obj.Addr1_provinceid;
                    ht.Add(p);
                }
            }
            if (obj.Addr1_cityid > 0)//地址1城市id
            {
                term += " and addr1_cityid=@addr1_cityid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addr1_cityid") == -1)
                {
                    SqlParameter p = new SqlParameter("@addr1_cityid", SqlDbType.Int, 4);
                    p.Value = obj.Addr1_cityid;
                    ht.Add(p);
                }
            }
            if (obj.Addr1_countryid > 0)//地址1国家id
            {
                term += " and addr1_countryid=@addr1_countryid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addr1_countryid") == -1)
                {
                    SqlParameter p = new SqlParameter("@addr1_countryid", SqlDbType.Int, 4);
                    p.Value = obj.Addr1_countryid;
                    ht.Add(p);
                }
            }
            if (obj.Addr2_provinceid > 0)//地址2省id
            {
                term += " and addr2_provinceid=@addr2_provinceid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addr2_provinceid") == -1)
                {
                    SqlParameter p = new SqlParameter("@addr2_provinceid", SqlDbType.Int, 4);
                    p.Value = obj.Addr2_provinceid;
                    ht.Add(p);
                }
            }
            if (obj.Addr2_cityid > 0)//地址2城市id
            {
                term += " and addr2_cityid=@addr2_cityid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addr2_cityid") == -1)
                {
                    SqlParameter p = new SqlParameter("@addr2_cityid", SqlDbType.Int, 4);
                    p.Value = obj.Addr2_cityid;
                    ht.Add(p);
                }
            }
            if (obj.Addr2_countryid > 0)//地址2国家id
            {
                term += " and addr2_countryid=@addr2_countryid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@addr2_countryid") == -1)
                {
                    SqlParameter p = new SqlParameter("@addr2_countryid", SqlDbType.Int, 4);
                    p.Value = obj.Addr2_countryid;
                    ht.Add(p);
                }
            }
            if (obj.Postcode != null && obj.Postcode.Trim().Length > 0)
            {
                term += " and postcode=@postcode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@postcode") == -1)
                {
                    SqlParameter p = new SqlParameter("@postcode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Postcode;
                    ht.Add(p);
                }
            }
            if (obj.Regionattribute > 0)//RegionAttribute
            {
                term += " and regionattribute=@regionattribute ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@regionattribute") == -1)
                {
                    SqlParameter p = new SqlParameter("@regionattribute", SqlDbType.Int, 4);
                    p.Value = obj.Regionattribute;
                    ht.Add(p);
                }
            }
            if (obj.Override_countryid > 0)//Override_Countryid
            {
                term += " and override_countryid=@override_countryid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@override_countryid") == -1)
                {
                    SqlParameter p = new SqlParameter("@override_countryid", SqlDbType.Int, 4);
                    p.Value = obj.Override_countryid;
                    ht.Add(p);
                }
            }
            if (obj.Override_provinceid > 0)//Override_Provinceid
            {
                term += " and override_provinceid=@override_provinceid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@override_provinceid") == -1)
                {
                    SqlParameter p = new SqlParameter("@override_provinceid", SqlDbType.Int, 4);
                    p.Value = obj.Override_provinceid;
                    ht.Add(p);
                }
            }
            if (obj.Override_cityid > 0)//Override_Cityid
            {
                term += " and override_cityid=@override_cityid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@override_cityid") == -1)
                {
                    SqlParameter p = new SqlParameter("@override_cityid", SqlDbType.Int, 4);
                    p.Value = obj.Override_cityid;
                    ht.Add(p);
                }
            }
            if (obj.Override_describe != null && obj.Override_describe.Trim().Length > 0)
            {
                term += " and override_describe=@override_describe ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@override_describe") == -1)
                {
                    SqlParameter p = new SqlParameter("@override_describe", SqlDbType.NVarChar, 200);
                    p.Value = obj.Override_describe;
                    ht.Add(p);
                }
            }
            if (obj.Industryid > 0)//IndustryID
            {
                term += " and industryid=@industryid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@industryid") == -1)
                {
                    SqlParameter p = new SqlParameter("@industryid", SqlDbType.Int, 4);
                    p.Value = obj.Industryid;
                    ht.Add(p);
                }
            }
            if (obj.Issueregion != null && obj.Issueregion.Trim().Length > 0)
            {
                term += " and issueregion=@issueregion ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@issueregion") == -1)
                {
                    SqlParameter p = new SqlParameter("@issueregion", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Issueregion;
                    ht.Add(p);
                }
            }
            if (obj.Branchs != null && obj.Branchs.Trim().Length > 0)
            {
                term += " and branchs=@branchs ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@branchs") == -1)
                {
                    SqlParameter p = new SqlParameter("@branchs", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Branchs;
                    ht.Add(p);
                }
            }
            if (obj.Publishperiods != null && obj.Publishperiods.Trim().Length > 0)
            {
                term += " and publishperiods=@publishperiods ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@publishperiods") == -1)
                {
                    SqlParameter p = new SqlParameter("@publishperiods", SqlDbType.NVarChar, 100);
                    p.Value = obj.Publishperiods;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(MediaitemshistInfo obj, ref Hashtable ht)
        {
            string term = string.Empty;
            if (obj.Id > 0)//id
            {
                term += " and a.id=@id ";
                if (!ht.ContainsKey("@id"))
                {
                    ht.Add("@id", obj.Id);
                }
            }
            if (obj.Version > 0)//版本号
            {
                term += " and a.version=@version ";
                if (!ht.ContainsKey("@version"))
                {
                    ht.Add("@version", obj.Version);
                }
            }
            if (obj.Mediaitemid > 0)//MediaitemID
            {
                term += " and a.mediaitemid=@mediaitemid ";
                if (!ht.ContainsKey("@mediaitemid"))
                {
                    ht.Add("@mediaitemid", obj.Mediaitemid);
                }
            }
            if (obj.Mediacname != null && obj.Mediacname.Trim().Length > 0)
            {
                term += " and a.mediacname=@mediacname ";
                if (!ht.ContainsKey("@mediacname"))
                {
                    ht.Add("@mediacname", obj.Mediacname);
                }
            }
            if (obj.Mediaename != null && obj.Mediaename.Trim().Length > 0)
            {
                term += " and a.mediaename=@mediaename ";
                if (!ht.ContainsKey("@mediaename"))
                {
                    ht.Add("@mediaename", obj.Mediaename);
                }
            }
            if (obj.Cshortname != null && obj.Cshortname.Trim().Length > 0)
            {
                term += " and a.cshortname=@cshortname ";
                if (!ht.ContainsKey("@cshortname"))
                {
                    ht.Add("@cshortname", obj.Cshortname);
                }
            }
            if (obj.Eshortname != null && obj.Eshortname.Trim().Length > 0)
            {
                term += " and a.eshortname=@eshortname ";
                if (!ht.ContainsKey("@eshortname"))
                {
                    ht.Add("@eshortname", obj.Eshortname);
                }
            }
            if (obj.Mediaitemtype > 0)//MediaItemType
            {
                term += " and a.mediaitemtype=@mediaitemtype ";
                if (!ht.ContainsKey("@mediaitemtype"))
                {
                    ht.Add("@mediaitemtype", obj.Mediaitemtype);
                }
            }
            if (obj.Currentversion > 0)//CurrentVersion
            {
                term += " and a.currentversion=@currentversion ";
                if (!ht.ContainsKey("@currentversion"))
                {
                    ht.Add("@currentversion", obj.Currentversion);
                }
            }
            if (obj.Status > 0)//Status
            {
                term += " and a.status=@status ";
                if (!ht.ContainsKey("@status"))
                {
                    ht.Add("@status", obj.Status);
                }
            }
            if (obj.Createdbyuserid > 0)//CreatedByUserID
            {
                term += " and a.createdbyuserid=@createdbyuserid ";
                if (!ht.ContainsKey("@createdbyuserid"))
                {
                    ht.Add("@createdbyuserid", obj.Createdbyuserid);
                }
            }
            if (obj.Createdip != null && obj.Createdip.Trim().Length > 0)
            {
                term += " and a.createdip=@createdip ";
                if (!ht.ContainsKey("@createdip"))
                {
                    ht.Add("@createdip", obj.Createdip);
                }
            }
            if (obj.Createddate != null && obj.Createddate.Trim().Length > 0)
            {
                term += " and a.createddate=@createddate ";
                if (!ht.ContainsKey("@createddate"))
                {
                    ht.Add("@createddate", obj.Createddate);
                }
            }
            if (obj.Lastmodifiedbyuserid > 0)//LastModifiedByUserID
            {
                term += " and a.lastmodifiedbyuserid=@lastmodifiedbyuserid ";
                if (!ht.ContainsKey("@lastmodifiedbyuserid"))
                {
                    ht.Add("@lastmodifiedbyuserid", obj.Lastmodifiedbyuserid);
                }
            }
            if (obj.Lastmodifieddate != null && obj.Lastmodifieddate.Trim().Length > 0)
            {
                term += " and a.lastmodifieddate=@lastmodifieddate ";
                if (!ht.ContainsKey("@lastmodifieddate"))
                {
                    ht.Add("@lastmodifieddate", obj.Lastmodifieddate);
                }
            }
            if (obj.Lastmodifiedip != null && obj.Lastmodifiedip.Trim().Length > 0)
            {
                term += " and a.lastmodifiedip=@lastmodifiedip ";
                if (!ht.ContainsKey("@lastmodifiedip"))
                {
                    ht.Add("@lastmodifiedip", obj.Lastmodifiedip);
                }
            }
            if (obj.Mediumsort != null && obj.Mediumsort.Trim().Length > 0)
            {
                term += " and a.mediumsort=@mediumsort ";
                if (!ht.ContainsKey("@mediumsort"))
                {
                    ht.Add("@mediumsort", obj.Mediumsort);
                }
            }
            if (obj.Readersort != null && obj.Readersort.Trim().Length > 0)
            {
                term += " and a.readersort=@readersort ";
                if (!ht.ContainsKey("@readersort"))
                {
                    ht.Add("@readersort", obj.Readersort);
                }
            }
            if (obj.Governingbody != null && obj.Governingbody.Trim().Length > 0)
            {
                term += " and a.governingbody=@governingbody ";
                if (!ht.ContainsKey("@governingbody"))
                {
                    ht.Add("@governingbody", obj.Governingbody);
                }
            }
            if (obj.Frontfor != null && obj.Frontfor.Trim().Length > 0)
            {
                term += " and a.frontfor=@frontfor ";
                if (!ht.ContainsKey("@frontfor"))
                {
                    ht.Add("@frontfor", obj.Frontfor);
                }
            }
            if (obj.Proprieter != null && obj.Proprieter.Trim().Length > 0)
            {
                term += " and a.proprieter=@proprieter ";
                if (!ht.ContainsKey("@proprieter"))
                {
                    ht.Add("@proprieter", obj.Proprieter);
                }
            }
            if (obj.Subproprieter != null && obj.Subproprieter.Trim().Length > 0)
            {
                term += " and a.subproprieter=@subproprieter ";
                if (!ht.ContainsKey("@subproprieter"))
                {
                    ht.Add("@subproprieter", obj.Subproprieter);
                }
            }
            if (obj.Chiefeditor != null && obj.Chiefeditor.Trim().Length > 0)
            {
                term += " and a.chiefeditor=@chiefeditor ";
                if (!ht.ContainsKey("@chiefeditor"))
                {
                    ht.Add("@chiefeditor", obj.Chiefeditor);
                }
            }
            if (obj.Admineditor != null && obj.Admineditor.Trim().Length > 0)
            {
                term += " and a.admineditor=@admineditor ";
                if (!ht.ContainsKey("@admineditor"))
                {
                    ht.Add("@admineditor", obj.Admineditor);
                }
            }
            if (obj.Subeditor != null && obj.Subeditor.Trim().Length > 0)
            {
                term += " and a.subeditor=@subeditor ";
                if (!ht.ContainsKey("@subeditor"))
                {
                    ht.Add("@subeditor", obj.Subeditor);
                }
            }
            if (obj.Manager != null && obj.Manager.Trim().Length > 0)
            {
                term += " and a.manager=@manager ";
                if (!ht.ContainsKey("@manager"))
                {
                    ht.Add("@manager", obj.Manager);
                }
            }
            if (obj.Zhuren != null && obj.Zhuren.Trim().Length > 0)
            {
                term += " and a.zhuren=@zhuren ";
                if (!ht.ContainsKey("@zhuren"))
                {
                    ht.Add("@zhuren", obj.Zhuren);
                }
            }
            if (obj.Producer != null && obj.Producer.Trim().Length > 0)
            {
                term += " and a.producer=@producer ";
                if (!ht.ContainsKey("@producer"))
                {
                    ht.Add("@producer", obj.Producer);
                }
            }
            if (obj.Startpublication != null && obj.Startpublication.Trim().Length > 0)
            {
                term += " and a.startpublication=@startpublication ";
                if (!ht.ContainsKey("@startpublication"))
                {
                    ht.Add("@startpublication", obj.Startpublication);
                }
            }
            if (obj.Publishdate != null && obj.Publishdate.Trim().Length > 0)
            {
                term += " and a.publishdate=@publishdate ";
                if (!ht.ContainsKey("@publishdate"))
                {
                    ht.Add("@publishdate", obj.Publishdate);
                }
            }
            if (obj.Telephoneexchange != null && obj.Telephoneexchange.Trim().Length > 0)
            {
                term += " and a.telephoneexchange=@telephoneexchange ";
                if (!ht.ContainsKey("@telephoneexchange"))
                {
                    ht.Add("@telephoneexchange", obj.Telephoneexchange);
                }
            }
            if (obj.Fax != null && obj.Fax.Trim().Length > 0)
            {
                term += " and a.fax=@fax ";
                if (!ht.ContainsKey("@fax"))
                {
                    ht.Add("@fax", obj.Fax);
                }
            }
            if (obj.Addressone != null && obj.Addressone.Trim().Length > 0)
            {
                term += " and a.addressone=@addressone ";
                if (!ht.ContainsKey("@addressone"))
                {
                    ht.Add("@addressone", obj.Addressone);
                }
            }
            if (obj.Addresstwo != null && obj.Addresstwo.Trim().Length > 0)
            {
                term += " and a.addresstwo=@addresstwo ";
                if (!ht.ContainsKey("@addresstwo"))
                {
                    ht.Add("@addresstwo", obj.Addresstwo);
                }
            }
            if (obj.Webaddress != null && obj.Webaddress.Trim().Length > 0)
            {
                term += " and a.webaddress=@webaddress ";
                if (!ht.ContainsKey("@webaddress"))
                {
                    ht.Add("@webaddress", obj.Webaddress);
                }
            }
            if (obj.Issn != null && obj.Issn.Trim().Length > 0)
            {
                term += " and a.issn=@issn ";
                if (!ht.ContainsKey("@issn"))
                {
                    ht.Add("@issn", obj.Issn);
                }
            }
            if (obj.Cooperate != null && obj.Cooperate.Trim().Length > 0)
            {
                term += " and a.cooperate=@cooperate ";
                if (!ht.ContainsKey("@cooperate"))
                {
                    ht.Add("@cooperate", obj.Cooperate);
                }
            }
            if (obj.Circulation > 0)//Circulation
            {
                term += " and a.circulation=@circulation ";
                if (!ht.ContainsKey("@circulation"))
                {
                    ht.Add("@circulation", obj.Circulation);
                }
            }
            if (obj.Publishchannels != null && obj.Publishchannels.Trim().Length > 0)
            {
                term += " and a.publishchannels=@publishchannels ";
                if (!ht.ContainsKey("@publishchannels"))
                {
                    ht.Add("@publishchannels", obj.Publishchannels);
                }
            }
            if (obj.Phoneone != null && obj.Phoneone.Trim().Length > 0)
            {
                term += " and a.phoneone=@phoneone ";
                if (!ht.ContainsKey("@phoneone"))
                {
                    ht.Add("@phoneone", obj.Phoneone);
                }
            }
            if (obj.Phonetwo != null && obj.Phonetwo.Trim().Length > 0)
            {
                term += " and a.phonetwo=@phonetwo ";
                if (!ht.ContainsKey("@phonetwo"))
                {
                    ht.Add("@phonetwo", obj.Phonetwo);
                }
            }
            if (obj.Endpublication != null && obj.Endpublication.Trim().Length > 0)
            {
                term += " and a.endpublication=@endpublication ";
                if (!ht.ContainsKey("@endpublication"))
                {
                    ht.Add("@endpublication", obj.Endpublication);
                }
            }
            if (obj.Adsphone != null && obj.Adsphone.Trim().Length > 0)
            {
                term += " and a.adsphone=@adsphone ";
                if (!ht.ContainsKey("@adsphone"))
                {
                    ht.Add("@adsphone", obj.Adsphone);
                }
            }
            if (obj.Adsprice > 0)//AdsPrice
            {
                term += " and a.adsprice=@adsprice ";
                if (!ht.ContainsKey("@adsprice"))
                {
                    ht.Add("@adsprice", obj.Adsprice);
                }
            }
            if (obj.Medialogo != null && obj.Medialogo.Trim().Length > 0)
            {
                term += " and a.medialogo=@medialogo ";
                if (!ht.ContainsKey("@medialogo"))
                {
                    ht.Add("@medialogo", obj.Medialogo);
                }
            }
            if (obj.Briefing > 0)//Briefing
            {
                term += " and a.briefing=@briefing ";
                if (!ht.ContainsKey("@briefing"))
                {
                    ht.Add("@briefing", obj.Briefing);
                }
            }
            if (obj.Mediaintro != null && obj.Mediaintro.Trim().Length > 0)
            {
                term += " and a.mediaintro=@mediaintro ";
                if (!ht.ContainsKey("@mediaintro"))
                {
                    ht.Add("@mediaintro", obj.Mediaintro);
                }
            }
            if (obj.Engintro != null && obj.Engintro.Trim().Length > 0)
            {
                term += " and a.engintro=@engintro ";
                if (!ht.ContainsKey("@engintro"))
                {
                    ht.Add("@engintro", obj.Engintro);
                }
            }
            if (obj.Remarks != null && obj.Remarks.Trim().Length > 0)
            {
                term += " and a.remarks=@remarks ";
                if (!ht.ContainsKey("@remarks"))
                {
                    ht.Add("@remarks", obj.Remarks);
                }
            }
            if (obj.Channelname != null && obj.Channelname.Trim().Length > 0)
            {
                term += " and a.channelname=@channelname ";
                if (!ht.ContainsKey("@channelname"))
                {
                    ht.Add("@channelname", obj.Channelname);
                }
            }
            if (obj.Dabfm != null && obj.Dabfm.Trim().Length > 0)
            {
                term += " and a.dabfm=@dabfm ";
                if (!ht.ContainsKey("@dabfm"))
                {
                    ht.Add("@dabfm", obj.Dabfm);
                }
            }
            if (obj.Topicname != null && obj.Topicname.Trim().Length > 0)
            {
                term += " and a.topicname=@topicname ";
                if (!ht.ContainsKey("@topicname"))
                {
                    ht.Add("@topicname", obj.Topicname);
                }
            }
            if (obj.Topicproperty > 0)//TopicProperty
            {
                term += " and a.topicproperty=@topicproperty ";
                if (!ht.ContainsKey("@topicproperty"))
                {
                    ht.Add("@topicproperty", obj.Topicproperty);
                }
            }
            if (obj.Overriderange != null && obj.Overriderange.Trim().Length > 0)
            {
                term += " and a.overriderange=@overriderange ";
                if (!ht.ContainsKey("@overriderange"))
                {
                    ht.Add("@overriderange", obj.Overriderange);
                }
            }
            if (obj.Rating != null && obj.Rating.Trim().Length > 0)
            {
                term += " and a.rating=@rating ";
                if (!ht.ContainsKey("@rating"))
                {
                    ht.Add("@rating", obj.Rating);
                }
            }
            if (obj.Topictime != null && obj.Topictime.Trim().Length > 0)
            {
                term += " and a.topictime=@topictime ";
                if (!ht.ContainsKey("@topictime"))
                {
                    ht.Add("@topictime", obj.Topictime);
                }
            }
            if (obj.Channelwebaddress != null && obj.Channelwebaddress.Trim().Length > 0)
            {
                term += " and a.channelwebaddress=@channelwebaddress ";
                if (!ht.ContainsKey("@channelwebaddress"))
                {
                    ht.Add("@channelwebaddress", obj.Channelwebaddress);
                }
            }
            if (obj.Countryid > 0)//国家id
            {
                term += " and a.countryid=@countryid ";
                if (!ht.ContainsKey("@countryid"))
                {
                    ht.Add("@countryid", obj.Countryid);
                }
            }
            if (obj.Provinceid > 0)//省id
            {
                term += " and a.provinceid=@provinceid ";
                if (!ht.ContainsKey("@provinceid"))
                {
                    ht.Add("@provinceid", obj.Provinceid);
                }
            }
            if (obj.Cityid > 0)//城市id
            {
                term += " and a.cityid=@cityid ";
                if (!ht.ContainsKey("@cityid"))
                {
                    ht.Add("@cityid", obj.Cityid);
                }
            }
            if (obj.Del > 0)//删除标记
            {
                term += " and a.del=@del ";
                if (!ht.ContainsKey("@del"))
                {
                    ht.Add("@del", obj.Del);
                }
            }
            if (obj.Mediatype != null && obj.Mediatype.Trim().Length > 0)
            {
                term += " and a.mediatype=@mediatype ";
                if (!ht.ContainsKey("@mediatype"))
                {
                    ht.Add("@mediatype", obj.Mediatype);
                }
            }
            if (obj.Addr1_provinceid > 0)//地址1省id
            {
                term += " and a.addr1_provinceid=@addr1_provinceid ";
                if (!ht.ContainsKey("@addr1_provinceid"))
                {
                    ht.Add("@addr1_provinceid", obj.Addr1_provinceid);
                }
            }
            if (obj.Addr1_cityid > 0)//地址1城市id
            {
                term += " and a.addr1_cityid=@addr1_cityid ";
                if (!ht.ContainsKey("@addr1_cityid"))
                {
                    ht.Add("@addr1_cityid", obj.Addr1_cityid);
                }
            }
            if (obj.Addr1_countryid > 0)//地址1国家id
            {
                term += " and a.addr1_countryid=@addr1_countryid ";
                if (!ht.ContainsKey("@addr1_countryid"))
                {
                    ht.Add("@addr1_countryid", obj.Addr1_countryid);
                }
            }
            if (obj.Addr2_provinceid > 0)//地址2省id
            {
                term += " and a.addr2_provinceid=@addr2_provinceid ";
                if (!ht.ContainsKey("@addr2_provinceid"))
                {
                    ht.Add("@addr2_provinceid", obj.Addr2_provinceid);
                }
            }
            if (obj.Addr2_cityid > 0)//地址2城市id
            {
                term += " and a.addr2_cityid=@addr2_cityid ";
                if (!ht.ContainsKey("@addr2_cityid"))
                {
                    ht.Add("@addr2_cityid", obj.Addr2_cityid);
                }
            }
            if (obj.Addr2_countryid > 0)//地址2国家id
            {
                term += " and a.addr2_countryid=@addr2_countryid ";
                if (!ht.ContainsKey("@addr2_countryid"))
                {
                    ht.Add("@addr2_countryid", obj.Addr2_countryid);
                }
            }
            if (obj.Postcode != null && obj.Postcode.Trim().Length > 0)
            {
                term += " and a.postcode=@postcode ";
                if (!ht.ContainsKey("@postcode"))
                {
                    ht.Add("@postcode", obj.Postcode);
                }
            }
            if (obj.Regionattribute > 0)//RegionAttribute
            {
                term += " and a.regionattribute=@regionattribute ";
                if (!ht.ContainsKey("@regionattribute"))
                {
                    ht.Add("@regionattribute", obj.Regionattribute);
                }
            }
            if (obj.Override_countryid > 0)//Override_Countryid
            {
                term += " and a.override_countryid=@override_countryid ";
                if (!ht.ContainsKey("@override_countryid"))
                {
                    ht.Add("@override_countryid", obj.Override_countryid);
                }
            }
            if (obj.Override_provinceid > 0)//Override_Provinceid
            {
                term += " and a.override_provinceid=@override_provinceid ";
                if (!ht.ContainsKey("@override_provinceid"))
                {
                    ht.Add("@override_provinceid", obj.Override_provinceid);
                }
            }
            if (obj.Override_cityid > 0)//Override_Cityid
            {
                term += " and a.override_cityid=@override_cityid ";
                if (!ht.ContainsKey("@override_cityid"))
                {
                    ht.Add("@override_cityid", obj.Override_cityid);
                }
            }
            if (obj.Override_describe != null && obj.Override_describe.Trim().Length > 0)
            {
                term += " and a.override_describe=@override_describe ";
                if (!ht.ContainsKey("@override_describe"))
                {
                    ht.Add("@override_describe", obj.Override_describe);
                }
            }
            if (obj.Industryid > 0)//IndustryID
            {
                term += " and a.industryid=@industryid ";
                if (!ht.ContainsKey("@industryid"))
                {
                    ht.Add("@industryid", obj.Industryid);
                }
            }
            if (obj.Issueregion != null && obj.Issueregion.Trim().Length > 0)
            {
                term += " and a.issueregion=@issueregion ";
                if (!ht.ContainsKey("@issueregion"))
                {
                    ht.Add("@issueregion", obj.Issueregion);
                }
            }
            if (obj.Branchs != null && obj.Branchs.Trim().Length > 0)
            {
                term += " and a.branchs=@branchs ";
                if (!ht.ContainsKey("@branchs"))
                {
                    ht.Add("@branchs", obj.Branchs);
                }
            }
            if (obj.Publishperiods != null && obj.Publishperiods.Trim().Length > 0)
            {
                term += " and a.publishperiods=@publishperiods ";
                if (!ht.ContainsKey("@publishperiods"))
                {
                    ht.Add("@publishperiods", obj.Publishperiods);
                }
            }
            return term;
        }
        //得到查询字符串
        private static string getQueryString(string front, string columns, string LinkTable, string terms)
        {
            if (front == null)
            {
                front = string.Empty;
            }
            if (columns == null)
            {
                columns = string.Empty;
            }
            else
            {
                columns = "," + columns;
            }
            columns = columns.TrimEnd(',');
            if (LinkTable == null)
            {
                LinkTable = string.Empty;
            }
            if (terms == null)
            {
                terms = string.Empty;
            }
            if (terms != null && terms.Trim().Length > 0)
            {
                if (!terms.Trim().StartsWith("and"))
                {
                    terms = " and " + terms;
                }
            }
            string sql = @"select {0} a.id as id,a.version as version,a.mediaitemid as mediaitemid,a.mediacname as mediacname,a.mediaename as mediaename,a.cshortname as cshortname,a.eshortname as eshortname,a.mediaitemtype as mediaitemtype,a.currentversion as currentversion,a.status as status,a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,a.lastmodifieddate as lastmodifieddate,a.lastmodifiedip as lastmodifiedip,a.mediumsort as mediumsort,a.readersort as readersort,a.governingbody as governingbody,a.frontfor as frontfor,a.proprieter as proprieter,a.subproprieter as subproprieter,a.chiefeditor as chiefeditor,a.admineditor as admineditor,a.subeditor as subeditor,a.manager as manager,a.zhuren as zhuren,a.producer as producer,a.startpublication as startpublication,a.publishdate as publishdate,a.telephoneexchange as telephoneexchange,a.fax as fax,a.addressone as addressone,a.addresstwo as addresstwo,a.webaddress as webaddress,a.issn as issn,a.cooperate as cooperate,a.circulation as circulation,a.publishchannels as publishchannels,a.phoneone as phoneone,a.phonetwo as phonetwo,a.endpublication as endpublication,a.adsphone as adsphone,a.adsprice as adsprice,a.medialogo as medialogo,a.briefing as briefing,a.mediaintro as mediaintro,a.engintro as engintro,a.remarks as remarks,a.channelname as channelname,a.dabfm as dabfm,a.topicname as topicname,a.topicproperty as topicproperty,a.overriderange as overriderange,a.rating as rating,a.topictime as topictime,a.channelwebaddress as channelwebaddress,a.countryid as countryid,a.provinceid as provinceid,a.cityid as cityid,a.del as del,a.mediatype as mediatype,a.addr1_provinceid as addr1_provinceid,a.addr1_cityid as addr1_cityid,a.addr1_countryid as addr1_countryid,a.addr2_provinceid as addr2_provinceid,a.addr2_cityid as addr2_cityid,a.addr2_countryid as addr2_countryid,a.postcode as postcode,a.regionattribute as regionattribute,a.override_countryid as override_countryid,a.override_provinceid as override_provinceid,a.override_cityid as override_cityid,a.override_describe as override_describe,a.industryid as industryid,a.issueregion as issueregion,a.branchs as branchs,a.publishperiods as publishperiods {1} from media_mediaitemshist as a {2} where 1=1 {3} ";
            return string.Format(sql, front, columns, LinkTable, terms);
        }

        private static string getQueryString(string front, ArrayList columns, string LinkTable, string terms)
        {
            if (columns == null)
            {
                columns = new ArrayList();
            }
            string col = string.Empty;
            if (columns.Count > 0)
            {
                col += ",";
                for (int i = 0; i < columns.Count; i++)
                {
                    col += columns[i].ToString();
                }
            }
            col = col.TrimEnd(',');
            return getQueryString(front, col, LinkTable, terms);
        }

        public static DataTable QueryInfo(string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para =ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfo(terms, para);
        }
        
        public static DataTable QueryInfo(string terms, Hashtable ht, SqlTransaction trans)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfo(trans, terms, para);
        }
        
        public static DataTable QueryInfo(string terms, params SqlParameter[] param)
        {
            DataTable dt = null;
            string front = " distinct ";
            string columns = null;
            string LinkTable = null;
            string sql = getQueryString(front, columns, LinkTable, terms);
            if (param != null && param.Length > 0)
            {
                dt = clsSelect.QueryBySql(sql, param);
            }
            else
            {
                dt = clsSelect.QueryBySql(sql);
            }
            return dt;
        }

        public static DataTable QueryInfo(SqlTransaction trans, string terms, params SqlParameter[] param)
        {
            DataTable dt = null;
            string front = " distinct ";
            string columns = null;
            string LinkTable = null;
            string sql = getQueryString(front, columns, LinkTable, terms);
            if (param != null && param.Length > 0)
            {
                dt = clsSelect.QueryBySql(trans, sql, param);
            }
            else
            {
                dt = clsSelect.QueryBySql(sql, trans);
            }
            return dt;
        }

        public static DataTable QueryInfoByObj(MediaitemshistInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }

        public static DataTable QueryInfoByObj(MediaitemshistInfo obj, string terms, params SqlParameter[] param)
        {
            if (terms == null)
            {
                terms = string.Empty;
            }
            Hashtable ht = new Hashtable();
            string temp = getQueryTerms(obj, ref ht);
            if (temp != null && temp.Trim().Length > 0)
            {
                terms += temp;
            }
            if (param != null && param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (!ht.ContainsKey(param[i].ParameterName))
                    {
                        ht.Add(param[i].ParameterName, param[i].Value);
                    }
                }
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfo(terms, para);
        }

        #endregion
        #region load
        public static MediaitemshistInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static MediaitemshistInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }


        public static MediaitemshistInfo setObject(DataRow dr)
        {
            MediaitemshistInfo obj = new MediaitemshistInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("version") && dr["version"] != DBNull.Value)//版本号
            {
                obj.Version = Convert.ToInt32(dr["version"]);
            }
            if (dr.Table.Columns.Contains("mediaitemid") && dr["mediaitemid"] != DBNull.Value)//媒体ID
            {
                obj.Mediaitemid = Convert.ToInt32(dr["mediaitemid"]);
            }
            if (dr.Table.Columns.Contains("mediacname") && dr["mediacname"] != DBNull.Value)//媒体中文名称
            {
                obj.Mediacname = (dr["mediacname"]).ToString();
            }
            if (dr.Table.Columns.Contains("mediaename") && dr["mediaename"] != DBNull.Value)//媒体英文名称
            {
                obj.Mediaename = (dr["mediaename"]).ToString();
            }
            if (dr.Table.Columns.Contains("cshortname") && dr["cshortname"] != DBNull.Value)//媒体中文简称
            {
                obj.Cshortname = (dr["cshortname"]).ToString();
            }
            if (dr.Table.Columns.Contains("eshortname") && dr["eshortname"] != DBNull.Value)//媒体英文简称
            {
                obj.Eshortname = (dr["eshortname"]).ToString();
            }
            if (dr.Table.Columns.Contains("mediaitemtype") && dr["mediaitemtype"] != DBNull.Value)//媒体类型1平面2网络3电视4广播
            {
                obj.Mediaitemtype = Convert.ToInt32(dr["mediaitemtype"]);
            }
            if (dr.Table.Columns.Contains("currentversion") && dr["currentversion"] != DBNull.Value)//媒体当前版本
            {
                obj.Currentversion = Convert.ToInt32(dr["currentversion"]);
            }
            if (dr.Table.Columns.Contains("status") && dr["status"] != DBNull.Value)//状态
            {
                obj.Status = Convert.ToInt32(dr["status"]);
            }
            if (dr.Table.Columns.Contains("createdbyuserid") && dr["createdbyuserid"] != DBNull.Value)//创建用户id
            {
                obj.Createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }
            if (dr.Table.Columns.Contains("createdip") && dr["createdip"] != DBNull.Value)//创建用户ip
            {
                obj.Createdip = (dr["createdip"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建日期
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastmodifiedbyuserid") && dr["lastmodifiedbyuserid"] != DBNull.Value)//修改id
            {
                obj.Lastmodifiedbyuserid = Convert.ToInt32(dr["lastmodifiedbyuserid"]);
            }
            if (dr.Table.Columns.Contains("lastmodifieddate") && dr["lastmodifieddate"] != DBNull.Value)//修改日期
            {
                obj.Lastmodifieddate = (dr["lastmodifieddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastmodifiedip") && dr["lastmodifiedip"] != DBNull.Value)//修改用户的ip
            {
                obj.Lastmodifiedip = (dr["lastmodifiedip"]).ToString();
            }
            if (dr.Table.Columns.Contains("mediumsort") && dr["mediumsort"] != DBNull.Value)//形态属性
            {
                obj.Mediumsort = (dr["mediumsort"]).ToString();
            }
            if (dr.Table.Columns.Contains("readersort") && dr["readersort"] != DBNull.Value)//受众属性
            {
                obj.Readersort = (dr["readersort"]).ToString();
            }
            if (dr.Table.Columns.Contains("governingbody") && dr["governingbody"] != DBNull.Value)//主管（所属）单位
            {
                obj.Governingbody = (dr["governingbody"]).ToString();
            }
            if (dr.Table.Columns.Contains("frontfor") && dr["frontfor"] != DBNull.Value)//主办单位
            {
                obj.Frontfor = (dr["frontfor"]).ToString();
            }
            if (dr.Table.Columns.Contains("proprieter") && dr["proprieter"] != DBNull.Value)//社长
            {
                obj.Proprieter = (dr["proprieter"]).ToString();
            }
            if (dr.Table.Columns.Contains("subproprieter") && dr["subproprieter"] != DBNull.Value)//副社长
            {
                obj.Subproprieter = (dr["subproprieter"]).ToString();
            }
            if (dr.Table.Columns.Contains("chiefeditor") && dr["chiefeditor"] != DBNull.Value)//总编
            {
                obj.Chiefeditor = (dr["chiefeditor"]).ToString();
            }
            if (dr.Table.Columns.Contains("admineditor") && dr["admineditor"] != DBNull.Value)//执行总编
            {
                obj.Admineditor = (dr["admineditor"]).ToString();
            }
            if (dr.Table.Columns.Contains("subeditor") && dr["subeditor"] != DBNull.Value)//副总编
            {
                obj.Subeditor = (dr["subeditor"]).ToString();
            }
            if (dr.Table.Columns.Contains("manager") && dr["manager"] != DBNull.Value)//台长
            {
                obj.Manager = (dr["manager"]).ToString();
            }
            if (dr.Table.Columns.Contains("zhuren") && dr["zhuren"] != DBNull.Value)//主任
            {
                obj.Zhuren = (dr["zhuren"]).ToString();
            }
            if (dr.Table.Columns.Contains("producer") && dr["producer"] != DBNull.Value)//制片人
            {
                obj.Producer = (dr["producer"]).ToString();
            }
            if (dr.Table.Columns.Contains("startpublication") && dr["startpublication"] != DBNull.Value)//创刊日期
            {
                obj.Startpublication = (dr["startpublication"]).ToString();
            }
            if (dr.Table.Columns.Contains("publishdate") && dr["publishdate"] != DBNull.Value)//发行日期
            {
                obj.Publishdate = (dr["publishdate"]).ToString();
            }
            if (dr.Table.Columns.Contains("telephoneexchange") && dr["telephoneexchange"] != DBNull.Value)//总机
            {
                obj.Telephoneexchange = (dr["telephoneexchange"]).ToString();
            }
            if (dr.Table.Columns.Contains("fax") && dr["fax"] != DBNull.Value)//传真
            {
                obj.Fax = (dr["fax"]).ToString();
            }
            if (dr.Table.Columns.Contains("addressone") && dr["addressone"] != DBNull.Value)//地址1
            {
                obj.Addressone = (dr["addressone"]).ToString();
            }
            if (dr.Table.Columns.Contains("addresstwo") && dr["addresstwo"] != DBNull.Value)//地址2
            {
                obj.Addresstwo = (dr["addresstwo"]).ToString();
            }
            if (dr.Table.Columns.Contains("webaddress") && dr["webaddress"] != DBNull.Value)//网址
            {
                obj.Webaddress = (dr["webaddress"]).ToString();
            }
            if (dr.Table.Columns.Contains("issn") && dr["issn"] != DBNull.Value)//刊号
            {
                obj.Issn = (dr["issn"]).ToString();
            }
            if (dr.Table.Columns.Contains("cooperate") && dr["cooperate"] != DBNull.Value)//合作方式
            {
                obj.Cooperate = (dr["cooperate"]).ToString();
            }
            if (dr.Table.Columns.Contains("circulation") && dr["circulation"] != DBNull.Value)//发行量
            {
                obj.Circulation = Convert.ToInt32(dr["circulation"]);
            }
            if (dr.Table.Columns.Contains("publishchannels") && dr["publishchannels"] != DBNull.Value)//发行渠道
            {
                obj.Publishchannels = (dr["publishchannels"]).ToString();
            }
            if (dr.Table.Columns.Contains("phoneone") && dr["phoneone"] != DBNull.Value)//热线1
            {
                obj.Phoneone = (dr["phoneone"]).ToString();
            }
            if (dr.Table.Columns.Contains("phonetwo") && dr["phonetwo"] != DBNull.Value)//热线2
            {
                obj.Phonetwo = (dr["phonetwo"]).ToString();
            }
            if (dr.Table.Columns.Contains("endpublication") && dr["endpublication"] != DBNull.Value)//截稿日期
            {
                obj.Endpublication = (dr["endpublication"]).ToString();
            }
            if (dr.Table.Columns.Contains("adsphone") && dr["adsphone"] != DBNull.Value)//广告部电话
            {
                obj.Adsphone = (dr["adsphone"]).ToString();
            }
            if (dr.Table.Columns.Contains("adsprice") && dr["adsprice"] != DBNull.Value)//广告报价
            {
                obj.Adsprice = Convert.ToInt32(dr["adsprice"]);
            }
            if (dr.Table.Columns.Contains("medialogo") && dr["medialogo"] != DBNull.Value)//媒体LOGO
            {
                obj.Medialogo = (dr["medialogo"]).ToString();
            }
            if (dr.Table.Columns.Contains("briefing") && dr["briefing"] != DBNull.Value)//剪报
            {
                obj.Briefing = Convert.ToInt32(dr["briefing"]);
            }
            if (dr.Table.Columns.Contains("mediaintro") && dr["mediaintro"] != DBNull.Value)//媒体简介
            {
                obj.Mediaintro = (dr["mediaintro"]).ToString();
            }
            if (dr.Table.Columns.Contains("engintro") && dr["engintro"] != DBNull.Value)//英文简介
            {
                obj.Engintro = (dr["engintro"]).ToString();
            }
            if (dr.Table.Columns.Contains("remarks") && dr["remarks"] != DBNull.Value)//备注
            {
                obj.Remarks = (dr["remarks"]).ToString();
            }
            if (dr.Table.Columns.Contains("channelname") && dr["channelname"] != DBNull.Value)//频道名称
            {
                obj.Channelname = (dr["channelname"]).ToString();
            }
            if (dr.Table.Columns.Contains("dabfm") && dr["dabfm"] != DBNull.Value)// 调频（FM103.9）
            {
                obj.Dabfm = (dr["dabfm"]).ToString();
            }
            if (dr.Table.Columns.Contains("topicname") && dr["topicname"] != DBNull.Value)//栏目名称
            {
                obj.Topicname = (dr["topicname"]).ToString();
            }
            if (dr.Table.Columns.Contains("topicproperty") && dr["topicproperty"] != DBNull.Value)//栏目类型
            {
                obj.Topicproperty = Convert.ToInt32(dr["topicproperty"]);
            }
            if (dr.Table.Columns.Contains("overriderange") && dr["overriderange"] != DBNull.Value)//覆盖范围
            {
                obj.Overriderange = (dr["overriderange"]).ToString();
            }
            if (dr.Table.Columns.Contains("rating") && dr["rating"] != DBNull.Value)//近期收视率
            {
                obj.Rating = (dr["rating"]).ToString();
            }
            if (dr.Table.Columns.Contains("topictime") && dr["topictime"] != DBNull.Value)//栏目播出时长
            {
                obj.Topictime = (dr["topictime"]).ToString();
            }
            if (dr.Table.Columns.Contains("channelwebaddress") && dr["channelwebaddress"] != DBNull.Value)//频道网址
            {
                obj.Channelwebaddress = (dr["channelwebaddress"]).ToString();
            }
            if (dr.Table.Columns.Contains("countryid") && dr["countryid"] != DBNull.Value)//国家id
            {
                obj.Countryid = Convert.ToInt32(dr["countryid"]);
            }
            if (dr.Table.Columns.Contains("provinceid") && dr["provinceid"] != DBNull.Value)//省id
            {
                obj.Provinceid = Convert.ToInt32(dr["provinceid"]);
            }
            if (dr.Table.Columns.Contains("cityid") && dr["cityid"] != DBNull.Value)//城市id
            {
                obj.Cityid = Convert.ToInt32(dr["cityid"]);
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//删除标记
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("mediatype") && dr["mediatype"] != DBNull.Value)//媒体类别
            {
                obj.Mediatype = (dr["mediatype"]).ToString();
            }
            if (dr.Table.Columns.Contains("addr1_provinceid") && dr["addr1_provinceid"] != DBNull.Value)//地址1省id
            {
                obj.Addr1_provinceid = Convert.ToInt32(dr["addr1_provinceid"]);
            }
            if (dr.Table.Columns.Contains("addr1_cityid") && dr["addr1_cityid"] != DBNull.Value)//地址1城市id
            {
                obj.Addr1_cityid = Convert.ToInt32(dr["addr1_cityid"]);
            }
            if (dr.Table.Columns.Contains("addr1_countryid") && dr["addr1_countryid"] != DBNull.Value)//地址1国家id
            {
                obj.Addr1_countryid = Convert.ToInt32(dr["addr1_countryid"]);
            }
            if (dr.Table.Columns.Contains("addr2_provinceid") && dr["addr2_provinceid"] != DBNull.Value)//地址2省id
            {
                obj.Addr2_provinceid = Convert.ToInt32(dr["addr2_provinceid"]);
            }
            if (dr.Table.Columns.Contains("addr2_cityid") && dr["addr2_cityid"] != DBNull.Value)//地址2城市id
            {
                obj.Addr2_cityid = Convert.ToInt32(dr["addr2_cityid"]);
            }
            if (dr.Table.Columns.Contains("addr2_countryid") && dr["addr2_countryid"] != DBNull.Value)//地址2国家id
            {
                obj.Addr2_countryid = Convert.ToInt32(dr["addr2_countryid"]);
            }
            if (dr.Table.Columns.Contains("postcode") && dr["postcode"] != DBNull.Value)//postcode
            {
                obj.Postcode = (dr["postcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("regionattribute") && dr["regionattribute"] != DBNull.Value)//地域属性
            {
                obj.Regionattribute = Convert.ToInt32(dr["regionattribute"]);
            }
            if (dr.Table.Columns.Contains("override_countryid") && dr["override_countryid"] != DBNull.Value)//override_countryid
            {
                obj.Override_countryid = Convert.ToInt32(dr["override_countryid"]);
            }
            if (dr.Table.Columns.Contains("override_provinceid") && dr["override_provinceid"] != DBNull.Value)//override_provinceid
            {
                obj.Override_provinceid = Convert.ToInt32(dr["override_provinceid"]);
            }
            if (dr.Table.Columns.Contains("override_cityid") && dr["override_cityid"] != DBNull.Value)//override_cityid
            {
                obj.Override_cityid = Convert.ToInt32(dr["override_cityid"]);
            }
            if (dr.Table.Columns.Contains("override_describe") && dr["override_describe"] != DBNull.Value)//覆盖范围描述
            {
                obj.Override_describe = (dr["override_describe"]).ToString();
            }
            if (dr.Table.Columns.Contains("industryid") && dr["industryid"] != DBNull.Value)//industryid
            {
                obj.Industryid = Convert.ToInt32(dr["industryid"]);
            }
            if (dr.Table.Columns.Contains("issueregion") && dr["issueregion"] != DBNull.Value)//issueregion
            {
                obj.Issueregion = (dr["issueregion"]).ToString();
            }
            if (dr.Table.Columns.Contains("branchs") && dr["branchs"] != DBNull.Value)//分部
            {
                obj.Branchs = (dr["branchs"]).ToString();
            }
            if (dr.Table.Columns.Contains("publishperiods") && dr["publishperiods"] != DBNull.Value)//publishperiods
            {
                obj.Publishperiods = (dr["publishperiods"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}
