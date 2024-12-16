using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    ///RecipientDataHelper 的摘要说明
    /// </summary>
    public class RecipientDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipientDataHelper"/> class.
        /// </summary>
        public RecipientDataHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region  成员方法
        /// <summary>
        /// 添加数据并更新GeneralInfo
        /// </summary>
        /// <param name="model"></param>
        /// <param name="generalModel"></param>
        /// <returns></returns>
        public int AddAndUpdateStatus(RecipientInfo model, GeneralInfo generalModel,List<Entity.ScoreRecordInfo> scoreList)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int recipientId = Add(model, conn, trans);
                    GeneralInfoDataProvider.Update(generalModel, conn, trans);
                    foreach (Entity.ScoreRecordInfo score in scoreList)
                    {
                        new ScoreRecordDataProvider().Add(score,trans);
                    }
                    trans.Commit();
                    return recipientId;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(RecipientInfo model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public int Add(RecipientInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Recipient(");
            strSql.Append("Gid,RecipientName,RecipientDate,Note,RecipientAmount,Status,receivePrice,account_name,account_bank,account_number,RecipientNo,AppraiseRemark,SinglePrice,Num,Des,IsConfirm,fileUrl)");
            strSql.Append(" values (");
            strSql.Append("@Gid,@RecipientName,@RecipientDate,@Note,@RecipientAmount,@Status,@receivePrice,@account_name,@account_bank,@account_number,@RecipientNo,@AppraiseRemark,@SinglePrice,@Num,@Des,@IsConfirm,@fileUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Gid", SqlDbType.Int,4),
					new SqlParameter("@RecipientName", SqlDbType.NVarChar),
					new SqlParameter("@RecipientDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Note", SqlDbType.NVarChar),
					new SqlParameter("@RecipientAmount", SqlDbType.Decimal,9),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@receivePrice",SqlDbType.Int,4),
                    new SqlParameter("@account_name",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_bank",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_number",SqlDbType.NVarChar,100),
                    new SqlParameter("@RecipientNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@AppraiseRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@SinglePrice", SqlDbType.NVarChar,200),
					new SqlParameter("@Num", SqlDbType.NVarChar,200),
					new SqlParameter("@Des", SqlDbType.NVarChar,200),
                    new SqlParameter("@IsConfirm",SqlDbType.Int,4),
                                        new SqlParameter("@fileUrl",SqlDbType.NVarChar,100)};
            parameters[0].Value = model.Gid;
            parameters[1].Value = model.RecipientName;
            parameters[2].Value = model.RecipientDate;
            parameters[3].Value = model.Note;
            parameters[4].Value = model.RecipientAmount;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.receivePrice;
            parameters[7].Value = model.account_name;
            parameters[8].Value = model.account_bank;
            parameters[9].Value = model.account_number;
            parameters[10].Value = model.RecipientNo;
            parameters[11].Value = model.AppraiseRemark;
            parameters[12].Value = model.SinglePrice;
            parameters[13].Value = model.Num;
            parameters[14].Value = model.Des;
            parameters[15].Value = model.IsConfirm;
            parameters[16].Value = model.FileUrl;

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
        /// <param name="model">The model.</param>
        public static void Update(RecipientInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Recipient set ");
            strSql.Append("Gid=@Gid,");
            strSql.Append("RecipientName=@RecipientName,");
            strSql.Append("RecipientDate=@RecipientDate,");
            strSql.Append("Note=@Note,");
            strSql.Append("RecipientAmount=@RecipientAmount,");
            strSql.Append("Status=@Status,");
            strSql.Append("receivePrice=@receivePrice");
            strSql.Append(",account_name=@account_name,account_bank=@account_bank,account_number=@account_number,");
            strSql.Append("RecipientNo=@RecipientNo,AppraiseRemark=@AppraiseRemark,");
            strSql.Append("SinglePrice=@SinglePrice,");
            strSql.Append("Num=@Num,");
            strSql.Append("Des=@Des,fileUrl=@fileUrl ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Gid", SqlDbType.Int,4),
					new SqlParameter("@RecipientName", SqlDbType.NVarChar),
					new SqlParameter("@RecipientDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Note", SqlDbType.NVarChar),
					new SqlParameter("@RecipientAmount", SqlDbType.Decimal,9),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@receivePrice",SqlDbType.Int,4),
                    new SqlParameter("@account_name",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_bank",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_number",SqlDbType.NVarChar,100),
                    new SqlParameter("@RecipientNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@AppraiseRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@SinglePrice", SqlDbType.NVarChar,200),
					new SqlParameter("@Num", SqlDbType.NVarChar,200),
					new SqlParameter("@Des", SqlDbType.NVarChar,200),
                    new SqlParameter("@fileUrl", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Gid;
            parameters[2].Value = model.RecipientName;
            parameters[3].Value = model.RecipientDate;
            parameters[4].Value = model.Note;
            parameters[5].Value = model.RecipientAmount;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.receivePrice;
            parameters[8].Value = model.account_name;
            parameters[9].Value = model.account_bank;
            parameters[10].Value = model.account_number;
            parameters[11].Value = model.RecipientNo;
            parameters[12].Value = model.AppraiseRemark;
            parameters[13].Value = model.SinglePrice;
            parameters[14].Value = model.Num;
            parameters[15].Value = model.Des;
            parameters[16].Value = model.FileUrl;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public static void UpdateConfirm(RecipientInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Recipient set ");
            strSql.Append("Note=@Note,");
            strSql.Append("RecipientAmount=@RecipientAmount,");
            strSql.Append("isconfirm=@isconfirm,");
            strSql.Append("receivePrice=@receivePrice,");
            strSql.Append("AppraiseRemark=@AppraiseRemark,");
            strSql.Append("SinglePrice=@SinglePrice,");
            strSql.Append("Num=@Num,");
            strSql.Append("Des=@Des,fileUrl=@fileUrl ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Note", SqlDbType.NVarChar),
					new SqlParameter("@RecipientAmount", SqlDbType.Decimal,9),
					new SqlParameter("@isconfirm", SqlDbType.Int,4),
                    new SqlParameter("@receivePrice",SqlDbType.Int,4),
                    new SqlParameter("@AppraiseRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@SinglePrice", SqlDbType.NVarChar,200),
					new SqlParameter("@Num", SqlDbType.NVarChar,200),
					new SqlParameter("@Des", SqlDbType.NVarChar,200),
                    new SqlParameter("@fileUrl", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Note;
            parameters[2].Value = model.RecipientAmount;
            parameters[3].Value = model.IsConfirm;
            parameters[4].Value = model.receivePrice;
            parameters[5].Value = model.AppraiseRemark;
            parameters[6].Value = model.SinglePrice;
            parameters[7].Value = model.Num;
            parameters[8].Value = model.Des;
            parameters[9].Value = model.FileUrl;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public static void Update(RecipientInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Recipient set ");
            strSql.Append("Gid=@Gid,");
            strSql.Append("RecipientName=@RecipientName,");
            strSql.Append("RecipientDate=@RecipientDate,");
            strSql.Append("Note=@Note,");
            strSql.Append("RecipientAmount=@RecipientAmount,");
            strSql.Append("Status=@Status,");
            strSql.Append("receivePrice=@receivePrice");
            strSql.Append(",account_name=@account_name,account_bank=@account_bank,account_number=@account_number,");
            strSql.Append("RecipientNo=@RecipientNo,AppraiseRemark=@AppraiseRemark,");
            strSql.Append("SinglePrice=@SinglePrice,");
            strSql.Append("Num=@Num,");
            strSql.Append("Des=@Des,fileUrl=@fileUrl ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Gid", SqlDbType.Int,4),
					new SqlParameter("@RecipientName", SqlDbType.NVarChar),
					new SqlParameter("@RecipientDate", SqlDbType.SmallDateTime),
					new SqlParameter("@Note", SqlDbType.NVarChar),
					new SqlParameter("@RecipientAmount", SqlDbType.Decimal,9),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@receivePrice",SqlDbType.Int,4),
                    new SqlParameter("@account_name",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_bank",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_number",SqlDbType.NVarChar,100),
                    new SqlParameter("@RecipientNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@AppraiseRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@SinglePrice", SqlDbType.NVarChar,200),
					new SqlParameter("@Num", SqlDbType.NVarChar,200),
					new SqlParameter("@Des", SqlDbType.NVarChar,200),
                                        new SqlParameter("@fileUrl", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Gid;
            parameters[2].Value = model.RecipientName;
            parameters[3].Value = model.RecipientDate;
            parameters[4].Value = model.Note;
            parameters[5].Value = model.RecipientAmount;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.receivePrice;
            parameters[8].Value = model.account_name;
            parameters[9].Value = model.account_bank;
            parameters[10].Value = model.account_number;
            parameters[11].Value = model.RecipientNo;
            parameters[12].Value = model.AppraiseRemark;
            parameters[13].Value = model.SinglePrice;
            parameters[14].Value = model.Num;
            parameters[15].Value = model.Des;

            DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parameters);
        }
        /// <summary>
        /// 更新付款状态
        /// </summary>
        /// <param name="recipientIds">更新的Ids</param>
        /// <param name="receivePrice">状态</param>
        public static void updateStatus(string recipientIds, int receivePrice)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_Recipient set ");
            strSql.Append(" receivePrice=@receivePrice");
            strSql.Append(" where Id in (" + recipientIds + ")");
            SqlParameter[] parms = { 
                    new SqlParameter("@receivePrice",SqlDbType.Int,4)};
            parms[0].Value = receivePrice;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parms);
        }

        /// <summary>
        /// 更新确认状态
        /// </summary>
        /// <param name="recipientId">The recipient id.</param>
        /// <param name="confirm">The confirm.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        public static void updateConfirm(int recipientId, int confirm, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_Recipient set ");
            strSql.Append(" isConfirm=@isConfirm");
            strSql.Append(" where Id in (" + recipientId + ")");
            SqlParameter[] parms = { 
                    new SqlParameter("@isConfirm",SqlDbType.Int,4)
            };
            parms[0].Value = confirm;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parms);
        }

        /// <summary>
        /// 收货单关联付款账期时更新收货单状态
        /// </summary>
        /// <param name="recipientIds">收货单ID列表</param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        public static void updateConfirmPayment(string recipientIds, int confirm, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_Recipient set ");
            strSql.Append(" isConfirm=@isConfirm");
            strSql.Append(" where Id in (" + recipientIds + ")");
            SqlParameter[] parms = { 
                    new SqlParameter("@isConfirm",SqlDbType.Int,4)
            };
            parms[0].Value = confirm;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parms);
        }

        public static decimal GetSumRecipientConfirmed(int generalID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(RecipientAmount) from T_Recipient where Gid={0}and IsConfirm in({1},{2})");
            string sql = string.Format(strSql.ToString(), generalID, State.recipentConfirm_PaymentCommit, State.recipentConfirm_PaymentCommit);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="Id">The id.</param>
        public static void Delete(int Id, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Recipient ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }

        public static RecipientInfo GetModel(int Id)
        {
            return GetModel(Id, null);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static RecipientInfo GetModel(int Id, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Recipient ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            RecipientInfo model = new RecipientInfo();
            DataSet ds = null;
            if(trans == null)
                ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            else
                ds = DbHelperSQL.Query(strSql.ToString(),trans.Connection,trans, parameters);
            model.Id = Id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Gid"].ToString() != "")
                {
                    model.Gid = int.Parse(ds.Tables[0].Rows[0]["Gid"].ToString());
                }
                model.RecipientName = ds.Tables[0].Rows[0]["RecipientName"].ToString();
                if (ds.Tables[0].Rows[0]["RecipientDate"].ToString() != "")
                {
                    model.RecipientDate = DateTime.Parse(ds.Tables[0].Rows[0]["RecipientDate"].ToString());
                }
                model.Note = ds.Tables[0].Rows[0]["Note"].ToString();
                if (ds.Tables[0].Rows[0]["RecipientAmount"].ToString() != "")
                {
                    model.RecipientAmount = decimal.Parse(ds.Tables[0].Rows[0]["RecipientAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsConfirm"].ToString() != "")
                {
                    model.IsConfirm = int.Parse(ds.Tables[0].Rows[0]["IsConfirm"].ToString());
                }
                model.receivePrice = ds.Tables[0].Rows[0]["receivePrice"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["receivePrice"].ToString());
                model.account_name = ds.Tables[0].Rows[0]["account_name"].ToString();
                model.account_bank = ds.Tables[0].Rows[0]["account_bank"].ToString();
                model.account_number = ds.Tables[0].Rows[0]["account_number"].ToString();
                model.RecipientNo = ds.Tables[0].Rows[0]["RecipientNo"].ToString();
                model.AppraiseRemark = ds.Tables[0].Rows[0]["AppraiseRemark"].ToString();
                model.SinglePrice = ds.Tables[0].Rows[0]["SinglePrice"].ToString();
                model.Num = ds.Tables[0].Rows[0]["Num"].ToString();
                model.Des = ds.Tables[0].Rows[0]["Des"].ToString();
                model.FileUrl = ds.Tables[0].Rows[0]["FileUrl"].ToString();
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
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Recipient ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得已收货列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static DataSet GetRecipientList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT distinct a.SinglePrice,a.Num,a.Des,a.Id as recipientId, a.Gid, a.RecipientName, a.RecipientDate, a.Note, a.RecipientAmount, a.Status, a.receivePrice,a.account_name,a.account_bank,a.account_number,a.RecipientNo, b.id AS Expr1, b.prNo, b.requestor, 
                      b.requestorname, b.app_date, b.requestor_info, b.requestor_group, b.enduser, b.endusername, b.enduser_info, b.enduser_group, b.goods_receiver, 
                      b.receivername, b.receivergroup, b.receiver_info, b.ship_address, b.project_code, b.project_descripttion, b.buggeted, b.supplier_name, 
                      b.supplier_address, b.supplier_linkman, b.supplier_cellphone, b.supplier_phone, b.supplier_fax, b.supplier_email, b.source, b.fa_no, b.sow, 
                      b.payment_terms, b.orderid, b.type, b.contrast, b.consult, b.first_assessor, b.first_assessorname, b.afterwards, b.afterwardsname, b.status AS Expr2, 
                      b.others, b.sow2, b.sow3, b.sow4, b.sow5, b.thirdParty_materielDesc, b.moneyType, b.requisition_overrule, b.order_overrule, b.lasttime, 
                      b.requisition_committime, b.order_committime, b.order_audittime, b.EmBuy, b.CusAsk, b.CusName, b.ContractNo, b.Filiale_Auditor, b.Department, 
                      b.requisitionflow, b.addstatus, b.totalprice, b.afterwardsReason, b.EmBuyReason, b.CusAskYesReason, b.contrastFile, b.consultFile, 
                      b.receiver_Otherinfo, b.fili_overrule, b.receivePrepay,b.prtype,b.inUse,b.appendReceiver,b.appendReceiverName,a.isconfirm,a.fileurl
                        FROM         T_Recipient AS a INNER JOIN
                                              T_GeneralInfo AS b ON a.Gid = b.id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1" + strWhere);
            }
            strSql.Append(" order by a.RecipientDate desc");
            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray());
        }

        /// <summary>
        /// 获得每条单子第一次收货的对象
        /// </summary>
        /// <param name="generalId">The general id.</param>
        /// <returns></returns>
        public static RecipientInfo getTopModelByGeneralID(int generalId)
        {
            RecipientInfo model = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM T_Recipient ");
            strSql.Append(" where 1=1 and gid=" + generalId + " order by a.id");
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql.ToString()))
            {
                while (r.Read())
                {
                    model = new RecipientInfo();
                    model.PopupData(r);
                }
                r.Close();
            }
            return model;
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<RecipientInfo> getModelList(string terms, List<SqlParameter> parms)
        {
            List<RecipientInfo> list = new List<RecipientInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Recipient ");
            strSql.Append(" where 1=1 {0} order by id desc");
            string sql = string.Format(strSql.ToString(), terms);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    RecipientInfo c = new RecipientInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        public static int getRecipientCount(int generalid)
        {
            return getRecipientCount(generalid, null, null);
        }

        /// <summary>
        /// Gets the recipient count.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static int getRecipientCount(int generalid, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(Gid) from T_Recipient where Gid={0}");
            string sql = string.Format(strSql.ToString(), generalid);
            object obj = null;
            if (conn == null)
                obj = DbHelperSQL.GetSingle(sql.ToString());
            else
                DbHelperSQL.GetSingle(sql.ToString(), conn, trans, null);
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
        /// Gets the group by recipient.
        /// </summary>
        /// <param name="wherestr">The wherestr.</param>
        /// <returns></returns>
        public static IList<string> getGroupByRecipient(string wherestr)
        {
            IList<string> list = new List<string>();
            string sql = "select gid from t_recipient where 1=1 "; ;
            if (!string.IsNullOrEmpty(wherestr))
            {
                sql += " and id in (" + wherestr + ")";
            }
            sql += " group by gid ";
            DataSet dataset = DbHelperSQL.Query(sql);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0] != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        string arr = string.Empty;
                        if (row["gid"].ToString() != "")
                            arr = row["gid"].ToString();

                        list.Add(arr);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 检查传进来的一组收货ID中是否存在金额不符的收货
        /// </summary>
        /// <param name="wherestr">The wherestr.</param>
        /// <returns>
        /// 	<c>true</c> if [is unsure recipient] [the specified wherestr]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnsureRecipient(string wherestr)
        {
            //IList<string> list = new List<string>();
            bool result = false;
            string sql = "select * from t_recipient where 1=1 "; ;
            if (!string.IsNullOrEmpty(wherestr))
            {
                sql += " and id in (" + wherestr + ")";
            }
            DataSet dataset = DbHelperSQL.Query(sql);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0] != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        if (null != row["status"] && row["status"].ToString() == State.recipientstatus_Unsure.ToString())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public static bool IsUnsureRecipient(string wherestr, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            bool result = false;
            string sql = "select * from t_recipient where 1=1 "; ;
            if (!string.IsNullOrEmpty(wherestr))
            {
                sql += " and id in (" + wherestr + ")";
            }
            DataSet dataset = DbHelperSQL.Query(sql, conn, trans);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0] != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        if (null != row["status"] && row["status"].ToString() == State.recipientstatus_Unsure.ToString())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 取得一个pr单里面所有的收货总金额.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public static decimal getRecipientSum(int generalid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(RecipientAmount) from T_Recipient where Gid={0}");
            string sql = string.Format(strSql.ToString(), generalid);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 获取申请单下的收货次数
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int getRecipientCountByGid(int gid, SqlTransaction trans)
        {
            string strSql = "select count(*) as recipientCount from t_recipient where gid="+gid;
            object obj = DbHelperSQL.GetSingle(strSql, trans.Connection, trans);
            return int.Parse(obj.ToString());
        }

        public static decimal getRecipientSum(int generalid, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(RecipientAmount) from T_Recipient where Gid={0}");
            string sql = string.Format(strSql.ToString(), generalid);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString(), conn, trans);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 获取未创建付款申请的收货信息
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataSet getNotCreatePNList(string terms, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select  a.*,b.prno,b.orderid,b.requestor,b.requestorname,b.goods_receiver, b.receivername,b.project_code,b.supplier_name,b.appendreceiver,b.appendreceivername  from t_recipient as a");
            //strSql.Append(" inner join t_generalinfo as b on a.gid=b.id");
            ////strSql.Append(" left join (select rid, max(logmodifiedTime) as appendTime from t_recipientlog where des like '%确认收货' and des not like '%供应商确认收货' group by rid) as c on a.id=c.rid");
            ////strSql.Append(" left join (select rid, max(logmodifiedTime) as supplierTime from t_recipientlog where des like '%已确认%' group by rid) as d on a.id=d.rid");
            //strSql.Append(" where b.source='协议供应商' and a.isconfirm <>" + State.recipentConfirm_PaymentCommit);

            strSql.Append(" select  a.*,c.bjpaymentuserid,c.shpaymentuserid,c.gzpaymentuserid,b.prno,b.orderid,b.requestor,b.requestorname,b.goods_receiver, b.receivername,b.project_code,b.supplier_name,b.appendreceiver,b.appendreceivername  from t_recipient as a");
            strSql.Append(" inner join t_generalinfo as b on a.gid=b.id");
            strSql.Append(" inner join (select general_id,supplierId,bjpaymentuserid,shpaymentuserid,gzpaymentuserid from (select min(id) as id,general_id,producttype,supplierId from t_orderinfo");
            strSql.Append(" group by general_id,producttype,supplierId) as a ");
            strSql.Append(" inner join t_type as b on a.producttype=b.typeid) as c on b.id=c.general_id");
            strSql.Append(" inner join t_supplier as d on c.supplierId=d.id");
            strSql.Append(" where b.inuse=" + (int)State.PRInUse.Use + @" and b.source='协议供应商' and b.id in (select id from (
                                select id, (select count(*) from t_paymentperiod where gid=a.id and status=0) counts,
                                (select count(*) from t_recipient where gid=a.id and isconfirm="+ State.recipentConfirm_Supplier + @") count2 
                                    from t_generalinfo a)aa where counts!=0 and count2!=0)");
            if (terms != "")
                strSql.Append(terms);
            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray());
        }

        public static DataSet GetPaymentNotifyAuditing(string terms, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
             strSql.Append(" select distinct * from f_return a inner join (select general_id,supplierId,bjpaymentuserid,shpaymentuserid,gzpaymentuserid ");
            strSql.Append(" from (select min(id) as id,general_id,producttype,supplierId from t_orderinfo ");
            strSql.Append("  group by general_id,producttype,supplierId) as a ");
            strSql.Append(" inner join t_type as b on a.producttype=b.typeid)as b on a.prid=b.general_id ");
            strSql.Append(" inner join t_supplier as c on b.supplierId=c.id");
            strSql.Append(" where needpurchaseaudit=1 and returnstatus in(1,2)");
            if (terms != "")
                strSql.Append(terms);
            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray());
        }

        #endregion  成员方法
    }
}