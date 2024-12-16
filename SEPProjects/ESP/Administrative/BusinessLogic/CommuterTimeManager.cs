using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Common;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Administrative.BusinessLogic
{
    public class CommuterTimeManager
    {
        private readonly CommuterTimeDataProvider dal = new CommuterTimeDataProvider();
        public CommuterTimeManager()
        { }
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CommuterTimeInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(CommuterTimeInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public CommuterTimeInfo GetModel(int ID)
		{
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return dal.GetList("");
		}
        
        /// <summary>
        /// 通过用户编号获得用户的上下班时间信息集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户上下班时间信息集合</returns>
        public List<CommuterTimeInfo> GetCommuterTimeByUserId(int userId)
        {
            return dal.GetCommuterTimeByUserId(userId);
        }

        public int UpdateUserCommuteType(int userId, int attendanceType)
        {
            return dal.UpdateUserCommuteType(userId, attendanceType);
        }

        public int UpdateUserCommuteType(int userId, int attendanceType,SqlTransaction trans )
        {
            return dal.UpdateUserCommuteType(userId, attendanceType, trans);
        }

        /// <summary>
        /// 获得用户某年某月的上下班时间信息集合
        /// </summary>
        /// <param name="list">用户上下班时间信息集合</param>
        /// <param name="dateTime">考勤统计的时间</param>
        public CommuterTimeInfo GetCommuterTimes(List<CommuterTimeInfo> list, DateTime dateTime)
        {
            CommuterTimeInfo model = new CommuterTimeInfo();
            model.AttendanceType = Status.UserBasicAttendanceType_Normal;
            model.BeginTime = Status.systemStratTime;
            model.EndTime = DateTime.Now.Date;
            model.GoWorkTime = Status.DefaultGoWorkTime;
            model.OffWorkTime = Status.DefaultOffWorkTime;
            if (list != null && list.Count > 0)
            {
                foreach (CommuterTimeInfo m in list)
                {
                    if (m.EndTime == null || DateTime.MinValue.Date == m.EndTime.Value.Date)
                    {
                        if (dateTime.Date >= DateTime.Now.Date)
                            m.EndTime = dateTime.Date;
                        else
                        {
                            DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                            m.EndTime = d1.Date;
                        }
                    }
                    if ((dateTime.Date >= m.BeginTime.Value.Date && dateTime.Date <= m.EndTime.Value.Date))
                    {
                        model = m;
                    }
                }
            }
            return model;
        }
		#endregion  成员方法
	}
}