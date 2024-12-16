using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.BusinessLogic
{
    public static class UserManagerEx
    {
        /// <summary>
        /// 返回指定 ID 列表的用户的 ID 与中文全名的对照表。
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetUserNames(int[] userIds)
        {

            Dictionary<int, string> dict = new Dictionary<int, string>();
            if (userIds == null || userIds.Length == 0)
                return dict;

            int start = 0;

            while (start < userIds.Length)
            {
                int count = userIds.Length - start;
                if (count > 200)
                    count = 200;

                StringBuilder sql = new StringBuilder(@"
select UserId, LastNameCN + FirstNameCN
from sep_Users
where UserID in (
");
                sql.Append(userIds[start]);
                for (var i = 1; i < count; i++)
                {
                    sql.Append(',').Append(userIds[start + i]);
                }
                sql.Append(@"
)
");
                using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
                {
                    while (reader.Read())
                    {
                        var uid = reader.GetInt32(0);
                        var name = reader.GetString(1);
                        dict[uid] = name;
                    }
                }

                start += count;
            }

            return dict;
        }
    }
}
