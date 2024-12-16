using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_BreakupDetail : ESP.Web.UI.PageBase
{
    int generalid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            productInfo.CurrentUserId = CurrentUserID;
        }
        if (!IsPostBack)
        {
            BindInfo();
           // productInfo.ItemListBind(" general_id = " + generalid);
            BindgvFPList();
        }
    }

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
            
            paymentInfo.Model = g;
            paymentInfo.BindInfo();

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
            labEmBuy.Text = g.EmBuy;
            labCusAsk.Text = g.CusAsk;
            if (g.CusAsk == "是")
                labCusName.Text = "客户名称:" + g.CusName;
            txtothers.Text = g.others;
            labContractNo.Text = g.ContractNo;

            productInfo.Model = g;
            
            productInfo.BindInfo();

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
        }
    }

    protected void btn_Click(Object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"] == null ? "OrderList.aspx" : Request["backUrl"].ToString());
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

    private void BindgvFPList()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@Gid", generalid));
        List<RecipientInfo> items = RecipientManager.getModelList(" and Gid=@Gid", parms);
        gvFP.DataSource = items;
        gvFP.DataBind();
    }

    protected void gvFP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labStatus = (Label)e.Row.FindControl("labStatus");
            labStatus.Text = State.recipient_state[int.Parse(labStatus.Text)].ToString();
        }
    }
}
