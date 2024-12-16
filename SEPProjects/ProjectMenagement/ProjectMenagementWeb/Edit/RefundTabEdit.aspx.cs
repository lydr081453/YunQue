using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;


namespace FinanceWeb.Edit
{
    public partial class RefundTabEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && !this.GridRefund.CausedCallback)
            {
                ListBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }


        private void ListBind()
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            term = "(RequestorID=@currentUserId and Status in(-1,0,1)) ";

            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = int.Parse(CurrentUser.SysID);
            paramlist.Add(puserid);

            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (projectname like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or refundcode like '%'+@prno+'%' or prid like '%'+@prno+'%' or amounts  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                p1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p1);
            }



            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and RequestDate between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    p3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(p3);
                    System.Data.SqlClient.SqlParameter p4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    p4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(p4);

                }
            }
            IList<ESP.Finance.Entity.RefundInfo> refundlist = ESP.Finance.BusinessLogic.RefundManager.GetList(term, paramlist);
            this.GridRefund.DataSource = refundlist;
            this.GridRefund.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.GridRefund.ItemDataBound += new ComponentArt.Web.UI.Grid.ItemDataBoundEventHandler(GridRefund_ItemDataBound);
            this.GridRefund.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(GridRefund_DeleteCommand);
            GridRefund.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridRefund_NeedRebind);
            GridRefund.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridRefund_PageIndexChanged);

        }
        void GridRefund_NeedRebind(object sender, EventArgs e)
        {
            ListBind();
        }

        void GridRefund_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridRefund.CurrentPageIndex = e.NewIndex;
        }

        void GridRefund_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            //撤销付款申请
            int ID = int.Parse(e.Item["ID"].ToString());
            ESP.Finance.Entity.RefundInfo refundModel = ESP.Finance.BusinessLogic.RefundManager.GetModel(ID);
            if (refundModel.Status == 0 || refundModel.Status == 1 || refundModel.Status == -1)
            {
                if (ESP.Finance.BusinessLogic.RefundManager.Delete(ID)==DeleteResult.Succeed)
                {
                    ESP.Purchase.Entity.LogInfo logModel = new ESP.Purchase.Entity.LogInfo();
                    logModel.Gid = refundModel.PRID;
                    logModel.Des = CurrentUserName + "撤销退款申请[" + refundModel.RefundCode + "] " + DateTime.Now;
                    logModel.LogUserId = CurrentUserID;
                    logModel.LogMedifiedTeme = DateTime.Now;
                    ESP.Purchase.BusinessLogic.LogManager.AddLog(logModel, Request);

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已成功将退款申请撤销！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('撤销失败！');", true);
                }

            }
        }

        void GridRefund_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.RefundInfo refundModel = (ESP.Finance.Entity.RefundInfo)e.DataItem;

            e.Item["StatusName"] = ReturnPaymentType.ReturnStatusString(refundModel.Status, 0, false);

            if ((refundModel.Status == (int)PaymentStatus.Save || refundModel.Status == (int)PaymentStatus.Rejected || refundModel.Status == (int)PaymentStatus.Created) && (int.Parse(CurrentUser.SysID) == refundModel.RequestorID))
            {
                e.Item["Edit"] = "<a href='/Refund/RefundEdit.aspx?" + RequestName.ModelID + "=" + refundModel.Id + "'><img src='/images/edit.gif' title='编辑' border='0px' /></a>";
            }
            else
            {
                e.Item["Edit"] = "";
                e.Item["Cancel"] = "";//只可以撤销保存状态的数据
            }

        }

    }
}