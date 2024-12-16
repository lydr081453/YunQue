using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Utilities;

namespace ESP.HumanResource.DataAccess
{
   public  class HeadAccountDataProvider
    {
       public HeadAccountDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Sep_HeadAccount");
            strSql.Append(" where id= @id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HeadAccountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sep_HeadAccount(");
            strSql.Append("ReplaceUserId,GroupId,CreatorId,Creator,CreateDate,Status,Remark,Position,PositionId,BaseId,BaseName,LevelId,LevelName,InterviewVPId,OfferLetterUserId,CostUrl,IsAAD,CustomerName,NewBiz,ReplaceReason,DimissionDate,Response,Requestment,TalentId,RCUserId)");
            strSql.Append(" values (");
            strSql.Append("@ReplaceUserId,@GroupId,@CreatorId,@Creator,@CreateDate,@Status,@Remark,@Position,@PositionId,@BaseId,@BaseName,@LevelId,@LevelName,@InterviewVPId,@OfferLetterUserId,@CostUrl,@IsAAD,@CustomerName,@NewBiz,@ReplaceReason,@DimissionDate,@Response,@Requestment,@TalentId,@RCUserId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReplaceUserId", SqlDbType.Int,4),
					new SqlParameter("@GroupId", SqlDbType.Int,4),
					new SqlParameter("@CreatorId", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Position", SqlDbType.NVarChar),
                    new SqlParameter("@PositionId", SqlDbType.Int,4),
                    new SqlParameter("@BaseId", SqlDbType.Int,4),
                    new SqlParameter("@BaseName", SqlDbType.NVarChar),
                    new SqlParameter("@LevelId", SqlDbType.Int,4),
                    new SqlParameter("@LevelName", SqlDbType.NVarChar),
                     new SqlParameter("@InterviewVPId", SqlDbType.Int,4),
                    new SqlParameter("@OfferLetterUserId", SqlDbType.Int,4),
                     new SqlParameter("@CostUrl", SqlDbType.NVarChar),
                      new SqlParameter("@IsAAD", SqlDbType.Bit),
                      //CustomerName,NewBiz,ReplaceReason,DimissionDate,Response,Requestment
                      new SqlParameter("@CustomerName", SqlDbType.NVarChar),
                      new SqlParameter("@NewBiz", SqlDbType.NVarChar),
                      new SqlParameter("@ReplaceReason", SqlDbType.NVarChar),
                      new SqlParameter("@DimissionDate", SqlDbType.NVarChar),
                      new SqlParameter("@Response", SqlDbType.NVarChar),
                      new SqlParameter("@Requestment", SqlDbType.NVarChar),
                       new SqlParameter("@TalentId", SqlDbType.Int,4),
                       new SqlParameter("@RCUserId", SqlDbType.Int,4)        
                                        };
            parameters[0].Value = model.ReplaceUserId;
            parameters[1].Value = model.GroupId;
            parameters[2].Value = model.CreatorId;
            parameters[3].Value = model.Creator;
            parameters[4].Value = model.CreateDate;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Position;
            parameters[8].Value = model.PositionId;
            parameters[9].Value = model.BaseId;
            parameters[10].Value = model.BaseName;
            parameters[11].Value = model.LevelId;
            parameters[12].Value = model.LevelName;
            parameters[13].Value = model.InterviewVPId;
            parameters[14].Value = model.OfferLetterUserId;
            parameters[15].Value = model.CostUrl;
            parameters[16].Value = model.IsAAD;

