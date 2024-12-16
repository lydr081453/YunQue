using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Administrative.DataAccess
{
    public class CardStoreDataProvider
    {
        public CardStoreDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_CardStore");
            strSql.Append(" where ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CardStoreInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_CardStore(");
            strSql.Append("CardNo,State,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,sort,AreaID)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@State,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@sort,@AreaID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID", SqlDbType.Int, 4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.State;
            parameters[2].Value = model.Deleted;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdateTime;
            parameters[5].Value = model.OperatorID;
            parameters[6].Value = model.OperatorDept;
            parameters[7].Value = model.sort;
            parameters[8].Value = model.AreaID;

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
        public int Add(CardStoreInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_CardStore(");
            strSql.Append("CardNo,State,Deleted,CreateTime,UpdateTime,OperatorID,OperatorDept,sort,AreaID)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@State,@Deleted,@CreateTime,@UpdateTime,@OperatorID,@OperatorDept,@sort,@AreaID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID", SqlDbType.Int, 4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.State;
            parameters[2].Value = model.Deleted;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdateTime;
            parameters[5].Value = model.OperatorID;
            parameters[6].Value = model.OperatorDept;
            parameters[7].Value = model.sort;
            parameters[8].Value = model.AreaID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
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
        public void Update(CardStoreInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_CardStore set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("State=@State,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept,");
            strSql.Append("sort=@sort,");
            strSql.Append("AreaID=@AreaID ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID", SqlDbType.Int, 4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CardNo;
            parameters[2].Value = model.State;
            parameters[3].Value = model.Deleted;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.UpdateTime;
            parameters[6].Value = model.OperatorID;
            parameters[7].Value = model.OperatorDept;
            parameters[8].Value = model.sort;
            parameters[9].Value = model.AreaID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(CardStoreInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_CardStore set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("State=@State,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperatorID=@OperatorID,");
            strSql.Append("OperatorDept=@OperatorDept,");
            strSql.Append("sort=@sort,");
            strSql.Append("AreaID=@AreaID");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.BigInt,8),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperatorID", SqlDbType.Int,4),
					new SqlParameter("@OperatorDept", SqlDbType.Int,4),
					new SqlParameter("@sort", SqlDbType.Int,4),
                    new SqlParameter("@AreaID", SqlDbType.Int, 4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CardNo;
            parameters[2].Value = model.State;
            parameters[3].Value = model.Deleted;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.UpdateTime;
            parameters[6].Value = model.OperatorID;
            parameters[7].Value = model.OperatorDept;
            parameters[8].Value = model.sort;
            parameters[9].Value = model.AreaID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_CardStore ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CardStoreInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_CardStore ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            CardStoreInfo model = new CardStoreInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = ID;
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
            strSql.Append("select * ");
            strSql.Append(" FROM AD_CardStore ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得最小的门卡记录信息对象
        /// </summary>
        /// <returns>返回门卡记录信息对象</returns>
        public CardStoreInfo GetFirstCardModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * FROM AD_CardStore where Deleted=0 and State=" + (int)CardStoreState.NotUsed + " ORDER BY Sort asc, Cardno asc");
            SqlParameter[] parameters = { new SqlParameter("@State", SqlDbType.Int, 4) };
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            CardStoreInfo model = new CardStoreInfo();
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
        /// 各地区获得各地区的可用门卡信息
        /// </summary>
        /// <param name="areaid">地区编号</param>
        /// <returns>返回一个可以使用门卡信息</returns>
        public CardStoreInfo GetFirstCardModel(int areaid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * FROM AD_CardStore where Deleted=0 and State=" + (int)CardStoreState.NotUsed + " and AreaID=" + areaid + " ORDER BY Sort asc, Cardno asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            CardStoreInfo model = new CardStoreInfo();
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
        /// 通过门卡号从门卡信息库中获得门卡记录信息
        /// </summary>
        /// <param name="cardNo">门卡号</param>
        /// <returns>返回一个门卡记录信息</returns>
        public CardStoreInfo GetModelByCardNo(string cardNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM AD_CardStore where CardNo=@CardNo and deleted=0");
            SqlParameter[] parameters = { new SqlParameter("@CardNo", SqlDbType.Int, 4) };
            parameters[0].Value = cardNo;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            CardStoreInfo model = new CardStoreInfo();
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
        /// 获得门卡库存数量
        /// </summary>
        /// <returns>返回一个门卡库存数量值</returns>
        public int GetCardStoreCount(int areaid)
        {
            int count = 0;
            string sql = "select count(*) from ad_cardstore where deleted=0 and state = 0 and AreaId=" + areaid;
            DataSet ds = DbHelperSQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                count = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            return count;
        }
        #endregion  成员方法
    }
}