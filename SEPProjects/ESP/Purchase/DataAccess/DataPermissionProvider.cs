using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
    public class DataPermissionProvider
    {
        /// <summary>
        /// 添加数据的页面权限信息
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="permissionList"></param>
        /// <param name="trans"></param>
        public void AddDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList, SqlTransaction trans)
        {
            deletePermission(dataInfo, trans);
            int dataInfoId = addDataInfo(dataInfo,trans);
            foreach (DataPermissionInfo model in permissionList)
            {
                model.DataInfoId = dataInfoId;
                addDataPermissions(model, trans);
            }
        }

        public void AddDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList)
        {
            deletePermission(dataInfo);
            int dataInfoId = addDataInfo(dataInfo);
            foreach (DataPermissionInfo model in permissionList)
            {
                model.DataInfoId = dataInfoId;
                addDataPermissions(model);
            }
        }

        /// <summary>
        /// 不删除原有，继续增加授权人
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="permissionList"></param>
        public void AppendDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList,SqlTransaction trans)
        {
            foreach (DataPermissionInfo model in permissionList)
            {
                model.DataInfoId = dataInfo.Id;
                addDataPermissions(model,trans);
            }
        }

        public void AppendDataPermission(DataInfo dataInfo, List<DataPermissionInfo> permissionList)
        {
            foreach (DataPermissionInfo model in permissionList)
            {
                model.DataInfoId = dataInfo.Id;
                addDataPermissions(model);
            }
        }

        /// <summary>
        /// 添加T_DataInfo信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        /// <returns>T_DataInfo的自增ID</returns>
        private int addDataInfo(DataInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_DataInfo(");
            strSql.Append("dataType,dataId)");
            strSql.Append(" values (");
            strSql.Append("@dataType,@dataId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@dataType", SqlDbType.Int,4),
                        new SqlParameter("@dataId", SqlDbType.Int,4)
                                            };
            parameters[0].Value = model.DataType;
            parameters[1].Value = model.DataId;
            return int.Parse(DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters).ToString());
        }

        private int addDataInfo(DataInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_DataInfo(");
            strSql.Append("dataType,dataId)");
            strSql.Append(" values (");
            strSql.Append("@dataType,@dataId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@dataType", SqlDbType.Int,4),
                        new SqlParameter("@dataId", SqlDbType.Int,4)
                                            };
            parameters[0].Value = model.DataType;
            parameters[1].Value = model.DataId;
            return int.Parse(DbHelperSQL.GetSingle(strSql.ToString(), parameters).ToString());
        }

        /// <summary>
        /// 添加T_DataPermission信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        public  void addDataPermissions(DataPermissionInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_DataPermission(");
            strSql.Append("dataInfoId,userId,isEditor,isViewer)");
            strSql.Append(" values (");
            strSql.Append("@dataInfoId,@userId,@isEditor,@isViewer)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@dataInfoId", SqlDbType.Int,4),
                        new SqlParameter("@userId", SqlDbType.Int,4),
                        new SqlParameter("@isEditor",SqlDbType.Bit),
                        new SqlParameter("@isViewer",SqlDbType.Bit)
                                            };
            parameters[0].Value = model.DataInfoId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.IsEditor;
            parameters[3].Value = model.IsViewer;
            DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
        }

        private void addDataPermissions(DataPermissionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_DataPermission(");
            strSql.Append("dataInfoId,userId,isEditor,isViewer)");
            strSql.Append(" values (");
            strSql.Append("@dataInfoId,@userId,@isEditor,@isViewer)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@dataInfoId", SqlDbType.Int,4),
                        new SqlParameter("@userId", SqlDbType.Int,4),
                        new SqlParameter("@isEditor",SqlDbType.Bit),
                        new SqlParameter("@isViewer",SqlDbType.Bit)
                                            };
            parameters[0].Value = model.DataInfoId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.IsEditor;
            parameters[3].Value = model.IsViewer;
            DbHelperSQL.GetSingle(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除数据的页面权限信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        private void deletePermission(DataInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_DataInfo where dataType=@dataType and dataId=@dataId;");
            strSql.Append("delete T_DataPermission where dataInfoId=@dataInfoId");
            SqlParameter[] parms = { 
                    new SqlParameter("@dataType",SqlDbType.Int,4),
                    new SqlParameter("@dataId",SqlDbType.Int,4),
                    new SqlParameter("@dataInfoId",SqlDbType.Int,4)
            };
            parms[0].Value = model.DataType;
            parms[1].Value = model.DataId;
            parms[2].Value = model.Id;
            if(trans == null)
                DbHelperSQL.ExecuteSql(strSql.ToString(), parms);
            else
                DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parms);
        }

        private void deletePermission(DataInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_DataInfo where dataType=@dataType and dataId=@dataId;");
            strSql.Append("delete T_DataPermission where dataInfoId=@dataInfoId");
            SqlParameter[] parms = { 
                    new SqlParameter("@dataType",SqlDbType.Int,4),
                    new SqlParameter("@dataId",SqlDbType.Int,4),
                    new SqlParameter("@dataInfoId",SqlDbType.Int,4)
            };
            parms[0].Value = model.DataType;
            parms[1].Value = model.DataId;
            parms[2].Value = model.Id;
            DbHelperSQL.ExecuteSql(strSql.ToString(),parms);
        }

        public int DeletePermissionByUserID(int dataInfoID, int UserID)
        {
            return DeletePermissionByUserID(dataInfoID, UserID, null);
        }

        public int DeletePermissionByUserID(int dataInfoID, int UserID,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_DataPermission where dataInfoId=@dataInfoId and userId=@userId");
            SqlParameter[] parms = { 
                    new SqlParameter("@dataInfoId",SqlDbType.Int,4),
                    new SqlParameter("@userId",SqlDbType.Int,4)
            };
            parms[0].Value = dataInfoID;
            parms[1].Value = UserID;
            if(trans == null)
                return DbHelperSQL.ExecuteSql(strSql.ToString(), parms);
            else
                return DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parms);
        }

        #region 已注释区域
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trans"></param>
        public void ADD(GeneralInfo generalModel, SqlTransaction trans)
        {
            //Delete(generalModel.id, trans);
            //UserViewForPrInfo model = new UserViewForPrInfo();
            //model.PrId = generalModel.id;
            ////申请人、使用人、收货人，初审人、分公司审核人
            //string userIds = generalModel.requestor + "," + generalModel.enduser + "," + generalModel.goods_receiver + "," + generalModel.first_assessor + "," + generalModel.Filiale_Auditor;
            ////业务审核人
            //DataSet operationUserList = new OperationAuditDataProvider().GetList(string.Format(" general_id={0}", generalModel.id));
            //if (operationUserList != null && operationUserList.Tables.Count > 0)
            //{
            //    foreach (DataRow dr in operationUserList.Tables[0].Rows)
            //    {
            //        userIds += dr["auditorId"].ToString() + ",";
            //    }
            //}
            //model.UserIds = userIds.TrimEnd(',');
            //insert(model, trans);
        }

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //private void insert(UserViewForPrInfo model,SqlTransaction trans)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into T_UserViewForPr(");
        //    strSql.Append("prId,userIds)");
        //    strSql.Append(" values (");
        //    strSql.Append("@prId,@userIds)");
        //    strSql.Append(";select @@IDENTITY");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@prId", SqlDbType.Int,4),
        //            new SqlParameter("@userIds", SqlDbType.VarChar,200)
        //                                };
        //    parameters[0].Value = model.PrId;
        //    parameters[1].Value = model.UserIds;
        //    DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
        //}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //private void Delete(int prId, SqlTransaction trans)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete T_UserViewForPr");
        //    strSql.Append(" where prId=@prId");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@prId", SqlDbType.Int,4)
        //        };
        //    parameters[0].Value = prId;
        //    DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection, trans, parameters);
        //}

        ///// <summary>
        ///// 获得对象
        ///// </summary>
        ///// <param name="prId"></param>
        ///// <returns></returns>
        //public UserViewForPrInfo GetModel(int prId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(" select * from T_UserViewForPr where prId=@prId");
        //    SqlParameter[] parameters = { new SqlParameter("@prId",SqlDbType.Int,4) };
        //    parameters[0].Value = prId;
        //    return ESP.Finance.Utility.CBO.FillObject<UserViewForPrInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        //}
        #endregion

        public DataInfo GetDataInfoModel(int dataType, int dataId)
        {
            return GetDataInfoModel(dataType, dataId, null);
        }
        /// <summary>
        /// 获得T_DataInfo的对象
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public DataInfo GetDataInfoModel(int dataType, int dataId,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from T_DataInfo where dataType=@dataType and dataId=@dataId");
            SqlParameter[] parameters = { 
                        new SqlParameter("@dataType", SqlDbType.Int, 4),
                        new SqlParameter("@dataId",SqlDbType.Int, 4)                
                                        };
            parameters[0].Value = dataType;
            parameters[1].Value = dataId;
            if(trans == null)
                return ESP.Finance.Utility.CBO.FillObject<DataInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            else
                return ESP.Finance.Utility.CBO.FillObject<DataInfo>(DbHelperSQL.Query(strSql.ToString(),trans.Connection,trans, parameters));
        }

        /// <summary>
        /// 获得T_DataPermission的List对象
        /// </summary>
        /// <param name="dataInfoId"></param>
        /// <returns></returns>
        public List<DataPermissionInfo> GetDataPermissionList(int dataInfoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from T_DataPermission where dataInfoId=@dataInfoId");
            SqlParameter[] parameters = { 
                        new SqlParameter("@dataInfoId", SqlDbType.Int, 4),               
                                        };
            parameters[0].Value = dataInfoId;
            return ESP.Finance.Utility.CBO.FillCollection<DataPermissionInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 检查是否为大权限用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool isMaxPermissionUser(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from T_MaxPermissionUser where userId=@userId and useProject=0");
            SqlParameter[] parameters = { 
                        new SqlParameter("@userId", SqlDbType.Int, 4),               
                                        };
            parameters[0].Value = userId;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return (ds.Tables[0].Rows.Count > 0);
        }
    }
}
