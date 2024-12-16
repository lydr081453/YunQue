using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;

namespace ESP.Administrative.DataAccess
{
    public class TimeSheetCategoryDataProvider
    {
        public TimeSheetCategoryDataProvider()
		{}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TimeSheetCategoryInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_TimeSheetCategory(");
			strSql.Append("TypeName,Remark)");
			strSql.Append(" values (");
			strSql.Append("@TypeName,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.TypeName;
			parameters[1].Value = model.Remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(TimeSheetCategoryInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_TimeSheetCategory set ");
			strSql.Append("TypeName=@TypeName,");
            strSql.Append("Remark=@Remark,Billable=@Billable");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@Billable",SqlDbType.Int,4)};
			parameters[0].Value = model.TypeName;
			parameters[1].Value = model.Remark;
			parameters[2].Value = model.Id;
            parameters[3].Value = model.Billable;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AD_TimeSheetCategory ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AD_TimeSheetCategory ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public TimeSheetCategoryInfo GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from AD_TimeSheetCategory ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			TimeSheetCategoryInfo model=new TimeSheetCategoryInfo();
			return CBO.FillObject<TimeSheetCategoryInfo>(DbHelperSQL.Query(strSql.ToString(),parameters));
			
		}

        public List<TimeSheetCategoryInfo> GetList(int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM AD_TimeSheetCategory ");
            strSql.Append(" where parentId=" + parentId);
            return CBO.FillCollection<TimeSheetCategoryInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TimeSheetCategoryInfo> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM AD_TimeSheetCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return CBO.FillCollection<TimeSheetCategoryInfo>(DbHelperSQL.Query(strSql.ToString()));
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public List<TimeSheetCategoryInfo> GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM AD_TimeSheetCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return CBO.FillCollection<TimeSheetCategoryInfo>(DbHelperSQL.Query(strSql.ToString()));
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM AD_TimeSheetCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
        public List<TimeSheetCategoryInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from AD_TimeSheetCategory T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return CBO.FillCollection<TimeSheetCategoryInfo>(DbHelperSQL.Query(strSql.ToString()));
		}
    }
}
