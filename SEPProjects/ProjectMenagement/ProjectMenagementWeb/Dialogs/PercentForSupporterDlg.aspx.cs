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
    public partial class PercentForSupporterDlg : ESP.Web.UI.PageBase
    {
        string supporterInfoClientID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]) && !string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
                {
                    BindSchedule();
                }
            }
        }

        private IList<SupporterScheduleInfo> infoList = new List<SupporterScheduleInfo>();
        private void BindSchedule()
        {
            IList<SupporterScheduleInfo> list = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList("SupporterID = " + Request[ESP.Finance.Utility.RequestName.SupportID]);
            ESP.Finance.Entity.SupporterInfo supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            decimal totalPercent = 0;
            decimal totalFee = 0;
            bool IsAdded = false;
            int rowCount = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.Percent]);
            string users = GetUser();
            if (users.IndexOf("," + CurrentUser.SysID + ",") >= 0)
            {
                rowCount += 6;
                IsAdded = true;
            }
            else if (supporterModel.BizEndDate != null)
            {
                if ((DateTime.Now.Year > supporterModel.BizEndDate.Value.Year) || (DateTime.Now.Year == supporterModel.BizEndDate.Value.Year && DateTime.Now.Month >= supporterModel.BizEndDate.Value.Month))
                {
                    if (IsAdded == false)
                        rowCount += 6;
                }
            }
            string beginYear = Request[ESP.Finance.Utility.RequestName.BeginYear];
            string beginMonth = Request[ESP.Finance.Utility.RequestName.BeginMonth];
            decimal ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            this.lblPercent2.Text = "0.00";
            this.lblTotalFee2.Text = ServiceFee.ToString("#,##0.00");
            for (int i = 0; i < rowCount; i++)
            {
                SupporterScheduleInfo info = new SupporterScheduleInfo();
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
                info.monthValue = month;
                info.YearValue = year;
                foreach (SupporterScheduleInfo schedule in list)
                {
                    if (info.YearValue == schedule.YearValue && info.monthValue == schedule.monthValue)
                    {
                        info.SupporterID = schedule.SupporterID;
                        info.MonthPercent = schedule.MonthPercent;
                        if (schedule.Fee == null)
                            info.Fee = ServiceFee * schedule.MonthPercent.Value / 100;
                        else
                            info.Fee = schedule.Fee;
                        info.ProjectID = schedule.ProjectID;
                        totalPercent += schedule.MonthPercent.Value;
                        totalFee += info.Fee == null ? 0 : info.Fee.Value;
                    }
                }
                infoList.Add(info);
            }

            this.gv.DataSource = infoList;
            this.gv.DataBind();
            this.lblTotal.Text = totalPercent.ToString("0.00");
            this.lblTotalFee.Text = (totalFee).ToString("#,##0.00");
        }


        private string GetUser()
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[RequestName.ProjectID]));
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectModel.BranchCode);

            string user = "," + projectModel.ApplicantUserID + "," + branchModel.FinalAccounter.ToString() + "," + branchModel.ProjectAccounter.ToString() + branchModel.OtherFinancialUsers;
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

            ESP.Finance.Entity.BranchProjectInfo branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectModel.GroupID.Value);

            if (branchProject != null)
            {
                retuser += branchProject.AuditorID + ",";
            }

            return retuser;
        }

        //平均分配未填百分比
        private void AutoCreatePercent()
        {
            decimal percent = 0;
            int txtAlreadyCount = 0;
            decimal totalPercent = 0;
            decimal totalFee = 0;
            decimal ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
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
                            txtPercent.Text = "0.00";
                            txtFee.Text = "0.00";
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
                                txtFee.Text = (ServiceFee * (createPercent + PreviousPercent) / 100).ToString("#,##0.00");
                                totalFee += Convert.ToDecimal(txtFee.Text);
                                PreviousPercent = 0;
                            }
                            else
                            {
                                txtPercent.Text = createPercent.ToString("0.00");
                                totalPercent += Convert.ToDecimal(createPercent.ToString("0.00"));
                                txtFee.Text = (ServiceFee * createPercent / 100).ToString("#,##0.00");
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
                SupporterScheduleInfo info = (SupporterScheduleInfo)e.Row.DataItem;
                SupporterInfo supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
                ESP.Finance.Entity.ProjectInfo pmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);
                IList<ESP.Finance.Entity.ProjectHistInfo> histList = ESP.Finance.BusinessLogic.ProjectHistManager.GetListByProject(pmodel.ProjectId, null, null);

                ESP.Finance.Entity.DeadLineInfo deadline = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();

                Label txtYear = (Label)e.Row.FindControl("txtYear");
                Label txtMonth = (Label)e.Row.FindControl("txtMonth");
                TextBox txtPercent = (TextBox)e.Row.FindControl("txtPercent");
                TextBox txtFee = (TextBox)e.Row.FindControl("txtFee");
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
                    if (info.YearValue.Value == deadline.ProjectDeadLine.Year && (info.monthValue.Value == 13 ? 12 : info.monthValue.Value) == deadline.ProjectDeadLine.Month && this.GetUser().IndexOf(CurrentUser.SysID) < 0)
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


        private void DeleteSupporterSchedule(int supporterID)
        {
            IList<SupporterScheduleInfo> list = ESP.Finance.BusinessLogic.SupporterScheduleManager.GetList(" SupporterID =" + supporterID.ToString());
            foreach (SupporterScheduleInfo schedule in list)
            {
                ESP.Finance.BusinessLogic.SupporterScheduleManager.Delete(schedule.SupporterScheduleID);
            }
        }

        private void SaveSchedule(IList<SupporterScheduleInfo> supporterList, int supportid)
        {
            DeleteSupporterSchedule(supportid);
            foreach (SupporterScheduleInfo model in supporterList)
            {
                ESP.Finance.BusinessLogic.SupporterScheduleManager.Add(model);
            }
        }

        protected void btnNewSupporter_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                supporterInfoClientID = "ctl00_ContentPlaceHolder1_SupporterInfo_";
                decimal ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
                SupporterInfo SupporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));

                IList<SupporterScheduleInfo> scheduleList = new List<SupporterScheduleInfo>();
                decimal totalPercent = 0;
                decimal totalFee = 0;
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

                        percent = string.IsNullOrEmpty(txtPercent.Text) ? 0 : Convert.ToDecimal(txtPercent.Text);
                        fee = string.IsNullOrEmpty(txtFee.Text) ? 0 : Convert.ToDecimal(txtFee.Text);
                        year = string.IsNullOrEmpty(txtYear.Text) ? 0 : Convert.ToInt32(txtYear.Text);
                        month = string.IsNullOrEmpty(txtMonth.Text) ? 0 : Convert.ToInt32(txtMonth.Text);

                        SupporterScheduleInfo model = new SupporterScheduleInfo();
                        if (txtYear != null && txtMonth != null && txtPercent != null)
                        {
                            totalPercent += percent;
                            totalFee += fee;
                            model.YearValue = year;
                            model.monthValue = month;
                            model.MonthPercent = percent;
                            model.Fee = fee;
                            model.SupporterID = SupporterModel.SupportID;
                            model.ProjectID = SupporterModel.ProjectID;
                            model.ProjectCode = SupporterModel.ProjectCode;
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
                    SaveSchedule(scheduleList, SupporterModel.SupportID);
                }

                string script = string.Empty;
                script = @"var uniqueId = 'ctl00$ContentPlaceHolder1$SupporterInfo$';
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
            decimal totalPercent = 0;
            decimal totalFee = 0;
            decimal ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            decimal rowFee = 0;
            decimal rowPercent = 0;
            foreach (GridViewRow dr in gv.Rows)
            {
                TextBox txtPercent = (TextBox)dr.FindControl("txtPercent");
                TextBox txtFee = (TextBox)dr.FindControl("txtFee");
                try
                {
                    if (txtPercent != null && !string.IsNullOrEmpty(txtPercent.Text))
                        rowPercent = Convert.ToDecimal(txtPercent.Text);
                    else
                    {
                        rowPercent = 0;
                        txtPercent.Text = "0.00";
                        txtFee.Text = "0.00";
                    }
                    if (txtFee != null && !string.IsNullOrEmpty(txtFee.Text))
                        rowFee = Convert.ToDecimal(txtFee.Text);
                    else
                    {
                        rowFee = 0;
                        txtPercent.Text = "0.00";
                        txtFee.Text = "0.00";
                    }
                    if (ServiceFee == 0)
                        txtPercent.Text = "0.00";
                    else
                        txtPercent.Text = (rowFee / ServiceFee * 100).ToString("0.00");

                    totalPercent += Convert.ToDecimal(txtPercent.Text);
                    totalFee += rowFee;
                }
                catch
                {
                    txtPercent.Text = "0.00";
                    txtFee.Text = "0.00";
                }
            }
            this.lblTotal.Text = totalPercent.ToString("0.00");
            this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
        }


        protected void txtPercent_TextChanged(object sender, EventArgs e)
        {
            decimal totalPercent = 0;
            decimal totalFee = 0;
            decimal rowFee = 0;
            decimal rowPercent = 0;
            decimal ServiceFee = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterOddAmount(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
            foreach (GridViewRow dr in gv.Rows)
            {
                TextBox txtPercent = (TextBox)dr.FindControl("txtPercent");
                TextBox txtFee = (TextBox)dr.FindControl("txtFee");
                try
                {
                    if (txtPercent != null && !string.IsNullOrEmpty(txtPercent.Text))
                        rowPercent = Convert.ToDecimal(txtPercent.Text);
                    else
                    {
                        rowPercent = 0;
                        txtPercent.Text = "0.00";
                        txtFee.Text = "0.00";
                    }
                    if (txtFee != null && !string.IsNullOrEmpty(txtFee.Text))
                        rowFee = Convert.ToDecimal(txtFee.Text);
                    else
                    {
                        rowFee = 0;
                        txtPercent.Text = "0.00";
                        txtFee.Text = "0.00";
                    }
                    if (txtFee.Enabled == true)
                        txtFee.Text = (rowPercent * ServiceFee / 100).ToString("#,##0.00");
                    totalPercent += rowPercent;
                    totalFee += Convert.ToDecimal(txtFee.Text);
                }
                catch
                {
                    txtPercent.Text = "0.00";
                    txtFee.Text = "0.00";
                }
            }
            this.lblTotal.Text = totalPercent.ToString("0.00");
            this.lblTotalFee.Text = totalFee.ToString("#,##0.00");
        }
    }
}