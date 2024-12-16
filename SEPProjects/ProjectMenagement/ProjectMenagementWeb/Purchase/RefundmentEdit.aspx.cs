using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Purchase
{
    public partial class RefundmentEdit : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ReturnInfo returnModel = null;
        ESP.Finance.Entity.ReturnInfo returnParent = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindData();
                bindList();
            }
        }

        private void bindData()
        {
            if (!string.IsNullOrEmpty(Request[RequestName.ReturnID]))
            {
                int returnId = Convert.ToInt32(Request[RequestName.ReturnID]);
                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
                returnParent = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnModel.ParentID.Value);
                this.txtRemark.Text = returnModel.Remark;
                this.txtRefund.Text = returnModel.PreFee.Value.ToString("#,##0.00");
                this.lblProjectCode.Text = returnModel.ProjectCode;
                this.lblAmount.Text = returnParent.FactFee.Value.ToString("#,##0.00");
                this.lblReturnCode.Text = returnParent.ReturnCode;
                this.hidReturnId.Value = returnParent.ReturnID.ToString();
            }
        }

        private void bindList()
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            term = " ReturnStatus=@ReturnStatus and RequestorId=@RequestorId and returntype not in(34,36) ";

            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@ReturnStatus", System.Data.SqlDbType.Int, 4);
            p.SqlValue = (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
            paramlist.Add(p);

            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@RequestorId", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = int.Parse(CurrentUser.SysID);
            paramlist.Add(p2);


            if (!string.IsNullOrEmpty(this.txtKeyword.Text.Trim()))
            {
                term += "  and (prno like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%' or prefee  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                p3.SqlValue = this.txtKeyword.Text.Trim();
                paramlist.Add(p3);
            }

            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Edit/ReturnTabEdit.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int returnId = 0;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
            {
                returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
                returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
            }
            else
            {
                returnModel = new ESP.Finance.Entity.ReturnInfo();
            }
            if (!string.IsNullOrEmpty(this.hidReturnId.Value))
            {
                returnParent = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(this.hidReturnId.Value));
                returnModel.ProjectID = returnParent.ProjectID;
                returnModel.ProjectCode = returnParent.ProjectCode;
                returnModel.DepartmentID = returnParent.DepartmentID;
                returnModel.DepartmentName = returnParent.DepartmentName;
                returnModel.RequestDate = DateTime.Now;
                returnModel.RequestorID = returnParent.RequestorID;
                returnModel.RequestEmployeeName = returnParent.RequestEmployeeName;
                returnModel.RequestPhone = returnParent.RequestPhone;
                returnModel.RequestUserCode = returnParent.RequestUserCode;
                returnModel.RequestUserName = returnParent.RequestUserName;
                returnModel.SupplierBankAccount = returnParent.SupplierBankAccount;
                returnModel.SupplierBankName = returnParent.SupplierBankName;
                returnModel.SupplierName = returnParent.SupplierName;
                returnModel.ReturnPreDate = DateTime.Now;
                returnModel.ReturnFactDate = DateTime.Now;
                returnModel.PreBeginDate = returnParent.PreBeginDate;
                returnModel.PreEndDate = returnParent.PreEndDate;
                returnModel.PaymentTypeCode = returnParent.PaymentTypeCode;
                returnModel.PaymentTypeID = returnParent.PaymentTypeID;
                returnModel.PaymentTypeName = returnParent.PaymentTypeName;
                returnModel.BranchID = returnParent.BranchID;
                returnModel.BranchCode = returnParent.BranchCode;
                returnModel.BranchName = returnParent.BranchName;
                returnModel.BankAccount = returnParent.BankAccount;
                returnModel.BankAccountName = returnParent.BankAccountName;
                returnModel.BankAddress = returnParent.BankAddress;
                returnModel.BankID = returnParent.BankID;
                returnModel.BankName = returnParent.BankName;

                returnModel.PRID = returnParent.PRID;
                returnModel.PRNo = returnParent.PRNo;
                returnModel.ParentID = returnParent.ReturnID;
                returnModel.ReturnStatus = 1;
                returnModel.ReturnType = -1;
                returnModel.Remark = this.txtRemark.Text;
                returnModel.ReturnContent = returnParent.ReturnCode + " 退款";

                if (decimal.Parse(this.txtRefund.Text) > returnParent.FactFee)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('退款金额不能大于原申请金额！');", true);
                    return;
                }
                returnModel.PreFee = decimal.Parse(this.txtRefund.Text);
                if (string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
                    returnId = ESP.Finance.BusinessLogic.ReturnManager.CreateReturnInFinance(returnModel);
                else
                    ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

                Response.Redirect("SetAuditor.aspx?" + RequestName.ReturnID + "=" + returnId.ToString());

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择要退款的申请单！');", true);
                return;
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            bindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                Literal output = e.Row.FindControl("literal") as Literal;
                output.Text = string.Format("<input type='radio' name='rdSelect' id='rdSelect{0}' value='value{0}' onclick=\"selectPN('" + model.ReturnID.ToString() + "','" + model.ReturnCode + "','" + model.ProjectCode + "','" + model.FactFee.Value.ToString("#,##0.00") + "');\"", e.Row.RowIndex);
            }
        }


        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }

    }
}
