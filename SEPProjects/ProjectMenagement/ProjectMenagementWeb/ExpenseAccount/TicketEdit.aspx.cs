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
    public partial class TicketEdit : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ReturnInfo model = null;
        ESP.Finance.Entity.ProjectInfo project = null;

        int id = 0;  //单据ID
        int detailid = 0;   //单据明细ID
        int typeid = 0;   //费用类型ID
        int detailRowCount = 0;   //明细条数
        bool typeIsMatch = false;

        int ReturnType = 0;  //单据类型

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.ExpenseAccount.TicketEdit));
            //if (CurrentUser.DimissionStatus == ESP.HumanResource.Common.Status.DimissionFinanceAudit)
            //{
            //    Response.Redirect("/Edit/TicketReceipient.aspx");
            //}

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = int.Parse(Request["id"].ToString());
            }
            if (!string.IsNullOrEmpty(Request["detailid"]))
            {
                detailid = int.Parse(Request["detailid"].ToString());

            }
            //报销单类型
            if (!string.IsNullOrEmpty(Request["ReturnType"]))
            {
                ReturnType = Convert.ToInt32(Request["ReturnType"]);
            }

            if (id > 0)
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
                ReturnType = model.ReturnType.Value;
            }

            if (!IsPostBack)
            {
                this.panTicketCancel.Visible = false;
                this.panAudit.Visible = false;
                BindInfo();
            }

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='TicketEdit.aspx?ReturnType=" + Request["ReturnType"] + "&id=" + Request["id"] + "&backurl=" + Request["backurl"] + "';", true);
        }

        private string GetReceptionIds()
        {
            return "," + ESP.Framework.BusinessLogic.OperationAuditManageManager.GetReceptionIds() + ",";
        }
        /// <summary>
        /// 绑定报销单信息
        /// </summary>
        protected void BindInfo()
        {
            bindSupplier();
            string receptionIds = GetReceptionIds();
            if (id > 0)
            {
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
                int boarderId = 0;
                if (!string.IsNullOrEmpty(Request["BoarderId"]) && Request["BoarderId"] != "0")
                    boarderId = int.Parse(Request["BoarderId"]);
                else
                    boarderId = model.RequestorID.Value;
                ESP.HumanResource.Entity.EmployeeBaseInfo employee = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(boarderId);
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(boarderId);
                this.lblReturnCode.Text = model.ReturnCode;
                this.txtContent.Text = model.ReturnContent;
                if (model.TicketNo != 0)
                    this.lblReturnCode.Text += "-" + model.TicketNo.ToString();

                this.lblReturnCode.Text += "&nbsp;&nbsp;&nbsp;&nbsp;" + model.SupplierName;
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

                if (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Submit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.CEOAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.ProjectManagerAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit)
                {
                    btnSetAuditor.Visible = false;
                    btnReturn.Visible = false;
                    this.panAudit.Visible = true;
                }


                BindList();

                labRequestUserName.Text = model.RequestEmployeeName;
                labRequestUserCode.Text = model.RequestUserCode;
                labRequestDate.Text = model.RequestDate.Value.ToString("yyyy-MM-dd");

                hidProejctId.Value = model.ProjectID.Value.ToString();

                txtproject_code1.Text = model.ProjectCode.Split('-')[0];
                txtproject_code2.Text = model.ProjectCode.Split('-')[1];
                txtproject_code3.Text = model.ProjectCode.Split('-')[2];
                txtproject_code.Text = model.ProjectCode.Split('-')[3];

                hidProject_Code1.Value = model.ProjectCode.Split('-')[0];
                hidProject_Code2.Value = model.ProjectCode.Split('-')[1];
                hidProject_Code3.Value = model.ProjectCode.Split('-')[2];
                hidProject_Code.Value = model.ProjectCode.Split('-')[3];

                if (model.ProjectID != null && model.ProjectID != 0)
                {
                    txtproject_descripttion.Text = project.BusinessDescription;
                    hidProject_Description.Value = project.BusinessDescription;
                }

                //txtMemo.Text = model.ReturnContent;

                labPreFee.Text = hidPreFee.Value = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(id).ToString();

                //设置费用所属组
                string depts = "";
                ESP.Compatible.Employee requestor = null;
                if (model != null && model.RequestorID != null)
                    requestor = new ESP.Compatible.Employee(model.RequestorID.Value);
                else
                    requestor = CurrentUser;
                int[] deptids = new ESP.Compatible.Employee(int.Parse(requestor.SysID)).GetDepartmentIDs();
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

                            if (CurrentUser.SysID.Equals(ESP.Configuration.ConfigurationManager.SafeAppSettings["HowardID"]))
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
                        if (OperationAuditManageManager.GetCurrentUserIsManager(CurrentUser.SysID)
                            || OperationAuditManageManager.GetCurrentUserIsDirector(CurrentUser.SysID)
                            || ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"].IndexOf("," + deptids[0] + ",") >= 0
                            || ESP.Configuration.ConfigurationManager.SafeAppSettings["SpecialDeptIDs"].IndexOf("," + deptids[0] + ",") >= 0
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
                if (typelist.Count > 0 && ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseTypeIDs"].IndexOf(typeid.ToString()) <= 0)
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

            //绑定明细信息
            if (detailid > 0 && !string.IsNullOrEmpty(Request["op"]) && ("detailedit".Equals(Request["op"].ToLower()) || "detailupdate".Equals(Request["op"].ToLower())))
            {
                BindDetailInfo();
                btnAddDetail.Text = " 保存 ";
            }
            else if (detailid > 0 && !string.IsNullOrEmpty(Request["op"]) && "detailchange".Equals(Request["op"].ToLower()))
            {
                BindDetailInfo();
                ticketEnable();
                this.btnNew.Visible = true;
                btnAddDetail.Text = " 保存 ";
            }
            else if (detailid > 0 && !string.IsNullOrEmpty(Request["op"]) && "detailcancel".Equals(Request["op"].ToLower()))
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

            if ((model != null && model.ReturnStatus >= 2) && string.IsNullOrEmpty(Request["op"]))
            {
                this.btnAddDetail.Visible = false;
            }

            hidTypeIsMatch.Value = typeIsMatch.ToString();
            if (model != null)
            {

                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + model.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    if (log == null)
                        continue;
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    if (emp != null)
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && (log.AuditType == null || log.AuditType.Value == 0)) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }

                if (model.ReturnStatus == 100 || model.ReturnStatus == 107 || model.ReturnStatus == 108 || model.ReturnStatus == 109)
                {

                    //出发地、目的地，出发日期 、登机人信息不能改，可以减少金额，增加金额10%，可以修改航班号
                    //前台修改信息后，系统自动发送邮件给申请人、项目负责人、leader
                    this.txtSource.Enabled = false;
                    this.txtDestination.Enabled = false;
                    this.txtAirTime.Enabled = false;
                    this.btnBoarder.Enabled = false;

                }
            }


        }

        private void ticketEnable()
        {
            this.txtAirNo.Enabled = false;
            this.txtAirTime.Enabled = false;
            this.txtSource.Enabled = false;
            this.txtDestination.Enabled = false;
            this.txtPrices.Enabled = false;
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
                if (!model.ProjectCode.Equals(hidProject_Code1.Value + "-" + hidProject_Code2.Value + "-" + hidProject_Code3.Value + "-" + hidProject_Code.Value) || model.PreFee.Value != ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID))
                {
                    try
                    {
                        ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(int.Parse(Request["id"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            //收集信息
            int projectId = Convert.ToInt32(hidProejctId.Value);
            string projectCode = hidProject_Code1.Value + "-" + hidProject_Code2.Value + "-" + hidProject_Code3.Value + "-" + hidProject_Code.Value;

            ESP.Compatible.Employee emp = null;
            if (model != null && model.RequestorID != null)
                emp = new ESP.Compatible.Employee(model.RequestorID.Value);
            else
                emp = CurrentUser;
            int deptid = emp.GetDepartmentIDs()[0];

           // ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(int.Parse(emp.SysID));
            ESP.Framework.Entity.OperationAuditManageInfo operation = null;

            if (projectId != 0)
            {
                operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(projectId);
            }
            if (operation == null)
                operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(int.Parse(emp.SysID)); ;

            if (operation == null)
                operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(deptid);

            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectCode.Substring(0, 1));
            model.ProjectID = projectId;
            model.ProjectCode = projectCode;
            model.BranchID = branchModel.BranchID;
            model.BranchCode = branchModel.BranchCode;
            model.ReturnContent = hidProject_Description.Value;
            model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket;

            model.ReceptionId = operation.ReceptionId;

            model.ReturnPreDate = DateTime.Now;
            model.PreBeginDate = DateTime.Now;
            model.PreEndDate = DateTime.Now;
            model.ReturnContent = txtContent.Text;

            if (!string.IsNullOrEmpty(hidDeptId.Value))
            {
                model.DepartmentID = Convert.ToInt32(hidDeptId.Value.Split(',')[0]);
                model.DepartmentName = hidDeptId.Value.Split(',')[1];
            }


            if (!string.IsNullOrEmpty(hidPreFee.Value))
            {
                model.PreFee = decimal.Parse(hidPreFee.Value);
                model.FactFee = model.PreFee;
            }
            else
            {
                model.PreFee = 0;
                model.FactFee = 0;
            }

            ESP.Finance.Entity.TicketSupplier supplier = null;
            if ((model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Submit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.CEOAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.ProjectManagerAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit) && isCommit == 1)
            {
                int supplierId = 0;
                supplierId = int.Parse(ddlSupplier.SelectedValue);
                if (supplierId == 0)
                {
                    return -1;
                }
                supplier = ESP.Finance.BusinessLogic.TicketSupplierManager.GetModel(supplierId);

                model.SupplierName = supplier.SupplierName;
                model.SupplierBankName = supplier.BankName;
                model.SupplierBankAccount = supplier.AccountNo;
                model.TicketSupplierId = supplier.SupplierId;
                model.ReciptionDate = DateTime.Now;

                model.ReturnStatus = (int)PaymentStatus.Ticket_ReceptionConfirm;
                model.FactFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                model.PreFee = model.FactFee;
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

                ESP.Finance.BusinessLogic.ReturnManager.DeleteWorkFlow(model.ReturnID);

            }
            if (!string.IsNullOrEmpty(Request["op"]) && Request["op"].ToLower() == "detailcancel")
            {
                model.ReturnStatus = (int)PaymentStatus.Ticket_ReceptionConfirm;
                model.TicketNo++;
            }


            if (!string.IsNullOrEmpty(Request["id"]))
            {
                if (ESP.Finance.BusinessLogic.ReturnManager.Update(model) == UpdateResult.Succeed)
                {
                    //send email
                    if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket && model.ReturnStatus == (int)PaymentStatus.Ticket_ReceptionConfirm)
                    {
                        string mail = string.Empty;
                        if (supplier != null)
                        {
                            mail = supplier.Email;
                            ESP.Finance.Utility.SendMailHelper.SendMailTicket(model.ReturnID, mail, 1);
                        }
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
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket;
                model.ReturnStatus = (int)PaymentStatus.Created;

                return ESP.Finance.BusinessLogic.ReturnManager.CreateReturnInFinance(model);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int returnid = save(0);
            if (returnid > 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "btnSave", "window.location='TicketEdit.aspx" + Request.Url.Query + "';", true);
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
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "btnSave", "window.location='TicketEdit.aspx?id=" + returnid.ToString() + "';", true);
                return;
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (GetReceptionIds().IndexOf("," + CurrentUser.SysID + ",") < 0)
                Response.Redirect("/Edit/TicketReceipient.aspx");
            else
                Response.Redirect("TicketList.aspx");
        }

        protected void btnReturn2_Click(object sender, EventArgs e)
        {

            if (GetReceptionIds().IndexOf("," + CurrentUser.SysID + ",") < 0)
                Response.Redirect("/Edit/TicketReceipient.aspx");
            else
                Response.Redirect("TicketList.aspx");

        }

        protected void btnSetAuditor_Click(object sender, EventArgs e)
        {
            int returnid = save(1);
            if (returnid > 0)
            {
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket && model.ReturnStatus > 2)
                {
                    if (GetReceptionIds().IndexOf("," + CurrentUser.SysID + ",") < 0)
                        Response.Redirect("/Edit/TicketReceipient.aspx");
                    else
                        Response.Redirect("TicketList.aspx");
                }
                else
                {
                    Response.Redirect("SetAuditor2.aspx?id=" + returnid);
                }
            }
            else if (returnid == -1)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('请选择机票供应商!');", true);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }

        }

        private void bindSupplier()
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee();
            if (model != null && model.RequestorID != null)
                emp = new ESP.Compatible.Employee(model.RequestorID.Value);
            else
                emp = CurrentUser;
            int deptid = emp.GetDepartmentIDs()[0];
            ESP.Framework.Entity.DepartmentInfo dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(deptid);
            IList<ESP.Finance.Entity.TicketSupplier> suplierList = ESP.Finance.BusinessLogic.TicketSupplierManager.GetList(" receptionId = " + CurrentUser.SysID, new List<SqlParameter>());
            ESP.Finance.Entity.TicketSupplier def = new ESP.Finance.Entity.TicketSupplier();
            def.SupplierId = 0;
            def.SupplierName = "请选择";
            suplierList.Insert(0, def);
            this.ddlSupplier.DataSource = suplierList;
            this.ddlSupplier.DataTextField = "SupplierName";
            this.ddlSupplier.DataValueField = "SupplierId";
            this.ddlSupplier.DataBind();

        }

        protected void ddlSupplier_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(this.ddlSupplier.SelectedValue);
            if (id != 0)
            {
                var model = ESP.Finance.BusinessLogic.TicketSupplierManager.GetModel(id);
                this.lblAddress.Text = model.Address;
                this.lblContacter.Text = model.Contacter;
                this.lblMail.Text = model.Email;
                this.lblMobile.Text = model.Mobile;
                this.lblTel.Text = model.Tel;
                this.hidSupplierAccount.Value = model.AccountNo;
                this.hidSupplierBank.Value = model.BankName;
                this.hidSupplierId.Value = model.SupplySupplierId.ToString();
                this.hidReceptionId.Value = model.ReceptionId.ToString();
            }
        }

        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;
            model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(int.Parse(Request["id"].ToString()));
            if (detailid > 0)
            {
                detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(detailid);
                //if (!string.IsNullOrEmpty(Request["op"]) && Request["op"].ToLower() != "detailupdate")
                //{
                //    if (Convert.ToDecimal(this.txtPrices.Value.Value) > detail.GoAmount * Convert.ToDecimal(1.1) && (model.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save && model.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Rejected && model.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Created))
                //    {
                //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('费用超出原申请金额的10%,需要重新审批!');", true);
                //        return;
                //    }
                //}
            }
            save(0);
            string query = "id=" + Request["id"].ToString();

            if (!string.IsNullOrEmpty(Request["id"]))
            {

                if (detailid > 0)
                {
                    decimal goamount = detail.GoAmount;
                    detail.ReturnID = Convert.ToInt32(Request["id"]);
                    detail.ExpenseType = Convert.ToInt32(hidExpenseTypeId.Value);
                    detail.CostDetailID = Convert.ToInt32(ddlProjectType.SelectedValue);
                    detail.TicketStatus = 0;
                    detail.GoAirNo = this.txtAirNo.Text;
                    // 增加仓位选择，backairno没有用上，暂时使用该字段
                    detail.BackAirNo = this.ddlSeat.SelectedItem.Text;
                    if (model.ReturnStatus == 1 || model.ReturnStatus == -1 || model.ReturnStatus == 0 || detail.GoAmount == 0)
                    {
                        detail.GoAmount = Convert.ToDecimal(this.txtPrices.Value.Value);
                    }
                    detail.TicketDestination = this.txtDestination.Text;
                    detail.TicketSource = this.txtSource.Text;
                    detail.TicketStatus = 0;
                    detail.BoarderId = int.Parse(this.hidBoarderId.Value);
                    detail.Boarder = this.hidBoarder.Value;
                    detail.BoarderIDCard = this.hidIDCard.Value;
                    detail.BoarderMobile = this.txtPhone.Text; ;
                    detail.ExpenseMoney = Convert.ToDecimal(this.txtPrices.Value.Value);
                    detail.ExpenseDate = Convert.ToDateTime(this.txtAirTime.Text);
                    detail.ExpenseTypeNumber = 1;
                    detail.ExpenseDesc = txtRemark.Text;
                    query += "&BoarderId=" + hidBoarderId.Value;
                    detail.TicketIsUsed = false;
                    detail.TicketIsConfirm = false;
                    detail.BoarderIDType = this.hidCardType.Value == string.Empty ? "身份证" : this.hidCardType.Value;
                    if (!string.IsNullOrEmpty(Request["op"]) && Request["op"].ToLower() == "detailcancel")
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
                        subdetail.BackAirNo = detail.BackAirNo;
                        subdetail.GoAmount = Convert.ToDecimal(this.txtCancelPrice.Text);
                        subdetail.BoarderId = detail.BoarderId;
                        subdetail.Boarder = detail.Boarder;
                        subdetail.BoarderIDCard = detail.BoarderIDCard;
                        subdetail.BoarderMobile = detail.BoarderMobile;
                        subdetail.TicketStatus = 0;
                        subdetail.TripType = 2;
                        subdetail.TicketIsUsed = false;
                        subdetail.TicketIsConfirm = false;
                        subdetail.BoarderIDType = this.hidCardType.Value == string.Empty ? "身份证" : this.hidCardType.Value;
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
                        log.AuditType = 0;
                        ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                    }
                    else if (!string.IsNullOrEmpty(Request["op"]) && Request["op"].ToLower() == "detailupate")
                    {
                        ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                        log.AuditeDate = DateTime.Now;
                        log.AuditorEmployeeName = CurrentUser.Name;
                        log.AuditorUserID = int.Parse(CurrentUser.SysID);
                        log.AuditorUserCode = CurrentUser.ID;
                        log.AuditorUserName = CurrentUser.ITCode;
                        log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Submit;
                        log.ExpenseAuditID = model.ReturnID;
                        log.Suggestion = detail.GoAirNo + "改签操作";
                        log.AuditType = 0;
                    }

                    if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Update(detail) == UpdateResult.Succeed)
                    {
                        if ((Convert.ToDecimal(this.txtPrices.Value.Value) != goamount) && (Convert.ToDecimal(this.txtPrices.Value) <= goamount * Convert.ToDecimal(1.1)))
                        {
                            //model.ReturnStatus = (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm;
                            model.TicketNo = model.TicketNo + 1;
                            ESP.Finance.Entity.ExpenseAuditDetailInfo log = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                            log.AuditeDate = DateTime.Now;
                            log.AuditorEmployeeName = CurrentUser.Name;
                            log.AuditorUserID = int.Parse(CurrentUser.SysID);
                            log.AuditorUserCode = CurrentUser.ID;
                            log.AuditorUserName = CurrentUser.ITCode;
                            log.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Submit;
                            log.ExpenseAuditID = model.ReturnID;
                            log.Suggestion = "金额调整";
                            log.AuditType = 0;
                            ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.Add(log);
                        }
                        model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                        model.FactFee = model.PreFee;
                        ESP.Finance.BusinessLogic.ReturnManager.Update(model);
                        try
                        {
                            ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(model.ReturnID);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存成功!');window.location='TicketEdit.aspx?" + query + "';", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('数据保存失败，请重试！');", true);
                    }
                }
                else
                {
                    detail = new ESP.Finance.Entity.ExpenseAccountDetailInfo();
                    ESP.HumanResource.Entity.EmployeeBaseInfo employee = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(hidBoarderId.Value));
                    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(hidBoarderId.Value));

                    detail.ReturnID = id;
                    detail.ExpenseType = Convert.ToInt32(hidExpenseTypeId.Value);
                    detail.CostDetailID = Convert.ToInt32(ddlProjectType.SelectedValue);
                    detail.Creater = Convert.ToInt32(CurrentUser.SysID);
                    detail.CreaterName = CurrentUser.Name;
                    detail.CreateTime = DateTime.Now;
                    detail.Status = 1;

                    detail.GoAirNo = this.txtAirNo.Text;
                    detail.BackAirNo = ddlSeat.SelectedItem.Text;
                    detail.GoAmount = (decimal)this.txtPrices.Value.Value;
                    detail.ExpenseDate = Convert.ToDateTime(this.txtAirTime.Text);
                    detail.TicketDestination = this.txtDestination.Text;
                    detail.TicketSource = this.txtSource.Text;
                    detail.TicketStatus = 0;
                    detail.BoarderId = string.IsNullOrEmpty(emp.SysID) ? 0 : int.Parse(emp.SysID);
                    detail.Boarder = hidBoarder.Value;
                    detail.BoarderIDCard = this.hidIDCard.Value;
                    detail.BoarderMobile = this.txtPhone.Text;
                    detail.ExpenseMoney = Convert.ToDecimal(this.txtPrices.Value.Value);
                    detail.ExpenseDesc = txtRemark.Text;
                    detail.ExpenseTypeNumber = 1;
                    detail.TicketIsUsed = false;
                    detail.TicketIsConfirm = false;
                    detail.BoarderIDType = this.hidCardType.Value;
                    query += "&BoarderId=" + hidBoarderId.Value;
                    query += "&" + RequestName.BackUrl + "=TicketList.aspx";


                    if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Add(detail) > 0)
                    {
                        model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                        model.FactFee = model.PreFee;
                        ESP.Finance.BusinessLogic.ReturnManager.Update(model);

                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存成功!');window.location='TicketEdit.aspx?" + query + "';", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('数据保存失败，请重试！');", true);
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
            list = list.OrderByDescending(N => N.CreateTime).ToList();
            gvG.DataSource = list;
            gvG.DataBind();

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
                LinkButton hylUpdate = (LinkButton)e.Row.FindControl("hylUpdate");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                Label lblExpenseDesc = (Label)e.Row.FindControl("lblExpenseDesc");
                ESP.Finance.Entity.ExpenseAccountDetailInfo detail = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Row.DataItem;
                ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(detail.CostDetailID.Value);
                ESP.HumanResource.Entity.EmployeeBaseInfo boarderModel = null;


                Label lblUserCode = (Label)e.Row.FindControl("lblUserCode");
                Label labCostDetailName = (Label)e.Row.FindControl("labCostDetailName");
                if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                    labCostDetailName.Text = projectType.typename;
                else
                    labCostDetailName.Text = "OOP";

                if (detail.BoarderId != 0)
                    boarderModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(detail.BoarderId);

                if (boarderModel != null)
                {
                    lblUserCode.Text = boarderModel.Code;
                }
                lblExpenseDesc.Text = detail.ExpenseDesc;
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
                    if (model != null && (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Submit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.CEOAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm || GetReceptionIds().IndexOf("," + CurrentUser.SysID + ",") >= 0))
                    {
                        btnDelete.Visible = false;//delete
                    }
                    else
                    {
                        btnDelete.Visible = true;
                    }

                    if (model != null && (model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm || model.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_Received))
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
                        hylUpdate.Visible = false;
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

                //机票申请
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                {


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
                    Response.Redirect("TicketEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailedit");
                }
                else
                {
                    Response.Redirect("TicketEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailedit&" + RequestName.BackUrl + "=" + backurl);
                }
            }

            if (e.CommandName == "Update")
            {
                string detailid = e.CommandArgument.ToString();

                if (string.IsNullOrEmpty(backurl))
                {
                    Response.Redirect("TicketEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailedit");
                }
                else
                {
                    Response.Redirect("TicketEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailupdate&" + RequestName.BackUrl + "=" + backurl);
                }
            }

            if (e.CommandName == "Cancel")
            {
                string detailid = e.CommandArgument.ToString();

                if (string.IsNullOrEmpty(backurl))
                {
                    Response.Redirect("TicketEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailCancel");
                }
                else
                {
                    Response.Redirect("TicketEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailCancel&" + RequestName.BackUrl + "=" + backurl);
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
                    model.FactFee = model.PreFee;
                    ESP.Finance.BusinessLogic.ReturnManager.Update(model);
                    if (string.IsNullOrEmpty(backurl))
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('删除成功！');window.location='TicketEdit.aspx?id=" + id + "';", true);
                    }
                    else
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('删除成功！');window.location='TicketEdit.aspx?id=" + id + "&" + RequestName.BackUrl + "=" + backurl + "';", true);
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

                if (typeid > 0)
                {
                    if (ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseTypeIDs"].IndexOf(typeid.ToString()) > 0)
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


                this.hidBoarderId.Value = detail.BoarderId.ToString();
                this.txtAirNo.Text = detail.GoAirNo;
                switch (detail.BackAirNo)
                {
                    case "头等舱":
                        ddlSeat.SelectedIndex = 1;
                        break;
                    case "商务舱":
                        ddlSeat.SelectedIndex = 2;
                        break;
                    case "经济舱":
                        ddlSeat.SelectedIndex = 3;
                        break;
                    default:
                        ddlSeat.SelectedIndex = 0;
                        break;
                }
                this.txtPrices.Value = (double)detail.ExpenseMoney;
                this.txtAirTime.Text = detail.ExpenseDate.Value.ToString("yyyy-MM-dd");
                this.txtDestination.Text = detail.TicketDestination;
                this.txtSource.Text = detail.TicketSource;
                this.lblBoarder.Text = detail.Boarder;
                this.hidBoarder.Value = detail.Boarder;
                this.hidIDCard.Value = detail.BoarderIDCard;
                this.txtPhone.Text = detail.BoarderMobile;
                this.txtRemark.Text = detail.ExpenseDesc;
                this.hidCardType.Value = detail.BoarderIDType;

            }


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
