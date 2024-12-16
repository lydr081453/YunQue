using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.Common
{
    public class Rule
    {

        #region IRule 成员

        public static ErrorMessage Check()
        {
            return new ErrorMessage();
        }

        public static string ErrorMessageFormat(ErrorMessage message)
        {
            return message.Description;
        }

        #endregion
    }
}
