using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using System.Data;
using ESP.Finance.Utility;

namespace ProjectMenagementWeb.Dialogs
{
    public partial class PercentForProjectDlg : ESP.Web.UI.PageBase
    {
        string projectInfoClientID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
                {
                    BindSchedule();
                }
            }
        }

        private IList<ProjectScheduleInfo> infoList = new List<ProjectScheduleInfo>();
        private void BindSchedule()
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            IList<ProjectScheduleInfo> list = ESP.Finance.BusinessLogic.ProjectScheduleManager.GetList("ProjectID = " + Request[ESP.Finance.Utility.RequestName.ProjectID]);
            ESP.Finance.Entity.DeadLineInfo deadLine = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();

            decimal totalPercent = 0;
            decimal totalFee = 0;
            string users = GetUser();
            bool IsAdded = false;
            int rowCount = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.Percent]);
            if (users.IndexOf("," + CurrentUser.SysID + ",") >= 0)
            {
                rowCount += 6;
                IsAdded = true;
            }
            else if (projectModel.EndDate != null)
            {
                if ((DateTime.Now.Year > projectModel.EndDate.Value.Year) || (DateTime.Now.Year == projectModel.EndDate.Value.Year && DateTime.Now.Month >= projectModel.EndDate.Value.Month))
                {
                    if (IsAdded == false)
                        rowCount += 6;
                }
            }
            string beginYear = Request[ESP.Finance.Utility.RequestName.BeginYear];
            string beginMonth = Request[ESP.Finance.Utility.RequestName.BeginMonth];
            decimal ServiceFee = 0;
            if (projectModel.IsCalculateByVAT == 1)
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectModel, null);
            else
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectModel, null);
            this.lblPercent2.Text = "100.00";
            this.lblTotalFee2.Text = ServiceFee.ToString("#,##0.00");
            for (int i = 0; i < rowCount; i++)
            {
                ProjectScheduleInfo info = new ProjectScheduleInfo();

                int month = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.BeginMonth]) + i;
                int year = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.BeginYear]);
                if (month >= 13)
                {
                    if (month % 13 == 0)
                        year = year + Convert.ToInt32(month / 13) - 1;
                    else
                        year = year + Convert.ToInt32(month / 13);
                    month = month % 13;
                    if (month == 0)
                        month = 13;

                }
                if (month == 13)
                {
                    rowCount++;
                }
                info.monthValue = month;
                info.YearValue = year;
                foreach (ProjectScheduleInfo schedule in list)
                {
                    if (info.YearValue == schedule.YearValue && info.monthValue == schedule.monthValue)
                    {
                        info.ScheduleID = schedule.ScheduleID;
                        info.MonthPercent = schedule.MonthPercent;
                        if (schedule.Fee == null)
                            info.Fee = 0;
                        else
                            info.Fee = schedule.Fee;
                        info.ProjectID = schedule.ProjectID;
                        totalPercent += schedule.MonthPercent.Value;
                        totalFee += info.Fee == null ? 0 : info.Fee.Value;
                        break;
                    }
                }
                infoList.Add(info);
            }
            this.gv.DataSource = infoList;
            this.gv.DataBind();
            this.lblTotal.Text = totalPercent.ToString("0.00");
            this.lblTotalFee.Text = (totalFee).ToString("#,##0.00");
        }

        private void SaveNewSchedule(ProjectScheduleInfo model)
        {
            ESP.Finance.BusinessLogic.ProjectScheduleManager.Add(model);
        }

        private void UpdateSchedule(ProjectScheduleInfo model)
        {
            ProjectScheduleInfo schedule = new ProjectScheduleInfo();
            //找到当前月份的结账日的设置
            IList<ProjectScheduleInfo> list = ESP.Finance.BusinessLogic.ProjectScheduleManager.GetList("ProjectID = " + model.ProjectID.ToString() + " AND YearValue=" + model.YearValue.Value.ToString() + " AND monthValue=" + model.monthValue.Value.ToString());
            if (list != null && list.Count > 0)
            {
                schedule = list[0];
                schedule.MonthPercent = model.MonthPercent;
                schedule.Fee = model.Fee;
                ESP.Finance.BusinessLogic.ProjectScheduleManager.Update(schedule);   //更新原有记录
            }
            else
            {
                ESP.Finance.BusinessLogic.ProjectScheduleManager.Add(model);   //添加新记录，说明原有记录中没有相对应的记录
            }
        }


        private void DeleteProjectSchedule(ProjectInfo projectModel)
        {
            string strCondition = string.Empty;
            strCondition += "ProjectID=" + projectModel.ProjectId.ToString();
            strCondition += "AND (((YearValue > " + DateTime.Now.Year.ToString() + ") ";
            strCondition += " OR (YearValue = " + DateTime.Now.Year.ToString() + " AND monthValue > " + DateTime.Now.Month.ToString() + ") ";
            DeadLineInfo line = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();
            if (line.ProjectDeadLineDay < DateTime.Now.Day)
            {
                strCondition += " OR (YearValue = " + DateTime.Now.Year.ToString() + " AND monthValue = " + DateTime.Now.Month.ToString() + ") ";
            }
            if (projectModel.BeginDate == null || projectModel.EndDate == null)
            {
                strCondition += ")";
            }
            else
            {
                strCondition += ") or (((YearValue<" + projectModel.BeginDate.Value.Year + ") or (YearValue=" + projectModel.BeginDate.Value.Year + " and monthValue<" + projectModel.BeginDate.Value.Month + ")) or ((yearValue>" + projectModel.EndDate.Value.Year + ") or (yearValue=" + projectModel.EndDate.Value.Year + " and monthValue>" + projectModel.EndDate.Value.Month + "))))";
            }
            ESP.Finance.BusinessLogic.ProjectScheduleManager.Delete(strCondition);
        }
        private void SaveProjectSchedule(ProjectInfo projectModel, IList<ProjectScheduleInfo> scheduleList)
        {
            DeleteProjectSchedule(projectModel);   //删除原有记录（只删除预计中的日期比当前日期大的记录）
            DeadLineInfo closeDate = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();
            foreach (ProjectScheduleInfo model in scheduleList)
            {
                //如果预计中的年份大于当前年份 或者 等与当前年份但月份大于当前月份，就可以生成新的记录
                if (model.YearValue.Value > DateTime.Now.Year || (model.YearValue.Value == DateTime.Now.Year && model.monthValue.Value > DateTime.Now.Month))
                {
                    SaveNewSchedule(model);
                }
                //否则，如果预计中的年份等于当前年份并且月份等于当前月份，就要到数据库中查找当前月结账日的记录，如果没有结账日的设置或结账日大于当前日期，就添加新记录，否则更新原有记录。
                else if ((model.YearValue.Value == DateTime.Now.Year && model.monthValue.Value == DateTime.Now.Month))
                {

                    if (closeDate.ProjectDeadLineDay < DateTime.Now.Day)
                    {
                        SaveNewSchedule(model);
                    }
                    else
                    {
                        UpdateSchedule(model);
                    }
                }
                //否则更新原有记录或添加新的记录（是否要添加新记录在UpdateSchedule方法中有判断）
                else
                {
                    UpdateSchedule(model);
                }
            }
        }
        //平均分配未填百分比
        private void AutoCreatePercent()
        {
            decimal percent = 0;
            int txtAlreadyCount = 0;
            decimal totalPercent = 0;
            decimal totalFee = 0;

            ProjectInfo projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

            decimal ServiceFee = 0;

            if (projectmodel.IsCalculateByVAT == 1)
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectmodel, null);
            else
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectmodel, null);
            ESP.Finance.Entity.DeadLineInfo deadline = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();
            foreach (GridViewRow dr in this.gv.Rows)
            {
                if (dr.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtPercent = (TextBox)dr.FindControl("txtPercent");
                    TextBox txtFee = (TextBox)dr.FindControl("txtFee");
                    if (txtPercent != null && txtPercent.Text != string.Empty && StringHelper.IsConvertDecimal(txtPercent.Text))
                    {
                        percent += Convert.ToDecimal(txtPercent.Text);
                        txtAlreadyCount += 1;
                        totalPercent += Convert.ToDecimal(txtPercent.Text);
                        if (txtFee != null && txtFee.Text != string.Empty && StringHelper.IsConvertDecimal(txtFee.Text))
                        {
                            totalFee += Convert.ToDecimal(txtFee.Text);
                        }
                        else
                        {
                            totalFee += ServiceFee * Convert.ToDecimal(txtPercent.Text) / 100;
                        }
                    }
                }
            }
            int createCout = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.Percent]) - txtAlreadyCount;

            if (createCout == 0)
                return;

            decimal createPercent = (100 - percent) / createCout;
            decimal PreviousPercent = 0;
            int PreviousCount = 0;
            int calculator = 0;
            createPercent = Convert.ToDecimal(createPercent.ToString("0.00"));
            foreach (GridViewRow dr in this.gv.Rows)
            {
                if (dr.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtPercent = (TextBox)dr.FindControl("txtPercent");
                    TextBox txtFee = (TextBox)dr.FindControl("txtFee");
                    Label txtYear = (Label)dr.FindControl("txtYear");
                    Label txtMonth = (Label)dr.FindControl("txtMonth");

                    if (txtPercent != null && (txtPercent.Text == string.Empty || !StringHelper.IsConvertDecimal(txtPercent.Text)))
                    {
                        calculator++;
                        if (deadline.ProjectDeadLineYear > Convert.ToInt32(txtYear.Text) || (deadline.ProjectDeadLineYear == Convert.ToInt32(txtYear.Text) && deadline.ProjectDeadLineMonth > Convert.ToInt32(txtMonth.Text)) || (deadline.ProjectDeadLineYear == Convert.ToInt32(txtYear.Text) && deadline.ProjectDeadLineMonth == Convert.ToInt32(txtMonth.Text) && deadline.ProjectDeadLineDay < DateTime.Now.Day))
                        {
                            txtPercent.Text = "0";
                            txtFee.Text = "0";
                            PreviousPercent += Convert.ToDecimal(createPercent.ToString("0.00"));
                            PreviousCount++;
                        }
                        else
                        {
                            if (calculator == createCout)
                            {
                                createPercent = createPercent + (100 - (Convert.ToDecimal(createPercent.ToString("0.00")) * createCout + percent)); //如果是最后一条自动填充的记录，就要减去多余或加上少的无法平均除开小数
                                createPercent = Convert.ToDecimal(createPercent.ToString("0.00"));
                            }
                            if (calculator - 1 == PreviousCount)
                            {
                                txtPercent.Text = (createPercent + PreviousPercent).ToString("0.00");
                                totalPercent += Convert.ToDecimal((createPercent + PreviousPercent).ToString("0.00"));
                                txtFee.Text = (ServiceFee * (Convert.ToDecimal((createPercent + PreviousPercent).ToString("0.00"))) / 100).ToString("#,##0.00");
                                totalFee += Convert.ToDecimal(txtFee.Text);
                                PreviousPercent = 0;
                            }
                            else
                            {
                                txtPercent.Text = createPercent.ToString("0.00");
                                totalPercent += Convert.ToDecimal(createPercent.ToString("0.00"));
                                txtFee.Text = (ServiceFee * Convert.ToDecimal((createPercent).ToString("0.00")) / 100).ToString("#,##0.00");
                                totalFee += Convert.ToDecimal(txtFee.Text);
                            }
                        }
                    }
                }
            }

            this.lblTotal.Text = totalPercent.ToString("0.00");
            this.lblTotalFee.Text = (totalFee).ToString("#,##0.00");
        }
        protected void btnAutoCreate_Click(object sender, EventArgs e)
        {
            AutoCreatePercent();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProjectScheduleInfo info = (ProjectScheduleInfo)e.Row.DataItem;
                Label txtYear = (Label)e.Row.FindControl("txtYear");
                Label txtMonth = (Label)e.Row.FindControl("txtMonth");
                TextBox txtPercent = (TextBox)e.Row.FindControl("txtPercent");
                TextBox txtFee = (TextBox)e.Row.FindControl("txtFee");
                ESP.Finance.Entity.ProjectInfo pmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
                IList<ESP.Finance.Entity.ProjectHistInfo> histList = ESP.Finance.BusinessLogic.ProjectHistManager.GetListByProject(pmodel.ProjectId, null, null);
                ESP.Finance.Entity.DeadLineInfo deadline = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();

                if (txtYear != null && txtMonth != null && txtPercent != null)
                {
                    txtYear.Text = info.YearValue.ToString();
                    txtMonth.Text = info.monthValue.ToString();
                    if (info.MonthPercent != null)
                    {
                        txtPercent.Text = Convert.ToDecimal(info.MonthPercent).ToString("0.00");
                        txtFee.Text = info.Fee.Value.ToString("#,##0.00");
                    }
                    //年-月-当前日 小于本月结账日的不能编辑，除非是：
                    //项目号是第一次创建的才可以编辑
                    if ((info.YearValue.Value == deadline.ProjectDeadLine.Year && (info.monthValue.Value == 13 ? 12 : info.monthValue.Value) == deadline.ProjectDeadLine.Month) && this.GetUser().IndexOf(CurrentUser.SysID) < 0)
                    {
                        if (DateTime.Now.Day > deadline.ProjectDeadLine.Day)
                        {
                            txtPercent.Enabled = false;
                            txtFee.Enabled = false;
                        }
                        else
                        {
                            txtPercent.Enabled = true;
                            txtFee.Enabled = true;
                        }
                    }
                    else if (new DateTime(info.YearValue.Value, info.monthValue.Value == 13 ? 12 : info.monthValue.Value, DateTime.DaysInMonth(info.YearValue.Value, info.monthValue.Value == 13 ? 12 : info.monthValue.Value)) < deadline.ProjectDeadLine)
                    {
                        if (((pmodel.Status != (int)ESP.Finance.Utility.Status.FinanceAuditComplete && pmodel.Status != (int)ESP.Finance.Utility.Status.ProjectPreClose) && (histList == null || histList.Count == 0)) || this.GetUser().IndexOf(CurrentUser.SysID) >= 0)
                        {
                            txtPercent.Enabled = true;
                            txtFee.Enabled = true;
                        }

                        else
                        {
                            txtPercent.Enabled = false;
                            txtFee.Enabled = false;
                        }
                    }

                }
            }
        }

        protected void btnNewSupporter_Click(object sender, EventArgs e)
        {
            try
            {
                projectInfoClientID = "ctl00_ContentPlaceHolder1_ProjectPercent_";
                decimal totalPercent = 0;
                decimal totalFee = 0;
                ProjectInfo projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

                decimal ServiceFee = 0;

                if (projectmodel.IsCalculateByVAT == 1)
                    ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectmodel, null);
                else
                    ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectmodel, null);

                IList<ProjectScheduleInfo> scheduleList = new List<ProjectScheduleInfo>();
                decimal percent = 0;
                decimal fee = 0;
                int year = 0;
                int month = 0;
                foreach (GridViewRow rw in this.gv.Rows)
                {
                    if (rw.RowType == DataControlRowType.DataRow)
                    {
                        Label txtYear = (Label)rw.FindControl("txtYear");
                        Label txtMonth = (Label)rw.FindControl("txtMonth");
                        TextBox txtPercent = (TextBox)rw.FindControl("txtPercent");
                        TextBox txtFee = (TextBox)rw.FindControl("txtFee");

                        ProjectScheduleInfo model = new ProjectScheduleInfo();
                        if (txtYear != null && txtFee != null && txtPercent != null)
                        {
                            percent = string.IsNullOrEmpty(txtPercent.Text) ? 0 : Convert.ToDecimal(txtPercent.Text);
                            fee = string.IsNullOrEmpty(txtFee.Text) ? 0 : Convert.ToDecimal(txtFee.Text);
                            year = string.IsNullOrEmpty(txtYear.Text) ? 0 : Convert.ToInt32(txtYear.Text);
                            month = string.IsNullOrEmpty(txtMonth.Text) ? 0 : Convert.ToInt32(txtMonth.Text);

                            totalPercent += percent;
                            totalFee += fee;
                            model.YearValue = year;
                            model.monthValue = month;
                            model.MonthPercent = percent;
                            model.Fee = fee;
                            model.ProjectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                            scheduleList.Add(model);
                        }
                    }
                }
                if (totalFee != ServiceFee)
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('百分比信息输入有误,请检查.');", true);
                }
                else
                {
                    SaveProjectSchedule(projectmodel, scheduleList);
                }

                string script = string.Empty;
                script = @"var uniqueId = 'ctl00$ContentPlaceHolder1$ProjectPercent$';
