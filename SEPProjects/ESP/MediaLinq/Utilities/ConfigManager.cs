using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ESP.MediaLinq.Utilities
{
    public class ConfigManager
    {


        #region run

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public static string SqlConnection
        {
            get { return ESP.Configuration.ConfigurationManager.SafeConnectionStrings["CustomerSqlConnection"].ConnectionString; }
        }

        public static string ErrorPage
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ErrorPage"]; }
        }

        //public static string ImgPath
        //{
        //    get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ImgPath"]; }
        //}

        public static string ReporterLogoPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ReporterLogoPath"]; }
        }

        public static string ProductLineLogoPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ProductLineLogoPath"]; }
        }

        public static string ClientLogoPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ClientLogoPath"]; }
        }

        public static string MediaLogoPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MediaLogoPath"]; }
        }

        public static string MediaPricePath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MediaPricePath"]; }
        }

        public static string MediaBriefPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MediaBriefPath"]; }
        }

        public static string EventSignPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["EventSignPath"]; }
        }

        public static string EventCommunicatePath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["EventCommunicatePath"]; }
        }

        public static string BillPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["BillPath"]; }

        }

        public static string EventBriefPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["EventBriefPath"]; }
        }

        public static string DailySignPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DailySignPath"]; }
        }

        public static string DailyCommunicatePath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DailyCommunicatePath"]; }
        }

        public static string DailyBriefPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DailyBriefPath"]; }
        }

        public static string ProjectBriefPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectBriefPath"]; }
        }

        public static string CacheServicePath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["CacheServicePath"]; }
        }
        public static string DisplayIconPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DisplayIconPath"]; }
        }

        public static string ShunyaWordLogoPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["ShunyaWordLogoPath"]; }
        }

        public static string EditIconPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["EditIconPath"]; }
        }
        public static string PRIconPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["PRIconPath"]; }
        }
        public static string DelIconPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DelIconPath"]; }

        }

        public static string DefauleImgPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DefauleImgPath"]; }

        }

        public static string SendMailAddress
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["SendMailAddress"]; }

        }

        public static string DisplaySendName
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["DisplaySendName"]; }

        }

        public static string SmtpHostAddress
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["SmtpHostAddress"]; }

        }

        public static string MailUserName
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MailUserName"]; }

        }

        public static string MailPassWord
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MailPassWord"]; }

        }

        public static string MailAttachmentPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MailAttachmentPath"]; }

        }


        public static string MediaFaceToFacePath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MediaFaceToFacePath"]; }

        }


        public static string MediaMeetings
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["MediaMeetings"]; }

        }

        public static string PRPath
        {
            get { return ESP.Configuration.ConfigurationManager.SafeAppSettings["PRPath"]; }
        }

        /// <summary>
        /// 普通用户角色的ID
        /// </summary>
        public static string OrdinaryRoleID
        {
            get { return string.IsNullOrEmpty(ESP.Configuration.ConfigurationManager.SafeAppSettings["OrdinaryRoleID"]) ? "129,131" : ESP.Configuration.ConfigurationManager.SafeAppSettings["OrdinaryRoleID"]; }
        }


        /// <summary>
        /// 普通用户角色的ID列表
        /// </summary>
        public static List<int> OrdinaryRoleIDList
        {
            get
            {
                List<int> Ids = new List<int>();
                string ids = OrdinaryRoleID;
                string[] id_array = ids.Split(',');
                for (int i = 0; i < id_array.Length; i++)
                {
                    Ids.Add(Convert.ToInt32(id_array[i]));
                }
                return Ids;
            }
        }


        /// <summary>
        /// 不参加积分角色的ID
        /// </summary>
        public static string NotIntegralRoleID
        {
            get { return string.IsNullOrEmpty(ESP.Configuration.ConfigurationManager.SafeAppSettings["NotIntegralRoleID"]) ? "129,131" : ESP.Configuration.ConfigurationManager.SafeAppSettings["NotIntegralRoleID"]; }
        }


        /// <summary>
        /// 不参加积分角色的ID列表
        /// </summary>
        public static List<int> NotIntegralRoleIDList
        {
            get
            {
                List<int> Ids = new List<int>();
                string ids = NotIntegralRoleID;
                string[] id_array = ids.Split(',');
                for (int i = 0; i < id_array.Length; i++)
                {
                    Ids.Add(Convert.ToInt32(id_array[i]));
                }
                return Ids;
            }
        }

        #endregion

    }
}
