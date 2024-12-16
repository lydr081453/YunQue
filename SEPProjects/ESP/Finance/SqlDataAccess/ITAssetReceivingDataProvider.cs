using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class ITAssetReceivingDataProvider : ESP.Finance.IDataAccess.IITAssetReceivingProvider
    {
        #region IAssetProvider 成员

        public int Add(ESP.Finance.Entity.ITAssetReceivingInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ITAssetReceiving(AssetId,UserId,ReceiveDate,ReturnDate,ReceiveDesc,OperatorId,Status,UserCode,UserName,Email,Mobile,DataServer,AssetName,Brand,Model,SerialCode,Price)");
            strSql.Append(" values (@AssetId,@UserId,@ReceiveDate,@ReturnDate,@ReceiveDesc,@OperatorId,@Status,@UserCode,@UserName,@Email,@Mobile,@DataServer,@AssetName,@Brand,@Model,@SerialCode,@Price)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@AssetId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ReturnDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ReceiveDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@OperatorId", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Email", SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
                    new SqlParameter("@DataServer", SqlDbType.NVarChar,50),
                    new SqlParameter("@AssetName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Brand", SqlDbType.NVarChar,50),
                    new SqlParameter("@Model", SqlDbType.NVarChar,50),
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@Price", SqlDbType.Decimal,18)
                                        };

            parameters[0].Value = model.AssetId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ReceiveDate;
            parameters[3].Value = model.ReturnDate;
            parameters[4].Value = model.ReceiveDesc;
            parameters[5].Value = model.OperatorId;
            parameters[6].Value = model.Status;

            parameters[7].Value = model.UserCode;
            parameters[8].Value = model.UserName;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.Mobile;
            parameters[11].Value = model.DataServer;

            parameters[12].Value = model.AssetName;
            parameters[13].Value = model.Brand;
            parameters[14].Value = model.Model;
            parameters[15].Value = model.SerialCode;
            parameters[16].Value = model.Price;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Add(ESP.Finance.Entity.ITAssetReceivingInfo model, string serverstring)
        {
            string newconnstring = System.Configuration.ConfigurationManager.AppSettings[serverstring];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ITAssetReceiving(AssetId,UserId,ReceiveDate,ReturnDate,ReceiveDesc,OperatorId,Status,UserCode,UserName,Email,Mobile,DataServer,AssetName,Brand,Model,SerialCode,Price)");
            strSql.Append(" values (@AssetId,@UserId,@ReceiveDate,@ReturnDate,@ReceiveDesc,@OperatorId,@Status,@UserCode,@UserName,@Email,@Mobile,@DataServer,@AssetName,@Brand,@Model,@SerialCode,@Price)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@AssetId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ReturnDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ReceiveDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@OperatorId", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Email", SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
                    new SqlParameter("@DataServer", SqlDbType.NVarChar,50),
                    new SqlParameter("@AssetName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Brand", SqlDbType.NVarChar,50),
                    new SqlParameter("@Model", SqlDbType.NVarChar,50),
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@Price", SqlDbType.Decimal,18)

                                        };

            parameters[0].Value = model.AssetId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ReceiveDate;
            parameters[3].Value = model.ReturnDate;
            parameters[4].Value = model.ReceiveDesc;
            parameters[5].Value = model.OperatorId;
            parameters[6].Value = model.Status;

            parameters[7].Value = model.UserCode;
            parameters[8].Value = model.UserName;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.Mobile;
            parameters[11].Value = model.DataServer;

            parameters[12].Value = model.AssetName;
            parameters[13].Value = model.Brand;
            parameters[14].Value = model.Model;
            parameters[15].Value = model.SerialCode;
            parameters[16].Value = model.Price;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),newconnstring, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public int Update(ESP.Finance.Entity.ITAssetReceivingInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ITAssetReceiving set ");
            strSql.Append("AssetId=@AssetId,UserId=@UserId,ReceiveDate=@ReceiveDate,ReturnDate=@ReturnDate,ReceiveDesc=@ReceiveDesc,OperatorId=@OperatorId,Status=@Status,");
            strSql.Append("UserCode=@UserCode,UserName=@UserName,Email=@Email,Mobile=@Mobile,DataServer=@DataServer,AssetName=@AssetName,Brand=@Brand,Model=@Model,SerialCode=@SerialCode,Price=@Price");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                     new SqlParameter("@AssetId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ReturnDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ReceiveDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@OperatorId", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                     new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Email", SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
                    new SqlParameter("@DataServer", SqlDbType.NVarChar,50),
                     new SqlParameter("@AssetName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Brand", SqlDbType.NVarChar,50),
                    new SqlParameter("@Model", SqlDbType.NVarChar,50),
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@Price", SqlDbType.Decimal,18),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.AssetId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ReceiveDate;
            parameters[3].Value = model.ReturnDate;
            parameters[4].Value = model.ReceiveDesc;
            parameters[5].Value = model.OperatorId;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.UserCode;
            parameters[8].Value = model.UserName;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.Mobile;
            parameters[11].Value = model.DataServer;
            parameters[12].Value = model.AssetName;
            parameters[13].Value = model.Brand;
            parameters[14].Value = model.Model;
            parameters[15].Value = model.SerialCode;
            parameters[16].Value = model.Price;
            parameters[17].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新其他系统领用状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="serverstring"></param>
        /// <returns></returns>
        public int UpdateReturnStatus(ESP.Finance.Entity.ITAssetReceivingInfo model, string serverstring)
        {
            string newconnstring = System.Configuration.ConfigurationManager.AppSettings[serverstring];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ITAssetReceiving set ");
            strSql.Append("ReturnDate=@ReturnDate,Status=@Status");
            strSql.Append(" where AssetId=@AssetId and UserId=@UserId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ReturnDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@AssetId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ReturnDate;
            parameters[1].Value = model.Status;
            parameters[2].Value = model.AssetId;
            parameters[3].Value = model.UserId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),newconnstring, parameters);
        }

        public int Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ITAssetReceiving where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };
            parameters[0].Value = Id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.ITAssetReceivingInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_ITAssetReceiving ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<ITAssetReceivingInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public IList<ESP.Finance.Entity.ITAssetReceivingInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  FROM F_ITAssetReceiving ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ITAssetReceivingInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        public ESP.Finance.Entity.ITAssetReceivingInfo getLastModel(int AssetId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from F_ITAssetReceiving ");
            strSql.Append(" where AssetId=@AssetId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AssetId", SqlDbType.Int,4)};
            parameters[0].Value = AssetId;

            strSql.Append(" order by Id desc");

            return CBO.FillObject<ITAssetReceivingInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        #endregion
    }
}
