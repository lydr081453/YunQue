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
    public partial class PrFileList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] state = State.requistionOrorderValue1_state;
                string[] statevalue = State.requistionOrorderValue_state;
                for (int i = 0; i < state.Length; i++)
                {
                    ddlState.Items.Add(new ListItem(state[i].ToString(), statevalue[i].ToString()));
                }
                ddlState.Items.Insert(0, new ListItem("请选择", "-1"));

                ddlRequisitionflow.Items.Add(new ListItem("全部", "-1"));
                ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
                ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
                ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));

                ListBind();
            }
        }

        private void ListBind()
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string term = "";

            term += " and status <> @status and status <> @status1 and status <> @status2";
            parms.Add(new SqlParameter("@status", State.requisition_save));
            parms.Add(new SqlParameter("@status1", State.requisition_return));
            parms.Add(new SqlParameter("@status2", State.requisition_del));

            if (ddlState.SelectedValue != "-1")
            {
                term += " and status=@status3";
                parms.Add(new SqlParameter("@status3", ddlState.SelectedValue));
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
            if (txtAudit.Text.Trim() != "")
            {
                term += " and first_assessorname like '%'+@first_assessorname+'%'";
                parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
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
            if (txtBegin.Text.Trim() != "")
            {
                term += " and order_audittime >=CONVERT(datetime , @begin, 120 )";
                parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
            }
            if (txtEnd.Text.Trim() != "")
            {
                term += " and order_audittime <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
                parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
            }
            if (txtFiliale_AuditName.Text.Trim() != "")
            {
                term += " and Filiale_AuditName like '%'+@Filiale_AuditName+'%'";
                parms.Add(new SqlParameter("@Filiale_AuditName", txtFiliale_AuditName.Text.Trim()));
            }

            List<GeneralInfo> list = GeneralInfoManager.GetStatusList(term, parms);
            gvList.DataSource = list;
            gvList.DataBind();

            if (gvList.PageCount > 1)
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
            labPageCount.Text = labPageCountT.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
            if (gvList.PageCount > 0)
            {
                if (gvList.PageIndex + 1 == gvList.PageCount)
                    disButton("last");
                else if (gvList.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvList.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex + 1) > gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex - 1) < 0 ? 0 : (gvList.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvList_PageIndexChanging(new object(), e);
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralInfo Model = (GeneralInfo)e.Row.DataItem;

                Literal litContrastUpFiles = (Literal)e.Row.FindControl("litContrastUpFiles");//比价信息备注
                string[] files = Model.contrastUpFiles.Split('#');
                int i = 0;
                foreach (string filepath in files)
                {
                    if (filepath.Trim() != "")
                    {
                        litContrastUpFiles.Text += "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + Model.id.ToString() + "&Index=" + i.ToString() + "&Type=ContrastFile'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;&nbsp;";
                        i++;
                    }
                }

                Literal litSow = (Literal)e.Row.FindControl("litSow");//工作需求描述
                litSow.Text = Model.sow2 == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + Model.id.ToString() + "&Index=0&Type=Sow2'><img src='/images/ico_04.gif' border='0' /></a>";

                Literal litContrast = (Literal)e.Row.FindControl("litContrast");// 比价节约
                litContrast.Text = Model.contrastFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + Model.id.ToString() + "&Index=0&Type=Contrast'><img src='/images/ico_04.gif' border='0' /></a>";

                Literal litConsult = (Literal)e.Row.FindControl("litConsult");//议价节约
                litConsult.Text = Model.consultFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + Model.id.ToString() + "&Index=0&Type=Consult'><img src='/images/ico_04.gif' border='0' /></a>";

                Literal litCusAskEmailFile = (Literal)e.Row.FindControl("litCusAskEmailFile"); //客户指定邮件
                litCusAskEmailFile.Text = Model.CusAskEmailFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + Model.id.ToString() + "&Index=0&Type=CusAskEmailFile'><img src='/images/ico_04.gif' border='0' /></a>";

                Repeater repProduct = (Repeater)e.Row.FindControl("repProduct"); //采购物品
                repProduct.DataSource = OrderInfoManager.GetListByGeneralId(Model.id);
                repProduct.DataBind();
            }
        }
    }
}
