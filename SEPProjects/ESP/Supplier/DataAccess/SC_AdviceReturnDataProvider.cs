using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_AdviceReturnDataProvider
    {
        public SC_AdviceReturnDataProvider()
        { }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_AdviceReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_AdviceReturn(");
            strSql.Append("AdviceID,Body,CreatedDate,CreatedUserID,CreatedName)");
            strSql.Append(" values (");
            strSql.Append("@AdviceID,@Body,@CreatedDate,@CreatedUserID,@CreatedName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AdviceID", SqlDbType.Int, 8),
					new SqlParameter("@Body", SqlDbType.NVarChar,2000),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserID", SqlDbType.Int,8),
                    new SqlParameter("@CreatedName",SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.AdviceID;
            parameters[1].Value = model.Body;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.CreatedName;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_AdviceReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_AdviceReturn set ");
            strSql.Append("AdviceID=@AdviceID,Body=@Body,CreatedDate=@CreatedDate,CreatedUserID=@CreatedUserID,CreatedName=@CreatedName");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@AdviceID", SqlDbType.Int, 8),
					new SqlParameter("@Body", SqlDbType.NVarChar,2000),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserID", SqlDbType.Int,8),
                    new SqlParameter("@CreatedName",SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.ID;
            parameters[0].Value = model.AdviceID;
            parameters[1].Value = model.Body;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.CreatedName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_AdviceReturn ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
            parameters[0].Value = Id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_AdviceReturn GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_AdviceReturn ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return ESP.ConfigCommon.CBO.FillObject<SC_AdviceReturn>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM SC_AdviceReturn ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_AdviceReturn> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_AdviceReturn where 1=1 ");
            strSql.Append(strWhere);
            return ESP.ConfigCommon.CBO.FillCollection<SC_AdviceReturn>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_AdviceReturn> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_AdviceReturn>(GetList(""));
        }
    }
}
