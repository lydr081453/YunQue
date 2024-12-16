using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;


namespace PurchaseWeb.Purchase.Requisition
{
    public partial class FeedBack : ESP.Web.UI.PageBase
    {
        int generalid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                generalid = int.Parse(Request[RequestName.GeneralID]);
                productInfo.CurrentUserId = CurrentUserID;
                //productInfo.ItemListBind(" general_id = " + generalid);
               
            }
            BindInfo();

            tabOverrule.Visible = false;
            if (!string.IsNullOrEmpty(Request["type"]) && Request["type"].ToString().Equals("audit"))
            {
                projectInfo.IsEditPage = true;
                tabOverrule.Visible = true;
            }
            if (!string.IsNullOrEmpty(Request["type"]) && Request["type"].ToString().Equals("auditR"))
            {
                projectInfo.IsEditPage = true;
                tabOverrule.Visible = true;
            }

        }

        /// <summary>
        /// Binds the info.
        /// </summary>
        public void BindInfo()
        {
            GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            if (null != g)
            {
                GenericInfo.Model = g;
                GenericInfo.BindInfo();

                projectInfo.Model = g;
                projectInfo.BindInfo();

                supplierInfo.Model = g;
                supplierInfo.BindInfo();

                RequirementDescInfo.BindInfo(g);
                labdownContrast.Text = g.contrastFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + g.id.ToString() + "&Index=0&Type=Contrast'><img src='/images/ico_04.gif' border='0' /></a>";
                labdownConsult.Text = g.consultFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + g.id.ToString() + "&Index=0&Type=Consult'><img src='/images/ico_04.gif' border='0' /></a>";
                txtorderid.Text = g.orderid;
                txttype.Text = g.type;
                txtcontrast.Text = g.contrast;
                txtconsult.Text = g.consult;

                if (g.PRType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && g.PRType != (int)ESP.Purchase.Common.PRTYpe.MPPR && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA && g.PRType != (int)ESP.Purchase.Common.PRTYpe.ADPR)
                {
                    ddlfirst_assessor.Text = g.first_assessorname;
                    ddlfirst_assessor.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(g.first_assessor) + "');";
                }

                labafterwards.Text = g.afterwardsname;
                if (g.afterwardsname == "是")
                    labafterwardsReason.Text = "理由：" + g.afterwardsReason;
                labEmBuy.Text = g.EmBuy;
                if (g.EmBuy == "是")
                    labEmBuyReason.Text = "理由：" + g.EmBuyReason;
                labCusAsk.Text = g.CusAsk;
                if (g.CusAsk == "是")
                {
                    labCusName.Text = "客户名称：" + g.CusName;
                    labCusAskYesReason.Text = "理  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;由：" + g.CusAskYesReason;
                }

                txtothers.Text = g.others;
                labContractNo.Text = g.ContractNo;

                productInfo.Model = g;
                
                productInfo.BindInfo();
                paymentInfo.Model = g;
                paymentInfo.BindInfo();
                paymentInfo.TotalPrice = productInfo.TotalPrice;
                if (g.fili_overrule != "")
                {
                    palFili.Visible = true;
                    labFili.Text = g.fili_overrule;
                }
                if (g.order_overrule != "")
                {
                    palOverrule.Visible = true;
                    labOverrule.Text = g.order_overrule;
                }
                if (g.requisition_overrule != "")
                {
                    palOverrulP.Visible = true;
                    labOverruleP.Text = g.requisition_overrule;
                }
                lablasttime.Text = g.lasttime.ToString();
                labrequisition_committime.Text = g.requisition_committime.ToString() == State.datetime_minvalue ? "" : g.requisition_committime.ToString();
                laborder_committime.Text = g.order_committime.ToString() == State.datetime_minvalue ? "" : g.order_committime.ToString();
                laborder_audittime.Text = g.order_audittime.ToString() == State.datetime_minvalue ? "" : g.order_audittime.ToString();

                if (g.PRType == (int)PRTYpe.MediaPR && g.status == State.order_mediaAuditYes)
                {
                    tabMedia.Visible = true;
                    litprMediaRemark.Text = g.prMediaRemark;
                }
                List<SqlParameter> parms = new List<SqlParameter>();
                string strWhere = " and (a.isconfirm > 0)";

                strWhere += " and a.Gid = @Gid";
                parms.Add(new SqlParameter("@Gid", g.id));

                DataSet ds = RecipientManager.GetRecipientList(strWhere, parms);
                gvSupplier.DataSource = ds;
                gvSupplier.DataBind();


                List<SqlParameter> parms1 = new List<SqlParameter>();
                string strWhere1 = " and (a.isconfirm = 0 or a.isconfirm is null or a.isconfirm = " + State.recipentConfirm_Emp1 + " or ( b.appendReceiver is not null and b.appendReceiver>0 and a.isconfirm=" + State.recipentConfirm_Emp2 + " and b.source='协议供应商'))";


                strWhere1 += " and a.Gid = @Gid";
                parms1.Add(new SqlParameter("@Gid", g.id));

                DataSet ds1 = RecipientManager.GetRecipientList(strWhere1, parms1);
                gvUnConfirmRecipient.DataSource = ds1;
                gvUnConfirmRecipient.DataBind();


                string strWhere2 = string.Format(" and (a.status = {0} or a.status = {1} ) ", State.PaymentStatus_commit, State.PaymentStatus_over);
                List<SqlParameter> parms2 = new List<SqlParameter>();

                strWhere2 += string.Format(" and b.id = '{0}' ", g.id.ToString());

                DataSet ds2 = PaymentPeriodManager.GetGeneralPaymentList(strWhere2, parms2);
                gvPayment.DataSource = ds2;
                gvPayment.DataBind();

                Page.SetFocus(btnSave.ClientID);
            }
        }

        protected void btnSave_Click(Object sender, EventArgs e)
        {
            BlackDetailInfo model = BlackDetailManager.GetModel(int.Parse(Request["BlackDetailId"]));
            model.ResponseContent = txtContent.Text.Trim();
            model.ResponseTime = DateTime.Now;
            if (BlackDetailManager.Update(model) > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交成功！');window.close();", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交失败！');", true);
            }
        }

        protected void btnOrderExport_Click(object sender, EventArgs e)
        {
            ExportToOrderInfoExcel(generalid);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToGeneralInfoExcel(generalid);

        }

        protected void ExportToOrderInfoExcel(int id)
        {
            FileHelper.ToOrderInfoExcel(id, Server.MapPath("~"), Response);
            GC.Collect();
        }

        protected void ExportToGeneralInfoExcel(int id)
        {
            FileHelper.ToGeneralInfoExcel(id, Server.MapPath("~"), Response);
            GC.Collect();
        }
        protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = (DataRowView)e.Row.DataItem;
            }
        }

        protected void gvUnConfirmRecipient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = (DataRowView)e.Row.DataItem;
            }
        }

        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        public string GetFormatPrice(decimal price)
        {
            return price.ToString("#,##0.00");
        }
    }
}
