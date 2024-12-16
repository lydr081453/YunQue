using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ESP.Finance.BusinessLogic
{
     
     
    public static class CheckManager
    {
        private static ESP.Finance.IDataAccess.ICheckDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICheckDataProvider>.Instance;}}
        private const string tablename = "CheckInfo";

        

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.CheckInfo GetModel(int checkId)
        {
            Entity.CheckInfo check = DataProvider.GetModel(checkId);
            return check;
        }


        public static IList<ESP.Finance.Entity.CheckInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        #region ICheckProvider 成员
         
         
         
        public static int Add(ESP.Finance.Entity.CheckInfo model)
        {
            //CheckSysCode,CheckCode
            if (!DataProvider.Exists(model.CheckSysCode, model.CheckCode))
            {
                return DataProvider.Add(model);
            }
            else
                return 0;
        }

        public static int Add(ESP.Finance.Entity.CheckInfo model,SqlTransaction trans)
        {
            //CheckSysCode,CheckCode
            if (!DataProvider.Exists(model.CheckSysCode, model.CheckCode,trans))
            {
                return DataProvider.Add(model,trans);
            }
            else
                return 0;
        }

       public static int Add(List<ESP.Finance.Entity.CheckInfo> models)
        {
            if (models == null || models.Count == 0) return 0;
            int counter = 0;
            using (SqlConnection conn = new SqlConnection(DataAccess.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (Entity.CheckInfo model in models)
                    {
                        int res = Add(model);
                        if (res == 0)
                        {
                            throw new Exception("CheckCode 已存在");
                        }
                        else if (res > 0)
                        {
                            counter++;
                        }
                    }
                    trans.Commit();
                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return counter;
        }

        public static ESP.Finance.Utility.UpdateResult Update(ESP.Finance.Entity.CheckInfo model)
        {
            int res = 0;
            try
            {
                //trans//res = DataProvider.Update(model,true);
                res = DataProvider.Update(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.UpdateResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.UpdateResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.UpdateResult.UnExecute;
            }
            return ESP.Finance.Utility.UpdateResult.Failed;
        }

        public static ESP.Finance.Utility.DeleteResult Delete(int checkId)
        {
            int res = 0;
            try
            {
                res = DataProvider.Delete(checkId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ESP.Finance.Utility.DeleteResult.Failed;
            }
            if (res > 0)
            {
                return ESP.Finance.Utility.DeleteResult.Succeed;
            }
            else if (res == 0)
            {
                return ESP.Finance.Utility.DeleteResult.UnExecute;
            }
            return ESP.Finance.Utility.DeleteResult.Failed;
        }

        public static IList<ESP.Finance.Entity.CheckInfo> GetAllList()
        {
            return GetList(string.Empty, null);
        }

        public static IList<ESP.Finance.Entity.CheckInfo> GetList(string term)
        {
            return GetList(term, null);
        }

        #endregion
    }
}
