using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

public partial class DimissionFormPrint : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["dimissionid"]))
            {
                int dimissionid = 0;
                if (!int.TryParse(Request["dimissionid"], out dimissionid))
                    dimissionid = 0;
                InitPage(dimissionid);
            }
        }
    }

    protected void InitPage(int dimissionid)
    {
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionid);
        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + dimissionFormInfo.UserId);  // 用户部门信息
        ESP.HumanResource.Entity.EmployeeBaseInfo employeeModel = EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);  // 用户基本信息

        radEmailIsDeleteFalse.Enabled = false;
        radEmailIsDeleteTrue.Enabled = false;
        //radAccountIsDeleteTrue.Enabled = false;
        //radAccountIsDeleteFalse.Enabled = false;
        radIsHaveArchivesFalse.Enabled = false;
        radIsHaveArchivesTrue.Enabled = false;
        if (dimissionFormInfo != null)
        {
            // 用户基本信息
            labUserCode.Text = dimissionFormInfo.UserCode;
            labuserName.Text = dimissionFormInfo.UserName;
            labPosition.Text = departments[0].DepartmentPositionName;
            labGroupName.Text = dimissionFormInfo.DepartmentName;
            labLastDate.Text = dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
            labEmail.Text = dimissionFormInfo.PrivateMail;
            labMobilePhone.Text = dimissionFormInfo.MobilePhone;

            // 业务交接信息
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList =
                ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionData(dimissionFormInfo.DimissionId);
            StringBuilder detailInfoStr = new StringBuilder();  // 业务交接信息
            StringBuilder receiverNameStr = new StringBuilder();  // 业务交接人信息
            if (detailList != null && detailList.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.DimissionDetailsInfo detail in detailList)
                {
                    detailInfoStr.Append(detail.FormCode + "(" + detail.FormType + "),");
                    if (receiverNameStr.ToString().IndexOf(detail.ReceiverName) == -1)
                        receiverNameStr.Append(detail.ReceiverName + ",");
                }
                labDetailInfo.Text = "交接正常";  //detailInfoStr.ToString().TrimEnd(',');
                labReceiverName.Text = receiverNameStr.ToString().TrimEnd(',');
            }
            else
            {
                labDetailInfo.Text = "无";
                labReceiverName.Text = "无";
            }
            labDirector.Text = dimissionFormInfo.DirectorName;
            labManager.Text = dimissionFormInfo.ManagerName;

            // 团队行政信息
            ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo groupHRDetailInfo =
                ESP.HumanResource.BusinessLogic.DimissionGrougHRDetailsManager.GetGroupHRDetailInfo(dimissionFormInfo.DimissionId);
            if (groupHRDetailInfo != null)
            {
                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userBasicManager = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                ESP.Administrative.BusinessLogic.AttendanceManager attMan = new ESP.Administrative.BusinessLogic.AttendanceManager();
                ESP.Administrative.Entity.UserAttBasicInfo userBasicModel = userBasicManager.GetModelByUserid(dimissionFormInfo.UserId);
                if (userBasicModel != null)
                {
                    DateTime preMonth = dimissionFormInfo.LastDay.Value.AddMonths(-1);
                    // 员工上月考勤信息
                    ESP.Administrative.BusinessLogic.MonthStatManager monthStat = new ESP.Administrative.BusinessLogic.MonthStatManager();
                    ESP.Administrative.Entity.MonthStatInfo preMonthStatInfo = monthStat.GetMonthStatInfoApprove(dimissionFormInfo.UserId, preMonth.Year, preMonth.Month);

                    // 员工当月考勤信息
                    ESP.Administrative.Entity.MonthStatInfo curMonthStatInfo = monthStat.GetMonthStatInfoApprove(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value.Year, dimissionFormInfo.LastDay.Value.Month);

                    #region 上月考勤
                    StringBuilder strPreMonthStatInfo = new StringBuilder();
                    if (preMonthStatInfo != null)
                    {
                        if (preMonthStatInfo.LateCount > 0)
                            strPreMonthStatInfo.Append("迟到：" + preMonthStatInfo.LateCount + "，");    // 迟到

                        if (preMonthStatInfo.LeaveEarlyCount > 0)
                            strPreMonthStatInfo.Append("早退：" + preMonthStatInfo.LeaveEarlyCount + "，");   // 早退

                        if (preMonthStatInfo.AbsentDays > 0)
                            strPreMonthStatInfo.Append("旷工：" + string.Format("{0:F1}", preMonthStatInfo.AbsentDays / ESP.Administrative.Common.Status.WorkingHours) + "D，");    // 旷工

                        if (preMonthStatInfo.SickLeaveHours > 0)
                            strPreMonthStatInfo.Append("病假：" + string.Format("{0:F1}", preMonthStatInfo.SickLeaveHours) + "H，"); // 病假

                        if (preMonthStatInfo.AffairLeaveHours > 0)
                            strPreMonthStatInfo.Append("事假：" + string.Format("{0:F1}", preMonthStatInfo.AffairLeaveHours) + "H，");  // 事假

                        if (preMonthStatInfo.AnnualLeaveDays > 0)
                            strPreMonthStatInfo.Append("年假：" + string.Format("{0:F1}", preMonthStatInfo.AnnualLeaveDays / ESP.Administrative.Common.Status.WorkingHours) + "D，");  // 年假

                        if (preMonthStatInfo.PCRefundAmount > 0)
                            strPreMonthStatInfo.Append("笔记本报销：" + preMonthStatInfo.PCRefundAmount.ToString() + "，");
                    }
                    labPreMonthStatInfo.Text = strPreMonthStatInfo.ToString().TrimEnd('，');
                    #endregion

                    #region 当月考勤
                    StringBuilder strCurMonthStatInfo = new StringBuilder();
                    if (curMonthStatInfo != null)
                    {
                        if (curMonthStatInfo.LateCount > 0)
                            strCurMonthStatInfo.Append("迟到：" + curMonthStatInfo.LateCount + "，");    // 迟到

                        if (curMonthStatInfo.LeaveEarlyCount > 0)
                            strCurMonthStatInfo.Append("早退：" + curMonthStatInfo.LeaveEarlyCount + "，");   // 早退

                        if (curMonthStatInfo.AbsentDays > 0)
                            strCurMonthStatInfo.Append("旷工：" + string.Format("{0:F1}", curMonthStatInfo.AbsentDays / ESP.Administrative.Common.Status.WorkingHours) + "D，");    // 旷工

                        if (curMonthStatInfo.SickLeaveHours > 0)
                            strCurMonthStatInfo.Append("病假：" + string.Format("{0:F1}", curMonthStatInfo.SickLeaveHours) + "H，"); // 病假

                        if (curMonthStatInfo.AffairLeaveHours > 0)
                            strCurMonthStatInfo.Append("事假：" + string.Format("{0:F1}", curMonthStatInfo.AffairLeaveHours) + "H，");  // 事假

                        if (curMonthStatInfo.AnnualLeaveDays > 0)
                            strCurMonthStatInfo.Append("年假：" + string.Format("{0:F1}", curMonthStatInfo.AnnualLeaveDays / ESP.Administrative.Common.Status.WorkingHours) + "D，");  // 年假

                        if (curMonthStatInfo.PCRefundAmount > 0)
                            strCurMonthStatInfo.Append("笔记本报销：" + curMonthStatInfo.PCRefundAmount.ToString() + "，");
                    }
                    labCurMonthStatInfo.Text = strCurMonthStatInfo.ToString().TrimEnd('，');
                    #endregion
                }

                //labAnnual.Text = groupHRDetailInfo.RemainAnnual.ToString();
                //labAdvanceAnnual.Text = groupHRDetailInfo.AdvanceAnnual.ToString();
                labFixedAssets.Text = groupHRDetailInfo.FixedAssets;
                labGroupHRPrincipal1.Text = groupHRDetailInfo.PrincipalName;
                labGroupHRPrincipal2.Text = groupHRDetailInfo.PrincipalName;
            }

            double annualBase = 0;
            double rewardBase = 0;

            double remainAnnual = 0;
            double canUseAnnual = 0;
            double usedAnnual = 0;

            double remainReward = 0;
            double canUseReward = 0;
            double usedReward = 0;
            try
            {
                remainAnnual = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, out canUseAnnual, out usedAnnual, out annualBase);
                remainReward = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetRewardLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, out canUseReward, out usedReward, out rewardBase);
            }
            catch
            {
                remainAnnual = 0;
                remainReward = 0;
            }
            //
            double canUseTotal = (canUseAnnual + canUseReward);//2.5+2.5

            int tempdays = (int)canUseTotal;
            //if ((tempdays + 0.5) >= canUseTotal)
            //    canUseTotal = tempdays;
            //else
            //    canUseTotal = tempdays + 0.5;
            canUseTotal = tempdays;


            double yuzhiTotal = (usedAnnual + usedReward) - canUseTotal > 0 ? (usedAnnual + usedReward) - canUseTotal : 0;//1
            double yuzhiAnnual = 0;// yuzhiTotal - usedReward > 0 ? yuzhiTotal - usedReward : 0;//0
            double yuzhiReward = 0;// usedReward - canUseReward > 0 ? usedReward - canUseReward : 0;//0
            //6.5-3.5
            if (canUseTotal >= annualBase)
                yuzhiReward = yuzhiTotal;
            else
            {
                yuzhiReward = usedReward;
                yuzhiAnnual = yuzhiTotal - usedReward;
            }
            //年假余
            labAnnual.Text = remainAnnual < 0 ? "0" : ((int)remainAnnual).ToString("#,##0.0");
            labOverDraft.Text = yuzhiTotal.ToString("#,##0.0");
            labOverAnnual.Text = yuzhiAnnual.ToString("#,##0.0");

            labOverReward.Text = yuzhiReward.ToString("#,##0.0");

            // 财务信息
            ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
               ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionFormInfo.DimissionId);
            if (financeDetailInfo != null)
            {
                labLoan.Text = financeDetailInfo.Loan.Replace("\r\n", "<br />");
                labBusinessCard.Text = financeDetailInfo.BusinessCard;
                labAccountsPayable.Text = financeDetailInfo.AccountsPayable.Replace("\r\n", "<br />");
                labSalary.Text = financeDetailInfo.Salary;
                labOther.Text = financeDetailInfo.Other;
                labTellers.Text = financeDetailInfo.TellerNames.TrimEnd(',');
                labBusinessCardAudits.Text = financeDetailInfo.BusinessCardAuditNames.TrimEnd(',');
                labAccountants.Text = financeDetailInfo.AccountantNames.TrimEnd(',');
                labFinanceDirector.Text = financeDetailInfo.DirectorName;
            }

            // IT信息
            ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo =
                ESP.HumanResource.BusinessLogic.DimissionITDetailsManager.GetITDetailInfo(dimissionFormInfo.DimissionId);
            if (itDetailInfo != null)
            {
                labCompanyEmail.Text = itDetailInfo.Email.Substring(0,10)+"<br/>"+itDetailInfo.Email.Substring(10);
                if (itDetailInfo.EmailIsDelete)
                {
                    radEmailIsDeleteTrue.Checked = true;
                    radEmailIsDeleteFalse.Checked = false;
                }
                else
                {
                    radEmailIsDeleteTrue.Checked = false;
                    radEmailIsDeleteFalse.Checked = true;
                    labEmailSaveLastDay.Text = itDetailInfo.EmailSaveLastDay.Value.ToString("yyyy-MM-dd");
                }
                //if (itDetailInfo.AccountIsDelete)
                //{
                //    radAccountIsDeleteTrue.Checked = true;
                //    radAccountIsDeleteFalse.Checked = false;
                //}
                //else
                //{
                //    radAccountIsDeleteTrue.Checked = false;
                //    radAccountIsDeleteFalse.Checked = true;
                //    labAccountSaveLastDay.Text = itDetailInfo.AccountSaveLastDay.Value.ToString("yyyy-MM-dd");
                //}
                labPCCode.Text = itDetailInfo.PCCode;
                labPCUsedDes.Text = itDetailInfo.PCUsedDes;
                labITOther.Text = itDetailInfo.OtherDes;
                labOwnPCCode.Text = itDetailInfo.OwnPCCode;
                labITPrincipal.Text = itDetailInfo.PrincipalName;
            }

            // 集团行政信息
            ESP.HumanResource.Entity.DimissionADDetailsInfo adDetailInfo =
                ESP.HumanResource.BusinessLogic.DimissionADDetailsManager.GetADDetailInfo(dimissionFormInfo.DimissionId);
            if (adDetailInfo != null)
            {
                labDoorCard.Text = adDetailInfo.DoorCard;
                //labLibraryManage.Text = adDetailInfo.LibraryManage;
                labADPrincipal.Text = adDetailInfo.PrincipalName;
            }

            // 集团人力资源
            ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo =
                ESP.HumanResource.BusinessLogic.DimissionHRDetailsManager.GetHRDetailInfo(dimissionFormInfo.DimissionId);
            if (hrDetailInfo != null)
            {
                labSocialInsY.Text = hrDetailInfo.SocialInsLastMonth.Year.ToString();
                labSocialInsM.Text = hrDetailInfo.SocialInsLastMonth.Month.ToString();
                labMedicalInsY.Text = hrDetailInfo.MedicalInsLastMonth.Year.ToString();
                labMedicalInsM.Text = hrDetailInfo.MedicalInsLastMonth.Month.ToString();
                labCapitalReserveY.Text = hrDetailInfo.CapitalReserveLastMonth.Year.ToString();
                labCapitalReserveM.Text = hrDetailInfo.CapitalReserveLastMonth.Month.ToString();

                if (hrDetailInfo.IsComplementaryMedical && hrDetailInfo.AddedMedicalInsLastMonth != null)
                {
                    labAddedMedicalIns.Text =
                        hrDetailInfo.AddedMedicalInsLastMonth.Value.Year.ToString()
                        + "年"
                        + hrDetailInfo.AddedMedicalInsLastMonth.Value.Month.ToString() 
                        + "月";
                }
                else
                {
                    labAddedMedicalIns.Text = "无";
                }
                if (hrDetailInfo.IsArchives)
                {
                    radIsHaveArchivesTrue.Checked = true;
                    radIsHaveArchivesFalse.Checked = false;
                    labTurnAroundDate.Text = hrDetailInfo.TurnAroundDate == null ? "_____________" : hrDetailInfo.TurnAroundDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    radIsHaveArchivesTrue.Checked = false;
                    radIsHaveArchivesFalse.Checked = true;
                }
                labHRPrincipal1.Text = hrDetailInfo.Principal1Name;
                labHRPrincipal2.Text = hrDetailInfo.Principal2Name;
            }
        }
    }

    protected void gvDetailView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label lab = e.Row.FindControl("labReceiverStatus") as Label;

            Label labReceiver = e.Row.FindControl("labReceiverName") as Label;
            HiddenField hid = e.Row.FindControl("hidReceiverName") as HiddenField;

            ESP.HumanResource.Entity.DimissionDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.DimissionDetailsInfo;
            if (detailsInfo.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
                lab.Text = "未确认";
            else if (detailsInfo.Status == (int)ESP.HumanResource.Common.AuditStatus.Audited)
                lab.Text = "已确认";
            else
                lab.Text = "已驳回";

            labReceiver.Text = detailsInfo.ReceiverName;
            hid.Value = detailsInfo.ReceiverId.ToString();
        }
    }
}