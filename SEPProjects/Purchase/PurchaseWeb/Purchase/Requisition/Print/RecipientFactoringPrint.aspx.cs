using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

namespace PurchaseWeb.Purchase.Requisition.Print
{
    public partial class RecipientFactoringPrint : ESP.Web.UI.PageBase
    {

        string id = "";
        int num = 1;
        string moneytype = "";
        int listCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    id = Request["id"].ToString();
                    DataSet ds = null;
                    if (string.IsNullOrEmpty(Request["newPrint"]))
                    {
                        ds = RecipientManager.GetRecipientList(" and a.GId in (" + id + ")", new List<SqlParameter>());
                        //添加操作日志
                        //ESP.Logging.Logger.Add(string.Format("查看/打印 PR流水号为 {0} 媒介收货单", id.ToString()));
                    }
                    else
                    {
                        ds = RecipientManager.GetRecipientList(" and a.id in (" + id + ")", new List<SqlParameter>());
                        //添加操作日志
                        //ESP.Logging.Logger.Add(string.Format("查看/打印 收货单号为 {0} 媒介收货单", id.ToString()));
                    }
                    listCount = ds.Tables[0].Rows.Count;
                    repList.DataSource = ds.Tables[0];
                    repList.DataBind();
                }
                if (!string.IsNullOrEmpty(Request["viewbutton"]) && Request["viewbutton"] == "no")
                {
                    btnPrint.Visible = false;
                    btnClose.Visible = false;
                }
            }
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                totalPrice = 0;

                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(int.Parse(drv.Row["Gid"].ToString()));

                ESP.Finance.Entity.BankInfo bankModel = null;

                //item列表项
                List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + drv.Row["Gid"].ToString());

                moneytype = drv.Row["moneytype"].ToString();
                Repeater repItem = (Repeater)e.Item.FindControl("repItem");

                if (orderList.Count == 0)
                {
                    orderList = new List<OrderInfo>();
                    orderList.Add(new OrderInfo());
                }
                repItem.DataSource = orderList;
                repItem.DataBind();
                //实发金额收货提醒
                Label lblLastNotify = (Label)e.Item.FindControl("lblLastNotify");
                if (int.Parse(drv.Row["Status"].ToString()) == (ESP.Purchase.Common.State.recipientstatus_Unsure) && lblLastNotify != null)
                {
                    lblLastNotify.Text = "此次实发金额收货为本采购订单最后一次收货，最终金额以本次收货金额以及历史已完成收货实际金额为准（如有）";
                }

                //附加收货人
                Label lab_AppendUser = (Label)e.Item.FindControl("lab_AppendUser");
                lab_AppendUser.Text = drv.Row["appendReceiverName"].ToString();

                Label labTotal = (Label)e.Item.FindControl("labTotal");
                labTotal.Text = totalPrice.ToString("#,##0.####");

                //供应商名称
                Label lab_supplier_name = (Label)e.Item.FindControl("lab_supplier_name");
                if (generalModel.IsFactoring > 0)
                {
                    bankModel = ESP.Finance.BusinessLogic.BankManager.GetModel(generalModel.IsFactoring);
                    lab_supplier_name.Text = bankModel.BankAccountName;
                }
                else
                {
                    lab_supplier_name.Text = drv.Row["supplier_name"].ToString();
                }



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
                Label lab_endusername = (Label)e.Item.FindControl("lab_endusername");
                lab_endusername.Text = drv.Row["receivername"].ToString();

                //业务组别
                Label lab_requestor_group = (Label)e.Item.FindControl("lab_requestor_group");
                lab_requestor_group.Text = drv.Row["ReceiverGroup"].ToString();

                //服务内容
                Label labproject_descripttion = (Label)e.Item.FindControl("labproject_descripttion");
                labproject_descripttion.Text = drv.Row["thirdParty_materielDesc"].ToString();

                //服务日期
                Label labintend_receipt_date = (Label)e.Item.FindControl("labintend_receipt_date");
                labintend_receipt_date.Text = drv.Row["RecipientDate"].ToString();

                //收货类型
                Label labRecipientType = (Label)e.Item.FindControl("labRecipientType");
                labRecipientType.Text = ESP.Purchase.Common.State.recipient_state[int.Parse(drv.Row["status"].ToString())];

                Label lab_moneytype = (Label)e.Item.FindControl("lab_moneytype");
                lab_moneytype.Text = decimal.Parse(drv.Row["RecipientAmount"].ToString()).ToString("#,##0.####");

                //日志
                List<RecipientLogInfo> loglist = RecipientLogManager.GetLoglistByRId(int.Parse(drv.Row["recipientId"].ToString()));
                string strlog = string.Empty;
                foreach (RecipientLogInfo log in loglist)
                {
                    if (bankModel != null)
                        strlog += log.Des.Replace(generalModel.supplier_name, bankModel.BankAccountName) + "<br/>";
                    else
                        strlog += log.Des + "<br/>";
                }
                Label lablog = (Label)e.Item.FindControl("lablog");
                lablog.Text = strlog;

                //历史收货
                Label labRecipientLog = (Label)e.Item.FindControl("labRecipientLog");
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
                        if (labRecipientLog.Text.Trim() != "")
                            labRecipientLog.Text += "<br />";
                        labRecipientLog.Text += recipientModel.RecipientName + "于" + recipientModel.RecipientDate + "收货" + recipientModel.RecipientAmount.ToString("#,##0.####");
                        histtotal += recipientModel.RecipientAmount;
                    }
                }
                Label labHistTotal = (Label)e.Item.FindControl("labHistTotal");
                labHistTotal.Text = histtotal.ToString("#,##0.####");
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
                    Label labafter = (Label)e.Item.FindControl("labafter");
                    labafter.Text = "<p style=\"page-break-after:always\">&nbsp;</p>";
                }
            }
        }

        static decimal totalPrice = 0;
        protected void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                OrderInfo orderModel = (OrderInfo)e.Item.DataItem;
                totalPrice += orderModel.total;
            }
        }
    }
}