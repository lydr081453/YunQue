using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_OperationAuditList : ESP.Web.UI.PageBase
{
    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
        #endregion
        if (!IsPostBack)
        {
            TypeBind();
            ListBind();
        }
    }

    void BindTree(TreeNodeCollection nds, int parentId, List<TypeInfo> items)
    {
        TreeNode tn = null;
        foreach (TypeInfo model in items)
        {
            tn = new TreeNode(model.typename);
            nds.Add(tn);
            BindTree(tn.ChildNodes, model.typeid, TypeManager.GetListByParentId(model.typeid));
        }
    }

    private void TypeBind()
    {

    }

    private void ListBind()
    {
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID,"PR单");
        List<GeneralInfo> list = new List<GeneralInfo>();
        List<WorkFlowModel.WorkItemData> list2 = null;
        //取得授权审核人的数据 begin
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo backUp in delegates)
        {
            list2 = workitemdata.getProcessDataList(backUp.UserID.ToString(), "PR单");
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                GeneralInfo model = (GeneralInfo)o.ItemData;
                model.WorkitemID = o.WorkItemID;
                model.InstanceID = o.InstanceID;
                model.WorkItemName = o.WorkItemName;
                model.ProcessID = o.ProcessID;

                list.Add(model);
            }
        }
        //取得授权审核人的数据 end
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            GeneralInfo model = (GeneralInfo)o.ItemData;
            model.WorkitemID = o.WorkItemID;
            model.InstanceID = o.InstanceID;
            model.WorkItemName = o.WorkItemName;
            model.ProcessID = o.ProcessID;

            list.Add(model);
        }
        if (txtGlideNo.Text.Trim() != "" && txtRequestor.Text.Trim() != "")
        {
            list = list.FindAll(delegate(GeneralInfo a) { return (a.id.ToString() == txtGlideNo.Text.TrimStart('0') && a.requestorname.Contains(txtRequestor.Text.Trim())); });
        }
        else if (txtGlideNo.Text.Trim() != "" && txtRequestor.Text.Trim() == "")
        {
            list = list.FindAll(delegate(GeneralInfo a) { return a.id.ToString() == txtGlideNo.Text.TrimStart('0'); });
        }
        else if (txtGlideNo.Text.Trim() == "" && txtRequestor.Text.Trim() != "")
        {
            list = list.FindAll(delegate(GeneralInfo a) { return a.requestorname.Contains(txtRequestor.Text.Trim()); });
        }
        list.Sort(new ESP.Purchase.BusinessLogic.GeneralInfoComparer());
        gvG.DataSource = list;
        gvG.DataBind();

        //gvLOG.DataSource = OperationAuditLogManager.GetAuditLog(int.Parse(CurrentUser.SysID));
        //gvLOG.DataBind();
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            HyperLink hypAuditurl = (HyperLink)e.Row.FindControl("hypAuditurl");
            if (null != hypAuditurl)
            {
                hypAuditurl.NavigateUrl = "OperationAudit.aspx?backUrl=OperationAuditList.aspx&" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString();// +"&workitemid=" + model.WorkitemID + "&workitemname=" + Server.UrlEncode(model.WorkItemName) + "&instanceid=" + model.InstanceID + "&processid=" + model.ProcessID;
                hypAuditurl.Text = model.PrNo;
            }
            HyperLink hypAuditurl1 = (HyperLink)e.Row.FindControl("hypAuditurl1");
            if (null != hypAuditurl1)
            {
                hypAuditurl1.NavigateUrl = "OperationAudit.aspx?backUrl=OperationAuditList.aspx&" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString();// +"&workitemid=" + model.WorkitemID + "&workitemname=" + Server.UrlEncode(model.WorkItemName) + "&instanceid=" + model.InstanceID + "&processid=" + model.ProcessID;
            }
            ESP.Purchase.Entity.GeneralInfo currentModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.id);
            if (currentModel != null && currentModel.InUse != (int)State.PRInUse.Use)
                hypAuditurl1.Visible = false;

            Repeater repProduct = (Repeater)e.Row.FindControl("repProduct");
            List<OrderInfo> list = OrderInfoManager.GetListByGeneralId(((GeneralInfo)e.Row.DataItem).id);
            string mtype = "";
            if (gvG.DataKeys[e.Row.RowIndex].Values[1].ToString() == "美元")
                mtype = "＄";
            else
                mtype = "￥";

            for (int i = 0; i < list.Count; i++)
            {
                list[i].moneytype = mtype + list[i].total.ToString("#,##0.####");
            }
            repProduct.DataSource = list;
            repProduct.DataBind();

            Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
            if (null != labRequisitionflow && labRequisitionflow.Text != "")
            {
                labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
            }
              //对私3000以上和媒体3000以上，不显示初审人
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
            {
                e.Row.Cells[7].Controls.Clear();
                e.Row.Cells[7].Text = "";
            }
            //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                if (typeoperationflow == State.typeoperationflow_MP)
                {
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[7].Text = "";
                }
            }
        }
    }

    protected void gvLOG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Data.DataRowView dv = (System.Data.DataRowView)e.Row.DataItem;
            HyperLink hypAuditurl = (HyperLink)e.Row.FindControl("hypView");
            hypAuditurl.NavigateUrl = "ShowRequisitionDetail.aspx?" + RequestName.GeneralID + "=" + dv["gid"] + "&pageUrl=OperationAuditList.aspx&helpfile=3";
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvG.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvG.PageIndex + 1) > gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvG.PageIndex - 1) < 0 ? 0 : (gvG.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvG_PageIndexChanging(new object(), e);
    }


    //protected void ExportToOrderInfoExcel(int id)
    //{
    //    FileHelper.ToOrderInfoExcel(id, Server.MapPath("~"), Response);
    //    GC.Collect();
    //}

    //protected void ExportToGeneralInfoExcel(int id)
    //{
    //    FileHelper.ToGeneralInfoExcel(id, Server.MapPath("~"), Response);
    //    GC.Collect();
    //}
}


