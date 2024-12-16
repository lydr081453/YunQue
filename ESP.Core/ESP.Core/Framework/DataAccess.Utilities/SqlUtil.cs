using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.DataAccess.Utilities
{
    /// <summary>
    /// SQL操作工具
    /// </summary>
    public static class SqlUtil
    {
        /// <summary>
        /// 向DbCommand内添加DbParameter数组
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dbps"></param>
        public static void SetParameters(System.Data.Common.DbCommand cmd, System.Data.Common.DbParameter[] dbps)
        {
            if (cmd == null || cmd.Parameters == null || dbps == null || dbps.Length == 0)
            {
                return;
            }
            for (int i = 0; i < dbps.Length; ++i)
            {
                cmd.Parameters.Add(dbps[i]);
            }
        }
    }
}
