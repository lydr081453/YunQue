using System;
using System.Linq;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Data;

namespace PurchaseWeb.Purchase.Requisition.SupplierCollaboration
{
    public partial class OtherMediaProductsReference : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                ListBind();
            }
        }

        private void BindType()
        {
            DataSet ds = ManuscriptTypeManager.GetList(" IsDel='False'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dllType.DataSource = ds.Tables[0];
                this.dllType.DataTextField = "TypeName";
                this.dllType.DataValueField = "ID";
                this.dllType.DataBind();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void ListBind()
        {
            string strType = string.Empty;
            if (this.dllType.SelectedItem.Value != string.Empty)
            {
                strType = " And ID=" + this.dllType.SelectedItem.Value;
            }
            DataSet dsMedia = OtherMediumInProductsManager.GetList(" IsDel='0' AND MediaName Like('%" + this.txtName.Text.Trim() + "%') " + strType);
            string[] ids = new string[dsMedia.Tables[0].Rows.Count];
            if (dsMedia.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsMedia.Tables[0].Rows.Count; i++)
                {
                    ids[i] = dsMedia.Tables[0].Rows[i]["ID"].ToString();
                }
            }

            List<SqlParameter> parms = new List<SqlParameter>();
            string term = string.Empty;
            term = " 1=1 ";

            if (ids.Length > 0)
            {
                term += " AND MediaProductID in(";
                for (int j = 0; j < ids.Length; j++)
                {
                    term += ids[j];
                    if (j != ids.Length - 1)
                    {
                        term += ",";
                    }
                }
                term += ")";
            }

            if (this.rdoYes.Checked)
            {
                term += " and IsHavePic=@IsHavePic";
                parms.Add(new SqlParameter("@IsHavePic", true));
            }

            if (this.rdoYes.Checked)
            {
                term += " and IsHavePic=@IsHavePic";
                parms.Add(new SqlParameter("@IsHavePic", false));
            }
            term += " AND Area Like('%" + this.txtArea.Text.Trim() + "%')";
            if (this.dllType.Items.Count > 0)
            {
                term += " AND ManuscriptType Like('%" + this.dllType.SelectedItem.Value + "%')";
            }

            DataSet list = OtherMediumInProductsDetailsManager.GetList(term);
            gvG.DataSource = list.Tables[0];
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
            if (list.Tables[0].Rows.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = list.Tables[0].Rows.Count.ToString();
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

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int id = int.Parse(gvG.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text.Trim());
            //GeneralInfo g = GeneralInfoManager.GetModel(id);
            //if (g.InUse == (int)State.PRInUse.Use)
            //{
            //    if (e.CommandName == "SendMail")
            //    {
            //        SendMail(g);

            //        //记录操作日志
            //        ESP.Logging.Logger.Add(string.Format("{0}对PR单中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, id.ToString(), "发送PO单至供应商处"), "已审批通过PR");
            //    }
            //    if (e.CommandName == "HandConfirm")
            //    {
            //        HandConfirm(g);
            //        //记录操作日志
            //        ESP.Logging.Logger.Add(string.Format("{0}对PR单中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, id.ToString(), "手动确认"), "已审批通过PR");
            //    }
            //    if (e.CommandName == "Return")
            //    {
            //        if (null != g)
            //        {
            //            ESP.ITIL.BusinessLogic.申请单业务设置.采购撤销订单(CurrentUser, ref g);
            //            GeneralInfoManager.Update(g);

            //            LogInfo log = new LogInfo();
            //            log.Gid = g.id;
            //            log.LogMedifiedTeme = DateTime.Now;
            //            log.LogUserId = CurrentUserID;

            //            log.Des = string.Format(State.log_requisition_cancelByauditor, CurrentUserName, DateTime.Now.ToString());
            //            LogManager.AddLog(log, Request);
            //            //记录操作日志
            //            ESP.Logging.Logger.Add(string.Format("{0}对PR单中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, id.ToString(), "撤销"), "已审批通过PR");

            //            //给供应商发送撤销通知
            //            string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/OrderPrint.aspx?id=" + id + "&showBottom=false";
            //            string body = "该订单已被撤销！</br>" + ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            //            ESP.ConfigCommon.SendMail.Send1("订单撤销通知", g.supplier_email, body, false, new Hashtable());
            //        }
            //        ClientScript.RegisterStartupScript(typeof(string), "", "alert('申请单审批操作撤销成功!');window.location='AuditOrderPassList.aspx'", true);

            //    }
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ESP.Purchase.Common.State.DisabledMessageForPR + "');window.location='OrderCommitList.aspx'", true);
            //}
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hidMediaID = (HiddenField)e.Row.FindControl("hidMediaID");
                Label lblMediaName = (Label)e.Row.FindControl("lblMediaName");
                if (hidMediaID != null && !string.IsNullOrEmpty(hidMediaID.Value))
                {
                    OtherMediumInProductInfo info = OtherMediumInProductsManager.GetModel(Convert.ToInt32(hidMediaID.Value));
                    if (info != null)
                    {
                        lblMediaName.Text = info.MediaName;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int orderID = 0;
            int generalID = Convert.ToInt32(Request[RequestName.GeneralID]);
            if (!string.IsNullOrEmpty(Request["OrderID"]) && Request["OrderID"] != "0")
            {
                orderID = Convert.ToInt32(Request["OrderID"]);
            }
            else
            {
                OrderInfo order = new OrderInfo();
                order.general_id = Convert.ToInt32(Request[RequestName.GeneralID]);
                order.producttype = Convert.ToInt32(Request[RequestName.ProductType]);
                orderID = OrderInfoManager.Add(order, UserInfo.UserID, UserInfo.Username);//还需要更多信息

                GeneralInfo gen = GeneralInfoManager.GetModel(Convert.ToInt32(Request[RequestName.GeneralID]));
                if (gen != null)
                {
                    gen.PRType = (int)PRTYpe.PR_OtherMedia;
                    GeneralInfoManager.Update(gen);
                }
            }
            foreach (GridViewRow dr in this.gvG.Rows)
            {
                CheckBox ckSel = (CheckBox)dr.FindControl("ckSel");
                if (ckSel != null && ckSel.Checked)
                {
                    HiddenField hidDetailID = (HiddenField)dr.FindControl("hidDetailID");
                    if (hidDetailID != null && !string.IsNullOrEmpty(hidDetailID.Value))
                    {
                        SaveOrderDetail(orderID, Convert.ToInt32(hidDetailID.Value));

                    }
                }
            }

            string clientId = "ctl00_ContentPlaceHolder1_";
            string clientId1 = "ctl00$ContentPlaceHolder1$";
            Response.Write("<script>opener.document.all." + clientId + "hidOrderID.value= '" + orderID + "'</script>");
            Response.Write("<script>opener.__doPostBack('" + clientId1 + "btnRefsh','');</script>");
            Response.Write(@"<script>window.close();</script>");
        }

        private void SaveOrderDetail(int orderID, int detailID)
        {
            OtherMediumForOrderInfo info = new OtherMediumForOrderInfo();
            info.OrderID = orderID;
            info.MediaID = detailID;

            OtherMediumInProductDetailsInfo detailInfo = OtherMediumInProductsDetailsManager.GetModel(detailID);

            OtherMediumInProductInfo proInfo = OtherMediumInProductsManager.GetModel(detailInfo.MediaProductID);

            info.MediaName = proInfo.MediaName;
            info.CreatedUserID = UserInfo.UserID;
            info.ModifiedUserID = UserInfo.UserID;

            info.MediaArea = detailInfo.Area;
            info.MediaDescription = detailInfo.Description;
            info.MediaShunYaDescription = detailInfo.ShunYaDescription;


            OtherMediumForOrderManager.Add(info);
        }
    }
}