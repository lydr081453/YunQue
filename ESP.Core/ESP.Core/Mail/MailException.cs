using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Mail
{
    /// <summary>
    /// 
    /// </summary>
    public class MailException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the System.ApplicationException class with
        /// a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public MailException(string message)
            : base(message)
        {
        }
    }
}
