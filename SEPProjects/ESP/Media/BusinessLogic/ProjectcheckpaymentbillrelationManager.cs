using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.Access.Utilities;

namespace ESP.Media.BusinessLogic
{
    public class ProjectcheckpaymentbillrelationManager
    {
        public static int add(ProjectcheckpaymentbillrelationInfo relation, int userid, SqlTransaction trans)
        {
            //string term = " and Personfeebillid=@Personfeebillid and Projectid = @Projectid  and del!=@del";
            //SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@Personfeebillid", relation.Personfeebillid);
            //param[1] = new SqlParameter("@Projectid", relation.Projectid);
            //param[2] = new SqlParameter("@del", (int)Global.FiledStatus.Del);
            //DataTable dt = ESP.Media.DataAccess.ProjectpersonfeebillrelationDataProvider.QueryInfo(trans, term, param);
            //if (dt == null || dt.Rows.Count == 0)
            //{
            return ESP.Media.DataAccess.ProjectcheckpaymentbillrelationDataProvider.insertinfo(relation, trans);
            //}
            //return 0;
        }


        public static int add(int billid, int[] projectids, int userid, SqlTransaction trans)
        {
            string term = " and Checkpaymentbillid=@Checkpaymentbillid and a.del!=@del";
            Hashtable ht = new Hashtable();
            ht.Add("@Checkpaymentbillid", billid);
            ht.Add("@del", (int)Global.FiledStatus.Del);
            DataTable dt = GetList(term, ht, trans);
            if (dt == null || dt.Rows.Count == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int id = dt.Rows[i]["id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["id"]);
                    del(id, trans);
                }
            }
            if (projectids == null || projectids.Length <= 0) return 0;
            for (int i = 0; i < projectids.Length; i++)
            {
                ProjectcheckpaymentbillrelationInfo relation = new ProjectcheckpaymentbillrelationInfo();
                relation.Checkpaymentbillid = billid;
                relation.Projectid = projectids[i];
                relation.Del = (int)Global.FiledStatus.Usable;
                add(relation, userid, trans);
            }
            return projectids.Length;
        }


        public static bool del(int id, SqlTransaction trans)
        {
            return ESP.Media.DataAccess.ProjectcheckpaymentbillrelationDataProvider.DeleteInfo(id, trans);
        }

        public static bool delByBillID(int billid, int userid, SqlTransaction trans)
        {
            return true;
        }

        private static DataTable query(string term, Hashtable ht)
        {
            string sql = @"select a.id as id,a.Checkpaymentbillid as Checkpaymentbillid,a.projectid as projectid,a.del as del, project.projectcode as projectcode 
                            from Media_projectcheckpaymentbillrelation as a 
                            inner join media_projects as project on a.projectid = project.ProjectId where 1=1 {0}";
            if (term == null) term = string.Empty;
            sql = string.Format(sql, term);
            if (ht == null) ht = new Hashtable();

            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
        }

        private static DataTable query(string term, Hashtable ht, SqlTransaction trans)
        {
            string sql = @"select a.id as id,a.Checkpaymentbillid as Checkpaymentbillid,a.projectid as projectid,a.del as del,project.projectcode as projectcode 
                            from Media_projectcheckpaymentbillrelation as a 
                            inner join media_projects as project on a.projectid = project.ProjectId where 1=1 {0}";
            if (term == null) term = string.Empty;
            sql = string.Format(sql, term);
            if (ht == null) ht = new Hashtable();

            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(trans, sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
        }

        public static DataTable GetList(string term, Hashtable ht, SqlTransaction trans)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and a.del != @del";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return query(term, ht, trans);
        }

        public static DataTable GetList(string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and a.del != @del";
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return query(term, ht);
        }


        public static DataTable GetListByBillID(int billid, string term, Hashtable ht)
        {
            if (term == null)
                term = string.Empty;
            if (ht == null)
                ht = new Hashtable();
            term += " and Checkpaymentbillid=@Checkpaymentbillid and a.del!=@del";
            if (!ht.ContainsKey("@Checkpaymentbillid"))
            {
                ht.Add("@Checkpaymentbillid", billid);
            }
            if (!ht.ContainsKey("@del"))
                ht.Add("@del", (int)Global.FiledStatus.Del);
            return query(term, ht);
        }
    }
}
