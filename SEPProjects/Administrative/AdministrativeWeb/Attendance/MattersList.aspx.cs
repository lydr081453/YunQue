using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ComponentArt.Web.UI;
using ESP.Administrative.Common;

namespace AdministrativeWeb.Attendance
{
    public partial class MattersList : ESP.Web.UI.PageBase
    {
        /// <summary>
        /// 事由业务类
        /// </summary>
        private readonly MattersManager mattersManager = new MattersManager();
        private readonly SingleOvertimeManager overTimeManager = new SingleOvertimeManager();
        private readonly ApproveLogManager approveManager = new ApproveLogManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["flag"] != null)
                {
                    string flag = Request["flag"];  // flag的值1、2；分别表示删除撤销
                    int matterid = int.Parse(Request["matterid"]);
                    int formType = int.Parse(Request["formType"]);
                    if (flag == "1")  // 删除
                    {
                        DeletedMatters(matterid, formType);
                    }
                    else if (flag == "2")   // 撤销
                    {
                        CancelMatters(matterid, formType);
                    }
                }

                // drpDate();
                PickerFrom.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                PickerTo.SelectedDate = DateTime.Now;
                if (PickerFrom.SelectedDate == PickerTo.SelectedDate)
                {
                    PickerFrom.SelectedDate = DateTime.Now.AddMonths(-1);
                }

