using System;
using System.Collections.Generic;
using System.Text;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 数据版本已更改异常
    /// </summary>
    /// <remarks>
    /// 在捕获到该异常后应重新加载数据
    /// </remarks>
    public class UnmatchedRowVersionException : Exception
    {
    }
}
