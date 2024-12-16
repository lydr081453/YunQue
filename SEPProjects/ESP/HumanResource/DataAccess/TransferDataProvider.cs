using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace ESP.HumanResource.DataAccess
{
    public class TransferDataProvider
    {
        public TransferDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TransferInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_Transfer(");
            strSql.Append("TransId,TransCode,TransName,OldGroupId,OldGroupName,OldPositionId,OldPositionName,OldLevelId,OldLevelName,NewGroupId,NewGroupName,NewPositionId,NewPositionName,NewLevelId,NewLevelName,SalaryBase,SalaryPromotion,TransInDate,TransOutDate,CreateDate,Status,CreaterId,Creater,Remark,HeadCountId)");
            strSql.Append(" values (");
            strSql.Append("@TransId,@TransCode,@TransName,@OldGroupId,@OldGroupName,@OldPositionId,@OldPositionName,@OldLevelId,@OldLevelName,@NewGroupId,");
            strSql.Append("@NewGroupName,@NewPositionId,@NewPositionName,@NewLevelId,@NewLevelName,@SalaryBase,@SalaryPromotion,@TransInDate,@TransOutDate,@CreateDate,@Status,@CreaterId,@Creater,@Remark,@HeadCountId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TransId", SqlDbType.Int),
					new SqlParameter("@TransCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TransName", SqlDbType.NVarChar,50),
					new SqlParameter("@OldGroupId", SqlDbType.Int),
                    new SqlParameter("@OldGroupName", SqlDbType.NVarChar,50),                    
                    new SqlParameter("@OldPositionId", SqlDbType.Int),
                    new SqlParameter("@OldPositionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@OldLevelId", SqlDbType.Int),
                    new SqlParameter("@OldLevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewGroupId", SqlDbType.Int),
                    new SqlParameter("@NewGroupName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewPositionId", SqlDbType.Int),
                    new SqlParameter("@NewPositionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewLevelId", SqlDbType.Int),
                    new SqlParameter("@NewLevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@SalaryBase", SqlDbType.Decimal),
                    new SqlParameter("@SalaryPromotion", SqlDbType.Decimal),
                    new SqlParameter("@TransInDate", SqlDbType.DateTime),
                    new SqlParameter("@TransOutDate", SqlDbType.DateTime),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@CreaterId", SqlDbType.Int),
                    new SqlParameter("@Creater", SqlDbType.NVarChar,50),
                     new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                      new SqlParameter("@HeadCountId", SqlDbType.Int)
                                        };
            parameters[0].Value = model.TransId;
            parameters[1].Value = model.TransCode;
            parameters[2].Value = model.TransName;
            parameters[3].Value = model.OldGroupId;
            parameters[4].Value = model.OldGroupName;
            parameters[5].Value = model.OldPositionId;
            parameters[6].Value = model.OldPositionName;
            parameters[7].Value = model.OldLevelId;
            parameters[8].Value = model.OldLevelName;
            parameters[9].Value = model.NewGroupId;
            parameters[10].Value = model.NewGroupName;
            parameters[11].Value = model.NewPositionId;
            parameters[12].Value = model.NewPositionName;
            parameters[13].Value = model.NewLevelId;
            parameters[14].Value = model.NewLevelName;
            parameters[15].Value = model.SalaryBase;
            parameters[16].Value = model.SalaryPromotion;
            parameters[17].Value = model.TransInDate;
            parameters[18].Value = model.TransOutDate;
            parameters[19].Value = model.CreateDate;
            parameters[20].Value = model.Status;
            parameters[21].Value = model.CreaterId;
            parameters[22].Value = model.Creater;
            parameters[23].Value = model.Remark;
            parameters[24].Value = model.HeadCountId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TransferInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_Transfer set ");
            strSql.Append("TransId=@TransId,TransCode=@TransCode,TransName=@TransName,OldGroupId=@OldGroupId,OldGroupName=@OldGroupName,");
            strSql.Append("OldPositionId=@OldPositionId,OldPositionName=@OldPositionName,OldLevelId=@OldLevelId,OldLevelName=@OldLevelName,");
            strSql.Append("NewGroupId=@NewGroupId,NewGroupName=@NewGroupName,NewPositionId=@NewPositionId,NewPositionName=@NewPositionName,");
            strSql.Append("NewLevelId=@NewLevelId,NewLevelName=@NewLevelName,SalaryBase=@SalaryBase,SalaryPromotion=@SalaryPromotion,");
            strSql.Append("TransInDate=@TransInDate,TransOutDate=@TransOutDate,CreateDate=@CreateDate,Status=@Status,CreaterId=@CreaterId,Creater=@Creater,Remark=@Remark");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@TransId", SqlDbType.Int),
					new SqlParameter("@TransCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TransName", SqlDbType.NVarChar,50),
					new SqlParameter("@OldGroupId", SqlDbType.Int),
                    new SqlParameter("@OldGroupName", SqlDbType.NVarChar,50),                    
                    new SqlParameter("@OldPositionId", SqlDbType.Int),
                    new SqlParameter("@OldPositionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@OldLevelId", SqlDbType.Int),
                    new SqlParameter("@OldLevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewGroupId", SqlDbType.Int),
                    new SqlParameter("@NewGroupName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewPositionId", SqlDbType.Int),
                    new SqlParameter("@NewPositionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewLevelId", SqlDbType.Int),
                    new SqlParameter("@NewLevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@SalaryBase", SqlDbType.Decimal),
                    new SqlParameter("@SalaryPromotion", SqlDbType.Decimal),
                    new SqlParameter("@TransInDate", SqlDbType.DateTime),
                    new SqlParameter("@TransOutDate", SqlDbType.DateTime),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@CreaterId", SqlDbType.Int),
                    new SqlParameter("@Creater", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TransId;
            parameters[2].Value = model.TransCode;
            parameters[3].Value = model.TransName;
            parameters[4].Value = model.OldGroupId;
            parameters[5].Value = model.OldGroupName;
            parameters[6].Value = model.OldPositionId;
            parameters[7].Value = model.OldPositionName;
            parameters[8].Value = model.OldLevelId;
            parameters[9].Value = model.OldLevelName;
            parameters[10].Value = model.NewGroupId;
            parameters[11].Value = model.NewGroupName;
            parameters[12].Value = model.NewPositionId;
            parameters[13].Value = model.NewPositionName;
            parameters[14].Value = model.NewLevelId;
            parameters[15].Value = model.NewLevelName;
            parameters[16].Value = model.SalaryBase;
            parameters[17].Value = model.SalaryPromotion;
            parameters[18].Value = model.TransInDate;
            parameters[19].Value = model.TransOutDate;
            parameters[20].Value = model.CreateDate;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.CreaterId;
            parameters[23].Value = model.Creater;
            parameters[24].Value = model.Remark;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        public int Update(TransferInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_Transfer set ");
            strSql.Append("TransId=@TransId,TransCode=@TransCode,TransName=@TransName,OldGroupId=@OldGroupId,OldGroupName=@OldGroupName,");
            strSql.Append("OldPositionId=@OldPositionId,OldPositionName=@OldPositionName,OldLevelId=@OldLevelId,OldLevelName=@OldLevelName,");
            strSql.Append("NewGroupId=@NewGroupId,NewGroupName=@NewGroupName,NewPositionId=@NewPositionId,NewPositionName=@NewPositionName,");
            strSql.Append("NewLevelId=@NewLevelId,NewLevelName=@NewLevelName,SalaryBase=@SalaryBase,SalaryPromotion=@SalaryPromotion,");
            strSql.Append("TransInDate=@TransInDate,TransOutDate=@TransOutDate,CreateDate=@CreateDate,Status=@Status,CreaterId=@CreaterId,Creater=@Creater,Remark=@Remark,HeadCountId=@HeadCountId");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@TransId", SqlDbType.Int),
					new SqlParameter("@TransCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TransName", SqlDbType.NVarChar,50),
					new SqlParameter("@OldGroupId", SqlDbType.Int),
                    new SqlParameter("@OldGroupName", SqlDbType.NVarChar,50),                    
                    new SqlParameter("@OldPositionId", SqlDbType.Int),
                    new SqlParameter("@OldPositionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@OldLevelId", SqlDbType.Int),
                    new SqlParameter("@OldLevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewGroupId", SqlDbType.Int),
                    new SqlParameter("@NewGroupName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewPositionId", SqlDbType.Int),
                    new SqlParameter("@NewPositionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NewLevelId", SqlDbType.Int),
                    new SqlParameter("@NewLevelName", SqlDbType.NVarChar,50),
                    new SqlParameter("@SalaryBase", SqlDbType.Decimal),
                    new SqlParameter("@SalaryPromotion", SqlDbType.Decimal),
                    new SqlParameter("@TransInDate", SqlDbType.DateTime),
                    new SqlParameter("@TransOutDate", SqlDbType.DateTime),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@CreaterId", SqlDbType.Int),
                    new SqlParameter("@Creater", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("HeadCountId",SqlDbType.Int)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TransId;
            parameters[2].Value = model.TransCode;
            parameters[3].Value = model.TransName;
            parameters[4].Value = model.OldGroupId;
            parameters[5].Value = model.OldGroupName;
            parameters[6].Value = model.OldPositionId;
            parameters[7].Value = model.OldPositionName;
            parameters[8].Value = model.OldLevelId;
            parameters[9].Value = model.OldLevelName;
            parameters[10].Value = model.NewGroupId;
            parameters[11].Value = model.NewGroupName;
            parameters[12].Value = model.NewPositionId;
            parameters[13].Value = model.NewPositionName;
            parameters[14].Value = model.NewLevelId;
            parameters[15].Value = model.NewLevelName;
            parameters[16].Value = model.SalaryBase;
            parameters[17].Value = model.SalaryPromotion;
            parameters[18].Value = model.TransInDate;
            parameters[19].Value = model.TransOutDate;
            parameters[20].Value = model.CreateDate;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.CreaterId;
            parameters[23].Value = model.Creater;
            parameters[24].Value = model.Remark;
            parameters[25].Value = model.HeadCountId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_Transfer ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public List<ESP.HumanResource.Entity.TransferDetailsInfo> GetTransferDataById(int transferId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_TransferDetails ");
            strSql.Append(" WHERE transferId=@transferId");
            SqlParameter[] parameters = {
					new SqlParameter("@transferId", SqlDbType.Int,4)};
            parameters[0].Value = transferId;

            return CBO.FillCollection<TransferDetailsInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public List<ESP.HumanResource.Entity.TransferDetailsInfo> GetTransferDataByUserId(int userId)
        {
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetStoredProcCommand("GetTransferData", userId);
            System.Data.DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            List<ESP.HumanResource.Entity.TransferDetailsInfo> transferDetailList = new List<ESP.HumanResource.Entity.TransferDetailsInfo>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    ESP.HumanResource.Entity.TransferDetailsInfo detailInfo = new ESP.HumanResource.Entity.TransferDetailsInfo();
                    if (r["FormId"].ToString() != "")
                    {
                        detailInfo.FormId = int.Parse(r["FormId"].ToString());
                    }
                    detailInfo.FormCode = r["FormCode"].ToString();
                    detailInfo.FormType = r["FormType"].ToString();
                    if (r["UserId"].ToString() != "")
                    {
                        detailInfo.UserId = int.Parse(r["UserId"].ToString());
                    }
                    detailInfo.UserName = r["UserName"].ToString();
                    if (r["ProjectId"].ToString() != "")
                    {
                        detailInfo.ProjectId = int.Parse(r["ProjectId"].ToString());
                    }
                    detailInfo.ProjectCode = r["ProjectCode"].ToString();
                    detailInfo.Description = r["Description"].ToString();
                    if (r["TotalPrice"].ToString() != "")
                    {
                        detailInfo.TotalPrice = decimal.Parse(r["TotalPrice"].ToString());
                    }
                    if (r["Status"].ToString() != "")
                    {
                        detailInfo.FormStatus = int.Parse(r["Status"].ToString());
                    }
                    detailInfo.WebSite = r["website"].ToString();
                    detailInfo.Url = r["url"].ToString();
                    transferDetailList.Add(detailInfo);
                }
            }
            return transferDetailList;
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TransferInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from SEP_Transfer ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<TransferInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }


        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<TransferInfo> GetList(string strWhere, List<SqlParameter> parms)
        {
            string strSql = "select * from SEP_Transfer where 1=1 ";
            strSql += strWhere;
            if (parms != null)
                return CBO.FillCollection<TransferInfo>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));
            else
                return CBO.FillCollection<TransferInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public List<TransferInfo> GetWaitAuditList(int currentUserId,string strWhere, List<SqlParameter> parms)
        {
            string strSql = "select * from SEP_Transfer where id in(select formid from sep_auditlog where formtype=3 and auditorid=" + currentUserId + " and auditStatus=0)";
            strSql += strWhere;
            if (parms != null)
                return CBO.FillCollection<TransferInfo>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));
            else
                return CBO.FillCollection<TransferInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion  成员方法
    }
}
