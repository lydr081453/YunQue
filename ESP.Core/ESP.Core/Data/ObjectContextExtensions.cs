using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Common;

namespace ESP.Data
{
    /// <summary>
    /// 扩展方法。
    /// </summary>
    public static class ObjectContextExtensions
    {
        /// <summary>
        /// 获取实体类型 T 对应的实体集的查询。
        /// 如果实体类型对应多个实体集， 则返回任意一个。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IQueryable<T> GetTable<T>(this ObjectContext context)
        {
            var typeName = typeof(T).FullName;
            var queryString = ESP.Configuration.ConfigurationManager.EntitySets[typeName];

            return context.CreateQuery<T>(queryString);
        }
    }
}
