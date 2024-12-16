using System.Data;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Data.SqlClient;

namespace ESP.Purchase.BusinessLogic
{
    public static class DataPermissionManager
    {
        private static DataPermissionProvider dal = new DataPermissionProvider();

        /// <summary>
        /// 检查用户是否对数据有编辑权限
        /// </summary>
        /// <param name="PrId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static bool CheckUserEditPermission(int dataTypeId, int dataId, int userId)
        {
            string  DelegateUserID = ",";
            IList<ESP.Framework.Entity.AuditBackUpInfo> backList =ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(userId);
           
            if (dal.isMaxPermissionUser(userId))
                return true;
            foreach (ESP.Framework.Entity.AuditBackUpInfo bak in backList)
            {
                DelegateUserID += bak.UserID.ToString() + ",";
                if (dal.isMaxPermissionUser(bak.UserID))
                    return true;
            }
            DataInfo dataInfoModel = dal.GetDataInfoModel(dataTypeId, dataId);
            if (dataInfoModel != null)
            {
                List<DataPermissionInfo> permissionList = dal.GetDataPermissionList(dataInfoModel.Id);
                foreach (DataPermissionInfo model in permissionList)
                {
                    if (model.IsEditor && (model.UserId == userId || DelegateUserID.IndexOf("," + model.UserId.ToString() + ",")>=0))
                        return true;
                }
                return false;
            }
            else
            {
                if (dataTypeId == (int)ESP.Purchase.Common.State.DataType.PR)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 检查用户是否对数据有查看权限
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <param name="dataId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckUserViewPermission(int dataTypeId, int dataId, int userId)
        {
            //if (userId == int.Parse(System.Configuration.ConfigurationSettings.AppSettings["TCGAssistant"].ToString()))
            //{
            //    userId = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["TCGManager"].ToString());
            //}
            if (dal.isMaxPermissionUser(userId))
                return true;
            string DelegateUserID = ",";
            IList<ESP.Framework.Entity.AuditBackUpInfo> backList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(userId);
            foreach (ESP.Framework.Entity.AuditBackUpInfo bak in backList)
            {
                DelegateUserID += bak.UserID.ToString() + ",";
                if (dal.isMaxPermissionUser(bak.UserID))
                    return true;
            }
            DataInfo dataInfoModel = dal.GetDataInfoModel(dataTypeId, dataId);
            if (dataInfoModel != null)
            {
                List<DataPermissionInfo> permissionList = dal.GetDataPermissionList(dataInfoModel.Id);
                foreach (DataPermissionInfo model in permissionList)
                {
                    if ((model.IsEditor || model.IsViewer) && (model.UserId == userId || DelegateUserID.IndexOf("," + model.UserId.ToString() + ",") >= 0))
                        return true;
                }
                
                return false;
            }
            else
            {
                if (dataTypeId == (int)ESP.Purchase.Common.State.DataType.PR)
                    return true;
                else
                    return false;
            }
        }

        public static void AddDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList)
        {
            dal.AddDataPermission(dataInfo, permissionList);
        }
        public static ESP.Purchase.Entity.DataInfo GetDataInfoModel(int dataType, int dataId, SqlTransaction trans)
        {
            return dal.GetDataInfoModel(dataType, dataId, trans);
        }

        public static void addDataPermissions(DataPermissionInfo model, SqlTransaction trans)
        {
            dal.addDataPermissions(model,trans);
        }

        public static void AddDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList,System.Data.SqlClient.SqlTransaction trans)
        {
            dal.AddDataPermission(dataInfo, permissionList,trans);
        }
        public static void AppendDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList)
        {
            dal.AppendDataPermission(dataInfo, permissionList);
        }
        public static void AppendDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList,System.Data.SqlClient.SqlTransaction trans)
        {
            dal.AppendDataPermission(dataInfo, permissionList,trans);
        }
        public static DataInfo GetDataInfoModel(int dataType, int dataId)
        {
            return dal.GetDataInfoModel(dataType, dataId);
        }
        public static List<DataPermissionInfo> GetDataPermissionList(int dataInfoId)
        {
            return dal.GetDataPermissionList(dataInfoId);
        }
        public static bool isMaxPermissionUser(int userId)
        {
            return dal.isMaxPermissionUser(userId);
        }

        public static int DeletePermissionByUserID(int dataInfoID, int UserID)
        {
            return dal.DeletePermissionByUserID(dataInfoID, UserID);
        }

        public static void addDataPermissions(DataPermissionInfo permission)
        {
            throw new System.NotImplementedException();
        }
    }
}