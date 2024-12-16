using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class  ITAssetsDataProvider:ESP.Finance.IDataAccess.IITAssetsProvider
    {

        #region IAssetProvider 成员

        public int Add(ESP.Finance.Entity.ITAssetsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ITAssets(SerialCode,CategoryId,CategoryName,AssetName,Brand,Model,Configuration,Price,PurchaseDate,AssetDesc,Status,ScrapDate,ScrapUserId,ScrapUserName,ScrapDesc,ScrapAuditorId,ScrapAuditor,ScrapAuditDate,ScrapAuditDesc,UpFile,RelationPO,ScrapLeaderId,ScrapLeader,ScrapLeaderDate,ScrapLeaderDesc,photo,EditDate)");
            strSql.Append(" values (@SerialCode,@CategoryId,@CategoryName,@AssetName,@Brand,@Model,@Configuration,@Price,@PurchaseDate,@AssetDesc,@Status,@ScrapDate,@ScrapUserId,@ScrapUserName,@ScrapDesc,@ScrapAuditorId,@ScrapAuditor,@ScrapAuditDate,@ScrapAuditDesc,@Upfile,@RelationPO,@ScrapLeaderId,@ScrapLeader,@ScrapLeaderDate,@ScrapLeaderDesc,@photo,@EditDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AssetName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Brand", SqlDbType.NVarChar,50),
                    new SqlParameter("@Model", SqlDbType.NVarChar,50),
                    new SqlParameter("@Configuration", SqlDbType.NVarChar,500),
                    new SqlParameter("@Price", SqlDbType.Decimal,20),
                    new SqlParameter("@PurchaseDate", SqlDbType.DateTime,8),
                    new SqlParameter("@AssetDesc", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ScrapDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ScrapUserId", SqlDbType.Int,4),
                    new SqlParameter("@ScrapUserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ScrapDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@ScrapAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@ScrapAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@ScrapAuditDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ScrapAuditDesc", SqlDbType.NVarChar,500),
                     new SqlParameter("@UpFile", SqlDbType.NVarChar,500),
                     new SqlParameter("@RelationPO", SqlDbType.NVarChar,500),
                      new SqlParameter("@ScrapLeaderId", SqlDbType.Int,4),
                    new SqlParameter("@ScrapLeader", SqlDbType.NVarChar,50),
                    new SqlParameter("@ScrapLeaderDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ScrapLeaderDesc", SqlDbType.NVarChar,500),
                     new SqlParameter("@Photo", SqlDbType.NVarChar,500),
                       new SqlParameter("@EditDate", SqlDbType.DateTime,8)
                                        };

            parameters[0].Value = model.SerialCode;
            parameters[1].Value = model.CategoryId;
            parameters[2].Value = model.CategoryName;
            parameters[3].Value = model.AssetName;
            parameters[4].Value = model.Brand;
            parameters[5].Value = model.Model;
            parameters[6].Value = model.Configuration;
            parameters[7].Value = model.Price;
            parameters[8].Value = model.PurchaseDate;
            parameters[9].Value = model.AssetDesc;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.ScrapDate;

            parameters[12].Value = model.ScrapUserId;
            parameters[13].Value = model.ScrapUserName;
            parameters[14].Value = model.ScrapDesc;
            parameters[15].Value = model.ScrapAuditorId;
            parameters[16].Value = model.ScrapAuditor;
            parameters[17].Value = model.ScrapAuditDate;
            parameters[18].Value = model.ScrapAuditDesc;
            parameters[19].Value = model.UpFile;
            parameters[20].Value = model.RelationPO;

            parameters[21].Value = model.ScrapLeaderId;
            parameters[22].Value = model.ScrapLeader;
            parameters[23].Value = model.ScrapLeaderDate;
            parameters[24].Value = model.ScrapLeaderDesc;
            parameters[25].Value = model.Photo;
            parameters[26].Value = model.EditDate;
            //ScrapLeaderId,ScrapLeader,ScrapLeaderDate,ScrapLeaderDesc

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

        public int Update(ESP.Finance.Entity.ITAssetsInfo model)
        {
            //ScrapLeaderId,ScrapLeader,ScrapLeaderDate,ScrapLeaderDesc
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ITAssets set ");
            strSql.Append("SerialCode=@SerialCode,CategoryId=@CategoryId,CategoryName=@CategoryName,AssetName=@AssetName,Brand=@Brand,Model=@Model,");
            strSql.Append("Configuration=@Configuration,Price=@Price,PurchaseDate=@PurchaseDate,AssetDesc=@AssetDesc,Status=@Status,ScrapDate=@ScrapDate,");
            strSql.Append("ScrapUserId=@ScrapUserId,ScrapUserName=@ScrapUserName,ScrapDesc=@ScrapDesc,ScrapAuditorId=@ScrapAuditorId,ScrapAuditor=@ScrapAuditor,");
            strSql.Append("ScrapAuditDate=@ScrapAuditDate,ScrapAuditDesc=@ScrapAuditDesc,UpFile=@UpFile,RelationPO=@RelationPO,");
            strSql.Append("ScrapLeaderId=@ScrapLeaderId,ScrapLeader=@ScrapLeader,ScrapLeaderDate=@ScrapLeaderDate,ScrapLeaderDesc=@ScrapLeaderDesc,Photo=@Photo,EditDate=@EditDate ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AssetName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Brand", SqlDbType.NVarChar,50),
                    new SqlParameter("@Model", SqlDbType.NVarChar,50),
                    new SqlParameter("@Configuration", SqlDbType.NVarChar,500),
                    new SqlParameter("@Price", SqlDbType.Decimal,20),
                    new SqlParameter("@PurchaseDate", SqlDbType.DateTime,8),
                    new SqlParameter("@AssetDesc", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ScrapDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ScrapUserId", SqlDbType.Int,4),
                    new SqlParameter("@ScrapUserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ScrapDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@ScrapAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@ScrapAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@ScrapAuditDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ScrapAuditDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@UpFile", SqlDbType.NVarChar,500),
                    new SqlParameter("@RelationPO", SqlDbType.NVarChar,500),
                    new SqlParameter("@ScrapLeaderId", SqlDbType.Int,4),
                    new SqlParameter("@ScrapLeader", SqlDbType.NVarChar,50),
                    new SqlParameter("@ScrapLeaderDate", SqlDbType.DateTime,8),
                    new SqlParameter("@ScrapLeaderDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@Photo", SqlDbType.NVarChar,500),
                    new SqlParameter("@EditDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.SerialCode;
            parameters[1].Value = model.CategoryId;
            parameters[2].Value = model.CategoryName;
            parameters[3].Value = model.AssetName;
            parameters[4].Value = model.Brand;
            parameters[5].Value = model.Model;
            parameters[6].Value = model.Configuration;
            parameters[7].Value = model.Price;
            parameters[8].Value = model.PurchaseDate;
            parameters[9].Value = model.AssetDesc;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.ScrapDate;

            parameters[12].Value = model.ScrapUserId;
            parameters[13].Value = model.ScrapUserName;
            parameters[14].Value = model.ScrapDesc;
            parameters[15].Value = model.ScrapAuditorId;
            parameters[16].Value = model.ScrapAuditor;
            parameters[17].Value = model.ScrapAuditDate;
            parameters[18].Value = model.ScrapAuditDesc;
            parameters[19].Value = model.UpFile;
            parameters[20].Value = model.RelationPO;

            parameters[21].Value = model.ScrapLeaderId;
            parameters[22].Value = model.ScrapLeader;
            parameters[23].Value = model.ScrapLeaderDate;
            parameters[24].Value = model.ScrapLeaderDesc;
            parameters[25].Value = model.Photo;
            parameters[26].Value = model.EditDate;
            parameters[27].Value = model.Id;
           
            return DbHelperSQL.ExecuteSql(strSql.ToString(),  parameters);
        }

        public int Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ITAssets set status=6 where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };
            parameters[0].Value = Id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.ITAssetsInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_ITAssets ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<ITAssetsInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public IList<ESP.Finance.Entity.ITAssetsInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  FROM F_ITAssets ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where 1=1 " + term);
            }
            strSql.Append(" order by EditDate desc "  );

            return CBO.FillCollection<ITAssetsInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
   
    }
}
