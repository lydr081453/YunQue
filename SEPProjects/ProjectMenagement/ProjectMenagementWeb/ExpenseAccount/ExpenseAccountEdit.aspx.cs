using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;
using ESP.Framework.BusinessLogic;

namespace FinanceWeb.ExpenseAccount
{
    public partial class ExpenseAccountEdit : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ReturnInfo model = null;
        ESP.Finance.Entity.ProjectInfo project = null;
        private List<ESP.Finance.Entity.ExpenseTypeInfo> typeInfo;
        int id = 0;  //单据ID
        int detailid = 0;   //单据明细ID
        int typeid = 0;   //费用类型ID
        int detailRowCount = 0;   //明细条数
        bool typeIsMatch = false;
        ESP.Administrative.Entity.UserAttBasicInfo userBasicModel;
        int ReturnType = 0;  //单据类型

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.ClientScript.RegisterStartupScript(typeof(ScriptManager), "AppInitialize", "", true);
            typeInfo = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetList(" and status = 1 ");
            userBasicModel = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetModelByUserid(UserID);
            //报销单类型
            if (!string.IsNullOrEmpty(Request["ReturnType"]))
            {
                ReturnType = Convert.ToInt32(Request["ReturnType"]);
            }
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.ExpenseAccount.ExpenseAccountEdit));
            //现金借款冲销 借款冲销单
            if (ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff && ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
            {
                if (CurrentUser.DimissionStatus == ESP.HumanResource.Common.Status.DimissionFinanceAudit)
                {
                    Response.Redirect("/Edit/OOPTabEdit.aspx");
                }
            }
            //第三方报销不需要填写银行账号
            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
            {
                this.panBank.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = int.Parse(Request["id"].ToString());
            }
            if (!string.IsNullOrEmpty(Request["detailid"]))
            {
                detailid = int.Parse(Request["detailid"].ToString());

            }


            if (id > 0)
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
                ReturnType = model.ReturnType.Value;

                //第三方报销不需要填写银行账号
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    this.panBank.Visible = false;
                }

            }

            if (!IsPostBack)
            {

                btnAuditCancel.Visible = false;
                btnAuditConfirm.Visible = false;
                btnAuditReturn.Visible = false;
                this.lblSuggestion.Visible = false;
                this.txtSuggestion.Visible = false;
                this.panTicketCancel.Visible = false;

                BindInfo();

                #region 物料树绑定

                initTree("");
                #endregion
            }

        }

        protected void initTree(string key)
        {
            System.Web.UI.WebControls.TreeNode rootNode = new System.Web.UI.WebControls.TreeNode();
            rootNode.Text = "报销费用类型结构图";
            rootNode.Expanded = true;
            rootNode.ImageUrl = "images/treeview/root.gif";

            userTreeView.Nodes.Add(rootNode);
            int n = 0;
            //List<ESP.Finance.Entity.ExpenseTypeInfo> typeInfo = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetList(" and status = 1 " + conditionStr == "" ? "" : conditionStr);
            var typelist = typeInfo.Where(x => x.ExpenseType.Contains(txtNodeName.Text) || x.TypeLevel == 1);
            string str = "";
            bool isP = false;
            bool isHerf = false;
            // 开始绑定物料节点
            BindTree(typelist.ToList(), rootNode, 0, ref  n, str, isP, isHerf);
            userTreeView.Nodes[0].Expand();
        }


        protected void bthSearchNode_Click(object sender, EventArgs e)
        {
            userTreeView.Nodes.Clear();
            if (!string.IsNullOrEmpty(txtNodeName.Text.Trim()))
            {
                initTree(txtNodeName.Text.Trim());
            }
            else
            {
                initTree("");
            }
        }

        protected void btnClearNode_Click(object sender, EventArgs e)
        {
            userTreeView.Nodes.Clear();
            txtNodeName.Text = "";
            initTree("");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='ExpenseAccountEdit.aspx?ReturnType=" + Request["ReturnType"] + "&id=" + Request["id"] + "&backurl=" + Request["backurl"] + "';", true);
        }

        /// <summary>
        /// 物料树绑定
        /// </summary>
        /// <param name="typeinfo"></param>
        /// <param name="parentNode"></param>
        /// <param name="Id"></param>
        /// <param name="n"></param>
        /// <param name="treenode"></param>
        /// <param name="isP"></param>
        /// <param name="isHerf"></param>
        protected void BindTree(IList<ESP.Finance.Entity.ExpenseTypeInfo> typeinfo, System.Web.UI.WebControls.TreeNode parentNode, int Id, ref int n, string treenode, bool isP, bool isHerf)
        {
            System.Web.UI.WebControls.TreeNode tn = null;

            foreach (ESP.Finance.Entity.ExpenseTypeInfo info in typeinfo)
            {
                if (info.ParentID == Id)
                {
                    if (userBasicModel.AttendanceType != ESP.Administrative.Common.Status.UserBasicAttendanceType_Special)
                    {
                        if (info.TypeLevel == 2)
                        {
                            tn = new System.Web.UI.WebControls.TreeNode();
                            tn.Text = info.ExpenseType;
                            tn.Value = info.ID.ToString();
                            tn.ImageUrl = "images/treeview/dept.gif";

                            //tn.NavigateUrl = "ExpenseAccountEdit.aspx?id=" + id + "&typeid=" + info.ID;
                            //tn.AutoPostBackOnSelect = true;
                        }
                        else
                        {
                            tn = new System.Web.UI.WebControls.TreeNode();
                            tn.Text = info.ExpenseType;
                            tn.Value = info.ID.ToString();
                            tn.ImageUrl = "images/treeview/corp.gif";

                        }
                        n++;
                        tn.ToolTip = info.ExpenseType;
                        parentNode.ChildNodes.Add(tn);
                        parentNode.CollapseAll();
                        BindTree(typeinfo, tn, info.ID, ref n, treenode, isP, isHerf);
                    }
                    else
                    {
                        //总监以上没有OT餐费的报销
                        if (info.ExpenseType.Equals("OT餐费"))
                        {
                            continue;
                        }
                        if (info.TypeLevel == 2)
                        {
                            tn = new System.Web.UI.WebControls.TreeNode();
                            tn.Text = info.ExpenseType;
                            tn.Value = info.ID.ToString();
                            tn.ImageUrl = "images/treeview/dept.gif";

                            //tn.NavigateUrl = "ExpenseAccountEdit.aspx?id=" + id + "&typeid=" + info.ID;
                            //tn.AutoPostBackOnSelect = true;
                        }
                        else
                        {
                            tn = new System.Web.UI.WebControls.TreeNode();
                            tn.Text = info.ExpenseType;
                            tn.Value = info.ID.ToString();
                            tn.ImageUrl = "images/treeview/corp.gif";
                        }
                        n++;
                        tn.ToolTip = info.ExpenseType;
                        parentNode.ChildNodes.Add(tn);

                        BindTree(typeinfo, tn, info.ID, ref n, treenode, isP, isHerf);
                    }
                }
            }

        }

        protected void userTreeView_NodeSelected(object sender, EventArgs e)
        {

            typeid = Convert.ToInt32(userTreeView.SelectedNode.Value);
            var mm = typeInfo.Where(x => x.ID == typeid).FirstOrDefault();
            if (mm.TypeLevel != 2)
                return;
            BindInfo();

            hidExpenseTypeId.Value = userTreeView.SelectedNode.Value;
            hidGasCostByKM.Value = ESP.Configuration.ConfigurationManager.SafeAppSettings["GasCostByKM"];
            labExpenseTypeName.Text = userTreeView.SelectedNode.Text;

            if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
            {
                txtExpenseDesc.Text =  drpPhoneYear.SelectedValue + "年" + drpPhoneMonth.SelectedValue + "月";
            }
            else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_OvertimeFare"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
            {
                txtExpenseDesc.Text = "";
            }
            else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
            {
                txtExpenseDesc.Text = "";
            }
            else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_GasCost"]))
            {
                labGasByKM.Text = "<font color='red'>公里数</font>";
                txtExpenseDesc.Text = "";
            }
            else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_OvertimeMeal"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
            {
                txtExpenseDesc.Text = "";
            }
            else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_CityTax"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
            {
                txtExpenseDesc.Text = "";
            }
            else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Bus"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
            {
                txtExpenseDesc.Text = "";
            }
            else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Food"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
            {
                txtExpenseDesc.Text = "";
            }
            else
            {
                labGasByKM.Text = "";
                txtExpenseDesc.Text = "";
            }
            ShowPan(Convert.ToInt32(userTreeView.SelectedNode.Value));

        }

        /// <summary>
        /// 绑定报销单信息
        /// </summary>
        protected void BindInfo()
        {
            //报销手机费所要选择的年份  只列出当前年和上一年
            int currentYear = DateTime.Now.Year;
            drpPhoneYear.Items.Clear();
            drpPhoneYear.Items.Add(new ListItem((currentYear - 1).ToString(), (currentYear - 1).ToString()));
            drpPhoneYear.Items.Add(new ListItem(currentYear.ToString(), currentYear.ToString()));
            drpPhoneYear.SelectedValue = currentYear.ToString();

            if (id > 0)
            {
                ShowPan(typeid);
                panDetailInfo.Visible = true;

                detailRowCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID =" + id).Count;

                if (detailRowCount > 0)
                {
                    btnOpenProject.Visible = false;
                    ddlDepartment.Enabled = false;

                }
                else
                {
                    btnOpenProject.Visible = true;
                    ddlDepartment.Enabled = true;

                }

                //btnSave.Text = " 保存 ";
                btnSetAuditor.Visible = true;
                table2.Visible = false;
            }
            else
            {
                panDetailInfo.Visible = false;
                //btnSave.Text = " 创建 ";
                btnSetAuditor.Visible = false;
                table2.Visible = true;
            }

            //绑定信息
            if (model != null)
            {
                //机票申请,自动带出机票的物料类别
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                {
                    int boarderId = 0;
                    if (!string.IsNullOrEmpty(Request["BoarderId"]) && Request["BoarderId"] != "0")
                        boarderId = int.Parse(Request["BoarderId"]);
                    else
                        boarderId = model.RequestorID.Value;
                    ESP.HumanResource.Entity.EmployeeBaseInfo employee = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(boarderId);
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(boarderId);
                    this.hidIDCard.Value = employee.IDNumber;
                    this.txtPhone.Text = employee.MobilePhone;
                    this.lblBoarder.Text = emp.Name;
                    this.hidBoarder.Value = emp.Name;
                    this.hidBoarderId.Value = boarderId.ToString();
                    int expenseTypeId = Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Ticket"]);
                    ESP.Finance.Entity.ExpenseTypeInfo typeModel = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(expenseTypeId);
                    if (typeModel != null)
                    {
                        this.labExpenseTypeName.Text = typeModel.ExpenseType;
                        this.hidExpenseTypeId.Value = typeModel.ID.ToString();
                    }
                    this.userTreeView.Enabled = false;
                    if (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit)
                    {
                        btnSetAuditor.Visible = false;
                        btnReturn.Visible = false;
                        btnAuditCancel.Visible = true;
                        btnAuditConfirm.Visible = true;
                        btnAuditReturn.Visible = true;
                        this.lblSuggestion.Visible = true;
                        this.txtSuggestion.Visible = true;
                    }
                    this.trTotalAmount.Visible = false;
                }

                BindList();

                labRequestUserName.Text = model.RequestEmployeeName;
                labRequestUserCode.Text = model.RequestUserCode;
                labRequestDate.Text = model.RequestDate.Value.ToString("yyyy-MM-dd");

                hidProejctId.Value = model.ProjectID.Value.ToString();

                txtproject_code1.Text = model.ProjectCode;

                hidProject_Code1.Value = model.ProjectCode;

                if (model.ProjectID != null && model.ProjectID != 0)
                {
                    txtproject_descripttion.Text = project.BusinessDescription;
                    hidProject_Description.Value = project.BusinessDescription;
                }

                //txtMemo.Text = model.ReturnContent;

                labPreFee.Text = hidPreFee.Value = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(id).ToString();

                //设置费用所属组
                string depts = "";
                int[] deptids = new ESP.Compatible.Employee(int.Parse(CurrentUser.SysID)).GetDepartmentIDs();
                if (model != null)
                {
                    if (model.ProjectID.Value != 0)
                    {
                        List<ESP.Purchase.Entity.V_GetProjectGroupList> list = ESP.Purchase.BusinessLogic.V_GetProjectGroupList.GetGroupListByPid(model.ProjectID.Value);
                        foreach (ESP.Purchase.Entity.V_GetProjectGroupList group in list)
                        {
                            //如果支持方是FEE，则可以选择主申请方的成本组
                            IList<ESP.Finance.Entity.SupporterInfo> supportList = ESP.Finance.BusinessLogic.SupporterManager.GetList("ProjectID=" + group.ProjectId.ToString() + " and GroupID=" + group.GroupID.ToString());
                            if (supportList != null && supportList.Count > 0)
                            {
                                if (supportList[0].IncomeType == "Fee")
                                {
                                    IList<ESP.Finance.Entity.SupportMemberInfo> memberList = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(" SupportID=" + supportList[0].SupportID.ToString() + " and MemberUserID=" + CurrentUser.SysID);
                                    if (memberList != null && memberList.Count > 0)
                                    {
                                        ESP.Finance.Entity.ProjectInfo ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supportList[0].ProjectID);
                                        depts += ProjectModel.GroupID + "," + ProjectModel.GroupName + "#";
                                    }
                                }
                            }

                            if (CurrentUser.SysID.Equals(System.Configuration.ConfigurationManager.AppSettings["HowardID"]))
                            {
                                depts += group.GroupID + "," + group.GroupName + "#";
                            }
                            else
                            {
                                if (ESP.Purchase.BusinessLogic.V_GetProjectList.MemberInProjectGroup(group.ProjectId, group.GroupID, int.Parse(CurrentUser.SysID)) || model.ProjectCode.Contains(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) || model.ProjectCode.Contains("-*GM-") || model.ProjectCode.Contains("-GM*-"))
                                    depts += group.GroupID + "," + group.GroupName + "#";
                            }
                        }
                    }
                    else
                    {
                        depts = model.DepartmentID + "," + model.DepartmentName;
                    }
                    if (string.IsNullOrEmpty(depts))
                    {
                        if (CurrentUser.SysID.Equals(System.Configuration.ConfigurationManager.AppSettings["HowardID"])
                            || OperationAuditManageManager.GetCurrentUserIsManager(CurrentUser.SysID)
                            || OperationAuditManageManager.GetCurrentUserIsDirector(CurrentUser.SysID)
                            || System.Configuration.ConfigurationManager.AppSettings["AdministrativeIDs"].IndexOf("," + deptids[0] + ",") >= 0
                            || System.Configuration.ConfigurationManager.AppSettings["SpecialDeptIDs"].IndexOf("," + deptids[0] + ",") >= 0
                            )
                        {
                            List<ESP.Purchase.Entity.V_GetProjectGroupList> list = ESP.Purchase.BusinessLogic.V_GetProjectGroupList.GetGroupListByPid(model.ProjectID.Value);
                            foreach (ESP.Purchase.Entity.V_GetProjectGroupList group in list)
                            {
                                depts += group.GroupID + "," + group.GroupName + "#";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(depts))
                {
                    ddlDepartment.Items.Clear();
                    string[] ids = depts.TrimEnd('#').Split('#');
                    for (int i = 0; i < ids.Length; i++)
                    {
                        ddlDepartment.Items.Insert(i, new ListItem(ids[i].Split(',')[1], ids[i].Split(',')[0]));
                    }
                    ddlDepartment.SelectedValue = model.DepartmentID.Value.ToString();
                    hidDeptId.Value = model.DepartmentID.Value.ToString() + "," + model.DepartmentName;
                    hidProejctIds.Value = depts.TrimEnd('#');

                }



                //报销手机费所要选择的月份 只列出没有报销的月份
                drpPhoneMonth.DataSource = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetPhoneMonthList(Convert.ToInt32(drpPhoneYear.SelectedValue), UserInfo.UserID, detailid);
                drpPhoneMonth.DataBind();

                //设置项目号下所包含的一级物料和二级物料
                string strTerms = " and a.projectId=@projectId and a.groupId = @groupId";
                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@projectId", SqlDbType.Int, 4));
                parms.Add(new SqlParameter("@groupId", SqlDbType.Int, 4));
                parms[0].Value = model.ProjectID;
                parms[1].Value = model.DepartmentID;

                List<ESP.Purchase.Entity.V_GetProjectTypeList> typelist = ESP.Purchase.BusinessLogic.V_GetProjectTypeList.GetList(strTerms, parms);
                ddlProjectType.Items.Clear();
                ddlProjectType.Items.Insert(0, new ListItem("OOP", "0"));
                //if (typelist.Count > 0 && typeid != 31 && typeid != 32 && typeid != 33 && typeid != 34 && typeid != 35) 
                if (typelist.Count > 0 && System.Configuration.ConfigurationManager.AppSettings["ExpenseTypeIDs"].IndexOf(typeid.ToString()) <= 0)
                {
                    ESP.Finance.Entity.ExpenseTypeInfo etype = null;
                    if (typeid > 0)
                    {
                        etype = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(typeid);
                    }

                    for (int i = 0; i < typelist.Count; i++)
                    {
                        ddlProjectType.Items.Insert(i + 1, new ListItem(typelist[i].typeName, typelist[i].TypeID.ToString()));
                        if (typeid > 0 && etype != null)
                        {
                            if (typelist[i].TypeID == etype.CostDetailID)
                            {
                                ddlProjectType.SelectedValue = etype.CostDetailID.ToString();
                                typeIsMatch = true;
                            }
                        }
                    }
                }
            }
            else
            {
                labRequestUserName.Text = CurrentUser.Name;
                labRequestUserCode.Text = CurrentUser.ID;
                labRequestDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                labPreFee.Text = hidPreFee.Value = "0";
            }

            //绑定个人银行卡号
            ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUser.IntID);
            txtCreatorBank.Text = empModel.SalaryBank;
            txtCreatorAccount.Text = empModel.SalaryCardNo;

            //绑定明细信息
            if (detailid > 0 && !string.IsNullOrEmpty(Request["op"]) && "detailedit".Equals(Request["op"]))
            {
                BindDetailInfo();
                btnAddDetail.Text = " 保存 ";
            }
            else if (detailid > 0 && !string.IsNullOrEmpty(Request["op"]) && "detailChange".Equals(Request["op"]))
            {
                BindDetailInfo();
                ticketEnable();
                this.btnNew.Visible = true;
                btnAddDetail.Text = " 保存 ";
            }
            else if (detailid > 0 && !string.IsNullOrEmpty(Request["op"]) && "detailCancel".Equals(Request["op"]))
            {
                BindDetailInfo();
                ticketEnable();
                this.btnNew.Visible = true;
                this.panTicketCancel.Visible = true;
                btnAddDetail.Text = " 保存 ";
            }
            else
            {
                btnAddDetail.Text = "添加至申请单";
            }

            hidTypeIsMatch.Value = typeIsMatch.ToString();
            if (model != null)
            {
                var auditLogs = ESP.Finance.BusinessLogic.AuditLogManager.GetOOPList(model.ReturnID);
                foreach (var log in auditLogs)
                {
                    labSuggestion.Text += log.AuditorEmployeeName + "(" + log.AuditorUserName + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorSysID.Value) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }
            }
        }

        private void ticketEnable()
        {
            this.txtAirNo.Enabled = false;
            this.txtAirTime.Enabled = false;
            this.txtSource.Enabled = false;
            this.txtDestination.Enabled = false;
            this.txtPrice.Enabled = false;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected int save(int isCommit)
        {
            //初始化return对象
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(Request["id"].ToString()));
            }
            else
            {
                model = new ESP.Finance.Entity.ReturnInfo();
            }

            //如果项目号变更过，则删除所有已存在的审核人列表
            if (!string.IsNullOrEmpty(model.ProjectCode))
            {
                if (!model.ProjectCode.Equals(hidProject_Code1.Value ) || model.PreFee.Value != ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID))
                {
                    try
                    {
                        ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(int.Parse(Request["id"].ToString()));
                    }
                    catch { }
                }
            }

            //收集信息
            int projectId = Convert.ToInt32(hidProejctId.Value);
            string projectCode = hidProject_Code1.Value ;

            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectCode.Substring(0, 1));
            model.ProjectID = projectId;
            model.ProjectCode = projectCode;
            model.BranchID = branchModel.BranchID;
            model.BranchCode = branchModel.BranchCode;
            model.ReturnContent = hidProject_Description.Value;

            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
            {
                string content = string.Empty;
                IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> details = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnId=" + model.ReturnID.ToString());
                if (details != null && details.Count != 0)
                {
                    foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo d in details)
                    {
                        content += d.Boarder + ":" + " " + d.TicketSource + "-" + d.TicketDestination + " " + d.ExpenseDesc + ";";
                    }
                    model.ReturnContent = content;
                    model.ReturnPreDate = DateTime.Now;
                    model.PreBeginDate = DateTime.Now;
                    model.PreEndDate = DateTime.Now;
                }
                else
                {
                    model.ReturnContent = hidProject_Description.Value;
                }
            }

            if (!string.IsNullOrEmpty(hidDeptId.Value))
            {
                model.DepartmentID = Convert.ToInt32(hidDeptId.Value.Split(',')[0]);
                model.DepartmentName = hidDeptId.Value.Split(',')[1];
            }


            if (!string.IsNullOrEmpty(hidPreFee.Value))
            {
                model.PreFee = decimal.Parse(hidPreFee.Value);
            }
            else
            {
                model.PreFee = 0;
            }

            //model.ReturnContent = txtMemo.Text.Trim();

            //设置申请单的类别
            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
            {
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount;
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
            {
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty;
            }

            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
            {
                IList<ESP.Finance.Entity.TicketSupplier> suplierList = ESP.Finance.BusinessLogic.TicketSupplierManager.GetList(" receptionid =" + CurrentUser.SysID, new List<SqlParameter>());
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket;
                if (suplierList != null && suplierList.Count > 0)
                {
                    model.SupplierName = suplierList[0].AccountName;
                    model.SupplierBankName = suplierList[0].BankName;
                    model.SupplierBankAccount = suplierList[0].AccountNo;
                }
                if (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit && isCommit == 1)
                {
                    model.ReturnStatus = (int)PaymentStatus.Ticket_ReceptionConfirm;
                    model.FactFee = model.PreFee;
                    ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                    log.AuditeDate = DateTime.Now;
                    log.AuditorEmployeeName = CurrentUser.Name;
                    log.AuditorUserID = int.Parse(CurrentUser.SysID);
                    log.AuditorUserCode = CurrentUser.ID;
                    log.AuditorUserName = CurrentUser.ITCode;
                    log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                    log.ExpenseAuditID = model.ReturnID;
                    log.Suggestion = this.txtSuggestion.Text;
                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                }
                if (Request["op"] == "detailCancel")
                {
                    model.ReturnStatus = (int)PaymentStatus.Rejected;
                }
            }


            if (!string.IsNullOrEmpty(Request["id"]))
            {
                if (ESP.Finance.BusinessLogic.ReturnManager.Update(model) == UpdateResult.Succeed)
                {
                    //普通报销更新个人银行信息
                    if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                    {
                        //更新当前申请人的个人工资卡信息
                        ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUser.IntID);
                        empModel.SalaryCardNo = txtCreatorAccount.Text;
                        empModel.SalaryBank = txtCreatorBank.Text;
                        ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empModel);
                    }
                    return model.ReturnID;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                model.RequestorID = Convert.ToInt32(CurrentUser.SysID);
                model.RequestUserCode = CurrentUser.ID;
                model.RequestUserName = CurrentUser.ITCode;
                model.RequestEmployeeName = CurrentUser.Name;
                model.RequestDate = DateTime.Now;

                model.ReturnStatus = (int)PaymentStatus.Created;

                 //普通报销更新个人银行信息
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                {
                    //更新当前申请人的个人工资卡信息
                    ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUser.IntID);
                    empModel.SalaryCardNo = txtCreatorAccount.Text;
                    empModel.SalaryBank = txtCreatorBank.Text;
                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empModel);
                }
                return ESP.Finance.BusinessLogic.ReturnManager.CreateReturnInFinance(model);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int returnid = save(0);
            if (returnid > 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "btnSave", "window.location='ExpenseAccountEdit.aspx" + Request.Url.Query + "';", true);
                return;
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            int returnid = save(0);
            if (returnid > 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "btnSave", "window.location='ExpenseAccountEdit.aspx?id=" + returnid.ToString() + "';", true);
                return;
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = int.Parse(Request["id"].ToString());
            }
            //报销单类型
            if (!string.IsNullOrEmpty(Request["ReturnType"]))
            {
                ReturnType = Convert.ToInt32(Request["ReturnType"]);
            }

            if (id > 0)
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
            }
            if (ReturnType == 40 || (model != null && model.ReturnType == 40))
            {
                Response.Redirect("/Edit/TicketReceipient.aspx");
            }
            else
            {
                string backUrl = Request[RequestName.BackUrl];
                if (!string.IsNullOrEmpty(backUrl))
                    Response.Redirect(backUrl);
                else
                    Response.Redirect("/Edit/OOPTabEdit.aspx");
            }
        }

        protected void btnReturn2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = int.Parse(Request["id"].ToString());
            }
            //报销单类型
            if (!string.IsNullOrEmpty(Request["ReturnType"]))
            {
                ReturnType = Convert.ToInt32(Request["ReturnType"]);
            }

            if (id > 0)
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
            }
            if (ReturnType == 40 || (model != null && model.ReturnType == 40))
            {
                Response.Redirect("/Edit/TicketReceipient.aspx");
            }
            else
            {
                string backUrl = Request[RequestName.BackUrl];
                if (!string.IsNullOrEmpty(backUrl))
                    Response.Redirect(backUrl);
                else
                    Response.Redirect("/Edit/OOPTabEdit.aspx");
            }
        }

        protected void btnSetAuditor_Click(object sender, EventArgs e)
        {
            int returnid = save(1);
            if (returnid > 0)
            {
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket && model.ReturnStatus > 2)
                {
                    string backUrl = Request[RequestName.BackUrl];
                    if (!string.IsNullOrEmpty(backUrl))
                        Response.Redirect(backUrl);
                    else
                        Response.Redirect("/Edit/TicketReceipient.aspx");
                }
                else
                {
                    Response.Redirect("SetAuditor2.aspx?id=" + returnid);
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }

        }

        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            string query = "id=" + Request["id"].ToString();

            DateTime dtExpense = string.IsNullOrEmpty(txtExpenseDate.Text) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(txtExpenseDate.Text);
            if (dtExpense.AddMonths(1) < DateTime.Now)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('费用发生日期已经超过一个月，无法提交!');", true);
                return;
            }
            else
            {
                save(0);

                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;
                    if (detailid > 0)
                    {
                        detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(detailid);
                        detail.ReturnID = Convert.ToInt32(Request["id"]);
                        detail.ExpenseDate = string.IsNullOrEmpty(txtExpenseDate.Text) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(txtExpenseDate.Text);
                        detail.ExpenseType = Convert.ToInt32(hidExpenseTypeId.Value);
                        detail.ExpenseDesc = txtExpenseDesc.Text;
                        detail.ExpenseMoney = txtExpenseMoney.Text == null ? 0 : Convert.ToDecimal(txtExpenseMoney.Text);
                        detail.CostDetailID = Convert.ToInt32(ddlProjectType.SelectedValue);
                        detail.ExpenseTypeNumber = txtNumber.Value == null ? 1 : Convert.ToInt32(txtNumber.Value);
                        detail.TicketStatus = 0;


                        if (detail.ExpenseType == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_BusinessTrip"]))
                        {
                            if (chkMealFee1.Checked)
                            {
                                detail.MealFeeMorning = 1;
                            }
                            else
                            {
                                detail.MealFeeMorning = 0;
                            }
                            if (chkMealFee2.Checked)
                            {
                                detail.MealFeeNoon = 1;
                            }
                            else
                            {
                                detail.MealFeeNoon = 0;
                            }
                            if (chkMealFee3.Checked)
                            {
                                detail.MealFeeNight = 1;
                            }
                            else
                            {
                                detail.MealFeeNight = 0;
                            }
                        }
                        else
                        {
                            detail.MealFeeMorning = 0;
                            detail.MealFeeNoon = 0;
                            detail.MealFeeNight = 0;
                        }
                        if (detail.ExpenseType == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_Cellphone"]))
                        {
                            detail.PhoneYear = Convert.ToInt32(drpPhoneYear.SelectedValue);
                            detail.PhoneMonth = Convert.ToInt32(drpPhoneMonth.SelectedValue);
                            detail.PhoneInvoice = Convert.ToInt32(ddlPhoneInvoice.SelectedValue);

                            string invoiceNo = string.Empty;

                            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                            {
                                invoiceNo += txtInvoiceNo.Text;

                                int invoiceCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CheckPhoneInvoiceNo(detail.ID, txtInvoiceNo.Text);

                                if (detail.PhoneInvoice == 2)
                                {
                                    if (string.IsNullOrEmpty(txtInvoiceNo.Text) || txtInvoiceNo.Text.Trim().Length != 8)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('请输入正确的手机电子发票号(第一张)！');", true);
                                        return;
                                    }

                                    if (invoiceCount > 0)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('您输入的第一张手机电子发票号已经使用过！');", true);
                                        return;
                                    }

                                }
                            }
                            if (!string.IsNullOrEmpty(txtInvoiceNo2.Text))
                            {
                                invoiceNo += "," + txtInvoiceNo2.Text;
                                int invoiceCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CheckPhoneInvoiceNo(detail.ID, txtInvoiceNo2.Text);

                                if (detail.PhoneInvoice == 2)
                                {
                                    if (string.IsNullOrEmpty(txtInvoiceNo2.Text) || txtInvoiceNo2.Text.Trim().Length != 8)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('请输入正确的手机电子发票号(第二张)！');", true);
                                        return;
                                    }

                                    if (invoiceCount > 0)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('您输入的第二张手机电子发票号已经使用过！');", true);
                                        return;
                                    }

                                }
                            }
                            if (!string.IsNullOrEmpty(txtInvoiceNo3.Text))
                            {
                                invoiceNo += "," + txtInvoiceNo3.Text;

                                int invoiceCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CheckPhoneInvoiceNo(detail.ID, txtInvoiceNo3.Text);

                                if (detail.PhoneInvoice == 2)
                                {
                                    if (string.IsNullOrEmpty(txtInvoiceNo3.Text) || txtInvoiceNo3.Text.Trim().Length != 8)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('请输入正确的手机电子发票号(第三张)！');", true);
                                        return;
                                    }

                                    if (invoiceCount > 0)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('您输入的第三张手机电子发票号已经使用过！');", true);
                                        return;
                                    }

                                }
                            }

                            detail.PhoneInvoiceNo = invoiceNo;



                        }
                        else
                        {
                            detail.PhoneYear = 0;
                            detail.PhoneMonth = 0;
                        }

                        if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                        {
                            detail.Recipient = txtRecipient.Text.Trim();
                            detail.City = txtCity.Text.Trim();
                            detail.BankName = txtBankName.Text.Trim();
                            detail.BankAccountNo = txtBankAccountNo.Text.Trim();
                        }

                        if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                        {
                            detail.GoAirNo = this.txtAirNo.Text;
                            detail.GoAmount = Convert.ToDecimal(this.txtPrice.Text);
                            detail.TicketDestination = this.txtDestination.Text;
                            detail.TicketSource = this.txtSource.Text;
                            detail.TicketStatus = 0;
                            detail.BoarderId = int.Parse(this.hidBoarderId.Value);
                            detail.Boarder = this.hidBoarder.Value;
                            detail.BoarderIDCard = this.hidIDCard.Value;
                            detail.BoarderMobile = this.txtPhone.Text; ;
                            detail.ExpenseMoney = Convert.ToDecimal(this.txtPrice.Text);
                            detail.ExpenseDesc = detail.ExpenseDesc = this.txtRemark.Text;
                            detail.ExpenseDate = Convert.ToDateTime(this.txtAirTime.Text);
                            detail.ExpenseTypeNumber = 1;
                            query += "&BoarderId=" + hidBoarderId.Value;
                            if (Request["op"] == "detailCancel")
                            {
                                detail.TicketStatus = 1;
                                ESP.Finance.Entity.ExpenseAccountDetailInfo subdetail = new ESP.Finance.Entity.ExpenseAccountDetailInfo();
                                subdetail.ReturnID = id;
                                subdetail.ExpenseDate = DateTime.Now;
                                subdetail.ExpenseType = Convert.ToInt32(hidExpenseTypeId.Value);
                                subdetail.ExpenseDesc = detail.GoAirNo + "退票费";
                                subdetail.ExpenseMoney = Convert.ToDecimal(this.txtCancelPrice.Text);
                                subdetail.ExpenseTypeNumber = 1;

                                subdetail.CostDetailID = Convert.ToInt32(ddlProjectType.SelectedValue);
                                subdetail.Creater = Convert.ToInt32(CurrentUser.SysID);
                                subdetail.CreaterName = CurrentUser.Name;
                                subdetail.CreateTime = DateTime.Now;
                                subdetail.Status = 1;
                                subdetail.ParentId = detail.ID;
                                subdetail.TicketSource = detail.TicketSource;
                                subdetail.TicketDestination = detail.TicketDestination;
                                subdetail.GoAirNo = detail.GoAirNo;
                                subdetail.GoAmount = detail.GoAmount;
                                subdetail.BoarderId = detail.BoarderId;
                                subdetail.Boarder = detail.Boarder;
                                subdetail.BoarderIDCard = detail.BoarderIDCard;
                                subdetail.BoarderMobile = detail.BoarderMobile;
                                subdetail.TicketStatus = 0;
                                subdetail.TripType = 2;
                                ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Add(subdetail);

                                ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                                log.AuditeDate = DateTime.Now;
                                log.AuditorEmployeeName = CurrentUser.Name;
                                log.AuditorUserID = int.Parse(CurrentUser.SysID);
                                log.AuditorUserCode = CurrentUser.ID;
                                log.AuditorUserName = CurrentUser.ITCode;
                                log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Submit;
                                log.ExpenseAuditID = model.ReturnID;
                                log.Suggestion = detail.GoAirNo + "退票操作";
                                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                            }
                        }
                        if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Update(detail) == UpdateResult.Succeed)
                        {
                            model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                            // if the order type is air ticket(40),then compare with the original amount
                            // if the amount over the original amount than 10% then reaudit.
                            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                            {
                                query += "&" + RequestName.BackUrl + "=TicketList.aspx";
                                decimal oldPrefee = model.FactFee == null ? 0 : model.FactFee.Value * decimal.Parse("1.1");
                                if ((model.FactFee != null && model.FactFee.Value > 0) && (model.PreFee.Value > oldPrefee))
                                {
                                    model.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Save;

                                    ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                                    log.AuditeDate = DateTime.Now;
                                    log.AuditorEmployeeName = CurrentUser.Name;
                                    log.AuditorUserID = int.Parse(CurrentUser.SysID);
                                    log.AuditorUserCode = CurrentUser.ID;
                                    log.AuditorUserName = CurrentUser.ITCode;
                                    log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Submit;
                                    log.ExpenseAuditID = model.ReturnID;
                                    log.Suggestion = "金额调整超过10%";
                                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                                }
                                else if ((model.FactFee != null && model.FactFee.Value > 0) && model.PreFee.Value != model.FactFee.Value)
                                {
                                    model.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm;

                                    ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                                    log.AuditeDate = DateTime.Now;
                                    log.AuditorEmployeeName = CurrentUser.Name;
                                    log.AuditorUserID = int.Parse(CurrentUser.SysID);
                                    log.AuditorUserCode = CurrentUser.ID;
                                    log.AuditorUserName = CurrentUser.ITCode;
                                    log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Submit;
                                    log.ExpenseAuditID = model.ReturnID;
                                    log.Suggestion = "金额调整10%以内";
                                    ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                                }
                            }
                            ESP.Finance.BusinessLogic.ReturnManager.Update(model);
                            try
                            {
                                ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(model.ReturnID);
                            }
                            catch { }

                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='ExpenseAccountEdit.aspx?" + query + "';", true);
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('数据保存失败，请重试！');", true);
                        }
                    }
                    else
                    {
                        if (hidExpenseTypeId.Value == "" || hidExpenseTypeId.Value == "-1" || hidExpenseTypeId.Value == "0")
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('请在左侧类型树中选择费用类型！');", true);
                                     
                            return;
                        }
                        detail = new ESP.Finance.Entity.ExpenseAccountDetailInfo();
                        detail.ReturnID = id;
                        detail.ExpenseDate = string.IsNullOrEmpty(txtExpenseDate.Text) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(txtExpenseDate.Text);
                        detail.ExpenseType = Convert.ToInt32(hidExpenseTypeId.Value);
                        detail.ExpenseDesc = txtExpenseDesc.Text;
                        detail.ExpenseMoney = txtExpenseMoney.Text == null ? 0 : Convert.ToDecimal(txtExpenseMoney.Text);
                        detail.ExpenseTypeNumber = txtNumber.Value == null ? 1 : Convert.ToInt32(txtNumber.Value);

                        detail.CostDetailID = Convert.ToInt32(ddlProjectType.SelectedValue);
                        detail.Creater = Convert.ToInt32(CurrentUser.SysID);
                        detail.CreaterName = CurrentUser.Name;
                        detail.CreateTime = DateTime.Now;
                        detail.Status = 1;
                        if (detail.ExpenseType == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_BusinessTrip"]))
                        {
                            if (chkMealFee1.Checked)
                            {
                                detail.MealFeeMorning = 1;
                            }
                            else
                            {
                                detail.MealFeeMorning = 0;
                            }
                            if (chkMealFee2.Checked)
                            {
                                detail.MealFeeNoon = 1;
                            }
                            else
                            {
                                detail.MealFeeNoon = 0;
                            }
                            if (chkMealFee3.Checked)
                            {
                                detail.MealFeeNight = 1;
                            }
                            else
                            {
                                detail.MealFeeNight = 0;
                            }
                        }
                        if (detail.ExpenseType == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_Cellphone"]))
                        {
                            detail.PhoneYear = Convert.ToInt32(drpPhoneYear.SelectedValue);
                            detail.PhoneMonth = Convert.ToInt32(drpPhoneMonth.SelectedValue);
                            detail.PhoneInvoice = Convert.ToInt32(ddlPhoneInvoice.SelectedValue);


                            string invoiceNo = string.Empty;

                            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                            {
                                invoiceNo += txtInvoiceNo.Text;

                                int invoiceCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CheckPhoneInvoiceNo(detail.ID, txtInvoiceNo.Text);

                                if (detail.PhoneInvoice == 2)
                                {
                                    if (string.IsNullOrEmpty(txtInvoiceNo.Text) || txtInvoiceNo.Text.Trim().Length != 8)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('请输入正确的手机电子发票号(第一张)！');", true);
                                        return;
                                    }

                                    if (invoiceCount > 0)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('您输入的第一张手机电子发票号已经使用过！');", true);
                                        return;
                                    }

                                }
                            }
                            if (!string.IsNullOrEmpty(txtInvoiceNo2.Text))
                            {
                                invoiceNo += "," + txtInvoiceNo2.Text;
                                int invoiceCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CheckPhoneInvoiceNo(detail.ID, txtInvoiceNo2.Text);

                                if (detail.PhoneInvoice == 2)
                                {
                                    if (string.IsNullOrEmpty(txtInvoiceNo2.Text) || txtInvoiceNo2.Text.Trim().Length != 8)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('请输入正确的手机电子发票号(第二张)！');", true);
                                        return;
                                    }

                                    if (invoiceCount > 0)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('您输入的第二张手机电子发票号已经使用过！');", true);
                                        return;
                                    }

                                }
                            }
                            if (!string.IsNullOrEmpty(txtInvoiceNo3.Text))
                            {
                                invoiceNo += "," + txtInvoiceNo3.Text;

                                int invoiceCount = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.CheckPhoneInvoiceNo(detail.ID, txtInvoiceNo3.Text);

                                if (detail.PhoneInvoice == 2)
                                {
                                    if (string.IsNullOrEmpty(txtInvoiceNo3.Text) || txtInvoiceNo3.Text.Trim().Length != 8)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('请输入正确的手机电子发票号(第三张)！');", true);
                                        return;
                                    }

                                    if (invoiceCount > 0)
                                    {
                                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('您输入的第三张手机电子发票号已经使用过！');", true);
                                        return;
                                    }

                                }
                            }

                            detail.PhoneInvoiceNo = invoiceNo;


                          
                        }

                        if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                        {
                            detail.Recipient = txtRecipient.Text.Trim();
                            detail.City = txtCity.Text.Trim();
                            detail.BankName = txtBankName.Text.Trim();
                            detail.BankAccountNo = txtBankAccountNo.Text.Trim();
                        }

                        if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                        {
                            ESP.HumanResource.Entity.EmployeeBaseInfo employee = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(hidBoarderId.Value));
                            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidBoarderId.Value));
                            detail.GoAirNo = this.txtAirNo.Text;
                            detail.GoAmount = Convert.ToDecimal(this.txtPrice.Text);
                            detail.ExpenseDate = Convert.ToDateTime(this.txtAirTime.Text);
                            detail.TicketDestination = this.txtDestination.Text;
                            detail.TicketSource = this.txtSource.Text;
                            detail.TicketStatus = 0;
                            detail.BoarderId = string.IsNullOrEmpty(emp.SysID) ? 0 : int.Parse(emp.SysID);
                            detail.Boarder = hidBoarder.Value;
                            detail.BoarderIDCard = this.hidIDCard.Value;
                            detail.BoarderMobile = this.txtPhone.Text;
                            detail.ExpenseMoney = Convert.ToDecimal(this.txtPrice.Text);
                            detail.ExpenseDesc = this.txtRemark.Text;
                            detail.ExpenseTypeNumber = 1;
                            query += "&BoarderId=" + hidBoarderId.Value;
                            query += "&" + RequestName.BackUrl + "=TicketList.aspx";
                        }

                        if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Add(detail) > 0)
                        {
                            model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                            ESP.Finance.BusinessLogic.ReturnManager.Update(model);

                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='ExpenseAccountEdit.aspx?" + query + "';", true);
                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('数据保存失败，请重试！');", true);
                        }

                    }
                }
            }

        }


        #region 前台审批
        protected void btnAuditConfirm_Click(object sender, EventArgs e)
        {
            btnSetAuditor_Click(sender, e);
        }

        protected void btnAuditCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(Request["id"].ToString()));
            }
            if (model != null)
            {
                model.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Save;
                ESP.Finance.BusinessLogic.ReturnManager.Update(model);
                ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                log.AuditeDate = DateTime.Now;
                log.AuditorEmployeeName = CurrentUser.Name;
                log.AuditorUserID = int.Parse(CurrentUser.SysID);
                log.AuditorUserCode = CurrentUser.ID;
                log.AuditorUserName = CurrentUser.ITCode;
                log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Reject;
                log.ExpenseAuditID = model.ReturnID;
                log.Suggestion = this.txtSuggestion.Text;
                ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                string backUrl = Request[RequestName.BackUrl];
                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核驳回操作成功！');window.location='" + backUrl + "';", true);

            }
        }

        protected void btnAuditReturn_Click(object sender, EventArgs e)
        {
            btnReturn2_Click(sender, e);
        }
        #endregion

        #region 报销明细列表

        /// <summary>
        /// 报销明细列表绑定
        /// </summary>
        protected void BindList()
        {
            List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
            if (id > 0)
            {
                list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + id);
            }
            else
            {
                list = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
            }
            gvG.DataSource = list;
            gvG.DataBind();

            txtNumber.Value = 1;
            if (list.Count > 0 && !IsPostBack)
            {
                txtRecipient.Text = list[0].Recipient;
                txtBankName.Text = list[0].BankName;
                txtBankAccountNo.Text = list[0].BankAccountNo;
                txtCity.Text = list[0].City;

                hidGasCostByKM.Value = ESP.Configuration.ConfigurationManager.SafeAppSettings["GasCostByKM"];
                labExpenseTypeName.Text = typeInfo.Where(x => x.ID == list[list.Count - 1].ExpenseType.Value).FirstOrDefault().ExpenseType;
                hidExpenseTypeId.Value = list[list.Count - 1].ExpenseType.Value.ToString();

                if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
                {
                    txtExpenseDesc.Text = "手机费:" + drpPhoneYear.SelectedValue + "年" + drpPhoneMonth.SelectedValue + "月";
                }
                else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_OvertimeFare"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
                {
                    txtExpenseDesc.Text = "OT车费:";
                }
                else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
                {
                    txtExpenseDesc.Text = "出差餐费:";
                }
                else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_GasCost"]))
                {
                    labGasByKM.Text = "<font color='red'>公里数</font>";
                    txtExpenseDesc.Text = "汽油费:";
                }
                else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_OvertimeMeal"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
                {
                    txtExpenseDesc.Text = "OT餐费:";
                    lblNotify.Text = "自2011年2月1日起，员工OT个人餐补增至50元/餐";
                }
                else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_CityTax"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
                {
                    txtExpenseDesc.Text = "市内出租车费:";
                }
                else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Bus"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
                {
                    txtExpenseDesc.Text = "车费:";
                }
                else if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Food"]) && string.IsNullOrEmpty(txtExpenseDesc.Text.Trim()))
                {
                    txtExpenseDesc.Text = "餐费:";
                }
                else
                {
                    labGasByKM.Text = "";
                    txtExpenseDesc.Text = "";
                }
                ShowPan(Convert.ToInt32(hidExpenseTypeId.Value));
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            BindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hylCancel = (LinkButton)e.Row.FindControl("hylCancel");
                LinkButton hylEdit = (LinkButton)e.Row.FindControl("hylEdit");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                ESP.Finance.Entity.ExpenseAccountDetailInfo detail = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Row.DataItem;
                ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(detail.CostDetailID.Value);

                Label labCostDetailName = (Label)e.Row.FindControl("labCostDetailName");
                if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                    labCostDetailName.Text = projectType.typename;
                else
                    labCostDetailName.Text = "OOP";


                Label labExpenseTypeName = (Label)e.Row.FindControl("labExpenseTypeName");
                string expenseTypeName = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
                if (detail.ExpenseType.Value == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_BusinessTrip"]))
                {
                    labExpenseTypeName.Text = expenseTypeName + "(";
                    labExpenseTypeName.Text += detail.MealFeeMorning != null && detail.MealFeeMorning == 1 ? "早餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNoon != null && detail.MealFeeNoon == 1 ? "午餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNight != null && detail.MealFeeNight == 1 ? "晚餐" : "";
                    labExpenseTypeName.Text += ")";
                }
                else if (detail.ExpenseType.Value == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_Cellphone"]))
                {
                    string mobileInfo = string.Empty;
                    ESP.Finance.Entity.MobileListInfo mobile = ESP.Finance.BusinessLogic.MobileListManager.GetModel(detail.Creater.Value);
                    if (mobile != null)
                    {
                        mobileInfo = "<font color='red'>从 " + mobile.EndDate.Year + "年" + mobile.EndDate.Month + "月起享受话费补贴</font>";
                    }
                    labExpenseTypeName.Text = expenseTypeName + "(" + detail.PhoneYear + "年" + detail.PhoneMonth + "月) " + mobileInfo;
                }
                else
                {
                    labExpenseTypeName.Text = expenseTypeName;
                }

                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    Label labRecipient = (Label)e.Row.FindControl("labRecipient");
                    Label labCity = (Label)e.Row.FindControl("labCity");
                    Label labBankName = (Label)e.Row.FindControl("labBankName");
                    Label labBankAccountNo = (Label)e.Row.FindControl("labBankAccountNo");

                    labRecipient.Text = detail.Recipient;
                    labCity.Text = detail.City;
                    labBankName.Text = detail.BankName;
                    labBankAccountNo.Text = detail.BankAccountNo;
                }
                //机票申请
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                {
                    if (detail.TicketStatus != 0)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                    if (detail.TripType != 0)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    if (model != null && (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm))
                    {
                        btnDelete.Visible = false;//delete
                    }
                    else
                    {
                        btnDelete.Visible = true;
                    }

                    if (model != null && (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm))
                    {
                        hylCancel.Visible = true;
                    }
                    else
                    {
                        hylCancel.Visible = false;
                    }


                    //已经被改签，或改签费用，退票费用不需要操作
                    if (detail.TicketStatus == 1 || detail.TripType == 1 || detail.TripType == 2)
                    {
                        hylEdit.Visible = false;//edit
                        btnDelete.Visible = false;//delete
                        hylCancel.Visible = false;//cancel
                    }
                }
            }
        }

        protected void gvG_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    e.Row.Cells[6].Visible = true;
                    e.Row.Cells[7].Visible = true;
                    e.Row.Cells[8].Visible = true;
                    e.Row.Cells[9].Visible = true;
                }
                else
                {
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                }
                //机票申请
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                {
                    e.Row.Cells[10].Visible = true;
                    e.Row.Cells[11].Visible = true;
                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[13].Visible = true;
                    e.Row.Cells[14].Visible = true;
                    e.Row.Cells[17].Visible = true;

                }
                else
                {
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[17].Visible = false;
                }

            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string backurl = Request[RequestName.BackUrl];

            if (e.CommandName == "Modify")
            {
                string detailid = e.CommandArgument.ToString();

                if (string.IsNullOrEmpty(backurl))
                {
                    Response.Redirect("ExpenseAccountEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailedit");
                }
                else
                {
                    Response.Redirect("ExpenseAccountEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailedit&" + RequestName.BackUrl + "=" + backurl);
                }
            }

            if (e.CommandName == "Cancel")
            {
                string detailid = e.CommandArgument.ToString();

                if (string.IsNullOrEmpty(backurl))
                {
                    Response.Redirect("ExpenseAccountEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailCancel");
                }
                else
                {
                    Response.Redirect("ExpenseAccountEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailCancel&" + RequestName.BackUrl + "=" + backurl);
                }
            }



            if (e.CommandName == "Del")
            {
                //删除报销申请
                int detailid = int.Parse(e.CommandArgument.ToString());
                if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Delete(detailid) == DeleteResult.Succeed)
                {
                    //BindInfo();
                    model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                    ESP.Finance.BusinessLogic.ReturnManager.Update(model);
                    if (string.IsNullOrEmpty(backurl))
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('删除成功！');window.location='ExpenseAccountEdit.aspx?id=" + id + "';", true);
                    }
                    else
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('删除成功！');window.location='ExpenseAccountEdit.aspx?id=" + id + "&" + RequestName.BackUrl + "=" + backurl + "';", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
                }
            }
        }

        #endregion


        /// <summary>
        /// 绑定报销单明细信息
        /// </summary>
        protected void BindDetailInfo()
        {

            ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;
            ESP.Finance.Entity.ExpenseAccountDetailInfo subdetail = null;
            if (detailid > 0)
            {

                detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(Convert.ToInt32(Request["detailid"]));

                labExpenseTypeName.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
                hidExpenseTypeId.Value = detail.ExpenseType.ToString();
                hidGasCostByKM.Value = System.Configuration.ConfigurationManager.AppSettings["GasCostByKM"];
                if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_GasCost"]))
                {
                    labGasByKM.Text = "<font color='red'>公里数</font>";
                }
                else
                {
                    labGasByKM.Text = "";
                }
                txtExpenseDate.Text = detail.ExpenseDate.Value.ToString("yyyy-MM-dd");

                txtExpenseDesc.Text = detail.ExpenseDesc;
                txtExpenseMoney.Text = detail.ExpenseMoney.Value.ToString();

                txtNumber.Value = detail.ExpenseTypeNumber.Value;


                ShowPan(detail.ExpenseType.Value);
                if (detail.ExpenseType.Value == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_BusinessTrip"]))
                {
                    if (detail.MealFeeMorning != null && detail.MealFeeMorning == 1)
                    {
                        chkMealFee1.Checked = true;
                    }
                    else
                    {
                        chkMealFee1.Checked = false;
                    }

                    if (detail.MealFeeNoon != null && detail.MealFeeNoon == 1)
                    {
                        chkMealFee2.Checked = true;
                    }
                    else
                    {
                        chkMealFee2.Checked = false;
                    }

                    if (detail.MealFeeNight != null && detail.MealFeeNight == 1)
                    {
                        chkMealFee3.Checked = true;
                    }
                    else
                    {
                        chkMealFee3.Checked = false;
                    }
                }
                else if (detail.ExpenseType.Value == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExpenseType_Cellphone"]))
                {
                    //报销手机费所要选择的月份 只列出没有报销的月份
                    drpPhoneMonth.DataSource = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetPhoneMonthList(Convert.ToInt32(drpPhoneYear.SelectedValue), UserInfo.UserID, detailid);
                    drpPhoneMonth.DataBind();

                    drpPhoneYear.SelectedValue = detail.PhoneYear.Value.ToString();
                    drpPhoneMonth.SelectedValue = detail.PhoneMonth.Value.ToString();

                    ddlPhoneInvoice.SelectedValue = detail.PhoneInvoice.ToString();

                    string[] invoiceNos = detail.PhoneInvoiceNo.Split(',');

                    if (invoiceNos != null && invoiceNos.Length > 0)
                    {
                        if (invoiceNos[0] != null)
                            txtInvoiceNo.Text = invoiceNos[0];
                        if (invoiceNos.Length>1 && invoiceNos[1] != null)
                            txtInvoiceNo2.Text = invoiceNos[1];
                        if (invoiceNos.Length > 2 && invoiceNos[2] != null)
                            txtInvoiceNo3.Text = invoiceNos[2];
                    }


                }

                if (typeid > 0)
                {
                    //if (typeid == 31 || typeid == 32 || typeid == 33 || typeid == 34 || typeid == 35)
                    if (System.Configuration.ConfigurationManager.AppSettings["ExpenseTypeIDs"].IndexOf(typeid.ToString()) > 0)
                    {
                        ddlProjectType.Items.Clear();
                        ddlProjectType.Items.Insert(0, new ListItem("OOP", "0"));
                    }
                    else
                    {
                        if (!typeIsMatch)
                        {
                            ddlProjectType.SelectedValue = detail.CostDetailID.Value.ToString();
                        }
                    }
                }
                else
                {
                    //if (detail.ExpenseType.Value == 31 || detail.ExpenseType.Value == 32 || detail.ExpenseType.Value == 33 || detail.ExpenseType.Value == 34 || detail.ExpenseType.Value == 35 )
                    if (ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseTypeIDs"].IndexOf(detail.ExpenseType.Value.ToString()) > 0)
                    {
                        ddlProjectType.Items.Clear();
                        ddlProjectType.Items.Insert(0, new ListItem("OOP", "0"));
                    }
                    else
                    {
                        if (!typeIsMatch)
                        {
                            ddlProjectType.SelectedValue = detail.CostDetailID.Value.ToString();
                        }
                    }
                }

                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                {
                    txtRecipient.Text = detail.Recipient;
                    txtCity.Text = detail.City;
                    txtBankName.Text = detail.BankName;
                    txtBankAccountNo.Text = detail.BankAccountNo;
                }

                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                {
                    this.hidBoarderId.Value = detail.BoarderId.ToString();
                    this.txtAirNo.Text = detail.GoAirNo;
                    this.txtPrice.Text = detail.ExpenseMoney.ToString();
                    this.txtAirTime.Text = detail.ExpenseDate.Value.ToString("yyyy-MM-dd");
                    this.txtDestination.Text = detail.TicketDestination;
                    this.txtSource.Text = detail.TicketSource;
                    this.lblBoarder.Text = detail.Boarder;
                    this.hidBoarder.Value = detail.Boarder;
                    this.hidIDCard.Value = detail.BoarderIDCard;
                    this.txtPhone.Text = detail.BoarderMobile;
                    this.txtRemark.Text = detail.ExpenseDesc;
                }
            }


        }


        protected void drpPhoneYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpPhoneMonth.Items.Clear();
            //报销手机费所要选择的月份 只列出没有报销的月份
            drpPhoneMonth.DataSource = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetPhoneMonthList(Convert.ToInt32(drpPhoneYear.SelectedValue), UserInfo.UserID, detailid);
            drpPhoneMonth.DataBind();

            if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
            {
                txtExpenseDesc.Text = "手机费:" + drpPhoneYear.SelectedValue + "年" + drpPhoneMonth.SelectedValue + "月";
            }
        }

        protected void drpPhoneMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
            {
                txtExpenseDesc.Text = "手机费:" + drpPhoneYear.SelectedValue + "年" + drpPhoneMonth.SelectedValue + "月";
            }
        }


        protected void ShowPan(int typeid)
        {
            if (typeid == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]))
            {
                panMealFee.Visible = true;
                panPhone.Visible = false;
                panOvertimeMeal.Visible = false;
            }
            else if (typeid == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
            {
                panMealFee.Visible = false;
                panOvertimeMeal.Visible = false;
                panPhone.Visible = true;

                //报销手机费所要选择的月份 只列出没有报销的月份
                drpPhoneMonth.DataSource = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetPhoneMonthList(Convert.ToInt32(drpPhoneYear.SelectedValue), UserInfo.UserID, detailid);
                drpPhoneMonth.DataBind();
                //iphone
                ESP.Finance.Entity.MobileListInfo mobile = ESP.Finance.BusinessLogic.MobileListManager.GetModel(int.Parse(CurrentUser.SysID));
                if (mobile != null)
                    lblIphone.Text = "从 " + mobile.EndDate.Year + "年" + mobile.EndDate.Month + "月起享受话费补贴";


            }
            //else if (typeid == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_CityTax"]))
            //{
            //    txtExpenseDesc.Text = "市内出租车费:";
            //}
            else if (typeid == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_OvertimeMeal"]))
            {
                panOvertimeMeal.Visible = true;
                panMealFee.Visible = false;
                panPhone.Visible = false;
            }
            else
            {
                panOvertimeMeal.Visible = false;
                panMealFee.Visible = false;
                panPhone.Visible = false;
            }

            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
            {
                panThirdParty.Visible = true;
            }
            else
            {
                panThirdParty.Visible = false;
            }
            //机票申请
            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
            {
                panTicket.Visible = true;
            }
            else
                panTicket.Visible = false;
        }

        #region 下拉联动

        [AjaxPro.AjaxMethod]
        public static List<List<string>> getTypeList(int projectId, int groupId)
        {
            List<List<string>> retlists = new List<List<string>>();

            //设置项目号下所包含的一级物料和二级物料
            string strTerms = " and a.projectId=@projectId and a.groupId = @groupId";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@projectId", SqlDbType.Int, 4));
            parms.Add(new SqlParameter("@groupId", SqlDbType.Int, 4));
            parms[0].Value = projectId;
            parms[1].Value = groupId;
            List<ESP.Purchase.Entity.V_GetProjectTypeList> typelist = ESP.Purchase.BusinessLogic.V_GetProjectTypeList.GetList(strTerms, parms);

            List<string> zero = new List<string>();
            zero.Add("0");
            zero.Add("OOP");
            retlists.Add(zero);

            foreach (ESP.Purchase.Entity.V_GetProjectTypeList item in typelist)
            {
                List<string> i = new List<string>();
                i.Add(item.TypeID.ToString());
                i.Add(item.typeName);
                retlists.Add(i);
            }
            return retlists;
        }

        #endregion

    }
}
