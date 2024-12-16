using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Administrative.BusinessLogic
{
    /// <summary>
    /// 考勤统计数据业务操作类
    /// </summary>
    public class AttendanceStatisticManager
    {
        private readonly AttendanceStatisticInfoDataProvider dal = new AttendanceStatisticInfoDataProvider();
        public AttendanceStatisticManager()
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
        public int Add(AttendanceStatisticInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(AttendanceStatisticInfo model)
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
        public AttendanceStatisticInfo GetModel(int ID)
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
        /// 获得考勤统计信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回考勤统计信息集合</returns>
        public DataSet GetAttendanceStatisticInfo(int userId, string strwhere, List<SqlParameter> parameterList)
        {
            UserAttBasicInfoManager manager = new UserAttBasicInfoManager();
            string userids = manager.GetStatUserIDs(userId);
            return GetList(" userid in (" + userids + ") " + strwhere, parameterList);
        }

        /// <summary>
        /// 删除某月的考勤统计信息
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        public void DeleteAttendanceStatInfos(int year, int month)
        {
            dal.DeleteAttendanceStatInfos(year, month);
        }

        /// <summary>
        /// 获得考勤统计信息
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public AttendanceStatisticInfo GetAttendanceStatisticModel(int UserID, int year, int month)
        {
            return dal.GetAttendanceStatisticModel(UserID, year, month);
        }

        /// <summary>
        /// 获得考勤统计信息
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <param name="parameterList">查询参数</param>
        /// <returns>返回考勤统计信息集合</returns>
        public DataSet GetList(string strwhere, List<SqlParameter> parameterList)
        {
            return dal.GetList(strwhere, parameterList);
        }

        /// <summary>
        /// 获得考勤提醒人员信息列表。
        /// </summary>
        /// <param name="year">年份。</param>
        /// <param name="month">月份。</param>
        public DataSet GetAttendanceRemind(int year, int month)
        {
            return dal.GetAttendanceRemind(year, month);
        }

        #endregion  成员方法
    }
}