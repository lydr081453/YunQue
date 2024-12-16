using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    class TalentDataProvider
    {
        public TalentDataProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TalentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_Talent(");
            strSql.Append("NameCN,Mobile,Position,Education,DeptShunya,PositionShunya,WorkBegin,HRInterview,GroupInterview,Resume,CreateTime,CreatorId,dept1id,dept1,dept2id,dept2,dept3id,dept3,customer,Professional,ResumeFiles,BirthDay,Language,UserId,Status,EMail)");
            strSql.Append(" values (");
            strSql.Append("@NameCN,@Mobile,@Position,@Education,@DeptShunya,@PositionShunya,@WorkBegin,@HRInterview,@GroupInterview,@Resume,@CreateTime,@CreatorId,@dept1id,@dept1,@dept2id,@dept2,@dept3id,@dept3,@customer,@Professional,@ResumeFiles,@BirthDay,@Language,@UserId,@Status,@EMail)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@NameCN", SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
                    new SqlParameter("@Position", SqlDbType.NVarChar,50),
                    new SqlParameter("@Education", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeptShunya", SqlDbType.NVarChar,50),
                    new SqlParameter("@PositionShunya", SqlDbType.NVarChar,50),

                    new SqlParameter("@WorkBegin", SqlDbType.DateTime),
                    new SqlParameter("@HRInterview", SqlDbType.NVarChar,50),
                    new SqlParameter("@GroupInterview", SqlDbType.NVarChar,50),
                    new SqlParameter("@Resume", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorId", SqlDbType.Int),

                    new SqlParameter("@dept1id", SqlDbType.Int),
                    new SqlParameter("@dept1", SqlDbType.NVarChar,50),
                    new SqlParameter("@dept2id", SqlDbType.Int),
                    new SqlParameter("@dept2", SqlDbType.NVarChar,50),
                    new SqlParameter("@dept3id", SqlDbType.Int),
                    new SqlParameter("@dept3", SqlDbType.NVarChar,50),
                    new SqlParameter("@customer", SqlDbType.NVarChar,500),
                    new SqlParameter("@Professional", SqlDbType.NVarChar,500),
                    new SqlParameter("@ResumeFiles", SqlDbType.NVarChar,500),
                      new SqlParameter("@BirthDay", SqlDbType.DateTime),
                      new SqlParameter("@Language", SqlDbType.NVarChar,50),
                       new SqlParameter("@UserId", SqlDbType.Int),
                        new SqlParameter("@Status", SqlDbType.Int),
                        new SqlParameter("@EMail", SqlDbType.NVarChar,50)

                                        };
            parameters[0].Value = model.NameCN;
            parameters[1].Value = model.Mobile;
            parameters[2].Value = model.Position;
            parameters[3].Value = model.Education;
            parameters[4].Value = model.DeptShunya;
            parameters[5].Value = model.PositionShunya;
            parameters[6].Value = model.WorkBegin;
            parameters[7].Value = model.HRInterview;
            parameters[8].Value = model.GroupInterview;
            parameters[9].Value = model.Resume;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.CreatorId;

            parameters[12].Value = model.Dept1Id;
            parameters[13].Value = model.Dept1;
            parameters[14].Value = model.Dept2Id;
            parameters[15].Value = model.Dept2;
            parameters[16].Value = model.Dept3Id;
            parameters[17].Value = model.Dept3;
            parameters[18].Value = model.Customer;
            parameters[19].Value = model.Professional;
            parameters[20].Value = model.ResumeFiles;
            parameters[21].Value = model.BirthDay;
            parameters[22].Value = model.Language;
            parameters[23].Value = model.UserId;
            parameters[24].Value = model.Status;
            parameters[25].Value = model.EMail;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TalentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_Talent set ");
            strSql.Append("NameCN=@NameCN,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("Position=@Position,");
            strSql.Append("Education=@Education,");
            strSql.Append("DeptShunya=@DeptShunya,");
            strSql.Append("PositionShunya=@PositionShunya,");
            strSql.Append("WorkBegin=@WorkBegin,");
            strSql.Append("HRInterview=@HRInterview,GroupInterview=@GroupInterview,Resume=@Resume,CreateTime=@CreateTime,CreatorId=@CreatorId,");
            strSql.Append("dept1id=@dept1id,dept1=@dept1,dept2id=@dept2id,dept2=@dept2,dept3id=@dept3id,dept3=@dept3,customer=@customer,Professional=@Professional,ResumeFiles=@ResumeFiles,BirthDay=@BirthDay,Language=@Language,UserId=@UserId,Status=@Status,EMail=@EMail");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@NameCN", SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
                    new SqlParameter("@Position", SqlDbType.NVarChar,50),
                    new SqlParameter("@Education", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeptShunya", SqlDbType.NVarChar,50),
                    new SqlParameter("@PositionShunya", SqlDbType.NVarChar,50),

                    new SqlParameter("@WorkBegin", SqlDbType.DateTime),
                    new SqlParameter("@HRInterview", SqlDbType.NVarChar,50),
                    new SqlParameter("@GroupInterview", SqlDbType.NVarChar,50),
                    new SqlParameter("@Resume", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreatorId", SqlDbType.Int),

                    new SqlParameter("@dept1id", SqlDbType.Int),
                    new SqlParameter("@dept1", SqlDbType.NVarChar,50),
                    new SqlParameter("@dept2id", SqlDbType.Int),
                    new SqlParameter("@dept2", SqlDbType.NVarChar,50),
                    new SqlParameter("@dept3id", SqlDbType.Int),
                    new SqlParameter("@dept3", SqlDbType.NVarChar,50),
                    new SqlParameter("@customer", SqlDbType.NVarChar,500),
                    new SqlParameter("@Professional", SqlDbType.NVarChar,500),
                    new SqlParameter("@ResumeFiles", SqlDbType.NVarChar,500),
                     new SqlParameter("@BirthDay", SqlDbType.DateTime),
                      new SqlParameter("@Language", SqlDbType.NVarChar,50),
                       new SqlParameter("@UserId", SqlDbType.Int),
                        new SqlParameter("@Status", SqlDbType.Int),
                        new SqlParameter("@EMail", SqlDbType.NVarChar,50),
                    new SqlParameter("@Id", SqlDbType.Int,8)
                                        };
            parameters[0].Value = model.NameCN;
            parameters[1].Value = model.Mobile;
            parameters[2].Value = model.Position;
            parameters[3].Value = model.Education;
            parameters[4].Value = model.DeptShunya;
            parameters[5].Value = model.PositionShunya;
            parameters[6].Value = model.WorkBegin;
            parameters[7].Value = model.HRInterview;
            parameters[8].Value = model.GroupInterview;
            parameters[9].Value = model.Resume;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.CreatorId;


            parameters[12].Value = model.Dept1Id;
            parameters[13].Value = model.Dept1;
            parameters[14].Value = model.Dept2Id;
            parameters[15].Value = model.Dept2;
            parameters[16].Value = model.Dept3Id;
            parameters[17].Value = model.Dept3;
            parameters[18].Value = model.Customer;
            parameters[19].Value = model.Professional;
            parameters[20].Value = model.ResumeFiles;
            parameters[21].Value = model.BirthDay;
            parameters[22].Value = model.Language;
            parameters[23].Value = model.UserId;
            parameters[24].Value = model.Status;
            parameters[25].Value = model.EMail;
            parameters[26].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SEP_Talent ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TalentInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from SEP_Talent ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            TalentInfo model = new TalentInfo();
            return CBO.FillObject<TalentInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<TalentInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SEP_Talent ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<TalentInfo>(DbHelperSQL.Query(strSql.ToString()));
        }


    }
}
