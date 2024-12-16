using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
	/// <summary>
    /// 数据访问类CustomerAttachDAL。
	/// </summary>
	internal class CustomerAttachDataProvider : ESP.Finance.IDataAccess.ICustomerAttachDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AttachID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_CustomerAttach");
			strSql.Append(" where AttachID=@AttachID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AttachID", SqlDbType.Int,4)};
			parameters[0].Value = AttachID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.CustomerAttachInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_CustomerAttach(");
            strSql.Append("CustomerID,Attachment,Description,FrameBeginDate,FrameEndDate,FrameContractTitle,FrameContractCode,Status,ProjectId)");
			strSql.Append(" values (");
            strSql.Append("@CustomerID,@Attachment,@Description,@FrameBeginDate,@FrameEndDate,@FrameContractTitle,@FrameContractCode,@Status,@ProjectId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
                    new SqlParameter("@FrameBeginDate", SqlDbType.DateTime,8),
					new SqlParameter("@FrameEndDate", SqlDbType.DateTime,8),
					new SqlParameter("@FrameContractTitle", SqlDbType.NVarChar,200),
                    new SqlParameter("@FrameContractCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@ProjectId",SqlDbType.Int,4),
                                        };
			parameters[0].Value =model.CustomerID;
			parameters[1].Value =model.Attachment;
			parameters[2].Value =model.Description;
            parameters[3].Value =model.FrameBeginDate;
            parameters[4].Value =model.FrameEndDate;
            parameters[5].Value =model.FrameContractTitle;
            parameters[6].Value =model.FrameContractCode;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.ProjectId;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(CustomerAttachInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_CustomerAttach set ");
			strSql.Append("CustomerID=@CustomerID,");
			strSql.Append("Attachment=@Attachment,");
			strSql.Append("Description=@Description,");
            strSql.Append("FrameBeginDate=@FrameBeginDate,");
            strSql.Append("FrameEndDate=@FrameEndDate,");
            strSql.Append("FrameContractTitle=@FrameContractTitle,");
            strSql.Append("FrameContractCode=@FrameContractCode,");
            strSql.Append("Status=@Status,ProjectId=@ProjectId");
			strSql.Append(" where AttachID=@AttachID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AttachID", SqlDbType.Int,4),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
                    new SqlParameter("@FrameBeginDate", SqlDbType.DateTime,8),
					new SqlParameter("@FrameEndDate", SqlDbType.DateTime,8),
					new SqlParameter("@FrameContractTitle", SqlDbType.NVarChar,200),
                    new SqlParameter("@FrameContractCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ProjectId", SqlDbType.Int,4),
                                        };
			parameters[0].Value =model.AttachID;
			parameters[1].Value =model.CustomerID;
			parameters[2].Value =model.Attachment;
			parameters[3].Value =model.Description;
            parameters[4].Value =model.FrameBeginDate;
            parameters[5].Value =model.FrameEndDate;
            parameters[6].Value =model.FrameContractTitle;
            parameters[7].Value =model.FrameContractCode;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.ProjectId;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int AttachID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_CustomerAttach ");
			strSql.Append(" where AttachID=@AttachID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AttachID", SqlDbType.Int,4)};
			parameters[0].Value = AttachID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Entity.CustomerAttachInfo GetModel(int AttachID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 AttachID,CustomerID,Attachment,Description,FrameBeginDate,FrameEndDate,FrameContractTitle,FrameContractCode,Status,ProjectId from F_CustomerAttach ");
			strSql.Append(" where AttachID=@AttachID ");
			SqlParameter[] parameters = {
					new SqlParameter("@AttachID", SqlDbType.Int,4)};
			parameters[0].Value = AttachID;

            return CBO.FillObject<CustomerAttachInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}




        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<CustomerAttachInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AttachID,CustomerID,Attachment,Description,FrameBeginDate,FrameEndDate,FrameContractTitle,FrameContractCode,Status,ProjectId ");
            strSql.Append(" FROM F_CustomerAttach ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<CustomerAttachInfo>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<CustomerAttachInfo>(DbHelperSQL.Query(strSql.ToString(),param));
        }


		#endregion  成员方法
	}
}