opener.__doPostBack(uniqueId + 'btnPercent', '');
window.close(); ";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('保存失败');window.close();", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close();", true);
        }

        protected void txtFee_TextChanged(object sender, EventArgs e)
        {
            ProjectInfo projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));

            decimal totalPercent = 0;
            decimal totalFee = 0;
            decimal ServiceFee = 0;
            decimal rowFee = 0;
            decimal rowPercent = 0;

            if (projectmodel.IsCalculateByVAT == 1)
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectmodel, null);
            else
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectmodel, null);

            foreach (GridViewRow dr in gv.Rows)
            {

                TextBox txtPercent = (TextBox)dr.FindControl("txtPercent");
                TextBox txtFee = (TextBox)dr.FindControl("txtFee");
                Label txtYear = (Label)dr.FindControl("txtYear");
                Label txtMonth = (Label)dr.FindControl("txtMonth");
                try
                {
                    if (txtPercent != null && !string.IsNullOrEmpty(txtPercent.Text))
                        rowPercent = Convert.ToDecimal(txtPercent.Text);
                    else
                    {
                        rowPercent = 0;
                        txtPercent.Text = "0";
                        txtFee.Text = "0";
                    }
                    if (txtFee != null && !string.IsNullOrEmpty(txtFee.Text))
                        rowFee = Convert.ToDecimal(txtFee.Text);
                    else
                    {
                        rowFee = 0;
                        txtPercent.Text = "0";
                    }
                    if (ServiceFee == 0)
                        txtPercent.Text = "0";
                    else
                        txtPercent.Text = (rowFee / ServiceFee * 100).ToString("0.00");
                    totalFee += rowFee;
                    if (totalFee == ServiceFee)
                        txtPercent.Text = Convert.ToDecimal(100 - totalPercent).ToString("0.00");
                    totalPercent += Convert.ToDecimal(txtPercent.Text);
                }
                catch
                {
                    txtPercent.Text = "0";
                    txtFee.Text = "0";
                }
            }

            this.lblTotal.Text = totalPercent.ToString("0.00");
            this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
        }

        private string GetUser()
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[RequestName.ProjectID]));
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectModel.BranchCode);
            IList<ESP.Finance.Entity.CustomerAuditorInfo> cuslist = ESP.Finance.BusinessLogic.CustomerAudtiorManager.GetList(" BranchCode='" + projectModel.BranchCode + "' and customerCode='" + projectModel.CustomerCode + "'");

            string user = "," + branchModel.FinalAccounter.ToString() + "," + branchModel.ProjectAccounter.ToString() + branchModel.OtherFinancialUsers;
            string retuser = user;
            string[] users = user.Split(',');
            for (int i = 0; i < users.Length; i++)
            {
                if (!string.IsNullOrEmpty(users[i]))
                {
                    ESP.Framework.Entity.AuditBackUpInfo model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(users[i]));
                    if (model != null)
                    {
                        retuser += model.BackupUserID.ToString() + ",";
                    }
                }
            }
            if (cuslist != null && cuslist.Count > 0)
                retuser += cuslist[0].ProjectAuditor.ToString() + ",";

            ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectModel.GroupID.Value);

            if (branchProject != null)
            {
                retuser += branchProject.AuditorID + ",";
            }
            return retuser;
        }

        protected void txtPercent_TextChanged(object sender, EventArgs e)
        {
            decimal totalPercent = 0;
            decimal totalFee = 0;
            decimal rowFee = 0;
            decimal rowPercent = 0;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[RequestName.ProjectID]));

            decimal ServiceFee = 0;

            if (projectModel.IsCalculateByVAT == 1)
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectModel, null);
            else
                ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectModel, null);
            DeadLineInfo deadLine = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();
            foreach (GridViewRow dr in gv.Rows)
            {
                TextBox txtPercent = (TextBox)dr.FindControl("txtPercent");
                TextBox txtFee = (TextBox)dr.FindControl("txtFee");
                Label txtYear = (Label)dr.FindControl("txtYear");
                Label txtMonth = (Label)dr.FindControl("txtMonth");
                try
                {

                    if (txtPercent != null && !string.IsNullOrEmpty(txtPercent.Text))
                        rowPercent = Convert.ToDecimal(txtPercent.Text);
                    else
                    {
                        rowPercent = 0;
                        txtPercent.Text = "0";
                        txtFee.Text = "0";
                    }
                    if (txtFee != null && !string.IsNullOrEmpty(txtFee.Text))
                        rowFee = Convert.ToDecimal(txtFee.Text);
                    else
                    {
                        rowFee = 0;
                        txtFee.Text = "0";
                    }
                    if (txtFee.Enabled == true)
                        txtFee.Text = (rowPercent * ServiceFee / 100).ToString("#,##00.00");
                    totalPercent += rowPercent;
                    totalFee += Convert.ToDecimal(txtFee.Text);

                }
                catch
                {
                    txtPercent.Text = "0";
                    txtFee.Text = "0";
                }
            }
            this.lblTotal.Text = totalPercent.ToString("00.00");
            this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
        }
    }
}