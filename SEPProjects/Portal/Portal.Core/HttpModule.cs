using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Portal.Core
{
    public class HttpModule : System.Web.IHttpModule
    {
        public static object visitor_locker = new object();
        #region IHttpModule 成员

        public void Dispose()
        {
        }

        public void Init(System.Web.HttpApplication context)
        {
        }

        #endregion


    }
}
