using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace Portal.Common.Compatible
{
    public class FrameworkAuthentication
    {
        private static string _defaultKey = "1234567890123456";
        /// <summary>
        /// 加密密钥
        /// </summary>
        private static readonly string CONFIGMANAGER_CRYPTOPROVIDER_KEY = "CryptoKey";
        /// <summary>
        /// 加密向量
        /// </summary>
        private static readonly string CONFIGMANAGER_CRYPTOPROVIDER_IV = "CryptoIV";

        /// <summary>
        /// 配置SQL Server连接字符串
        /// </summary>
        private static string ConfigSqlConnectionString
        {
            get
            {
                return ESP.Configuration.ConfigurationManager.SafeConnectionStrings["FrameworkCS"].ConnectionString;
                //return "server=10.1.2.8;database=Framework;user id=sa;password=q1W@e3R$";
            }
        }

        /// <summary>
        /// 获取加密对称加密密匙
        /// </summary>
        public static string CryptoKey
        {
            get
            {
                try
                {
                    string key = ConfigSetting(CONFIGMANAGER_CRYPTOPROVIDER_KEY);
                    key = key + _defaultKey;
                    key = key.Substring(0, 16);
                    return key;
                }
                catch (Exception ex)
                {
                    throw new Exception("获取CryptoKey出错！", ex);
                }
            }
        }

        /// <summary>
        /// 获取对称加密初始化向量
        /// </summary>
        public static string CryptoIV
        {
            get
            {
                try
                {
                    string iv = ConfigSetting(CONFIGMANAGER_CRYPTOPROVIDER_IV);
                    iv = iv + _defaultKey;
                    iv = iv.Substring(0, 16);
                    return iv;
                }
                catch (Exception ex)
                {
                    throw new Exception("获取CryptoKey出错！", ex);
                }
            }
        }

        /// <summary>
        /// 配置缓存
        /// </summary>
        private static Hashtable configCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///  解密传入的字符串
        /// </summary>
        /// <param name="source">需要解密的字符串</param>
        /// <returns>原字符串</returns>
        public static string Decrypt(string source)
        {
            if (source == null) return "";
            if (source.Trim() == "") return "";
            try
            {
                RijndaelManaged mCSP = new RijndaelManaged();
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;

                ct = mCSP.CreateDecryptor(StringToByteArr(CryptoKey), StringToByteArr(CryptoIV));
                byt = Convert.FromBase64String(source);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("解密字符串出错！", ex);
            }
        }

        /// <summary>
        ///  根据传入字符串生成BYTE数组
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns>结果为byte数组</returns>
        public static byte[] StringToByteArr(string source)
        {
            byte[] byts = new byte[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                byts[i] = Convert.ToByte(source.Substring(i, 1));
            }
            return byts;
        }


        /// <summary>
        ///  根据参数名称得到参数值
        /// </summary>
        /// <param name="configName">参数名称</param>
        /// <returns>参数值，如果查无参数，抛出异常</returns>
        public static string ConfigSetting(string configName)
        {
            configName = configName.Trim();

            lock (configCache)
            {
                string configValue = (string)configCache[configName];

                if (configValue == null)
                {

                    string sql = "select Value from F_Config where Name = @name ";
                    try
                    {
                        configValue = SqlExecuteScalar(sql, new SqlParameter[]{
                            new SqlParameter("@name", configName)
                        }).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Get Framework setting '" + configName + "' error.", ex);
                    }

                    configCache.Add(configName, configValue);
                }
                return configValue;
            }
        }

        public static object SqlExecuteScalar(string sqlText, SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(ConfigSqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                cmd.Parameters.AddRange(paras);
                conn.Open();
                object ret = cmd.ExecuteScalar();
                conn.Close();

                return ret;
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader SqlExecuteReader(CommandType commandType, string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(ConfigSqlConnectionString);
            SqlCommand cmd = new SqlCommand(SQLString, connection);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(cmdParms);
            connection.Open();
            try
            {
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                connection.Close();
                return null;
            }
        }

        public static bool ParseToken(string encToken, out string userCode)
        {
            string ipAddr;
            return ParseToken(encToken, out userCode, out ipAddr);
        }
        public static bool ParseToken(string encToken, out string userCode, out string ipAddr)
        {
            userCode = null;
            ipAddr = null;

            if (encToken == null || encToken.Length == 0)
                return false;

            try
            {
                string token = Decrypt(encToken);
                XmlDocument xdc = new XmlDocument();
                xdc.LoadXml(token);
                string uc = xdc.DocumentElement.GetAttribute("userid");
                if (uc == null || uc.Length == 0)
                    return false;

                string ip = xdc.DocumentElement.GetAttribute("ip");


                userCode = uc;
                ipAddr = ip;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  对字符串中的XML特殊字符做处理
        /// </summary>
        /// <param name="inStr">需要处理的字符串</param>
        /// <returns>处理结果</returns>
        public static string XmlEncode(string inStr)
        {
            if (inStr == "" || inStr == null)
            {
                return "";
            }

            string retStr = "";

            for (int i = 0; i < inStr.Length; i++)
            {
                switch (inStr.Substring(i, 1))
                {
                    case "&":
                        retStr += @"&amp;";
                        break;
                    case "\"":
                        retStr += @"&quot;";
                        break;
                    case "'":
                        retStr += @"&apos;";
                        break;
                    case "<":
                        retStr += @"&lt;";
                        break;
                    case ">":
                        retStr += @"&gt;";
                        break;
                    default:
                        retStr += inStr.Substring(i, 1);
                        break;
                }
            }

            return retStr;
        }

        /// <summary>
        ///  加密传入的字符串
        /// </summary>
        /// <param name="source">加密的字符串</param>
        /// <returns>加密结果</returns>
        public static string Encrypt(string source)
        {
            if (source == null) return "";
            if (source.Trim() == "") return "";
            try
            {
                RijndaelManaged mCSP = new RijndaelManaged();

                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;

                ct = mCSP.CreateEncryptor(StringToByteArr(CryptoKey), StringToByteArr(CryptoIV));

                byt = Encoding.UTF8.GetBytes(source);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("加密字符串出错！", ex);
            }
        }


        /// <summary>
        /// 根据用户ITCODE得到用户的口令
        /// </summary>
        /// <param name="userCode">用户ITCode</param>
        /// <returns>口令</returns>
        private static string GetUserPassword(string userCode)
        {
            if (userCode == null || userCode.Length == 0)
            {
                return null;
            }

            string sql = "select Password from F_Employee where UserCode = @code";
            SqlParameter[] paras = { new SqlParameter("@code", userCode) };
            return (string)SqlExecuteScalar(sql, paras);
        }


        /// <summary>
        /// 得到含有用户登录信息的加密Xml字符串
        /// </summary>
        /// <param name="userCode">需要的用户编码</param>
        /// <returns>返回加密结果</returns>
        public static string GetLogonToken(string userCode, string ip)
        {
            //ip = "172.16.11.208"; // TODO: 测试代码，需删除

            if (userCode != null)
                userCode = userCode.Trim();

            string xml = @"<?xml version='1.0' encoding='gb2312'?>
<token userid='{0}' password='{1}' ip='{2}' />
";
            string pwd = GetUserPassword(userCode);
            xml = string.Format(xml, XmlEncode(userCode), XmlEncode(pwd), XmlEncode(ip));
            return System.Web.HttpUtility.UrlEncode(Encrypt(xml));
        }

        public static IDataReader GetUserRow(int sysUserId)
        {
            string sql = "select * from F_Employee where SysUserID=@SysUserID";
            return SqlExecuteReader(CommandType.Text, sql, new SqlParameter[] { new SqlParameter("@SysUserID", sysUserId) });
        }

        public static int GetUserSysID(string userCode)
        {
            string sql = "select CAST(SysUserID AS int) from F_Employee where UserCode=@UserCode";
            object obj = SqlExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@UserCode", userCode) });
            if (obj != null && DBNull.Value != obj)
            {
                return (int)obj;
            }
            return 0;
        }

        public static IList<FrameworkDomain> GetUserDomains(string userCode)
            {
                string sql = @"
select F_Domain.SID, NodeName, F_Module.Description, DNSNamespace
from F_Module, F_ModuleURI, F_Domain 
where NodeLevel=1 and  F_Module.DNSName *= F_Domain.SID and F_Module.DefaultURI *= F_ModuleURI.SID 
    and ( UniqID in ( select MID from F_RoleModuleRelation where RID in ( 
            select Role from F_UserRoleRelation where UserCode = @UserCode ) )
        or UniqID in (select MID from F_RoleModuleRelation where RID in (
            select RID from F_Role where Type = 0 )) 
        or UniqID in ( select MID from F_AcceditModule where SID in ( 
            select SID from F_Accedit where Accepter = @UserCode 
                and Type = 1 and BeginTime <= GetDate() and EndTime >= GetDate()  ))) 
order by NodePath
";
                SqlDataReader Reader = SqlExecuteReader(CommandType.Text, sql, new SqlParameter("@UserCode", userCode.Trim()));
                List<FrameworkDomain> Rv = new List<FrameworkDomain>();
                while (Reader.Read())
                {
                    try
                    {
                        if (Reader["NodeName"].ToString() == "内网门户")
                        {
                            continue;
                        }
                        FrameworkDomain NewDomain = new FrameworkDomain();
                        NewDomain.ID = (decimal)Reader["SID"];
                        NewDomain.URL = Reader["DNSNamespace"].ToString();
                        NewDomain.Name = Reader["NodeName"].ToString();
                        NewDomain.Description = Reader["Description"].ToString();
                        Rv.Add(NewDomain);
                    }
                    catch { }
                }
                return Rv;
            }

    }
    public class FrameworkDomain
    {
        public decimal ID;
        public string URL;
        public string Name;
        public string Description;

        public FrameworkDomain()
        {
        }

    }
}
