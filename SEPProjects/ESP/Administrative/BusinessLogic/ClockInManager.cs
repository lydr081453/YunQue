using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;

namespace ESP.Administrative.BusinessLogic
{
    public class ClockInManager
    {
        private readonly ClockInDataProvider dal = new ClockInDataProvider();
        public ClockInManager()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

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
        public int Add(ClockInInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.ClockInInfo model)
        {
            dal.Update(model);
        }
        /// <summary>
        /// 更新多少记录
        /// </summary>
        /// <param name="newUserCode"></param>
        /// <param name="oldUserCode"></param>
        public void Update(string newUserCode, string oldUserCode) 
        {
            dal.Update(newUserCode,oldUserCode);
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
        public ESP.Administrative.Entity.ClockInInfo GetModel(int ID)
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
        /// 获得员工上下班时间
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="AttendanceDate"></param>
        /// <param name="GoWorkTime"></param>
        /// <param name="OffWorkTime"></param>
        public void GetAttendanceTime(int Userid, DateTime AttendanceDate, out DateTime GoWorkTime, out DateTime OffWorkTime)
        {
            GoWorkTime = new DateTime(1900, 1, 1);
            OffWorkTime = new DateTime(1900, 1, 1);
            dal.GetAttendanceTime(Userid, AttendanceDate, out GoWorkTime, out OffWorkTime);
        }

        /// <summary>
        /// 获得用户上下班时间集合
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户上下班时间集合</returns>
        public System.Collections.Hashtable GetClockInTimesOfMonth(int year, int month, int userId)
        {
            return dal.GetClockInTimesOfMonth(year, month, userId);
        }

        /// <summary>
        /// 获得统计人员的打卡时间信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="day1">日期，如果没有按日期查询则传一个0</param>
        /// <returns>返回所有统计人员的打卡记录信息</returns>
        public Dictionary<int, Dictionary<long, DateTime>> GetClockInTimes(int year, int month, int day1)
        {
            return dal.GetClockInTimes(year, month, day1);
        }

        /// <summary>
        /// 获得当前传入日期的前后三天的上下班时间
        /// </summary>
        /// <param name="time">当前选择的日期</param>
        /// <param name="userId">用户编号</param>
        /// <returns>返回前后三天的上下班时间</returns>
        public Dictionary<int, DateTime> GetClockInTimes(DateTime time, int userId)
        {
            return dal.GetClockInTimes(time, userId);
        }

        /// <summary>
        /// 通过员工编号获得用户信息
        /// </summary>
        /// <param name="userCode">员工编号</param>
        /// <returns>返回用户信息</returns>
        public ESP.Framework.Entity.EmployeeInfo GetEmployeeInfoByCode(String userCode)
        {
            return dal.GetEmployeeInfoByCode(userCode);
        }
        #endregion  成员方法
    }
}

