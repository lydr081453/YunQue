using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ChangeAuditUser : ESP.Web.UI.PageBase
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
        if(!IsPostBack)
            BindInfo();
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

            paymentInfo.Model = g;
            paymentInfo.TotalPrice = g.totalprice;

            txtorderid.Text = g.orderid;
            txttype.Text = g.type;
            txtcontrast.Text = g.contrast;
            txtconsult.Text = g.consult;
            txtfirst_assessor.Text = g.first_assessorname;
            labafterwards.Text = g.afterwardsname;
            labEmBuy.Text = g.EmBuy;
            labCusAsk.Text = g.CusAsk;
            if (g.CusAsk == "是")
                labCusName.Text = "客户名称:" + g.CusName;
            txtothers.Text = g.others;
            labContractNo.Text = g.ContractNo;

            productInfo.Model = g;
            
            productInfo.BindInfo();
            lablasttime.Text = g.lasttime.ToString();
            labrequisition_committime.Text = g.requisition_committime.ToString() == State.datetime_minvalue ? "" : g.requisition_committime.ToString();
            laborder_committime.Text = g.order_committime.ToString() == State.datetime_minvalue ? "" : g.order_committime.ToString();
            laborder_audittime.Text = g.order_audittime.ToString() == State.datetime_minvalue ? "" : g.order_audittime.ToString();
        }
    }

    /// <summary>
    /// Handles the Click event of the btn control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btn_Click(Object sender, EventArgs e)
    {
        Response.Redirect(Request["backUrl"] == null ? "OrderList.aspx" : Request["backUrl"].ToString());
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hidfirst_assessor.Value))
        {
            GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            g.first_assessor = int.Parse(hidfirst_assessor.Value.Split('-')[0]);
            g.first_assessorname = hidfirst_assessor.Value.Split('-')[1];


            LogInfo log = new LogInfo();
            log.Gid = g.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_changecheker, CurrentUserName + "(" + CurrentUser.ITCode + ")", g.first_assessorname,
                                    DateTime.Now.ToString());
            LogManager.AddLog(log, Request);

            GeneralInfoDataProvider.Update(g);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "变更初审人"), "申请单");
            string Msg = "您提交的流水号：" + g.glideno + "  申请单号：" + g.PrNo + "  的申请单初审人变更为：" + g.first_assessorname;
            ESP.ConfigCommon.SendMail.Send1("变更初审人", State.getEmployeeEmailBySysUserId(g.requestor), Msg, true, "");
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='EditOrder.aspx?" + RequestName.GeneralID + "=" + generalid.ToString() + "';alert('变更初审人成功！');", true);
    }

}
