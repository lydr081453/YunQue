using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using System.Text;
using ESP.Finance.DataAccess;

namespace ESP.Purchase.BusinessLogic
{
    public static class GeneralPageManager
    {
        public static List<GeneralInfo> GetModelListPage(int pageSize, int pageIndex,
    string strWhere, List<SqlParameter> parms)
        {

            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("SELECT TOP (@PageSize) * FROM(");

            strB.Append(" select a.id,a.prNo,a.requestor,a.requestorname,a.project_code,a.ProjectID,");
            strB.Append(" a.requisition_committime,a.app_date,a.first_assessor,a.first_assessorname,a.Filiale_Auditor,a.Filiale_AuditName,a.requestor_group,");
strB.Append(" a.supplier_name,a.status,a.inUse,a.requisitionflow,a.Department,a.DepartmentId,a.lasttime,a.moneyType,a.supplier_email,a.order_audittime,a.totalprice,");
strB.Append(" a.PRType,b.item_no as itemno,b.totalprice as ototalprice,b.supplierid,b.producttype,b.producttypename,ROW_NUMBER() OVER (ORDER BY a.lasttime desc) AS [__i_RowNumber] from t_generalinfo as a ");
            strB.Append(" left join ( ");
            strB.Append(" select b.id,b.general_id,b.item_no,a.totalprice,b.supplierid,b.producttype,b.producttype as producttypename from  ");
            strB.Append(" (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a ");
            strB.Append(" inner join t_orderinfo as b on a.id = b.id) as b ");
            strB.Append(" on a.id=b.general_id ");

            string sql = string.Format(strB.ToString() + " where 1=1 {0} ", strWhere);
            sql += ") t WHERE t.[__i_RowNumber] > @PageStart order by lasttime desc";
            SqlParameter psize = new SqlParameter("@PageSize", pageSize);
            SqlParameter pstart = new SqlParameter("@PageStart", pageIndex * pageSize);
            parms.Add(psize);
            parms.Add(pstart);
            return ESP.Finance.Utility.CBO.FillCollection<GeneralInfo>(ESP.Finance.DataAccess.DbHelperSQL.Query(sql, parms.ToArray()));
        }


        public static DataTable GetRecordsCount(string strWhere, List<SqlParameter> parms)
        {
            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select count(*) as count,SUM(a.totalprice) totalPrice from t_generalinfo as a ");
            strB.Append(" left join ( ");
            strB.Append(" select b.id,b.general_id,b.item_no,a.totalprice,b.supplierid,b.producttype from  ");
            strB.Append(" (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a ");
            strB.Append(" inner join t_orderinfo as b on a.id = b.id) as b ");
            strB.Append(" on a.id=b.general_id ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0}", strWhere);

            return DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];
        }

     

    }
}