            parameters[17].Value = model.CustomerName;
            parameters[18].Value = model.NewBiz;
            parameters[19].Value = model.ReplaceReason;
            parameters[20].Value = model.DimissionDate;
            parameters[21].Value = model.Response;
            parameters[22].Value = model.Requestment;
            parameters[23].Value = model.TalentId;
            parameters[24].Value = model.RCUserId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                model.Id = Convert.ToInt32(obj);
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(HeadAccountInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sep_HeadAccount set ");
            strSql.Append("ReplaceUserId=@ReplaceUserId,");
            strSql.Append("GroupId=@GroupId,");
            strSql.Append("CreatorId=@CreatorId,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");

            strSql.Append("Position=@Position,");
            strSql.Append("PositionId=@PositionId,");
            strSql.Append("BaseId=@BaseId,");
            strSql.Append("BaseName=@BaseName,");
            strSql.Append("LevelId=@LevelId,");
            strSql.Append("LevelName=@LevelName,InterviewVPId=@InterviewVPId,OfferLetterUserId=@OfferLetterUserId,CostUrl=@CostUrl,IsAAD=@IsAAD,");
            strSql.Append("CustomerName=@CustomerName,NewBiz=@NewBiz,ReplaceReason=@ReplaceReason,DimissionDate=@DimissionDate,Response=@Response,Requestment=@Requestment,TalentId=@TalentId,RCUserId=@RCUserId");

            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),   
					new SqlParameter("@ReplaceUserId", SqlDbType.Int,4),
					new SqlParameter("@GroupId", SqlDbType.Int,4),
					new SqlParameter("@CreatorId", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Position", SqlDbType.NVarChar),
                    new SqlParameter("@PositionId", SqlDbType.Int,4),
                    new SqlParameter("@BaseId", SqlDbType.Int,4),
                    new SqlParameter("@BaseName", SqlDbType.NVarChar),
                    new SqlParameter("@LevelId", SqlDbType.Int,4),
                    new SqlParameter("@LevelName", SqlDbType.NVarChar),
                    new SqlParameter("@InterviewVPId", SqlDbType.Int,4),
                    new SqlParameter("@OfferLetterUserId", SqlDbType.Int,4),
                    new SqlParameter("@CostUrl", SqlDbType.NVarChar),
                    new SqlParameter("@IsAAD", SqlDbType.Bit),
                     //CustomerName,NewBiz,ReplaceReason,DimissionDate,Response,Requestment
                      new SqlParameter("@CustomerName", SqlDbType.NVarChar),
                      new SqlParameter("@NewBiz", SqlDbType.NVarChar),
                      new SqlParameter("@ReplaceReason", SqlDbType.NVarChar),
                      new SqlParameter("@DimissionDate", SqlDbType.NVarChar),
                      new SqlParameter("@Response", SqlDbType.NVarChar),
                      new SqlParameter("@Requestment", SqlDbType.NVarChar),
                       new SqlParameter("@TalentId", SqlDbType.Int,4),
                       new SqlParameter("@RCUserId", SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.ReplaceUserId;
            parameters[2].Value = model.GroupId;
            parameters[3].Value = model.CreatorId;
            parameters[4].Value = model.Creator;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.Position;
            parameters[9].Value = model.PositionId;
            parameters[10].Value = model.BaseId;
            parameters[11].Value = model.BaseName;
            parameters[12].Value = model.LevelId;
            parameters[13].Value = model.LevelName;
            parameters[14].Value = model.InterviewVPId;
            parameters[15].Value = model.OfferLetterUserId;
            parameters[16].Value = model.CostUrl;
            parameters[17].Value = model.IsAAD;

            parameters[18].Value = model.CustomerName;
            parameters[19].Value = model.NewBiz;
            parameters[20].Value = model.ReplaceReason;
            parameters[21].Value = model.DimissionDate;
            parameters[22].Value = model.Response;
            parameters[23].Value = model.Requestment;
            parameters[24].Value = model.TalentId;
            parameters[25].Value = model.RCUserId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(HeadAccountInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sep_HeadAccount set ");
            strSql.Append("ReplaceUserId=@ReplaceUserId,");
            strSql.Append("GroupId=@GroupId,");
            strSql.Append("CreatorId=@CreatorId,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Position=@Position,");
            strSql.Append("PositionId=@PositionId,");
            strSql.Append("BaseId=@BaseId,");
            strSql.Append("BaseName=@BaseName,");
            strSql.Append("LevelId=@LevelId,");
            strSql.Append("LevelName=@LevelName,InterviewVPId=@InterviewVPId,OfferLetterUserId=@OfferLetterUserId,CostUrl=@CostUrl,IsAAD=@IsAAD,");
            strSql.Append("CustomerName=@CustomerName,NewBiz=@NewBiz,ReplaceReason=@ReplaceReason,DimissionDate=@DimissionDate,Response=@Response,Requestment=@Requestment,TalentId=@TalentId,RCUserId=@RCUserId");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                                            new SqlParameter("@id", SqlDbType.Int,4),   
					new SqlParameter("@ReplaceUserId", SqlDbType.Int,4),
					new SqlParameter("@GroupId", SqlDbType.Int,4),
					new SqlParameter("@CreatorId", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@Position", SqlDbType.NVarChar),
                    new SqlParameter("@PositionId", SqlDbType.Int,4),
                    new SqlParameter("@BaseId", SqlDbType.Int,4),
                    new SqlParameter("@BaseName", SqlDbType.NVarChar),
                    new SqlParameter("@LevelId", SqlDbType.Int,4),
                    new SqlParameter("@LevelName", SqlDbType.NVarChar),
                     new SqlParameter("@InterviewVPId", SqlDbType.Int,4),
                     new SqlParameter("@OfferLetterUserId", SqlDbType.Int,4),
                     new SqlParameter("@CostUrl", SqlDbType.NVarChar),
                     new SqlParameter("@IsAAD", SqlDbType.Bit),
                      //CustomerName,NewBiz,ReplaceReason,DimissionDate,Response,Requestment
                      new SqlParameter("@CustomerName", SqlDbType.NVarChar),
                      new SqlParameter("@NewBiz", SqlDbType.NVarChar),
                      new SqlParameter("@ReplaceReason", SqlDbType.NVarChar),
                      new SqlParameter("@DimissionDate", SqlDbType.NVarChar),
                      new SqlParameter("@Response", SqlDbType.NVarChar),
                      new SqlParameter("@Requestment", SqlDbType.NVarChar),
                      new SqlParameter("@TalentId", SqlDbType.Int,4),
                      new SqlParameter("@RCUserId", SqlDbType.Int,4)
                                    
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.ReplaceUserId;
            parameters[2].Value = model.GroupId;
            parameters[3].Value = model.CreatorId;
            parameters[4].Value = model.Creator;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.Position;
            parameters[9].Value = model.PositionId;
            parameters[10].Value = model.BaseId;
            parameters[11].Value = model.BaseName;
            parameters[12].Value = model.LevelId;
            parameters[13].Value = model.LevelName;
            parameters[14].Value = model.InterviewVPId;
            parameters[15].Value = model.OfferLetterUserId;
            parameters[16].Value = model.CostUrl;
            parameters[17].Value = model.IsAAD;

            parameters[18].Value = model.CustomerName;
            parameters[19].Value = model.NewBiz;
            parameters[20].Value = model.ReplaceReason;
            parameters[21].Value = model.DimissionDate;
            parameters[22].Value = model.Response;
            parameters[23].Value = model.Requestment;
            parameters[24].Value = model.TalentId;
            parameters[25].Value = model.RCUserId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Sep_HeadAccount ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HeadAccountInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sep_HeadAccount ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            HeadAccountInfo model = new HeadAccountInfo();
            return CBO.FillObject<HeadAccountInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HeadAccountInfo GetModelByUserId(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from Sep_HeadAccount ");
            strSql.Append(" where offerletteruserid =@userid");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)};
            parameters[0].Value = userid;
            HeadAccountInfo model = new HeadAccountInfo();
            return CBO.FillObject<HeadAccountInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Sep_HeadAccount ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<HeadAccountInfo> GetModelList(string strWhere, SqlParameter[] param)
        {
            string strSql = "select * from Sep_HeadAccount where 1=1 ";
            strSql += strWhere;
            strSql += " order by id desc";
            return CBO.FillCollection<HeadAccountInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<HeadAccountInfo> GetModelList(string strWhere)
        {
            string strSql = "select * from Sep_HeadAccount where 1=1 ";
            strSql += strWhere;
            strSql += " order by createdate desc";
            return CBO.FillCollection<HeadAccountInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion  成员方法
    }
}
