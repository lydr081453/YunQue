using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using System.Collections.Generic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

public partial class Purchase_Requisition_AddRequisitionStep5 : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;
    string query = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            productInfo.CurrentUserId = CurrentUserID;
            //productInfo.ItemListBind(" general_id = " + generalid);
           
        }
        if (!IsPostBack)
        {
            BindInfo();
        }
        query = Request.Url.Query;

    }

    /// <summary>
    /// 是否使用设置业务审核人功能
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

        if (manageModel==null)
            manageModel = OperationAuditManageManager.GetModelByDepId(generalInfo.Departmentid);

        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["RunOperationAudit"] != null && ESP.Configuration.ConfigurationManager.SafeAppSettings["RunOperationAudit"] == "true" && manageModel != null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Binds the info.
    /// </summary>
    public void BindInfo()
    {
        if (generalid > 0)
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

                productInfo.Model = g;
                productInfo.BindInfo();

                paymentInfo.Model = g;
                paymentInfo.TotalPrice = productInfo.TotalPrice;
                paymentInfo.BindInfo();

                RequirementDescInfo.BindInfo(g);
                if (isSetAuditor(g))
                {
                    btnCommit.Value = " 下一步 ";
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('在系统中，你不属于任何一个部门！');window.location='RequisitionSaveList.aspx'", true);
                }
            }
        }
    }

    /// <summary>
    /// Saves the info.
    /// </summary>
    /// <param name="g">The g.</param>
    /// <returns></returns>
    public int SaveInfo(GeneralInfo g)
    {
        int ret = 0;
        RequirementDescInfo.setModelInfo(g);
        g.Addstatus = 7;
        //对私有发票，按对公处理
        if (g.OperationType == 1 && g.HaveInvoice == true)
        {
            g.PRType = 0;
        }
        if (g.OperationType == 1 && g.HaveInvoice == false)
        {
            g.PRType = 6;
        }

        //if (Check(g))
        //{
            if (generalid == 0)
            {
                ret = GeneralInfoDataProvider.Add(g);
                if (ret > 0)
                    // updateBillStatus();
                    return ret;
            }
            else
            {
                try
                {
                    GeneralInfoDataProvider.Update(g);
                    //如果申请单为协议供应商删除付款账期信息
                    if ((g.status == State.requisition_save || g.status == State.requisition_commit || g.status == State.requisition_temporary_commit) && g.source == "协议供应商")
                    {
                        int periodsCount = PaymentPeriodManager.GetList(" gid = " + g.id).Tables[0].Rows.Count;
                        if (periodsCount == 0)
                        {
                            int supplierPeriod = 0;
                            //获得协议供应商的账期
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
                            PaymentPeriodManager.Add(paymentPeriod);
                        }
                    }
                    return 1;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        //}
        //else
        //{
        //    if (g.PRType != 1 && g.PRType != 7)
        //        return 3;
        //}
        return 2;
    }

    /// <summary>
    /// 检查采购物品的物料类别是否一致
    /// </summary>
    private bool Check(GeneralInfo generalInfo)
    {
        if (string.IsNullOrEmpty(generalInfo.supplier_address) || string.IsNullOrEmpty(generalInfo.account_name) || string.IsNullOrEmpty(generalInfo.account_number) || string.IsNullOrEmpty(generalInfo.account_bank) || string.IsNullOrEmpty(generalInfo.supplier_email) || (generalInfo.source == "客户指定" && string.IsNullOrEmpty(generalInfo.CusAskEmailFile)))
            return false;
        else
            return true;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        g.totalprice = productInfo.getTotalPrice();
        int ret = SaveInfo(g);
        if (ret == 3)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('供应商信息维护不完整,请检查供应商信息!');", true);
            return;
        }
        query = query.AddParam(RequestName.GeneralID, generalid);
        Response.Redirect("SetOperationAudit.aspx?backUrl=AddRequisitionStep5.aspx&" + query);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        GeneralInfo g = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
        int returnValue = SaveInfo(g);
        if (returnValue == 1)
        {
            //记录操作日志
            //  ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第五步保存并返回列表"), "创建采购申请单");

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='RequisitionSaveList.aspx'", true);
        }
        else if (returnValue == 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        GeneralInfo g = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
        g.Addstatus = 5;
        try
        {
            GeneralInfoDataProvider.Update(g);

            //记录操作日志
            //    ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第五步跳转第四步"), "创建采购申请单");
        }
        catch (Exception ex)
        {
            //
        }
        finally
        {
            query = query.AddParam(RequestName.GeneralID, generalid);
            if (query.Length > 0 && query.Substring(0, 1).Equals("&"))
            {
                query = query.Substring(1, query.Length - 1);
            }
            Response.Redirect("AddRequisitionStep7.aspx?" + query);
        }
    }

    int num = 1;
    protected void gdvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderInfo ordermodel = OrderInfoManager.GetModelByGeneralID(Convert.ToInt32(Request[RequestName.GeneralID]));

            if (!string.IsNullOrEmpty(Request[RequestName.BillID]) || (ordermodel != null && ordermodel.BillID > 0))//针对媒介对私的报销单
            {
                e.Row.Cells[0].Text = num.ToString();
                num++;
                Label labDown = (Label)e.Row.FindControl("labDown");
                labDown.Text = "<a target='_blank' href='" + labDown.Text + "'><img src='/images/ico_04.gif' border='0' /></a>";
                try
                {
                    if (ordermodel != null)
                    {
                        e.Row.Cells[2].Text = ordermodel.producttypename;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                e.Row.Cells[0].Text = num.ToString();
                num++;
                Label labDown = (Label)e.Row.FindControl("labDown");
                labDown.Text = labDown.Text == "" ? "" : "<a target='_blank' href='../../" + labDown.Text + "'><img src='/images/ico_04.gif' border='0' /></a>";
                DataRowView dr = (DataRowView)e.Row.DataItem;
                int productTypeID = int.Parse(dr["productType"].ToString());
                TypeInfo ty = TypeManager.GetModel(productTypeID);

                e.Row.Cells[2].Text = ty == null ? "" : TypeManager.GetModel(productTypeID).typename;
            }
        }
    }
}
