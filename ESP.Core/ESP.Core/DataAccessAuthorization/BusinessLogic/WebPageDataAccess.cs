using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.DataAccessAuthorization.BusinessLogic
{
    /// <summary>
    /// 数据表单权限操作业务类
    /// </summary>
    public static class WebPageDataAccess
    {
        #region Data Provider
        private static DataAccess.IWebPageDataAccess DataProvider
        {
            get
            {
                return ESP.Configuration.ProviderHelper<DataAccess.IWebPageDataAccess>.Instance;
            }
        }
        #endregion

        /// <summary>
        /// 新建权限操作
        /// 如果新建重复的权限操作会捕捉到相应的异常，并返回（-1）
        /// </summary>
        /// <param name="model"></param>
        /// <returns>如果新建成功返回新建立的权限操作序号，如果新建失败也没有引发异常则返回（0），插入重复数据则会引发异常返回（-1）</returns>
        public static int NewMember(Entity.WebPageDataAccess model)
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
        public static void UpdateAction(Entity.WebPageDataAccess model)
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
        /// 获取页面的数据权限配置列表
        /// </summary>
        /// <param name="webPageId"></param>
        /// <returns></returns>
        public static IList<Entity.DataAccessAction> GetWebPageDataAccessActionList(int webPageId)
        {
            return DataProvider.GetDataAccessActionList(webPageId);
        }
    }
}
