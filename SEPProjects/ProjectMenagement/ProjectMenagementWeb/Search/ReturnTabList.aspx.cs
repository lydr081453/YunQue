using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Linq;

namespace FinanceWeb.project
{
    public partial class ReturnTabList : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.BranchInfo> branches;
        int userid;
        Dictionary<int, ESP.Purchase.Common.State.PRInUse> InUses;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 600;

            userid = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            if (!IsPostBack && !this.GridReturn.CausedCallback)
            {
                BindInfo();
                Search();
                if (userid.ToString() != ESP.Configuration.ConfigurationManager.SafeAppSettings["EddyBinID"])
                {
                    this.btnDiscount.Visible = false;
                }
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            GridReturn.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridReturn_NeedRebind);
            GridReturn.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridReturn_PageIndexChanged);

        }
        void GridReturn_NeedRebind(object sender, EventArgs e)
        {
            Search();
        }

        void GridReturn_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridReturn.CurrentPageIndex = e.NewIndex;
        }

        private string GetDelegateUser()
        {
            string users = string.Empty;
            DataTable dt = ESP.Framework.BusinessLogic.AuditBackUpManager.GetList("backupuserid=" + userid + " and type=3").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    users += dt.Rows[i]["UserID"].ToString().Trim() + ",";
                }
            }
            return users.TrimEnd(',');
        }

        private string getGroupIds()
        {
            string managerid = string.Empty;
            //if (userid.ToString() == System.Configuration.ConfigurationSettings.AppSettings["TCGAssistant"])
            //{
            //    managerid = System.Configuration.ConfigurationSettings.AppSettings["TCGManager"].ToString().Trim();
            //}
            //else
                managerid = userid.ToString();
            string str = string.Format(" (directorid={0} or managerid={0} or ceoid ={0} or faid={0}) and depid>0 ", managerid);
            DataSet ds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(str);

            string groupids = string.Empty;
            if (ds != null && ds.Tables[0] != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    groupids += ds.Tables[0].Rows[i]["depid"].ToString() + ",";
                }
            }
            return groupids.TrimEnd(',');
        }

        private void Search()
        {
            string delegateusers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(userid);
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                delegateusers += model.UserID.ToString() + ",";
            }
            delegateusers += GetDelegateUser();
            delegateusers = delegateusers.TrimEnd(',');
            string Branchs = string.Empty;

            branches = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            var branchList = branches.Where(x => x.OtherFinancialUsers != null && x.OtherFinancialUsers.Contains("," + userid + ","));
            if (branchList != null)
            {
                foreach (ESP.Finance.Entity.BranchInfo b in branchList)
                {
                    Branchs += "'" + b.BranchCode + "',";
                }
            }
            Branchs = Branchs.TrimEnd(',');
            string groupids = getGroupIds();
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

            if (!string.IsNullOrEmpty(delegateusers))
            {
                term = " ((RequestorID=@currentUserId or RequestorID in(" + delegateusers + ")) or returnid in(select returnid from F_ReturnAuditHist where auditoruserid =@currentUserId)";
            }
            else
            {
                term = " ((RequestorID=@currentUserId) or returnid in(select returnid from F_ReturnAuditHist where auditoruserid =@currentUserId) ";
            }

            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = userid;
            paramlist.Add(puserid);

            if (!string.IsNullOrEmpty(Branchs))
            {
                term += " or branchcode in(" + Branchs + ")";
            }
            if (!string.IsNullOrEmpty(groupids))
            {
                term += " or departmentid in(" + groupids + ")";
            }
            term += ")";

            term += " and returnType not in(30,31,32,33,34,35,36,37,40,20)";

            bool ischoose = false;

            if (this.ddlType.SelectedIndex == 0)
            {
                term += " and returnType in(0,11)";
                Session["OOPTabList_returnType"] = string.Empty;
            }
            else
            {
                ischoose = true;
                term += " and returnType=" + ddlType.SelectedItem.Value;
            }

            if (this.txtKey.Text.Trim() != string.Empty)
            {
                ischoose = true;
                term += "  and (prno like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%' or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                p1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p1);
            }
            #region 新增查询条件
            if (!string.IsNullOrEmpty(txtProjectCode.Text.Trim()))
            {
                term += string.Format(" and ProjectCode like '%{0}%'  ", txtProjectCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtRequestEmployeeName.Text.Trim()))
            {
                term += string.Format(" and RequestEmployeeName like '%{0}%'  ", txtRequestEmployeeName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBegin.Text.Trim()))
            {
                term += string.Format(" and '{0}' <= Convert(Char(10),RequestDate,120) ", txtBegin.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEnd.Text.Trim()))
            {
                term += string.Format(" and '{0}' >= Convert(Char(10),RequestDate,120) ", txtEnd.Text.Trim());
            }
            if (ddlDepartment3.SelectedValue != "-1")
            {
                term += " and departmentid=@GroupId";
                paramlist.Add(new SqlParameter("@GroupId", ddlDepartment3.SelectedValue));
            }
            else
            {
                if (ddlDepartment2.SelectedValue != "-1")
                {
                    term += @" and departmentid in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                where b.DepartmentID=@GroupID)";
                    paramlist.Add(new SqlParameter("@GroupId", ddlDepartment2.SelectedValue));
                }
                else
                {
                    if (ddlDepartment1.SelectedValue != "-1")
                    {
                        term += @" and departmentid in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                inner join sep_Departments as a on b.ParentID=a.DepartmentID
                                where a.DepartmentID=@GroupID)";
                        paramlist.Add(new SqlParameter("@GroupId", ddlDepartment1.SelectedValue));
                    }
                }
            }
            #endregion

            if (this.ddlStatus.SelectedIndex != 0)
            {
                ischoose = true;
                term += " and returnStatus=@returnStatus";
                System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
                p2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(p2);
            }

            if (this.ddlInvoice.SelectedIndex != 0)
            {
                ischoose = true;
                term += " and IsInvoice=@IsInvoice";
                System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@IsInvoice", System.Data.SqlDbType.Int, 4);
                p3.SqlValue = this.ddlInvoice.SelectedValue;
                paramlist.Add(p3);
            }

            if (ischoose == false)
            {
                term += " and RequestDate>'2024-1-1'";
            }

            int total;
            int offset = this.GridReturn.CurrentPageIndex * this.GridReturn.PageSize;
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist, offset, 20, out total).OrderByDescending(x => x.ReturnCode).ToList();

            var prs = returnlist.Where(x => x.PRID.HasValue).Select(x => x.PRID.Value).ToArray();
            InUses = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetInUses(prs);



            this.GridReturn.CallbackCachingEnabled = true;
            this.GridReturn.CallbackCacheLookAhead = 2;

            this.GridReturn.DataSource = returnlist;
            this.GridReturn.DataBind();
            this.GridReturn.RecordCount = total;

            labTotal.Text = returnlist.Sum(x => x.PreFee) == null ? "0.00" : returnlist.Sum(x => x.PreFee).Value.ToString("#,##0.00");
        }

        protected void GridReturn_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.DataItem;
            if (returnModel.IsInvoice != null)
            {
                if (returnModel.IsInvoice == 1)
                    e.Item["IsInvoice"] = "已开";
                else if (returnModel.IsInvoice == 0)
                    e.Item["IsInvoice"] = "未开";
                else
                    e.Item["IsInvoice"] = "无需发票";
            }
            if (userid.ToString() != ESP.Configuration.ConfigurationManager.SafeAppSettings["EddyBinID"])
            {
                e.Item["Choice"] = "";
            }

            //协议供应商PN付款，业务不需要打印
            if (returnModel.NeedPurchaseAudit == true)
            {
                e.Item["Print"] = "";
            }
            else
            {
                e.Item["Print"] = "<a target='_blank' href='/Purchase/Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";
            }

            e.Item["ReturnStatus"] = ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus ?? 0, 0, returnModel.IsDiscount);
           
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

            if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                e.Item["PRNO"] = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + returnModel.PRID.ToString() + "' style='cursor: hand' target='_blank'>" + returnModel.PRNo + "</a>";
            else
                e.Item["PRNO"] = returnModel.PRNo;
            e.Item["ViewAudit"] = "<a href=\"/project/ProjectWorkFlow.aspx?Type=return&FlowID=" + e.Item["ReturnID"].ToString() + "\" target=\"_blank\">" +
                                                       "<img src=\"/images/AuditStatus.gif\" border=\"0px;\" title=\"审批状态\" /></a>";
            e.Item["RequestEmployeeName"] = "<a onclick=\"showUserInfoAsync(" + (returnModel.RequestorID.Value) + ");\">" + e.Item["RequestEmployeeName"] + "</a>";

            //媒体稿费的单子
            //if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs) && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            //{
            //    e.Item["Attach"] = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + returnModel.MediaOrderIDs + "' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;'></img></a>";
            //    e.Item["Attach"] += "<a href='/Purchase/Print\\MediaUnPayment.aspx?OrderID=" + returnModel.MediaOrderIDs + "' style='cursor: hand' target='_blank'> <img title='未付款记者浏览' src='/images/PrintDefault.gif' border='0px;' ></img></a>";
            //}
            //else
            //{
             //   e.Item["Attach"] = "";
            //}
            //3000以下对私的单子有附件显示
            //if (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            //{
            //    e.Item["Attach"] = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\RequisitionPrint.aspx?id=" + returnModel.PRID.ToString() + "&viewButton=no&Action=ViewOldPr' style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;'></img></a>";
            //}
            //if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs) && (returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
            //{
            //    e.Item["Export"] = "<img src='/images/PrintDefault.gif' style='cursor:pointer'  onclick=\"window.open('/Dialogs/ExportFile.aspx?returnID=" + returnModel.ReturnID + "&commandName=Export');\" title='导出所有记者' border='0' />";
            //    e.Item["Export"] += "&nbsp;<img src='/images/PrintDefault.gif' style='cursor:pointer'  onclick=\"window.open('/Dialogs/ExportFile.aspx?returnID=" + returnModel.ReturnID + "&commandName=Journalist');\" title='导出未付款记者' border='0' />";
            //}
            //else
            //{
              //  e.Item["Export"] = "";
            //}
            //重汇判断
            if (returnModel.ReturnStatus != (int)PaymentStatus.FinanceComplete || this.GetFinanceUser(returnModel).Contains(userid) == false || returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift)
            {
                e.Item["RePay"] = "";
               // e.Item["ReFund"] = "";
            }
            else
            {
                e.Item["RePay"] = "<a href='/Purchase/FinanceRePay.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() + "' style='cursor: hand'><img title='重汇' src='/images/Edit.gif' border='0px;' ></img></a>";
               // e.Item["ReFund"] = "<a href='/Purchase/FinanceReFund.aspx?" + RequestName.ReturnID + "=" + returnModel.ReturnID.ToString() + "' style='cursor: hand'><img title='退款' src='/images/Audit.gif' border='0px;' ></img></a>";
            }

            //如果申请单暂停，不能重汇付款申请
            ESP.Purchase.Common.State.PRInUse inuse;
            if (returnModel.PRID.HasValue && InUses.TryGetValue(returnModel.PRID.Value, out inuse) && inuse != ESP.Purchase.Common.State.PRInUse.Use)
            {
                e.Item["RePay"] = "";
               // e.Item["ReFund"] = "";
            }
        }

        private int[] GetFinanceUser(ESP.Finance.Entity.ReturnInfo returnModel)
        {
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditer = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" returnid =" + returnModel.ReturnID.ToString() + " and auditoruserid =" + CurrentUserID.ToString());
            if (branches == null)
                branches = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            var branchCode = returnModel.ProjectCode.Substring(0, 1);
            var branch = branches.Where(x => x.BranchCode == branchCode).FirstOrDefault();
            if (branch != null)
            {
                if (auditer == null || auditer.Count == 0)
                    return new int[] { branch.FirstFinanceID, branch.FinalAccounter };
                else
                    return new int[] { branch.FirstFinanceID, branch.FinalAccounter, CurrentUserID };
            }
            return new int[0];

            //string users = "," + branch.FirstFinanceID.ToString() + "," + branch.FinalAccounter.ToString() + ",";
            //return users;
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnDiscount_OnClick(object sender, EventArgs e)
        {
            string returnids = hidWorkItemID.Value;
            if (!string.IsNullOrEmpty(returnids) && returnids.Split(',').Length > 0)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = null;

                int returnid = 0;
                for (int i = 0; i < returnids.Split(',').Length; i++)
                {
                    returnid = Convert.ToInt32(returnids.Split(',')[i]);
                    if (returnid > 0)
                    {
                        returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
                        returnModel.IsDiscount = true;
                        returnModel.DiscountDate = DateTime.Now;

                        ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);

                        ESP.Finance.Entity.AuditLogInfo log = new ESP.Finance.Entity.AuditLogInfo();
                        log.AuditDate = DateTime.Now;
                        log.AuditorEmployeeName = CurrentUser.Name;
                        log.AuditorSysID = Convert.ToInt32(CurrentUser.SysID);
                        log.AuditorUserCode = CurrentUser.ID;
                        log.AuditorUserName = CurrentUser.ITCode;
                        log.AuditStatus = (int)ESP.Finance.Utility.AuditHistoryStatus.PassAuditing;
                        log.FormID = returnModel.ReturnID;
                        log.FormType = (int)ESP.Finance.Utility.FormType.Return;
                        log.Suggestion = "抵扣操作";
                        ESP.Finance.BusinessLogic.AuditLogManager.Add(log);
                    }
                }

                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('抵扣操作成功!');", true);

            }
        }

        protected string getIsDiscount(string returnId, decimal factFee)
        {
            return "<input type=\"checkbox\" id=\"chkAudit\" name=\"chkAudit\" value='" + returnId + "' OnClick='CalSelected(this.checked," + factFee + "," + returnId + ");'/>";
        }

        protected void BindInfo()
        {
            BindDepartment(ddlDepartment1, 0);
            ddlDepartment2.Items.Insert(0, new ListItem("请选择", "-1"));
            ddlDepartment3.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        private void BindDepartment(DropDownList ddl, int parentId)
        {
            ddl.DataSource = ESP.Compatible.DepartmentManager.GetByParent(parentId);
            ddl.DataTextField = "NodeName";
            ddl.DataValueField = "UniqID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        protected void ddlDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartment(ddlDepartment2, int.Parse(ddlDepartment1.SelectedValue));
            ddlDepartment3.Items.Clear();
            ddlDepartment3.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        protected void ddlDepartment2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDepartment3.Items.Clear();
            BindDepartment(ddlDepartment3, int.Parse(ddlDepartment2.SelectedValue));
        }
    }
}
