using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class CompletePaymentList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePub], State.OperationTypePub.ToString()));
                ddlType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePri], State.OperationTypePri.ToString()));
                ddlType.Items.Insert(0, new ListItem("全部", "-1"));

                TypeBind();
                ListBind();
            }
        }

        /// <summary>
        /// 绑定申请单流向
        /// </summary>
        private void TypeBind()
        {
            ddlRequisitionflow.Items.Add(new ListItem("全部", "-1"));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void ListBind()
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string term = string.Empty;

            term = " and (status=@completed) ";
            parms.Add(new SqlParameter("@completed", State.requisition_paid));
            if (txtAudit.Text.Trim() != "")
            {
                term += " and first_assessorname like '%'+@first_assessorname+'%'";
                parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
            }
            if (txtFiliale_AuditName.Text.Trim() != "")
            {
                term += " and Filiale_AuditName like '%'+@Filiale_AuditName+'%'";
                parms.Add(new SqlParameter("@Filiale_AuditName", txtFiliale_AuditName.Text.Trim()));
            }
            if (ddlRequisitionflow.SelectedValue != "-1")
            {
                term += " and requisitionflow = @requisitionflow ";
                parms.Add(new SqlParameter("@requisitionflow", ddlRequisitionflow.SelectedValue));
            }

            if (txtPrNo.Text.Trim() != "")
            {
                term += " and prNo like '%'+@prNo+'%'";
                parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
            }
            if (txtProjectCode.Text.Trim() != "")
            {
                term += " and project_code like '%'+@projectcode+'%'";
                parms.Add(new SqlParameter("@projectcode", txtProjectCode.Text.Trim()));
            }
            if (txtGlideNo.Text.Trim() != "")
            {
                int totalgno = 0;
                bool res = int.TryParse(txtGlideNo.Text, out totalgno);
                if (res)
                {
                    term += " and a.id = @id";
                    parms.Add(new SqlParameter("@id", txtGlideNo.Text.TrimStart('0')));
                }
            }
            if (txtsupplierName.Text.Trim() != "")
            {
                term += " and supplier_name like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
            }
            if (txtRequestor.Text.Trim() != "")
            {
                term += " and requestorname like '%'+@requestorname+'%'";
                parms.Add(new SqlParameter("@requestorname", txtRequestor.Text.Trim()));
            }
            if (txtTotalMin.Text.Trim() != "")
            {
                int totalmin = 0;
                bool res = int.TryParse(txtTotalMin.Text, out totalmin);
                if (res)
                {
                    term += " and b.totalprice >=@totalmin";
                    parms.Add(new SqlParameter("@totalmin", txtTotalMin.Text.Trim()));
                }

            }
            if (ddlType.SelectedValue != "-1")
            {
                term += " and operationType=" + ddlType.SelectedValue;
            }
            if (txtTotalMax.Text.Trim() != "")
            {
                int totalmax = 0;
                bool res = int.TryParse(txtTotalMax.Text, out totalmax);
                if (res)
                {
                    term += " and b.totalprice <= @totalmax";
                    parms.Add(new SqlParameter("@totalmax", txtTotalMax.Text.Trim()));
                }
            }
            if (txtProductType.Text.Trim() != "")
            {
                term += @" and a.id in (select distinct a.general_id from t_orderinfo as a 
                        inner join t_type as b on b.typeid=a.producttype
                        where b.typelevel=3 and b.typename like '%'+@typename+'%')";
                parms.Add(new SqlParameter("@typename", txtProductType.Text.Trim()));
            }


            List<GeneralInfo> list = GeneralInfoManager.GetStatusList(term, parms);
            list.Sort(new ESP.Purchase.BusinessLogic.GeneralInfoCompareAudit());
            gvG.DataSource = list;
            gvG.DataBind();

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
            if (list.Count > 0)
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
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralInfo model = (GeneralInfo)e.Row.DataItem;
                Label labState = ((Label)e.Row.FindControl("labState"));
                LinkButton lnkSend = (LinkButton)e.Row.FindControl("lnkSend");
                HyperLink hypNoView = (HyperLink)e.Row.FindControl("hypNoView");
                if (null != hypNoView)
                {
                    hypNoView.NavigateUrl = "OrderDetailTab.aspx?backUrl=CompletePaymentList.aspx&" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString() + "&helpfile=3";
                }

                if (null != labState)
                {
                    labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();
                }
                Repeater repProductType = (Repeater)e.Row.FindControl("repProductType");
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

                repProductType.DataSource = list;
                repProductType.DataBind();



                Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
                if (null != labRequisitionflow && labRequisitionflow.Text != "")
                {
                    labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
                }
                //对私3000以上和媒体3000以上，不显示初审人
                if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                {
                    e.Row.Cells[8].Controls.Clear();
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[8].Text = "";
                    e.Row.Cells[7].Text = "";
                }
                //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
                int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
                if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                {
                    if (typeoperationflow == State.typeoperationflow_MP )
                    {
                        e.Row.Cells[7].Controls.Clear();
                        e.Row.Cells[8].Controls.Clear();
                        e.Row.Cells[7].Text = "";
                        e.Row.Cells[8].Text = "";
                    }
                }
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

        #region 分页
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

        #endregion

    }
}
