using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
using System.Data;
using ESP.MediaLinq.Utilities;
using ESP.Data;
using ESP.MediaLinq.Entity;
using System.Reflection;
using System.Transactions;
namespace ESP.MediaLinq.BusinessLogic
{
    public class ReporterManager : BaseEntityInfo
    {
        public static int Add(ESP.MediaLinq.Entity.media_ReportersInfo info)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                mdc.AddTomedia_ReportersInfo(info);
                mdc.SaveChanges();
                return info.ReporterID;
            }
        }

        /// <summary>
        /// Adds the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Add(media_ReportersInfo obj, string filename, int userid, out string errmsg)
        {
            int ret = 0;

            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                errmsg = string.Empty;
                if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                {
                    obj.Photo = filename.ToString();
                }

                var r = from rh in mdc.media_reportersHistInfo
                        where rh.ReporterID == obj.ReporterID
                        select rh;
                int? version = r.Max(x => (int?)x.version);

                if (version == null)
                {
                    version = 0;
                }

                obj.CurrentVersion = (int)version;
                obj.del = 0;
                ret = Add(obj);
                OperateLogManager.Add((int)Global.SysOperateType.Add, (int)Global.Tables.reporters, userid);//添加记者日志
                // obj.ReporterID = ret;
                obj.LastModifiedByUserID = userid;
                obj.LastModifiedIP = obj.LastModifiedIP;
                obj.LastModifiedDate = DateTime.Now;
                SaveHist(obj, userid, (int)version);

                ESP.Logging.Logger.Add("Save a new reporter is success.", "Media system", ESP.Logging.LogLevel.Information);




            }
            return ret;
        }

        public static bool Update(ESP.MediaLinq.Entity.media_ReportersInfo info)
        {
            if (info == null)
                return false;

            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                mdc.GetObjectByKey(info.EntityKey);
                mdc.ApplyPropertyChanges("media_ReportersInfo", info);
                mdc.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Updates the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Update(media_ReportersInfo obj, string filename, int userid, out string errmsg)
        {
            int histcount = 0;
            DateTime begin=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
            DateTime end=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,23,59,59);
            DataTable dt = ESP.Media.BusinessLogic.ReportersManager.GetHistListCurrentDay(obj.ReporterID, userid,begin,end);
            histcount = dt.Rows.Count;
            int ret = 0;
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                errmsg = string.Empty;
                if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                {
                    obj.Photo = filename.ToString();
                }

                var r = from rh in mdc.media_reportersHistInfo
                        where rh.ReporterID == obj.ReporterID
                        select rh;
                int? version = r.Max(x => (int?)x.version);

                if (version == null)
                {
                    version = 0;
                }

                obj.CurrentVersion = (int)version;

                if (Update(obj))
                {
                    obj.LastModifiedByUserID = userid;
                    obj.LastModifiedIP = obj.LastModifiedIP;
                    obj.LastModifiedDate = DateTime.Now;
                    SaveHist(obj, userid, (int)version);
                    if (histcount == 0)
                        OperateLogManager.Add((int)Global.SysOperateType.Edit, (int)Global.Tables.reporters, userid);//更新记者信息

                    ESP.Logging.Logger.Add("Save a new reporter is sucess.", "Media system", ESP.Logging.LogLevel.Information);
                    ret = 1;
                }
                else
                {
                    errmsg = "修改失败!";

                    ESP.Logging.Logger.Add("Save a new reporter is failed.", "Media system", ESP.Logging.LogLevel.Information);

                    ret = -3;
                }


                return ret;
            }
        }

        public static bool Delete(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                ESP.MediaLinq.Entity.media_ReportersInfo model = mdc.media_ReportersInfo.FirstOrDefault(c => c.ReporterID == id);
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
        /// Deletes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="errmsg">The errmsg.</param>
        /// <returns></returns>
        public static int Delete(media_ReportersInfo obj, out string errmsg)
        {
            errmsg = "删除成功!";
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();

                obj.del = (int)Global.FiledStatus.Del;
                if (Update(obj))
                {
                    ESP.Logging.Logger.Add("Delete a reporter is sucess.", "Media system", ESP.Logging.LogLevel.Information);
                    return 1;
                }
                else
                {
                    errmsg = "删除失败!";
                    return -3;
                }


            }
        }

        public static int DeleteRelation(ESP.MediaLinq.Entity.media_ReportersInfo obj)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                try
                {
                    obj.Media_ID = 0;
                    mdc.SaveChanges();
                    ESP.Logging.Logger.Add("Delete a reporter relation is success.", "Media system", ESP.Logging.LogLevel.Information);
                    return 1;

                }
                catch (Exception exception)
                {
                    ESP.Logging.Logger.Add("Delete a reporter relation is error.", "Media system", ESP.Logging.LogLevel.Information, exception);
                    return -2;
                }
            }
        }

        public static IList<ESP.MediaLinq.Entity.media_ReportersInfo> GetList()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_ReportersInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_ReportersInfo> list = q.ToList();
                return list;
            }
        }

        public static DataTable GetDataTable()
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_ReportersInfo
                        select info;

                List<ESP.MediaLinq.Entity.media_ReportersInfo> list = q.ToList();
                return ListToDataSet.ToDataSets<ESP.MediaLinq.Entity.media_ReportersInfo>(list).Tables[0];
            }
        }
        public static DataTable GetListByMediaID(int mediaid)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from info in mdc.media_ReportersInfo
                        join mediaitem in mdc.media_MediaItemsInfo on info.Media_ID equals mediaitem.MediaitemID
                        where info.Media_ID == mediaid
                        select new
                        {
                            info.ReporterID,
                            reportername = info.ReporterName,
                            mediaitem.MediaCName,
                            mediaitem.ChannelName,
                            mediaitem.TopicName,
                            medianame = "",
                            sexValue = info.Sex,
                            sex = "",
                            info.ReporterPosition,
                            responsibledomain = info.ResponsibleDomain,
                            mobile = info.UsualMobile,
                            tel = info.Tel_O,
                            email = info.EmailOne

                        };

                var list = q.ToList();
                MethodInfo method = typeof(ListToDataSet).GetMethod("ToDataSets");
                MethodInfo method2 = method.MakeGenericMethod(q.ElementType);
                DataSet ds = (DataSet)method2.Invoke(null, new object[] { list });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["medianame"] = (dr.IsNull("mediacname") ? " " : dr["mediacname"].ToString()) + ""
                                        + (dr.IsNull("ChannelName") ? " " : dr["ChannelName"].ToString()) + ""
                                        + (dr.IsNull("TopicName") ? " " : dr["TopicName"].ToString());
                    dr["sex"] = GetSex(int.Parse(dr.IsNull("sexValue") ? "0" : dr["sexValue"].ToString()));

                }
                return ds.Tables[0];
            }

            //using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            //{
            //    Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
            //    var q = from info in mdc.media_ReportersInfo
            //            where info.Media_ID == mediaid
            //            select info;

            //    List<ESP.MediaLinq.Entity.media_ReportersInfo> list = q.ToList();
            //    return ListToDataSet.ToDataSets<ESP.MediaLinq.Entity.media_ReportersInfo>(list).Tables[0];
            //}
        }

        public static ESP.MediaLinq.Entity.media_ReportersInfo GetModel(int id)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                var q = from t in mdc.media_ReportersInfo where t.ReporterID == id select t;
                return mdc.media_ReportersInfo.FirstOrDefault(c => c.ReporterID == id);
            }
        }

        /// <summary>
        /// Gets the list by media.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="ht">The ht.</param>
        /// <param name="mediaId">The media id.</param>
        /// <returns></returns>
        public static DataTable GetListByMedia(string reportername, string mobile, string email, string idcard, string MediaCName, int? mediaId)
        {
            using (DbConnectionHolder holder = new DbConnectionHolder(ConnectionStringSettings))
            {
                Media2Entities mdc = holder.GetObjectContext<Media2Entities>();
                ///left join 的实现，如果是多表的级联需要从最外的两个表连起
                var q1 = mdc.media_MediaItemsInfo.GroupJoin(mdc.media_mediaTypeInfo, (mi) => mi.MediaItemType, (mt) => mt.id,
                        (mi, mtCol) => new
                        {
                            mi.TopicProperty,
                            mi.MediaCName,
                            mi.ChannelName,
                            mi.TopicName,
                            mi.MediaType,
                            mi.ReaderSort,
                            mi.MediaitemID,
                            TypeName = mtCol.FirstOrDefault().name
                        });

                var q2 = mdc.media_ReportersInfo.GroupJoin(q1, (rep) => rep.Media_ID, (mi) => mi.MediaitemID,
                    (rep, mediaCol) => new { Reporter = rep, MediaItem = mediaCol.FirstOrDefault() });

                var q3 = q2.GroupJoin(mdc.media_CityInfo, (rep) => rep.Reporter.cityid, (city) => city.City_ID,
                    (item, cityCity) => new { item.Reporter, item.MediaItem, City = cityCity.FirstOrDefault() });

                var q = from item in q3
                        where item.Reporter.del != (int)Global.FiledStatus.Del
                        select new
                        {
                            TypeName = item.MediaItem.TypeName,
                            CityName = item.City.City_Name,
                            reporterid = item.Reporter.ReporterID,
                            reportername = item.Reporter.ReporterName,
                            tel = item.Reporter.Tel_O,
                            mobile = item.Reporter.UsualMobile,
                            email = item.Reporter.EmailOne,
                            responsibledomain = item.Reporter.ResponsibleDomain,
                            medianame = item.MediaItem.MediaCName + " " + item.MediaItem.ChannelName + " " + item.MediaItem.TopicName,
                            TopicPropertyValue = item.MediaItem.TopicProperty,
                            TopicProperty = "",
                            MediaType = item.MediaItem.MediaType,
                            ReaderSort = item.MediaItem.ReaderSort,
                            item.Reporter.ReporterLevel,
                            item.Reporter.OtherMessageSoftware,
                            item.Reporter.Remark,
                            MediaitemID = (int?)item.MediaItem.MediaitemID,
                            item.Reporter.CardNumber,
                            item.Reporter.bankname,
                            item.Reporter.PayType,
                            item.Reporter.bankacountname,
                            item.Reporter.bankcardname,
                            sexValue = item.Reporter.Sex,
                            sex = "",
                            item.Reporter.BackupMobile,
                            item.Reporter.EmailTwo,
                            item.Reporter.EmailThree,
                            item.MediaItem.ChannelName,
                            item.MediaItem.TopicName,
                            item.Reporter.Media_ID,
                            item.Reporter.ReporterPosition
                        };
                if (mediaId != null)
                {
                    q = q.Where(x => x.Media_ID == mediaId);
                }
                if (!string.IsNullOrEmpty(reportername))
                {
                    q = q.Where(x => x.reportername.Contains(reportername));
                }
                if (!string.IsNullOrEmpty(mobile))
                {
                    q = q.Where(x => x.mobile.Contains(mobile) || x.BackupMobile.Contains(mobile));
                }
                if (!string.IsNullOrEmpty(email))
                {
                    q = q.Where(x => x.email.Contains(email) || x.EmailTwo.Contains(email) || x.EmailThree.Contains(email));
                }
                if (!string.IsNullOrEmpty(idcard))
                {
                    q = q.Where(x => x.CardNumber.Contains(idcard));
                }
                if (!string.IsNullOrEmpty(MediaCName))
                {
                    q = q.Where(x => x.medianame.Contains(MediaCName) || x.ChannelName.Contains(MediaCName) || x.TopicName.Contains(MediaCName));
                }


                var list = q.ToList();

                //foreach (var item in list)
                //{
                //    item.TopicProperty = GetTopicProperty(item.TopicPropertyValue);
                //    item.sex = GetSex(item.sexValue);
                //}

                MethodInfo method = typeof(ListToDataSet).GetMethod("ToDataSets");
                MethodInfo method2 = method.MakeGenericMethod(q.ElementType);
                DataSet ds = (DataSet)method2.Invoke(null, new object[] { list });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["TopicProperty"] = GetTopicProperty(int.Parse(dr.IsNull("TopicPropertyValue") ? "0" : dr["TopicPropertyValue"].ToString()));
                    dr["sex"] = GetSex(int.Parse(dr.IsNull("sexValue") ? "0" : dr["sexValue"].ToString()));

                }

                return ds.Tables[0];
            }
        }


        public static string GetTopicProperty(int? tp)
        {
            if (tp == null)
            {
                return "无";
            }
            string name = "";
            switch (tp.ToString())
            {
                case "1":
                    name = "新闻"; break;
                case "2":
                    name = "访谈"; break;
                case "3":
                    name = "娱乐"; break;
                case "4":
                    name = "体育"; break;
                case "5":
                    name = "教育"; break;
                default:
                    name = "无"; break;

            }
            return name;
        }

        private static string GetSex(int? sex)
        {
            if (sex == null)
            {
                return "保密";
            }
            string name = "";
            switch (sex.ToString())
            {
                case "1":
                    name = "男"; break;
                case "2":
                    name = "女"; break;
                default:
                    name = "无"; break;
            }
            return name;
        }

        /// <summary>
        /// Saves the hist.
        /// </summary>
        /// <param name="trans">The trans.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="userid">The userid.</param>
        private static void SaveHist(media_ReportersInfo obj, int userid, int Version)
        {
            media_reportersHistInfo hist = new media_reportersHistInfo();
            hist.LastModifiedByUserID = userid;
            hist.LastModifiedIP = obj.LastModifiedIP;
            hist.LastModifiedDate = DateTime.Now;
            //基本信息
            hist.ReporterID = obj.ReporterID;
            hist.ReporterName = obj.ReporterName;
            hist.PenName = obj.PenName;
            hist.Sex = obj.Sex;
            hist.Birthday = obj.Birthday;
            hist.Postcode_H = obj.Postcode_H;
            hist.CardNumber = obj.CardNumber;
            hist.Address_H = obj.Address_H;
            //联系信息
            hist.Tel_O = obj.Tel_O;
            hist.Tel_H = obj.Tel_H;
            hist.UsualMobile = obj.UsualMobile;
            hist.BackupMobile = obj.BackupMobile;
            hist.Fax = obj.Fax;
            hist.QQ = obj.QQ;
            hist.MSN = obj.MSN;
            hist.EmailOne = obj.EmailOne;
            hist.EmailTwo = obj.EmailTwo;
            hist.EmailThree = obj.EmailThree;
            //个人信息
            hist.Attention = obj.Attention;
            hist.Hobby = obj.Hobby;
            hist.Character = obj.Character;
            hist.Marriage = obj.Marriage;
            hist.Family = obj.Family;
            hist.Writing = obj.Writing;
            //教育信息
            hist.Education = obj.Education;
            //照片
            hist.Photo = obj.Photo;
            //工作信息
            hist.Experience = obj.Experience;
            hist.hometown = obj.hometown;
            hist.CreatedByUserID = (int)obj.LastModifiedByUserID;
            hist.CreatedDate = (DateTime)obj.LastModifiedDate;
            hist.CreatedIP = obj.LastModifiedIP;
            hist.Status = obj.Status;

            hist.bankname = obj.bankname;
            hist.bankacountname = obj.bankacountname;
            hist.PayType = obj.PayType;
            hist.paymentmode = obj.paymentmode;
            hist.writingfee = obj.writingfee;
            hist.referral = obj.referral;
            hist.haveInvoice = obj.haveInvoice;
            hist.paystatus = obj.paystatus;
            hist.uploadstarttime = obj.uploadstarttime;
            hist.uploadendtime = obj.uploadendtime;
            hist.PrivateRemark = obj.PrivateRemark;
            hist.cooperatecircs = obj.cooperatecircs;
            hist.cityid = obj.cityid;
            hist.cityname = obj.cityname;
            hist.OtherMessageSoftware = obj.OtherMessageSoftware;
            hist.Remark = obj.Remark;

            hist.ReporterPosition = obj.ReporterPosition;
            hist.ResponsibleDomain = obj.ResponsibleDomain;

            hist.Tel_H = obj.Tel_H;
            hist.Tel_O = obj.Tel_O;

            //sn
            hist.SN = obj.SN;

            hist.version = Version;
            ESP.MediaLinq.BusinessLogic.ReportersHistManager.Add(hist);
            ESP.Logging.Logger.Add("Save reporter history is success.", "Media system", ESP.Logging.LogLevel.Information);

        }
    }
}