                BindInfo();
              
            }
        }

        /// <summary>
        /// 绑定下拉列表中的日期和时间内容
        /// </summary>
        //private void drpDate()
        //{
        //    int year = DateTime.Now.Year - 10;
        //    for (int i = 0; i <= 10; i++)
        //    {
        //        drpYear.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
        //    }

        //    drpYear.SelectedValue = DateTime.Now.Year.ToString();

        //    for (int i = 1; i <= 12; i++)
        //    {
        //        drpMonth.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));
        //    }

        //    if (10 < DateTime.Now.Day)
        //    {
        //        drpMonth.SelectedValue = DateTime.Now.Month.ToString("00");
        //    }
        //    else if (1 != DateTime.Now.Month)
        //    {
        //        drpMonth.SelectedValue = (DateTime.Now.Month - 1).ToString("00");
        //    }
        //    else
        //    {
        //        drpMonth.SelectedValue = (DateTime.Now.Month - 1).ToString("00");
        //        drpYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
        //    }
        //}

        /// <summary>
        /// 查询考勤事由，并绑定到Grid上
        /// </summary>
        protected void BindInfo()
        {
            string sqlStr = "";
            ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(UserID);
            
            string strWhere = string.Format(" (DirectorId={0} or ManagerId={0} or CEOId={0} or hrid={0} or HRAttendanceId={0} or ReceptionId={0})",UserID);

            var operationList = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(strWhere);
            string depts = string.Empty;
            if (operationList != null && operationList.Count > 0)
            {
                foreach (var operation in operationList)
                {
                    depts += operation.DepId + ",";
                }
            }
            depts = depts.TrimEnd(',');

            if (!string.IsNullOrEmpty(depts))
            {
                sqlStr += " and Userid in(select userid from sep_EmployeesInPositions where departmentid in(" + depts + "))";
            }
            else
            {
                sqlStr += " and Userid = " + UserID;
            }

            if (!string.IsNullOrEmpty(drpFormType.SelectedValue) && drpFormType.SelectedValue != "-1")
            {
                sqlStr += string.Format(" and FormType = {0} ", drpFormType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(PickerFrom.SelectedDate.ToString()) && PickerFrom.SelectedDate.Year != 1)
            {
                sqlStr += string.Format(" and datediff(dd, cast('{0}' as smalldatetime), CreateTime ) >= 0  ", PickerFrom.SelectedDate);
            }
            if (!string.IsNullOrEmpty(PickerTo.SelectedDate.ToString()) && PickerTo.SelectedDate.Year != 1)
            {
                sqlStr += string.Format(" and datediff(dd, cast('{0}' as smalldatetime), CreateTime ) <= 0  ", PickerTo.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtKey.Text))
            { 
                string str =string.Format(" and (MatterContent like '%{0}%' or ApproveDesc like '%{0}%' or userid in(select userid from sep_users where lastnamecn+firstNamecn like '%{0}%' or username like '%{0}%'))",txtKey.Text);
                sqlStr += str;
            }

            Grid1.DataSource = mattersManager.GetMattersViewList(sqlStr);
            Grid1.DataBind();
        }

        /// <summary>
        /// 查询事由类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        /// <summary>
        /// 删除考勤事由信息
        /// </summary>
        /// <param name="id">考勤事由ID</param>
        protected void DeletedMatters(int id,int formType)
        {
            MonthStatManager monthStatManager =new MonthStatManager();
            if (formType == 1)
            {
                MattersInfo mattersModel = mattersManager.GetModel(id);
                if (mattersModel != null)
                {
                    if (mattersModel.Deleted)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('此操作无效，单据不能重复删除。');", true);
                        return;
                    }
                    // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                    if (!monthStatManager.TryOperateData(UserID, mattersModel.EndTime))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                        return;
                    }
                    mattersModel.Deleted = true;
                    //if (mattersModel.MatterType == Status.MattersType_OffTune)
                    //{
                    //    mattersManager.DeleteOffTuneInfo(mattersModel);
                    //}
                    //else
                    //{
                        mattersManager.Delete(mattersModel);
                    //}
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")删除了一条考勤事由信息,编号(" + mattersModel.ID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                }
            }
            else if (formType == 2)
            {
                SingleOvertimeInfo overTimeModel = overTimeManager.GetModel(id);
                if (overTimeModel != null)
                {
                    if (overTimeModel.Deleted)
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('此操作无效，单据不能重复删除。');", true);
                        return;
                    }
                    // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                    if (!monthStatManager.TryOperateData(UserID, overTimeModel.EndTime))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                        return;
                    }
                    overTimeModel.Deleted = true;
                    overTimeManager.Update(overTimeModel);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")删除了一条OT单信息,编号(" + overTimeModel.ID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                }
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据删除成功。');window.location='MattersList.aspx';", true);
        }

        /// <summary>
        /// 撤销考勤事由信息
        /// </summary>
        /// <param name="id">考勤事由ID</param>
        protected void CancelMatters(int id, int formType)
        {
            MonthStatManager monthStatManager = new MonthStatManager();
            if (formType == 1)
            {
                MattersInfo mattersModel = mattersManager.GetModel(id);
                if (mattersModel != null)
                {
                    // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                    if (!monthStatManager.TryOperateData(UserID, mattersModel.EndTime))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                        return;
                    }
                    mattersModel.MatterState = Status.MattersState_Cancel;
                    mattersManager.Update(mattersModel);
                    approveManager.CancelApproveLog(id, Status.ApproveState_NoPassed);

                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")撤销了一条考勤事由信息,编号(" + mattersModel.ID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                }
            }
            else if (formType == 2)
            {
                SingleOvertimeInfo overTimeModel = overTimeManager.GetModel(id);
                if (overTimeModel != null)
                {
                    // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                    if (!monthStatManager.TryOperateData(UserID, overTimeModel.EndTime))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                        return;
                    }
                    overTimeModel.Approvestate = Status.OverTimeState_Cancel;
                    overTimeManager.Update(overTimeModel);
                    approveManager.CancelApproveLog(id, Status.ApproveState_NoPassed);
                    ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")撤销了一条OT单信息,编号(" + overTimeModel.ID + ")",
                                "考勤系统事由信息", ESP.Logging.LogLevel.Information);
                }
            }
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据撤销成功。');window.location='MattersList.aspx';", true);
        }

        /// <summary>
        /// 获得用户姓名
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="formID">事由ID</param>
        /// <param name="fromType">事由类型</param>
        /// <returns>返回事由查看链接</returns>
        protected string GetUserName(string userid, string formID, string fromType)
        {
            string name = new ESP.Compatible.Employee(int.Parse(userid)).Name;
            string url ="";

            if (fromType.Equals("1"))
            {
                url = "<a href='MattersView.aspx?matterid=" + formID + "&backurl=MattersList.aspx'>" + name + "</a>";
            }
            else if (fromType.Equals("2"))
            {
                url = "<a href='MattersView.aspx?singleid=" + formID + "&backurl=MattersList.aspx'>" + name + "</a>";
            }
            return url;
        }

        /// <summary>
        /// 获得事由类型名称
        /// </summary>
        /// <param name="formType">事由类别，1表示考勤基本事由，2表示OT事由</param>
        /// <param name="matterType">考勤基本事由类型</param>
        /// <param name="overtimeType">OT事由类型</param>
        /// <returns>返回考勤事由类型的名称</returns>
        protected string GetTypeName(string formType,int matterType,string overtimeType)
        {
            string typename = "";
            if (formType.Equals("1"))
            {
                if (matterType == Status.MattersType_Annual)
                {
                    typename = "年假";
                }
                else if (matterType == Status.MattersType_Bereavement)
                {
                    typename = "丧假";
                }
                else if (matterType == Status.MattersType_Leave)
                {
                    typename = "事假";
                }
                else if (matterType == Status.MattersType_Marriage)
                {
                    typename = "婚假";
                }
                else if (matterType == Status.MattersType_Maternity)
                {
                    typename = "产假";
                }
                else if (matterType == Status.MattersType_PeiChanJia)
                {
                    typename = "陪产假";
                }
                else if (matterType == Status.MattersType_OffTune)
                {
                    typename = "调休";
                }
                else if (matterType == Status.MattersType_Other)
                {
                    typename = "其他";
                }
                else if (matterType == Status.MattersType_Out)
                {
                    typename = "外出";
                }
                else if (matterType == Status.MattersType_Sick)
                {
                    typename = "病假";
                }
                else if (matterType == Status.MattersType_Travel)
                {
                    typename = "出差";
                }
                else if (matterType == Status.MattersType_PrenatalCheck)
                {
                    typename = "产检";
                }
                //else if (matterType == Status.MattersType_Incentive)
                //{
                //    typename = "福利假";
                //}
            }
            else if (formType.Equals("2"))
            {
                typename = "OT";
            }
            return typename;
        }

        /// <summary>
        /// 获得状态名称
        /// </summary>
        /// <param name="formType">事由类别，1表示考勤基本事由，2表示OT事由</param>
        /// <param name="matterState">考勤基本事由类型状态</param>
        /// <param name="approveState">OT事由状态</param>
        /// <returns>返回考勤事由状态名称</returns>
        protected string GetStateName(string formType, int matterState, int approveState)
        {
            string statename = "";
            if (formType.Equals("1"))
            {
                if (matterState == Status.MattersState_NoSubmit || matterState == Status.MattersState_Cancel)
                {
                    statename = "未提交";
                }
                else if (matterState == Status.MattersState_Passed)
                {
                    statename = "审批通过";
                }
                else if (matterState == Status.MattersState_WaitDirector)
                {
                    statename = "等待总监审批";
                }
                else if (matterState == Status.MattersState_WaitHR)
                {
                    statename = "等待人力审批";
                }
                else if (matterState == Status.MattersState_Overrule)
                {
                    statename = "审批驳回";
                }
            }
            else if (formType.Equals("2"))
            {
                if (approveState == Status.OverTimeState_NotSubmit || approveState == Status.OverTimeState_Cancel)
                {
                    statename = "未提交";
                }
                else if (approveState == Status.OverTimeState_WaitDirector)
                {
                    statename = "等待总监审批";
                }
                else if (approveState == Status.OverTimeState_WaitHR)
                {
                    statename = "等待人力审批";
                }
                else if (approveState == Status.OverTimeState_Passed)
                {
                    statename = "审批通过";
                }
                else if (approveState == Status.OverTimeState_Overrule)
                {
                    statename = "审批驳回";
                }
            }
            return statename;
        }

        /// <summary>
        /// 获得事由编辑链接
        /// </summary>
        /// <param name="formType">事由类别，1表示考勤基本事由，2表示OT事由</param>
        /// <param name="formID">事由ID</param>
        /// <param name="matterType">考勤基本事由类型</param>
        /// <param name="matterState">考勤基本事由状态</param>
        /// <param name="approveState">OT事由状态</param>
        /// <returns>考勤事由类型编辑链接</returns>
        protected string GetEditUrl(string formType, int formID, int matterType, int matterState, int approveState)
        {
            string tab = "";
            string url = "";

            if (formType.Equals("1"))
            {
                // 如果是未提交和驳回状态都可以修改
                if (matterState == Status.MattersState_NoSubmit || matterState == Status.MattersState_Overrule)
                {
                    if (matterType == Status.MattersType_Sick || matterType == Status.MattersType_Leave ||
                        matterType == Status.MattersType_Annual || matterType == Status.MattersType_Marriage ||
                        matterType == Status.MattersType_Maternity || matterType == Status.MattersType_Bereavement || 
                        matterType == Status.MattersType_PrenatalCheck || matterType == Status.MattersType_Incentive ||
                        matterType == Status.MattersType_PeiChanJia)
                    {
                        tab = "0";
                    }
                    else if (matterType == Status.MattersType_Out)
                    {
                        tab = "1";
                    }
                    else if (matterType == Status.MattersType_Travel)
                    {
                        tab = "2";
                    }
                    else if (matterType == Status.MattersType_OffTune)
                    {
                        tab = "3";
                    }
                    else if (matterType == Status.MattersType_OTLate)
                    {
                        tab = "4";
                    }

                    url = "<a href='MattersEdit.aspx?matterid=" + formID + "&backurl=MattersList.aspx&tabtype=" + tab + "'><img src='../images/edit.gif' /></a>";
                }
            }
            else if (formType.Equals("2"))
            {
                tab = "1";
                // 如果OT单的状态为未提交或者驳回状态
                if (approveState == Status.OverTimeState_NotSubmit || approveState == Status.OverTimeState_Overrule)
                {
                    url = "<a href='MattersEdit.aspx?matterid=" + formID + "&backurl=MattersList.aspx&tabtype=" + tab + "'><img src='../images/edit.gif' /></a>";
                }
            }
            return url;
        }

        /// <summary>
        /// 获得事由删除链接
        /// </summary>
        /// <param name="formType">事由类别，1表示考勤基本事由，2表示OT事由</param>
        /// <param name="formID">事由ID</param>
        /// <param name="matterState">考勤基本事由状态</param>
        /// <param name="approveState">OT事由状态</param>
        /// <returns></returns>
        protected string GetDeleteUrl(string formType, int formID, int matterState, int approveState)
        {
            string url = "";

            if (formType.Equals("1"))
            {
                url = "<a href=\"#\" onclick=\"return DeleteInfo(this," + formID + "," + formType + ");\"><img src=\"../images/disable.gif\" /></a>";
            }
            else if (formType.Equals("2"))
            {
                url = "<a href=\"#\" onclick=\"return DeleteInfo(this," + formID + "," + formType + ");\"><img src=\"../images/disable.gif\" /></a>";
            }
            return url;
        }

        /// <summary>
        /// 获得事由撤销链接
        /// </summary>
        /// <param name="formType">事由类别，1表示考勤基本事由，2表示OT事由</param>
        /// <param name="formID">事由ID</param>
        /// <param name="matterState">考勤基本事由状态</param>
        /// <param name="approveState">OT事由状态</param>
        /// <returns></returns>
        protected string GetCancelUrl(string formType, int formID, int matterState, int approveState)
        {
            string url = "";

            if (formType.Equals("1"))
            {

                if (matterState == Status.MattersState_WaitDirector)
                {
                    url = "<a href=\"#\" onclick=\"return CalInfo(this," + formID + "," + formType + ");\"><img src=\"../images/Icon_Cancel.gif\" /></a>";
                }
            }
            else if (formType.Equals("2"))
            {

                if (approveState == Status.OverTimeState_WaitDirector)
                {
                    url = "<a href=\"#\" onclick=\"return CalInfo(this," + formID + "," + formType + ");\"><img src=\"../images/Icon_Cancel.gif\" /></a>";
                }
            }
            return url;
        }
    }
}