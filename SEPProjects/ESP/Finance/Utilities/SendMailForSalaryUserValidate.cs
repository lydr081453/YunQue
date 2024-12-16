using System;
using System.Collections.Generic;
using System.Collections;

namespace ESP.Finance.Utility
{
    public class SendMailForSalaryUserValidate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMailHelper"/> class.
        /// </summary>
        public SendMailForSalaryUserValidate()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static void SendMailToUser(int userid, string pwd)
        {
            if (!string.IsNullOrEmpty(pwd) && userid > 0)
            {
                string msgBody = "您新的工资系统验证密码为：" + pwd;
                string mail=new ESP.Compatible.Employee(userid).EMail;
                    if(!string.IsNullOrEmpty(mail))
                ESP.Finance.Utility.SendMail.Send1("工资系统验证密码（极为重要）",mail , msgBody, false, "");
            }
        }
    }
}
