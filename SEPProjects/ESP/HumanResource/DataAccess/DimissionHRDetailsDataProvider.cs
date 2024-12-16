using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionHRDetailsDataProvider
    {
        public DimissionHRDetailsDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionHRDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM SEP_DimissionHRDetails");
            strSql.Append(" WHERE DimissionHRDetailId= @DimissionHRDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionHRDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionHRDetailId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionHRDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionHRDetails(");
            strSql.Append("DimissionId,SocialInsLastMonth,MedicalInsLastMonth,CapitalReserveLastMonth,AddedMedicalInsLastMonth,IsArchives,TurnAroundDate,Principal1ID,Principal1Name,Principal2ID,Principal2Name,Remark,BranchId,IsShowPosition,IsComplementaryMedical)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@SocialInsLastMonth,@MedicalInsLastMonth,@CapitalReserveLastMonth,@AddedMedicalInsLastMonth,@IsArchives,@TurnAroundDate,@Principal1ID,@Principal1Name,@Principal2ID,@Principal2Name,@Remark,@BranchId,@IsShowPosition,@IsComplementaryMedical)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@SocialInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@MedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@CapitalReserveLastMonth", SqlDbType.DateTime),
					new SqlParameter("@AddedMedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@IsArchives", SqlDbType.Bit,1),
					new SqlParameter("@TurnAroundDate", SqlDbType.DateTime),
					new SqlParameter("@Principal1ID", SqlDbType.Int,4),
					new SqlParameter("@Principal1Name", SqlDbType.NVarChar),
					new SqlParameter("@Principal2ID", SqlDbType.Int,4),
					new SqlParameter("@Principal2Name", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@BranchId", SqlDbType.Int,4),
					new SqlParameter("@IsShowPosition", SqlDbType.Bit,1),
                    new SqlParameter("@IsComplementaryMedical", SqlDbType.Bit, 1)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.SocialInsLastMonth;
            parameters[2].Value = model.MedicalInsLastMonth;
            parameters[3].Value = model.CapitalReserveLastMonth;
            parameters[4].Value = model.AddedMedicalInsLastMonth;
            parameters[5].Value = model.IsArchives;
            parameters[6].Value = model.TurnAroundDate;
            parameters[7].Value = model.Principal1ID;
            parameters[8].Value = model.Principal1Name;
            parameters[9].Value = model.Principal2ID;
            parameters[10].Value = model.Principal2Name;
            parameters[11].Value = model.Remark;
            parameters[12].Value = model.BranchId;
            parameters[13].Value = model.IsShowPosition;
            parameters[14].Value = model.IsComplementaryMedical;

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
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionHRDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionHRDetails(");
            strSql.Append("DimissionId,SocialInsLastMonth,MedicalInsLastMonth,CapitalReserveLastMonth,AddedMedicalInsLastMonth,IsArchives,TurnAroundDate,Principal1ID,Principal1Name,Principal2ID,Principal2Name,Remark,BranchId,IsShowPosition,IsComplementaryMedical)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@SocialInsLastMonth,@MedicalInsLastMonth,@CapitalReserveLastMonth,@AddedMedicalInsLastMonth,@IsArchives,@TurnAroundDate,@Principal1ID,@Principal1Name,@Principal2ID,@Principal2Name,@Remark,@BranchId,@IsShowPosition,@IsComplementaryMedical)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@SocialInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@MedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@CapitalReserveLastMonth", SqlDbType.DateTime),
					new SqlParameter("@AddedMedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@IsArchives", SqlDbType.Bit,1),
					new SqlParameter("@TurnAroundDate", SqlDbType.DateTime),
					new SqlParameter("@Principal1ID", SqlDbType.Int,4),
					new SqlParameter("@Principal1Name", SqlDbType.NVarChar),
					new SqlParameter("@Principal2ID", SqlDbType.Int,4),
					new SqlParameter("@Principal2Name", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@BranchId", SqlDbType.Int,4),
					new SqlParameter("@IsShowPosition", SqlDbType.Bit,1),
                    new SqlParameter("@IsComplementaryMedical", SqlDbType.Bit, 1)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.SocialInsLastMonth;
            parameters[2].Value = model.MedicalInsLastMonth;
            parameters[3].Value = model.CapitalReserveLastMonth;
            parameters[4].Value = model.AddedMedicalInsLastMonth;
            parameters[5].Value = model.IsArchives;
            parameters[6].Value = model.TurnAroundDate;
            parameters[7].Value = model.Principal1ID;
            parameters[8].Value = model.Principal1Name;
            parameters[9].Value = model.Principal2ID;
            parameters[10].Value = model.Principal2Name;
            parameters[11].Value = model.Remark;
            parameters[12].Value = model.BranchId;
            parameters[13].Value = model.IsShowPosition;
            parameters[14].Value = model.IsComplementaryMedical;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), stran.Connection, stran, parameters);
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
        public void Update(ESP.HumanResource.Entity.DimissionHRDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionHRDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("SocialInsLastMonth=@SocialInsLastMonth,");
            strSql.Append("MedicalInsLastMonth=@MedicalInsLastMonth,");
            strSql.Append("CapitalReserveLastMonth=@CapitalReserveLastMonth,");
            strSql.Append("AddedMedicalInsLastMonth=@AddedMedicalInsLastMonth,");
            strSql.Append("IsArchives=@IsArchives,");
            strSql.Append("TurnAroundDate=@TurnAroundDate,");
            strSql.Append("Principal1ID=@Principal1ID,");
            strSql.Append("Principal1Name=@Principal1Name,");
            strSql.Append("Principal2ID=@Principal2ID,");
            strSql.Append("Principal2Name=@Principal2Name,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BranchId=@BranchId,");
            strSql.Append("IsShowPosition=@IsShowPosition,");
            strSql.Append("IsComplementaryMedical=@IsComplementaryMedical ");
            strSql.Append(" WHERE DimissionHRDetailId=@DimissionHRDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionHRDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@SocialInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@MedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@CapitalReserveLastMonth", SqlDbType.DateTime),
					new SqlParameter("@AddedMedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@IsArchives", SqlDbType.Bit,1),
					new SqlParameter("@TurnAroundDate", SqlDbType.DateTime),
					new SqlParameter("@Principal1ID", SqlDbType.Int,4),
					new SqlParameter("@Principal1Name", SqlDbType.NVarChar),
					new SqlParameter("@Principal2ID", SqlDbType.Int,4),
					new SqlParameter("@Principal2Name", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@BranchId", SqlDbType.Int,4),
					new SqlParameter("@IsShowPosition", SqlDbType.Bit,1),
                    new SqlParameter("@IsComplementaryMedical", SqlDbType.Bit, 1)};
            parameters[0].Value = model.DimissionHRDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.SocialInsLastMonth;
            parameters[3].Value = model.MedicalInsLastMonth;
            parameters[4].Value = model.CapitalReserveLastMonth;
            parameters[5].Value = model.AddedMedicalInsLastMonth;
            parameters[6].Value = model.IsArchives;
            parameters[7].Value = model.TurnAroundDate;
            parameters[8].Value = model.Principal1ID;
            parameters[9].Value = model.Principal1Name;
            parameters[10].Value = model.Principal2ID;
            parameters[11].Value = model.Principal2Name;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.BranchId;
            parameters[14].Value = model.IsShowPosition;
            parameters[15].Value = model.IsComplementaryMedical;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionHRDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionHRDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("SocialInsLastMonth=@SocialInsLastMonth,");
            strSql.Append("MedicalInsLastMonth=@MedicalInsLastMonth,");
            strSql.Append("CapitalReserveLastMonth=@CapitalReserveLastMonth,");
            strSql.Append("AddedMedicalInsLastMonth=@AddedMedicalInsLastMonth,");
            strSql.Append("IsArchives=@IsArchives,");
            strSql.Append("TurnAroundDate=@TurnAroundDate,");
            strSql.Append("Principal1ID=@Principal1ID,");
            strSql.Append("Principal1Name=@Principal1Name,");
            strSql.Append("Principal2ID=@Principal2ID,");
            strSql.Append("Principal2Name=@Principal2Name,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BranchId=@BranchId,");
            strSql.Append("IsShowPosition=@IsShowPosition,");
            strSql.Append("IsComplementaryMedical=@IsComplementaryMedical ");
            strSql.Append(" WHERE DimissionHRDetailId=@DimissionHRDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionHRDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@SocialInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@MedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@CapitalReserveLastMonth", SqlDbType.DateTime),
					new SqlParameter("@AddedMedicalInsLastMonth", SqlDbType.DateTime),
					new SqlParameter("@IsArchives", SqlDbType.Bit,1),
					new SqlParameter("@TurnAroundDate", SqlDbType.DateTime),
					new SqlParameter("@Principal1ID", SqlDbType.Int,4),
					new SqlParameter("@Principal1Name", SqlDbType.NVarChar),
					new SqlParameter("@Principal2ID", SqlDbType.Int,4),
					new SqlParameter("@Principal2Name", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@BranchId", SqlDbType.Int,4),
					new SqlParameter("@IsShowPosition", SqlDbType.Bit,1),
                    new SqlParameter("@IsComplementaryMedical", SqlDbType.Bit, 1)};
            parameters[0].Value = model.DimissionHRDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.SocialInsLastMonth;
            parameters[3].Value = model.MedicalInsLastMonth;
            parameters[4].Value = model.CapitalReserveLastMonth;
            parameters[5].Value = model.AddedMedicalInsLastMonth;
            parameters[6].Value = model.IsArchives;
            parameters[7].Value = model.TurnAroundDate;
            parameters[8].Value = model.Principal1ID;
            parameters[9].Value = model.Principal1Name;
            parameters[10].Value = model.Principal2ID;
            parameters[11].Value = model.Principal2Name;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.BranchId;
            parameters[14].Value = model.IsShowPosition;
            parameters[15].Value = model.IsComplementaryMedical;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DimissionHRDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE SEP_DimissionHRDetails ");
            strSql.Append(" WHERE DimissionHRDetailId=@DimissionHRDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionHRDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionHRDetailId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionHRDetailsInfo GetModel(int DimissionHRDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionHRDetails ");
            strSql.Append(" WHERE DimissionHRDetailId=@DimissionHRDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionHRDetailId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionHRDetailId;
            ESP.HumanResource.Entity.DimissionHRDetailsInfo model = new ESP.HumanResource.Entity.DimissionHRDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.DimissionHRDetailId = DimissionHRDetailId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_DimissionHRDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        
        /// <summary>
        /// 通过离职单编号获得人力资源审批信息
        /// </summary>
        /// <param name="dimissionId">离职单编号</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.DimissionHRDetailsInfo GetHRDetailInfo(int dimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionHRDetails ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;
            ESP.HumanResource.Entity.DimissionHRDetailsInfo model = new ESP.HumanResource.Entity.DimissionHRDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);          
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}