using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    internal class GetWaitPaymentListDataProvider : ESP.Finance.IDataAccess.IGetWaitPaymentListDataProvider
    {
        #region IV_GetWaitPaymentListProvider 成员

        public ESP.Finance.Entity.WaitPaymentListViewInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            string sql = @"SELECT top 1 [id]
                            ,[gid]
                            ,[beginDate]
                            ,[endDate]
                            ,[periodRemark]
                            ,[inceptPrice]
                            ,[inceptDate]
                            ,[periodType]
	                        ,[periodTypeName] = case [periodType] when 0 then '标准条款' when 1 then '预付条款' else '' end
                            ,[periodDatumPoint]
	                        ,[periodDatumPointName] = case [periodDatumPoint] when 0 then '收货日期' when 1 then '合同/订单日期' else '' end
	                        ,[periodDay]
	                        ,[periodDayName] = case [periodDay] when 0 then '工作日' when 1 then '自然日' else '' end
                            ,[dateType]
                            ,[expectPaymentPrice]
                            ,[expectPaymentPercent]
                            ,[Status]
	                        ,[StatusName] = case [Status] when 0 then '帐期已创建' when 1 then '保存' when 2 then '等待付款' when 3 then '待定2' when 4 then '待定3' when 5 then '已付款' else '' end
                            ,[prNo]
                            ,[requestorname]
                            ,[endusername]
                            ,[project_id]
                            ,[project_code]
                          FROM  [V_WaitPaymentList] ";
            strSql.Append(sql);
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<Entity.WaitPaymentListViewInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }




        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.WaitPaymentListViewInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            string sql = @"SELECT [id]
                            ,[gid]
                            ,[beginDate]
                            ,[endDate]
                            ,[periodRemark]
                            ,[inceptPrice]
                            ,[inceptDate]
                            ,[periodType]
	                        ,[periodTypeName] = case [periodType] when 0 then '标准条款' when 1 then '预付条款' else '' end
                            ,[periodDatumPoint]
	                        ,[periodDatumPointName] = case [periodDatumPoint] when 0 then '收货日期' when 1 then '合同/订单日期' else '' end
	                        ,[periodDay]
	                        ,[periodDayName] = case [periodDay] when 0 then '工作日' when 1 then '自然日' else '' end
                            ,[dateType]
                            ,[expectPaymentPrice]
                            ,[expectPaymentPercent]
                            ,[Status]
	                        ,[StatusName] = case [Status] when 0 then '帐期已创建' when 1 then '保存' when 2 then '等待付款' when 3 then '待定2' when 4 then '待定3' when 5 then '已付款' else '' end
                            ,[prNo]
                            ,[requestorname]
                            ,[endusername]
                            ,[project_id]
                            ,[project_code]
                          FROM [CG].[dbo].[V_GetWaitPaymentList] ";
            strSql.Append(sql);
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<Entity.WaitPaymentListViewInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<Entity.WaitPaymentListViewInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }


        #endregion
    }
}
