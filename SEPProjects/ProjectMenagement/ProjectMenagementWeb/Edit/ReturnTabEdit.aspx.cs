using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Edit
{
    public partial class ReturnTabEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.GridReturn.CausedCallback)
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
            string Branchs = string.Empty;
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> trafficList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" auditoruserid=" + CurrentUser.SysID + " and audittype>9 and returnid in(select returnid from f_return where (returntype=11 and returnstatus in(137,138)))");
            if (trafficList != null && trafficList.Count > 0)
            {
                foreach (ESP.Finance.Entity.ReturnAuditHistInfo b in trafficList)
                {
                    Branchs += b.ReturnID.ToString() + ",";
                }
            }
            Branchs = Branchs.TrimEnd(',');

            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(Branchs))
            {
                term = string.Format(" ((returnStatus!=1 and returnid in(select returnid from f_returninvoice where status!=2 and (faid=" + CurrentUser.SysID + " or financeid=" + CurrentUser.SysID + "))) or returnid in({0}) or (RequestorID=@currentUserId and returnstatus=1) ", Branchs);
            }
            else
                term = " ((returnStatus!=1 and returnid in(select returnid from f_returninvoice where status!=2 and (faid=@currentUserId or financeid=@currentUserId))) or (RequestorID=@currentUserId and returnstatus=1) ";

            term += string.Format(" or (RequestorID=@currentUserId and ((returnStatus not in(1,137,138) and (isinvoice=0 or isinvoice is null)) or returnStatus in(136,150)))) and returntype not in(30,31,32,33,34,35,36,37,40)", (int)ESP.Finance.Utility.PaymentStatus.Save);

            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = int.Parse(CurrentUser.SysID);
            paramlist.Add(puserid);

            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (prno like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%' or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
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
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            this.GridReturn.DataSource = returnlist;
            this.GridReturn.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.GridReturn.ItemDataBound += new ComponentArt.Web.UI.Grid.ItemDataBoundEventHandler(GridReturn_ItemDataBound);
            this.GridReturn.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(GridReturn_DeleteCommand);
            GridReturn.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridReturn_NeedRebind);
            GridReturn.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridReturn_PageIndexChanged);

        }
        void GridReturn_NeedRebind(object sender, EventArgs e)
        {
            ListBind();
        }

        void GridReturn_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridReturn.CurrentPageIndex = e.NewIndex;
        }

        void GridReturn_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            //撤销付款申请
            int returnID = int.Parse(e.Item["returnID"].ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            if (returnModel.ReturnStatus == 0 || returnModel.ReturnStatus == 1)
            {
                if (ESP.Finance.BusinessLogic.ReturnManager.returnPaymentInfo(returnID))
                {
                    ESP.Purchase.Entity.LogInfo logModel = new ESP.Purchase.Entity.LogInfo();
                    logModel.Gid = returnModel.PRID.Value;
                    logModel.Des = CurrentUserName + "撤销付款申请[" + returnModel.ReturnCode + "] " + DateTime.Now;
                    logModel.LogUserId = CurrentUserID;
                    logModel.LogMedifiedTeme = DateTime.Now;
                    ESP.Purchase.BusinessLogic.LogManager.AddLog(logModel, Request);
                    //ListBind();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已成功将付款申请撤销至采购系统！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('撤销失败！');", true);
                }

            }
        }

        void GridReturn_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.DataItem;

            e.Item["ReturnStatusName"] = ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus ?? 0, 0, returnModel.IsDiscount);
            IList<ESP.Finance.Entity.PNBatchRelationInfo> relationList = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" ReturnID=" + returnModel.ReturnID.ToString(), null);
            if (relationList != null && relationList.Count > 0)
                e.Item["Print"] = "";
            else
                e.Item["Print"] = "<a target='_blank' href='/Purchase/Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID + "'><img src='/images/Icon_Output.gif' border='0px' title='打印预览' /></a>";
            //协议供应商PN付款，业务不需要打印
            if (returnModel.NeedPurchaseAudit == true)
            {
                e.Item["Print"] = "";
            }

            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs) && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                e.Item["Attach"] = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + returnModel.MediaOrderIDs + "' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            else
            {
                e.Item["Attach"] = "";
            }
            //3000以下对私的单子有附件显示
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                e.Item["Attach"] = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + returnModel.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
            }
            if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs) && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            {
                e.Item["Export"] = "<a href='/Dialogs/ExportFile.aspx?returnID=" + returnModel.ReturnID + "&Page=ReturnTabEdit.aspx' target='_blank'><img src='/images/PrintDefault.gif' title='导出' border='0'/></a>";
            }

            if (returnModel.ReturnStatus == (int)PaymentStatus.Save && (int.Parse(CurrentUser.SysID) == returnModel.RequestorID))
            {
                    e.Item["Edit"] = "<a href='/Purchase/ReturnEdit.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID + "'><img src='/images/edit.gif' title='编辑' border='0px' /></a>";
            }
            else
            {
                e.Item["Edit"] = "";
                e.Item["Cancel"] = "";//只可以撤销保存状态的数据
            }
            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                //如果经过媒介和个人处理的3000一下付款申请，不可以撤销
                e.Item["Cancel"] = "";
            }
            //如果申请单暂停，不能编辑付款申请
            if (returnModel.PRID != null && returnModel.PRID.Value != 0)
            {
                if (ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value).InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                {
                    e.Item["Edit"] = "";
                    e.Item["Cancel"] = "";
                }
            }
            //重汇判断
            if (returnModel.ReturnStatus == (int)PaymentStatus.FinanceReject && (int.Parse(CurrentUser.SysID) == returnModel.RequestorID))

                e.Item["RePay"] = " <a href='/Purchase/ReturnBankCancel.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() + "' style='cursor: hand'> <img title='重汇' src='/images/edit.gif' border='0px;'></img></a>";
            else
                e.Item["RePay"] = "";
            if (returnModel.PRID != null && returnModel.PRID.Value != 0)
            {
                if (ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(returnModel.PRID.Value).InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                {
                    e.Item["RePay"] = "";
                }
            }
 
            //发票更新判断
            if ((returnModel.IsInvoice == null || returnModel.IsInvoice.Value == 0))
            {
                ESP.Finance.Entity.ReturnInvoiceInfo invoiceModel = ESP.Finance.BusinessLogic.ReturnInvoiceManager.GetModelByReturnID(returnModel.ReturnID);
                if (invoiceModel == null)
                {
                    e.Item["Invoice"] = "<a href='/Purchase/InvoiceAppending.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() + "' style='cursor: hand'> <img title='发票更新' src='/images/edit.gif' border='0px;'></img></a>";
                }
                else if (invoiceModel.Status == 0)//该FA审核
                {
                    if (invoiceModel.FAID == Convert.ToInt32(CurrentUser.SysID))
                    {
                        e.Item["Invoice"] = "<a href='/Purchase/InvoiceAppending.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() + "' style='cursor: hand'> <img title='发票更新' src='/images/edit.gif' border='0px;'></img></a>";
                    }
                    else
                        e.Item["Invoice"] = "";
                }
                else if (invoiceModel.Status == 1)//该财务审核
                {
                    if (invoiceModel.FinanceID == Convert.ToInt32(CurrentUser.SysID))
                    {
                        e.Item["Invoice"] = "<a href='/Purchase/InvoiceAppending.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() + "' style='cursor: hand'> <img title='发票更新' src='/images/edit.gif' border='0px;'></img></a>";
                    }
                    else
                        e.Item["Invoice"] = "";
                }
            }
            else
            {
                e.Item["Invoice"] = "";
            }

            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
            {
                e.Item["ReturnCode"] = " <a href=\"/ForeGift/ForegiftDetail.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() +
                                                              "\" target=\"_blank\">" + returnModel.ReturnCode + "</a>";
            }
            else
            {
                e.Item["ReturnCode"] = " <a href=\"/Purchase/ReturnDisplay.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() +
                                                              "\" target=\"_blank\">" + returnModel.ReturnCode + "</a>";
            }

            
            e.Item["prNo"] = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "' style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
           

            if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift && (returnModel.ReturnStatus == 136 || returnModel.ReturnStatus == 137 || returnModel.ReturnStatus == 138))
            {
                e.Item["Traffic"] = "<a href= \"/ForeGift/killForeGift.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() +
                                                                "\" style='cursor: hand'> <img title='销账' src='/images/Audit.gif' border='0px;'></img></a>";
            }
            else
            {
                e.Item["Traffic"] = "";
            }
        }

    }
}
