using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

using Portal.Data.Provider;
using Portal.Model;
using Portal.Common;

namespace Portal.Task
{
    public class TwitterTask : BaseTask
    {
        /// <summary>
        /// 任务逻辑
        /// </summary>
        public override void Execute()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("TwitterTask");
#endif
            //List<ITwitterMessage> list = Message.GetInstance().GetPublicTimeLine(20);//这里写定取前20条记录。应该设置到配置文件中（Task.config），将来实现！
            //string data = string.Empty;
            //System.Web.HttpRuntime.Cache.Remove(Global.TWITTER_PUBLIC_TIME_LINE_KEY);
            //System.Web.HttpRuntime.Cache.Add(Global.TWITTER_PUBLIC_TIME_LINE_KEY, list, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
        }
    }
}
