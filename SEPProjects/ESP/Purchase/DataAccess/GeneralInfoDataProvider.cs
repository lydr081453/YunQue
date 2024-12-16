using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Configuration;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类GeneralInfoDataProvider。
    /// </summary>
    public class GeneralInfoDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralInfoDataProvider"/> class.
        /// </summary>
        public GeneralInfoDataProvider()
        { }

        public static int GetFinanceAccounter(string projectCode)
        {
            string branchcode = projectCode.Substring(0, 1);
            DataTable dt = null;
            string sql = "select FirstFinanceID from F_Branch where BranchCode=@BranchCode";
            SqlParameter[] parameters = {
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = branchcode;
            dt = DbHelperSQL.Query(sql, parameters).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }

        }

        #region  成员方法
        /// <summary>
        /// Ups the load.
        /// </summary>
        /// <param name="generalInfo">The general info.</param>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static int UpLoad(GeneralInfo generalInfo, List<OrderInfo> items)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = Add(generalInfo, conn, trans);
                    foreach (OrderInfo model in items)
                    {
                        model.general_id = id;
                        new OrderInfoDataHelper().Add(model, conn, trans);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "GenerlInfoDataProvider.UpLoad", ESP.Logging.LogLevel.Error, ex);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
            return 1;
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(GeneralInfo model)
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
        public static int Add(GeneralInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_GeneralInfo(");
            strSql.Append("prNo,requestor,requestorname,app_date,requestor_info,requestor_group,enduser,");
            strSql.Append("endusername,enduser_info,enduser_group,goods_receiver,receivername,receivergroup,");
            strSql.Append("receiver_info,ship_address,project_code,project_descripttion,buggeted,supplier_name,");
            strSql.Append("supplier_address,supplier_linkman,supplier_phone,supplier_fax,supplier_email,source,");
            strSql.Append("fa_no,sow,payment_terms,orderid,type,contrast,consult,first_assessor,first_assessorname,");
            strSql.Append("afterwardsname,status,supplier_cellphone,others,sow2,sow3,sow4,thirdParty_materielDesc,");
            strSql.Append("moneytype,requisition_overrule,order_overrule,lasttime,requisition_committime,order_committime,");
            strSql.Append("order_audittime,EmBuy,CusAsk,CusName,ContractNo,Filiale_Auditor,Department,requisitionflow,");
            strSql.Append("addstatus,totalprice,afterwardsReason,EmBuyReason,CusAskYesReason,contrastFile,consultFile,");
            strSql.Append("receiver_Otherinfo,fili_overrule,receivePrepay,prtype,projectid,Filiale_AuditName,CusAskEmailFile,");
            strSql.Append("account_name,account_bank,account_number,contrastRemark,contrastUpFiles,prepayBegindate,prepayEnddate,");
            strSql.Append("project_id,DepartmentId,thirdParty_materielID,prMediaRemark,prMediaAuditTime,isMajordomoUndo,OperationType,purchaseAuditor,purchaseAuditorName,mediaAuditor,mediaAuditorName,adAuditor,adAuditorName,adRemark,adAuditTime,oldflag,isCast,inuse,foregift");
            strSql.Append(",appendReceiver,appendReceiverName,appendReceiverInfo,appendReceiverGroup,PaymentUserID,PeriodType,MediaOldAmount,NewMediaOrderIDs,HaveInvoice,ValueLevel,PRAuthorizationId,IsMediaOrder,IsFactoring,FactoringDate,RCAuditor,RCAuditorName)");
            strSql.Append(" values (");
            strSql.Append("@prNo,@requestor,@requestorname,@app_date,@requestor_info,@requestor_group,@enduser,");
            strSql.Append("@endusername,@enduser_info,@enduser_group,@goods_receiver,@receivername,@receivergroup,");
            strSql.Append("@receiver_info,@ship_address,@project_code,@project_descripttion,@buggeted,@supplier_name,");
            strSql.Append("@supplier_address,@supplier_linkman,@supplier_phone,@supplier_fax,@supplier_email,@source,");
            strSql.Append("@fa_no,@sow,@payment_terms,@orderid,@type,@contrast,@consult,@first_assessor,@first_assessorname,");
            strSql.Append("@afterwardsname,@status,@supplier_cellphone,@others,@sow2,@sow3,@sow4,@thirdParty_materielDesc,");
            strSql.Append("@moneytype,@requisition_overrule,@order_overrule,@lasttime,@requisition_committime,@order_committime,");
            strSql.Append("@order_audittime,@EmBuy,@CusAsk,@CusName,@ContractNo,@Filiale_Auditor,@Department,@requisitionflow,");
            strSql.Append("@addstatus,@totalprice,@afterwardsReason,@EmBuyReason,@CusAskYesReason,@contrastFile,@consultFile,");
            strSql.Append("@receiver_Otherinfo,@fili_overrule,@receivePrepay,@prtype,@projectid,@Filiale_AuditName,@CusAskEmailFile,");
            strSql.Append("@account_name,@account_bank,@account_number,@contrastRemark,@contrastUpFiles,@prepayBegindate,@prepayEnddate,");
            strSql.Append("@project_id,@DepartmentId,@thirdParty_materielID,@prMediaRemark,@prMediaAuditTime,@isMajordomoUndo,@OperationType,@purchaseAuditor,@purchaseAuditorName,@mediaAuditor,@mediaAuditorName,@adAuditor,@adAuditorName,@adRemark,@adAuditTime,@oldflag,@isCast,@inuse,@foregift");
            strSql.Append(",@appendReceiver,@appendReceiverName,@appendReceiverInfo,@appendReceiverGroup,@PaymentUserID,@PeriodType,@MediaOldAmount,@NewMediaOrderIDs,@HaveInvoice,@ValueLevel,@PRAuthorizationId,@IsMediaOrder,@IsFactoring,@FactoringDate,@RCAuditor,@RCAuditorName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@requestor", SqlDbType.Int,4),
					new SqlParameter("@requestorname", SqlDbType.VarChar,100),
					new SqlParameter("@app_date", SqlDbType.DateTime),
					new SqlParameter("@requestor_info", SqlDbType.VarChar,100),
					new SqlParameter("@requestor_group", SqlDbType.VarChar,100),
					new SqlParameter("@enduser", SqlDbType.Int,4),
					new SqlParameter("@endusername", SqlDbType.VarChar,100),
					new SqlParameter("@enduser_info", SqlDbType.VarChar,100),
					new SqlParameter("@enduser_group", SqlDbType.VarChar,100),
					new SqlParameter("@goods_receiver", SqlDbType.Int,4),
					new SqlParameter("@receivername", SqlDbType.VarChar,100),
					new SqlParameter("@receiver_info", SqlDbType.VarChar,100),
					new SqlParameter("@ship_address", SqlDbType.VarChar,500),
					new SqlParameter("@project_code", SqlDbType.VarChar,100),
					new SqlParameter("@project_descripttion", SqlDbType.VarChar,200),
					new SqlParameter("@buggeted", SqlDbType.Decimal),
					new SqlParameter("@supplier_name", SqlDbType.VarChar,100),
					new SqlParameter("@supplier_address", SqlDbType.VarChar,500),
					new SqlParameter("@supplier_linkman", SqlDbType.VarChar,50),
					new SqlParameter("@supplier_phone", SqlDbType.VarChar,50),
					new SqlParameter("@supplier_fax", SqlDbType.VarChar,50),
					new SqlParameter("@supplier_email", SqlDbType.VarChar,50),
					new SqlParameter("@source", SqlDbType.VarChar,200),
					new SqlParameter("@fa_no", SqlDbType.VarChar,100),
					new SqlParameter("@sow", SqlDbType.NVarChar,4000),
					new SqlParameter("@payment_terms", SqlDbType.VarChar,200),
					new SqlParameter("@orderid", SqlDbType.VarChar,100),
					new SqlParameter("@type", SqlDbType.VarChar,100),
					new SqlParameter("@contrast", SqlDbType.VarChar,200),
					new SqlParameter("@consult", SqlDbType.VarChar,200),
					new SqlParameter("@first_assessor", SqlDbType.Int,4),
					new SqlParameter("@first_assessorname", SqlDbType.VarChar,100),
					new SqlParameter("@afterwardsname", SqlDbType.VarChar,200),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@prNo", SqlDbType.VarChar,50),
                    new SqlParameter("@supplier_cellphone",SqlDbType.VarChar,50),
                    new SqlParameter("@others",SqlDbType.VarChar,4000),
                    new SqlParameter("@sow2",SqlDbType.VarChar,200),
                    new SqlParameter("@sow3",SqlDbType.VarChar,200),
                    new SqlParameter("@sow4",SqlDbType.VarChar,200),
                    new SqlParameter("@thirdParty_materielDesc",SqlDbType.NVarChar,400),
                    new SqlParameter("@moneytype",SqlDbType.VarChar,50),
                    new SqlParameter("@requisition_overrule",SqlDbType.NVarChar,2000),
                    new SqlParameter("@order_overrule",SqlDbType.NVarChar,2000),
                    new SqlParameter("@lasttime",SqlDbType.DateTime,8),
                    new SqlParameter("@requisition_committime",SqlDbType.DateTime,8),
                    new SqlParameter("@order_committime",SqlDbType.DateTime,8),
                    new SqlParameter("@order_audittime",SqlDbType.DateTime,8),
                    new SqlParameter("@EmBuy",SqlDbType.NVarChar,10),
                    new SqlParameter("@CusAsk",SqlDbType.NVarChar,10),
                    new SqlParameter("@CusName",SqlDbType.VarChar,100),
                    new SqlParameter("@ContractNo",SqlDbType.VarChar,100),
                    new SqlParameter("@receivergroup",SqlDbType.VarChar,100),
                    new SqlParameter("@Filiale_Auditor",SqlDbType.Int,4),
                    new SqlParameter("@Department",SqlDbType.VarChar,100),
                    new SqlParameter("@requisitionflow",SqlDbType.Int,4),
                    new SqlParameter("@addstatus",SqlDbType.Int,4),
                    new SqlParameter("@totalprice",SqlDbType.Decimal),
                    new SqlParameter("@afterwardsReason",SqlDbType.NVarChar,2000),
                    new SqlParameter("@EmBuyReason",SqlDbType.NVarChar,2000),
                    new SqlParameter("@CusAskYesReason",SqlDbType.NVarChar,2000),
                    new SqlParameter("@contrastFile",SqlDbType.VarChar,100),
                    new SqlParameter("@consultFile",SqlDbType.VarChar,100),
                    new SqlParameter("@receiver_Otherinfo",SqlDbType.VarChar,100),
                    new SqlParameter("@fili_overrule",SqlDbType.NVarChar,2000),
                    new SqlParameter("@receivePrepay",SqlDbType.Int,4),
                    new SqlParameter("@prtype",SqlDbType.Int,4),
                    new SqlParameter("@projectid",SqlDbType.Int,4),
                    new SqlParameter("@Filiale_AuditName",SqlDbType.VarChar,100),
                    new SqlParameter("@CusAskEmailFile",SqlDbType.VarChar,100),
                    new SqlParameter("@account_name",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_bank",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_number",SqlDbType.NVarChar,100),
                    new SqlParameter("@contrastRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@contrastUpFiles",SqlDbType.VarChar,2000),
                    new SqlParameter("@prepayBegindate",SqlDbType.DateTime,8),
                    new SqlParameter("@prepayEnddate",SqlDbType.DateTime,8),
                    new SqlParameter("@project_id",SqlDbType.Int,4),
                    new SqlParameter("@DepartmentId",SqlDbType.Int,4),
                    new SqlParameter("@thirdParty_materielID",SqlDbType.NVarChar,400),
                    new SqlParameter("@prMediaRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@prMediaAuditTime",SqlDbType.DateTime,8),
                    new SqlParameter("@isMajordomoUndo",SqlDbType.Bit),
                    new SqlParameter("@OperationType",SqlDbType.Bit),
                    new SqlParameter("@purchaseAuditor",SqlDbType.Int,4),
                    new SqlParameter("@purchaseAuditorName",SqlDbType.VarChar,100),
                    new SqlParameter("@mediaAuditor",SqlDbType.Int,4),
                    new SqlParameter("@mediaAuditorName",SqlDbType.VarChar,100),
                    new SqlParameter("@adAuditor",SqlDbType.Int,4),
                    new SqlParameter("@adAuditorName",SqlDbType.VarChar,100),
                    new SqlParameter("@adRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@adAuditTime",SqlDbType.DateTime,8),
                    new SqlParameter("@oldflag",SqlDbType.Bit),
                    new SqlParameter("@isCast",SqlDbType.Int,4),
                    new SqlParameter("@inuse",SqlDbType.Int,4),
                    new SqlParameter("@foregift",SqlDbType.Decimal),
                    new SqlParameter("@appendReceiver",SqlDbType.Int,4),
                    new SqlParameter("@appendReceiverName",SqlDbType.VarChar,100),
                    new SqlParameter("@appendReceiverInfo",SqlDbType.VarChar,100),
                    new SqlParameter("@appendReceiverGroup",SqlDbType.VarChar,100),
                    new SqlParameter("@PaymentUserID",SqlDbType.Int,4),
                    new SqlParameter("@PeriodType",SqlDbType.Int,4),
                    new SqlParameter("@MediaOldAmount",SqlDbType.Decimal),
                    new SqlParameter("@NewMediaOrderIDs",SqlDbType.NVarChar,4000),
                    new SqlParameter("@HaveInvoice",SqlDbType.Bit),
                    new SqlParameter("@ValueLevel",SqlDbType.Int,4),
                    new SqlParameter("@PRAuthorizationId",SqlDbType.Int,4),
                    new SqlParameter("@IsMediaOrder",SqlDbType.Int,4),
                    new SqlParameter("@IsFactoring",SqlDbType.Int,4),
                    new SqlParameter("@FactoringDate",SqlDbType.DateTime,8),
                    new SqlParameter("@RCAuditor",SqlDbType.Int,4),
                     new SqlParameter("@RCAuditorName",SqlDbType.VarChar,50)
                    
                                        };
            parameters[0].Value = model.requestor;
            parameters[1].Value = model.requestorname;
            parameters[2].Value = model.app_date == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.app_date;
            parameters[3].Value = model.requestor_info;
            parameters[4].Value = model.requestor_group;
            parameters[5].Value = model.enduser;
            parameters[6].Value = model.endusername;
            parameters[7].Value = model.enduser_info;
            parameters[8].Value = model.enduser_group;
            parameters[9].Value = model.goods_receiver;
            parameters[10].Value = model.receivername;
            parameters[11].Value = model.receiver_info;
            parameters[12].Value = model.ship_address;
            parameters[13].Value = model.project_code;
            parameters[14].Value = model.project_descripttion;
            parameters[15].Value = model.buggeted;
            parameters[16].Value = model.supplier_name;
            parameters[17].Value = model.supplier_address;
            parameters[18].Value = model.supplier_linkman;
            parameters[19].Value = model.supplier_phone;
            parameters[20].Value = model.supplier_fax;
            parameters[21].Value = model.supplier_email;
            parameters[22].Value = model.source;
            parameters[23].Value = model.fa_no;
            parameters[24].Value = model.sow;
            parameters[25].Value = model.payment_terms;
            parameters[26].Value = model.orderid;
            parameters[27].Value = model.type;
            //parameters[28].Value = model.contrast;
            //parameters[29].Value = model.consult;
            parameters[28].Value = "0";
            parameters[29].Value = "0";
            parameters[30].Value = model.first_assessor;
            parameters[31].Value = model.first_assessorname;
            parameters[32].Value = model.afterwardsname;
            parameters[33].Value = model.status;
            parameters[34].Value = model.PrNo;
            parameters[35].Value = model.Supplier_cellphone;
            parameters[36].Value = model.others;
            parameters[37].Value = model.sow2;
            parameters[38].Value = model.sow3;
            parameters[39].Value = "0";
            parameters[40].Value = model.thirdParty_materielDesc;
            parameters[41].Value = model.moneytype;
            parameters[42].Value = model.requisition_overrule;
            parameters[43].Value = model.order_overrule;
            parameters[44].Value = DateTime.Now;//最后编辑时间
            parameters[45].Value = Common.State.datetime_minvalue;
            parameters[46].Value = Common.State.datetime_minvalue;
            parameters[47].Value = Common.State.datetime_minvalue;
            parameters[48].Value = model.EmBuy;
            parameters[49].Value = model.CusAsk;
            parameters[50].Value = model.CusName;
            parameters[51].Value = model.ContractNo;
            parameters[52].Value = model.ReceiverGroup;
            parameters[53].Value = model.Filiale_Auditor;
            parameters[54].Value = model.Department;
            parameters[55].Value = model.Requisitionflow;
            parameters[56].Value = model.Addstatus;
            parameters[57].Value = model.totalprice;
            parameters[58].Value = model.afterwardsReason;
            parameters[59].Value = model.EmBuyReason;
            parameters[60].Value = model.CusAskYesReason;
            parameters[61].Value = model.contrastFile;
            parameters[62].Value = model.consultFile;
            parameters[63].Value = model.receiver_Otherinfo;
            parameters[64].Value = model.fili_overrule;
            parameters[65].Value = model.receivePrepay;
            parameters[66].Value = model.PRType;
            parameters[67].Value = model.ProjectID;
            parameters[68].Value = model.Filiale_AuditName;
            parameters[69].Value = model.CusAskEmailFile;
            parameters[70].Value = model.account_name;
            parameters[71].Value = model.account_bank;
            parameters[72].Value = model.account_number;
            parameters[73].Value = model.contrastRemark;
            parameters[74].Value = model.contrastUpFiles;
            parameters[75].Value = model.prepayBegindate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.prepayBegindate;
            parameters[76].Value = model.prepayEnddate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.prepayEnddate;
            parameters[77].Value = model.Project_id;
            parameters[78].Value = model.Departmentid;
            parameters[79].Value = model.thirdParty_materielID;
            parameters[80].Value = model.prMediaRemark;
            parameters[81].Value = model.prMediaAuditTime;
            parameters[82].Value = model.isMajordomoUndo;
            parameters[83].Value = model.OperationType;
            parameters[84].Value = model.purchaseAuditor;
            parameters[85].Value = model.purchaseAuditorName;
            parameters[86].Value = model.mediaAuditor;
            parameters[87].Value = model.mediaAuditorName;
            parameters[88].Value = model.adAuditor;
            parameters[89].Value = model.adAuditorName;
            parameters[90].Value = model.adRemark;
            parameters[91].Value = model.adAuditTime == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.adAuditTime;
            parameters[92].Value = model.oldFlag;
            parameters[93].Value = model.isCast;
            parameters[94].Value = ((int)State.PRInUse.Use + 0);
            parameters[95].Value = model.Foregift;
            parameters[96].Value = model.appendReceiver;
            parameters[97].Value = model.appendReceiverName;
            parameters[98].Value = model.appendReceiverInfo;
            parameters[99].Value = model.appendReceiverGroup;
            parameters[100].Value = model.PaymentUserID;
            parameters[101].Value = model.PeriodType;
            parameters[102].Value = model.MediaOldAmount;
            parameters[103].Value = model.NewMediaOrderIDs;
            parameters[104].Value = model.HaveInvoice;
            parameters[105].Value = model.ValueLevel;
            parameters[106].Value = model.PRAuthorizationId;
            parameters[107].Value = model.IsMediaOrder;
            parameters[108].Value = model.IsFactoring;
            parameters[109].Value = Common.State.datetime_minvalue; 
            object obj = null;
            if (trans != null)
            {
                obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
                model.id = Convert.ToInt32(obj);
                addPermission(model, trans);
            }
            else
            {
                using (SqlConnection newConn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    newConn.Open();
                    SqlTransaction newTrans = newConn.BeginTransaction();
                    try
                    {
                        obj = DbHelperSQL.GetSingle(strSql.ToString(), newConn, newTrans, parameters);
                        model.id = Convert.ToInt32(obj);
                        addPermission(model, newTrans);
                        newTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        newTrans.Rollback();
                        ESP.Logging.Logger.Add(ex.Message, "GeneralInfoDataProvider.Add", ESP.Logging.LogLevel.Error, ex);
                        return 0;
                    }
                }
            }
            return model.id;
        }

        public static void UpdateProjectCode(int projectId, string projectCode, SqlTransaction trans)
        {
            IList<ESP.Purchase.Entity.GeneralInfo> generalList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModelList(" project_id="+projectId.ToString(),new List<SqlParameter>());

            if (generalList != null && generalList.Count != 0)
            {
                foreach (ESP.Purchase.Entity.GeneralInfo gen in generalList)
                {
                    if (gen.project_code != projectCode)
                    {
                        ESP.Purchase.Entity.LogInfo log = new LogInfo();
                        log.Gid = gen.id;
                        log.LogUserId = 0;
                        log.PrNo = gen.PrNo;
                        log.Status = 1;
                        log.LogMedifiedTeme = DateTime.Now;
                        log.Des = "<font coler='red'>原项目号:" + gen.project_code+"</font>";

                        ESP.Purchase.BusinessLogic.LogManager.AddLog(log, trans);
                    }
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_GeneralInfo set project_code=@projectCode where project_id=@projectId");
            SqlParameter[] parameters = {
					new SqlParameter("@projectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@projectId", SqlDbType.Int,4)
                                           };
            parameters[0].Value = projectCode;
            parameters[1].Value = projectId;
            if (trans != null)
            {
                DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
            }
            else
            {
                using (SqlConnection newConn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    newConn.Open();
                    SqlTransaction newTrans = newConn.BeginTransaction();
                    try
                    {
                        DbHelperSQL.ExecuteSql(strSql.ToString(), newConn, newTrans, parameters);
                        newTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        newTrans.Rollback();
                    }
                }
            }

        }

        /// <summary>
        /// 添加数据人员权限
        /// </summary>
        /// <param name="generalModel"></param>
        /// <param name="trans"></param>
        private static void addPermission(GeneralInfo generalModel, SqlTransaction trans)
        {
            Entity.DataInfo dataModel = new DataInfo();
            dataModel.DataType = (int)State.DataType.PR;
            dataModel.DataId = generalModel.id;
            #region 维护人员权限
            List<Entity.DataPermissionInfo> permissionList = new List<DataPermissionInfo>();
            //申请人
            Entity.DataPermissionInfo perission1 = new DataPermissionInfo();
            perission1.UserId = generalModel.requestor;
            perission1.IsEditor = true;
            perission1.IsViewer = true;
            permissionList.Add(perission1);

            //使用人
            Entity.DataPermissionInfo perission2 = new DataPermissionInfo();
            perission2.UserId = generalModel.enduser;
            perission2.IsEditor = false;
            perission2.IsViewer = true;
            permissionList.Add(perission2);

            //收货人
            Entity.DataPermissionInfo perission3 = new DataPermissionInfo();
            perission3.UserId = generalModel.goods_receiver;
            perission3.IsEditor = false;
            perission3.IsViewer = true;
            permissionList.Add(perission3);

            Entity.DataPermissionInfo perissionappend = new DataPermissionInfo();
            perissionappend.UserId = generalModel.appendReceiver;
            perissionappend.IsEditor = false;
            perissionappend.IsViewer = true;
            permissionList.Add(perissionappend);

            //初审人
            Entity.DataPermissionInfo perission4 = new DataPermissionInfo();
            perission4.UserId = generalModel.first_assessor;
            perission4.IsEditor = true;
            perission4.IsViewer = true;
            permissionList.Add(perission4);

            //分公司审核人
            Entity.DataPermissionInfo perission5 = new DataPermissionInfo();
            perission5.UserId = generalModel.Filiale_Auditor;
            perission5.IsEditor = true;
            perission5.IsViewer = true;
            permissionList.Add(perission5);

            //风控
            Entity.DataPermissionInfo perission6 = new DataPermissionInfo();
            if (generalModel.RCAuditor != null)
            {
                perission6.UserId = generalModel.RCAuditor.Value;
                perission6.IsEditor = true;
                perission6.IsViewer = true;
                permissionList.Add(perission6);
            }
            //采购总监审批人
            Entity.DataPermissionInfo perission7 = new DataPermissionInfo();
            perission7.UserId = generalModel.purchaseAuditor;
            perission7.IsEditor = true;
            perission7.IsViewer = true;
            permissionList.Add(perission7);

            //业务审核人
            DataSet operationUserList = new OperationAuditDataProvider().GetList(string.Format(" general_id={0}", generalModel.id));
            if (operationUserList != null && operationUserList.Tables.Count > 0)
            {
                foreach (DataRow dr in operationUserList.Tables[0].Rows)
                {
                    Entity.DataPermissionInfo auditorPerission = new DataPermissionInfo();
                    auditorPerission.UserId = int.Parse(dr["auditorId"].ToString());
                    auditorPerission.IsEditor = false;
                    auditorPerission.IsViewer = true;
                    permissionList.Add(auditorPerission);
                }
            }


            #endregion
            new DataAccess.DataPermissionProvider().AddDataPermission(dataModel, permissionList, trans);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public static void Update(GeneralInfo model)
        {
            Update(model, null, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="conn">The conn.</param>
        /// <param name="trans">The trans.</param>
        public static void Update(GeneralInfo model, SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_GeneralInfo set ");
            strSql.Append("requestor=@requestor,");
            strSql.Append("requestorname=@requestorname,");
            strSql.Append("app_date=@app_date,");
            strSql.Append("requestor_info=@requestor_info,");
            strSql.Append("requestor_group=@requestor_group,");
            strSql.Append("enduser=@enduser,");
            strSql.Append("endusername=@endusername,");
            strSql.Append("enduser_info=@enduser_info,");
            strSql.Append("enduser_group=@enduser_group,");
            strSql.Append("goods_receiver=@goods_receiver,");
            strSql.Append("receivername=@receivername,");
            strSql.Append("receiver_info=@receiver_info,");
            strSql.Append("ship_address=@ship_address,");
            strSql.Append("project_code=@project_code,");
            strSql.Append("project_descripttion=@project_descripttion,");
            strSql.Append("buggeted=@buggeted,");
            strSql.Append("supplier_name=@supplier_name,");
            strSql.Append("supplier_address=@supplier_address,");
            strSql.Append("supplier_linkman=@supplier_linkman,");
            strSql.Append("supplier_phone=@supplier_phone,");
            strSql.Append("supplier_fax=@supplier_fax,");
            strSql.Append("supplier_email=@supplier_email,");
            strSql.Append("source=@source,");
            strSql.Append("fa_no=@fa_no,");
            strSql.Append("sow=@sow,");
            strSql.Append("payment_terms=@payment_terms,");
            strSql.Append("orderid=@orderid,");
            strSql.Append("type=@type,");
            strSql.Append("contrast=@contrast,");
            strSql.Append("consult=@consult,");
            strSql.Append("first_assessor=@first_assessor,");
            strSql.Append("first_assessorname=@first_assessorname,");
            strSql.Append("afterwardsname=@afterwardsname,");
            strSql.Append("status=@status,");
            strSql.Append("prNO=@prNO,");
            strSql.Append("supplier_cellphone=@supplier_cellphone,");
            strSql.Append("others=@others,");
            strSql.Append("sow2=@sow2,");
            strSql.Append("sow3=@sow3,");
            strSql.Append("sow4=@sow4,");
            strSql.Append("thirdParty_materielDesc=@thirdParty_materielDesc,");
            strSql.Append("moneytype=@moneytype,");
            strSql.Append("requisition_overrule=@requisition_overrule,");
            strSql.Append("order_overrule=@order_overrule,");
            strSql.Append("lasttime=@lasttime,");
            strSql.Append("requisition_committime=@requisition_committime,");
            strSql.Append("order_committime=@order_committime,");
            strSql.Append("order_audittime=@order_audittime,");
            strSql.Append("EmBuy=@EmBuy,");
            strSql.Append("CusAsk=@CusAsk,");
            strSql.Append("CusName=@CusName,");
            strSql.Append("ContractNo=@ContractNo,");
            strSql.Append("receivergroup=@receivergroup,");
            strSql.Append("Filiale_Auditor=@Filiale_Auditor,");
            strSql.Append("Department=@Department,");
            strSql.Append("requisitionflow=@requisitionflow,");
            strSql.Append("addstatus=@addstatus,");
            strSql.Append("totalprice=@totalprice,");
            strSql.Append("afterwardsReason=@afterwardsReason,");
            strSql.Append("EmBuyReason=@EmBuyReason,");
            strSql.Append("CusAskYesReason=@CusAskYesReason,");
            strSql.Append("contrastFile=@contrastFile,");
            strSql.Append("consultFile=@consultFile,");
            strSql.Append("receiver_Otherinfo=@receiver_Otherinfo,");
            strSql.Append("fili_overrule=@fili_overrule,");
            strSql.Append("receivePrepay=@receivePrepay,");
            strSql.Append("prtype=@prtype,");
            strSql.Append("projectid=@projectid,");
            strSql.Append("Filiale_AuditName=@Filiale_AuditName,");
            strSql.Append("CusAskEmailFile=@CusAskEmailFile,");
            strSql.Append("account_name=@account_name,account_bank=@account_bank,account_number=@account_number,");
            strSql.Append("contrastRemark=@contrastRemark,contrastUpFiles=@contrastUpFiles,");
            strSql.Append("prepayBegindate=@prepayBegindate,prepayEnddate=@prepayEnddate");
            strSql.Append(",instanceId = @instanceId, processId=@processId,project_id=@project_id,DepartmentId=@DepartmentId,thirdParty_materielID=@thirdParty_materielID,prMediaRemark=@prMediaRemark,prMediaAuditTime=@prMediaAuditTime");
            strSql.Append(",isMajordomoUndo=@isMajordomoUndo,OperationType=@OperationType ");
            strSql.Append(",purchaseAuditor = @purchaseAuditor,");
            strSql.Append("purchaseAuditorName = @purchaseAuditorName,");
            strSql.Append("mediaAuditor = @mediaAuditor,");
            strSql.Append("mediaAuditorName = @mediaAuditorName,");
            strSql.Append("adAuditor = @adAuditor,");
            strSql.Append("adAuditorName = @adAuditorName,");
            strSql.Append("adRemark = @adRemark,");
            strSql.Append("adAuditTime = @adAuditTime,oldflag=@oldflag,isCast=@isCast,inuse=@inuse,foregift=@foregift");
            strSql.Append(",appendReceiver=@appendReceiver,appendReceiverName=@appendReceiverName,appendReceiverInfo=@appendReceiverInfo,appendReceiverGroup=@appendReceiverGroup,PaymentUserID=@PaymentUserID,PeriodType=@PeriodType");
            strSql.Append(",MediaOldAmount=@MediaOldAmount, NewMediaOrderIDs=@NewMediaOrderIDs,HaveInvoice=@HaveInvoice,ValueLevel=@ValueLevel,PRAuthorizationId=@PRAuthorizationId,IsMediaOrder=@IsMediaOrder,IsFactoring=@IsFactoring,FactoringDate=@FactoringDate,RCAuditor=@RCAuditor,RCAuditorName=@RCAuditorName");
            strSql.Append(",InvoiceType=@InvoiceType,TaxRate=@TaxRate,FCPrIds=@FCPrIds");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@requestor", SqlDbType.Int,4),
					new SqlParameter("@requestorname", SqlDbType.VarChar,100),
					new SqlParameter("@app_date", SqlDbType.DateTime),
					new SqlParameter("@requestor_info", SqlDbType.VarChar,100),
					new SqlParameter("@requestor_group", SqlDbType.VarChar,100),
					new SqlParameter("@enduser", SqlDbType.Int,4),
					new SqlParameter("@endusername", SqlDbType.VarChar,100),
					new SqlParameter("@enduser_info", SqlDbType.VarChar,100),
					new SqlParameter("@enduser_group", SqlDbType.VarChar,100),
					new SqlParameter("@goods_receiver", SqlDbType.Int,4),
					new SqlParameter("@receivername", SqlDbType.VarChar,100),
					new SqlParameter("@receiver_info", SqlDbType.VarChar,100),
					new SqlParameter("@ship_address", SqlDbType.VarChar,500),
					new SqlParameter("@project_code", SqlDbType.VarChar,100),
					new SqlParameter("@project_descripttion", SqlDbType.VarChar,200),
					new SqlParameter("@buggeted", SqlDbType.Decimal),
					new SqlParameter("@supplier_name", SqlDbType.VarChar,100),
					new SqlParameter("@supplier_address", SqlDbType.VarChar,500),
					new SqlParameter("@supplier_linkman", SqlDbType.VarChar,50),
					new SqlParameter("@supplier_phone", SqlDbType.VarChar,50),
					new SqlParameter("@supplier_fax", SqlDbType.VarChar,50),
					new SqlParameter("@supplier_email", SqlDbType.VarChar,50),
					new SqlParameter("@source", SqlDbType.VarChar,200),
					new SqlParameter("@fa_no", SqlDbType.VarChar,100),
					new SqlParameter("@sow", SqlDbType.NVarChar,4000),
					new SqlParameter("@payment_terms", SqlDbType.VarChar,200),
					new SqlParameter("@orderid", SqlDbType.VarChar,100),
					new SqlParameter("@type", SqlDbType.VarChar,100),
					new SqlParameter("@contrast", SqlDbType.VarChar,200),
					new SqlParameter("@consult", SqlDbType.VarChar,200),
					new SqlParameter("@first_assessor", SqlDbType.Int,4),
					new SqlParameter("@first_assessorname", SqlDbType.VarChar,100),
					new SqlParameter("@afterwardsname", SqlDbType.VarChar,200),
					new SqlParameter("@status", SqlDbType.Int,4),
                    new SqlParameter("@prNo",SqlDbType.VarChar,50),
                    new SqlParameter("@supplier_cellphone",SqlDbType.VarChar,50),
                    new SqlParameter("@others",SqlDbType.VarChar,4000),
                    new SqlParameter("@sow2",SqlDbType.VarChar,200),
                    new SqlParameter("@sow3",SqlDbType.VarChar,200),
                    new SqlParameter("@sow4",SqlDbType.VarChar,200),
                    new SqlParameter("@thirdParty_materielDesc",SqlDbType.NVarChar,400),
                    new SqlParameter("@moneytype",SqlDbType.VarChar,50),
                    new SqlParameter("@requisition_overrule",SqlDbType.NVarChar,2000),
                    new SqlParameter("@order_overrule",SqlDbType.NVarChar,2000),
                    new SqlParameter("@lasttime",SqlDbType.DateTime,8),
                    new SqlParameter("@requisition_committime",SqlDbType.DateTime,8),
                    new SqlParameter("@order_committime",SqlDbType.DateTime,8),
                    new SqlParameter("@order_audittime",SqlDbType.DateTime,8),
                    new SqlParameter("@EmBuy",SqlDbType.NVarChar,10),
                    new SqlParameter("@CusAsk",SqlDbType.NVarChar,10),
                    new SqlParameter("@CusName",SqlDbType.VarChar,100),
                    new SqlParameter("@ContractNo",SqlDbType.VarChar,100),
                    new SqlParameter("@receivergroup",SqlDbType.VarChar,100),
                    new SqlParameter("@Filiale_Auditor",SqlDbType.Int,4),
                    new SqlParameter("@Department",SqlDbType.VarChar,100),
                    new SqlParameter("@requisitionflow",SqlDbType.Int,4),
                    new SqlParameter("@addstatus",SqlDbType.Int,4),
                    new SqlParameter("@totalprice",SqlDbType.Decimal),
                    new SqlParameter("@afterwardsReason",SqlDbType.NVarChar,2000),
                    new SqlParameter("@EmBuyReason",SqlDbType.NVarChar,2000),
                    new SqlParameter("@CusAskYesReason",SqlDbType.NVarChar,2000),
                    new SqlParameter("@contrastFile",SqlDbType.VarChar,100),
                    new SqlParameter("@consultFile",SqlDbType.VarChar,100),
                    new SqlParameter("@receiver_Otherinfo",SqlDbType.VarChar,100),
                    new SqlParameter("@fili_overrule",SqlDbType.NVarChar,2000),
                    new SqlParameter("@receivePrepay",SqlDbType.Int,4),
                    new SqlParameter("@prtype",SqlDbType.Int,4),
                    new SqlParameter("@projectid",SqlDbType.Int,4),
                    new SqlParameter("@Filiale_AuditName",SqlDbType.VarChar,100),
                    new SqlParameter("@CusAskEmailFile",SqlDbType.VarChar,100),
                    new SqlParameter("@account_name",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_bank",SqlDbType.NVarChar,100),
                    new SqlParameter("@account_number",SqlDbType.NVarChar,100),
                    new SqlParameter("@contrastRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@contrastUpFiles",SqlDbType.VarChar,2000),                    
                    new SqlParameter("@prepayBegindate",SqlDbType.DateTime,8),
                    new SqlParameter("@prepayEnddate",SqlDbType.DateTime,8),
                    new SqlParameter("@instanceid",SqlDbType.Int,4),
                    new SqlParameter("@processId", SqlDbType.Int,4),
                    new SqlParameter("@project_id",SqlDbType.Int,4),
                    new SqlParameter("@DepartmentId",SqlDbType.Int,4),
                    new SqlParameter("@thirdParty_materielID",SqlDbType.NVarChar,400),
                    new SqlParameter("@prMediaRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@prMediaAuditTime",SqlDbType.DateTime,8),
                    new SqlParameter("@isMajordomoUndo",SqlDbType.Bit),
                    new SqlParameter("@OperationType",SqlDbType.Bit),
                    new SqlParameter("@purchaseAuditor",SqlDbType.Int,4),
                    new SqlParameter("@purchaseAuditorName",SqlDbType.VarChar,100),
                    new SqlParameter("@mediaAuditor",SqlDbType.Int,4),
                    new SqlParameter("@mediaAuditorName",SqlDbType.VarChar,100),
                    new SqlParameter("@adAuditor",SqlDbType.Int,4),
                    new SqlParameter("@adAuditorName",SqlDbType.VarChar,100),
                    new SqlParameter("@adRemark",SqlDbType.NVarChar,1000),
                    new SqlParameter("@adAuditTime",SqlDbType.DateTime,8),
                    new SqlParameter("@oldflag",SqlDbType.Bit),
                    new SqlParameter("@isCast",SqlDbType.Int,4),
                    new SqlParameter("@inuse",SqlDbType.Int,4),
                    new SqlParameter("@foregift",SqlDbType.Decimal),
                    new SqlParameter("@appendReceiver",SqlDbType.Int,4),
                    new SqlParameter("@appendReceiverName",SqlDbType.VarChar,100),
                    new SqlParameter("@appendReceiverInfo",SqlDbType.VarChar,100),
                    new SqlParameter("@appendReceiverGroup",SqlDbType.VarChar,100),
                    new SqlParameter("@PaymentUserID",SqlDbType.Int,4),
                    new SqlParameter("@PeriodType",SqlDbType.Int,4),
                    new SqlParameter("@MediaOldAmount",SqlDbType.Decimal),
                    new SqlParameter("@NewMediaOrderIDs",SqlDbType.NVarChar,4000),
                    new SqlParameter("@HaveInvoice",SqlDbType.Bit),
                    new SqlParameter("@ValueLevel",SqlDbType.Int,4),
                    new SqlParameter("@PRAuthorizationId",SqlDbType.Int,4),
                    new SqlParameter("@IsMediaOrder",SqlDbType.Int,4),
                    new SqlParameter("@IsFactoring",SqlDbType.Int,4),
                    new SqlParameter("@FactoringDate",SqlDbType.DateTime,8),
                    new SqlParameter("@RCAuditor",SqlDbType.Int,4),
                    new SqlParameter("@RCAuditorName",SqlDbType.VarChar,50),
                    new SqlParameter("@InvoiceType",SqlDbType.VarChar,50),
                    new SqlParameter("@TaxRate",SqlDbType.Int),
                    new SqlParameter("@FCPrIds",SqlDbType.NVarChar),
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.requestor;
            parameters[2].Value = model.requestorname;
            parameters[3].Value = model.app_date;
            parameters[4].Value = model.requestor_info;
            parameters[5].Value = model.requestor_group;
            parameters[6].Value = model.enduser;
            parameters[7].Value = model.endusername;
            parameters[8].Value = model.enduser_info;
            parameters[9].Value = model.enduser_group;
            parameters[10].Value = model.goods_receiver;
            parameters[11].Value = model.receivername;
            parameters[12].Value = model.receiver_info;
            parameters[13].Value = model.ship_address;
            parameters[14].Value = model.project_code;
            parameters[15].Value = model.project_descripttion;
            parameters[16].Value = model.buggeted;
            parameters[17].Value = model.supplier_name;
            parameters[18].Value = model.supplier_address;
            parameters[19].Value = model.supplier_linkman;
            parameters[20].Value = model.supplier_phone;
            parameters[21].Value = model.supplier_fax;
            parameters[22].Value = model.supplier_email;
            parameters[23].Value = model.source;
            parameters[24].Value = model.fa_no;
            parameters[25].Value = model.sow;
            parameters[26].Value = model.payment_terms;
            parameters[27].Value = model.orderid;
            parameters[28].Value = model.type;
            parameters[29].Value = string.IsNullOrEmpty(model.contrast) ? "0.00" : model.contrast;
            parameters[30].Value = string.IsNullOrEmpty(model.consult) ? "0.00" : model.consult;
            parameters[31].Value = model.first_assessor;
            parameters[32].Value = model.first_assessorname;
            parameters[33].Value = model.afterwardsname;
            parameters[34].Value = model.status;
            parameters[35].Value = model.PrNo;
            parameters[36].Value = model.Supplier_cellphone;
            parameters[37].Value = model.others;
            parameters[38].Value = model.sow2;
            parameters[39].Value = model.sow3;
            parameters[40].Value = string.IsNullOrEmpty(model.sow4) ? "0.00" : model.sow4;
            parameters[41].Value = model.thirdParty_materielDesc;
            parameters[42].Value = model.moneytype;
            parameters[43].Value = model.requisition_overrule;
            parameters[44].Value = model.order_overrule;
            parameters[45].Value = DateTime.Now;
            parameters[46].Value = (model.status == Common.State.requisition_return || model.status == Common.State.requisition_save) ? DateTime.Parse(Common.State.datetime_minvalue) : model.requisition_committime;
            parameters[47].Value = model.status < Common.State.order_commit ? DateTime.Parse(Common.State.datetime_minvalue) : model.order_committime;
            parameters[48].Value = model.order_audittime == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.order_audittime;
            parameters[49].Value = model.EmBuy;
            parameters[50].Value = model.CusAsk;
            parameters[51].Value = model.CusName;
            parameters[52].Value = model.ContractNo;
            parameters[53].Value = model.ReceiverGroup;
            parameters[54].Value = model.Filiale_Auditor;
            parameters[55].Value = model.Department;
            parameters[56].Value = model.Requisitionflow;
            parameters[57].Value = model.Addstatus;
            parameters[58].Value = model.totalprice;
            parameters[59].Value = model.afterwardsReason;
            parameters[60].Value = model.EmBuyReason;
            parameters[61].Value = model.CusAskYesReason;
            parameters[62].Value = model.contrastFile;
            parameters[63].Value = model.consultFile;
            parameters[64].Value = model.receiver_Otherinfo;
            parameters[65].Value = model.fili_overrule;
            parameters[66].Value = model.receivePrepay;
            parameters[67].Value = model.PRType;
            parameters[68].Value = model.ProjectID;
            parameters[69].Value = model.Filiale_AuditName;
            parameters[70].Value = model.CusAskEmailFile;
            parameters[71].Value = model.account_name;
            parameters[72].Value = model.account_bank;
            parameters[73].Value = model.account_number;
            parameters[74].Value = model.contrastRemark;
            parameters[75].Value = model.contrastUpFiles;
            parameters[76].Value = model.prepayBegindate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.prepayBegindate;
            parameters[77].Value = model.prepayEnddate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.prepayEnddate;
            parameters[78].Value = model.InstanceID;
            parameters[79].Value = model.ProcessID;
            parameters[80].Value = model.Project_id;
            parameters[81].Value = model.Departmentid;
            parameters[82].Value = model.thirdParty_materielID;
            parameters[83].Value = model.prMediaRemark;
            parameters[84].Value = model.prMediaAuditTime;
            parameters[85].Value = model.isMajordomoUndo;
            parameters[86].Value = model.OperationType;
            parameters[87].Value = model.purchaseAuditor;
            parameters[88].Value = model.purchaseAuditorName;
            parameters[89].Value = model.mediaAuditor;
            parameters[90].Value = model.mediaAuditorName;
            parameters[91].Value = model.adAuditor;
            parameters[92].Value = model.adAuditorName;
            parameters[93].Value = model.adRemark;
            parameters[94].Value = (model.adAuditTime == DateTime.MinValue || model.adAuditTime == null) ? DateTime.Parse(Common.State.datetime_minvalue) : model.adAuditTime;
            parameters[95].Value = model.oldFlag;
            parameters[96].Value = model.isCast;
            parameters[97].Value = model.InUse;
            parameters[98].Value = model.Foregift;
            parameters[99].Value = model.appendReceiver;
            parameters[100].Value = model.appendReceiverName;
            parameters[101].Value = model.appendReceiverInfo;
            parameters[102].Value = model.appendReceiverGroup;
            parameters[103].Value = model.PaymentUserID;
            parameters[104].Value = model.PeriodType;
            parameters[105].Value = model.MediaOldAmount;
            parameters[106].Value = model.NewMediaOrderIDs;
            parameters[107].Value = model.HaveInvoice;
            parameters[108].Value = model.ValueLevel;
            parameters[109].Value = model.PRAuthorizationId;
            parameters[110].Value = model.IsMediaOrder;
            parameters[111].Value = model.IsFactoring;
            parameters[112].Value = (model.FactoringDate == DateTime.MinValue || model.FactoringDate == null) ? DateTime.Parse(Common.State.datetime_minvalue) : model.FactoringDate;
            parameters[113].Value = model.RCAuditor;
            parameters[114].Value = model.RCAuditorName;
            parameters[115].Value = model.InvoiceType;
            parameters[116].Value = model.TaxRate;
            parameters[117].Value = model.FCPrIds;
            if (trans != null)
            {
                DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
                if (model.status <= State.order_ok || model.status == State.requisition_temporary_commit || model.status == State.requisition_operationAduit)
                    addPermission(model, trans);
            }
            else
            {
                using (SqlConnection newConn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    newConn.Open();
                    SqlTransaction newTrans = newConn.BeginTransaction();
                    try
                    {
                        DbHelperSQL.ExecuteSql(strSql.ToString(), newConn, newTrans, parameters);
                        if (model.status <= State.order_ok || model.status == State.requisition_temporary_commit || model.status == State.requisition_operationAduit)
                            addPermission(model, newTrans);
                        newTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        newTrans.Rollback();
                        ESP.Logging.Logger.Add(ex.Message, "GeneralInfoDataProvider.Update", ESP.Logging.LogLevel.Error, ex);
                    }
                }
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static int LogicDelete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_GeneralInfo set status=@status");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id",SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = id;
            parameters[1].Value = State.requisition_del;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public static GeneralInfo GetModel(int id)
        {
            return GetModel(id, null);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static GeneralInfo GetModel(int id,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_GeneralInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            GeneralInfo model = new GeneralInfo();
            DataSet ds = null;
            if (trans != null)
                //return ESP.Finance.Utility.CBO.FillObject<GeneralInfo>(DbHelperSQL.Query(strSql.ToString(),trans.Connection,trans, parameters));
                ds = DbHelperSQL.Query(strSql.ToString(),trans.Connection,trans, parameters);
            else
            {
                ds = DbHelperSQL.Query(strSql.ToString(), parameters);

                //return ESP.Finance.Utility.CBO.FillObject<GeneralInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.PrNo = ds.Tables[0].Rows[0]["prNo"].ToString();
                if (ds.Tables[0].Rows[0]["requestor"].ToString() != "")
                {
                    model.requestor = int.Parse(ds.Tables[0].Rows[0]["requestor"].ToString());
                }
                model.requestorname = ds.Tables[0].Rows[0]["requestorname"].ToString();
                if (ds.Tables[0].Rows[0]["app_date"].ToString() != "")
                {
                    model.app_date = DateTime.Parse(ds.Tables[0].Rows[0]["app_date"].ToString());
                }
                model.requestor_info = ds.Tables[0].Rows[0]["requestor_info"].ToString();
                model.requestor_group = ds.Tables[0].Rows[0]["requestor_group"].ToString();
                if (ds.Tables[0].Rows[0]["enduser"].ToString() != "")
                {
                    model.enduser = int.Parse(ds.Tables[0].Rows[0]["enduser"].ToString());
                }
                model.endusername = ds.Tables[0].Rows[0]["endusername"].ToString();
                model.enduser_info = ds.Tables[0].Rows[0]["enduser_info"].ToString();
                model.enduser_group = ds.Tables[0].Rows[0]["enduser_group"].ToString();
                if (ds.Tables[0].Rows[0]["goods_receiver"].ToString() != "")
                {
                    model.goods_receiver = int.Parse(ds.Tables[0].Rows[0]["goods_receiver"].ToString());
                }
                model.receivername = ds.Tables[0].Rows[0]["receivername"].ToString();
                model.receiver_info = ds.Tables[0].Rows[0]["receiver_info"].ToString();
                model.ship_address = ds.Tables[0].Rows[0]["ship_address"].ToString();
                model.project_code = ds.Tables[0].Rows[0]["project_code"].ToString();
                model.project_descripttion = ds.Tables[0].Rows[0]["project_descripttion"].ToString();
                if (ds.Tables[0].Rows[0]["buggeted"].ToString() != "")
                {
                    model.buggeted = decimal.Parse(ds.Tables[0].Rows[0]["buggeted"].ToString());
                }
                model.supplier_name = ds.Tables[0].Rows[0]["supplier_name"].ToString();
                model.supplier_address = ds.Tables[0].Rows[0]["supplier_address"].ToString();
                model.supplier_linkman = ds.Tables[0].Rows[0]["supplier_linkman"].ToString();
                model.supplier_phone = ds.Tables[0].Rows[0]["supplier_phone"].ToString();
                model.Supplier_cellphone = ds.Tables[0].Rows[0]["supplier_cellphone"].ToString();
                model.supplier_fax = ds.Tables[0].Rows[0]["supplier_fax"].ToString();
                model.supplier_email = ds.Tables[0].Rows[0]["supplier_email"].ToString();
                model.source = ds.Tables[0].Rows[0]["source"].ToString();
                model.fa_no = ds.Tables[0].Rows[0]["fa_no"].ToString();
                model.sow = ds.Tables[0].Rows[0]["sow"].ToString();
                model.payment_terms = ds.Tables[0].Rows[0]["payment_terms"].ToString();
                model.orderid = ds.Tables[0].Rows[0]["orderid"].ToString();
                model.type = ds.Tables[0].Rows[0]["type"].ToString();
                model.contrast = ds.Tables[0].Rows[0]["contrast"].ToString();
                model.consult = ds.Tables[0].Rows[0]["consult"].ToString();
                if (ds.Tables[0].Rows[0]["first_assessor"].ToString() != "")
                {
                    model.first_assessor = int.Parse(ds.Tables[0].Rows[0]["first_assessor"].ToString());
                }
                model.first_assessorname = ds.Tables[0].Rows[0]["first_assessorname"].ToString();
                model.afterwardsname = ds.Tables[0].Rows[0]["afterwardsname"].ToString();
                model.others = ds.Tables[0].Rows[0]["others"].ToString();
                model.sow2 = ds.Tables[0].Rows[0]["sow2"].ToString();
                model.sow3 = ds.Tables[0].Rows[0]["sow3"].ToString();
                model.sow4 = ds.Tables[0].Rows[0]["sow4"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                model.thirdParty_materielDesc = ds.Tables[0].Rows[0]["thirdParty_materielDesc"].ToString();
                model.thirdParty_materielID = ds.Tables[0].Rows[0]["thirdParty_materielID"].ToString();
                model.moneytype = ds.Tables[0].Rows[0]["moneytype"].ToString();
                model.requisition_overrule = ds.Tables[0].Rows[0]["requisition_overrule"].ToString();
                model.order_overrule = ds.Tables[0].Rows[0]["order_overrule"].ToString();
                model.lasttime = ds.Tables[0].Rows[0]["lasttime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(ds.Tables[0].Rows[0]["lasttime"].ToString());
                model.requisition_committime = ds.Tables[0].Rows[0]["requisition_committime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(ds.Tables[0].Rows[0]["requisition_committime"].ToString());
                model.order_committime = ds.Tables[0].Rows[0]["order_committime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(ds.Tables[0].Rows[0]["order_committime"].ToString());
                model.order_audittime = ds.Tables[0].Rows[0]["order_audittime"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(ds.Tables[0].Rows[0]["order_audittime"].ToString());

                model.EmBuy = ds.Tables[0].Rows[0]["EmBuy"].ToString();
                model.CusAsk = ds.Tables[0].Rows[0]["CusAsk"].ToString();
                model.ContractNo = ds.Tables[0].Rows[0]["ContractNo"].ToString();
                model.CusName = ds.Tables[0].Rows[0]["CusName"].ToString();
                model.ReceiverGroup = ds.Tables[0].Rows[0]["receivergroup"].ToString();

                model.Filiale_Auditor = ds.Tables[0].Rows[0]["Filiale_Auditor"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["Filiale_Auditor"].ToString());
                model.Department = ds.Tables[0].Rows[0]["Department"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["Department"].ToString();
                if (ds.Tables[0].Rows[0]["requisitionflow"].ToString() != "")
                {
                    model.Requisitionflow = int.Parse(ds.Tables[0].Rows[0]["requisitionflow"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addstatus"].ToString() != "")
                {
                    model.Addstatus = int.Parse(ds.Tables[0].Rows[0]["addstatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalprice"].ToString() != "")
                {
                    model.totalprice = decimal.Parse(ds.Tables[0].Rows[0]["totalprice"].ToString());
                }
                model.afterwardsReason = ds.Tables[0].Rows[0]["afterwardsReason"].ToString();
                model.EmBuyReason = ds.Tables[0].Rows[0]["EmBuyReason"].ToString();
                model.CusAskYesReason = ds.Tables[0].Rows[0]["CusAskYesReason"].ToString();
                model.contrastFile = ds.Tables[0].Rows[0]["contrastFile"].ToString();
                model.consultFile = ds.Tables[0].Rows[0]["consultFile"].ToString();
                model.receiver_Otherinfo = ds.Tables[0].Rows[0]["receiver_Otherinfo"].ToString();
                model.fili_overrule = ds.Tables[0].Rows[0]["fili_overrule"].ToString();
                model.receivePrepay = ds.Tables[0].Rows[0]["receivePrepay"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["receivePrepay"].ToString());
                model.PRType = ds.Tables[0].Rows[0]["prtype"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["prtype"].ToString());
                model.ProjectID = ds.Tables[0].Rows[0]["ProjectID"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["ProjectID"].ToString());
                model.Filiale_AuditName = ds.Tables[0].Rows[0]["Filiale_AuditName"].ToString();
                model.CusAskEmailFile = ds.Tables[0].Rows[0]["CusAskEmailFile"].ToString();
                model.account_name = ds.Tables[0].Rows[0]["account_name"].ToString();
                model.account_bank = ds.Tables[0].Rows[0]["account_bank"].ToString();
                model.account_number = ds.Tables[0].Rows[0]["account_number"].ToString();
                model.contrastRemark = ds.Tables[0].Rows[0]["contrastRemark"].ToString();
                model.contrastUpFiles = ds.Tables[0].Rows[0]["contrastUpFiles"].ToString();
                model.prepayBegindate = ds.Tables[0].Rows[0]["prepayBegindate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(ds.Tables[0].Rows[0]["prepayBegindate"].ToString());
                model.prepayEnddate = ds.Tables[0].Rows[0]["prepayEnddate"] == DBNull.Value ? DateTime.Parse(Common.State.datetime_minvalue) : DateTime.Parse(ds.Tables[0].Rows[0]["prepayEnddate"].ToString());
                model.InstanceID = ds.Tables[0].Rows[0]["instanceid"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["instanceId"].ToString());
                model.ProcessID = ds.Tables[0].Rows[0]["processId"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["processId"].ToString());

                if (ds.Tables[0].Rows[0]["project_id"].ToString() != "")
                {
                    model.Project_id = int.Parse(ds.Tables[0].Rows[0]["project_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DepartmentId"].ToString() != "")
                {
                    model.Departmentid = int.Parse(ds.Tables[0].Rows[0]["DepartmentId"].ToString());
                }
                model.prMediaRemark = ds.Tables[0].Rows[0]["prMediaRemark"].ToString();
                model.prMediaAuditTime = ds.Tables[0].Rows[0]["prMediaAuditTime"] == DBNull.Value ? DateTime.Parse(State.datetime_minvalue) : DateTime.Parse(ds.Tables[0].Rows[0]["prMediaAuditTime"].ToString());
                model.isMajordomoUndo = ds.Tables[0].Rows[0]["isMajordomoUndo"] == DBNull.Value ? false : bool.Parse(ds.Tables[0].Rows[0]["isMajordomoUndo"].ToString());
                if (ds.Tables[0].Rows[0]["OperationType"].ToString() != "")
                {
                    model.OperationType = int.Parse(ds.Tables[0].Rows[0]["OperationType"].ToString());
                }
                model.purchaseAuditor = ds.Tables[0].Rows[0]["purchaseAuditor"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["purchaseAuditor"].ToString());
                model.purchaseAuditorName = ds.Tables[0].Rows[0]["purchaseAuditorName"].ToString();
                model.mediaAuditor = ds.Tables[0].Rows[0]["mediaAuditor"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["mediaAuditor"].ToString());
                model.mediaAuditorName = ds.Tables[0].Rows[0]["mediaAuditorName"].ToString();
                model.adAuditor = ds.Tables[0].Rows[0]["adAuditor"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["adAuditor"].ToString());
                model.adAuditorName = ds.Tables[0].Rows[0]["adAuditorName"].ToString();
                model.adRemark = ds.Tables[0].Rows[0]["adRemark"].ToString();
                model.adAuditTime = ds.Tables[0].Rows[0]["adAuditTime"] == DBNull.Value ? DateTime.Parse(State.datetime_minvalue) : DateTime.Parse(ds.Tables[0].Rows[0]["adAuditTime"].ToString());
                model.oldFlag = ds.Tables[0].Rows[0]["oldflag"] == DBNull.Value ? false : bool.Parse(ds.Tables[0].Rows[0]["oldflag"].ToString());
                model.isCast = ds.Tables[0].Rows[0]["isCast"] == DBNull.Value ? 1 : int.Parse(ds.Tables[0].Rows[0]["isCast"].ToString());
                model.InUse = ds.Tables[0].Rows[0]["inuse"] == DBNull.Value ? ((int)State.PRInUse.Use) : int.Parse(ds.Tables[0].Rows[0]["inuse"].ToString());
                model.Foregift = ds.Tables[0].Rows[0]["foregift"] == DBNull.Value ? 0 : decimal.Parse(ds.Tables[0].Rows[0]["foregift"].ToString());
                model.appendReceiver = ds.Tables[0].Rows[0]["appendReceiver"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["appendReceiver"].ToString());
                model.appendReceiverName = ds.Tables[0].Rows[0]["appendReceiverName"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["appendReceiverName"].ToString();
                model.appendReceiverInfo = ds.Tables[0].Rows[0]["appendReceiverInfo"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["appendReceiverInfo"].ToString();
                model.appendReceiverGroup = ds.Tables[0].Rows[0]["appendReceiverGroup"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["appendReceiverGroup"].ToString();
                model.PaymentUserID = ds.Tables[0].Rows[0]["PaymentUserID"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["PaymentUserID"].ToString());
                model.PeriodType = ds.Tables[0].Rows[0]["PeriodType"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["PeriodType"].ToString());
                model.MediaOldAmount = ds.Tables[0].Rows[0]["MediaOldAmount"] == DBNull.Value ? 0 : decimal.Parse(ds.Tables[0].Rows[0]["MediaOldAmount"].ToString());
                model.NewMediaOrderIDs = ds.Tables[0].Rows[0]["NewMediaOrderIDs"].ToString();
                model.HaveInvoice = ds.Tables[0].Rows[0]["HaveInvoice"] == DBNull.Value ? false : bool.Parse(ds.Tables[0].Rows[0]["HaveInvoice"].ToString());

                if (ds.Tables[0].Rows[0]["ValueLevel"].ToString() != "")
                {
                    model.ValueLevel = int.Parse(ds.Tables[0].Rows[0]["ValueLevel"].ToString());
                }
                else
                    model.ValueLevel = 2;
                if (ds.Tables[0].Rows[0]["PRAuthorizationId"] != DBNull.Value)
                    model.PRAuthorizationId = int.Parse(ds.Tables[0].Rows[0]["PRAuthorizationId"].ToString());

                if (ds.Tables[0].Rows[0]["IsMediaOrder"].ToString() != "")
                {
                    model.IsMediaOrder = int.Parse(ds.Tables[0].Rows[0]["IsMediaOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsFactoring"].ToString() != "")
                {
                    model.IsFactoring = int.Parse(ds.Tables[0].Rows[0]["IsFactoring"].ToString());
                }
                model.FactoringDate = ds.Tables[0].Rows[0]["FactoringDate"] == DBNull.Value ? DateTime.Parse(State.datetime_minvalue) : DateTime.Parse(ds.Tables[0].Rows[0]["FactoringDate"].ToString());
                if (ds.Tables[0].Rows[0]["RCAuditor"].ToString() != "")
                {
                    model.RCAuditor = int.Parse(ds.Tables[0].Rows[0]["RCAuditor"].ToString());
                }
                model.RCAuditorName = ds.Tables[0].Rows[0]["RCAuditorName"].ToString();
                model.InvoiceType = ds.Tables[0].Rows[0]["InvoiceType"].ToString();
                if(ds.Tables[0].Rows[0]["TaxRate"] != DBNull.Value)
                    model.TaxRate = int.Parse(ds.Tables[0].Rows[0]["TaxRate"].ToString());
                model.FCPrIds = ds.Tables[0].Rows[0]["FCPrIds"].ToString();
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
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM T_GeneralInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by lasttime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得单子列表，每条单子对应自己多个的采购物品
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static DataTable GetList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.*,b.*,c.*, d.typeid as typleLevel2id, d.typename as typeLevel2Name,e.typeid as typleLevel3id, e.typename as typeLevel3Name from t_generalinfo as a");
            strSql.Append(" inner join t_orderinfo as b on a.id=b.general_id");
            strSql.Append(" inner join t_type as c on b.producttype = c.typeid");
            strSql.Append(" inner join t_type as d on c.parentid = d.typeid");
            strSql.Append(" inner join t_type as e on d.parentid = e.typeid");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by a.id desc");

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parms.ToArray());
            if (ds == null || ds.Tables.Count == 0)
                return null;
            return ds.Tables[0];
        }

        public static List<GeneralInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM T_GeneralInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.Finance.Utility.CBO.FillCollection<GeneralInfo>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));
        }
        /// <summary>
        /// 获得项目号下所有申请单
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<GeneralInfo> GetListByProjectId(int projectId)
        {
            return GetStatusList(" and project_id=" + projectId);
        }


        public static DataTable GetCostMonitor(int userid,string supplier,string projectcode,string begindate,string enddate)
        {
            string strB=" select * from V_Monitor where 1=1 ";
            if (userid != 0)
            {
                strB+=string.Format(" and (requestor={0} or goods_receiver={0} or appendreceiver={0} or applicantuserid={0} or (id in(select general_id from t_operationaudit where auditorid ={0})))", userid);
            }
            if (!string.IsNullOrEmpty(supplier))
            {
                strB += string.Format(" and supplier_name like '%{0}%'",supplier);
            }
            if (!string.IsNullOrEmpty(projectcode))
            {
                strB += string.Format(" and project_code like '%{0}%'",projectcode);
            }
            if (!string.IsNullOrEmpty(begindate))
            {
                if (string.IsNullOrEmpty(enddate))
                {
                    enddate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                strB += string.Format(" and (app_date between '{0}' and '{1}')",begindate,enddate);
            }
            return DbHelperSQL.Query(strB).Tables[0];
        }


        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static List<GeneralInfo> GetStatusList(string strWhere)
        {
            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select a.*,b.item_no,b.totalprice as ototalprice from t_generalinfo as a ");
            strB.Append(" left join ( ");
            strB.Append(" select b.id,b.general_id,b.item_no,a.totalprice from  ");
            strB.Append(" (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a ");
            strB.Append(" inner join t_orderinfo as b on a.id = b.id) as b ");
            strB.Append(" on a.id=b.general_id ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by a.lasttime desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    GeneralInfo c = new GeneralInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the status list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<GeneralInfo> GetStatusList(string strWhere, List<SqlParameter> parms)
        {
            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select a.*,b.item_no,b.totalprice as ototalprice,b.supplierid,b.producttype from t_generalinfo as a ");
            strB.Append(" left join ( ");
            strB.Append(" select b.id,b.general_id,b.item_no,a.totalprice,b.supplierid,b.producttype from  ");
            strB.Append(" (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a ");
            strB.Append(" inner join t_orderinfo as b on a.id = b.id) as b ");
            strB.Append(" on a.id=b.general_id ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by a.lasttime desc", strWhere);

            //using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            //{
            //    while (r.Read())
            //    {
            //        GeneralInfo c = new GeneralInfo();
            //        c.PopupData(r);
            //        list.Add(c);
            //    }
            //    r.Close();
            //}
            //return list;
           return ESP.Finance.Utility.CBO.FillCollection<GeneralInfo>(DbHelperSQL.Query(sql, parms.ToArray()));
        }



        public static List<GeneralInfo> GetRequisitionCommitList(string strWhere, List<SqlParameter> parms)
        {
            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append("select a.id,a.prNo,a.orderid,a.requisition_committime,a.project_code,a.project_id,a.Filiale_Auditor,a.Filiale_AuditName,");
            strB.Append("a.first_assessor,a.first_assessorname,a.supplier_name,a.status,a.requisitionflow,a.inUse,a.moneyType,");
            strB.Append("a.requestor,a.enduser,a.goods_receiver,a.requestorname,a.order_audittime,a.PRType,a.ValueLevel,a.supplier_email,");
            strB.Append("(select top 1 item_no from t_orderinfo where general_id=a.id) item_no,");
            strB.Append("a.totalprice as ototalprice,");
            strB.Append("(select top 1 supplierid from t_orderinfo where general_id=a.id) supplierid,");
            strB.Append("(select top 1 producttype from t_orderinfo where general_id=a.id) producttype ");
            strB.Append(" from t_generalinfo as a ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by a.lasttime desc", strWhere);

            //using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            //{
            //    while (r.Read())
            //    {
            //        GeneralInfo c = new GeneralInfo();
            //        c.PopupData(r);
            //        list.Add(c);
            //    }
            //    r.Close();
            //}
            //return list;
             return ESP.Finance.Utility.CBO.FillCollection<GeneralInfo>(DbHelperSQL.Query(sql, parms.ToArray()));
        }






        /// <summary>
        /// 获取可以进行收货但未收货的PR列表
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetRemindRecipientList(string terms, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.* from ( ");
            strSql.Append(" select '' as RecipientNo, EstimateDate,'' as recipientdate,a.* from t_generalinfo as a  ");
            strSql.Append(" inner join (select general_id,max(substring(intend_receipt_date,patindex('%#%',intend_receipt_date)+1,len(intend_receipt_date))) as EstimateDate "); 
            strSql.Append(" from t_orderinfo group by general_id) as b on a.id=b.general_id  ");
            strSql.Append(" where ((status = " + State.order_confirm + ") or (status = " + State.order_ok + " and requisitionflow = " + State.requisitionflow_toR + " ))  ");
            strSql.Append(" and a.id not in (select distinct gid from t_recipient where status in (" + State.recipientstatus_Unsure + "," + State.recipientstatus_All + "))");
            strSql.Append(" and EstimateDate < convert(varchar,getdate(),21)  and a.source='协议供应商' and a.inuse="+(int)State.PRInUse.Use);
            strSql.Append(" union ");
            strSql.Append(" select a.recipientNo,EstimateDate,a.recipientdate,b.* from t_recipient as a  ");
            strSql.Append(" inner join t_generalinfo as b on a.gid=b.id ");
            strSql.Append(" inner join (select general_id,max(substring(intend_receipt_date,patindex('%#%',intend_receipt_date)+1,len(intend_receipt_date))) as EstimateDate ");
            strSql.Append(" from t_orderinfo group by general_id) as c on b.id=c.general_id ");
            strSql.Append(" where a.isconfirm=" + State.recipentConfirm_Emp1 + " and b.source='协议供应商' and b.inuse=" + (int)State.PRInUse.Use);
            strSql.Append(" ) as a ");
            strSql.Append(" inner join (select general_id,supplierId,bjpaymentuserid,shpaymentuserid,gzpaymentuserid from (select min(id) as id,general_id,producttype,supplierId from t_orderinfo");
            strSql.Append(" group by general_id,producttype,supplierId) as a ");
            strSql.Append(" inner join t_type as b on a.producttype=b.typeid) as c on a.id=c.general_id");
            strSql.Append(" inner join t_supplier as d on c.supplierId=d.id");
            strSql.Append(" where 1=1" + terms);
            strSql.Append(" order by EstimateDate desc ");
            return DbHelperSQL.Query(strSql.ToString(), parms.ToArray()).Tables[0];
        }

        /// <summary>
        /// 用于媒介或个人新生成的PR单查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetTableByNewMedia(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.*, b.item_no,b.totalprice as ototalprice,c.oldprid,d.prno as oldprno from t_generalinfo as a
                                left join (
	                                select b.id,b.general_id,b.item_no,a.totalprice from
	                                (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a
		                                inner join t_orderinfo as b on a.id = b.id) as b
                                on a.id=b.general_id
                                inner join t_MediaPREditHis as c on a.id=c.newPRid
                                inner join t_generalinfo as d on c.oldprid=d.id");
            string sql = string.Format(strSql.ToString() + " where 1=1 {0} order by a.lasttime desc", strWhere);
            return DbHelperSQL.Query(sql, parms.ToArray()).Tables[0];
        }

        public static List<GeneralInfo> GetListForCollate(string strWhere, List<SqlParameter> parms)
        {
            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select a.*,b.item_no,b.totalprice as ototalprice from t_generalinfo as a ");
            strB.Append(" left join ( ");
            strB.Append(" select b.id,b.general_id,b.item_no,a.totalprice from  ");
            strB.Append(" (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a ");
            strB.Append(" inner join t_orderinfo as b on a.id = b.id) as b ");
            strB.Append(" on a.id=b.general_id ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by a.lasttime desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    GeneralInfo c = new GeneralInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the status list by media.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<GeneralInfo> GetStatusListByMedia(string strWhere, List<SqlParameter> parms)
        {
            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select aa.*,bb.item_no,bb.totalprice as ototalprice  from  ");
            strB.Append(" (select distinct a.* from t_generalinfo as a  ");
            strB.Append("             inner join T_OrderInfo as b on a.id=b.general_id and b.billtype=8 ");
            strB.Append("             inner join T_MediaOrder as c on b.id=c.orderid and c.status=0) as aa  ");
            strB.Append(" left join  ");
            strB.Append(" (select b.id,b.general_id,b.item_no,a.totalprice from  ");
            strB.Append("             (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a  ");
            strB.Append("              inner join t_orderinfo as b on a.id = b.id) as bb on aa.id=bb.general_id ");

            string sql = string.Format(strB.ToString() + " where 1=1 {0} order by aa.lasttime desc", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    GeneralInfo c = new GeneralInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the status list by pri order.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<GeneralInfo> GetStatusListByPriOrder(string strWhere, List<SqlParameter> parms)
        {
            List<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(
                @" select aa.* from (select c.ototalprice,a.*,(select top 1 item_no from t_orderinfo where general_id=a.id) as item_no 
                    ,(select top 1 orderstatus from t_orderinfo where general_id=a.id) as orderstatus 
                    ,(select top 1 desctiprtion from t_orderinfo where general_id=a.id) as desctiprtion 
                    from t_generalinfo as a inner join T_PaymentPeriod as b on a.id=b.gid and b.status=0 and b.periodtype=0 and a.status=11
                                inner join (select expectPaymentPrice as ototalprice,gid from T_PaymentPeriod where status=0 and 
                    periodtype=0 ) as c on a.id= c.gid) as aa where orderstatus=0 {0} 
                                union
                    select aa.*  from ( select b.expectPaymentPrice as ototalprice,a.* ,(select top 1 item_no from t_orderinfo 
                    where general_id=a.id) as item_no,
                    (select top 1 orderstatus from t_orderinfo where general_id=a.id) as orderstatus
                    ,(select top 1 desctiprtion from t_orderinfo where general_id=a.id) as desctiprtion 
                    from t_generalinfo as a inner join T_PaymentPeriod as b on a.id=b.gid and b.status=0 
                    and b.periodtype=1 and a.status in(5,6,7,11)) as aa where orderstatus=0 {1} order by lasttime desc");


            //string sql = string.Format(strB.ToString() + " where 1=1 {0} order by aa.lasttime desc", strWhere);
            string sql = string.Format(strB.ToString(), strWhere, strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    GeneralInfo c = new GeneralInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the status list by pri order.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetStatusListByPriOrder(string strWhere)
        {
            StringBuilder strB = new StringBuilder();
            strB.Append(
                @" select aa.* from (select c.ototalprice,b.id as pid,a.*,(select top 1 item_no from t_orderinfo where general_id=a.id) as item_no 
                    ,(select top 1 orderstatus from t_orderinfo where general_id=a.id) as orderstatus 
                    ,(select top 1 desctiprtion from t_orderinfo where general_id=a.id) as desctiprtion 
                    from t_generalinfo as a inner join T_PaymentPeriod as b on a.id=b.gid and b.status=0 and b.periodtype=0 and a.status=11
                                inner join (select expectPaymentPrice as ototalprice,gid from T_PaymentPeriod where status=0 and 
                    periodtype=0 ) as c on a.id= c.gid) as aa where orderstatus=0 {0} 
                                union
                    select aa.*  from ( select b.expectPaymentPrice as ototalprice,b.id as pid,a.* ,(select top 1 item_no from t_orderinfo 
                    where general_id=a.id) as item_no,
                    (select top 1 orderstatus from t_orderinfo where general_id=a.id) as orderstatus
                    ,(select top 1 desctiprtion from t_orderinfo where general_id=a.id) as desctiprtion 
                    from t_generalinfo as a inner join T_PaymentPeriod as b on a.id=b.gid and b.status=0 
                    and b.periodtype=1 and a.status in(5,6,7,11)) as aa where orderstatus=0 {1} order by lasttime desc");
            string sql = string.Format(strB.ToString(), strWhere, strWhere);

            return DbHelperSQL.Query(sql.ToString());
        }


        /// <summary>
        /// 获得已收货并收货单为确认状态的申请单列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<GeneralInfo> GetPaymentGeneralList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strB = new StringBuilder();
            List<GeneralInfo> list = new List<GeneralInfo>();
            strB.Append(" select distinct a.*,e.totalprice as ototalprice from t_generalinfo as a inner join (select min(id) id,sum(total) totalprice,general_id from t_orderinfo group by general_id) as e on a.id=e.general_id");
            strB.Append(" inner join t_recipient as b on a.id = b.gid ");
            strB.Append(" where a.status not in(-1,2,4,17,21) and ((b.isconfirm=" + State.recipentConfirm_Supplier + " or (b.isconfirm=" + State.recipentConfirm_Emp2 + " and a.requisitionflow={0}))");
            strB.Append(" or ((a.foregift is not null and a.foregift > 0 and (select count(*) from f_return where prid=a.id and returntype=" + (int)Common.PRTYpe.PN_ForeGift + ") = 0))) ");//未添加押金
            string sql = string.Format(string.Format(strB.ToString(), State.requisitionflow_toR) + " {0} ", strWhere);

            sql += string.Format(" union select distinct a.*,e.totalprice as ototalprice from t_generalinfo as a inner join (select min(id) id,sum(total) totalprice,general_id from t_orderinfo group by general_id) as e on a.id=e.general_id  inner join T_PaymentPeriod as b on a.id = b.gid where b.periodtype=1 and b.status<>" + State.PaymentStatus_over + " and (a.status={0} or a.status={1} or a.status={5} or (a.status={2} and a.requisitionflow <> {3}))  {4} ", State.order_confirm.ToString(), Common.State.requisition_recipiented.ToString(), State.order_ok.ToString(), State.requisitionflow_toO.ToString(), strWhere, State.requisition_recipienting);
            sql += string.Format(" union select distinct a.*,e.totalprice as ototalprice from t_generalinfo as a inner join (select min(id) id,sum(total) totalprice,general_id from t_orderinfo group by general_id) as e on a.id=e.general_id  inner join F_Return as b on a.id = b.prid where  b.ReturnStatus in(0,1) {0} order by a.lasttime desc",strWhere);

            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql, parms))
            {
                while (r.Read())
                {
                    GeneralInfo c = new GeneralInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <param name="depID"></param>
        /// <returns></returns>
        public static String createOrderID()
        {
            return CreateNum("PO", "orderid");
        }

        /// <summary>
        /// 生成申请单编号
        /// </summary>
        /// <returns></returns>
        public static String createPrNo()
        {
            return CreateNum("PR", "prNo");
        }


        /// <summary>
        /// 创建自增编号
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static string CreateNum(string prefix, string columnName)
        {
            string date = DateTime.Now.ToString("yy-MM").Replace("-", "");
            string strSql = "select max(" + columnName + ") as maxId from dbo.T_GeneralInfo where " + columnName + " like '" + prefix + date + "%'";
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            int num = 0;
            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["PrNoModel"] != "Test")
            {
                num = ds.Tables[0].Rows[0]["maxId"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["maxId"].ToString().Substring(6, 4).ToString());
            }
            else
            {
                num = ds.Tables[0].Rows[0]["maxId"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[0]["maxId"].ToString().Substring(6, 3).ToString());
            }
            string id = prefix + date;
            num++;
            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["PrNoModel"] != "Test")
            {
                for (int i = 0; i < (4 - num.ToString().Length); i++)
                {
                    id += "0";
                }
            }
            else
            {
                for (int i = 0; i < (3 - num.ToString().Length); i++)
                {
                    id += "0";
                }
            }
            if (!string.IsNullOrEmpty(ESP.Configuration.ConfigurationManager.SafeAppSettings["IsTest"]))
            {
                return id + num + ESP.Configuration.ConfigurationManager.SafeAppSettings["IsTest"].ToString();
            }
            else
            {
                return id + num;
            }
        }

        /// <summary>
        /// 检查物料总价是否超过了预算金额
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static bool contrastPrice(int id, decimal price, int orderid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.buggeted,b.totalprice as ototalprice from t_generalinfo as a");
            strSql.Append(" left join (select general_id,sum(total) as totalprice from t_orderinfo");
            if (orderid > 0)
                strSql.Append(" where id<>" + orderid);
            strSql.Append(" group by general_id) as b on a.id=b.general_id");
            strSql.Append(" where a.id=" + id);

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            decimal totalprice = (ds.Tables[0].Rows[0]["ototalprice"] == DBNull.Value ? 0 : decimal.Parse(ds.Tables[0].Rows[0]["ototalprice"].ToString())) + price;
            decimal buggeted = ds.Tables[0].Rows[0]["buggeted"] == DBNull.Value ? 0 : decimal.Parse(ds.Tables[0].Rows[0]["buggeted"].ToString());
            return buggeted >= totalprice;
        }

        /// <summary>
        /// 获得收货确定信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public static IList<GeneralInfo> GetConfrimStatusList(string strWhere)
        {
            IList<GeneralInfo> list = new List<GeneralInfo>();
            StringBuilder strB = new StringBuilder();
            strB.Append(" select a.*,b.item_no,b.totalprice as ototalprice from t_generalinfo as a ");
            strB.Append(" left join ( ");
            strB.Append(" select b.id,b.general_id,b.item_no,a.totalprice from  ");
            strB.Append(" (select min(id) id,sum(total) totalprice from t_orderinfo group by general_id) as a ");
            strB.Append(" inner join t_orderinfo as b on a.id = b.id) as b ");
            strB.Append(" on a.id=b.general_id ");
            string sql = string.Format(strB.ToString() + " where 1=1 {0}", strWhere);
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    GeneralInfo c = new GeneralInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// Gets the group by suppliername.
        /// </summary>
        /// <param name="wherestr">The wherestr.</param>
        /// <returns></returns>
        public static IList<string[]> getGroupBySuppliername(string wherestr)
        {
            IList<string[]> list = new List<string[]>();
            string sql = "select substring(project_code, 1,1) as project_de, supplier_name,  count(supplier_name) con "
                + "from t_generalInfo where 1=1 ";
            if (!string.IsNullOrEmpty(wherestr))
            {
                sql += " and id in (" + wherestr + ")";
            }
            sql += " group by supplier_name,substring(project_code, 1,1) "
                + "order by substring(project_code, 1,1)";
            DataSet dataset = DbHelperSQL.Query(sql);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0] != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        string[] arr = new string[3];
                        arr[0] = row["supplier_name"] == null ? "" : row["supplier_name"].ToString();
                        arr[1] = row["con"] == null ? "0" : row["con"].ToString();
                        arr[2] = row["project_de"] == null ? "" : row["project_de"].ToString();
                        list.Add(arr);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 收货人收货提醒
        /// </summary>
        /// <returns></returns>
        public static string sendMailToReceiver()
        {
            string Msg = "";
            string strsql = @"select a.prno,a.orderid,a.requestor,a.requestorname,a.goods_receiver,a.receivername,d.intend_receipt_date from T_generalinfo as a
                                inner join (select b.general_id,intend_receipt_date from t_orderinfo as b
                                right join (select min(id) as id,general_id from t_orderinfo group by general_id) as c on b.id=c.id) as d
                                on a.id = d.general_id
                                where status = 7 and id not in(select distinct gid from t_recipient)";

            DataSet ds = DbHelperSQL.Query(strsql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string begin = "";
                string end = "";
                DateTime Date;
                if (dr["intend_receipt_date"].ToString().IndexOf('#') != -1)
                {
                    begin = dr["intend_receipt_date"].ToString().Split('#')[0];
                    end = dr["intend_receipt_date"].ToString().Split('#')[1];
                    if (isDate(begin) && isDate(end))
                    {
                        Date = DateTime.Parse(begin) >= DateTime.Parse(end) ? DateTime.Parse(begin) : DateTime.Parse(end);
                    }
                    else if (isDate(begin) || isDate(end))
                    {
                        if (isDate(begin))
                            Date = DateTime.Parse(begin);
                        else
                            Date = DateTime.Parse(end);
                    }
                    else
                    {
                        continue;
                    }

                }
                else
                {
                    if (isDate(dr["intend_receipt_date"].ToString()))
                        Date = DateTime.Parse(dr["intend_receipt_date"].ToString());
                    else
                        continue;
                }
                if (DateTime.Now > Date.AddDays(3))
                {
                    //给收货人发信
                    string body = string.Format("您是订单编号为{0}的收货人，该订单的预计收货时间为{1}，请您及时收货。", dr["orderid"].ToString(), Date.ToString("yyyy-MM-dd"));
                    string receiverEmail = new ESP.Compatible.Employee(int.Parse(dr["goods_receiver"].ToString())).EMail;
                    try
                    {
                        ESP.ConfigCommon.SendMail.Send1("收货提醒", receiverEmail, body, true);
                        Msg += string.Format("订单编号：{0} 收货人邮箱：{1} 发送时间：{2} 发送状态：成功 \r\n", dr["orderid"].ToString(), receiverEmail, DateTime.Now.ToString());
                    }
                    catch (Exception ex)
                    {
                        Msg += string.Format("订单编号：{0} 收货人邮箱：{1} 发送时间：{2} 发送状态：失败 \r\n", dr["orderid"].ToString(), receiverEmail, DateTime.Now.ToString());
                        continue;
                    }
                }
            }
            return Msg;
        }

        /// <summary>
        /// Determines whether the specified STR is date.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>
        /// 	<c>true</c> if the specified STR is date; otherwise, <c>false</c>.
        /// </returns>
        private static bool isDate(string str)
        {
            try
            {
                DateTime.Parse(str);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 费用明细描述
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string GetPNDes(int gid)
        {
            string pnDes = "";
            Entity.GeneralInfo gModel = DataAccess.GeneralInfoDataProvider.GetModel(gid);
            List<Entity.OrderInfo> oModelList = DataAccess.OrderInfoDataHelper.GetListByGeneralId(gid);
            if (gModel != null)
            {
                pnDes += gModel.project_descripttion + "项目" + (State.oldFlagNames[gModel.oldFlag == false ? 0 : 1]) + "：";
                foreach (Entity.OrderInfo oModel in oModelList)
                {
                    pnDes += oModel.Item_No + "；";
                }
            }
            return pnDes;
        }

        /// <summary>
        /// 根据新pr单取得老PR单id
        /// </summary>
        /// <param name="gid">The gid.</param>
        /// <returns></returns>
        public static int GetOldPRIdByNewPRId(int gid)
        {
            int oldprid = 0;
            StringBuilder strB = new StringBuilder();
            strB.Append(" select distinct top 1 OldPRId from T_MediaPREditHis where NewPRId={0}");
            string sql = string.Format(strB.ToString(), gid.ToString());
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    if (null != r["OldPRId"] && r["OldPRId"].ToString() != string.Empty)
                    {
                        oldprid = int.Parse(r["OldPRId"].ToString());
                    }
                }
                r.Close();
            }
            return oldprid;
        }

        /// <summary>
        /// 根据新pN单取得老PR单id
        /// </summary>
        /// <param name="pnid">The pnid.</param>
        /// <returns></returns>
        public static int GetOldPRIdByNewPNId(int pnid)
        {
            int oldprid = 0;
            StringBuilder strB = new StringBuilder();
            strB.Append(" select distinct top 1 OldPRId from T_MediaPREditHis where NewPNId={0}");
            string sql = string.Format(strB.ToString(), pnid.ToString());
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    if (null != r["OldPRId"] && r["OldPRId"].ToString() != string.Empty)
                    {
                        oldprid = int.Parse(r["OldPRId"].ToString());
                    }
                }
                r.Close();
            }
            return oldprid;
        }

        public static int GetPaymentPeriodCount(int prid)
        {
            int count = 0;
            StringBuilder strB = new StringBuilder();
            strB.Append(" select count(*) as count from t_paymentperiod where gid={0}");
            string sql = string.Format(strB.ToString(), prid.ToString());
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    if (null != r["count"] && r["count"].ToString() != string.Empty)
                    {
                        count = int.Parse(r["count"].ToString());
                    }
                }
                r.Close();
            }
            return count;
        }

        public static int GetPNCount(int prid)
        {
            int count = 0;
            StringBuilder strB = new StringBuilder();
            strB.Append(" select count(*) as count from f_return where prid={0} and returntype not in(11,34)");
            string sql = string.Format(strB.ToString(), prid.ToString());
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    if (null != r["count"] && r["count"].ToString() != string.Empty)
                    {
                        count = int.Parse(r["count"].ToString());
                    }
                }
                r.Close();
            }
            return count;
        }


        /// <summary>
        /// 变更申请单的业务审核人
        /// </summary>
        /// <param name="prIds"></param>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <param name="currentUser"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static int ChangePrOperationAuditor(string prIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser, System.Web.HttpRequest request)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] Ids = prIds.Split(',');
                    foreach(string id in Ids)
                    {
                        OperationAuditDataProvider provider = new OperationAuditDataProvider();
                        OperationAuditInfo operationAudit = new OperationAuditDataProvider().GetModelByUserId(int.Parse(id),oldUserId,trans);
                        operationAudit.auditorId = newUserId;
                        if (provider.Update(operationAudit, trans.Connection, trans) > 0)
                        {
                            Entity.GeneralInfo Model = GetModel(int.Parse(id), trans);
                            if (Model.ProcessID > 0 && Model.InstanceID > 0)
                            {
                                WorkFlowDAO.ProcessInstanceDao instanceDao = new WorkFlowDAO.ProcessInstanceDao();
                                instanceDao.UpdateRoleWhenLastDay("SR", Model.ProcessID, Model.InstanceID, oldUserId, newUserId, emp1.Name, emp.Name, trans);
                            }
                            addPermission(int.Parse(id), "operationaudit", newUserId, trans);
                            ESP.Purchase.Entity.LogInfo log = new LogInfo();
                            log.Gid = int.Parse(id);
                            log.LogUserId = int.Parse(currentUser.SysID);
                            log.LogMedifiedTeme = DateTime.Now;
                            log.Des = "业务审核人" + emp1.Name + "变更为" + emp.Name + " " + DateTime.Now;
                            new ESP.Purchase.DataAccess.LogDataProvider().Add(log, request, trans.Connection, trans);
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static DataTable getGeneralJoinOperationAudit(int oldUserId, string changeColumn)
        {
            string strSql = @"select a.*,b.auditorid from t_generalinfo as a
                                inner join t_operationaudit as b on a.id=b.general_id";
            strSql += " where Status not in (" + State.requisition_del + "," + State.requisition_save + "," + State.requisition_return + "," + State.requisition_Stop + "," + State.requisition_paid + ")";
            strSql += string.Format(" and ({0}={1})", changeColumn,oldUserId);
            return DbHelperSQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 变更申请单人员信息
        /// </summary>
        /// <param name="prIds">申请单ID</param>
        /// <param name="changeColumn">要变更的字段</param>
        /// <param name="oldUserId">原始人员ID</param>
        /// <param name="newUserId">新人员ID</param>
        /// <returns></returns>
        public static int ChangePrUsers(string prIds,string changeColumn, int oldUserId, int newUserId,ESP.Compatible.Employee currentUser,System.Web.HttpRequest request)
        {
            changeColumn = changeColumn.ToLower();
            string strSql = "update t_generalInfo set " + changeColumn + "=" + newUserId;
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            string logDes = "";
            string groupName  = emp.GetDepartmentNames().Count == 0 ? "" : emp.GetDepartmentNames()[0].ToString();
            switch (changeColumn)
            {
                case "requestor":
                    strSql += ",requestorname='" + emp.Name + "',requestor_info='" + emp.Telephone + "',requestor_group='" + groupName+"'";
                    logDes = "申请人"+emp1.Name+"变更为"+emp.Name;
                    break;
                case "goods_receiver":
                    strSql += ",receivername='" + emp.Name + "',receiver_info='" + emp.Telephone + "',receivergroup='" + groupName + "'";
                    logDes = "收货人" + emp1.Name + "变更为" + emp.Name;
                    break;
                case "appendreceiver":
                    strSql += ",appendReceiverName='" + emp.Name + "',appendReceiverInfo='" + emp.Telephone + "',appendReceiverGroup='" + groupName + "'";
                    logDes = "附加收货人" + emp1.Name + "变更为" + emp.Name;
                    break;
                case "filiale_auditor":
                    strSql += ", Filiale_AuditName='" + emp.Name + "'";
                    logDes = "分公司审核人" + emp1.Name + "变更为" + emp.Name;
                    break;
                case "first_assessor":
                    strSql += ",first_assessorname='" + emp.Name + "'";
                    logDes = "物料审核人" + emp1.Name + "变更为" + emp.Name;
                    break;
                case "purchaseauditor":
                    strSql += ",purchaseAuditorName='" + emp.Name + "'";
                    logDes = "采购总监" + emp1.Name + "变更为" + emp.Name;
                    break;
                case "mediaauditor":
                    strSql += ",mediaAuditorName='" + emp.Name + "'";
                    logDes = "媒介总监" + emp1.Name + "变更为" + emp.Name;
                    break;
                case "adauditor":
                    strSql += ",adAuditorName='" + emp.Name + "'";
                    logDes = "AD总监" + emp1.Name + "变更为" + emp.Name;
                    break;
            }
            strSql += " where id=@prId and " + changeColumn + "=" + oldUserId;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] ids = prIds.Split(',');
                    foreach(string id in ids)
                    {
                        SqlParameter parm = new SqlParameter("@prId", id);
                        if (DbHelperSQL.ExecuteSql(strSql, conn, trans, parm) > 0)
                        {
                            addPermission(int.Parse(id), changeColumn, newUserId, trans);
                            ESP.Purchase.Entity.LogInfo log = new LogInfo();
                            log.Gid = int.Parse(id);
                            log.LogUserId = int.Parse(currentUser.SysID);
                            log.LogMedifiedTeme = DateTime.Now;
                            log.Des = logDes + " " + DateTime.Now;
                            new ESP.Purchase.DataAccess.LogDataProvider().Add(log, request,trans.Connection,trans);
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch 
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        private static void addPermission(int generalId,string changeColumn,int newUserId,SqlTransaction trans)
        {
            GeneralInfo generalModel = GetModel(generalId,trans);
            Entity.DataInfo dataModel = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)State.DataType.PR,generalModel.id,trans);
            if (dataModel == null)
            {
                dataModel = new DataInfo();
                dataModel.DataType = (int)State.DataType.PR;
                dataModel.DataId = generalId;
            }
            #region 维护人员权限
            List<Entity.DataPermissionInfo> permissionList = new List<DataPermissionInfo>();
            Entity.DataPermissionInfo perission = new DataPermissionInfo();
            perission.UserId = newUserId;
            changeColumn = changeColumn.ToLower();
            switch (changeColumn)
            {
                case "requestor":
                    perission.IsEditor = true;
                    perission.IsViewer = true;
                    break;
                case "goods_receiver":
                    perission.IsEditor = false;
                    perission.IsViewer = true;
                    break;
                case "appendreceiver":
                    perission.IsEditor = false;
                    perission.IsViewer = true;
                    break;
                case "filiale_auditor":
                    perission.IsEditor = true;
                    perission.IsViewer = true;
                    break;
                case "first_assessor":
                    perission.IsEditor = true;
                    perission.IsViewer = true;
                    break;
                case "purchaseauditor":
                    perission.IsEditor = true;
                    perission.IsViewer = true;
                    break;
                case "mediaauditor":
                    perission.IsEditor = true;
                    perission.IsViewer = true;
                    break;
                case "adauditor":
                    perission.IsEditor = true;
                    perission.IsViewer = true;
                    break;
                case "operationaudit" :
                    perission.IsEditor = false;
                    perission.IsViewer = true;
                    break;
            }
            permissionList.Add(perission);
            #endregion
            if(dataModel.Id > 0)
                new DataAccess.DataPermissionProvider().AppendDataPermission(dataModel, permissionList, trans);
            else
                new DataAccess.DataPermissionProvider().AddDataPermission(dataModel, permissionList, trans);
        }

        #endregion  成员方法
    }
}