using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.BusinessLogic.Maintenance
{
    /// <summary>
    /// 运维操作业务逻辑类
    /// 根据一个项目号或者流水号查询此项目内所有业务
    /// 1.接受项目号或者流水号
    /// 2.查出项目主申请方信息
    /// 3.查出项目支持方信息
    /// 4.查出项目内全部PR单，并根据申请方分大类，根据业务类型（BusinessType）分小类显示，并标明哪些是手填的项目号
    /// 5.查出项目内全部PR单的收货信息
    /// 6.查出项目内全部PR单的PN信息
    /// 7.查出项目内全部PR单的PA信息
    /// 8.标明所有单据的下一步走向
    /// 注意：如果是媒体稿件费用或对私费用，需要将新生成的单据也要全部显示出来
    /// </summary>
    public partial class Operation
    {
        /// <summary>
        /// 获取项目快照
        /// </summary>
        /// <param name="projectId">项目流水号</param>
        /// <returns></returns>
        public Entity.ProjectSnapshot GetProjectSnapshot(int projectId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取项目快照
        /// </summary>
        /// <param name="projectNo">项目号</param>
        /// <returns></returns>
        public Entity.ProjectSnapshot GetProjectSnapshot(string projectNo)
        {
            throw new NotImplementedException();
        }

        #region 查询项目主申请方信息
        /// <summary>
        /// 查询项目主申请方信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        internal ESP.Finance.Entity.ProjectInfo GetProjectMainInfo(int projectId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询项目主申请方信息
        /// </summary>
        /// <param name="projectNo"></param>
        /// <returns></returns>
        internal ESP.Finance.Entity.ProjectInfo GetProjectMainInfo(string projectNo)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询项目主申请方成员信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        internal IList<ESP.Finance.Entity.ProjectMemberInfo> GetProjectMainMemberList(int projectId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 查询项目支持方信息
        /// <summary>
        /// 查询支持方快照信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        internal IList<Entity.SupportInfoSnapshot> GetSupportInfoList(int projectId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
