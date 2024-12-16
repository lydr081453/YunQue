using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using System.Data.SqlClient;

public partial class Purchase_Print_PNPrintForPurchaseBatch : System.Web.UI.Page
{
    int batchId = 0;

    decimal totalPrice = 0m;
    string accountName = "";
    string accountBankName = "";
    string accountBankNo = "";

    string id = "";
    int num = 1;
    int numP = 1;
    string moneytype = "";
    int listCount = 0;
    private decimal dynamicPercent = 0;
    decimal dynamicTotalPrice = 0;
    static decimal recipienttotalPrice = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            batchId = int.Parse(Request[ESP.Finance.Utility.RequestName.BatchID]);
            ESP.Finance.Entity.PNBatchInfo batchModel = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);
            this.lblPurchaseBatchCode.Text = batchModel.PurchaseBatchCode;
            this.lblBatchCode.Text = batchModel.BatchCode;

            getAuditLog(batchId);
            this.lblTitle.Text = "支票/电汇付款申请单";

            string terms = " and c.batchId=" + batchId;
            //列表绑定
            DataTable dtList = ESP.Finance.BusinessLogic.ReturnManager.GetPNTableLinkPR(terms, new List<System.Data.SqlClient.SqlParameter>());



            if (dtList != null && dtList.Rows.Count > 0 && !string.IsNullOrEmpty(dtList.Rows[0]["PaymentTypeID"].ToString()))
            {
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(dtList.Rows[0]["ProjectCode"].ToString().Substring(0, 1));
                logoImg.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/" + branchModel.Logo;

                ESP.Finance.Entity.PaymentTypeInfo paymentModel = ESP.Finance.BusinessLogic.PaymentTypeManager.GetModel(Convert.ToInt32(dtList.Rows[0]["PaymentTypeID"].ToString()));
                if (paymentModel != null)
                {
                    if (paymentModel.Tag == "PR")
                    {
                        this.lblTitle.Text = "支票/电汇付款申请单";
                    }
                    else if (paymentModel.Tag == "Cash")
                    {
                        if (Convert.ToInt32(dtList.Rows[0]["ReturnType"].ToString()) == (int)ESP.Purchase.Common.PRTYpe.PN_KillCash || Convert.ToInt32(dtList.Rows[0]["ReturnType"].ToString()) == (int)ESP.Purchase.Common.PRTYpe.MediaPR || Convert.ToInt32(dtList.Rows[0]["ReturnType"].ToString()) == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                        {
                            this.lblTitle.Text = "现金付款申请单";
                        }
                        else if (Convert.ToInt32(dtList.Rows[0]["ReturnType"].ToString()) == (int)ESP.Purchase.Common.PRTYpe.PN_Cash10Down)
                        {
                            if (Convert.ToInt32(dtList.Rows[0]["ReturnStatus"].ToString()) == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                                this.lblTitle.Text = "现金付款申请单";
                            else
                                this.lblTitle.Text = "现金借款申请单";
                        }
                        else
                        {
                            if (Convert.ToInt32(dtList.Rows[0]["ReturnStatus"].ToString()) == (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete)
                                this.lblTitle.Text = "现金付款申请单";
                            else
                                this.lblTitle.Text = "现金借款申请单";
                        }
                    }
                    else if (paymentModel.Tag == "Card")
                    {
                        this.lblTitle.Text = "商务卡付款申请单";
                    }
                }
            }
            if (string.IsNullOrEmpty(Request["Type"]))
            {
                printPRGR(dtList);
            }
            repList.DataSource = dtList;
            repList.DataBind();

            lab_TotalPrice.Text = totalPrice.ToString("#,##0.00");// dr["inceptPrice"] == DBNull.Value ? "" : decimal.Parse(dr["inceptPrice"].ToString()).ToString("#,##0.00");
            labAccountName.Text = accountName; //dr["account_name"].ToString();
            labAccountBankName.Text = accountBankName; //dr["account_bank"].ToString();
            labAccountBankNo.Text = accountBankNo; //dr["account_number"].ToString();


            //if (batchModel.Status == (int)PaymentStatus.PurchaseMajor1)
            //{
            //   int currentAduitType = (int)ESP.Finance.Utility.auditorType.purchase_major2;
            //   ESP.Framework.Entity.EmployeeInfo nextAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(batchModel.BranchCode).FirstFinanceID);
            //    batchModel.Status = (int)PaymentStatus.MajorAudit;
            //   int nextAuditType = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            //   bool isLast = false;

            //   batchModel.PaymentUserID = nextAuditor.UserID;
            //   batchModel.PaymentCode = nextAuditor.Code;
            //   batchModel.PaymentEmployeeName = nextAuditor.FullNameCN;
            //   batchModel.PaymentUserName = nextAuditor.FullNameEN;

            //   ESP.Compatible.Employee CurrentUser = new  ESP.Compatible.Employee(batchModel.CreatorID.Value);

            //   ESP.Finance.BusinessLogic.PNBatchManager.BatchAuditForPurchase(batchModel, batchModel, CurrentUser, (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing, currentAduitType, nextAuditType);
            //   ESP.Finance.Utility.SendMailHelper.SendMailPurchaseBatch(true, isLast, nextAuditor.FullNameCN, nextAuditor.Email, batchModel.BatchID);

            //}
        }
    }

    private void getAuditLog(int batchID)
    {
        IList<ESP.Finance.Entity.AuditLogInfo> logList = ESP.Finance.BusinessLogic.AuditLogManager.GetBatchList(batchID);
        if (logList.Count > 0)
        {
            foreach (ESP.Finance.Entity.AuditLogInfo hist in logList)
            {
                string austatus = string.Empty;
                if (hist.AuditStatus == (int)AuditHistoryStatus.PassAuditing)
                {
                    austatus = "审批通过";
                }
                else if (hist.AuditStatus == (int)AuditHistoryStatus.TerminateAuditing)
                {
                    austatus = "审批驳回";
                }
                string auditdate = hist.AuditDate == null ? "" : hist.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                this.lblAuditList.Text += hist.AuditorEmployeeName + "(" + hist.AuditorUserName + ")" + austatus + "[" + auditdate + "]" + hist.Suggestion + "<br/>";
            }
        }
    }
    private void printPRGR(DataTable dtList)
    {
        //string printContent = "";
        //System.Collections.SortedList ht = new System.Collections.SortedList();
        //System.Collections.ArrayList keys = new System.Collections.ArrayList();
        //for(int i=0;i<dtList.Rows.Count;i++)
        //{
        //    if (!ht.Contains(int.Parse(dtList.Rows[i]["prid"].ToString())))
        //    {
        //        ht.Add(int.Parse(dtList.Rows[i]["prid"].ToString()), int.Parse(dtList.Rows[i]["returnid"].ToString()));
        //        keys.Add(dtList.Rows[i]["prid"].ToString());
        //    }
        //    else
        //    {
        //        ht[int.Parse(dtList.Rows[i]["prid"].ToString())] = ht[int.Parse(dtList.Rows[i]["prid"].ToString())] + "," + dtList.Rows[i]["returnid"].ToString();
        //    }
        //}
        //foreach (object o in keys)
        //{
        //    printContent += o.ToString() + ",";
        //}
        //printContent = printContent.TrimEnd(',');
        List<GeneralInfo> list = GeneralInfoManager.GetStatusList(" and a.id in (select prid from f_return where returnid in(select returnid from f_pnbatchrelation where BatchID=" + batchId.ToString() + "))");
        listCount = list.Count;
        repPRGR.DataSource = list;
        repPRGR.DataBind();
    }

    protected void repPRGR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GeneralInfo model = (GeneralInfo)e.Item.DataItem;
            Repeater repPR = (Repeater)e.Item.FindControl("repPR");
            Repeater repGR = (Repeater)e.Item.FindControl("repGR");
            List<GeneralInfo> list = GeneralInfoManager.GetStatusList(" and a.id in (" + model.id + ")");
            listCount = list.Count;
            repPR.DataSource = list;
            repPR.DataBind();

            batchId = int.Parse(Request[ESP.Finance.Utility.RequestName.BatchID]);
            IList<ESP.Finance.Entity.ReturnInfo> batchlist = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchId);
            string returnids = "";
            foreach (ESP.Finance.Entity.ReturnInfo ret in batchlist)
            {
                returnids += ret.ReturnID.ToString() + ",";
            }
            returnids = returnids.TrimEnd(',');

            DataSet ds = RecipientManager.GetRecipientList(" and a.GId in (" + model.id + ")", new List<SqlParameter>());
            listCount = ds.Tables[0].Rows.Count;
            repGR.DataSource = ds.Tables[0];
            repGR.DataBind();
        }
    }

    protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;

            Label labReturnFactDate = (Label)e.Item.FindControl("labReturnFactDate");
            ESP.Purchase.Entity.PaymentPeriodInfo paymentModel = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(Convert.ToInt32(dr["returnid"]));
            if (paymentModel != null)
            {
                List<ESP.Purchase.Entity.PeriodRecipientInfo> relationList = ESP.Purchase.BusinessLogic.PeriodRecipientManager.GetModelList(" periodid=" + paymentModel.id.ToString());
                if (relationList != null && relationList.Count > 0)
                {
                    ESP.Purchase.Entity.RecipientInfo recipientModel = ESP.Purchase.BusinessLogic.RecipientManager.GetModel(relationList[0].recipientId);
                    labReturnFactDate.Text = recipientModel.RecipientDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    labReturnFactDate.Text = dr["ReturnPreDate"].ToString();
                }
            }
            else
            {
                labReturnFactDate.Text = dr["ReturnPreDate"].ToString();
            }

            Label labProjectCode = (Label)e.Item.FindControl("labProjectCode");
            labProjectCode.Text = dr["projectcode"].ToString();
            Label labRequestorUserName = (Label)e.Item.FindControl("labRequestorUserName");
            labRequestorUserName.Text = dr["requestemployeename"].ToString();
            Label labRequestorID = (Label)e.Item.FindControl("labRequestorID");
            labRequestorID.Text = new ESP.Compatible.Employee(int.Parse(dr["requestorid"].ToString())).ID.ToString();
            Label labDepartment = (Label)e.Item.FindControl("labDepartment");
            labDepartment.Text = dr["Department"].ToString();
            Label labReturnContent = (Label)e.Item.FindControl("labReturnContent");
            labReturnContent.Text = dr["returnContent"].ToString();
            Label labPreFee = (Label)e.Item.FindControl("labPreFee");
            labPreFee.Text = dr["prefee"] == DBNull.Value ? "" : decimal.Parse(dr["prefee"].ToString()).ToString("#,##0.00");
            Label labOrderid = (Label)e.Item.FindControl("labOrderid");
            labOrderid.Text = dr["orderid"].ToString();
            //Label lblRemark = (Label)e.Item.FindControl("lblRemark");
            //lblRemark.Text = dr["remark"].ToString();
            totalPrice += decimal.Parse(dr["prefee"] == DBNull.Value ? "0" : dr["prefee"].ToString());
            accountName = dr["supplierName"].ToString();
            accountBankName = dr["supplierBankName"].ToString();
            accountBankNo = dr["supplierBankAccount"].ToString();
            Label labPNno = (Label)e.Item.FindControl("labPNno");
            labPNno.Text = dr["returnCode"].ToString();
        }
    }


    #region PR

    public void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex == 0)
        {
            num = 1;
        }
        Label number = (Label)(e.Item.FindControl("labNum"));
        number.Text = num.ToString();
        num++;

        Label repMoneytype = (Label)(e.Item.FindControl("lab_rep_moneytype"));
        repMoneytype.Text = moneytype;
    }

    public void repPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex == 0)
        {
            numP = 1;
            dynamicPercent = 0;
            //dynamicTotalPrice = 0;
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label number = (Label)(e.Item.FindControl("labNum"));
            number.Text = numP.ToString();
            numP++;

            DataRowView dv = (DataRowView)e.Item.DataItem;

            Literal LitOverplusPrice = (Literal)e.Item.FindControl("LitOverplusPrice");

            dynamicPercent += decimal.Parse(dv["expectPaymentPercent"].ToString());
            LitOverplusPrice.Text = (dynamicTotalPrice - dynamicTotalPrice * (dynamicPercent / 100)).ToString("#,##0.00");
        }
    }

    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }

    protected void repPR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GeneralInfo generalInfo = (GeneralInfo)e.Item.DataItem;
            //item列表项
            List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + generalInfo.id);
            List<ESP.Purchase.Entity.PaymentPeriodInfo> paymentList = PaymentPeriodManager.GetModelList(" gid=" + generalInfo.id);
            string PNS = string.Empty;
            moneytype = generalInfo.moneytype;
            Repeater repItem = (Repeater)e.Item.FindControl("repItem");
            repItem.DataSource = orderList;
            repItem.DataBind();

            Label labglideNo = (Label)e.Item.FindControl("labglideNo");
            Label lblPN = (Label)e.Item.FindControl("lblPN");
            Label lab_AppendUser = (Label)e.Item.FindControl("lab_AppendUser");
            Label laboldprinfo = (Label)e.Item.FindControl("laboldprinfo");
            Label lblYaJin = (Label)e.Item.FindControl("lblYaJin");
            lblYaJin.Text = generalInfo.Foregift == null ? "" : generalInfo.Foregift.ToString("#,##0.00");

            if (!string.IsNullOrEmpty(Request["Action"]) && Request["Action"].ToString() == "ViewOldPr")
            {
                laboldprinfo.Text = "<br>姓名: " + generalInfo.supplier_name + "<br>身份证:" + generalInfo.supplier_address + "<br>帐号:" + generalInfo.account_number + "<br>银行:" + generalInfo.account_bank + "<br>名称：" + generalInfo.account_name;
            }
            lab_AppendUser.Text = generalInfo.appendReceiverName + "(" + generalInfo.appendReceiverInfo + ")";
            labglideNo.Text = generalInfo.id.ToString("0000000");
            //PN number
            foreach (ESP.Purchase.Entity.PaymentPeriodInfo p in paymentList)
            {
                PNS += p.ReturnCode + ",";
            }
            if (!string.IsNullOrEmpty(PNS))
                lblPN.Text = "付款单号：" + PNS.TrimEnd(',') + "<br/>";
            ESP.Purchase.Entity.MediaPREditHisInfo relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(generalInfo.id);
            if (relationModel != null && relationModel.NewPRId != null)
            {
                ESP.Purchase.Entity.GeneralInfo newGeneral = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(relationModel.NewPRId.Value);
                lblPN.Text += "新PR流水：" + newGeneral.id.ToString() + "<br/>";
                lblPN.Text += "新PR单号：" + newGeneral.PrNo;
            }
            //申请单号
            Label labPrno = (Label)e.Item.FindControl("labPrno");
            labPrno.Text = generalInfo.PrNo;

            Label labLX = (Label)e.Item.FindControl("labLX");
            labLX.Text = "Pr → " + ESP.Purchase.Common.State.requisitionflow_state[generalInfo.Requisitionflow].ToString();

            //日志
            List<ESP.Purchase.Entity.LogInfo> loglist = LogManager.GetLoglistWithFinance(generalInfo.id);
            string strlog = string.Empty;
            foreach (ESP.Purchase.Entity.LogInfo log in loglist)
            {
                strlog += log.Des + "<br/>";
            }
            Label lablog = (Label)e.Item.FindControl("lablog");
            lablog.Text = strlog;

            //业务审核日志
            IList<ESP.Purchase.Entity.AuditLogInfo> oploglist = AuditLogManager.GetModelListByGID(generalInfo.id);
            string strOplog = string.Empty;
            foreach (ESP.Purchase.Entity.AuditLogInfo log in oploglist)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.auditUserId);
                strOplog += log.auditUserName + "(" + emp.FullNameEN + ")" + ESP.Purchase.Common.State.operationAudit_statusName[log.auditType] + " " + log.remark + " " + log.remarkDate + "<br/>";
            }
            Label labAuditLog = (Label)e.Item.FindControl("labAuditLog");
            labAuditLog.Text = strOplog;

            Label labOverrule = (Label)e.Item.FindControl("labOverrule");
            labOverrule.Text = generalInfo.requisition_overrule;

            //供应商名称
            Label lab_supplier_name = (Label)e.Item.FindControl("lab_supplier_name");
            lab_supplier_name.Text = generalInfo.supplier_name;
            //地址
            Label lab_supplier_address = (Label)e.Item.FindControl("lab_supplier_address");
            lab_supplier_address.Text = generalInfo.supplier_address;
            //联系人
            Label lab_supplier_linkman = (Label)e.Item.FindControl("lab_supplier_linkman");
            lab_supplier_linkman.Text = generalInfo.supplier_linkman;
            //联系电话
            Label lab_supplier_phone = (Label)e.Item.FindControl("lab_supplier_phone");
            lab_supplier_phone.Text = generalInfo.supplier_phone;
            //传真 
            Label lab_supplier_fax = (Label)e.Item.FindControl("lab_supplier_fax");
            lab_supplier_fax.Text = generalInfo.supplier_fax;
            //电子邮件
            Label lab_supplier_email = (Label)e.Item.FindControl("lab_supplier_email");
            lab_supplier_email.Text = generalInfo.supplier_email;
            //供应商来源
            Label lab_source = (Label)e.Item.FindControl("lab_source");
            lab_source.Text = generalInfo.source;

            //送货至地址
            Label lab_ship_address = (Label)e.Item.FindControl("lab_ship_address");
            lab_ship_address.Text = generalInfo.ship_address;
            //送货至邮件
            if (generalInfo.goods_receiver > 0)
            {
                ESP.Compatible.Employee receiver = new ESP.Compatible.Employee(generalInfo.goods_receiver);
            }

            //采购方
            ESP.Compatible.Employee buyer = null;

            string[] projectNo = generalInfo.project_code.Split('-');
            if (projectNo.Length > 0 && !string.IsNullOrEmpty(projectNo[0]))
            {
                Label lab_DepartmentName = (Label)e.Item.FindControl("lab_DepartmentName");
                Label lab_buyer_Address = (Label)e.Item.FindControl("lab_buyer_Address");
                lab_DepartmentName.Text = ESP.Purchase.Common.State.NameHT[projectNo[0].ToString()] == null ? "项目号有误" : ESP.Purchase.Common.State.NameHT[projectNo[0].ToString()].ToString();
                lab_buyer_Address.Text = ESP.Purchase.Common.State.AddressHT[projectNo[0].ToString()] == null ? "项目号有误" : ESP.Purchase.Common.State.AddressHT[projectNo[0].ToString()].ToString();
            }

            if (/*generalInfo.Filiale_Auditor != null && */generalInfo.Filiale_Auditor != 0)
            {
                buyer = new ESP.Compatible.Employee(generalInfo.Filiale_Auditor);
            }
            else
            {
                buyer = new ESP.Compatible.Employee(generalInfo.first_assessor);
            }

            if (buyer != null)
            {
                Label lab_buyer_contect_Name = (Label)e.Item.FindControl("lab_buyer_contect_Name");
                Label lab_buyer_Telephone = (Label)e.Item.FindControl("lab_buyer_Telephone");
                Label lab_buyer_EMail = (Label)e.Item.FindControl("lab_buyer_EMail");
                lab_buyer_contect_Name.Text = buyer.Name;
                lab_buyer_Telephone.Text = buyer.Telephone;
                lab_buyer_EMail.Text = buyer.EMail;
            }

            decimal totalprice = 0;
            foreach (OrderInfo o in orderList)
            {
                totalprice += o.total;
            }
            dynamicTotalPrice = totalprice;
            //总价
            Label lab_moneytype = (Label)e.Item.FindControl("lab_moneytype");
            lab_moneytype.Text = (generalInfo.moneytype == "美元" ? "＄" : "￥") + decimal.Round(totalprice, 4).ToString("#,##0.00"); //totalprice.ToString("#.##0.00");


            //工作需求描述
            Label lab_sow = (Label)e.Item.FindControl("lab_sow");
            lab_sow.Text = generalInfo.sow;

            //备注
            Label lab_sow3 = (Label)e.Item.FindControl("lab_sow3");
            lab_sow3.Text = generalInfo.sow3;

            //申请人
            Label lab_requestorname = (Label)e.Item.FindControl("lab_requestorname");
            lab_requestorname.Text = generalInfo.requestorname;
            //申请日期
            Label lab_app_date = (Label)e.Item.FindControl("lab_app_date");
            lab_app_date.Text = generalInfo.app_date.ToString("yyyy-MM-dd");
            //联络
            Label lab_requestor_info = (Label)e.Item.FindControl("lab_requestor_info");
            lab_requestor_info.Text = generalInfo.requestor_info;
            //业务组别
            Label lab_requestor_group = (Label)e.Item.FindControl("lab_requestor_group");
            lab_requestor_group.Text = generalInfo.requestor_group;
            //使用人
            Label lab_endusername = (Label)e.Item.FindControl("lab_endusername");
            lab_endusername.Text = generalInfo.endusername;
            //联络
            Label lab_enduser_info = (Label)e.Item.FindControl("lab_enduser_info");
            lab_enduser_info.Text = generalInfo.enduser_info;
            //收货人
            Label lab_receivername2 = (Label)e.Item.FindControl("lab_receivername2");
            lab_receivername2.Text = generalInfo.receivername;
            //联络
            Label lab_receiver_info2 = (Label)e.Item.FindControl("lab_receiver_info2");
            lab_receiver_info2.Text = generalInfo.receiver_info;
            //项目号
            Label lab_project_code = (Label)e.Item.FindControl("lab_project_code");
            lab_project_code.Text = generalInfo.project_code;
            //项目号内容描述 
            Label lab_project_descripttion = (Label)e.Item.FindControl("lab_project_descripttion");
            lab_project_descripttion.Text = generalInfo.project_descripttion + ESP.Purchase.Common.State.oldFlagNames[generalInfo.oldFlag == false ? 0 : 1];
            //第三方采购成本预算 
            Label lab_buggeted = (Label)e.Item.FindControl("lab_buggeted");
            lab_buggeted.Text = generalInfo.buggeted.ToString("#,##0.0");
            //收货人其他联络方式: 
            Label lab_OtherInfo = (Label)e.Item.FindControl("lab_OtherInfo");
            lab_OtherInfo.Text = generalInfo.receiver_Otherinfo;

            if (listCount > 1 && e.Item.ItemIndex != (listCount - 1))
            {
                Label labafter = (Label)e.Item.FindControl("labafter");
                labafter.Text = "<p style=\"page-break-after:always\">&nbsp;</p>";
            }

            //账期列表项
            DataTable dt = PaymentPeriodManager.GetPaymentPeriodList(generalInfo.id);
            Repeater repPeriod = (Repeater)e.Item.FindControl("repPeriod");
            repPeriod.DataSource = dt;
            repPeriod.DataBind();
        }
    }

    #endregion

    #region GR
    protected void repGR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            recipienttotalPrice = 0;
            //item列表项
            List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + drv.Row["Gid"].ToString());

            moneytype = drv.Row["moneytype"].ToString();
            Repeater repRecipientItem = (Repeater)e.Item.FindControl("repRecipientItem");

            if (orderList.Count == 0)
            {
                orderList = new List<OrderInfo>();
                orderList.Add(new OrderInfo());
            }
            repRecipientItem.DataSource = orderList;
            repRecipientItem.DataBind();
            //实发金额收货提醒
            Label lblLastNotify = (Label)e.Item.FindControl("lblLastNotify");
            if (int.Parse(drv.Row["Status"].ToString()) == (ESP.Purchase.Common.State.recipientstatus_Unsure) && lblLastNotify != null)
            {
                lblLastNotify.Text = "此次实发金额收货为本采购订单最后一次收货，最终金额以本次收货金额以及历史已完成收货实际金额为准（如有）";
            }

            //附加收货人
            Label lblRecipientAppend = (Label)e.Item.FindControl("lblRecipientAppend");
            lblRecipientAppend.Text = drv.Row["appendReceiverName"].ToString();

            Label labTotal = (Label)e.Item.FindControl("labTotal");
            labTotal.Text = totalPrice.ToString("#,##0.00");

            //供应商名称
            Label lblRecipientSupplier = (Label)e.Item.FindControl("lblRecipientSupplier");
            lblRecipientSupplier.Text = drv.Row["supplier_name"].ToString();

            //订单号
            Label labOrderNo = (Label)e.Item.FindControl("labOrderNo");
            labOrderNo.Text = drv.Row["orderid"].ToString();

            //服务项目
            Label labproject_name = (Label)e.Item.FindControl("labproject_name");
            labproject_name.Text = drv.Row["project_descripttion"].ToString();

            //项目号
            Label labproject_code = (Label)e.Item.FindControl("labproject_code");
            labproject_code.Text = drv.Row["project_code"].ToString();

            //负责人
            Label lblRecipientResponser = (Label)e.Item.FindControl("lblRecipientResponser");
            lblRecipientResponser.Text = drv.Row["receivername"].ToString();

            //业务组别
            Label lblRecipientDept = (Label)e.Item.FindControl("lblRecipientDept");
            lblRecipientDept.Text = drv.Row["ReceiverGroup"].ToString();

            //服务内容
            Label labproject_descripttion = (Label)e.Item.FindControl("labproject_descripttion");
            labproject_descripttion.Text = drv.Row["thirdParty_materielDesc"].ToString();

            //服务日期
            Label labintend_receipt_date = (Label)e.Item.FindControl("labintend_receipt_date");
            labintend_receipt_date.Text = drv.Row["RecipientDate"].ToString();

            //收货类型
            Label labRecipientType = (Label)e.Item.FindControl("labRecipientType");
            labRecipientType.Text = ESP.Purchase.Common.State.recipient_state[int.Parse(drv.Row["status"].ToString())];

            Label lblRecipientAmount = (Label)e.Item.FindControl("lblRecipientAmount");
            lblRecipientAmount.Text = decimal.Parse(drv.Row["RecipientAmount"].ToString()).ToString("#,##0.00");

            //日志
            List<RecipientLogInfo> loglist = RecipientLogManager.GetLoglistByRId(int.Parse(drv.Row["recipientId"].ToString()));
            string strlog = string.Empty;
            foreach (RecipientLogInfo log in loglist)
            {
                strlog += log.Des + "<br/>";
            }
            Label lblRecipientLog = (Label)e.Item.FindControl("lblRecipientLog");
            lblRecipientLog.Text = strlog;

            //历史收货
            Label labRecipientHist = (Label)e.Item.FindControl("labRecipientHist");
            List<RecipientInfo> recipientList = RecipientManager.getModelList(" and gid=" + drv.Row["Gid"].ToString(), new List<SqlParameter>());
            decimal histtotal = 0;
            foreach (RecipientInfo recipientModel in recipientList)
            {
                DateTime recipientDate;
                if (drv.Row["RecipientDate"] == DBNull.Value)
                {
                    recipientDate = DateTime.Now;
                }
                else
                {
                    recipientDate = Convert.ToDateTime(drv.Row["RecipientDate"]);
                }
                if (drv.Row["recipientId"].ToString() != recipientModel.Id.ToString() && recipientModel.RecipientDate < recipientDate)
                {
                    if (labRecipientHist.Text.Trim() != "")
                        labRecipientHist.Text += "<br />";
                    labRecipientHist.Text += recipientModel.RecipientName + "于" + recipientModel.RecipientDate + "收货" + recipientModel.RecipientAmount.ToString("#,##0.00");
                    histtotal += recipientModel.RecipientAmount;
                }
            }
            Label labHistTotal = (Label)e.Item.FindControl("labHistTotal");
            labHistTotal.Text = histtotal.ToString("#,##0.00");
            //收货明细
            //单价
            Label labSinglePrice = (Label)e.Item.FindControl("labSinglePrice");
            labSinglePrice.Text = drv.Row["SinglePrice"].ToString();

            //数量
            Label labNum = (Label)e.Item.FindControl("labNum");
            labNum.Text = drv.Row["Num"].ToString();

            //内容
            Label labDes = (Label)e.Item.FindControl("labDes");
            labDes.Text = drv.Row["Des"].ToString();

            //备注
            Label labNote = (Label)e.Item.FindControl("labNote");
            labNote.Text = drv.Row["Note"].ToString();

            //收货单号
            Label labRecipientNo = (Label)e.Item.FindControl("labRecipientNo");
            labRecipientNo.Text = drv.Row["RecipientNo"].ToString();

            if (listCount > 1 && e.Item.ItemIndex != (listCount - 1))
            {
                Label lblRecipientAfter = (Label)e.Item.FindControl("lblRecipientAfter");
                lblRecipientAfter.Text = "<p style=\"page-break-after:always\">&nbsp;</p>";
            }
        }
    }
    protected void repRecipientItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            OrderInfo orderModel = (OrderInfo)e.Item.DataItem;
            recipienttotalPrice += orderModel.total;
        }
    }
    #endregion
}
