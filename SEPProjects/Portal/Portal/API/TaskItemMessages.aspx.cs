using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;

namespace Portal.WebSite.API
{
    public partial class TashItemMessages : ESP.Web.UI.PageBase
    {
        protected override void OnPreInit(EventArgs e)
        {
            this.SkipLogging = true;
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ApplicantID"] != null)
            {
                int applicantID = Convert.ToInt32(Request["ApplicantID"]);
                string userInfostr = getUserInfo(applicantID);
                Response.Write(ESP.Utilities.JavascriptUtility.QuoteJScriptString(userInfostr, false, true));
            }
            else if (Request["assetuserid"] != null)
            {
                int assetuserid = Convert.ToInt32(Request["ApplicantID"]);
                string assetInfo = getAssetInfo(assetuserid);
                Response.Write(ESP.Utilities.JavascriptUtility.QuoteJScriptString(assetInfo, false, true));
            }
            else if (Request["keyword"] != null)
            {
                string keyword = Request["keyword"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.Trim();
                    IList<ESP.Framework.Entity.EmployeeInfo> list = new List<ESP.Framework.Entity.EmployeeInfo>();

                    list = ESP.Framework.BusinessLogic.EmployeeManager.Search(keyword);

                    // 格式化一下要显示的用户信息
                    if (list != null && list.Count > 0)
                    {
                        string empInfoStr = "";
                        foreach (ESP.Framework.Entity.EmployeeInfo emp in list)
                        {
                            empInfoStr += "<tr><td><a href=\"#\" style=\"font-size:12px;color:Black\" onclick=\"getUserInfo("
                                + emp.UserID.ToString() + ");\">" + emp.FullNameCN + "</a><br>" + emp.FullNameEN + "</td>";
                        }
                        Response.Write(ESP.Utilities.JavascriptUtility.QuoteJScriptString(empInfoStr, false, true));
                    }
                }
            }
            else if (Request["getservertime"] != null)
            {
                Response.Write(ESP.Utilities.JavascriptUtility.QuoteJScriptString(
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), false, true));
            }
            else
            {
                // 获得当前登陆用户的待处理事件集合
                try
                {
                    IDictionary<string, IList<TaskItemInfo>> list = ESP.Framework.BusinessLogic.TaskItemManager.GetTaskItems(Portal.Common.Global.WORK_ITEM_TASK_CACHE_KEY, this.UserID);
                    if (list == null)
                        list = new Dictionary<string, IList<TaskItemInfo>>();

                    IDictionary<string, IList<TaskItemInfo>> hclist = getHeadCount();

                    IDictionary<string, IList<TaskItemInfo>> transferlist = getTransfer();

                    IDictionary<string, IList<TaskItemInfo>> ticketlist = getTicketWaitingConfirm();

                    IDictionary<string, IList<TaskItemInfo>> emaillist = getEmailClosingConfirm();

                    if (list != null && list.ContainsKey("HeadCount"))
                    {
                        list.Remove("HeadCount");
                    }
                    if (hclist != null && hclist.Count > 0)
                    {
                        foreach (var hc in hclist)
                        {
                            list.Add(hc);
                        }
                    }

                    if (list != null && list.ContainsKey("转组"))
                    {
                        list.Remove("转组");
                    }
                    if (transferlist != null && transferlist.Count > 0)
                    {
                        foreach (var trans in transferlist)
                        {
                            list.Add(trans);
                        }
                    }

                    if (list != null && list.ContainsKey("机票使用确认"))
                    {
                        list.Remove("机票使用确认");
                    }
                    if (ticketlist != null && ticketlist.Count > 0)
                    {
                        foreach (var trans in ticketlist)
                        {
                            list.Add(trans);
                        }
                    }

                    //IT email
                    if (list != null && list.ContainsKey("离职邮箱关闭"))
                    {
                        list.Remove("离职邮箱关闭");
                    }
                    if (emaillist != null && emaillist.Count > 0)
                    {
                        foreach (var trans in emaillist)
                        {
                            list.Add(trans);
                        }
                    }

                    if (list != null && list.Count > 0)
                    {
                        Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(list));
                    }
                    else
                    {
                        Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(new List<string>()));
                    }
                }
                catch (System.Exception ex)
                {
                    Response.Write(ESP.Utilities.JavascriptUtility.QuoteJScriptString(ex.Message + "\n" + ex.StackTrace, false, true));
                }
            }
            Response.End();
        }


        private Dictionary<string, IList<TaskItemInfo>> getHeadCount()
        {
            Dictionary<string, IList<TaskItemInfo>> dici = new Dictionary<string, IList<TaskItemInfo>>();
            IList<TaskItemInfo> list = new List<TaskItemInfo>();

            string userids = UserID.ToString() + ",";
            var delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
            if (delegateList != null && delegateList.Count > 0)
            {
                foreach (var de in delegateList)
                {
                    userids += de.UserID.ToString() + ",";
                }
            }

            userids = userids.TrimEnd(',');

            var deptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" (HeadCountAuditorId in(" + userids + "))");
            string depts = string.Empty;
            foreach (var dep in deptlist)
            {
                depts += dep.DepId.ToString() + ",";
            }

            depts = depts.TrimEnd(',');


            var vpdeptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" (HeadCountDirectorId in(" + userids + "))");
            string vpdepts = string.Empty;
            foreach (var dep in vpdeptlist)
            {
                vpdepts += dep.DepId.ToString() + ",";
            }

            vpdepts = vpdepts.TrimEnd(',');


            var rcdeptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" (HCFinalAuditor in(" + userids + "))");
            string rcdepts = string.Empty;
            foreach (var dep in rcdeptlist)
            {
                rcdepts += dep.DepId.ToString() + ",";
            }

            rcdepts = rcdepts.TrimEnd(',');


            string strwhere = string.Empty;

            if (!string.IsNullOrEmpty(vpdepts) || !string.IsNullOrEmpty(depts))
            {
                if (!string.IsNullOrEmpty(depts))
                {
                    if (!string.IsNullOrEmpty(vpdepts))
                        strwhere = string.Format(" ((status={0} and groupid in({1})) or (status={2} and groupid in({3})) or (status={4} and interviewVPId in({5})) )", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit, vpdepts, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitPreVPAudit, depts, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, userids);
                    else
                        strwhere = string.Format(" ((status={0} and groupid in({1})) or (status={2} and interviewVPId in({3})))", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitPreVPAudit, depts, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, userids);
                }
                else
                {
                    strwhere = string.Format("((status={0} and interviewVPId={1}) or (status={2} and groupid in({3})))", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterView, UserID, (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit, vpdepts);
                }
                strwhere += string.Format(" or (status={0} and RCUserId={1})", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit, UserID);
            }

            else if (!string.IsNullOrEmpty(rcdepts))
                strwhere += string.Format(" (status={0} and RCUserId={1})", (int)ESP.HumanResource.Common.Status.HeadAccountStatus.WaitCFOAudit, UserID);
            else
                return null;

                var headaccountList = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetList(" and  (" + strwhere + ")");

                string HRHeader = ESP.Configuration.ConfigurationManager.Items["HRHeader"];

                foreach (var hc in headaccountList)
                {
                    var dept = ESP.Framework.BusinessLogic.DepartmentManager.Get(hc.GroupId);
                    TaskItemInfo model = new TaskItemInfo();
                    model.ApplicantID = hc.CreatorId;
                    model.ApplicantName = hc.Creator;
                    model.AppliedTime = hc.CreateDate == null ? new DateTime(1900, 1, 1) : hc.CreateDate.Value;

                    model.Description = dept.DepartmentName + "-" + hc.Position;
                    model.FormID = hc.Id;
                    string strId = hc.Id.ToString();

                    while (strId.Length < 5)
                    {
                        strId = "0" + strId;
                    }
                    model.FormNumber = "HC" + strId;

                    model.FormType = "HeadCount";
                    model.ApproverID = UserID;
                    model.ApproverName = CurrentUserName;

                    string url = string.Format("/HR/Join/HeadAccountAudit.aspx?haid={0}", model.FormID);

                    model.Url = HRHeader + "/Default.aspx?contentUrl=" +
                        HttpUtility.UrlEncode(url);

                    list.Add(model);

                }
                if (list.Count > 0)
                {
                    dici.Add("HeadCount", list);

                    return dici;
                }
                else
                    return null;
            
               
        }


        private Dictionary<string, IList<TaskItemInfo>> getTransfer()
        {
            Dictionary<string, IList<TaskItemInfo>> dici = new Dictionary<string, IList<TaskItemInfo>>();
            IList<TaskItemInfo> list = new List<TaskItemInfo>();

            var transList = ESP.HumanResource.BusinessLogic.TransferManager.GetWaitAuditList(UserID, "", null);

            string HRHeader = ESP.Configuration.ConfigurationManager.Items["HRHeader"];

            foreach (var trans in transList)
            {

                TaskItemInfo model = new TaskItemInfo();
                model.ApplicantID = trans.CreaterId;
                model.ApplicantName = trans.Creater;
                model.AppliedTime = trans.CreateDate;

                if (trans.TransName != string.Empty)
                {
                    model.Description = trans.TransName + "[" + trans.TransCode + "]:" + trans.NewGroupName + "-" + trans.NewPositionName;
                }
                else
                    model.Description = trans.NewGroupName + "-" + trans.NewPositionName;
                model.FormID = trans.Id;
                model.FormNumber = "";
                model.FormType = "转组";
                model.ApproverID = UserID;
                model.ApproverName = CurrentUserName;

                string url = string.Empty;
                if (trans.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.HRCommit)
                {
                    url = string.Format("/HR/Transfer/TransferHrEdit.aspx?id={0}", model.FormID);
                }
                else
                {
                    url = string.Format("/HR/Transfer/TransferAudit.aspx?id={0}", model.FormID);
                }
                model.Url = HRHeader + "/Default.aspx?contentUrl=" +
                    HttpUtility.UrlEncode(url);

                list.Add(model);

            }

            if (list.Count > 0)
            {
                dici.Add("转组", list);

                return dici;
            }
            else
                return null;

        }

        private Dictionary<string, IList<TaskItemInfo>> getTicketWaitingConfirm()
        {
            Dictionary<string, IList<TaskItemInfo>> dici = new Dictionary<string, IList<TaskItemInfo>>();
            IList<TaskItemInfo> list = new List<TaskItemInfo>();

            string strwhere = " and creater=" + UserID;
            strwhere += " and TicketIsConfirm=0 ";
            var detailConfirmlist = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetIicketConfirm(strwhere);


            string fheader = ESP.Configuration.ConfigurationManager.Items["FinancialHeader"];

            foreach (var ticket in detailConfirmlist)
            {

                TaskItemInfo model = new TaskItemInfo();
                model.ApplicantID = ticket.Creater.Value;
                model.ApplicantName = ticket.CreaterName;
                model.AppliedTime = ticket.ExpenseDate.Value;

                model.Description = ticket.Boarder + "[" + ticket.BoarderIDCard + "]" + ticket.GoAirNo + ":" + ticket.TicketSource + "--" + ticket.TicketDestination;

                model.FormID = ticket.ReturnID.Value;
                model.FormNumber = ticket.ExpenseDesc;
                model.FormType = "机票使用确认";
                model.ApproverID = UserID;
                model.ApproverName = CurrentUserName;

                string url = string.Empty;
                url = "/Ticket/TicketConfirm.aspx";

                model.Url = fheader + "/Default.aspx?contentUrl=" + HttpUtility.UrlEncode(url);

                list.Add(model);

            }

            if (list.Count > 0)
            {
                dici.Add("机票使用确认", list);

                return dici;
            }
            else
                return null;

        }


        private Dictionary<string, IList<TaskItemInfo>> getEmailClosingConfirm()
        {
            string ITUsers = System.Configuration.ConfigurationManager.AppSettings["EmailClosingOperator"];

            if (ITUsers.IndexOf("," + UserID.ToString() + ",") < 0)
                return null;
            else
            {
                Dictionary<string, IList<TaskItemInfo>> dici = new Dictionary<string, IList<TaskItemInfo>>();
                IList<TaskItemInfo> list = new List<TaskItemInfo>();

                string strwhere = " status =0 ";

                var emailList = ESP.HumanResource.BusinessLogic.EmailClosingManager.GetList(strwhere);


                string hrheader = ESP.Configuration.ConfigurationManager.Items["HRHeader"];

                foreach (var email in emailList)
                {

                    TaskItemInfo model = new TaskItemInfo();
                    model.ApplicantID = email.UserId;
                    model.ApplicantName = email.NameCN;
                    model.AppliedTime = email.CloseDate;

                    model.Description = email.DeptName + " - " + email.Email;

                    model.FormID = email.UserId;
                    model.FormNumber = "";
                    model.FormType = "离职邮箱关闭";
                    model.ApproverID = UserID;
                    model.ApproverName = CurrentUserName;

                    string url = string.Empty;
                    url = string.Format("/IT/EmailClosing.aspx?userId={0}", email.UserId);

                    model.Url = hrheader + "/Default.aspx?contentUrl=" + HttpUtility.UrlEncode(url);

                    list.Add(model);

                }

                if (list.Count > 0)
                {
                    dici.Add("离职邮箱关闭", list);

                    return dici;
                }
                else
                    return null;
            }
        }



        private string getUserInfo(int userid)
        {
            string script = string.Empty;
            UserInfo user = ESP.Framework.BusinessLogic.UserManager.Get(userid);
            EmployeeBaseInfo empBase = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userid);
            IList<EmployeePositionInfo> ep = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid);
            ESP.Finance.Entity.DepartmentViewInfo dept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(ep[0].DepartmentID);
            int currentuserdeptid = CurrentUser.GetDepartmentIDs()[0];

            string strdept = ep[0].DepartmentPositionName.ToString();
            int b = viewpermchar(userid);
            bool ret = ESP.HumanResource.BusinessLogic.DimissionFormManager.ExistsUser(user.UserID);
            string fullNameCN = user.LastNameCN + user.FirstNameCN + "&nbsp;&nbsp;[" + empBase.Code + "]";
            if (ret)
            {
                if (int.Parse(CurrentUser.SysID) == int.Parse(System.Configuration.ConfigurationManager.AppSettings["DavidZhangID"]) || System.Configuration.ConfigurationManager.AppSettings["AdministrativeIDs"].IndexOf("," + currentuserdeptid.ToString() + ",") >= 0)
                    fullNameCN += "  <font color='red'>(已提交离职)</font>";
            }
            if (!string.IsNullOrEmpty(user.Comment))
            {
                fullNameCN += "  <font color='red'>(" + user.Comment + ")</font>";
            }
            script += "<table><tr><td width='75%'>";
            script += "<ul style='text-align:left'><li>员工姓名：" + fullNameCN + "</li>";
            script += "<li>英文名称：" + user.Username + "</li>";
            script += "<li>所属部门：" + dept.level1 + "-" + dept.level2 + "-" + dept.level3 + "</li>";
            script += "<li>工作地点：" + empBase.WorkCity + "</li>";
            if (b == 1 || b == 2)
                script += "<li>移动电话：" + empBase.MobilePhone + "</li>";
            script += "<li>电子邮箱：<a href='mailto:" + empBase.InternalEmail + "'>" + empBase.InternalEmail + "</a></li>";
            if (b == 1)
                script += "<li>职位：" + strdept + "</li></ul>";
            script += "</td><td valign='top' width='25%' height='120'>";
            script += "<div style='float:right;margin:15px 30px 0 0;' width=\"120\" height=\"120\">";
            script += "<img hspace=\"0\" vspace=\"0\"  width=\"120\" height=\"120\" src='" + Portal.Common.Global.USER_ICON_FOLDER + empBase.Photo + "'/>";
            script += "</div>";
            script += "</td></tr></table>";
            return script;
        }

        private string getAssetInfo(int userid)
        {
            string script = string.Empty;
            string terms = " usercode=@usercode and status=@status";
            List<SqlParameter> parms = new List<SqlParameter>();
            int icount = 0;
            parms.Add(new SqlParameter("@usercode", CurrentUser.ID));
            parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

            IList<ESP.Finance.Entity.ITAssetReceivingInfo> list = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(terms, parms);

            if (list != null && list.Count > 0)
            {
                script += "<table id=\"assetTable\"><thead><tr style=\"background-color:gray; text-align:center;\"><th style=\"width:50px;\">序号</th><th style=\"width:200px;\">资产名称</th><th style=\"width:200px;\">资产型号</th><th style=\"width:100px;\">领用日期</th><thead><tbody>";
                icount = 1;
                foreach (ESP.Finance.Entity.ITAssetReceivingInfo rec in list)
                {
                    ESP.Finance.Entity.ITAssetsInfo assetModel = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(rec.AssetId);
                    if (assetModel != null)
                    {
                        //  script += "<li>" + assetModel.AssetName + "&nbsp;&nbsp;" + assetModel.Brand + "&nbsp;&nbsp;"+rec.ReceiveDate.ToString("yyyy-MM-dd")+ "</li>";
                        script += "<tr><td style=\"text-align:center;\">" + icount + "</td><td>" + assetModel.AssetName + "</td><td>" + assetModel.Brand + "</td><td style=\"text-align:center;\">" + rec.ReceiveDate.ToString("yyyy-MM-dd") + "</td></tr>";
                    }

                    icount++;

                    if (icount > 15)
                        break;
                }

                script += "</tbody></table>";
            }


            return script;
        }


        private string getDelegateUsers(int UserID)
        {
            string ret = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateUsers = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateUsers)
            {
                ret += model.UserID.ToString() + ",";
            }
            return ret.TrimEnd(',');
        }

        /// <summary>
        /// 判断登录用户是否有权限查看当前所查看用户的基本信息
        /// </summary>
        /// <param name="userid">被查看人的用户编号</param>
        /// <returns>返回值
        /// 1.超级用户、团队LEADER和各BU HR可以查看所有用户信息，其中团队LEADER和BU HR所能查看的是自己做负责的团队用户信息；
        /// 2.AAD级别及以上级、集团行政、集团人力和分公司人力用户可以查看用户手机号；
        /// 3.普通用户可以查看用户分机和邮箱。</returns>
        private int viewpermchar(int userid)
        {
            #region 0.查询自己信息
            if (userid == UserID)
            {
                return 1;
            }
            #endregion
            #region 1.级别一：CEO（张总）、财务总监（Eddy）、人力资源总监（Rosa）、人力资源C&B（任洁）
            string humanMapAdmin = "";
            DataCodeManager dataCodeManager = new DataCodeManager();
            List<DataCodeInfo> humanMapAdminList = dataCodeManager.GetDataCodeByType("HumanMapAdmin");
            if (humanMapAdminList != null && humanMapAdminList.Count > 0)
            {
                DataCodeInfo dataCodeModel = humanMapAdminList[0];
                if (dataCodeModel != null)
                {
                    humanMapAdmin = dataCodeModel.Code;
                }
            }

            if (humanMapAdmin.IndexOf(UserID.ToString()) != -1)
            {
                return 1;
            }
            #endregion

            #region 2.级别二：TeamLeader和BU HR
            // 获得被查看用户的部门信息
            string adminIds = System.Configuration.ConfigurationManager.AppSettings["AdministrativeIDs"];

            IList<EmployeePositionInfo> empPositonlist = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid);
            int currentuserdeptid = CurrentUser.GetDepartmentIDs()[0];
            if (adminIds.IndexOf("," + currentuserdeptid.ToString() + ",") >= 0)
            {
                return 1;
            }


            string teamLeaderUserIds = "";
            string buHRUserIds = "";
            if (empPositonlist != null && empPositonlist.Count > 0)
            {

                foreach (EmployeePositionInfo empPosInfo in empPositonlist)
                {
                    ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(empPosInfo.DepartmentID);
                    if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                    {
                        ESP.Framework.Entity.OperationAuditManageInfo operationAuditManageInfo = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(departmentInfo.DepartmentID);

                        teamLeaderUserIds += operationAuditManageInfo.DirectorId + "," + operationAuditManageInfo.ManagerId + ",";
                        buHRUserIds += operationAuditManageInfo.HRId + ",";
                    }
                }
            }

            if (teamLeaderUserIds.IndexOf(UserID.ToString()) != -1 || buHRUserIds.IndexOf(UserID.ToString()) != -1)
            {
                return 1;
            }
            #endregion

            #region 3.级别三：AAD级别及以上级、集团行政、集团人力和分公司人力用户
            List<DataCodeInfo> humanMapGroupHR = dataCodeManager.GetDataCodeByType("HumanMapGroupHR"); // 集团人力
            List<DataCodeInfo> humanMapGroupAdmin = dataCodeManager.GetDataCodeByType("HumanMapGroupAdmin"); // 集团行政
            List<DataCodeInfo> humanMapBranchHRAdmin = dataCodeManager.GetDataCodeByType("HumanMapBranchHRAdmin"); // 分公司人事行政
            string humanMapHRAdmin = "";
            if (humanMapGroupHR != null && humanMapGroupHR.Count > 0)
            {
                DataCodeInfo dataCodeModel = humanMapGroupHR[0];
                if (dataCodeModel != null)
                {
                    humanMapHRAdmin += dataCodeModel.Code + ",";
                }
            }
            if (humanMapGroupAdmin != null && humanMapGroupAdmin.Count > 0)
            {
                DataCodeInfo dataCodeModel = humanMapGroupAdmin[0];
                if (dataCodeModel != null)
                {
                    humanMapHRAdmin += dataCodeModel.Code + ",";
                }
            }
            if (humanMapBranchHRAdmin != null && humanMapBranchHRAdmin.Count > 0)
            {
                DataCodeInfo dataCodeModel = humanMapBranchHRAdmin[0];
                if (dataCodeModel != null)
                {
                    humanMapHRAdmin += dataCodeModel.Code + ",";
                }
            }


            if (humanMapHRAdmin.IndexOf(UserID.ToString()) != -1)
            {
                return 2;
            }
            //职能部门


            // AAD职位级别数

            //List<DataCodeInfo> AADPositongLevel = dataCodeManager.GetDataCodeByType("AADPositongLevel"); // AAD职务级别
            int majordomoPosLevel = 5;
            ////Convert.ToInt32(ESP.Framework.BusinessLogic.SettingManager.GetSetting(0, "MajordomoPositongLevel", false).SettingValue);
            //if (AADPositongLevel != null && AADPositongLevel.Count > 0)
            //{
            //    DataCodeInfo dataCodeModel = AADPositongLevel[0];
            //    if (dataCodeModel != null)
            //    {
            //        majordomoPosLevel = Convert.ToInt32(dataCodeModel.Code);
            //    }
            //}

            IList<EmployeePositionInfo> curempPositonlist = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
            if (curempPositonlist != null && curempPositonlist.Count > 0)
            {
                foreach (EmployeePositionInfo empPosInfo in curempPositonlist)
                {
                    if (empPosInfo.PositionLevel <= majordomoPosLevel)
                        return 2;
                }
            }

            System.Data.DataTable dt = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetList("teamleaderid=" + UserID + " and UserId = " + userid).Tables[0];
            if (dt.Rows.Count > 0)
                return 2;

            #endregion

            return 3;
        }
    }
}