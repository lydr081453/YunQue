using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.DataAccess
{
    /// <summary>
    /// 待办事宜抽象数据访问接口
    /// </summary>
    [ESP.Configuration.Provider]
    public interface ITaskItemDataProvider
    {
        /// <summary>
        /// 获取指定ID的用户的待办事宜列表
        /// 从Web缓存中
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="userId">用户ID</param>
        /// <returns>待办事宜列表</returns>
        IDictionary<string, IList<ESP.Framework.Entity.TaskItemInfo>> GetTaskItems(string key, int userId);

        /// <summary>
        /// 获取所有人的所有代办事宜
        /// 存入Web缓存中
        /// </summary>
        /// <returns></returns>
        IDictionary<int, IDictionary<string, IList<ESP.Framework.Entity.TaskItemInfo>>> GetAllTaskItems();
    }
}
