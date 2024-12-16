using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class PositionLevelsDataProvider
    {
        public PositionLevelsDataProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PositionLevelsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_PositionLevels(");
            strSql.Append("LevelName,BeginDate,EndDate,ChargeRate,SalaryHigh,SalaryLow,Remark,BillableRate)");
            strSql.Append(" values (");
            strSql.Append("@LevelName,@BeginDate,@EndDate,@ChargeRate,@SalaryHigh,@SalaryLow,@Remark,@BillableRate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@ChargeRate", SqlDbType.Decimal,20),
                    new SqlParameter("@SalaryHigh", SqlDbType.Decimal,20),
                    new SqlParameter("@SalaryLow", SqlDbType.Decimal,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@BillableRate", SqlDbType.Decimal,20),
                                        };
            parameters[0].Value = model.LevelName;
            parameters[1].Value = model.BeginDate;
            parameters[2].Value = model.EndDate;
            parameters[3].Value = model.ChargeRate;
            parameters[4].Value = model.SalaryHigh;
            parameters[5].Value = model.SalaryLow;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.BillableRate;


            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PositionLevelsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_PositionLevels set ");
            strSql.Append("LevelName=@LevelName,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("ChargeRate=@ChargeRate,");
            strSql.Append("SalaryHigh=@SalaryHigh,");
            strSql.Append("SalaryLow=@SalaryLow,");
            strSql.Append("Remark=@Remark,BillableRate=@BillableRate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@LevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@ChargeRate", SqlDbType.Decimal,20),
                    new SqlParameter("@SalaryHigh", SqlDbType.Decimal,20),
                    new SqlParameter("@SalaryLow", SqlDbType.Decimal,20),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@BillableRate", SqlDbType.Decimal,20),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.LevelName;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SEP_PositionLevels ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SEP_PositionLevels ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PositionLevelsInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from SEP_PositionLevels ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            PositionLevelsInfo model = new PositionLevelsInfo();
            return CBO.FillObject<PositionLevelsInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PositionLevelsInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SEP_PositionLevels ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<PositionLevelsInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<PositionLevelsInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SEP_PositionLevels ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return CBO.FillCollection<PositionLevelsInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SEP_PositionLevels ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<PositionLevelsInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from SEP_PositionLevels T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return CBO.FillCollection<PositionLevelsInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
