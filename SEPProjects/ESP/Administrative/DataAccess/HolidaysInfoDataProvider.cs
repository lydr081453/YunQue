using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Administrative.DataAccess
{
    public class HolidaysInfoDataProvider
    {
        public HolidaysInfoDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AD_HolidaysInfo");
			strSql.Append(" where ID= @ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(HolidaysInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AD_HolidaysInfo(");
			strSql.Append("HoliDate,Type,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort,HoliYear)");
			strSql.Append(" values (");
            strSql.Append("@HoliDate,@Type,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort,@HoliYear)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@HoliDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@HoliYear",SqlDbType.Int, 4)};
			parameters[0].Value = model.HoliDate;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.Deleted;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.UpdateTime;
			parameters[5].Value = model.OperateorID;
			parameters[6].Value = model.OperateorDept;
			parameters[7].Value = model.Sort;
            parameters[8].Value = model.Holiyear;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
        public void Update(HolidaysInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AD_HolidaysInfo set ");
			strSql.Append("HoliDate=@HoliDate,");
			strSql.Append("Type=@Type,");
			strSql.Append("Deleted=@Deleted,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("OperateorID=@OperateorID,");
			strSql.Append("OperateorDept=@OperateorDept,");
			strSql.Append("Sort=@Sort,");
            strSql.Append("HoliYear=@HoliYear ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@HoliDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
                    new SqlParameter("@HoliYear",SqlDbType.Int, 4)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.HoliDate;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.Deleted;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.UpdateTime;
			parameters[6].Value = model.OperateorID;
			parameters[7].Value = model.OperateorDept;
			parameters[8].Value = model.Sort;
            parameters[9].Value = model.Holiyear;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete AD_HolidaysInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
			parameters[0].Value = ID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public HolidaysInfo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from AD_HolidaysInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
            HolidaysInfo model = new HolidaysInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			model.ID=ID;
			if(ds.Tables[0].Rows.Count>0)
			{
                model.PopupData(ds.Tables[0].Rows[0]);
				return model;
			}
			else
			{
			return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * FROM AD_HolidaysInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获取一年中某个月的节假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回一个节假日信息集合</returns>
        public HashSet<int> GetHolidayListByMonth(int year, int month)
        {
            string sql = @"
SELECT HoliDate FROM AD_HolidaysInfo WHERE DATEPART(year, HoliDate) = @Year AND DATEPART(month, HoliDate) = @Month
";

            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@Year", year),
                new SqlParameter("@Month", month)
            }))
            {
                HashSet<int> list = new HashSet<int>();
                while (reader.Read())
                {
                    list.Add(reader.GetDateTime(0).Day);
                }

                return list;
            }
        }

        /// <summary>
        /// 获取某一年中所有的节假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>返回一个节假日信息集合</returns>
        public HashSet<int> GetHolidayListByMonth(int year)
        {
            string sql = @"
SELECT HoliDate FROM AD_HolidaysInfo WHERE DATEPART(year, HoliDate) = @Year ";

            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@Year", year)
            }))
            {
                HashSet<int> list = new HashSet<int>();
                while (reader.Read())
                {
                    list.Add(reader.GetDateTime(0).Day);
                }

                return list;
            }
        }

        /// <summary>
        /// 获取某一年中所有的节假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>返回一个节假日信息集合</returns>
        public HashSet<int> GetHolidayListByMonth(DateTime beginTime, DateTime endTime)
        {
            string sql = @"
SELECT HoliDate FROM AD_HolidaysInfo WHERE HoliDate BETWEEN @beginTime AND @endTime ";

            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@beginTime", beginTime.Date),
                new SqlParameter("@endTime", endTime.Date)
            }))
            {
                HashSet<int> list = new HashSet<int>();
                while (reader.Read())
                {
                    list.Add(reader.GetDateTime(0).Day);
                }
                return list;
            }
        }


        /// <summary>
        /// 获取某一年中所有的节假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>返回一个节假日信息集合</returns>
        public HashSet<int> GetHolidayListByMonthDate(DateTime beginTime, DateTime endTime)
        {
            string sql = @"
SELECT HoliDate FROM AD_HolidaysInfo WHERE HoliDate BETWEEN @beginTime AND @endTime AND Type=1";

            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@beginTime", beginTime.Date),
                new SqlParameter("@endTime", endTime.Date)
            }))
            {
                HashSet<int> list = new HashSet<int>();
                while (reader.Read())
                {
                    list.Add(reader.GetDateTime(0).Day);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取一年中某个月的节假日信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回一个节假日信息集合</returns>
        public HashSet<int> GetSabbaticalLeaveByMonth(int year, int month)
        {
            string sql = @"
SELECT HoliDate FROM AD_HolidaysInfo WHERE DATEPART(year, HoliDate) = @Year 
    AND DATEPART(month, HoliDate) = @Month
    AND Type=@HolidayType
";

            using (IDataReader reader = DbHelperSQL.ExecuteReader(sql, new SqlParameter[]{
                new SqlParameter("@Year", year),
                new SqlParameter("@Month", month),
                new SqlParameter("@HolidayType", HolidayType.SabbaticalLeave)
            }))
            {
                HashSet<int> list = new HashSet<int>();
                while (reader.Read())
                {
                    list.Add(reader.GetDateTime(0).Day);
                }

                return list;
            }
        }
		#endregion  成员方法


        
	}
}
