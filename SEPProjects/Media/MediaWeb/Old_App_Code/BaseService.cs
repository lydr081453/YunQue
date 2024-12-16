using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Media.Service;

namespace Media.Service
{
    /// <summary>
    ///BaseService 的摘要说明
    /// </summary>
    public abstract class BaseService : System.Web.Services.WebService
    {
        private CredentialSoapHeader credentials;
        public CredentialSoapHeader Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        protected bool validCredential()
        {
            return true;
        }

        public BaseService()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
    }
}