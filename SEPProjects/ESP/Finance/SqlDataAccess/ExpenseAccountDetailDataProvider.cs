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
    /// 数据访问类ExpenseDAL。
    /// </summary>
    internal class ExpenseAccountDetailDataProvider : ESP.Finance.IDataAccess.IExpenseAccountDetailProvider
    {

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpenseAccountDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseAccountDetail(");
            //IsRoundTrip,GoTime,BackTime,Boarder,BoarderIDCard
            strSql.Append("ReturnID,ExpenseDate,ExpenseType,ExpenseDesc,ExpenseMoney,FinanceMemo,Creater,CreaterName,CreateTime,PhoneYear,PhoneMonth,MealFeeMorning,MealFeeNoon,MealFeeNight,CostDetailID,ExpenseTypeNumber,Recipient,BankName,BankAccountNo,Status,City,TicketSource,TicketDestination,IsRoundTrip,GoTime,BackTime,Boarder,BoarderIDCard,TripType,BoarderMobile,GoAirNo,BackAirNo,GoAmount,BackAmount,TicketStatus,parentId,BoarderId,BoarderIDType,TicketIsUsed,PhoneInvoice,PhoneInvoiceNo,TicketIsConfirm)");
            strSql.Append(" values (");
            strSql.Append("@ReturnID,@ExpenseDate,@ExpenseType,@ExpenseDesc,@ExpenseMoney,@FinanceMemo,@Creater,@CreaterName,@CreateTime,@PhoneYear,@PhoneMonth,@MealFeeMorning,@MealFeeNoon,@MealFeeNight,@CostDetailID,@ExpenseTypeNumber,@Recipient,@BankName,@BankAccountNo,@Status,@City,@TicketSource,@TicketDestination,@IsRoundTrip,@GoTime,@BackTime,@Boarder,@BoarderIDCard,@TripType,@BoarderMobile,@GoAirNo,@BackAirNo,@GoAmount,@BackAmount,@TicketStatus,@parentId,@BoarderId,@BoarderIDType,@TicketIsUsed,@PhoneInvoice,@PhoneInvoiceNo,@TicketIsConfirm)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDate", SqlDbType.SmallDateTime),
					new SqlParameter("@ExpenseType", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDesc", SqlDbType.NVarChar,2000),
					new SqlParameter("@ExpenseMoney", SqlDbType.Decimal,9),
					new SqlParameter("@FinanceMemo", SqlDbType.NVarChar,2000),
					new SqlParameter("@Creater", SqlDbType.Int,4),
					new SqlParameter("@CreaterName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@PhoneYear", SqlDbType.Int,4),
					new SqlParameter("@PhoneMonth", SqlDbType.Int,4),
					new SqlParameter("@MealFeeMorning", SqlDbType.Int,4),
					new SqlParameter("@MealFeeNoon", SqlDbType.Int,4),
					new SqlParameter("@MealFeeNight", SqlDbType.Int,4),
                    new SqlParameter("@CostDetailID",SqlDbType.Int,4),
                    new SqlParameter("@ExpenseTypeNumber",SqlDbType.Int,4),
                    new SqlParameter("@Recipient", SqlDbType.NVarChar,50),
                    new SqlParameter("@BankName", SqlDbType.NVarChar,200),
                    new SqlParameter("@BankAccountNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@City", SqlDbType.NVarChar,50),
                    new SqlParameter("@TicketSource", SqlDbType.NVarChar,50),
                    new SqlParameter("@TicketDestination", SqlDbType.NVarChar,50),

                    new SqlParameter("@IsRoundTrip", SqlDbType.Bit),
                      new SqlParameter("@GoTime", SqlDbType.DateTime),
                       new SqlParameter("@BackTime", SqlDbType.DateTime),
                        new SqlParameter("@Boarder", SqlDbType.NVarChar,50),
                         new SqlParameter("@BoarderIDCard", SqlDbType.NVarChar,18),
                         new SqlParameter("@TripType", SqlDbType.Int,4),
                         //BoarderMobile,GoAirNo,BackAirNo,GoAmount,BackAmount
                         new SqlParameter("@BoarderMobile", SqlDbType.NVarChar,50),
                         new SqlParameter("@GoAirNo", SqlDbType.NVarChar,50),
                         new SqlParameter("@BackAirNo", SqlDbType.NVarChar,50),
                         new SqlParameter("@GoAmount", SqlDbType.Decimal,20),
                         new SqlParameter("@BackAmount", SqlDbType.Decimal,20),
                         new SqlParameter("@TicketStatus",SqlDbType.Int,4),
                         new SqlParameter("@ParentId",SqlDbType.Int,4),
                         new SqlParameter("@BoarderId",SqlDbType.Int,4),
                         new SqlParameter("@BoarderIDType",SqlDbType.NVarChar,50),
                         new SqlParameter("@TicketIsUsed",SqlDbType.Bit),
                         new SqlParameter("@PhoneInvoice",SqlDbType.Int,4),
                         new SqlParameter("@PhoneInvoiceNo",SqlDbType.NVarChar,50),
                          new SqlParameter("@TicketIsConfirm",SqlDbType.Bit)
            };
            parameters[0].Value = model.ReturnID;
            parameters[1].Value = model.ExpenseDate;
            parameters[2].Value = model.ExpenseType;
            parameters[3].Value = model.ExpenseDesc;
            parameters[4].Value = model.ExpenseMoney;
            parameters[5].Value = model.FinanceMemo;
            parameters[6].Value = model.Creater;
            parameters[7].Value = model.CreaterName;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.PhoneYear;
            parameters[10].Value = model.PhoneMonth;
            parameters[11].Value = model.MealFeeMorning;
            parameters[12].Value = model.MealFeeNoon;
            parameters[13].Value = model.MealFeeNight;
            parameters[14].Value = model.CostDetailID;
            parameters[15].Value = model.ExpenseTypeNumber;
            parameters[16].Value = model.Recipient;
            parameters[17].Value = model.BankName;
            parameters[18].Value = model.BankAccountNo;
            parameters[19].Value = model.Status;
            parameters[20].Value = model.City;
            parameters[21].Value = model.TicketSource;
            parameters[22].Value = model.TicketDestination;

            parameters[23].Value = model.IsRoundTrip;
            parameters[24].Value = model.GoTime;
            parameters[25].Value = model.BackTime;
            parameters[26].Value = model.Boarder;
            parameters[27].Value = model.BoarderIDCard;
            parameters[28].Value = model.TripType;
            //BoarderMobile,GoAirNo,BackAirNo,GoAmount,BackAmount
            parameters[29].Value = model.BoarderMobile;
            parameters[30].Value = model.GoAirNo;
            parameters[31].Value = model.BackAirNo;
            parameters[32].Value = model.GoAmount;
            parameters[33].Value = model.BackAmount;
            parameters[34].Value = model.TicketStatus;
            parameters[35].Value = model.ParentId;
            parameters[36].Value = model.BoarderId;
            parameters[37].Value = model.BoarderIDType;
            parameters[38].Value = model.TicketIsUsed;

            parameters[39].Value = model.PhoneInvoice;
            parameters[40].Value = model.PhoneInvoiceNo;
            parameters[41].Value = model.TicketIsConfirm;

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
        public int Update(ESP.Finance.Entity.ExpenseAccountDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseAccountDetail set ");
            strSql.Append("ReturnID=@ReturnID,");
            strSql.Append("ExpenseDate=@ExpenseDate,");
            strSql.Append("ExpenseType=@ExpenseType,");
            strSql.Append("ExpenseDesc=@ExpenseDesc,");
            strSql.Append("ExpenseMoney=@ExpenseMoney,");
            strSql.Append("FinanceMemo=@FinanceMemo,");
            strSql.Append("Creater=@Creater,");
            strSql.Append("CreaterName=@CreaterName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("PhoneYear=@PhoneYear,");
            strSql.Append("PhoneMonth=@PhoneMonth,");
            strSql.Append("MealFeeMorning=@MealFeeMorning,");
            strSql.Append("MealFeeNoon=@MealFeeNoon,");
            strSql.Append("MealFeeNight=@MealFeeNight,");
            strSql.Append("CostDetailID = @CostDetailID,");
            strSql.Append("ExpenseTypeNumber = @ExpenseTypeNumber, ");
            strSql.Append("Recipient = @Recipient, ");
            strSql.Append("BankName = @BankName, ");
            strSql.Append("BankAccountNo = @BankAccountNo, ");
            strSql.Append("Status = @Status,City=@City, ");
            strSql.Append("TicketSource = @TicketSource,TicketDestination=@TicketDestination, ");
            strSql.Append("IsRoundTrip=@IsRoundTrip,GoTime=@GoTime,BackTime=@BackTime,Boarder=@Boarder,BoarderIDCard=@BoarderIDCard,TripType=@TripType,");
            strSql.Append("BoarderMobile=@BoarderMobile,GoAirNo=@GoAirNo,BackAirNo=@BackAirNo,GoAmount=@GoAmount,BackAmount=@BackAmount,");
            strSql.Append("TicketStatus=@TicketStatus,ParentId=@ParentId,BoarderId=@BoarderId,BoarderIDType=@BoarderIDType,TicketIsUsed=@TicketIsUsed,");
            strSql.Append("PhoneInvoice=@PhoneInvoice,PhoneInvoiceNo=@PhoneInvoiceNo,TicketIsConfirm=@TicketIsConfirm");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDate", SqlDbType.SmallDateTime),
					new SqlParameter("@ExpenseType", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDesc", SqlDbType.NVarChar,2000),
					new SqlParameter("@ExpenseMoney", SqlDbType.Decimal,9),
					new SqlParameter("@FinanceMemo", SqlDbType.NVarChar,2000),
					new SqlParameter("@Creater", SqlDbType.Int,4),
					new SqlParameter("@CreaterName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@PhoneYear", SqlDbType.Int,4),
					new SqlParameter("@PhoneMonth", SqlDbType.Int,4),
					new SqlParameter("@MealFeeMorning", SqlDbType.Int,4),
					new SqlParameter("@MealFeeNoon", SqlDbType.Int,4),
					new SqlParameter("@MealFeeNight", SqlDbType.Int,4),
                    new SqlParameter("@CostDetailID",SqlDbType.Int,4),
                    new SqlParameter("@ExpenseTypeNumber",SqlDbType.Int,4),
                    new SqlParameter("@Recipient", SqlDbType.NVarChar,50),
                    new SqlParameter("@BankName", SqlDbType.NVarChar,200),
                    new SqlParameter("@BankAccountNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                     new SqlParameter("@City", SqlDbType.NVarChar,50),
                     new SqlParameter("@TicketSource", SqlDbType.NVarChar,50),
                     new SqlParameter("@TicketDestination", SqlDbType.NVarChar,50),
                                         new SqlParameter("@IsRoundTrip", SqlDbType.Bit),
                      new SqlParameter("@GoTime", SqlDbType.DateTime),
                       new SqlParameter("@BackTime", SqlDbType.DateTime),
                        new SqlParameter("@Boarder", SqlDbType.NVarChar,50),
                         new SqlParameter("@BoarderIDCard", SqlDbType.NVarChar,18),
                         new SqlParameter("@TripType", SqlDbType.Int,4),
                           //BoarderMobile,GoAirNo,BackAirNo,GoAmount,BackAmount
                         new SqlParameter("@BoarderMobile", SqlDbType.NVarChar,50),
                         new SqlParameter("@GoAirNo", SqlDbType.NVarChar,50),
                         new SqlParameter("@BackAirNo", SqlDbType.NVarChar,50),
                         new SqlParameter("@GoAmount", SqlDbType.Decimal,20),
                         new SqlParameter("@BackAmount", SqlDbType.Decimal,20),
                         new SqlParameter("@TicketStatus",SqlDbType.Int,4),
                         new SqlParameter("@ParentId",SqlDbType.Int,4),
                         new SqlParameter("@BoarderId",SqlDbType.Int,4),
                         new SqlParameter("@BoarderIDType",SqlDbType.NVarChar,50),
                         new SqlParameter("@TicketIsUsed",SqlDbType.Bit),
                         new SqlParameter("@PhoneInvoice",SqlDbType.Int,4),
                         new SqlParameter("@PhoneInvoiceNo",SqlDbType.NVarChar,50),
                         new SqlParameter("@TicketIsConfirm",SqlDbType.Bit)
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ReturnID;
            parameters[2].Value = model.ExpenseDate;
            parameters[3].Value = model.ExpenseType;
            parameters[4].Value = model.ExpenseDesc;
            parameters[5].Value = model.ExpenseMoney;
            parameters[6].Value = model.FinanceMemo;
            parameters[7].Value = model.Creater;
            parameters[8].Value = model.CreaterName;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.PhoneYear;
            parameters[11].Value = model.PhoneMonth;
            parameters[12].Value = model.MealFeeMorning;
            parameters[13].Value = model.MealFeeNoon;
            parameters[14].Value = model.MealFeeNight;
            parameters[15].Value = model.CostDetailID;
            parameters[16].Value = model.ExpenseTypeNumber;
            parameters[17].Value = model.Recipient;
            parameters[18].Value = model.BankName;
            parameters[19].Value = model.BankAccountNo;
            parameters[20].Value = model.Status;
            parameters[21].Value = model.City;
            parameters[22].Value = model.TicketSource;
            parameters[23].Value = model.TicketDestination;
            parameters[24].Value = model.IsRoundTrip;
            parameters[25].Value = model.GoTime;
            parameters[26].Value = model.BackTime;
            parameters[27].Value = model.Boarder;
            parameters[28].Value = model.BoarderIDCard;
            parameters[29].Value = model.TripType;
            parameters[30].Value = model.BoarderMobile;
            parameters[31].Value = model.GoAirNo;
            parameters[32].Value = model.BackAirNo;
            parameters[33].Value = model.GoAmount;
            parameters[34].Value = model.BackAmount;
            parameters[35].Value = model.TicketStatus;
            parameters[36].Value = model.ParentId;
            parameters[37].Value = model.BoarderId;
            parameters[38].Value = model.BoarderIDType;
            parameters[39].Value = model.TicketIsUsed;

            parameters[40].Value = model.PhoneInvoice;
            parameters[41].Value = model.PhoneInvoiceNo;
            parameters[42].Value = model.TicketIsConfirm;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int CheckPhoneInvoiceNo(int detailId, string invoiceNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from  F_ExpenseAccountDetail where ID<>@ID and phoneInvoiceNo like '%'+@phoneInvoiceNo+'%' ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@phoneInvoiceNo", SqlDbType.NVarChar,50),
            };
            parameters[0].Value = detailId;
            parameters[1].Value = invoiceNo;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);

            return int.Parse(obj.ToString());
        }

        public int Update(ESP.Finance.Entity.ExpenseAccountDetailInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseAccountDetail set ");
            strSql.Append("ReturnID=@ReturnID,");
            strSql.Append("ExpenseDate=@ExpenseDate,");
            strSql.Append("ExpenseType=@ExpenseType,");
            strSql.Append("ExpenseDesc=@ExpenseDesc,");
            strSql.Append("ExpenseMoney=@ExpenseMoney,");
            strSql.Append("FinanceMemo=@FinanceMemo,");
            strSql.Append("Creater=@Creater,");
            strSql.Append("CreaterName=@CreaterName,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("PhoneYear=@PhoneYear,");
            strSql.Append("PhoneMonth=@PhoneMonth,");
            strSql.Append("MealFeeMorning=@MealFeeMorning,");
            strSql.Append("MealFeeNoon=@MealFeeNoon,");
            strSql.Append("MealFeeNight=@MealFeeNight,");
            strSql.Append("CostDetailID = @CostDetailID,");
            strSql.Append("ExpenseTypeNumber = @ExpenseTypeNumber, ");
            strSql.Append("Recipient = @Recipient, ");
            strSql.Append("BankName = @BankName, ");
            strSql.Append("BankAccountNo = @BankAccountNo, ");
            strSql.Append("Status = @Status,City=@City, ");
            strSql.Append("TicketSource = @TicketSource,TicketDestination=@TicketDestination, ");
            strSql.Append("IsRoundTrip=@IsRoundTrip,GoTime=@GoTime,BackTime=@BackTime,Boarder=@Boarder,BoarderIDCard=@BoarderIDCard,TripType=@TripType,");
            strSql.Append("BoarderMobile=@BoarderMobile,GoAirNo=@GoAirNo,BackAirNo=@BackAirNo,GoAmount=@GoAmount,BackAmount=@BackAmount,");
            strSql.Append("TicketStatus=@TicketStatus,ParentId=@ParentId,BoarderId=@BoarderId,BoarderIDType=@BoarderIDType,TicketIsUsed=@TicketIsUsed,");
            strSql.Append("PhoneInvoice=@PhoneInvoice,PhoneInvoiceNo=@PhoneInvoiceNo,TicketIsConfirm=@TicketIsConfirm");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDate", SqlDbType.SmallDateTime),
					new SqlParameter("@ExpenseType", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDesc", SqlDbType.NVarChar,2000),
					new SqlParameter("@ExpenseMoney", SqlDbType.Decimal,9),
					new SqlParameter("@FinanceMemo", SqlDbType.NVarChar,2000),
					new SqlParameter("@Creater", SqlDbType.Int,4),
					new SqlParameter("@CreaterName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@PhoneYear", SqlDbType.Int,4),
					new SqlParameter("@PhoneMonth", SqlDbType.Int,4),
					new SqlParameter("@MealFeeMorning", SqlDbType.Int,4),
					new SqlParameter("@MealFeeNoon", SqlDbType.Int,4),
					new SqlParameter("@MealFeeNight", SqlDbType.Int,4),
                    new SqlParameter("@CostDetailID",SqlDbType.Int,4),
                    new SqlParameter("@ExpenseTypeNumber",SqlDbType.Int,4),
                    new SqlParameter("@Recipient", SqlDbType.NVarChar,50),
                    new SqlParameter("@BankName", SqlDbType.NVarChar,200),
                    new SqlParameter("@BankAccountNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                     new SqlParameter("@City", SqlDbType.NVarChar,50),
                     new SqlParameter("@TicketSource", SqlDbType.NVarChar,50),
                     new SqlParameter("@TicketDestination", SqlDbType.NVarChar,50),
                                         new SqlParameter("@IsRoundTrip", SqlDbType.Bit),
                      new SqlParameter("@GoTime", SqlDbType.DateTime),
                       new SqlParameter("@BackTime", SqlDbType.DateTime),
                        new SqlParameter("@Boarder", SqlDbType.NVarChar,50),
                         new SqlParameter("@BoarderIDCard", SqlDbType.NVarChar,18),
                         new SqlParameter("@TripType", SqlDbType.Int,4),
                           //BoarderMobile,GoAirNo,BackAirNo,GoAmount,BackAmount
                         new SqlParameter("@BoarderMobile", SqlDbType.NVarChar,50),
                         new SqlParameter("@GoAirNo", SqlDbType.NVarChar,50),
                         new SqlParameter("@BackAirNo", SqlDbType.NVarChar,50),
                         new SqlParameter("@GoAmount", SqlDbType.Decimal,20),
                         new SqlParameter("@BackAmount", SqlDbType.Decimal,20),
                         new SqlParameter("@TicketStatus",SqlDbType.Int,4),
                         new SqlParameter("@ParentId",SqlDbType.Int,4),
                         new SqlParameter("@BoarderId",SqlDbType.Int,4),
                         new SqlParameter("@BoarderIDType",SqlDbType.NVarChar,50),
                         new SqlParameter("@TicketIsUsed",SqlDbType.Bit),
                         new SqlParameter("@PhoneInvoice",SqlDbType.Int,4),
                         new SqlParameter("@PhoneInvoiceNo",SqlDbType.NVarChar,50),
                          new SqlParameter("@TicketIsConfirm",SqlDbType.Bit)
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ReturnID;
            parameters[2].Value = model.ExpenseDate;
            parameters[3].Value = model.ExpenseType;
            parameters[4].Value = model.ExpenseDesc;
            parameters[5].Value = model.ExpenseMoney;
            parameters[6].Value = model.FinanceMemo;
            parameters[7].Value = model.Creater;
            parameters[8].Value = model.CreaterName;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.PhoneYear;
            parameters[11].Value = model.PhoneMonth;
            parameters[12].Value = model.MealFeeMorning;
            parameters[13].Value = model.MealFeeNoon;
            parameters[14].Value = model.MealFeeNight;
            parameters[15].Value = model.CostDetailID;
            parameters[16].Value = model.ExpenseTypeNumber;
            parameters[17].Value = model.Recipient;
            parameters[18].Value = model.BankName;
            parameters[19].Value = model.BankAccountNo;
            parameters[20].Value = model.Status;
            parameters[21].Value = model.City;
            parameters[22].Value = model.TicketSource;
            parameters[23].Value = model.TicketDestination;
            parameters[24].Value = model.IsRoundTrip;
            parameters[25].Value = model.GoTime;
            parameters[26].Value = model.BackTime;
            parameters[27].Value = model.Boarder;
            parameters[28].Value = model.BoarderIDCard;
            parameters[29].Value = model.TripType;
            parameters[30].Value = model.BoarderMobile;
            parameters[31].Value = model.GoAirNo;
            parameters[32].Value = model.BackAirNo;
            parameters[33].Value = model.GoAmount;
            parameters[34].Value = model.BackAmount;
            parameters[35].Value = model.TicketStatus;
            parameters[36].Value = model.ParentId;
            parameters[37].Value = model.BoarderId;
            parameters[38].Value = model.BoarderIDType;
            parameters[39].Value = model.TicketIsUsed;

            parameters[40].Value = model.PhoneInvoice;
            parameters[41].Value = model.PhoneInvoiceNo;
            parameters[42].Value = model.TicketIsConfirm;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAccountDetail ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseAccountDetailInfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_ExpenseAccountDetail ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return CBO.FillObject<ExpenseAccountDetailInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<ESP.Finance.Entity.TicketUserInfo> GetTicketUserList(string strWhere)
        {
            List<ESP.Finance.Entity.TicketUserInfo> userlist = new List<TicketUserInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct boarder,boarderIDCard,boarderMobile ");
            strSql.Append(" FROM F_ExpenseAccountDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ESP.Finance.Entity.TicketUserInfo t = new TicketUserInfo();
                t.Boarder = dt.Rows[i][0].ToString();
                t.BoarderIDCard = dt.Rows[i][1].ToString();
                t.BoarderMobile = dt.Rows[i][2].ToString();
                userlist.Add(t);
            }
            return userlist;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExpenseAccountDetailInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_ExpenseAccountDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by id asc ");
            return CBO.FillCollection<ExpenseAccountDetailInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public DataTable GetDetailListForReport(int batchId)
        {
            //case p.BusinessTypeName when '营销解决方案业务' then 'M' when '营销技术运营业务' then 'T' when '数据技术产品业务' then 'D' else 'A' end

            string strSql = @"select b.ReturnID ,b.ReturnCode,b.ProjectID,b.ProjectCode,b.BranchID,b.BranchCode,b.DepartmentId,b.DepartmentName,
c.CostDetailID, 
c.ExpenseDate,c.ExpenseDesc,c.ExpenseMoney,
'1301'+(select  top 1 customercode from F_Customertmp where ProjectID=b.ProjectID) FinanceCode,
(select top 1 NameCN1 from F_Customertmp where ProjectID=b.ProjectID) customerName,
case when emp.FinanceCode IS NULL then v.DepartmentCode when emp.FinanceCode='' then v.DepartmentCode else emp.FinanceCode end T0,
emp.code T1, '' as T2,
(select  top 1 customercode from F_Customertmp where F_Customertmp.ProjectID=b.ProjectID) T3,
SUBSTRING(b.ProjectCode,7,1) T4, 
SUBSTRING(b.ProjectCode,1,1)+SUBSTRING(b.ProjectCode,9,8) T5, 
d.typecode T6
from f_pnbatchrelation a join f_return b on a.returnid =b.returnid 
left join F_Project p on b.ProjectID =p.ProjectId
join V_Department v on b.DepartmentId= v.level3Id
join sep_Employees emp on b.RequestorID = emp.UserID 
join F_ExpenseAccountDetail c on b.ReturnID=c.ReturnID
left join T_Type d on c.CostDetailID = d.typeid
where BatchID ={0} ";

            strSql = string.Format(strSql, batchId);

            strSql += "order by a.BatchRelationID,c.ID";

            return DbHelperSQL.Query(strSql).Tables[0];
        }

        public List<ExpenseAccountDetailInfo> GetIicketUsed(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* ");
            strSql.Append(" FROM F_ExpenseAccountDetail a join f_return b on a.returnid=b.returnid");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where b.returntype=40 and a.id not in(select parentid from f_expenseaccountdetail where parentid<>0) and b.returnstatus not in(0,1,-1,100,101,102,103,104,105,106,107) and b.returnid not in(select returnid from f_pnbatchrelation) " + strWhere);
            }
            strSql.Append(" order by id asc ");
            return CBO.FillCollection<ExpenseAccountDetailInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public List<ExpenseAccountDetailInfo> GetIicketConfirm(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,a.ReturnID,ExpenseDate,ExpenseType,b.returncode as ExpenseDesc,ExpenseMoney,FinanceMemo,Creater,CreaterName,CreateTime,PhoneYear,PhoneMonth,PhoneInvoice,PhoneInvoiceNo,MealFeeMorning,MealFeeNoon,MealFeeNight,CostDetailID,ExpenseTypeNumber,Recipient,a.BankName,BankAccountNo,Status,City,TicketSource,TicketDestination,IsRoundTrip,GoTime,BackTime,BoarderId,Boarder,BoarderMobile,BoarderIDCard,TripType,GoAirNo,BackAirNo,GoAmount,BackAmount,TicketStatus,a.ParentId,BoarderIDType,TicketIsUsed,TicketIsConfirm ");
            strSql.Append(" FROM F_ExpenseAccountDetail a join f_return b on a.returnid=b.returnid");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where b.returntype=40 and a.id not in(select parentid from f_expenseaccountdetail where parentid<>0 and ReturnID =b.ReturnID ) and b.returnstatus not in(0,1,-1,100,101,102,103,104,105,106,107) and b.returnid not in(select returnid from f_pnbatchrelation) " + strWhere);
            }
            strSql.Append(" order by id asc ");
            return CBO.FillCollection<ExpenseAccountDetailInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public DataTable GetIicketCheck(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ReturnCode, ProjectCode,RequestEmployeeName,Boarder,GoAirNo,ExpenseMoney,ExpenseDesc,ExpenseDate,TicketSource,TicketDestination ");
            strSql.Append(" FROM F_ExpenseAccountDetail a join f_return b on a.returnid=b.returnid where b.returntype=40 and b.returnstatus not in(0,1,-1) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by id asc ");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获得当前报销单下的总金额
        /// </summary>
        /// <param name="ReturnID"></param>
        /// <returns></returns>
        public Decimal GetTotalMoneyByReturnID(int ReturnID)
        {
            string sql = string.Format("SELECT ReturnID,sum(ExpenseMoney) as totalMoney FROM F_ExpenseAccountDetail where ReturnID = {0} and (TicketStatus=0 or TicketStatus is null) group by ReturnID", ReturnID);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            decimal totalMoney = 0;
            if (dt.Rows.Count > 0)
            {
                totalMoney = Convert.ToDecimal(dt.Rows[0]["totalMoney"]);
            }
            return totalMoney;
        }

        /// <summary>
        /// 获得当前报销单下,除了此条明细以外的总金额
        /// </summary>
        /// <param name="ReturnID"></param>
        /// <returns></returns>
        public Decimal GetTotalMoneyByReturnID(int ReturnID, int DetailID)
        {
            string sql = string.Format("SELECT ReturnID,sum(ExpenseMoney) as totalMoney FROM F_ExpenseAccountDetail where Returnid={0} and (TicketStatus=0 or TicketStatus is null) and ID!={1} group by returnID", ReturnID, DetailID);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            decimal totalMoney = 0;
            if (dt.Rows.Count > 0)
            {
                totalMoney = Convert.ToDecimal(dt.Rows[0]["totalMoney"]);
            }
            return totalMoney;
        }

        /// <summary>
        /// 获得个人手机费可报销月份
        /// </summary>
        /// <param name="year"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<int> GetPhoneMonthList(int year, int userid, int detailid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT b.PhoneMonth FROM F_Return as a inner join  F_ExpenseAccountDetail as b on a.ReturnID=b.ReturnID where 1=1 ");
            sql.Append(" and b.PhoneYear = " + year);
            sql.Append(" and b.creater= " + userid);
            DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
            List<int> list = new List<int>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(Convert.ToInt32(dt.Rows[i]["PhoneMonth"].ToString()));
                }
            }
            int currentMonth = 0;
            if (detailid != 0)
            {
                ESP.Finance.Entity.ExpenseAccountDetailInfo detailmodel = this.GetModel(detailid);
                if (detailmodel.PhoneMonth != null)
                {
                    currentMonth = detailmodel.PhoneMonth.Value;
                }

            }
            return CalculateMonth(list, currentMonth);
        }

        /// <summary>
        /// 计算个人手机费可报销月份
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<int> CalculateMonth(List<int> list, int currentMonth)
        {
            List<int> months = new List<int>();
            if (list == null || list.Count == 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    months.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {

                    if (!list.Contains(i))
                    {
                        months.Add(i);
                    }
                    else
                    {
                        if (currentMonth != 0 && currentMonth == i)
                        {
                            months.Add(i);
                        }
                    }
                }
            }
            return months;
        }

        /// <summary>
        /// 判断餐费是否已报销过
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ExistsMellFee(int year, int month, int userid, string type)
        {
            string sql = " SELECT count(1) FROM F_Return as a inner join F_ExpenseAccountDetail as b on a.ReturnID=b.ReturnID where 1=1 ";
            sql += string.Format(" and a.year = {0} and a.month= {1} and b.creater={2}", year, month, userid);
            if (type.Equals("morning"))
            {
                sql += string.Format(" and MealFeeMorning = 1");
            }
            if (type.Equals("noon"))
            {
                sql += string.Format(" and MealFeeNoon = 1");
            }
            if (type.Equals("night"))
            {
                sql += string.Format(" and MealFeeNight = 1");
            }
            return DbHelperSQL.Exists(sql);
        }

        /// <summary>
        /// 获得此报销单中所有的OOP费用
        /// </summary>
        /// <param name="returnid"></param>
        /// <returns></returns>
        public decimal GetTotalOOPByReturnID(int returnid)
        {
            decimal totaloop = 0;
            string sql = " SELECT  ReturnID,sum(expensemoney) as totalcost FROM F_ExpenseAccountDetail where CostDetailID = 0 and returnid= " + returnid + " and (TicketStatus=0 or TicketStatus is null) group by ReturnID ";
            DataTable dt = DbHelperSQL.Query(sql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (null != dt.Rows[0]["totalcost"] && dt.Rows[0]["totalcost"].ToString() != "" && decimal.Parse(dt.Rows[0]["totalcost"].ToString()) > 0)
                {
                    totaloop = decimal.Parse(dt.Rows[0]["totalcost"].ToString());
                }
            }
            return totaloop;
        }

        public List<ExpenseAccountDetailInfo> GetTicketDetail(int returnid)
        {
            string strWhere = string.Format("select * from f_expenseaccountdetail where returnid={0} and ticketstatus<>1 order by id asc", returnid);
            return CBO.FillCollection<ExpenseAccountDetailInfo>(DbHelperSQL.Query(strWhere));
        }

        /// <summary>
        /// 更新明细状态
        /// </summary>
        /// <param name="returnID"></param>
        /// <param name="status">0 不计算成本  1 计算成本</param>
        /// <returns></returns>
        public int UpdateStatusByReturnID(int returnID, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseAccountDetail set ");
            strSql.Append("Status = @Status ");
            strSql.Append(" where ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ReturnID", SqlDbType.Int,4)
            };
            parameters[0].Value = status;
            parameters[1].Value = returnID;


            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int CloseWorkflow(string instanceid, int workitemid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wf_WorkflowInstances set ");
            strSql.Append("Status = 5 ");
            strSql.Append(" where instanceid='" + instanceid + "';update wf_WorkItems set status=1 where workitemid=" + workitemid.ToString() + ";");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }


        public ESP.Finance.Entity.ExpenseAccountDetailInfo GetParentModel(int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ReturnID,ExpenseDate,ExpenseType,ExpenseDesc,ExpenseMoney,FinanceMemo,Creater,CreaterName,CreateTime,PhoneYear,PhoneMonth,MealFeeMorning,MealFeeNoon,MealFeeNight,CostDetailID,ExpenseTypeNumber,Recipient,BankName,BankAccountNo,Status,City,TicketSource,TicketDestination,IsRoundTrip,GoTime,BackTime,Boarder,BoarderIDCard,TripType,BoarderMobile,GoAirNo,BackAirNo,GoAmount,BackAmount,TicketStatus,ParentId,BoarderId,BoarderIDType,TicketIsUsed,TicketIsConfirm from F_ExpenseAccountDetail ");
            strSql.Append(" where ParentId=@ParentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ParentId", SqlDbType.Int,4)};
            parameters[0].Value = parentId;

            return CBO.FillObject<ExpenseAccountDetailInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public int UpdateTicketUsed(string ids)
        {
            string strSql =
            strSql = string.Format("update F_ExpenseAccountDetail set TicketIsUsed =~TicketIsUsed where id in({0}) ", ids);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int UpdateTicketConfirm(string ids)
        {
            string strSql =
            strSql = string.Format("update F_ExpenseAccountDetail set TicketIsConfirm =~TicketIsConfirm where id in({0}) ", ids);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion  成员方法


    }
}

