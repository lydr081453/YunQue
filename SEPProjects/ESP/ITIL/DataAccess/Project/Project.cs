using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ESP.ITIL.Common;

namespace ESP.ITIL.DataAccess.Project
{
    class Project
    {
        //取得t_orderinfo的明细
        public static DataSet 采购物品明细(string terms)
        {
            string strSql = @"select a.id,a.project_id,b.total,b.producttype 
                                from t_generalinfo as a inner join T_OrderInfo as b on a.id=b.general_id where prtype not in({0},{1}) and status not in({2},{3}) ";
            strSql = string.Format(strSql, (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA, (int)ESP.Purchase.Common.PRTYpe.PR_PriFA, ESP.Purchase.Common.State.requisition_save, ESP.Purchase.Common.State.requisition_return);
            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }

        //取得项目主申请方费用明细
        public static DataSet 主申请方费用明细(string terms)
        {
            string strSql = @" select * from F_ContractCost where 1=1 ";

            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }


        //取得项目支持方费用明细
        public static DataSet 支持方费用明细(string terms)
        {
            string strSql = @" select a.Supportid,a.Description,a.Amounts,a.CosttypeID,b.ProjectID,b.GroupID 
                            from F_SupporterCost as a inner join F_Supporter as b on a.Supportid = b.SupportID where 1=1 ";

            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }

        //取得项目信息
        public static DataSet 项目_项目信息(string terms)
        {
            string strSql = @" SELECT * FROM F_Project where 1=1 ";

            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }

        //取得项目中的费用,包括oop和媒体车马费
        public static DataSet 项目_主申请方其他费用(string terms)
        {
            string strSql = @" SELECT * FROM F_ProjectExpense where 1=1 ";

            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }

        //取得支持方费用,包括oop和媒体车马费
        public static DataSet 项目_支持方其他费用(string terms)
        {
            string strSql = @" select a.*,b.groupid,b.projectid from F_SupporterExpense as a inner join
                            F_Supporter as b on a.supporterid=b.supportid where 1=1 ";

            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }

        //取得所有f_return表中的数据，包括报销、媒体车马费、3000以下再处理的pn单
        public static DataSet 实际花费_其他费用(string terms)
        {
            string strSql = @" SELECT * FROM F_Return where 1=1 ";

            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }

        public static DataSet 实际花费_报销费用(string terms1,string terms2)
        {
            string strSql = @" SELECT a.*,b.* FROM F_Return  as a inner join (SELECT  [ReturnID],sum(expensemoney) as totalcost
                              FROM F_ExpenseAccountDetail 
                            where status = 1 ";

            if (!string.IsNullOrEmpty(terms1))
                strSql += terms1;

            strSql += "  group by ReturnID  ) as b on a.returnid=b.returnid ";

            if (!string.IsNullOrEmpty(terms2))
                strSql += terms2;


            return DbHelperSQL.Query(strSql);
        }

        //取得所有采购系统产生费用,即通过pr单产生的费用
        public static DataSet 实际花费_采购系统产生的费用(string terms)
        {
            string strSql = @" SELECT a.id,a.prNo,a.project_id,a.status as prstatus,a.DepartmentId,a.PRType,b.expectPaymentPrice,
                                b.inceptPrice,b.Status as pnstatus,c.PreFee,c.FactFee,c.ReturnStatus
                                  FROM T_GeneralInfo as a inner join t_paymentperiod as b on a.id=b.gid left join f_return as c
                                on b.ReturnId=c.ReturnID
                                where a.status not in({0},{1},{2}) and a.id not in(select oldprid from t_mediapredithis) ";
            strSql = string.Format(strSql, ESP.Purchase.Common.State.requisition_save, ESP.Purchase.Common.State.requisition_return, ESP.Purchase.Common.State.requisition_del);
            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }

        public static DataSet 实际花费_所有帐期金额(string terms)
        {
            string strSql = @" SELECT * FROM t_paymentperiod where 1=1 ";

            if (!string.IsNullOrEmpty(terms))
                strSql += terms;

            return DbHelperSQL.Query(strSql);
        }
    }
}
