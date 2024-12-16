using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using System.Data.SqlClient;
using ESP.Framework.Entity;
using ComponentArt.Web.UI;
using ESP.Framework.BusinessLogic;
using System.Text;
using ESP.Administrative.Common;

namespace AdministrativeWeb.Attendance
{
    public partial class AAndRLeaveList : ESP.Web.UI.PageBase
    {
        /// <summary>
        /// 年假奖励假管理类
        /// </summary>
        ALAndRLManager manager = new ALAndRLManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpDate();
                LoadDepartmentInfo();
                DataSet ds = BindInfo();
                Grid1.DataSource = ds;
                Grid1.DataBind();
            }
        }

        protected int DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days / 365;
            return dateDiff;
            #region note
            //C#中使用TimeSpan计算两个时间的差值
            //可以反加两个日期之间任何一个时间单位。
            //TimeSpan ts = Date1 - Date2;
            //double dDays = ts.TotalDays;//带小数的天数，比如1天12小时结果就是1.5 
            //int nDays = ts.Days;//整数天数，1天12小时或者1天20小时结果都是1  
            #endregion
        }

        /// <summary>
        /// 绑定下拉列表中的日期和时间内容
        /// </summary>
        protected void drpDate()
        {
            int year = DateTime.Now.Year - 3;
            for (int i = 0; i <= 4; i++)
            {
                drpYear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                ddlYearAdd.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }
            drpYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlYearAdd.SelectedValue = DateTime.Now.Year.ToString();
            // ddlWorkYear.SelectedValue = DateTime.Now.Year.ToString();
            //ddlImportYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 绑定年假信息
        /// </summary>
        /// <returns></returns>
        private DataSet BindInfo()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();

            // 查询字符串
            StringBuilder strBuilder = new StringBuilder();

            int year = int.Parse(drpYear.SelectedValue);
            strBuilder.Append(" AND a.LeaveYear=@LeaveYear ");
            parameterList.Add(new SqlParameter("@LeaveYear", year));

            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                strBuilder.Append(" AND (a.EmployeeName LIKE '%'+@EmployeeName+'%' or em.InternalEmail  LIKE '%'+@EmployeeName+'%') ");
                parameterList.Add(new SqlParameter("@EmployeeName", txtUserName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtUserCode.Text))
            {
                strBuilder.Append(" AND a.UserCode LIKE '%'+@UserCode+'%' ");
                parameterList.Add(new SqlParameter("@UserCode", txtUserCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtPositions.Text))
            {
                strBuilder.Append(" AND p.DepartmentPositionName LIKE '%'+@Position+'%' ");
                parameterList.Add(new SqlParameter("@Position", txtPositions.Text.Trim()));
            }
            // 分公司
            if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
            {
                strBuilder.Append(" AND d.level1id=@level1id ");
                SqlParameter p = new SqlParameter("@level1id", SqlDbType.NVarChar);
                p.Value = cbCompany.SelectedValue;
                parameterList.Add(p);
            }
            // 团队
            if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
            {
                strBuilder.Append(" AND d.level2id=@level2id ");
                SqlParameter p = new SqlParameter("@level2id", SqlDbType.NVarChar);
                p.Value = cbDepartment1.SelectedValue;
                parameterList.Add(p);
            }
            // 部门
            if (!string.IsNullOrEmpty(cbDepartment2.SelectedValue))
            {
                strBuilder.Append(" AND d.level3id=@level3id ");
                SqlParameter p = new SqlParameter("@level3id", SqlDbType.NVarChar);
                p.Value = cbDepartment2.SelectedValue;
                parameterList.Add(p);
            }

            DataSet ds = manager.GetAnnualLeaveInfo(UserID, strBuilder.ToString(), parameterList);
            return ds;
        }

        /// <summary>
        /// 获得获得假期年份信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        protected string GetLeaveTime(string year, string month)
        {
            return year + "-" + month;
        }

        /// <summary>
        /// 查询OT统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Grid1.DataSource = BindInfo();
            Grid1.DataBind();
        }


        private void AddAnnual(ESP.HumanResource.Entity.EmployeeBaseInfo userModel, int year)
        {
           
            var annualModel = (new ESP.Administrative.BusinessLogic.ALAndRLManager()).GetALAndRLModel(userModel.UserID, year, 1);
            var user = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(userModel.UserID);

            double totalAnnualDays = userModel.AnnualLeaveBase;

            if (userModel.JoinDate.Value.Year == year)
            {

                DateTime EntryTime = userModel.JoinDate.Value;
                DateTime expiryDate = new DateTime(EntryTime.Year, 12, 31);
                int OneYearDays = 365;
                if (DateTime.IsLeapYear(userModel.JoinDate.Value.Year))
                {
                    OneYearDays = 366;
                }

                DateTime endDay = expiryDate.AddDays(1);
                TimeSpan span = endDay - EntryTime;


                // 截止离职日期所享有的年假总数
                totalAnnualDays = span.TotalDays / OneYearDays * ((double)userModel.AnnualLeaveBase);

                int tempdays = (int)totalAnnualDays;
                if ((tempdays + 0.5) >= totalAnnualDays)
                    totalAnnualDays = tempdays;
                else
                    totalAnnualDays = tempdays + 0.5;

            }

            if (annualModel == null && userModel.AnnualLeaveBase != 0)
            {
                annualModel = new ALAndRLInfo();
                annualModel.UserID = userModel.UserID;
                annualModel.UserCode = userModel.Code;
                annualModel.CreateTime = DateTime.Now;
                annualModel.Deleted = false;
                annualModel.EmployeeName = user.LastNameCN + user.FirstNameCN;
                annualModel.UserName = user.Username;
                annualModel.LeaveMonth = 0;
                annualModel.LeaveNumber = (decimal)totalAnnualDays;
                annualModel.LeaveType = 1;
                annualModel.LeaveYear = year;
                annualModel.OperatorID = CurrentUserID;
                annualModel.RemainingNumber = (decimal)totalAnnualDays;
                annualModel.UpdateTime = DateTime.Now;
                annualModel.ValidTo = new DateTime(year, 12, 31);
                (new ESP.Administrative.BusinessLogic.ALAndRLManager()).Add(annualModel);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int userid = int.Parse(hidAddUserId.Value);
            int year = int.Parse(ddlYearAdd.SelectedValue);

            ESP.HumanResource.Entity.EmployeeBaseInfo userModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userid);

            ALAndRLManager alManager =new ALAndRLManager();

            List<ALAndRLInfo> existList = alManager.GetModelList(" userid =" + userid + " and LeaveNumber=0");

            if (existList != null && existList.Count > 0)
            {
                foreach (var al in existList)
                {
                    alManager.Delete(al.ID);
                }
            }

            if (DateTime.Parse(txtJoinDate.Text) != userModel.JoinDate || int.Parse(ddlAnnualType.SelectedValue) != userModel.AnnualLeaveBase)
            {
                userModel.JoinDate = DateTime.Parse(txtJoinDate.Text);
                userModel.AnnualLeaveBase = int.Parse(ddlAnnualType.SelectedValue);
                ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(userModel);
            }

            AddAnnual(userModel, year);

            txtUserNameAdd.Text = string.Empty;
            txtCodeAdd.Text = string.Empty;
            lblUserNameAdd.Text = string.Empty;
            ddlAnnualType.SelectedValue = "0";
            txtJoinDate.Text = string.Empty;
            txtWorkBegin.Text = string.Empty;
            hidAddUserId.Value = string.Empty;

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + userModel.FullNameCN + "法定假已经计算并添加完毕!');", true);

        }

        protected void Grid1_InsertCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            UpdateDb(e.Item, "INSERT");
        }

        protected void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            UpdateDb(e.Item, "UPDATE");
        }

        protected void Grid1_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            UpdateDb(e.Item, "DELETE");
        }

        public void OnNeedRebind(object sender, EventArgs oArgs)
        {
            Grid1.DataBind();
        }

        public void OnNeedDataSource(object sender, EventArgs oArgs)
        {
            drpDate();
            LoadDepartmentInfo();
            Grid1.DataSource = BindInfo();

        }


        private void UpdateDb(ComponentArt.Web.UI.GridItem item, string command)
        {
            try
            {
                ALAndRLInfo model = new ALAndRLInfo();
                switch (command)
                {
                    case "INSERT":
                        if (item["UserCode"] != null && !string.IsNullOrEmpty(item["UserCode"].ToString()))
                        {
                            string userCode = item["UserCode"].ToString().Trim();

                            ESP.Framework.Entity.EmployeeInfo employeeBaseModel = ESP.Framework.BusinessLogic.EmployeeManager.GetByCode(userCode);
                            if (employeeBaseModel != null)
                            {
                                decimal leaveNumber = decimal.Parse(item["LeaveNumber"].ToString().Trim());
                                decimal remainingNumber = decimal.Parse(item["RemainingNumber"].ToString().Trim());
                                if (remainingNumber <= leaveNumber)
                                {
                                    ALAndRLInfo arModel = new ALAndRLInfo();
                                    arModel.UserID = employeeBaseModel.UserID;
                                    arModel.UserCode = userCode;
                                    arModel.UserName = employeeBaseModel.Username;
                                    // 中文名
                                    arModel.EmployeeName = employeeBaseModel.FullNameCN;
                                    // 年假年份
                                    arModel.LeaveYear = int.Parse(item["LeaveYear"].ToString().Trim());
                                    // 年假总天数
                                    arModel.LeaveNumber = decimal.Parse(item["LeaveNumber"].ToString().Trim());
                                    // 剩余年假数
                                    arModel.RemainingNumber = decimal.Parse(item["RemainingNumber"].ToString().Trim());
                                    // 年假类型
                                    arModel.LeaveType = (int)AAndRLeaveType.AnnualType;
                                    arModel.ValidTo = new DateTime(arModel.LeaveYear, Status.AnnualLeaveLastMonth, Status.AnnualLeaveLastDay);
                                    arModel.CreateTime = DateTime.Now;
                                    arModel.UpdateTime = DateTime.Now;
                                    arModel.Deleted = false;
                                    arModel.OperatorID = UserID;
                                    arModel.OperatorDept = 0;
                                    manager.Add(arModel);
                                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + employeeBaseModel.FullNameCN + "的年假信息添加成功!');", true);
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('剩余年假不能大于年假总数，添加失败!');", true);
                                    return;
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(string), "", "alert('员工编号有误，添加失败!');", true);
                                return;
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(string), "", "alert('员工编号不能为空，添加失败!');", true);
                            return;
                        }
                        break;
                    case "UPDATE":
                        int modelid = int.Parse(item["ID"].ToString());
                        model = manager.GetModel(modelid);
                        if (model != null)
                        {
                            decimal leaveNumber = decimal.Parse(item["LeaveNumber"].ToString().Trim());
                            decimal remainingNumber = decimal.Parse(item["RemainingNumber"].ToString().Trim());
                            if (remainingNumber <= leaveNumber)
                            {
                                model.UpdateTime = DateTime.Now;
                                model.OperatorID = UserID;
                                model.LeaveNumber = decimal.Parse(item["LeaveNumber"].ToString().Trim());
                                model.RemainingNumber = decimal.Parse(item["RemainingNumber"].ToString().Trim());
                                model.LeaveYear = int.Parse(item["LeaveYear"].ToString().Trim());
                                manager.Update(model);
                                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.EmployeeName + "的年假信息更新成功!');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(string), "", "alert('剩余年假不能大于年假总数，修改失败!');", true);
                                return;
                            }
                        }
                        break;
                    case "DELETE":
                        int modeldelid = int.Parse(item["ID"].ToString());
                        model = manager.GetModel(modeldelid);
                        if (model != null)
                        {
                            manager.Delete(modeldelid);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('年假信息操作失败，请与系统管理员联系!');", true);
                ESP.Logging.Logger.Add(ex.Message, "年假信息操作", ESP.Logging.LogLevel.Error, ex);
                return;
            }
        }

        #region 部门信息加载
        /// <summary>
        /// 加载一级部门
        /// </summary>
        public void LoadDepartmentInfo()
        {
            IList<DepartmentInfo> deptlist = DepartmentManager.GetAll();
            if (deptlist != null && deptlist.Count > 0)
            {
                foreach (DepartmentInfo dept in deptlist)
                {
                    if (dept.DepartmentLevel == 1)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Text = dept.DepartmentName;
                        item.Value = dept.DepartmentID.ToString();
                        cbCompany.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 根据用户选择的一级部门加载相应的二级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
                {
                    int companyId = int.Parse(cbCompany.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(companyId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment1.Items.Clear();
                        cbDepartment1.SelectedItem = null;
                        cbDepartment1.Text = "";
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }

        /// <summary>
        /// 根据用户选择的二级部门加载相应的三级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
                {
                    int departmentId = int.Parse(cbDepartment1.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(departmentId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment2.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }
        #endregion

        protected void btnAddSearch_Click(object sender, EventArgs e)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(txtUserNameAdd.Text))
            {
                strWhere = " and b.username like '%" + txtUserNameAdd.Text + "%'";
            }
            if (!string.IsNullOrEmpty(txtCodeAdd.Text))
            {
                strWhere = " and a.code like '%" + txtCodeAdd.Text + "%'";
            }
            ESP.HumanResource.Entity.EmployeeBaseInfo userModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere).FirstOrDefault();

            if (userModel != null)
            {
                txtUserNameAdd.Text = userModel.Username;
                txtCodeAdd.Text = userModel.Code;
                lblUserNameAdd.Text = userModel.FullNameCN;
                ddlAnnualType.SelectedValue = userModel.AnnualLeaveBase.ToString();
                if (userModel.WorkBegin != null)
                    txtWorkBegin.Text = userModel.WorkBegin.Value.ToString("yyyy-MM-dd");
                if (userModel.JoinDate != null)
                    txtJoinDate.Text = userModel.JoinDate.Value.ToString("yyyy-MM-dd");
                hidAddUserId.Value = userModel.UserID.ToString();

                ddlAnnualType.SelectedValue = userModel.AnnualLeaveBase.ToString();

            }

        }

        protected void btnBatchAdd_Click(object sender, EventArgs e)
        {
            var empList = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(" and a.status in(1,3)");
            int year = int.Parse(ddlYearAdd.SelectedValue);
            ALAndRLManager alManager = new ALAndRLManager();

            foreach (var userModel in empList)
            {
                List<ALAndRLInfo> existList = alManager.GetModelList(" userid =" + userModel.UserID + " and LeaveNumber=0");

                if (existList != null && existList.Count > 0)
                {
                    foreach (var al in existList)
                    {
                        alManager.Delete(al.ID);
                    }
                }

                AddAnnual(userModel, year);
            }

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + year.ToString() + "法定假已经计算并导入完毕!');", true);

        }
    }
}