using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.DataAccessAuthorization.BusinessLogic
{
    /// <summary>
    /// 权限成员业务类
    /// </summary>
    public static class DataAccessMember
    {
        #region Data Provider
        private static DataAccess.IDataAccessMember DataProvider
        {
            get
            {
                return ESP.Configuration.ProviderHelper<DataAccess.IDataAccessMember>.Instance;
            }
        }
        #endregion

        /// <summary>
        /// 新建权限操作
        /// 如果新建重复的权限操作会捕捉到相应的异常，并返回（-1）
        /// </summary>
        /// <param name="model"></param>
        /// <returns>如果新建成功返回新建立的权限操作序号，如果新建失败也没有引发异常则返回（0），插入重复数据则会引发异常返回（-1）</returns>
        public static int NewMember(Entity.DataAccessMember model)
        {
            try
            {
                return DataProvider.Add(model);
            }
            catch (System.Data.DuplicateNameException e)
            {
                e.ToString();
                return -1;
            }
        }

        /// <summary>
        /// 更新权限操作
        /// 如果更新的数据量不等于1都视为失败，由于没有规范的处理方法，所以抛出异常
        /// </summary>
        /// <param name="model"></param>
        public static void UpdateAction(Entity.DataAccessMember model)
        {
            int r = DataProvider.Update(model);
            if (r <= 0)
            {
                throw new Exception("更新失败，可能记录已经不存在或主键错误，请联系管理员！");
            }
            else if (r > 1)
            {
                throw new Exception("更新的记录不唯一，可能主键错误或程序错误，请联系管理员！");
            }
        }

        /// <summary>
        /// 根据主键获取一个权限操作实例对象
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static Entity.DataAccessMember Get(int memberId)
        {
            return DataProvider.GetModel(memberId);
        }
        /// <summary>
        /// 根据设定获取AccessMemberList
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IList<Entity.DataAccessMember> GetMemberListBySettings(IList<int> settings)
        {
            StringBuilder s = new StringBuilder();
            s.Append("(");
            foreach (int z in settings)
            {
                s.Append(z);
                s.Append(",");
            }
            if (s.ToString().EndsWith(","))
            {
                s.Remove(s.Length - 1, 1);
            }
            s.Append(")");
            return DataProvider.GetList(" DataAccessMemberID in " + s.ToString());
        }
    }
}
