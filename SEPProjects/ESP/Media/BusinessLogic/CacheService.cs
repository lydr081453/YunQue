﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3053
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// 此源代码由 wsdl 自动生成, Version=2.0.50727.3038。
// 
namespace MediaLib.Service
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "CacheServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class CacheService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private CredentialSoapHeader credentialSoapHeaderValueField;

        private System.Threading.SendOrPostCallback insertNewCacheOperationCompleted;

        private System.Threading.SendOrPostCallback updateCacheOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteCacheOperationCompleted;

        private System.Threading.SendOrPostCallback GetCacheModelOperationCompleted;

        /// <remarks/>
        public CacheService(string url)
        {
            this.Url = url;
        }

        public CredentialSoapHeader CredentialSoapHeaderValue
        {
            get
            {
                return this.credentialSoapHeaderValueField;
            }
            set
            {
                this.credentialSoapHeaderValueField = value;
            }
        }

        /// <remarks/>
        public event insertNewCacheCompletedEventHandler insertNewCacheCompleted;

        /// <remarks/>
        public event updateCacheCompletedEventHandler updateCacheCompleted;

        /// <remarks/>
        public event DeleteCacheCompletedEventHandler DeleteCacheCompleted;

        /// <remarks/>
        public event GetCacheModelCompletedEventHandler GetCacheModelCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("CredentialSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/insertNewCache", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int insertNewCache(T_Cache cache)
        {
            object[] results = this.Invoke("insertNewCache", new object[] {
                    cache});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegininsertNewCache(T_Cache cache, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("insertNewCache", new object[] {
                    cache}, callback, asyncState);
        }

        /// <remarks/>
        public int EndinsertNewCache(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void insertNewCacheAsync(T_Cache cache)
        {
            this.insertNewCacheAsync(cache, null);
        }

        /// <remarks/>
        public void insertNewCacheAsync(T_Cache cache, object userState)
        {
            if ((this.insertNewCacheOperationCompleted == null))
            {
                this.insertNewCacheOperationCompleted = new System.Threading.SendOrPostCallback(this.OninsertNewCacheOperationCompleted);
            }
            this.InvokeAsync("insertNewCache", new object[] {
                    cache}, this.insertNewCacheOperationCompleted, userState);
        }

        private void OninsertNewCacheOperationCompleted(object arg)
        {
            if ((this.insertNewCacheCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insertNewCacheCompleted(this, new insertNewCacheCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("CredentialSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/updateCache", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int updateCache(T_Cache cache)
        {
            object[] results = this.Invoke("updateCache", new object[] {
                    cache});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginupdateCache(T_Cache cache, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("updateCache", new object[] {
                    cache}, callback, asyncState);
        }

        /// <remarks/>
        public int EndupdateCache(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void updateCacheAsync(T_Cache cache)
        {
            this.updateCacheAsync(cache, null);
        }

        /// <remarks/>
        public void updateCacheAsync(T_Cache cache, object userState)
        {
            if ((this.updateCacheOperationCompleted == null))
            {
                this.updateCacheOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateCacheOperationCompleted);
            }
            this.InvokeAsync("updateCache", new object[] {
                    cache}, this.updateCacheOperationCompleted, userState);
        }

        private void OnupdateCacheOperationCompleted(object arg)
        {
            if ((this.updateCacheCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateCacheCompleted(this, new updateCacheCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("CredentialSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteCache", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteCache(int sid)
        {
            object[] results = this.Invoke("DeleteCache", new object[] {
                    sid});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginDeleteCache(int sid, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("DeleteCache", new object[] {
                    sid}, callback, asyncState);
        }

        /// <remarks/>
        public int EndDeleteCache(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void DeleteCacheAsync(int sid)
        {
            this.DeleteCacheAsync(sid, null);
        }

        /// <remarks/>
        public void DeleteCacheAsync(int sid, object userState)
        {
            if ((this.DeleteCacheOperationCompleted == null))
            {
                this.DeleteCacheOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteCacheOperationCompleted);
            }
            this.InvokeAsync("DeleteCache", new object[] {
                    sid}, this.DeleteCacheOperationCompleted, userState);
        }

        private void OnDeleteCacheOperationCompleted(object arg)
        {
            if ((this.DeleteCacheCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteCacheCompleted(this, new DeleteCacheCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("CredentialSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCacheModel", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public T_Cache GetCacheModel(int generalid, int projectid, string projectcode, int sysuserid)
        {
            object[] results = this.Invoke("GetCacheModel", new object[] {
                    generalid,
                    projectid,
                    projectcode,
                    sysuserid});
            return ((T_Cache)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetCacheModel(int generalid, int projectid, string projectcode, int sysuserid, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetCacheModel", new object[] {
                    generalid,
                    projectid,
                    projectcode,
                    sysuserid}, callback, asyncState);
        }

        /// <remarks/>
        public T_Cache EndGetCacheModel(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((T_Cache)(results[0]));
        }

        /// <remarks/>
        public void GetCacheModelAsync(int generalid, int projectid, string projectcode, int sysuserid)
        {
            this.GetCacheModelAsync(generalid, projectid, projectcode, sysuserid, null);
        }

        /// <remarks/>
        public void GetCacheModelAsync(int generalid, int projectid, string projectcode, int sysuserid, object userState)
        {
            if ((this.GetCacheModelOperationCompleted == null))
            {
                this.GetCacheModelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCacheModelOperationCompleted);
            }
            this.InvokeAsync("GetCacheModel", new object[] {
                    generalid,
                    projectid,
                    projectcode,
                    sysuserid}, this.GetCacheModelOperationCompleted, userState);
        }

        private void OnGetCacheModelOperationCompleted(object arg)
        {
            if ((this.GetCacheModelCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCacheModelCompleted(this, new GetCacheModelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
    public partial class CredentialSoapHeader : System.Web.Services.Protocols.SoapHeader
    {

        private string userNameField;

        private string passwordField;

        private System.Xml.XmlAttribute[] anyAttrField;

        /// <remarks/>
        public string UserName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        /// <remarks/>
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr
        {
            get
            {
                return this.anyAttrField;
            }
            set
            {
                this.anyAttrField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class T_Cache
    {

        private int projectIDField;

        private int sIDField;

        private string projectCodeField;

        private int generalIDField;

        private int sysUserIDField;

        /// <remarks/>
        public int ProjectID
        {
            get
            {
                return this.projectIDField;
            }
            set
            {
                this.projectIDField = value;
            }
        }

        /// <remarks/>
        public int SID
        {
            get
            {
                return this.sIDField;
            }
            set
            {
                this.sIDField = value;
            }
        }

        /// <remarks/>
        public string projectCode
        {
            get
            {
                return this.projectCodeField;
            }
            set
            {
                this.projectCodeField = value;
            }
        }

        /// <remarks/>
        public int GeneralID
        {
            get
            {
                return this.generalIDField;
            }
            set
            {
                this.generalIDField = value;
            }
        }

        /// <remarks/>
        public int SysUserID
        {
            get
            {
                return this.sysUserIDField;
            }
            set
            {
                this.sysUserIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void insertNewCacheCompletedEventHandler(object sender, insertNewCacheCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insertNewCacheCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insertNewCacheCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void updateCacheCompletedEventHandler(object sender, updateCacheCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class updateCacheCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal updateCacheCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void DeleteCacheCompletedEventHandler(object sender, DeleteCacheCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteCacheCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal DeleteCacheCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetCacheModelCompletedEventHandler(object sender, GetCacheModelCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCacheModelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetCacheModelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public T_Cache Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((T_Cache)(this.results[0]));
            }
        }
    }
}