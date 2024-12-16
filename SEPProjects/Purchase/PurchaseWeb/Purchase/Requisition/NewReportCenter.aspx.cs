using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class NewReportCenter : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string where = " DirectorId = " + CurrentUser.SysID + " or ManagerId=" + CurrentUser.SysID + " or CEOId=" + CurrentUser.SysID;


                txtBegin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd").Remove(8) + "25";
                txtEnd.Text = DateTime.Now.ToString("yyyy-MM-dd").Remove(8) + "25";
                BindDepartList();
                ListBind();
            }
        }

        protected void ddlDepart1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartList();
        }

        protected void ddlDepart2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartList();
        }

        protected void ddlDepart3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartList();
        }

        bool isBJPurchaseUser = false;
        bool isCQPurchaseUser = false;

        private bool isPurchaseUser()
        {
            bool isPurchaseUser = false;
            foreach (int departmentId in CurrentUser.GetDepartmentIDs())
            {
                if (departmentId == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["StockDeparmentUniqID"].ToString()))
                {
                    isBJPurchaseUser = true;
                    isPurchaseUser = true;
                    break;
                }
                if (departmentId == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["CQStockDeparmentUniqID"].ToString()))
                {
                    isCQPurchaseUser = true;
                    isPurchaseUser = true;
                    break;
                }
            }
            return isPurchaseUser;
        }

        private void BindDepartList()
        {

            string selectedValue1 = string.IsNullOrEmpty(ddlDepart1.SelectedValue) ? "0" : ddlDepart1.SelectedValue;
            string selectedValue2 = string.IsNullOrEmpty(ddlDepart2.SelectedValue) ? "0" : ddlDepart2.SelectedValue;
            string selectedValue3 = string.IsNullOrEmpty(ddlDepart3.SelectedValue) ? "0" : ddlDepart3.SelectedValue;

            ddlDepart1.Items.Clear();
            ddlDepart2.Items.Clear();
            ddlDepart3.Items.Clear();

            if (!isPurchaseUser())
            {
                #region 非采购部人员

                ddlDepart1.Items.Insert(0, new ListItem("全部", "0"));
                ddlDepart2.Items.Insert(0, new ListItem("全部", "0"));
                ddlDepart3.Items.Insert(0, new ListItem("全部", "0"));

                string where = " DirectorId = " + CurrentUser.SysID + " or ManagerId=" + CurrentUser.SysID + " or CEOId=" + CurrentUser.SysID;
                DataTable dt = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(where).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该页面的访问权限！');window.location.href='/default.aspx';", true);
                }

                List<ESP.Framework.Entity.DepartmentInfo> depart1 = new List<ESP.Framework.Entity.DepartmentInfo>();
                List<ESP.Framework.Entity.DepartmentInfo> depart2 = new List<ESP.Framework.Entity.DepartmentInfo>();
                List<ESP.Framework.Entity.DepartmentInfo> depart3 = new List<ESP.Framework.Entity.DepartmentInfo>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string depId = dt.Rows[i]["depId"].ToString();
                    ESP.Framework.Entity.DepartmentInfo dep3Model = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(depId));
                    if (depart3.Find(n => n.DepartmentID == dep3Model.DepartmentID) == null)
                    {
                        ESP.Framework.Entity.DepartmentInfo dep2Model = ESP.Framework.BusinessLogic.DepartmentManager.Get(dep3Model.ParentID);
                        ESP.Framework.Entity.DepartmentInfo dep1Model = ESP.Framework.BusinessLogic.DepartmentManager.Get(dep2Model.ParentID);
                        if (!dep3Model.DepartmentName.Contains("作废"))
                            depart3.Add(dep3Model);
                        if (depart2.Find(n => n.DepartmentID == dep2Model.DepartmentID) == null && !dep2Model.DepartmentName.Contains("作废"))
                            depart2.Add(dep2Model);
                        if (depart1.Find(n => n.DepartmentID == dep1Model.DepartmentID) == null && !dep1Model.DepartmentName.Contains("作废"))
                            depart1.Add(dep1Model);
                    }

                }

                foreach (var o in depart1)
                {
                    ddlDepart1.Items.Add(new ListItem(o.DepartmentName, o.DepartmentID.ToString()));
                }
                try
                {
                    ddlDepart1.SelectedValue = selectedValue1;
                }
                catch
                {
                    ddlDepart1.SelectedValue = "0";
                }
                foreach (var o in depart2.FindAll(n => n.ParentID == int.Parse(ddlDepart1.SelectedValue)))
                {
                    ddlDepart2.Items.Add(new ListItem(o.DepartmentName, o.DepartmentID.ToString()));
                }
                try
                {
                    ddlDepart2.SelectedValue = selectedValue2;
                }
                catch
                {
                    ddlDepart2.SelectedValue = "0";
                }
                foreach (var o in depart3.FindAll(n => n.ParentID == int.Parse(ddlDepart2.SelectedValue)))
                {
                    ddlDepart3.Items.Add(new ListItem(o.DepartmentName, o.DepartmentID.ToString()));
                }
                try
                {
                    ddlDepart3.SelectedValue = selectedValue3;
                }
                catch
                {
                    ddlDepart3.SelectedValue = "0";
                }
                #endregion
            }
            else
            {

                List<ESP.Compatible.Department> depart1List = ESP.Compatible.DepartmentManager.GetByParent(0).FindAll(n=>!n.NodeName.Contains("作废"));
                if (isCQPurchaseUser)
                {
                    depart1List = depart1List.FindAll(n => n.UniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["CQStockDeparmentUniqID"].ToString()));
                }
               
                ddlDepart1.DataSource = depart1List;
                ddlDepart1.DataTextField = "NodeName";
                ddlDepart1.DataValueField = "Uniqid";
                ddlDepart1.DataBind();
                ddlDepart1.Items.Insert(0, new ListItem("全部", "0"));
                try
                {
                    ddlDepart1.SelectedValue = selectedValue1;
                }
                catch { }
                List<ESP.Compatible.Department> depart2List = ESP.Compatible.DepartmentManager.GetByParent(int.Parse(ddlDepart1.SelectedValue == "0" ? "-1" : ddlDepart1.SelectedValue)).FindAll(n => !n.NodeName.Contains("作废")); 
                ddlDepart2.DataSource = depart2List;
                ddlDepart2.DataTextField = "NodeName";
                ddlDepart2.DataValueField = "Uniqid";
                ddlDepart2.DataBind();
                ddlDepart2.Items.Insert(0, new ListItem("全部", "0"));
                try
                {
                    ddlDepart2.SelectedValue = selectedValue2;
                }
                catch { }
                List<ESP.Compatible.Department> depart3List = ESP.Compatible.DepartmentManager.GetByParent(int.Parse(ddlDepart2.SelectedValue == "0" ? "-1" : ddlDepart2.SelectedValue)).FindAll(n => !n.NodeName.Contains("作废"));
                ddlDepart3.DataSource = depart3List;
                ddlDepart3.DataTextField = "NodeName";
                ddlDepart3.DataValueField = "Uniqid";
                ddlDepart3.DataBind();
                ddlDepart3.Items.Insert(0, new ListItem("全部", "0"));
                try
                {
                    ddlDepart3.SelectedValue = selectedValue3;
                }
                catch { }
            }
        }

        private void GetSelectedDepartmentIds(ref string strWhere)
        {
            string allowedlyDepartIds = "";
            if (!isPurchaseUser())
            {
                List<ESP.Framework.Entity.DepartmentInfo> depart1 = new List<ESP.Framework.Entity.DepartmentInfo>();
                List<ESP.Framework.Entity.DepartmentInfo> depart2 = new List<ESP.Framework.Entity.DepartmentInfo>();
                List<ESP.Framework.Entity.DepartmentInfo> depart3 = new List<ESP.Framework.Entity.DepartmentInfo>();

                string where = " DirectorId = " + CurrentUser.SysID + " or ManagerId=" + CurrentUser.SysID + " or CEOId=" + CurrentUser.SysID;
                DataTable departDT = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(where).Tables[0];
                for (int i = 0; i < departDT.Rows.Count; i++)
                {
                    if (ddlDepart1.SelectedValue != "0")
                    {
                        string depId = departDT.Rows[i]["depId"].ToString();
                        ESP.Framework.Entity.DepartmentInfo dep3Model = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(depId));
                        ESP.Framework.Entity.DepartmentInfo dep2Model = ESP.Framework.BusinessLogic.DepartmentManager.Get(dep3Model.ParentID);
                        ESP.Framework.Entity.DepartmentInfo dep1Model = ESP.Framework.BusinessLogic.DepartmentManager.Get(dep2Model.ParentID);
                        if (ddlDepart3.SelectedValue != "0")
                        {
                            if (ddlDepart3.SelectedValue == dep3Model.DepartmentID.ToString())
                                allowedlyDepartIds += dep3Model.DepartmentID.ToString() + ",";
                        }
                        else
                        {
                            if (ddlDepart2.SelectedValue != "0")
                            {
                                if (ddlDepart2.SelectedValue == dep2Model.DepartmentID.ToString())
                                    allowedlyDepartIds += dep3Model.DepartmentID.ToString() + ",";
                            }
                            else
                            {
                                if (ddlDepart1.SelectedValue == dep1Model.DepartmentID.ToString())
                                    allowedlyDepartIds += dep3Model.DepartmentID.ToString() + ",";
                            }
                        }
                    }
                    else
                    {
                        allowedlyDepartIds += departDT.Rows[i]["depId"].ToString() + ",";
                    }
                }
            }
            else
            {
                if (ddlDepart3.SelectedValue != "0")
                {
                    allowedlyDepartIds = ddlDepart3.SelectedValue;
                }
                else
                {
                    if (ddlDepart2.SelectedValue != "0")
                    {
                        foreach (ESP.Framework.Entity.DepartmentInfo m2 in ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(int.Parse(ddlDepart2.SelectedValue)))
                        {
                            allowedlyDepartIds += m2.DepartmentID + ",";
                        }
                    }
                    else
                    {
                        if (ddlDepart1.SelectedValue != "0")
                        {
                            foreach (ESP.Framework.Entity.DepartmentInfo m1 in ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(int.Parse(ddlDepart1.SelectedValue)))
                            {
                                foreach (ESP.Framework.Entity.DepartmentInfo m2 in ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(m1.DepartmentID))
                                {
                                    allowedlyDepartIds += m2.DepartmentID + ",";
                                }
                            }
                        }
                        else
                        {
                            foreach (ESP.Framework.Entity.DepartmentInfo m1 in ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(0))
                            {
                                foreach (ESP.Framework.Entity.DepartmentInfo m2 in ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(m1.DepartmentID))
                                {
                                    foreach (ESP.Framework.Entity.DepartmentInfo m3 in ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(m2.DepartmentID))
                                    {
                                        allowedlyDepartIds += m3.DepartmentID + ",";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (allowedlyDepartIds.TrimEnd(',') != "")
            {
                strWhere += " and DepartmentID in (" + allowedlyDepartIds.TrimEnd(',') + ")";
            }
        }

        private void ListBind()
        {
            DataTable dt = new DataTable();
            string strWhere = "";
            List<SqlParameter> parms = new List<SqlParameter>();

            strWhere += string.Format(" a.Status not in ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", State.requisition_save, State.requisition_commit, State.requisition_return, State.order_return, State.requisition_submit, State.requisition_ok, State.requisition_temporary_commit, State.requisition_operationAduit, State.order_mediaAuditWait, State.order_ADAuditWait, State.requisition_MediaFAOperated, State.requisition_del);  //State.order_ok, State.order_sended, State.order_confirm, State.requisition_recipiented, State.order_mediaAuditYes);
            strWhere += string.Format(" and a.prtype not in ({0},{1})", (int)PRTYpe.PR_MediaFA, (int)PRTYpe.PR_PriFA);
            if (txtBegin.Text.Trim() != "")
            {
                strWhere += " and order_audittime >=CONVERT(datetime , @begin, 120 )";
                parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
            }
            if (txtEnd.Text.Trim() != "")
            {
                strWhere += " and order_audittime <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
                parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
            }
            if (txtSupplierName.Text.Trim() != "")
            {
                strWhere += " and supplier_name like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtSupplierName.Text.Trim()));
            }
            if (txtProductType.Text.Trim() != "")
            {
                strWhere += @" and a.id in (select distinct a.general_id from t_orderinfo as a 
                        inner join t_type as b on b.typeid=a.producttype
                        where b.typelevel=3 and b.typename like '%'+@typename+'%')";
                parms.Add(new SqlParameter("@typename", txtProductType.Text.Trim()));
            }
            if (txtID.Text.Trim() != "")
            {
                int totalgno = 0;
                bool res = int.TryParse(txtID.Text, out totalgno);
                if (res)
                {
                    strWhere += " and a.id = @pid";
                    parms.Add(new SqlParameter("@pid", txtID.Text.Trim()));
                }
            }
            if (txtPr.Text.Trim() != "")
            {
                strWhere += " and prNo like '%'+@prNo+'%'";
                parms.Add(new SqlParameter("@prNo", txtPr.Text.Trim()));
            }

            GetSelectedDepartmentIds(ref strWhere);

            List<ESP.Purchase.Entity.GeneralInfo> list = GeneralInfoManager.GetStatusList(" and" + strWhere, parms);
            gvG.DataSource = list;
            gvG.DataBind();

            dt = GeneralInfoManager.GetList(strWhere, parms);
            Session["dt"] = dt;
            Session["date"] = txtBegin.Text + " 至 " + txtEnd.Text;

            if (gvG.PageCount > 1)
            {
                PageBottom.Visible = true;
                PageTop.Visible = true;
            }
            else
            {
                PageBottom.Visible = false;
                PageTop.Visible = false;
            }
            if (dt.Rows.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = list.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvG.PageIndex + 1).ToString() + "/" + gvG.PageCount.ToString();
            if (gvG.PageCount > 0)
            {
                if (gvG.PageIndex + 1 == gvG.PageCount)
                    disButton("last");
                else if (gvG.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Purchase.Entity.GeneralInfo model = (ESP.Purchase.Entity.GeneralInfo)e.Row.DataItem;
                //对私3000以上和媒体3000以上，不显示初审人
                if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                {
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[7].Text = "";
                    e.Row.Cells[6].Text = "";
                }
                //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
                int typeoperationflow = OrderInfoManager.getTypeOperationFlow(model.id);
                if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                {
                    if (typeoperationflow == State.typeoperationflow_MP)
                    {
                        e.Row.Cells[6].Controls.Clear();
                        e.Row.Cells[7].Controls.Clear();
                        e.Row.Cells[6].Text = "";
                        e.Row.Cells[7].Text = "";
                    }
                }
                Label labState = ((Label)e.Row.FindControl("labState"));
                labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();

                Repeater repProductType = (Repeater)e.Row.FindControl("repProductType");
                Repeater repProduct = (Repeater)e.Row.FindControl("repProduct");
                List<ESP.Purchase.Entity.OrderInfo> list = OrderInfoManager.GetListByGeneralId(model.id);
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

                repProductType.DataSource = list;
                repProductType.DataBind();

                Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
                if (null != labRequisitionflow && labRequisitionflow.Text != "")
                    labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
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

        private void disButton(string page)
        {
            switch (page)
            {
                case "first":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = false;
                    btnPrevious2.Enabled = false;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
                case "last":
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = false;
                    btnLast2.Enabled = false;
                    break;
                default:
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
            }
        }

        protected void btnDailyPurchase_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dt"];
            FileHelper.ExportDailyPurchase(dt.Select(), Session["date"].ToString(), Server.MapPath("~"), Response);
            GC.Collect();
        }

        protected void btnCusAsk_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dt"];
            FileHelper.ExportCusAsk(dt.Select("cusAsk='是'"), Session["date"].ToString(), Server.MapPath("~"), Response);
            GC.Collect();
        }

        protected void btnAfterwards_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dt"];
            FileHelper.ExportAfterwards(dt.Select("afterwardsname='是'"), Session["date"].ToString(), Server.MapPath("~"), Response);
            GC.Collect();
        }

        protected void btnEmBuy_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dt"];
            FileHelper.ExportEmBuy(dt.Select("EmBuy='是'"), Session["date"].ToString(), Server.MapPath("~"), Response);
            GC.Collect();
        }

        protected void btnCostReport_Click(object sender, EventArgs e)
        {
            FileHelper.ExprotCostReport((DataTable)Session["dt"], "", Server.MapPath("~"), Response);
        }
    }
}
