using System;
using System.Collections.Generic;
using System.Web;


namespace ESP.Security
{
    /// <summary>
    ///根据Url对请求进行授权的Http模块
    /// </summary>
    public class UrlAuthorizationModule : IHttpModule
    {
        #region IHttpModule 成员

        /// <summary>
        /// 释放当前对象
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 初始化当前对象
        /// </summary>
        /// <param name="context">当前的HttpApplication实例</param>
        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += new EventHandler(OnEnter);
        }

        void OnEnter(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            if (!context.SkipAuthorization)
            {
                if(!UrlAuthorization.IsUserAllowed(context.User, context.Request.Url, context.Request.RequestType))
                {
                    context.Response.StatusCode = 401;

                    application.CompleteRequest();
                }
            }


        }

        #endregion
    }
}