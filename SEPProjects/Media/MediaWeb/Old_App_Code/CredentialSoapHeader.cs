using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Media.Service
{
    /// <summary>
    ///CredentialSoapHeader 的摘要说明
    /// </summary>
    public class CredentialSoapHeader : SoapHeader
    {
        private string username;
        private string password;

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

}