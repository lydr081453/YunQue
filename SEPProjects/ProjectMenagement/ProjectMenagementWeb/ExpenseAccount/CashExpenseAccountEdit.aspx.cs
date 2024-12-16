using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;

namespace FinanceWeb.ExpenseAccount
{
    public partial class CashExpenseAccountEdit : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.ReturnInfo model = null;
        ESP.Finance.Entity.ProjectInfo project = null;
        ESP.Administrative.Entity.UserAttBasicInfo userBasicModel;
        protected bool showflag = false;
        int id = 0;  //单据ID
        int detailid = 0;   //单据明细ID
        int typeid = 0;   //费用类型ID
        int detailRowCount = 0;   //明细条数
        bool typeIsMatch = false;

        int ReturnType = 0;  //单据类型

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.ExpenseAccount.CashExpenseAccountEdit));
            userBasicModel = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetModelByUserid(UserID);
            //报销单ID
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                id = Convert.ToInt32(Request["id"]);
            }
            //报销单明细ID  一般修改明细的时候大于0
            if (!string.IsNullOrEmpty(Request["detailid"]))
            {
                detailid = Convert.ToInt32(Request["detailid"]);

            }
            //报销单类型
            if (!string.IsNullOrEmpty(Request["ReturnType"]))
            {
                ReturnType = Convert.ToInt32(Request["ReturnType"]);
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                {
                    showflag = true;
                }
                else
                    showflag = false;
            }

            //支票/电汇不需要填写银行账号
            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
            {
                this.panBank.Visible = false;
            }

            if (id > 0)
            {
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(id);
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);
                ReturnType = model.ReturnType.Value;
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                {
                    showflag = true;
                }
                else
                    showflag = false;

                //支票/电汇不需要填写银行账号
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                {
                    this.panBank.Visible = false;
                }
            }

            if (!IsPostBack)
            {
                BindInfo();

                #region 物料树绑定

                initTree("");

                #endregion
            }

        }

        protected void initTree(string conditionStr)
        {
            ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
            rootNode.Text = "报销费用类型结构图";
            rootNode.Expanded = true;
            rootNode.ImageUrl = "treeview/root.gif";

            userTreeView.Nodes.Add(rootNode);
            int n = 0;
            List<ESP.Finance.Entity.ExpenseTypeInfo> typeInfo = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetList(" and status = 1 " + conditionStr == "" ? "" : conditionStr);
            string str = "";
            bool isP = false;
            bool isHerf = false;
            // 开始绑定物料节点
            BindTree(typeInfo, rootNode, 0, ref  n, str, isP, isHerf);
        }

        protected void bthSearchNode_Click(object sender, EventArgs e)
        {
            userTreeView.Nodes.Clear();
            if (!string.IsNullOrEmpty(txtNodeName.Text.Trim()))
            {
                initTree(" and ( ExpenseType like '%" + txtNodeName.Text.Trim() + "%' or TypeLevel = 1 )");
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
        protected void BindTree(IList<ESP.Finance.Entity.ExpenseTypeInfo> typeinfo, ComponentArt.Web.UI.TreeViewNode parentNode, int Id, ref int n, string treenode, bool isP, bool isHerf)
        {
            ComponentArt.Web.UI.TreeViewNode tn = null;


            foreach (ESP.Finance.Entity.ExpenseTypeInfo info in typeinfo)
            {
                if (info.ParentID == Id && (!"OOP".Equals(info.ExpenseType) || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard))
                {

                    if (userBasicModel.AttendanceType != ESP.Administrative.Common.Status.UserBasicAttendanceType_Special)
                    {
                        if (info.TypeLevel == 2)
                        {
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.ExpenseType;
                            tn.Value = info.ID.ToString();
                            tn.ImageUrl = "treeview/dept.gif";
                            tn.AutoPostBackOnSelect = true;
                        }
                        else
                        {
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.ExpenseType;
                            tn.Value = info.ID.ToString();
                            tn.ImageUrl = "treeview/corp.gif";
                        }
                        n++;
                        tn.ToolTip = info.ExpenseType;
                        parentNode.Nodes.Add(tn);

                        BindTree(typeinfo, tn, info.ID, ref n, treenode, isP, isHerf);
                    }
                    else
                    {
                        //总监以上没有OT餐费的报销
                        if (!info.ExpenseType.Equals("OT餐费"))
                        {
                            if (info.TypeLevel == 2)
                            {
                                tn = new ComponentArt.Web.UI.TreeViewNode();
                                tn.Text = info.ExpenseType;
                                tn.Value = info.ID.ToString();
                                tn.ImageUrl = "treeview/dept.gif";
                                tn.AutoPostBackOnSelect = true;
                            }
                            else
                            {
                                tn = new ComponentArt.Web.UI.TreeViewNode();
                                tn.Text = info.ExpenseType;
                                tn.Value = info.ID.ToString();
                                tn.ImageUrl = "treeview/corp.gif";
                            }
                            n++;
                            tn.ToolTip = info.ExpenseType;
                            parentNode.Nodes.Add(tn);

                            BindTree(typeinfo, tn, info.ID, ref n, treenode, isP, isHerf);
                        }
                    }


                }
            }
        }

        protected void userTreeView_NodeSelected(object sender, ComponentArt.Web.UI.TreeViewNodeEventArgs e)
        {

            typeid = Convert.ToInt32(e.Node.Value);
            BindInfo();

            hidExpenseTypeId.Value = e.Node.Value;
            hidGasCostByKM.Value = ESP.Configuration.ConfigurationManager.SafeAppSettings["GasCostByKM"];
            labExpenseTypeName.Text = e.Node.Text;

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

            ShowPan(Convert.ToInt32(e.Node.Value));

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
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                {
                    showflag = true;
                    this.chkIsFixCheque.Checked = model.IsFixCheque == null ? false : model.IsFixCheque.Value;
                }
                else
                    showflag = false;

                labPreFee.Text = hidPreFee.Value = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(id).ToString();

                //设置费用所属组
                string depts = "";
                int[] deptids = new ESP.Compatible.Employee(int.Parse(CurrentUser.SysID)).GetDepartmentIDs();
                if (model != null)
                {
                    depts = model.DepartmentID + "," + model.DepartmentName + "#";

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
            else
            {
                btnAddDetail.Text = "添加至申请单";
            }

            hidTypeIsMatch.Value = typeIsMatch.ToString();
            //审批日志
            if (model != null)
            {
                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + model.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    if (emp != null)
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && log.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }
            }
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected int save()
        {

            //ESP.Finance.Entity.ReturnInfo model = null;

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
                if (!model.ProjectCode.Equals(hidProject_Code1.Value) || model.PreFee.Value != ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID))
                {
                    try
                    {
                        ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(int.Parse(Request["id"].ToString()));
                    }
                    catch { }
                }
            }

            //收集信息
            model.ProjectID = Convert.ToInt32(hidProejctId.Value);
            model.ProjectCode = hidProject_Code1.Value;
            model.ReturnContent = hidProject_Description.Value;

            if (!string.IsNullOrEmpty(hidDeptId.Value))
            {
                model.DepartmentID = Convert.ToInt32(hidDeptId.Value.Split(',')[0]);
                model.DepartmentName = hidDeptId.Value.Split(',')[1];
            }
            else
            {
                model.DepartmentID = 0;
                model.DepartmentName = "";
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
            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
            {
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow;
                model.IsFixCheque = false;
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
            {
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic;
                model.IsFixCheque = this.chkIsFixCheque.Checked;
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard)
            {
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard;
                model.IsFixCheque = false;
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
            {
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff;
                model.IsFixCheque = false;
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
            {
                model.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia;
                model.IsFixCheque = false;
            }

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                if (ESP.Finance.BusinessLogic.ReturnManager.Update(model) == UpdateResult.Succeed)
                {
                    //现金借款更新个人银行信息
                    if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
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
                //现金借款更新个人银行信息
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                {
                    //更新当前申请人的个人工资卡信息
                    ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(CurrentUser.IntID);
                    empModel.SalaryCardNo = txtCreatorAccount.Text;
                    empModel.SalaryBank = txtCreatorBank.Text;
                    ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empModel);
                }
                model.RequestorID = Convert.ToInt32(CurrentUser.SysID);
                model.RequestUserCode = CurrentUser.ID;
                model.RequestUserName = CurrentUser.ITCode;
                model.RequestEmployeeName = CurrentUser.Name;
                model.RequestDate = DateTime.Now;

                model.ReturnStatus = (int)PaymentStatus.Created;

                return ESP.Finance.BusinessLogic.ReturnManager.CreateReturnInFinance(model);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int returnid = save();
            if (returnid > 0)
            {
                if (model.IsMediaOrder == 1)
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='CashExpenseAccountEdit.aspx?id=" + returnid + "&Media=1';", true);
                else
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='CashExpenseAccountEdit.aspx?id=" + returnid + "';", true);
                return;
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            int returnid = save();
            if (returnid > 0)
            {
                if (model.IsMediaOrder == 1)
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='CashExpenseAccountEdit.aspx?id=" + returnid + "&Media=1';", true);
                else
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "window.location='CashExpenseAccountEdit.aspx?id=" + returnid + "';", true);
                return;
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {

            //Response.Redirect("CashExpenseAccountList.aspx?ReturnType=" + ReturnType);
            Response.Redirect("/Edit/OOPTabEdit.aspx");
        }

        protected void btnReturn2_Click(object sender, EventArgs e)
        {

            //Response.Redirect("CashExpenseAccountList.aspx?ReturnType=" + ReturnType);
            Response.Redirect("/Edit/OOPTabEdit.aspx");
        }

        protected void btnSetAuditor_Click(object sender, EventArgs e)
        {
            int returnid = save();
            if (returnid > 0)
            {
                Response.Redirect("CashSetAuditor.aspx?id=" + returnid);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存失败，请重试!');", true);
            }

        }

        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            save();
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;
                if (detailid > 0)
                {
                    detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(detailid);
                    detail.ReturnID = Convert.ToInt32(Request["id"]);
                    detail.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);
                    detail.ExpenseType = Convert.ToInt32(hidExpenseTypeId.Value);
                    detail.ExpenseDesc = txtExpenseDesc.Text;
                    detail.ExpenseMoney = Convert.ToDecimal(txtExpenseMoney.Value == null ? 1 : txtExpenseMoney.Value);
                    detail.CostDetailID = Convert.ToInt32(ddlProjectType.SelectedValue);
                    detail.ExpenseTypeNumber = Convert.ToInt32(txtNumber.Value == null ? 1 : txtNumber.Value);


                    if (detail.ExpenseType == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]))
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
                    if (detail.ExpenseType == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
                    {
                        detail.PhoneYear = Convert.ToInt32(drpPhoneYear.SelectedValue);
                        detail.PhoneMonth = Convert.ToInt32(drpPhoneMonth.SelectedValue);
                    }
                    else
                    {
                        detail.PhoneYear = 0;
                        detail.PhoneMonth = 0;
                    }

                    if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                    {
                        detail.Recipient = txtRecipient.Text.Trim();
                        detail.City = txtCity.Text.Trim();
                        detail.BankName = txtBankName.Text.Trim();
                        detail.BankAccountNo = txtBankAccountNo.Text.Trim();

                        List<ESP.Finance.Entity.ExpenseAccountDetailInfo> details = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnid =" + detail.ReturnID.ToString());
                        if (details != null && details.Count > 0)
                        {
                            foreach (ESP.Finance.Entity.ExpenseAccountDetailInfo d in details)
                            {
                                if (detail.Recipient != d.Recipient || detail.BankAccountNo != d.BankAccountNo)
                                {
                                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('报销明细中的付款信息必须保持一致！');", true);
                                    return;
                                }
                            }
                        }
                    }

                    if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Update(detail) == UpdateResult.Succeed)
                    {
                        model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                        ESP.Finance.BusinessLogic.ReturnManager.Update(model);
                        try
                        {
                            ESP.Finance.BusinessLogic.ExpenseAuditerListManager.DeleteByReturnID(model.ReturnID);
                        }
                        catch { }
                        if (model.IsMediaOrder == 1)
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存成功!');window.location='CashExpenseAccountEdit.aspx?id=" + id + "&Media=1';", true);
                        else
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存成功!');window.location='CashExpenseAccountEdit.aspx?id=" + id + "';", true);

                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('数据保存失败，请重试！');", true);
                    }
                }
                else
                {
                    detail = new ESP.Finance.Entity.ExpenseAccountDetailInfo();
                    detail.ReturnID = id;
                    detail.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);
                    detail.ExpenseType = Convert.ToInt32(hidExpenseTypeId.Value);
                    detail.ExpenseDesc = txtExpenseDesc.Text;
                    detail.ExpenseMoney = Convert.ToDecimal(txtExpenseMoney.Value == null ? 1 : txtExpenseMoney.Value);
                    detail.ExpenseTypeNumber = Convert.ToInt32(txtNumber.Value == null ? 1 : txtNumber.Value);

                    detail.CostDetailID = Convert.ToInt32(ddlProjectType.SelectedValue);
                    detail.Creater = Convert.ToInt32(CurrentUser.SysID);
                    detail.CreaterName = CurrentUser.Name;
                    detail.CreateTime = DateTime.Now;
                    detail.Status = 1;
                    if (detail.ExpenseType == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]))
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
                    if (detail.ExpenseType == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
                    {
                        detail.PhoneYear = Convert.ToInt32(drpPhoneYear.SelectedValue);
                        detail.PhoneMonth = Convert.ToInt32(drpPhoneMonth.SelectedValue);
                    }
                    if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                    {
                        detail.Recipient = txtRecipient.Text.Trim();
                        detail.City = txtCity.Text.Trim();
                        detail.BankName = txtBankName.Text.Trim();
                        detail.BankAccountNo = txtBankAccountNo.Text.Trim();
                    }

                    if (ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.Add(detail) > 0)
                    {
                        model.PreFee = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(model.ReturnID);
                        ESP.Finance.BusinessLogic.ReturnManager.Update(model);

                        if (model.IsMediaOrder == 1)
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存成功!');window.location='CashExpenseAccountEdit.aspx?id=" + id + "&Media=1';", true);
                        else
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('数据保存成功!');window.location='CashExpenseAccountEdit.aspx?id=" + id + "';", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "alert('数据保存失败，请重试！');", true);
                    }

                }
            }

        }


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

            //if (list.Count > 0 && !IsPostBack)
            //{
            //    txtRecipient.Text = list[0].Recipient;
            //    txtBankName.Text = list[0].BankName;
            //    txtBankAccountNo.Text = list[0].BankAccountNo;
            //    txtCity.Text = list[0].City;
            //}
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
                ESP.Finance.Entity.ExpenseAccountDetailInfo detail = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Row.DataItem;
                ESP.Purchase.Entity.TypeInfo projectType = ESP.Purchase.BusinessLogic.TypeManager.GetModel(detail.CostDetailID.Value);

                Label labCostDetailName = (Label)e.Row.FindControl("labCostDetailName");
                if (projectType != null && !string.IsNullOrEmpty(projectType.typename))
                    labCostDetailName.Text = projectType.typename;
                else
                    labCostDetailName.Text = "OOP";


                Label labExpenseTypeName = (Label)e.Row.FindControl("labExpenseTypeName");
                string expenseTypeName = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
                if (detail.ExpenseType.Value == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]))
                {
                    labExpenseTypeName.Text = expenseTypeName + "(";
                    labExpenseTypeName.Text += detail.MealFeeMorning != null && detail.MealFeeMorning == 1 ? "早餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNoon != null && detail.MealFeeNoon == 1 ? "午餐 " : "";
                    labExpenseTypeName.Text += detail.MealFeeNight != null && detail.MealFeeNight == 1 ? "晚餐" : "";
                    labExpenseTypeName.Text += ")";
                }
                else if (detail.ExpenseType.Value == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
                {
                    labExpenseTypeName.Text = expenseTypeName + "(" + detail.PhoneYear + "年" + detail.PhoneMonth + "月)";
                }
                else
                {
                    labExpenseTypeName.Text = expenseTypeName;
                }
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
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
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                string detailid = e.CommandArgument.ToString();
                if (model.IsMediaOrder == 1)
                    Response.Redirect("CashExpenseAccountEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailedit&Media=1");
                else
                    Response.Redirect("CashExpenseAccountEdit.aspx?detailid=" + detailid + "&id=" + id + "&op=detailedit");
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
                    if (model.IsMediaOrder == 1)
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('删除成功！');window.location='CashExpenseAccountEdit.aspx?id=" + id + "&Media=1';", true);
                    else
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('删除成功！');window.location='CashExpenseAccountEdit.aspx?id=" + id + "';", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
                }
            }
        }

        protected void gvG_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
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
            }
        }

        #endregion


        /// <summary>
        /// 绑定报销单明细信息
        /// </summary>
        protected void BindDetailInfo()
        {

            ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;
            if (detailid > 0)
            {

                detail = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(Convert.ToInt32(Request["detailid"]));

                labExpenseTypeName.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detail.ExpenseType.Value).ExpenseType;
                hidExpenseTypeId.Value = detail.ExpenseType.ToString();
                hidGasCostByKM.Value = ESP.Configuration.ConfigurationManager.SafeAppSettings["GasCostByKM"];
                if (Convert.ToInt32(hidExpenseTypeId.Value) == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_GasCost"]))
                {
                    labGasByKM.Text = "<font color='red'>公里数</font>";
                }
                else
                {
                    labGasByKM.Text = "";
                }

                txtExpenseDate.Text = detail.ExpenseDate.Value.ToString("yyyy-MM-dd");

                txtExpenseDesc.Text = detail.ExpenseDesc;
                txtExpenseMoney.Value = Convert.ToDouble(detail.ExpenseMoney.Value);

                txtNumber.Value = Convert.ToInt32(detail.ExpenseTypeNumber.Value);


                ShowPan(detail.ExpenseType.Value);
                if (detail.ExpenseType.Value == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_BusinessTrip"]))
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
                else if (detail.ExpenseType.Value == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
                {
                    //报销手机费所要选择的月份 只列出没有报销的月份
                    drpPhoneMonth.DataSource = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetPhoneMonthList(Convert.ToInt32(drpPhoneYear.SelectedValue), UserInfo.UserID, detailid);
                    drpPhoneMonth.DataBind();

                    drpPhoneYear.SelectedValue = detail.PhoneYear.Value.ToString();
                    drpPhoneMonth.SelectedValue = detail.PhoneMonth.Value.ToString();

                }

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
                if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                {
                    txtRecipient.Text = detail.Recipient;
                    txtCity.Text = detail.City;
                    txtBankName.Text = detail.BankName;
                    txtBankAccountNo.Text = detail.BankAccountNo;
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
            }
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
            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic || ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
            {
                panThirdParty.Visible = true;
            }
            else
            {
                panThirdParty.Visible = false;
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
