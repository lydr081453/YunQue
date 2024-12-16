using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using System.Data;

namespace ESP.Purchase.DataAccess
{
  public  class SupplierLogDataProvider
    {
      public IList<ESP.Purchase.Entity.SupplierLogInfo> GetList(string strWhere)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("select * ");
          strSql.Append(" FROM T_SupplierLog where 1=1");
          if (strWhere.Trim() != "")
          {
              strSql.Append(strWhere);
          }
          return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.SupplierLogInfo>(DbHelperSQL.Query(strSql.ToString()));
      }

      public int Add(ESP.Purchase.Entity.SupplierLogInfo model)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("insert into T_SupplierLog(");
          strSql.Append("EspSupplierId,SupplySupplierId,OldName,NewName,ChangeDate,Operator,Remark)");
          strSql.Append(" values (");
          strSql.Append("@EspSupplierId,@SupplySupplierId,@OldName,@NewName,@ChangeDate,@Operator,@Remark)");
          strSql.Append(";select @@IDENTITY");
          SqlParameter[] parameters = {
					new SqlParameter("@EspSupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplySupplierId", SqlDbType.Int,4),
					new SqlParameter("@OldName", SqlDbType.NVarChar,500),
					new SqlParameter("@NewName", SqlDbType.NVarChar,500),
                    new SqlParameter("@ChangeDate",SqlDbType.DateTime),
                     new SqlParameter("@Operator",SqlDbType.NVarChar,50),
                      new SqlParameter("@Remark",SqlDbType.NVarChar,500)
                                      };
          parameters[0].Value = model.EspSupplierId;
          parameters[1].Value = model.SupplySupplierId;
          parameters[2].Value = model.OldName;
          parameters[3].Value = model.NewName;
          parameters[4].Value = model.ChangeDate;

          parameters[5].Value = model.Operator;
          parameters[6].Value = model.Remark;

          object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
          if (obj == null)
          {
              return 1;
          }
          else
          {
              return Convert.ToInt32(obj);
          }
      }
    }
}
