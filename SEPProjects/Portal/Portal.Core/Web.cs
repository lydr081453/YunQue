using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Core
{
    public class Web
    {
        private static object _locker = new object();

        /// <summary>
        /// 记录最近到访者
        /// </summary>
        /// <param name="user"></param>
        /// <param name="visitor"></param>
        public static void VisitUserSpace(int user, int visitor)
        {
            if (user == visitor) return;
            if (System.Web.HttpContext.Current.Application[Common.Global.USER_AND_VISITOR_KEY] == null)
            {
                lock (_locker)
                {
                    if (System.Web.HttpContext.Current.Application[Common.Global.USER_AND_VISITOR_KEY] == null)
                    {
                        System.Web.HttpContext.Current.Application.Add(Common.Global.USER_AND_VISITOR_KEY, new Dictionary<int, Common.UniqueQueue<int>>());
                    }
                }
            }
            Dictionary<int, Common.UniqueQueue<int>> visitorDict = System.Web.HttpContext.Current.Application[Common.Global.USER_AND_VISITOR_KEY] as Dictionary<int, Common.UniqueQueue<int>>;
            if (visitorDict.ContainsKey(user))
            {
                visitorDict[user].Enqueue(visitor);
            }
            else
            {
                Common.UniqueQueue<int> q = new Common.UniqueQueue<int>(Common.Global.RECENT_VISITOR_COUNT);
                q.Enqueue(visitor);
                visitorDict.Add(user, q);
            }
        }

        /// <summary>
        /// 获取某个用户的最近来访者
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<int> GetMyVisitor(int user)
        {
            if (System.Web.HttpContext.Current.Application[Common.Global.USER_AND_VISITOR_KEY] == null)
            {
                lock (_locker)
                {
                    if (System.Web.HttpContext.Current.Application[Common.Global.USER_AND_VISITOR_KEY] == null)
                    {
                        System.Web.HttpContext.Current.Application.Add(Common.Global.USER_AND_VISITOR_KEY, new Dictionary<int, Common.UniqueQueue<int>>());
                    }
                }
            }
            Dictionary<int, Common.UniqueQueue<int>> visitorDict = System.Web.HttpContext.Current.Application[Common.Global.USER_AND_VISITOR_KEY] as Dictionary<int, Common.UniqueQueue<int>>;
            if (visitorDict.ContainsKey(user))
            {
                return visitorDict[user].List;
            }
            else
            {
                Common.UniqueQueue<int> q = new Common.UniqueQueue<int>(Common.Global.RECENT_VISITOR_COUNT);
                visitorDict.Add(user, q);
                return q.List;
            }
        }

        /// <summary>
        /// 用户访问记录
        /// </summary>
        /// <param name="r"></param>
        /// <param name="visitor"></param>
        public static void Visit(Portal.Core.Page p, int visitor)
        {
            #region 访问用户空间
            if (p.IsUserSpace && (p.Interviewee > 0) && visitor > 0 && (p.Interviewee != visitor))
            {
                Web.VisitUserSpace(p.Interviewee, visitor);
            }
            #endregion
        }
    }
}
