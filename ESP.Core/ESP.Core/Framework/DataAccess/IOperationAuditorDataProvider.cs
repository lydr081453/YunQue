using System;
namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 审核人管理
    /// </summary>
    [ESP.Configuration.Provider]
    public interface IOperationAuditorDataProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int Add(ESP.Framework.Entity.OperationAuditorInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        void Delete(int id);

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        bool Exists(int id);

        /// <summary>
        /// 获得考勤审批人的sysids
        /// </summary>
        /// <returns></returns>
        int[] GetAllAttendanceIds();

        /// <summary>
        /// 获得CEO的sysids
        /// </summary>
        /// <returns></returns>
        int[] GetAllCEOIds();

        /// <summary>
        /// 获得总监的sysids
        /// </summary>
        /// <returns></returns>
        int[] GetAllDirectorIds();

        /// <summary>
        /// 获得HR审批人的sysids
        /// </summary>
        /// <returns></returns>
        int[] GetAllHRIds();

        /// <summary>
        /// 获得总经理的sysids
        /// </summary>
        /// <returns></returns>
        int[] GetAllManagerIds();
        
        /// <summary>
        /// 获得行政管理员的sysids
        /// </summary>
        /// <returns></returns>
        int[] GetAllADManagerIds();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        System.Collections.Generic.IList<ESP.Framework.Entity.OperationAuditorInfo> GetList(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        ESP.Framework.Entity.OperationAuditorInfo GetModel(int id);

        /// <summary>
        /// 根据部门ID获得一个对象实体
        /// </summary>
        /// <param name="departmentId">The dep id.</param>
        /// <returns></returns>
        ESP.Framework.Entity.OperationAuditorInfo GetModelByDepId(int departmentId);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(ESP.Framework.Entity.OperationAuditorInfo model);
    }
}
