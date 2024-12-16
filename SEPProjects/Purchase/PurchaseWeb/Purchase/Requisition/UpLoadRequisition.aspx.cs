using System;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_UpLoadRequisition : ESP.Web.UI.PageBase
{
    /// <summary>
    /// 页面装载
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">事件对象</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Handles the Click event of the btnUpload control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (txtId.Text != "")
        {
            List<GeneralInfo> listG = GeneralInfoManager.GetStatusList(" and a.id=" + txtId.Text.TrimStart('0'));
            GeneralInfo model;
            if(listG.Count>0)
                model = listG[0];
            else
                model = new GeneralInfo();
            if(model.id>0)
            {
                try
                {
                    if (model.Project_id <= 0 || model.Departmentid <= 0)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('您选择的申请单数据不完整，不能进行导入!');", true);
                        return;
                    }
                    //申请人
                    model.requestor = int.Parse(CurrentUser.SysID);
                    model.requestorname = CurrentUser.Name;
                    //申请时间
                    model.app_date = DateTime.Now;
                    //申请人业务组
                    model.requestor_group = CurrentUser.GetDepartmentNames().Count == 0 ? "" : CurrentUser.GetDepartmentNames()[0].ToString();
                    //申请人联络方式
                    model.requestor_info = CurrentUser.Telephone;
                    model.PrNo = "";
                    model.orderid = "";
                    model.requisition_committime = DateTime.Now;
                    model.status = State.requisition_save;
                    model.requisition_overrule = "";
                    model.order_overrule = "";
                    model.lasttime = DateTime.Now;
                    model.order_committime = DateTime.Now;
                    model.EmBuy = "否";
                    model.EmBuyReason = "";
                    model.CusAsk = "否";
                    model.CusName = "";
                    model.CusAskYesReason = "";
                    model.afterwardsReason = "";
                    model.Requisitionflow = State.requisitionflow_toO;
                    model.order_audittime = DateTime.Now;

                    #region 采购物品信息

                    List<OrderInfo> items = OrderInfoManager.GetListByGeneralId(model.id);
                    #endregion

                    int gid = GeneralInfoManager.Add(model, 0, "");
                    if(gid >0)
                    {
                        foreach (OrderInfo o in items)
                        {
                            o.general_id = gid;
                            OrderInfoManager.Add(o, 0, "");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('导入失败!');", true);
                    }
                    ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "window.location='EditRequisition.aspx?" + RequestName.GeneralID + "=" + gid + "';alert('导入成功!');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('导入失败!');", true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('导入失败,流水号错误!');", true);
            }
        }
    }
}