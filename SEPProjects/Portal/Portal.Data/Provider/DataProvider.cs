using System;
using System.Text;
using System.Reflection;

namespace Portal.Data.Provider
{
    public class DataProvider
    {
        private DataProvider()
        { }

        //private static IDatabaseProvider _databaseinst = null;
        //private static IOtherDataProvider _otherdatainst = null;
        private static object lockHelper = new object();

        static DataProvider()
        {
            GetProvider();
        }

        private static void GetProvider()
        {
            try
            {
                //string value = ESP.Configuration.ConfigurationManager.SafeAppSettings[Portal.Common.Global.PORTAL_DATABASE_PROVIDER];
                //_databaseinst = (IDatabaseProvider)Activator.CreateInstance(Type.GetType(string.Format("Portal.Data.{0}.DataProvider, Portal.Data.{0}", value)));
                //string value = ESP.Configuration.ConfigurationManager.SafeAppSettings[Portal.Common.Global.PORTAL_OTHER_DATA_PROVIDER];
                //_otherdatainst = (IOtherDataProvider)Activator.CreateInstance(Type.GetType(string.Format("Portal.Data.{0}.DataProvider, Portal.Data.{0}", value)));
            }
            catch(Exception e)
            {
                e.ToString();
                throw new Exception(string.Format("请检查web.config中{0}节点数据库类型是否正确", Portal.Common.Global.PORTAL_DATABASE_PROVIDER));
            }
        }

        //public static IDatabaseProvider GetDatabaseInstance()
        //{
        //    if (_databaseinst == null)
        //    {
        //        lock (lockHelper)
        //        {
        //            if (_databaseinst == null)
        //            {
        //                GetProvider();
        //            }
        //        }
        //    }
        //    return _databaseinst;
        //}

        //public static IOtherDataProvider GetWebDataInstance()
        //{
        //    if (_otherdatainst == null)
        //    {
        //        lock (lockHelper)
        //        {
        //            if (_otherdatainst == null)
        //            {
        //                GetProvider();
        //            }
        //        }
        //    }
        //    return _otherdatainst;
        //}

        public static void ResetDbProvider()
        {
            //_otherdatainst = null;
            //_databaseinst = null;
        }
    }
}