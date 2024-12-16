using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.BusinessLogic;
using System.Web.UI;
using ESP.Framework.Entity;

namespace ESP.Web.UI
{

    /// <summary>
    /// 用户控件的扩展基类
    /// </summary>
    public class UserControlBase : UserControl
    {
        /// <summary>
        /// 当前用户的ID
        /// </summary>
        public int UserID
        {
            get { return UserManager.GetCurrentUserID(); }
        }

        /// <summary>
        /// 当前控件所在的页面（如果页面不是PageBase类型，则返回空引用）。
        /// </summary>
        public PageBase SEPPage
        {
            get { return this.Page as PageBase; }
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                PageBase p = this.SEPPage;
                if (p != null)
                {
                    return p.UserInfo;
                }

                return UserManager.Get();
            }
        }

    }

    //public static class WebUIExtender
    //{
    //    public static int GetUserID(this Control control)
    //    {
    //        return UserManager.GetCurrentUserID();
    //    }

    //    public static PageBase GetSEPPage(this Control control)
    //    {
    //        return control.Page as PageBase;
    //    }

    //    public static UserInfo GetUserInfo(this Control control)
    //    {
    //        PageBase p = GetSEPPage(control);
    //        if (p != null)
    //        {
    //            return p.UserInfo;
    //        }

    //        return UserManager.Get();
    //    }
    //}
}
