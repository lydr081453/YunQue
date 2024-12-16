using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

using ESP.Media.Access.Utilities;
using ESP.Media.Access;
using ESP.Media.Entity;
namespace ESP.Media.BusinessLogic
{
    public class IntegralruleManager
    {
        public IntegralruleManager()
        { 
        
        }


        /// <summary>
        /// ���һ�����ֹ���
        /// </summary>
        /// <param name="rule"></param>
        /// <returns>
        /// -1,�Ѿ������˲���
        /// -2,�Ѿ������˹ؼ���
        /// >0,��ӳɹ�,���ز����ID
        ///</returns>
        public static int Add(IntegralruleInfo rule)
        {
            int result = -1;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                DataTable dt = null;
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@operateid", SqlDbType.Int);
                param[0].Value = rule.Operateid;
                param[1] = new SqlParameter("@tableid", SqlDbType.Int);
                param[1].Value = rule.Tableid;
                dt = ESP.Media.DataAccess.IntegralruleDataProvider.QueryInfo(trans, "operateid =  @operateid and tableid=@tableid", param);//��ѯ�Ƿ��Ѿ��д������
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = -1;
                    trans.Rollback();
                    conn.Close();
                    return result;
                }
                param = new SqlParameter[1];
                param[0] = new SqlParameter("@name", SqlDbType.NVarChar);
                param[0].Value = rule.Name;
                dt = ESP.Media.DataAccess.IntegralruleDataProvider.QueryInfo(trans, "name=@name", param);//��ѯ�Ƿ��Ѿ��д˹ؼ���
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = -2;
                    trans.Rollback();
                    conn.Close();
                    return result;
                }
                rule.Del = (int)Global.FiledStatus.Usable;
                result = ESP.Media.DataAccess.IntegralruleDataProvider.insertinfo(rule, trans);
                trans.Commit();
                conn.Close();
                return result;
            }
        }


        public static int Modify(List<IntegralruleInfo> rules, int userid)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (IntegralruleInfo rule in rules)
                    {
                        result += Modify(rule, userid, trans);
                    }
                    trans.Commit();
                    return result;
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

        }

        public static int Modify(IntegralruleInfo rule, int userid, SqlTransaction trans)
        {
            bool result = false;
            result = ESP.Media.DataAccess.IntegralruleDataProvider.updateInfo(trans, null, rule, null, null);
            if (result) return 1;
            return 0;
        }

        public static IntegralruleInfo GetModel(int id)
        {
            if (id <= 0) return new IntegralruleInfo();
            return ESP.Media.DataAccess.IntegralruleDataProvider.Load(id);
        }

        /// <summary>
        /// ����һ���������������ȡһ�����ֹ������
        /// </summary>
        /// <param name="operateid"></param>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static IntegralruleInfo GetModel(int operateid, int tableid)
        {
            IntegralruleInfo rule = null;
            using(SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    rule = GetModel(operateid, tableid, trans);
                    trans.Commit();
                    conn.Close();
                    return rule;
                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    conn.Close();
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// ����һ���������������ȡһ�����ֹ������(�������)
        /// </summary>
        /// <param name="operateid"></param>
        /// <param name="tableid"></param>
        /// <param name="trans">����</param>
        /// <returns></returns>
        public static IntegralruleInfo GetModel(int operateid, int tableid,SqlTransaction trans)
        {
            string term = "Operateid=@Operateid and Tableid = @Tableid and del != @del";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Operateid", SqlDbType.Int);
            param[0].Value = operateid;
            param[1] = new SqlParameter("@Tableid", SqlDbType.Int);
            param[1].Value = tableid;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            DataTable dt = ESP.Media.DataAccess.IntegralruleDataProvider.QueryInfo(trans,term, param);
            if (dt == null || dt.Rows.Count <= 0) return null;
            IntegralruleInfo rule = new IntegralruleInfo();
            rule.Id = dt.Rows[0]["id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["id"]);
            rule.Operateid = dt.Rows[0]["Operateid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Operateid"]);
            rule.Tableid = dt.Rows[0]["Tableid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Tableid"]);
            rule.Integral = dt.Rows[0]["Integral"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Integral"]);
            rule.Name = dt.Rows[0]["Name"] == DBNull.Value ? string.Empty : dt.Rows[0]["Name"].ToString();
            rule.Altname = dt.Rows[0]["Altname"] == DBNull.Value ? string.Empty : dt.Rows[0]["Altname"].ToString();
            return rule;
        }

        /// <summary>
        /// ����һ���������������ȡһ������
        /// </summary>
        /// <param name="operateid"></param>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static int GetIntegral(int operateid, int tableid)
        {
            IntegralruleInfo rule = GetModel(operateid, tableid);
            if (rule == null)
            {
                rule = new IntegralruleInfo();
            }
            return rule.Integral;
        }

        /// <summary>
        /// ����һ���������������ȡһ������(������)
        /// </summary>
        /// <param name="operateid"></param>
        /// <param name="tableid"></param>
        /// <param name="trans">����</param>
        /// <returns></returns>
        public static int GetIntegral(int operateid, int tableid,SqlTransaction trans)
        {
            IntegralruleInfo rule = GetModel(operateid, tableid, trans);
            if (rule == null)
            {
                rule = new IntegralruleInfo();
            }
            return rule.Integral;
        }

        /// <summary>
        /// ����һ���������������ȡһ�����ֹ���ID
        /// </summary>
        /// <param name="operateid"></param>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static int GetIntegralID(int operateid, int tableid)
        {
            IntegralruleInfo rule = GetModel(operateid, tableid);
            if (rule == null)
            {
                rule = new IntegralruleInfo();
            }
            return rule.Id;
        }


        /// <summary>
        /// ����һ���������������ȡһ�����ֹ���ID(�������)
        /// </summary>
        /// <param name="operateid"></param>
        /// <param name="tableid"></param>
        /// <param name="trans">����</param>
        /// <returns></returns>
        public static int GetIntegralID(int operateid, int tableid, SqlTransaction trans)
        {
            IntegralruleInfo rule = GetModel(operateid, tableid, trans);
            if (rule == null)
            {
                rule = new IntegralruleInfo();
            }
            return rule.Id;
        }

        /// <summary>
        /// ��ȡ���еĻ��ֹ���
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll()
        {
            return ESP.Media.DataAccess.IntegralruleDataProvider.QueryInfo(null,new Hashtable());
        }
    }
}
