using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using ESP.Purchase.DataAccess;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

public partial class Purchase_Requisition_EditRequisition : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            BindInfo();
            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                productInfo.ItemListBind(" general_id = " + Request[RequestName.GeneralID]);
                paymentInfo.TotalPrice = productInfo.getTotalPrice();
            }
        }
        productInfo.CurrentUser = CurrentUser;
    }

    public void BindInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        if (null != g)
        {
            genericInfo.Model = g;
            genericInfo.BindInfo();

            projectInfo.Model = g;
            projectInfo.BindInfo();

            supplierInfo.Model = g;
            supplierInfo.BindInfo();

            RequirementDescInfo.BindInfo(g);
            productInfo.Model = g;
            productInfo.BindInfo();

            paymentInfo.Model = g;
            if (g.source == "" || g.source == "Э�鹩Ӧ��")
            {
                paymentInfo.IsShowCommand = false;
                paymentInfo.IsShowGridView = false;
                btnEditSupplier.Visible = false;
            }

            if (g.fili_overrule != "")
            {
                palFili.Visible = true;
                labfilioverrule.Text = g.fili_overrule;
            }
            else if (g.requisition_overrule != "")
            {
                palOverrule.Visible = true;
                labOverrule.Text = g.requisition_overrule;
            }
            lablasttime.Text = g.lasttime.ToString();

            if (isSetAuditor(g))
            {
                btnAdd.Text = " ��һ�� ";
                btnSave.Visible = false;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('��ϵͳ�У��㲻�����κ�һ�����ţ�');window.location='RequisitionSaveList.aspx'", true);
            }
        }
    }

    public int SaveInfo(GeneralInfo g, bool bo)
    {
        g.id = generalid;

        genericInfo.Model = g;
        g = genericInfo.setModelInfo();

        productInfo.Model = g;
        g = productInfo.setModelInfo();

        RequirementDescInfo.setModelInfo(g);
        if (bo)
        {
            try
            {
                paymentInfo.AddPayment(false);
            }
            catch { }
            try
            {
                GeneralInfoDataProvider.Update(g);
                //��¼������־
                ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}�����ݽ��� {2} �Ĳ���", CurrentUser.Name, g.id, "�༭���뵥"), "���뵥");
                //������뵥ΪЭ�鹩Ӧ��ɾ������������Ϣ
                if ((g.status == State.requisition_save || g.status == State.requisition_commit || g.status == State.requisition_temporary_commit) && g.source == "Э�鹩Ӧ��")
                {
                    int periodsCount = PaymentPeriodManager.GetList(" gid = " + g.id).Tables[0].Rows.Count;
                    if (periodsCount == 0)
                    {
                        int supplierPeriod = 0;
                        //���Э�鹩Ӧ�̵�����
                        List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(g.id);
                        if (orderList != null && orderList.Count > 0)
                        {
                            foreach (OrderInfo order in orderList)
                            {
                                if (order.supplierId > 0)
                                {
                                    SupplierInfo supplier = SupplierManager.GetModel(order.supplierId);
                                    try
                                    {
                                        supplierPeriod = int.Parse(supplier.business_paytime);
                                    }
                                    catch { }
                                    break;
                                }
                            }
                        }

                        PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                        paymentPeriod.gid = g.id;
                        paymentPeriod.expectPaymentPrice = OrderInfoDataHelper.GetTotalPrice(generalid);
                        paymentPeriod.expectPaymentPercent = 100;
                        paymentPeriod.periodDay = State.supplierpayment[supplierPeriod].ToString();
                        paymentPeriod.beginDate = DateTime.Parse(DateTime.Now.AddMonths(2 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(14);
                        paymentPeriod.periodType = (int)State.PeriodType.period;
                        paymentPeriod.Status = State.PaymentStatus_save;
                        int paymentPeriodId = PaymentPeriodManager.Add(paymentPeriod);
                        //��¼������־
                        ESP.Logging.Logger.Add(string.Format("{0}��T_PaymentPeriod���е�id={1}�����ݽ��� {2} �Ĳ���", CurrentUser.Name, paymentPeriodId, "����Ĭ�ϸ�������"), "Ĭ�ϸ�������");
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        else
        {
            try
            {
                GeneralInfoDataProvider.Update(g);
                //��¼������־
                ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}�����ݽ��� {2} �Ĳ���", CurrentUser.Name, g.id, "�༭���뵥"), "���뵥");
                //������뵥ΪЭ�鹩Ӧ��ɾ������������Ϣ
                if ((g.status == State.requisition_save || g.status == State.requisition_commit || g.status == State.requisition_temporary_commit) && g.source == "Э�鹩Ӧ��")
                {
                    int periodsCount = PaymentPeriodManager.GetList(" gid = " + g.id).Tables[0].Rows.Count;
                    if (periodsCount == 0)
                    {
                        int supplierPeriod = 0;
                        //���Э�鹩Ӧ�̵�����
                        List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(g.id);
                        if (orderList != null && orderList.Count > 0)
                        {
                            foreach (OrderInfo order in orderList)
                            {
                                if (order.supplierId > 0)
                                {
                                    SupplierInfo supplier = SupplierManager.GetModel(order.supplierId);
                                    try
                                    {
                                        supplierPeriod = int.Parse(supplier.business_paytime);
                                    }
                                    catch { }
                                    break;
                                }
                            }
                        }

                        PaymentPeriodInfo paymentPeriod = new PaymentPeriodInfo();
                        paymentPeriod.gid = g.id;
                        paymentPeriod.expectPaymentPrice = OrderInfoDataHelper.GetTotalPrice(generalid);
                        paymentPeriod.expectPaymentPercent = 100;
                        paymentPeriod.periodDay = State.supplierpayment[supplierPeriod].ToString();
                        paymentPeriod.beginDate = DateTime.Parse(DateTime.Now.AddMonths(2 + supplierPeriod).ToString("yyyy-MM") + "-01").AddDays(14);
                        paymentPeriod.periodType = (int)State.PeriodType.period;
                        paymentPeriod.Status = State.PaymentStatus_save;
                        int paymentPeriodId = PaymentPeriodManager.Add(paymentPeriod);

                        //��¼������־
                        ESP.Logging.Logger.Add(string.Format("{0}��T_PaymentPeriod���е�id={1}�����ݽ��� {2} �Ĳ���", CurrentUser.Name, paymentPeriodId, "����Ĭ�ϸ�������"), "Ĭ�ϸ�������");
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRequisitionStep7.aspx?" + RequestName.GeneralID + "=" + generalid);
    }

    /// <summary>
    /// �Ƿ�ʹ������ҵ������˹���
    /// </summary>
    /// <param name="generalInfo"></param>
    /// <returns></returns>
    private bool isSetAuditor(GeneralInfo generalInfo)
    {
        //int[] depts = new ESP.Compatible.Employee(generalInfo.requestor).GetDepartmentIDs();
        //OperationAuditManageInfo manageModel = OperationAuditManageManager.GetModelByUserId(generalInfo.requestor);
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = null;

        if (generalInfo.Project_id != 0)
        {
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(generalInfo.Project_id);
        }
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(generalInfo.requestor); ;

        if (manageModel == null)
            manageModel = OperationAuditManageManager.GetModelByDepId(generalInfo.Departmentid);


        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["RunOperationAudit"] != null && ESP.Configuration.ConfigurationManager.SafeAppSettings["RunOperationAudit"] == "true" && manageModel != null)
        {
            return true;
        }
        return false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        paymentInfo.AddPayment(false);
        paymentInfo.DynamicPercent = 0;
        paymentInfo.ListBind();

        g.totalprice = productInfo.getTotalPrice();
        int retrunValue = SaveInfo(g, true);
        paymentInfo.Model = g;
        string Msg = paymentInfo.SetModel(g);
        if (Msg != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }

        Response.Redirect("SetOperationAudit.aspx?backUrl=EditRequisition.aspx&" + RequestName.GeneralID + "=" + generalid);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        int returnValue = SaveInfo(g, true);
        paymentInfo.Model = g;
        string Msg = paymentInfo.SetModel(g);
        if (Msg != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
            return;
        }
        if (returnValue == 1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('���ݱ���ɹ�!');window.location='EditRequisition.aspx?" + RequestName.GeneralID + "=" + generalid + "'", true);
        }
        else if (returnValue == 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('���ݱ���ʧ��!');", true);
        }
    }


    protected void BackUrl_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Purchase.Common.RequestName.BackUrl]))
        {
            Response.Redirect(Request[ESP.Purchase.Common.RequestName.BackUrl]);
        }
        else
            Response.Redirect("RequisitionSaveList.aspx");
    }

}
