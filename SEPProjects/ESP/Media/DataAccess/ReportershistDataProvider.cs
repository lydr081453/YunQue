using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;
namespace ESP.Media.DataAccess
{
    public class ReportershistDataProvider
    {
        #region 构造函数
        public ReportershistDataProvider()
        {
        }
        #endregion
        #region 插入
        //插入字符串
        private static string strinsert(ReportershistInfo obj, ref List<SqlParameter> ht)
        {
            if (ht == null)
            {
                ht = new List<SqlParameter>();
            }
            string sql = @"insert into media_reportershist (version,reporterid,currentversion,sn,status,createdbyuserid,createdip,createddate,lastmodifiedbyuserid,lastmodifiedip,lastmodifieddate,reportername,penname,sex,birthday,cardnumber,media_id,attention,reporterlevel,reporterposition,tel_o,tel_h,address_h,postcode_h,character,hobby,marriage,writing,family,usualmobile,backupmobile,fax,qq,msn,emailone,emailtwo,emailthree,unittype,unitname,photo,experience,education,del,cityid,bankname,responsibledomain,paytype,bankcardcode,bankcardname,bankacountname,writingfee,referral,haveinvoice,paystatus,uploadstarttime,uploadendtime,paymentmode,privateremark,cooperatecircs,hometown,othermessagesoftware,remark,cityname) values (@version,@reporterid,@currentversion,@sn,@status,@createdbyuserid,@createdip,@createddate,@lastmodifiedbyuserid,@lastmodifiedip,@lastmodifieddate,@reportername,@penname,@sex,@birthday,@cardnumber,@media_id,@attention,@reporterlevel,@reporterposition,@tel_o,@tel_h,@address_h,@postcode_h,@character,@hobby,@marriage,@writing,@family,@usualmobile,@backupmobile,@fax,@qq,@msn,@emailone,@emailtwo,@emailthree,@unittype,@unitname,@photo,@experience,@education,@del,@cityid,@bankname,@responsibledomain,@paytype,@bankcardcode,@bankcardname,@bankacountname,@writingfee,@referral,@haveinvoice,@paystatus,@uploadstarttime,@uploadendtime,@paymentmode,@privateremark,@cooperatecircs,@hometown,@othermessagesoftware,@remark,@cityname);select @@IDENTITY as rowNum;";
            SqlParameter param_Version = new SqlParameter("@Version", SqlDbType.Int, 4);
            param_Version.Value = obj.Version;
            ht.Add(param_Version);
            SqlParameter param_Reporterid = new SqlParameter("@Reporterid", SqlDbType.Int, 4);
            param_Reporterid.Value = obj.Reporterid;
            ht.Add(param_Reporterid);
            SqlParameter param_Currentversion = new SqlParameter("@Currentversion", SqlDbType.Int, 4);
            param_Currentversion.Value = obj.Currentversion;
            ht.Add(param_Currentversion);
            SqlParameter param_Sn = new SqlParameter("@Sn", SqlDbType.NVarChar, 100);
            param_Sn.Value = obj.Sn;
            ht.Add(param_Sn);
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
            SqlParameter param_Lastmodifiedip = new SqlParameter("@Lastmodifiedip", SqlDbType.NVarChar, 100);
            param_Lastmodifiedip.Value = obj.Lastmodifiedip;
            ht.Add(param_Lastmodifiedip);
            SqlParameter param_Lastmodifieddate = new SqlParameter("@Lastmodifieddate", SqlDbType.DateTime, 8);
            param_Lastmodifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Lastmodifieddate);
            ht.Add(param_Lastmodifieddate);
            SqlParameter param_Reportername = new SqlParameter("@Reportername", SqlDbType.NVarChar, 100);
            param_Reportername.Value = obj.Reportername;
            ht.Add(param_Reportername);
            SqlParameter param_Penname = new SqlParameter("@Penname", SqlDbType.NVarChar, 100);
            param_Penname.Value = obj.Penname;
            ht.Add(param_Penname);
            SqlParameter param_Sex = new SqlParameter("@Sex", SqlDbType.SmallInt, 2);
            param_Sex.Value = obj.Sex;
            ht.Add(param_Sex);
            SqlParameter param_Birthday = new SqlParameter("@Birthday", SqlDbType.DateTime, 8);
            param_Birthday.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Birthday);
            ht.Add(param_Birthday);
            SqlParameter param_Cardnumber = new SqlParameter("@Cardnumber", SqlDbType.NVarChar, 100);
            param_Cardnumber.Value = obj.Cardnumber;
            ht.Add(param_Cardnumber);
            SqlParameter param_id = new SqlParameter("@media_id", SqlDbType.Int, 4);
            param_id.Value = obj.Media_id;
            ht.Add(param_id);
            SqlParameter param_Attention = new SqlParameter("@Attention", SqlDbType.NVarChar, 100);
            param_Attention.Value = obj.Attention;
            ht.Add(param_Attention);
            SqlParameter param_Reporterlevel = new SqlParameter("@Reporterlevel", SqlDbType.NVarChar, 100);
            param_Reporterlevel.Value = obj.Reporterlevel;
            ht.Add(param_Reporterlevel);
            SqlParameter param_Reporterposition = new SqlParameter("@Reporterposition", SqlDbType.NVarChar, 100);
            param_Reporterposition.Value = obj.Reporterposition;
            ht.Add(param_Reporterposition);
            SqlParameter param_Tel_o = new SqlParameter("@Tel_o", SqlDbType.NVarChar, 200);
            param_Tel_o.Value = obj.Tel_o;
            ht.Add(param_Tel_o);
            SqlParameter param_Tel_h = new SqlParameter("@Tel_h", SqlDbType.NVarChar, 100);
            param_Tel_h.Value = obj.Tel_h;
            ht.Add(param_Tel_h);
            SqlParameter param_Address_h = new SqlParameter("@Address_h", SqlDbType.NVarChar, 1000);
            param_Address_h.Value = obj.Address_h;
            ht.Add(param_Address_h);
            SqlParameter param_Postcode_h = new SqlParameter("@Postcode_h", SqlDbType.NVarChar, 100);
            param_Postcode_h.Value = obj.Postcode_h;
            ht.Add(param_Postcode_h);
            SqlParameter param_Character = new SqlParameter("@Character", SqlDbType.NVarChar, 4000);
            param_Character.Value = obj.Character;
            ht.Add(param_Character);
            SqlParameter param_Hobby = new SqlParameter("@Hobby", SqlDbType.NVarChar, 4000);
            param_Hobby.Value = obj.Hobby;
            ht.Add(param_Hobby);
            SqlParameter param_Marriage = new SqlParameter("@Marriage", SqlDbType.SmallInt, 2);
            param_Marriage.Value = obj.Marriage;
            ht.Add(param_Marriage);
            SqlParameter param_Writing = new SqlParameter("@Writing", SqlDbType.NVarChar, 4000);
            param_Writing.Value = obj.Writing;
            ht.Add(param_Writing);
            SqlParameter param_Family = new SqlParameter("@Family", SqlDbType.NVarChar, 4000);
            param_Family.Value = obj.Family;
            ht.Add(param_Family);
            SqlParameter param_Usualmobile = new SqlParameter("@Usualmobile", SqlDbType.NVarChar, 100);
            param_Usualmobile.Value = obj.Usualmobile;
            ht.Add(param_Usualmobile);
            SqlParameter param_Backupmobile = new SqlParameter("@Backupmobile", SqlDbType.NVarChar, 100);
            param_Backupmobile.Value = obj.Backupmobile;
            ht.Add(param_Backupmobile);
            SqlParameter param_Fax = new SqlParameter("@Fax", SqlDbType.NVarChar, 100);
            param_Fax.Value = obj.Fax;
            ht.Add(param_Fax);
            SqlParameter param_Qq = new SqlParameter("@Qq", SqlDbType.NVarChar, 100);
            param_Qq.Value = obj.Qq;
            ht.Add(param_Qq);
            SqlParameter param_Msn = new SqlParameter("@Msn", SqlDbType.NVarChar, 200);
            param_Msn.Value = obj.Msn;
            ht.Add(param_Msn);
            SqlParameter param_Emailone = new SqlParameter("@Emailone", SqlDbType.NVarChar, 200);
            param_Emailone.Value = obj.Emailone;
            ht.Add(param_Emailone);
            SqlParameter param_Emailtwo = new SqlParameter("@Emailtwo", SqlDbType.NVarChar, 200);
            param_Emailtwo.Value = obj.Emailtwo;
            ht.Add(param_Emailtwo);
            SqlParameter param_Emailthree = new SqlParameter("@Emailthree", SqlDbType.NVarChar, 200);
            param_Emailthree.Value = obj.Emailthree;
            ht.Add(param_Emailthree);
            SqlParameter param_Unittype = new SqlParameter("@Unittype", SqlDbType.SmallInt, 2);
            param_Unittype.Value = obj.Unittype;
            ht.Add(param_Unittype);
            SqlParameter param_Unitname = new SqlParameter("@Unitname", SqlDbType.NVarChar, 100);
            param_Unitname.Value = obj.Unitname;
            ht.Add(param_Unitname);
            SqlParameter param_Photo = new SqlParameter("@Photo", SqlDbType.NVarChar, 1000);
            param_Photo.Value = obj.Photo;
            ht.Add(param_Photo);
            SqlParameter param_Experience = new SqlParameter("@Experience", SqlDbType.NVarChar, 4000);
            param_Experience.Value = obj.Experience;
            ht.Add(param_Experience);
            SqlParameter param_Education = new SqlParameter("@Education", SqlDbType.NVarChar, 4000);
            param_Education.Value = obj.Education;
            ht.Add(param_Education);
            SqlParameter param_Del = new SqlParameter("@Del", SqlDbType.SmallInt, 2);
            param_Del.Value = obj.Del;
            ht.Add(param_Del);
            SqlParameter param_Cityid = new SqlParameter("@Cityid", SqlDbType.Int, 4);
            param_Cityid.Value = obj.Cityid;
            ht.Add(param_Cityid);
            SqlParameter param_Bankname = new SqlParameter("@Bankname", SqlDbType.NVarChar, 1000);
            param_Bankname.Value = obj.Bankname;
            ht.Add(param_Bankname);
            SqlParameter param_Responsibledomain = new SqlParameter("@Responsibledomain", SqlDbType.NVarChar, 200);
            param_Responsibledomain.Value = obj.Responsibledomain;
            ht.Add(param_Responsibledomain);
            SqlParameter param_Paytype = new SqlParameter("@Paytype", SqlDbType.Int, 4);
            param_Paytype.Value = obj.Paytype;
            ht.Add(param_Paytype);
            SqlParameter param_Bankcardcode = new SqlParameter("@Bankcardcode", SqlDbType.NVarChar, 100);
            param_Bankcardcode.Value = obj.Bankcardcode;
            ht.Add(param_Bankcardcode);
            SqlParameter param_Bankcardname = new SqlParameter("@Bankcardname", SqlDbType.NVarChar, 100);
            param_Bankcardname.Value = obj.Bankcardname;
            ht.Add(param_Bankcardname);
            SqlParameter param_Bankacountname = new SqlParameter("@Bankacountname", SqlDbType.NVarChar, 100);
            param_Bankacountname.Value = obj.Bankacountname;
            ht.Add(param_Bankacountname);
            SqlParameter param_Writingfee = new SqlParameter("@Writingfee", SqlDbType.Float, 8);
            param_Writingfee.Value = obj.Writingfee;
            ht.Add(param_Writingfee);
            SqlParameter param_Referral = new SqlParameter("@Referral", SqlDbType.NVarChar, 100);
            param_Referral.Value = obj.Referral;
            ht.Add(param_Referral);
            SqlParameter param_Haveinvoice = new SqlParameter("@Haveinvoice", SqlDbType.Int, 4);
            param_Haveinvoice.Value = obj.Haveinvoice;
            ht.Add(param_Haveinvoice);
            SqlParameter param_Paystatus = new SqlParameter("@Paystatus", SqlDbType.Int, 4);
            param_Paystatus.Value = obj.Paystatus;
            ht.Add(param_Paystatus);
            SqlParameter param_Uploadstarttime = new SqlParameter("@Uploadstarttime", SqlDbType.DateTime, 8);
            param_Uploadstarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstarttime);
            ht.Add(param_Uploadstarttime);
            SqlParameter param_Uploadendtime = new SqlParameter("@Uploadendtime", SqlDbType.DateTime, 8);
            param_Uploadendtime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadendtime);
            ht.Add(param_Uploadendtime);
            SqlParameter param_Paymentmode = new SqlParameter("@Paymentmode", SqlDbType.Int, 4);
            param_Paymentmode.Value = obj.Paymentmode;
            ht.Add(param_Paymentmode);
            SqlParameter param_Privateremark = new SqlParameter("@Privateremark", SqlDbType.NVarChar, 1000);
            param_Privateremark.Value = obj.Privateremark;
            ht.Add(param_Privateremark);
            SqlParameter param_Cooperatecircs = new SqlParameter("@Cooperatecircs", SqlDbType.NVarChar, 1000);
            param_Cooperatecircs.Value = obj.Cooperatecircs;
            ht.Add(param_Cooperatecircs);
            SqlParameter param_Hometown = new SqlParameter("@Hometown", SqlDbType.NVarChar, 100);
            param_Hometown.Value = obj.Hometown;
            ht.Add(param_Hometown);
            SqlParameter param_Othermessagesoftware = new SqlParameter("@Othermessagesoftware", SqlDbType.NVarChar, 100);
            param_Othermessagesoftware.Value = obj.Othermessagesoftware;
            ht.Add(param_Othermessagesoftware);
            SqlParameter param_Remark = new SqlParameter("@Remark", SqlDbType.NVarChar, 2000);
            param_Remark.Value = obj.Remark;
            ht.Add(param_Remark);
            SqlParameter param_CityName = new SqlParameter("@cityname", SqlDbType.NVarChar, 50);
            param_CityName.Value = obj.CityName;
            ht.Add(param_CityName);
            return sql;
        }


        //插入一条记录
        public static int insertinfo(ReportershistInfo obj, SqlTransaction trans)
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
                if(rowNum > 0)
                    ESP.Logging.Logger.Add("Save a new reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);
                else
                    ESP.Logging.Logger.Add("Save a new reporter history is failed.", "Media system", ESP.Logging.LogLevel.Information); 
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("Save a new reporter history is error.", "Media system", ESP.Logging.LogLevel.Information, ex); 
            }
            return rowNum;
        }


        //插入一条记录
        public static int insertinfo(ReportershistInfo obj)
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
                    ESP.Logging.Logger.Add("Save a new reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add("Save a new reporter history is error.", "Media system", ESP.Logging.LogLevel.Information, ex); 
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
            string sql = "delete media_reportershist where id=@id";
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
            param.Value = id;
            try
            {
                rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                if (rows > 0)
                {
                    ESP.Logging.Logger.Add("delete a reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("delete a reporter history is error.", "Media system", ESP.Logging.LogLevel.Information, ex);
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
                        ESP.Logging.Logger.Add("delete a reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);
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
                    ESP.Logging.Logger.Add("delete a reporter history is error.", "Media system", ESP.Logging.LogLevel.Information, ex);
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
        public static string getUpdateString(ReportershistInfo objTerm, ReportershistInfo Objupdate, string term, ref List<SqlParameter> ht, params SqlParameter[] param)
        {
            string sql = "update media_reportershist set version=@version,reporterid=@reporterid,currentversion=@currentversion,sn=@sn,status=@status,createdbyuserid=@createdbyuserid,createdip=@createdip,createddate=@createddate,lastmodifiedbyuserid=@lastmodifiedbyuserid,lastmodifiedip=@lastmodifiedip,lastmodifieddate=@lastmodifieddate,reportername=@reportername,penname=@penname,sex=@sex,birthday=@birthday,cardnumber=@cardnumber,media_id=@media_id,attention=@attention,reporterlevel=@reporterlevel,reporterposition=@reporterposition,tel_o=@tel_o,tel_h=@tel_h,address_h=@address_h,postcode_h=@postcode_h,character=@character,hobby=@hobby,marriage=@marriage,writing=@writing,family=@family,usualmobile=@usualmobile,backupmobile=@backupmobile,fax=@fax,qq=@qq,msn=@msn,emailone=@emailone,emailtwo=@emailtwo,emailthree=@emailthree,unittype=@unittype,unitname=@unitname,photo=@photo,experience=@experience,education=@education,del=@del,cityid=@cityid,bankname=@bankname,responsibledomain=@responsibledomain,paytype=@paytype,bankcardcode=@bankcardcode,bankcardname=@bankcardname,bankacountname=@bankacountname,writingfee=@writingfee,referral=@referral,haveinvoice=@haveinvoice,paystatus=@paystatus,uploadstarttime=@uploadstarttime,uploadendtime=@uploadendtime,paymentmode=@paymentmode,privateremark=@privateremark,cooperatecircs=@cooperatecircs,hometown=@hometown,othermessagesoftware=@othermessagesoftware,remark=@remark,cityname=@cityname where 1=1 ";
            SqlParameter param_version = new SqlParameter("@version", SqlDbType.Int, 4);
            param_version.Value = Objupdate.Version;
            ht.Add(param_version);
            SqlParameter param_reporterid = new SqlParameter("@reporterid", SqlDbType.Int, 4);
            param_reporterid.Value = Objupdate.Reporterid;
            ht.Add(param_reporterid);
            SqlParameter param_currentversion = new SqlParameter("@currentversion", SqlDbType.Int, 4);
            param_currentversion.Value = Objupdate.Currentversion;
            ht.Add(param_currentversion);
            SqlParameter param_sn = new SqlParameter("@sn", SqlDbType.NVarChar, 100);
            param_sn.Value = Objupdate.Sn;
            ht.Add(param_sn);
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
            SqlParameter param_lastmodifiedip = new SqlParameter("@lastmodifiedip", SqlDbType.NVarChar, 100);
            param_lastmodifiedip.Value = Objupdate.Lastmodifiedip;
            ht.Add(param_lastmodifiedip);
            SqlParameter param_lastmodifieddate = new SqlParameter("@lastmodifieddate", SqlDbType.DateTime, 8);
            param_lastmodifieddate.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Lastmodifieddate);
            ht.Add(param_lastmodifieddate);
            SqlParameter param_reportername = new SqlParameter("@reportername", SqlDbType.NVarChar, 100);
            param_reportername.Value = Objupdate.Reportername;
            ht.Add(param_reportername);
            SqlParameter param_penname = new SqlParameter("@penname", SqlDbType.NVarChar, 100);
            param_penname.Value = Objupdate.Penname;
            ht.Add(param_penname);
            SqlParameter param_sex = new SqlParameter("@sex", SqlDbType.SmallInt, 2);
            param_sex.Value = Objupdate.Sex;
            ht.Add(param_sex);
            SqlParameter param_birthday = new SqlParameter("@birthday", SqlDbType.DateTime, 8);
            param_birthday.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Birthday);
            ht.Add(param_birthday);
            SqlParameter param_cardnumber = new SqlParameter("@cardnumber", SqlDbType.NVarChar, 100);
            param_cardnumber.Value = Objupdate.Cardnumber;
            ht.Add(param_cardnumber);
            SqlParameter param_id = new SqlParameter("@media_id", SqlDbType.Int, 4);
            param_id.Value = Objupdate.Media_id;
            ht.Add(param_id);
            SqlParameter param_attention = new SqlParameter("@attention", SqlDbType.NVarChar, 100);
            param_attention.Value = Objupdate.Attention;
            ht.Add(param_attention);
            SqlParameter param_reporterlevel = new SqlParameter("@reporterlevel", SqlDbType.NVarChar, 100);
            param_reporterlevel.Value = Objupdate.Reporterlevel;
            ht.Add(param_reporterlevel);
            SqlParameter param_reporterposition = new SqlParameter("@reporterposition", SqlDbType.NVarChar, 100);
            param_reporterposition.Value = Objupdate.Reporterposition;
            ht.Add(param_reporterposition);
            SqlParameter param_tel_o = new SqlParameter("@tel_o", SqlDbType.NVarChar, 200);
            param_tel_o.Value = Objupdate.Tel_o;
            ht.Add(param_tel_o);
            SqlParameter param_tel_h = new SqlParameter("@tel_h", SqlDbType.NVarChar, 100);
            param_tel_h.Value = Objupdate.Tel_h;
            ht.Add(param_tel_h);
            SqlParameter param_address_h = new SqlParameter("@address_h", SqlDbType.NVarChar, 1000);
            param_address_h.Value = Objupdate.Address_h;
            ht.Add(param_address_h);
            SqlParameter param_postcode_h = new SqlParameter("@postcode_h", SqlDbType.NVarChar, 100);
            param_postcode_h.Value = Objupdate.Postcode_h;
            ht.Add(param_postcode_h);
            SqlParameter param_character = new SqlParameter("@character", SqlDbType.NVarChar, 4000);
            param_character.Value = Objupdate.Character;
            ht.Add(param_character);
            SqlParameter param_hobby = new SqlParameter("@hobby", SqlDbType.NVarChar, 4000);
            param_hobby.Value = Objupdate.Hobby;
            ht.Add(param_hobby);
            SqlParameter param_marriage = new SqlParameter("@marriage", SqlDbType.SmallInt, 2);
            param_marriage.Value = Objupdate.Marriage;
            ht.Add(param_marriage);
            SqlParameter param_writing = new SqlParameter("@writing", SqlDbType.NVarChar, 4000);
            param_writing.Value = Objupdate.Writing;
            ht.Add(param_writing);
            SqlParameter param_family = new SqlParameter("@family", SqlDbType.NVarChar, 4000);
            param_family.Value = Objupdate.Family;
            ht.Add(param_family);
            SqlParameter param_usualmobile = new SqlParameter("@usualmobile", SqlDbType.NVarChar, 100);
            param_usualmobile.Value = Objupdate.Usualmobile;
            ht.Add(param_usualmobile);
            SqlParameter param_backupmobile = new SqlParameter("@backupmobile", SqlDbType.NVarChar, 100);
            param_backupmobile.Value = Objupdate.Backupmobile;
            ht.Add(param_backupmobile);
            SqlParameter param_fax = new SqlParameter("@fax", SqlDbType.NVarChar, 100);
            param_fax.Value = Objupdate.Fax;
            ht.Add(param_fax);
            SqlParameter param_qq = new SqlParameter("@qq", SqlDbType.NVarChar, 100);
            param_qq.Value = Objupdate.Qq;
            ht.Add(param_qq);
            SqlParameter param_msn = new SqlParameter("@msn", SqlDbType.NVarChar, 200);
            param_msn.Value = Objupdate.Msn;
            ht.Add(param_msn);
            SqlParameter param_emailone = new SqlParameter("@emailone", SqlDbType.NVarChar, 200);
            param_emailone.Value = Objupdate.Emailone;
            ht.Add(param_emailone);
            SqlParameter param_emailtwo = new SqlParameter("@emailtwo", SqlDbType.NVarChar, 200);
            param_emailtwo.Value = Objupdate.Emailtwo;
            ht.Add(param_emailtwo);
            SqlParameter param_emailthree = new SqlParameter("@emailthree", SqlDbType.NVarChar, 200);
            param_emailthree.Value = Objupdate.Emailthree;
            ht.Add(param_emailthree);
            SqlParameter param_unittype = new SqlParameter("@unittype", SqlDbType.SmallInt, 2);
            param_unittype.Value = Objupdate.Unittype;
            ht.Add(param_unittype);
            SqlParameter param_unitname = new SqlParameter("@unitname", SqlDbType.NVarChar, 100);
            param_unitname.Value = Objupdate.Unitname;
            ht.Add(param_unitname);
            SqlParameter param_photo = new SqlParameter("@photo", SqlDbType.NVarChar, 1000);
            param_photo.Value = Objupdate.Photo;
            ht.Add(param_photo);
            SqlParameter param_experience = new SqlParameter("@experience", SqlDbType.NVarChar, 4000);
            param_experience.Value = Objupdate.Experience;
            ht.Add(param_experience);
            SqlParameter param_education = new SqlParameter("@education", SqlDbType.NVarChar, 4000);
            param_education.Value = Objupdate.Education;
            ht.Add(param_education);
            SqlParameter param_del = new SqlParameter("@del", SqlDbType.SmallInt, 2);
            param_del.Value = Objupdate.Del;
            ht.Add(param_del);
            SqlParameter param_cityid = new SqlParameter("@cityid", SqlDbType.Int, 4);
            param_cityid.Value = Objupdate.Cityid;
            ht.Add(param_cityid);
            SqlParameter param_bankname = new SqlParameter("@bankname", SqlDbType.NVarChar, 1000);
            param_bankname.Value = Objupdate.Bankname;
            ht.Add(param_bankname);
            SqlParameter param_responsibledomain = new SqlParameter("@responsibledomain", SqlDbType.NVarChar, 200);
            param_responsibledomain.Value = Objupdate.Responsibledomain;
            ht.Add(param_responsibledomain);
            SqlParameter param_paytype = new SqlParameter("@paytype", SqlDbType.Int, 4);
            param_paytype.Value = Objupdate.Paytype;
            ht.Add(param_paytype);
            SqlParameter param_bankcardcode = new SqlParameter("@bankcardcode", SqlDbType.NVarChar, 100);
            param_bankcardcode.Value = Objupdate.Bankcardcode;
            ht.Add(param_bankcardcode);
            SqlParameter param_bankcardname = new SqlParameter("@bankcardname", SqlDbType.NVarChar, 100);
            param_bankcardname.Value = Objupdate.Bankcardname;
            ht.Add(param_bankcardname);
            SqlParameter param_bankacountname = new SqlParameter("@bankacountname", SqlDbType.NVarChar, 100);
            param_bankacountname.Value = Objupdate.Bankacountname;
            ht.Add(param_bankacountname);
            SqlParameter param_writingfee = new SqlParameter("@writingfee", SqlDbType.Float, 8);
            param_writingfee.Value = Objupdate.Writingfee;
            ht.Add(param_writingfee);
            SqlParameter param_referral = new SqlParameter("@referral", SqlDbType.NVarChar, 100);
            param_referral.Value = Objupdate.Referral;
            ht.Add(param_referral);
            SqlParameter param_haveinvoice = new SqlParameter("@haveinvoice", SqlDbType.Int, 4);
            param_haveinvoice.Value = Objupdate.Haveinvoice;
            ht.Add(param_haveinvoice);
            SqlParameter param_paystatus = new SqlParameter("@paystatus", SqlDbType.Int, 4);
            param_paystatus.Value = Objupdate.Paystatus;
            ht.Add(param_paystatus);
            SqlParameter param_uploadstarttime = new SqlParameter("@uploadstarttime", SqlDbType.DateTime, 8);
            param_uploadstarttime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadstarttime);
            ht.Add(param_uploadstarttime);
            SqlParameter param_uploadendtime = new SqlParameter("@uploadendtime", SqlDbType.DateTime, 8);
            param_uploadendtime.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(Objupdate.Uploadendtime);
            ht.Add(param_uploadendtime);
            SqlParameter param_paymentmode = new SqlParameter("@paymentmode", SqlDbType.Int, 4);
            param_paymentmode.Value = Objupdate.Paymentmode;
            ht.Add(param_paymentmode);
            SqlParameter param_privateremark = new SqlParameter("@privateremark", SqlDbType.NVarChar, 1000);
            param_privateremark.Value = Objupdate.Privateremark;
            ht.Add(param_privateremark);
            SqlParameter param_cooperatecircs = new SqlParameter("@cooperatecircs", SqlDbType.NVarChar, 1000);
            param_cooperatecircs.Value = Objupdate.Cooperatecircs;
            ht.Add(param_cooperatecircs);
            SqlParameter param_hometown = new SqlParameter("@hometown", SqlDbType.NVarChar, 100);
            param_hometown.Value = Objupdate.Hometown;
            ht.Add(param_hometown);
            SqlParameter param_othermessagesoftware = new SqlParameter("@othermessagesoftware", SqlDbType.NVarChar, 100);
            param_othermessagesoftware.Value = Objupdate.Othermessagesoftware;
            ht.Add(param_othermessagesoftware);
            SqlParameter param_remark = new SqlParameter("@remark", SqlDbType.NVarChar, 2000);
            param_remark.Value = Objupdate.Remark;
            ht.Add(param_remark);

            SqlParameter param_CityName = new SqlParameter("@cityname", SqlDbType.NVarChar, 50);
            param_CityName.Value = Objupdate.CityName;
            ht.Add(param_CityName);
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
        public static bool updateInfo(SqlTransaction trans, ReportershistInfo objterm, ReportershistInfo Objupdate, string term, params SqlParameter[] param)
        {
            if (Objupdate == null)
            {
                return false;
            }
            List<SqlParameter> ht = new List<SqlParameter>();
            string sql = getUpdateString(objterm, Objupdate, term, ref ht, param);
            SqlParameter[] para = ht.ToArray();
            try
            {
                int rows = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, para);
                if (rows >= 0)
                {
                    ESP.Logging.Logger.Add("update a reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);
                    return true;
                }
                ESP.Logging.Logger.Add("update a reporter history is failed.", "Media system", ESP.Logging.LogLevel.Information);
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("update a reporter history is error.", "Media system", ESP.Logging.LogLevel.Information, ex);
            }
            return false;
        }

        //更新操作
        public static bool updateInfo(ReportershistInfo objterm, ReportershistInfo Objupdate, string term, params SqlParameter[] param)
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
                    ESP.Logging.Logger.Add("update a reporter history is error.", "Media system", ESP.Logging.LogLevel.Information, ex);
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            if (rowNum >= 0)
            {
                ESP.Logging.Logger.Add("update a reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);
                return true;
            }
            ESP.Logging.Logger.Add("update a reporter history is failed.", "Media system", ESP.Logging.LogLevel.Information);
            return false;
        }

        private static string getTerms(ReportershistInfo obj, ref List<SqlParameter> ht)
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
            if (obj.Reporterid > 0)//ReporterID
            {
                term += " and reporterid=@reporterid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reporterid") == -1)
                {
                    SqlParameter p = new SqlParameter("@reporterid", SqlDbType.Int, 4);
                    p.Value = obj.Reporterid;
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
            if (obj.Sn != null && obj.Sn.Trim().Length > 0)
            {
                term += " and sn=@sn ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@sn") == -1)
                {
                    SqlParameter p = new SqlParameter("@sn", SqlDbType.NVarChar, 100);
                    p.Value = obj.Sn;
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
            if (obj.Reportername != null && obj.Reportername.Trim().Length > 0)
            {
                term += " and reportername=@reportername ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reportername") == -1)
                {
                    SqlParameter p = new SqlParameter("@reportername", SqlDbType.NVarChar, 100);
                    p.Value = obj.Reportername;
                    ht.Add(p);
                }
            }
            if (obj.Penname != null && obj.Penname.Trim().Length > 0)
            {
                term += " and penname=@penname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@penname") == -1)
                {
                    SqlParameter p = new SqlParameter("@penname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Penname;
                    ht.Add(p);
                }
            }
            if (obj.Sex > 0)//Sex
            {
                term += " and sex=@sex ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@sex") == -1)
                {
                    SqlParameter p = new SqlParameter("@sex", SqlDbType.SmallInt, 2);
                    p.Value = obj.Sex;
                    ht.Add(p);
                }
            }
            if (obj.Birthday != null && obj.Birthday.Trim().Length > 0)
            {
                term += " and birthday=@birthday ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@birthday") == -1)
                {
                    SqlParameter p = new SqlParameter("@birthday", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Birthday);
                    ht.Add(p);
                }
            }
            if (obj.Cardnumber != null && obj.Cardnumber.Trim().Length > 0)
            {
                term += " and cardnumber=@cardnumber ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cardnumber") == -1)
                {
                    SqlParameter p = new SqlParameter("@cardnumber", SqlDbType.NVarChar, 100);
                    p.Value = obj.Cardnumber;
                    ht.Add(p);
                }
            }
            if (obj.Media_id > 0)//media_id
            {
                term += " and media_id=@media_id ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@media_id") == -1)
                {
                    SqlParameter p = new SqlParameter("@media_id", SqlDbType.Int, 4);
                    p.Value = obj.Media_id;
                    ht.Add(p);
                }
            }
            if (obj.Attention != null && obj.Attention.Trim().Length > 0)
            {
                term += " and attention=@attention ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@attention") == -1)
                {
                    SqlParameter p = new SqlParameter("@attention", SqlDbType.NVarChar, 100);
                    p.Value = obj.Attention;
                    ht.Add(p);
                }
            }
            if (obj.Reporterlevel != null && obj.Reporterlevel.Trim().Length > 0)
            {
                term += " and reporterlevel=@reporterlevel ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reporterlevel") == -1)
                {
                    SqlParameter p = new SqlParameter("@reporterlevel", SqlDbType.NVarChar, 100);
                    p.Value = obj.Reporterlevel;
                    ht.Add(p);
                }
            }
            if (obj.Reporterposition != null && obj.Reporterposition.Trim().Length > 0)
            {
                term += " and reporterposition=@reporterposition ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@reporterposition") == -1)
                {
                    SqlParameter p = new SqlParameter("@reporterposition", SqlDbType.NVarChar, 100);
                    p.Value = obj.Reporterposition;
                    ht.Add(p);
                }
            }
            if (obj.Tel_o != null && obj.Tel_o.Trim().Length > 0)
            {
                term += " and tel_o=@tel_o ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@tel_o") == -1)
                {
                    SqlParameter p = new SqlParameter("@tel_o", SqlDbType.NVarChar, 200);
                    p.Value = obj.Tel_o;
                    ht.Add(p);
                }
            }
            if (obj.Tel_h != null && obj.Tel_h.Trim().Length > 0)
            {
                term += " and tel_h=@tel_h ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@tel_h") == -1)
                {
                    SqlParameter p = new SqlParameter("@tel_h", SqlDbType.NVarChar, 100);
                    p.Value = obj.Tel_h;
                    ht.Add(p);
                }
            }
            if (obj.Address_h != null && obj.Address_h.Trim().Length > 0)
            {
                term += " and address_h=@address_h ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@address_h") == -1)
                {
                    SqlParameter p = new SqlParameter("@address_h", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Address_h;
                    ht.Add(p);
                }
            }
            if (obj.Postcode_h != null && obj.Postcode_h.Trim().Length > 0)
            {
                term += " and postcode_h=@postcode_h ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@postcode_h") == -1)
                {
                    SqlParameter p = new SqlParameter("@postcode_h", SqlDbType.NVarChar, 100);
                    p.Value = obj.Postcode_h;
                    ht.Add(p);
                }
            }
            if (obj.Character != null && obj.Character.Trim().Length > 0)
            {
                term += " and character=@character ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@character") == -1)
                {
                    SqlParameter p = new SqlParameter("@character", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Character;
                    ht.Add(p);
                }
            }
            if (obj.Hobby != null && obj.Hobby.Trim().Length > 0)
            {
                term += " and hobby=@hobby ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@hobby") == -1)
                {
                    SqlParameter p = new SqlParameter("@hobby", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Hobby;
                    ht.Add(p);
                }
            }
            if (obj.Marriage > 0)//Marriage
            {
                term += " and marriage=@marriage ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@marriage") == -1)
                {
                    SqlParameter p = new SqlParameter("@marriage", SqlDbType.SmallInt, 2);
                    p.Value = obj.Marriage;
                    ht.Add(p);
                }
            }
            if (obj.Writing != null && obj.Writing.Trim().Length > 0)
            {
                term += " and writing=@writing ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@writing") == -1)
                {
                    SqlParameter p = new SqlParameter("@writing", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Writing;
                    ht.Add(p);
                }
            }
            if (obj.Family != null && obj.Family.Trim().Length > 0)
            {
                term += " and family=@family ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@family") == -1)
                {
                    SqlParameter p = new SqlParameter("@family", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Family;
                    ht.Add(p);
                }
            }
            if (obj.Usualmobile != null && obj.Usualmobile.Trim().Length > 0)
            {
                term += " and usualmobile=@usualmobile ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@usualmobile") == -1)
                {
                    SqlParameter p = new SqlParameter("@usualmobile", SqlDbType.NVarChar, 100);
                    p.Value = obj.Usualmobile;
                    ht.Add(p);
                }
            }
            if (obj.Backupmobile != null && obj.Backupmobile.Trim().Length > 0)
            {
                term += " and backupmobile=@backupmobile ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@backupmobile") == -1)
                {
                    SqlParameter p = new SqlParameter("@backupmobile", SqlDbType.NVarChar, 100);
                    p.Value = obj.Backupmobile;
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
            if (obj.Qq != null && obj.Qq.Trim().Length > 0)
            {
                term += " and qq=@qq ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@qq") == -1)
                {
                    SqlParameter p = new SqlParameter("@qq", SqlDbType.NVarChar, 100);
                    p.Value = obj.Qq;
                    ht.Add(p);
                }
            }
            if (obj.Msn != null && obj.Msn.Trim().Length > 0)
            {
                term += " and msn=@msn ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@msn") == -1)
                {
                    SqlParameter p = new SqlParameter("@msn", SqlDbType.NVarChar, 200);
                    p.Value = obj.Msn;
                    ht.Add(p);
                }
            }
            if (obj.Emailone != null && obj.Emailone.Trim().Length > 0)
            {
                term += " and emailone=@emailone ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@emailone") == -1)
                {
                    SqlParameter p = new SqlParameter("@emailone", SqlDbType.NVarChar, 200);
                    p.Value = obj.Emailone;
                    ht.Add(p);
                }
            }
            if (obj.Emailtwo != null && obj.Emailtwo.Trim().Length > 0)
            {
                term += " and emailtwo=@emailtwo ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@emailtwo") == -1)
                {
                    SqlParameter p = new SqlParameter("@emailtwo", SqlDbType.NVarChar, 200);
                    p.Value = obj.Emailtwo;
                    ht.Add(p);
                }
            }
            if (obj.Emailthree != null && obj.Emailthree.Trim().Length > 0)
            {
                term += " and emailthree=@emailthree ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@emailthree") == -1)
                {
                    SqlParameter p = new SqlParameter("@emailthree", SqlDbType.NVarChar, 200);
                    p.Value = obj.Emailthree;
                    ht.Add(p);
                }
            }
            if (obj.Unittype > 0)//UnitType
            {
                term += " and unittype=@unittype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@unittype") == -1)
                {
                    SqlParameter p = new SqlParameter("@unittype", SqlDbType.SmallInt, 2);
                    p.Value = obj.Unittype;
                    ht.Add(p);
                }
            }
            if (obj.Unitname != null && obj.Unitname.Trim().Length > 0)
            {
                term += " and unitname=@unitname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@unitname") == -1)
                {
                    SqlParameter p = new SqlParameter("@unitname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Unitname;
                    ht.Add(p);
                }
            }
            if (obj.Photo != null && obj.Photo.Trim().Length > 0)
            {
                term += " and photo=@photo ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@photo") == -1)
                {
                    SqlParameter p = new SqlParameter("@photo", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Photo;
                    ht.Add(p);
                }
            }
            if (obj.Experience != null && obj.Experience.Trim().Length > 0)
            {
                term += " and experience=@experience ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@experience") == -1)
                {
                    SqlParameter p = new SqlParameter("@experience", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Experience;
                    ht.Add(p);
                }
            }
            if (obj.Education != null && obj.Education.Trim().Length > 0)
            {
                term += " and education=@education ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@education") == -1)
                {
                    SqlParameter p = new SqlParameter("@education", SqlDbType.NVarChar, 4000);
                    p.Value = obj.Education;
                    ht.Add(p);
                }
            }
            if (obj.Del > 0)//del
            {
                term += " and del=@del ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@del") == -1)
                {
                    SqlParameter p = new SqlParameter("@del", SqlDbType.SmallInt, 2);
                    p.Value = obj.Del;
                    ht.Add(p);
                }
            }
            if (obj.Cityid > 0)//城市编号
            {
                term += " and cityid=@cityid ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cityid") == -1)
                {
                    SqlParameter p = new SqlParameter("@cityid", SqlDbType.Int, 4);
                    p.Value = obj.Cityid;
                    ht.Add(p);
                }
            }
            if (obj.Bankname != null && obj.Bankname.Trim().Length > 0)
            {
                term += " and bankname=@bankname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankname", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Bankname;
                    ht.Add(p);
                }
            }
            if (obj.Responsibledomain != null && obj.Responsibledomain.Trim().Length > 0)
            {
                term += " and responsibledomain=@responsibledomain ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@responsibledomain") == -1)
                {
                    SqlParameter p = new SqlParameter("@responsibledomain", SqlDbType.NVarChar, 200);
                    p.Value = obj.Responsibledomain;
                    ht.Add(p);
                }
            }
            if (obj.Paytype > 0)//PayType
            {
                term += " and paytype=@paytype ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paytype") == -1)
                {
                    SqlParameter p = new SqlParameter("@paytype", SqlDbType.Int, 4);
                    p.Value = obj.Paytype;
                    ht.Add(p);
                }
            }
            if (obj.Bankcardcode != null && obj.Bankcardcode.Trim().Length > 0)
            {
                term += " and bankcardcode=@bankcardcode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankcardcode") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankcardcode", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankcardcode;
                    ht.Add(p);
                }
            }
            if (obj.Bankcardname != null && obj.Bankcardname.Trim().Length > 0)
            {
                term += " and bankcardname=@bankcardname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankcardname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankcardname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankcardname;
                    ht.Add(p);
                }
            }
            if (obj.Bankacountname != null && obj.Bankacountname.Trim().Length > 0)
            {
                term += " and bankacountname=@bankacountname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@bankacountname") == -1)
                {
                    SqlParameter p = new SqlParameter("@bankacountname", SqlDbType.NVarChar, 100);
                    p.Value = obj.Bankacountname;
                    ht.Add(p);
                }
            }
            if (obj.Writingfee > 0)//writingfee
            {
                term += " and writingfee=@writingfee ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@writingfee") == -1)
                {
                    SqlParameter p = new SqlParameter("@writingfee", SqlDbType.Float, 8);
                    p.Value = obj.Writingfee;
                    ht.Add(p);
                }
            }
            if (obj.Referral != null && obj.Referral.Trim().Length > 0)
            {
                term += " and referral=@referral ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@referral") == -1)
                {
                    SqlParameter p = new SqlParameter("@referral", SqlDbType.NVarChar, 100);
                    p.Value = obj.Referral;
                    ht.Add(p);
                }
            }
            if (obj.Haveinvoice > 0)//haveInvoice
            {
                term += " and haveinvoice=@haveinvoice ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@haveinvoice") == -1)
                {
                    SqlParameter p = new SqlParameter("@haveinvoice", SqlDbType.Int, 4);
                    p.Value = obj.Haveinvoice;
                    ht.Add(p);
                }
            }
            if (obj.Paystatus > 0)//paystatus
            {
                term += " and paystatus=@paystatus ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paystatus") == -1)
                {
                    SqlParameter p = new SqlParameter("@paystatus", SqlDbType.Int, 4);
                    p.Value = obj.Paystatus;
                    ht.Add(p);
                }
            }
            if (obj.Uploadstarttime != null && obj.Uploadstarttime.Trim().Length > 0)
            {
                term += " and uploadstarttime=@uploadstarttime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadstarttime") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadstarttime", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadstarttime);
                    ht.Add(p);
                }
            }
            if (obj.Uploadendtime != null && obj.Uploadendtime.Trim().Length > 0)
            {
                term += " and uploadendtime=@uploadendtime ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@uploadendtime") == -1)
                {
                    SqlParameter p = new SqlParameter("@uploadendtime", SqlDbType.DateTime, 8);
                    p.Value = ESP.Media.Access.Utilities.Common.StringToDateTime(obj.Uploadendtime);
                    ht.Add(p);
                }
            }
            if (obj.Paymentmode > 0)//付款方式
            {
                term += " and paymentmode=@paymentmode ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@paymentmode") == -1)
                {
                    SqlParameter p = new SqlParameter("@paymentmode", SqlDbType.Int, 4);
                    p.Value = obj.Paymentmode;
                    ht.Add(p);
                }
            }
            if (obj.Privateremark != null && obj.Privateremark.Trim().Length > 0)
            {
                term += " and privateremark=@privateremark ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@privateremark") == -1)
                {
                    SqlParameter p = new SqlParameter("@privateremark", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Privateremark;
                    ht.Add(p);
                }
            }
            if (obj.Cooperatecircs != null && obj.Cooperatecircs.Trim().Length > 0)
            {
                term += " and cooperatecircs=@cooperatecircs ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cooperatecircs") == -1)
                {
                    SqlParameter p = new SqlParameter("@cooperatecircs", SqlDbType.NVarChar, 1000);
                    p.Value = obj.Cooperatecircs;
                    ht.Add(p);
                }
            }
            if (obj.Hometown != null && obj.Hometown.Trim().Length > 0)
            {
                term += " and hometown=@hometown ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@hometown") == -1)
                {
                    SqlParameter p = new SqlParameter("@hometown", SqlDbType.NVarChar, 100);
                    p.Value = obj.Hometown;
                    ht.Add(p);
                }
            }
            if (obj.Othermessagesoftware != null && obj.Othermessagesoftware.Trim().Length > 0)
            {
                term += " and othermessagesoftware=@othermessagesoftware ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@othermessagesoftware") == -1)
                {
                    SqlParameter p = new SqlParameter("@othermessagesoftware", SqlDbType.NVarChar, 100);
                    p.Value = obj.Othermessagesoftware;
                    ht.Add(p);
                }
            }
            if (obj.Remark != null && obj.Remark.Trim().Length > 0)
            {
                term += " and remark=@remark ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@remark") == -1)
                {
                    SqlParameter p = new SqlParameter("@remark", SqlDbType.NVarChar, 2000);
                    p.Value = obj.Remark;
                    ht.Add(p);
                }
            }

            if (obj.CityName != null && obj.CityName.Trim().Length > 0)
            {
                term += " and cityname=@cityname ";
                if (ESP.Media.Access.Utilities.Common.Find(ht, "@cityname") == -1)
                {
                    SqlParameter p = new SqlParameter("@cityname", SqlDbType.NVarChar, 50);
                    p.Value = obj.CityName;
                    ht.Add(p);
                }
            }
            return term;
        }
        #endregion
        #region 查询
        private static string getQueryTerms(ReportershistInfo obj, ref Hashtable ht)
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
            if (obj.Reporterid > 0)//ReporterID
            {
                term += " and a.reporterid=@reporterid ";
                if (!ht.ContainsKey("@reporterid"))
                {
                    ht.Add("@reporterid", obj.Reporterid);
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
            if (obj.Sn != null && obj.Sn.Trim().Length > 0)
            {
                term += " and a.sn=@sn ";
                if (!ht.ContainsKey("@sn"))
                {
                    ht.Add("@sn", obj.Sn);
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
            if (obj.Lastmodifiedip != null && obj.Lastmodifiedip.Trim().Length > 0)
            {
                term += " and a.lastmodifiedip=@lastmodifiedip ";
                if (!ht.ContainsKey("@lastmodifiedip"))
                {
                    ht.Add("@lastmodifiedip", obj.Lastmodifiedip);
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
            if (obj.Reportername != null && obj.Reportername.Trim().Length > 0)
            {
                term += " and a.reportername=@reportername ";
                if (!ht.ContainsKey("@reportername"))
                {
                    ht.Add("@reportername", obj.Reportername);
                }
            }
            if (obj.Penname != null && obj.Penname.Trim().Length > 0)
            {
                term += " and a.penname=@penname ";
                if (!ht.ContainsKey("@penname"))
                {
                    ht.Add("@penname", obj.Penname);
                }
            }
            if (obj.Sex > 0)//Sex
            {
                term += " and a.sex=@sex ";
                if (!ht.ContainsKey("@sex"))
                {
                    ht.Add("@sex", obj.Sex);
                }
            }
            if (obj.Birthday != null && obj.Birthday.Trim().Length > 0)
            {
                term += " and a.birthday=@birthday ";
                if (!ht.ContainsKey("@birthday"))
                {
                    ht.Add("@birthday", obj.Birthday);
                }
            }
            if (obj.Cardnumber != null && obj.Cardnumber.Trim().Length > 0)
            {
                term += " and a.cardnumber=@cardnumber ";
                if (!ht.ContainsKey("@cardnumber"))
                {
                    ht.Add("@cardnumber", obj.Cardnumber);
                }
            }
            if (obj.Media_id > 0)//media_id
            {
                term += " and a.media_id=@media_id ";
                if (!ht.ContainsKey("@media_id"))
                {
                    ht.Add("@media_id", obj.Media_id);
                }
            }
            if (obj.Attention != null && obj.Attention.Trim().Length > 0)
            {
                term += " and a.attention=@attention ";
                if (!ht.ContainsKey("@attention"))
                {
                    ht.Add("@attention", obj.Attention);
                }
            }
            if (obj.Reporterlevel != null && obj.Reporterlevel.Trim().Length > 0)
            {
                term += " and a.reporterlevel=@reporterlevel ";
                if (!ht.ContainsKey("@reporterlevel"))
                {
                    ht.Add("@reporterlevel", obj.Reporterlevel);
                }
            }
            if (obj.Reporterposition != null && obj.Reporterposition.Trim().Length > 0)
            {
                term += " and a.reporterposition=@reporterposition ";
                if (!ht.ContainsKey("@reporterposition"))
                {
                    ht.Add("@reporterposition", obj.Reporterposition);
                }
            }
            if (obj.Tel_o != null && obj.Tel_o.Trim().Length > 0)
            {
                term += " and a.tel_o=@tel_o ";
                if (!ht.ContainsKey("@tel_o"))
                {
                    ht.Add("@tel_o", obj.Tel_o);
                }
            }
            if (obj.Tel_h != null && obj.Tel_h.Trim().Length > 0)
            {
                term += " and a.tel_h=@tel_h ";
                if (!ht.ContainsKey("@tel_h"))
                {
                    ht.Add("@tel_h", obj.Tel_h);
                }
            }
            if (obj.Address_h != null && obj.Address_h.Trim().Length > 0)
            {
                term += " and a.address_h=@address_h ";
                if (!ht.ContainsKey("@address_h"))
                {
                    ht.Add("@address_h", obj.Address_h);
                }
            }
            if (obj.Postcode_h != null && obj.Postcode_h.Trim().Length > 0)
            {
                term += " and a.postcode_h=@postcode_h ";
                if (!ht.ContainsKey("@postcode_h"))
                {
                    ht.Add("@postcode_h", obj.Postcode_h);
                }
            }
            if (obj.Character != null && obj.Character.Trim().Length > 0)
            {
                term += " and a.character=@character ";
                if (!ht.ContainsKey("@character"))
                {
                    ht.Add("@character", obj.Character);
                }
            }
            if (obj.Hobby != null && obj.Hobby.Trim().Length > 0)
            {
                term += " and a.hobby=@hobby ";
                if (!ht.ContainsKey("@hobby"))
                {
                    ht.Add("@hobby", obj.Hobby);
                }
            }
            if (obj.Marriage > 0)//Marriage
            {
                term += " and a.marriage=@marriage ";
                if (!ht.ContainsKey("@marriage"))
                {
                    ht.Add("@marriage", obj.Marriage);
                }
            }
            if (obj.Writing != null && obj.Writing.Trim().Length > 0)
            {
                term += " and a.writing=@writing ";
                if (!ht.ContainsKey("@writing"))
                {
                    ht.Add("@writing", obj.Writing);
                }
            }
            if (obj.Family != null && obj.Family.Trim().Length > 0)
            {
                term += " and a.family=@family ";
                if (!ht.ContainsKey("@family"))
                {
                    ht.Add("@family", obj.Family);
                }
            }
            if (obj.Usualmobile != null && obj.Usualmobile.Trim().Length > 0)
            {
                term += " and a.usualmobile=@usualmobile ";
                if (!ht.ContainsKey("@usualmobile"))
                {
                    ht.Add("@usualmobile", obj.Usualmobile);
                }
            }
            if (obj.Backupmobile != null && obj.Backupmobile.Trim().Length > 0)
            {
                term += " and a.backupmobile=@backupmobile ";
                if (!ht.ContainsKey("@backupmobile"))
                {
                    ht.Add("@backupmobile", obj.Backupmobile);
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
            if (obj.Qq != null && obj.Qq.Trim().Length > 0)
            {
                term += " and a.qq=@qq ";
                if (!ht.ContainsKey("@qq"))
                {
                    ht.Add("@qq", obj.Qq);
                }
            }
            if (obj.Msn != null && obj.Msn.Trim().Length > 0)
            {
                term += " and a.msn=@msn ";
                if (!ht.ContainsKey("@msn"))
                {
                    ht.Add("@msn", obj.Msn);
                }
            }
            if (obj.Emailone != null && obj.Emailone.Trim().Length > 0)
            {
                term += " and a.emailone=@emailone ";
                if (!ht.ContainsKey("@emailone"))
                {
                    ht.Add("@emailone", obj.Emailone);
                }
            }
            if (obj.Emailtwo != null && obj.Emailtwo.Trim().Length > 0)
            {
                term += " and a.emailtwo=@emailtwo ";
                if (!ht.ContainsKey("@emailtwo"))
                {
                    ht.Add("@emailtwo", obj.Emailtwo);
                }
            }
            if (obj.Emailthree != null && obj.Emailthree.Trim().Length > 0)
            {
                term += " and a.emailthree=@emailthree ";
                if (!ht.ContainsKey("@emailthree"))
                {
                    ht.Add("@emailthree", obj.Emailthree);
                }
            }
            if (obj.Unittype > 0)//UnitType
            {
                term += " and a.unittype=@unittype ";
                if (!ht.ContainsKey("@unittype"))
                {
                    ht.Add("@unittype", obj.Unittype);
                }
            }
            if (obj.Unitname != null && obj.Unitname.Trim().Length > 0)
            {
                term += " and a.unitname=@unitname ";
                if (!ht.ContainsKey("@unitname"))
                {
                    ht.Add("@unitname", obj.Unitname);
                }
            }
            if (obj.Photo != null && obj.Photo.Trim().Length > 0)
            {
                term += " and a.photo=@photo ";
                if (!ht.ContainsKey("@photo"))
                {
                    ht.Add("@photo", obj.Photo);
                }
            }
            if (obj.Experience != null && obj.Experience.Trim().Length > 0)
            {
                term += " and a.experience=@experience ";
                if (!ht.ContainsKey("@experience"))
                {
                    ht.Add("@experience", obj.Experience);
                }
            }
            if (obj.Education != null && obj.Education.Trim().Length > 0)
            {
                term += " and a.education=@education ";
                if (!ht.ContainsKey("@education"))
                {
                    ht.Add("@education", obj.Education);
                }
            }
            if (obj.Del > 0)//del
            {
                term += " and a.del=@del ";
                if (!ht.ContainsKey("@del"))
                {
                    ht.Add("@del", obj.Del);
                }
            }
            if (obj.Cityid > 0)//城市编号
            {
                term += " and a.cityid=@cityid ";
                if (!ht.ContainsKey("@cityid"))
                {
                    ht.Add("@cityid", obj.Cityid);
                }
            }
            if (obj.Bankname != null && obj.Bankname.Trim().Length > 0)
            {
                term += " and a.bankname=@bankname ";
                if (!ht.ContainsKey("@bankname"))
                {
                    ht.Add("@bankname", obj.Bankname);
                }
            }
            if (obj.Responsibledomain != null && obj.Responsibledomain.Trim().Length > 0)
            {
                term += " and a.responsibledomain=@responsibledomain ";
                if (!ht.ContainsKey("@responsibledomain"))
                {
                    ht.Add("@responsibledomain", obj.Responsibledomain);
                }
            }
            if (obj.Paytype > 0)//PayType
            {
                term += " and a.paytype=@paytype ";
                if (!ht.ContainsKey("@paytype"))
                {
                    ht.Add("@paytype", obj.Paytype);
                }
            }
            if (obj.Bankcardcode != null && obj.Bankcardcode.Trim().Length > 0)
            {
                term += " and a.bankcardcode=@bankcardcode ";
                if (!ht.ContainsKey("@bankcardcode"))
                {
                    ht.Add("@bankcardcode", obj.Bankcardcode);
                }
            }
            if (obj.Bankcardname != null && obj.Bankcardname.Trim().Length > 0)
            {
                term += " and a.bankcardname=@bankcardname ";
                if (!ht.ContainsKey("@bankcardname"))
                {
                    ht.Add("@bankcardname", obj.Bankcardname);
                }
            }
            if (obj.Bankacountname != null && obj.Bankacountname.Trim().Length > 0)
            {
                term += " and a.bankacountname=@bankacountname ";
                if (!ht.ContainsKey("@bankacountname"))
                {
                    ht.Add("@bankacountname", obj.Bankacountname);
                }
            }
            if (obj.Writingfee > 0)//writingfee
            {
                term += " and a.writingfee=@writingfee ";
                if (!ht.ContainsKey("@writingfee"))
                {
                    ht.Add("@writingfee", obj.Writingfee);
                }
            }
            if (obj.Referral != null && obj.Referral.Trim().Length > 0)
            {
                term += " and a.referral=@referral ";
                if (!ht.ContainsKey("@referral"))
                {
                    ht.Add("@referral", obj.Referral);
                }
            }
            if (obj.Haveinvoice > 0)//haveInvoice
            {
                term += " and a.haveinvoice=@haveinvoice ";
                if (!ht.ContainsKey("@haveinvoice"))
                {
                    ht.Add("@haveinvoice", obj.Haveinvoice);
                }
            }
            if (obj.Paystatus > 0)//paystatus
            {
                term += " and a.paystatus=@paystatus ";
                if (!ht.ContainsKey("@paystatus"))
                {
                    ht.Add("@paystatus", obj.Paystatus);
                }
            }
            if (obj.Uploadstarttime != null && obj.Uploadstarttime.Trim().Length > 0)
            {
                term += " and a.uploadstarttime=@uploadstarttime ";
                if (!ht.ContainsKey("@uploadstarttime"))
                {
                    ht.Add("@uploadstarttime", obj.Uploadstarttime);
                }
            }
            if (obj.Uploadendtime != null && obj.Uploadendtime.Trim().Length > 0)
            {
                term += " and a.uploadendtime=@uploadendtime ";
                if (!ht.ContainsKey("@uploadendtime"))
                {
                    ht.Add("@uploadendtime", obj.Uploadendtime);
                }
            }
            if (obj.Paymentmode > 0)//付款方式
            {
                term += " and a.paymentmode=@paymentmode ";
                if (!ht.ContainsKey("@paymentmode"))
                {
                    ht.Add("@paymentmode", obj.Paymentmode);
                }
            }
            if (obj.Privateremark != null && obj.Privateremark.Trim().Length > 0)
            {
                term += " and a.privateremark=@privateremark ";
                if (!ht.ContainsKey("@privateremark"))
                {
                    ht.Add("@privateremark", obj.Privateremark);
                }
            }
            if (obj.Cooperatecircs != null && obj.Cooperatecircs.Trim().Length > 0)
            {
                term += " and a.cooperatecircs=@cooperatecircs ";
                if (!ht.ContainsKey("@cooperatecircs"))
                {
                    ht.Add("@cooperatecircs", obj.Cooperatecircs);
                }
            }
            if (obj.Hometown != null && obj.Hometown.Trim().Length > 0)
            {
                term += " and a.hometown=@hometown ";
                if (!ht.ContainsKey("@hometown"))
                {
                    ht.Add("@hometown", obj.Hometown);
                }
            }
            if (obj.Othermessagesoftware != null && obj.Othermessagesoftware.Trim().Length > 0)
            {
                term += " and a.othermessagesoftware=@othermessagesoftware ";
                if (!ht.ContainsKey("@othermessagesoftware"))
                {
                    ht.Add("@othermessagesoftware", obj.Othermessagesoftware);
                }
            }
            if (obj.Remark != null && obj.Remark.Trim().Length > 0)
            {
                term += " and a.remark=@remark ";
                if (!ht.ContainsKey("@remark"))
                {
                    ht.Add("@remark", obj.Remark);
                }
            }

            if (obj.CityName != null && obj.CityName.Trim().Length > 0)
            {
                term += " and a.cityname=@cityname ";
                if (!ht.ContainsKey("@cityname"))
                {
                    ht.Add("@cityname", obj.CityName);
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
            string sql = @"select {0} a.id as id,a.version as version,a.reporterid as reporterid,a.currentversion as currentversion,a.sn as sn,a.status as status,a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,a.lastmodifiedbyuserid as lastmodifiedbyuserid,a.lastmodifiedip as lastmodifiedip,a.lastmodifieddate as lastmodifieddate,a.reportername as reportername,a.penname as penname,a.sex as sex,a.birthday as birthday,a.cardnumber as cardnumber,a.media_id as media_id,a.attention as attention,a.reporterlevel as reporterlevel,a.reporterposition as reporterposition,a.tel_o as tel_o,a.tel_h as tel_h,a.address_h as address_h,a.postcode_h as postcode_h,a.character as character,a.hobby as hobby,a.marriage as marriage,a.writing as writing,a.family as family,a.usualmobile as usualmobile,a.backupmobile as backupmobile,a.fax as fax,a.qq as qq,a.msn as msn,a.emailone as emailone,a.emailtwo as emailtwo,a.emailthree as emailthree,a.unittype as unittype,a.unitname as unitname,a.photo as photo,a.experience as experience,a.education as education,a.del as del,a.cityid as cityid,a.bankname as bankname,a.responsibledomain as responsibledomain,a.paytype as paytype,a.bankcardcode as bankcardcode,a.bankcardname as bankcardname,a.bankacountname as bankacountname,a.writingfee as writingfee,a.referral as referral,a.haveinvoice as haveinvoice,a.paystatus as paystatus,a.uploadstarttime as uploadstarttime,a.uploadendtime as uploadendtime,a.paymentmode as paymentmode,a.privateremark as privateremark,a.cooperatecircs as cooperatecircs,a.hometown as hometown,a.othermessagesoftware as othermessagesoftware,a.remark as remark,a.cityname as cityname {1} from media_reportershist as a {2} where 1=1 {3} ";
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
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
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
            try
            {
                if (param != null && param.Length > 0)
                {
                    dt = clsSelect.QueryBySql(sql, param);
                }
                else
                {
                    dt = clsSelect.QueryBySql(sql);
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("select reporters history is error.", "Media system", ESP.Logging.LogLevel.Information, ex);
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
            try
            {
                if (param != null && param.Length > 0)
                {
                    dt = clsSelect.QueryBySql(trans, sql, param);
                }
                else
                {
                    dt = clsSelect.QueryBySql(sql, trans);
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("select reporters history is error.", "Media system", ESP.Logging.LogLevel.Information, ex);
            }
            return dt;
        }

        public static DataTable QueryInfoByObj(ReportershistInfo obj, string terms, Hashtable ht)
        {
            if (ht == null)
            {
                ht = new Hashtable();
            }
            SqlParameter[] para = ESP.Media.Access.Utilities.Common.DictToSqlParam(ht);
            return QueryInfoByObj(obj, terms, para);
        }

        public static DataTable QueryInfoByObj(ReportershistInfo obj, string terms, params SqlParameter[] param)
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
        public static ReportershistInfo Load(int id)
        {
            DataTable dt = null;
            dt = QueryInfo(" and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }

        public static ReportershistInfo Load(int id, SqlTransaction trans)
        {
            DataTable dt = null;
            dt = QueryInfo(trans, " and a.id=@id ", new SqlParameter("@id", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                return setObject(dt.Rows[0]);
            }
            return null;
        }

        public static ReportershistInfo setObject(DataRow dr)
        {
            ReportershistInfo obj = new ReportershistInfo();
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//id
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("version") && dr["version"] != DBNull.Value)//版本号
            {
                obj.Version = Convert.ToInt32(dr["version"]);
            }
            if (dr.Table.Columns.Contains("reporterid") && dr["reporterid"] != DBNull.Value)//reporterid
            {
                obj.Reporterid = Convert.ToInt32(dr["reporterid"]);
            }
            if (dr.Table.Columns.Contains("currentversion") && dr["currentversion"] != DBNull.Value)//当前版本
            {
                obj.Currentversion = Convert.ToInt32(dr["currentversion"]);
            }
            if (dr.Table.Columns.Contains("sn") && dr["sn"] != DBNull.Value)//编号
            {
                obj.Sn = (dr["sn"]).ToString();
            }
            if (dr.Table.Columns.Contains("status") && dr["status"] != DBNull.Value)//状态
            {
                obj.Status = Convert.ToInt32(dr["status"]);
            }
            if (dr.Table.Columns.Contains("createdbyuserid") && dr["createdbyuserid"] != DBNull.Value)//创建用户ID
            {
                obj.Createdbyuserid = Convert.ToInt32(dr["createdbyuserid"]);
            }
            if (dr.Table.Columns.Contains("createdip") && dr["createdip"] != DBNull.Value)//创建IP
            {
                obj.Createdip = (dr["createdip"]).ToString();
            }
            if (dr.Table.Columns.Contains("createddate") && dr["createddate"] != DBNull.Value)//创建时间
            {
                obj.Createddate = (dr["createddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastmodifiedbyuserid") && dr["lastmodifiedbyuserid"] != DBNull.Value)//修改用户ID
            {
                obj.Lastmodifiedbyuserid = Convert.ToInt32(dr["lastmodifiedbyuserid"]);
            }
            if (dr.Table.Columns.Contains("lastmodifiedip") && dr["lastmodifiedip"] != DBNull.Value)//修改ip
            {
                obj.Lastmodifiedip = (dr["lastmodifiedip"]).ToString();
            }
            if (dr.Table.Columns.Contains("lastmodifieddate") && dr["lastmodifieddate"] != DBNull.Value)//修改时间
            {
                obj.Lastmodifieddate = (dr["lastmodifieddate"]).ToString();
            }
            if (dr.Table.Columns.Contains("reportername") && dr["reportername"] != DBNull.Value)//记者名
            {
                obj.Reportername = (dr["reportername"]).ToString();
            }
            if (dr.Table.Columns.Contains("penname") && dr["penname"] != DBNull.Value)//笔名
            {
                obj.Penname = (dr["penname"]).ToString();
            }
            if (dr.Table.Columns.Contains("sex") && dr["sex"] != DBNull.Value)//性别0保密1男2女
            {
                obj.Sex = Convert.ToInt32(dr["sex"]);
            }
            if (dr.Table.Columns.Contains("birthday") && dr["birthday"] != DBNull.Value)//出生日期
            {
                obj.Birthday = (dr["birthday"]).ToString();
            }
            if (dr.Table.Columns.Contains("cardnumber") && dr["cardnumber"] != DBNull.Value)//证件号码
            {
                obj.Cardnumber = (dr["cardnumber"]).ToString();
            }
            if (dr.Table.Columns.Contains("id") && dr["id"] != DBNull.Value)//媒体编号
            {
                obj.Id = Convert.ToInt32(dr["id"]);
            }
            if (dr.Table.Columns.Contains("attention") && dr["attention"] != DBNull.Value)//关注领域
            {
                obj.Attention = (dr["attention"]).ToString();
            }
            if (dr.Table.Columns.Contains("reporterlevel") && dr["reporterlevel"] != DBNull.Value)//记者类别
            {
                obj.Reporterlevel = (dr["reporterlevel"]).ToString();
            }
            if (dr.Table.Columns.Contains("reporterposition") && dr["reporterposition"] != DBNull.Value)//记者职位
            {
                obj.Reporterposition = (dr["reporterposition"]).ToString();
            }
            if (dr.Table.Columns.Contains("tel_o") && dr["tel_o"] != DBNull.Value)//办公电话
            {
                obj.Tel_o = (dr["tel_o"]).ToString();
            }
            if (dr.Table.Columns.Contains("tel_h") && dr["tel_h"] != DBNull.Value)//家庭电话
            {
                obj.Tel_h = (dr["tel_h"]).ToString();
            }
            if (dr.Table.Columns.Contains("address_h") && dr["address_h"] != DBNull.Value)//家庭住址
            {
                obj.Address_h = (dr["address_h"]).ToString();
            }
            if (dr.Table.Columns.Contains("postcode_h") && dr["postcode_h"] != DBNull.Value)//家庭邮编
            {
                obj.Postcode_h = (dr["postcode_h"]).ToString();
            }
            if (dr.Table.Columns.Contains("character") && dr["character"] != DBNull.Value)//性格特点
            {
                obj.Character = (dr["character"]).ToString();
            }
            if (dr.Table.Columns.Contains("hobby") && dr["hobby"] != DBNull.Value)//爱好
            {
                obj.Hobby = (dr["hobby"]).ToString();
            }
            if (dr.Table.Columns.Contains("marriage") && dr["marriage"] != DBNull.Value)//是否结婚0保密1未婚2结婚
            {
                obj.Marriage = Convert.ToInt32(dr["marriage"]);
            }
            if (dr.Table.Columns.Contains("writing") && dr["writing"] != DBNull.Value)//主要作品
            {
                obj.Writing = (dr["writing"]).ToString();
            }
            if (dr.Table.Columns.Contains("family") && dr["family"] != DBNull.Value)//家庭成员
            {
                obj.Family = (dr["family"]).ToString();
            }
            if (dr.Table.Columns.Contains("usualmobile") && dr["usualmobile"] != DBNull.Value)//常用手机
            {
                obj.Usualmobile = (dr["usualmobile"]).ToString();
            }
            if (dr.Table.Columns.Contains("backupmobile") && dr["backupmobile"] != DBNull.Value)//备用手机
            {
                obj.Backupmobile = (dr["backupmobile"]).ToString();
            }
            if (dr.Table.Columns.Contains("fax") && dr["fax"] != DBNull.Value)//传真
            {
                obj.Fax = (dr["fax"]).ToString();
            }
            if (dr.Table.Columns.Contains("qq") && dr["qq"] != DBNull.Value)//QQ号
            {
                obj.Qq = (dr["qq"]).ToString();
            }
            if (dr.Table.Columns.Contains("msn") && dr["msn"] != DBNull.Value)//MSN号
            {
                obj.Msn = (dr["msn"]).ToString();
            }
            if (dr.Table.Columns.Contains("emailone") && dr["emailone"] != DBNull.Value)//邮箱1
            {
                obj.Emailone = (dr["emailone"]).ToString();
            }
            if (dr.Table.Columns.Contains("emailtwo") && dr["emailtwo"] != DBNull.Value)//邮箱2
            {
                obj.Emailtwo = (dr["emailtwo"]).ToString();
            }
            if (dr.Table.Columns.Contains("emailthree") && dr["emailthree"] != DBNull.Value)//邮箱3
            {
                obj.Emailthree = (dr["emailthree"]).ToString();
            }
            if (dr.Table.Columns.Contains("unittype") && dr["unittype"] != DBNull.Value)//单位类型0媒体1非媒体
            {
                obj.Unittype = Convert.ToInt32(dr["unittype"]);
            }
            if (dr.Table.Columns.Contains("unitname") && dr["unitname"] != DBNull.Value)//单位名称
            {
                obj.Unitname = (dr["unitname"]).ToString();
            }
            if (dr.Table.Columns.Contains("photo") && dr["photo"] != DBNull.Value)//记者照片路径
            {
                obj.Photo = (dr["photo"]).ToString();
            }
            if (dr.Table.Columns.Contains("experience") && dr["experience"] != DBNull.Value)//职业经历
            {
                obj.Experience = (dr["experience"]).ToString();
            }
            if (dr.Table.Columns.Contains("education") && dr["education"] != DBNull.Value)//教育背景
            {
                obj.Education = (dr["education"]).ToString();
            }
            if (dr.Table.Columns.Contains("del") && dr["del"] != DBNull.Value)//del
            {
                obj.Del = Convert.ToInt32(dr["del"]);
            }
            if (dr.Table.Columns.Contains("cityid") && dr["cityid"] != DBNull.Value)//城市编号
            {
                obj.Cityid = Convert.ToInt32(dr["cityid"]);
            }
            if (dr.Table.Columns.Contains("bankname") && dr["bankname"] != DBNull.Value)//bankname
            {
                obj.Bankname = (dr["bankname"]).ToString();
            }
            if (dr.Table.Columns.Contains("responsibledomain") && dr["responsibledomain"] != DBNull.Value)//responsibledomain
            {
                obj.Responsibledomain = (dr["responsibledomain"]).ToString();
            }
            if (dr.Table.Columns.Contains("paytype") && dr["paytype"] != DBNull.Value)//支付类型(0刊后,1刊前)
            {
                obj.Paytype = Convert.ToInt32(dr["paytype"]);
            }
            if (dr.Table.Columns.Contains("bankcardcode") && dr["bankcardcode"] != DBNull.Value)//bankcardcode
            {
                obj.Bankcardcode = (dr["bankcardcode"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankcardname") && dr["bankcardname"] != DBNull.Value)//bankcardname
            {
                obj.Bankcardname = (dr["bankcardname"]).ToString();
            }
            if (dr.Table.Columns.Contains("bankacountname") && dr["bankacountname"] != DBNull.Value)//bankacountname
            {
                obj.Bankacountname = (dr["bankacountname"]).ToString();
            }
            if (dr.Table.Columns.Contains("writingfee") && dr["writingfee"] != DBNull.Value)//writingfee
            {
                obj.Writingfee = Convert.ToDouble(dr["writingfee"]);
            }
            if (dr.Table.Columns.Contains("referral") && dr["referral"] != DBNull.Value)//referral
            {
                obj.Referral = (dr["referral"]).ToString();
            }
            if (dr.Table.Columns.Contains("haveinvoice") && dr["haveinvoice"] != DBNull.Value)//haveinvoice
            {
                obj.Haveinvoice = Convert.ToInt32(dr["haveinvoice"]);
            }
            if (dr.Table.Columns.Contains("paystatus") && dr["paystatus"] != DBNull.Value)//paystatus
            {
                obj.Paystatus = Convert.ToInt32(dr["paystatus"]);
            }
            if (dr.Table.Columns.Contains("uploadstarttime") && dr["uploadstarttime"] != DBNull.Value)//剪报上传起始时间
            {
                obj.Uploadstarttime = (dr["uploadstarttime"]).ToString();
            }
            if (dr.Table.Columns.Contains("uploadendtime") && dr["uploadendtime"] != DBNull.Value)//剪报上传结束时间
            {
                obj.Uploadendtime = (dr["uploadendtime"]).ToString();
            }
            if (dr.Table.Columns.Contains("paymentmode") && dr["paymentmode"] != DBNull.Value)//付款方式
            {
                obj.Paymentmode = Convert.ToInt32(dr["paymentmode"]);
            }
            if (dr.Table.Columns.Contains("privateremark") && dr["privateremark"] != DBNull.Value)//privateremark
            {
                obj.Privateremark = (dr["privateremark"]).ToString();
            }
            if (dr.Table.Columns.Contains("cooperatecircs") && dr["cooperatecircs"] != DBNull.Value)//cooperatecircs
            {
                obj.Cooperatecircs = (dr["cooperatecircs"]).ToString();
            }
            if (dr.Table.Columns.Contains("hometown") && dr["hometown"] != DBNull.Value)//籍贯
            {
                obj.Hometown = (dr["hometown"]).ToString();
            }
            if (dr.Table.Columns.Contains("othermessagesoftware") && dr["othermessagesoftware"] != DBNull.Value)//othermessagesoftware
            {
                obj.Othermessagesoftware = (dr["othermessagesoftware"]).ToString();
            }
            if (dr.Table.Columns.Contains("remark") && dr["remark"] != DBNull.Value)//remark
            {
                obj.Remark = (dr["remark"]).ToString();
            }
            if (dr.Table.Columns.Contains("cityname") && dr["cityname"] != DBNull.Value)//remark
            {
                obj.CityName = (dr["cityname"]).ToString();
            }
            return obj;
        }
        #endregion
    }
}