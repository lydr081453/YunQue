using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.DataAccessAuthorization.BusinessLogic
{
    /// <summary>
    /// WebPage要求某些权限（按照所有权限要求的并集去检查，不满足全部权限就不可进入，并提示错误）
    /// WebPage要求某个具体权限时都需要指定一个具体的AccessMember对象，如果同一个权限允许多个AccessMember对象访问，那么应该配置多条
    /// </summary>
    public class DataAccessAuthorization
    {
        /// <summary>
        /// 检查权限并返回当前用户可以进行的Action，以及当前Action允许的Member定义
        /// 注意：页面需要声明自己支持的操作（Action），
        /// 然后配置允许此种Action的Member，
        /// 而以下判断规则是根据配置决定的，
        /// 如果没有配置相应的Member则不进行检查。
        /// 1.判断是否属于允许的部门（无数据库操作，完全从配置中读取）
        /// 2.判断是否属于允许的角色（读取sep_UsersInRoles，完全从配置中读取）
        /// 3.判断是否大于某个职务级别（无数据库操作，完全从配置中读取）
        /// 4.判断是否属于允许的职务（无数据库操作，完全从配置中读取）
        /// 以下需要业务关联，目前完全由业务逻辑负责检查
        /// ×.判断是否为Owner（无数据库操作，需要定义公共属性）
        /// ×.判断是否属于支持方（读取F_ProjectMember，与业务相关）
        /// ×.判断是否属于审批人员（读取MODELTASK，与业务相关）
        /// </summary>
        /// <param name="member"></param>
        /// <param name="actionList"></param>
        /// <returns></returns>
        public static Dictionary<Entity.DataAccessAction, IList<Entity.DataAccessMember>> Authorization(ESP.Framework.Entity.MemberInfo member, IList<Entity.DataAccessAction> actionList)
        {
            Dictionary<Entity.DataAccessAction, IList<Entity.DataAccessMember>> DataAccessPermission = new Dictionary<ESP.DataAccessAuthorization.Entity.DataAccessAction, IList<ESP.DataAccessAuthorization.Entity.DataAccessMember>>();

            #region 权限判定
            foreach (Entity.DataAccessAction action in actionList)
            {
                foreach (Entity.DataAccessMember m in action.DataAccessMemberList)
                {
                    //如果是角色
                    //如果是部门
                    //如果是职务
                    //如果是职务级别
                }
            }
            #endregion

            return DataAccessPermission;
        }
    }
}
