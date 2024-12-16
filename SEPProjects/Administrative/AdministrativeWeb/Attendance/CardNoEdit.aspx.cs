using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.Common;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using System.Net.Mail;
using ESP.HumanResource.BusinessLogic;
using System.Text;

namespace AdministrativeWeb.Attendance
{
    public partial class CardNoEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 通过用户ID获得用户信息
                if (Request["ApplicantID"] != null)
                {
                    int applicantID = Convert.ToInt32(Request["ApplicantID"]);
                    upUserInfo.Visible = true;
                    getUserInfo(applicantID);
                }
                GetDepartmentInfo();
            }
        }

        /// <summary>
        /// 绑定部门节点信息
        /// </summary>
        /// <param name="depinfo">部门信息</param>
        /// <param name="parentNode">节点树的根节点</param>
        /// <param name="Id"></param>
        /// <param name="n"></param>
        /// <param name="treenode"></param>
        /// <param name="isP"></param>
        /// <param name="isHerf"></param>
        void BindTree(IList<ESP.Framework.Entity.DepartmentInfo> depinfo,
            ComponentArt.Web.UI.TreeViewNode parentNode, int Id, ref int n, string treenode, bool isP, bool isHerf)
        {
            ComponentArt.Web.UI.TreeViewNode tn = null;
            // 部门下面的人员列表
            IList<ESP.Framework.Entity.EmployeeInfo> employeeList = null;
            int departmentId = 0;
            foreach (ESP.Framework.Entity.DepartmentInfo info in depinfo)
            {
                if (info.DepartmentName.IndexOf("作废") == -1)
                {
                    if (info.ParentID == Id)
                    {
                        if (info.DepartmentLevel == 3)
                        {
                            employeeList = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(info.DepartmentID);
                            departmentId = info.DepartmentID;
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.DepartmentName;
                            tn.Value = info.DepartmentID.ToString();
                            tn.ImageUrl = "/images/treeview/dept.gif";
                        }
                        else if (info.DepartmentLevel == 2)
                        {
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.DepartmentName;
                            tn.Value = info.DepartmentID.ToString();
                            tn.ImageUrl = "/images/treeview/dept.gif";
                        }
                        else
                        {
                            tn = new ComponentArt.Web.UI.TreeViewNode();
                            tn.Text = info.DepartmentName;
                            tn.Value = info.DepartmentID.ToString();
                            tn.ImageUrl = "/images/treeview/corp.gif";
                        }

                        n++;
                        tn.ToolTip = info.DepartmentName;
                        parentNode.Nodes.Add(tn);

                        if (info.DepartmentLevel == 3 && employeeList != null
                            && employeeList.Count > 0 && departmentId == info.DepartmentID)
                        {
                            foreach (ESP.Framework.Entity.EmployeeInfo employeeinfo in employeeList)
                            {
                                if (employeeinfo.Status != ESP.HumanResource.Common.Status.Dimission)
                                {
                                    ComponentArt.Web.UI.TreeViewNode tnemployee = new ComponentArt.Web.UI.TreeViewNode();
                                    tnemployee.Text = employeeinfo.FullNameCN;
                                    tnemployee.Value = employeeinfo.UserID.ToString();
                                    tnemployee.ImageUrl = "/images/treeview/user.gif";
                                    tnemployee.NavigateUrl = "CardNoEdit.aspx?ApplicantID=" + employeeinfo.UserID;

                                    n++;
                                    tnemployee.ToolTip = employeeinfo.FullNameCN;

                                    tn.Nodes.Add(tnemployee);
                                }
                            }
                            employeeList = null;
                            departmentId = 0;
                        }

                        BindTree(depinfo, tn, info.DepartmentID, ref n, treenode, isP, isHerf);
                    }
                }
            }
        }

        /// <summary>
        /// 获得当天用户所能产看的部门信息
        /// </summary>
        public void GetDepartmentInfo()
        {
            // 获得当前登录用户的人员部门信息
            IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);

            // 局部变量：保存部门信息包括1,2,3级部门信息
            IList<ESP.Framework.Entity.DepartmentInfo> depinfo = new List<ESP.Framework.Entity.DepartmentInfo>();
            if (list != null && list.Count > 0)
            {
                Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
                foreach (ESP.Framework.Entity.EmployeePositionInfo empPosInfo in list)
                {
                    List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                    ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(empPosInfo.DepartmentID);
                    if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                    {
                        // 添加当前用户上级部门信息
                        depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(empPosInfo.DepartmentID, depList);
                        foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
                        {
                            if (dm != null && dm.DepartmentLevel == 1)
                            {
                                // 判断这个部门是否已经在Dictionary中了
                                if (!dic.ContainsKey(dm.DepartmentID))
                                    dic.Add(dm.DepartmentID, dm);
                                // 保存地区ID
                                AreaID = dm.DepartmentID.ToString();
                                IList<ESP.Framework.Entity.DepartmentInfo> childrenList = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(dm.DepartmentID);
                                if (childrenList != null && childrenList.Count > 0)
                                {
                                    foreach (ESP.Framework.Entity.DepartmentInfo childdm in childrenList)
                                    {
                                        // 判断这个部门是否已经在Dictionary中了
                                        if (!dic.ContainsKey(childdm.DepartmentID))
                                            dic.Add(childdm.DepartmentID, childdm);
                                    }
                                }
                            }
                        }
                    }
                }

                // 将Dictionary中的值保存到一个List的集合中
                Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
                foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
                {
                    depinfo.Add(departmentInfo);
                }
            }

            // 创建一个人员节点数的根节点
            ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
            rootNode.Text = "星言云汇人力结构图";
            rootNode.Expanded = true;
            rootNode.ImageUrl = "/images/treeview/root.gif";
            userTreeView.Nodes.Add(rootNode);
            int n = 0;
            // 获得部门信息包括1,2,3级部门信息
            string str = "";
            bool isP = false;
            bool isHerf = false;
            // 开始绑定人员节点数
            depinfo = depinfo.OrderBy(c => c.DepartmentName).ToList<ESP.Framework.Entity.DepartmentInfo>();
            BindTree(depinfo, rootNode, 0, ref  n, str, isP, isHerf);
        }

        /// <summary>
        /// 通过用户编号获得用户信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        private void getUserInfo(int userid)
        {
            string script = string.Empty;
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
            
            // 获得当前用户的部门信息
            string deptstr = string.Empty;
            // 获得当前登录用户的人员部门信息
            IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(userid);
            if (list != null && list.Count > 0)
            {
                foreach (EmployeePositionInfo empinfo in list)
                {
                    List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                    ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(empinfo.DepartmentID);
                    if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                    {
                        hidDeptId.Value = departmentInfo.DepartmentID.ToString();
                        // 添加当前用户上级部门信息
                        depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(empinfo.DepartmentID, depList);
                        foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
                        {
                            deptstr += dm.DepartmentName + "-";
                        }
                        deptstr = deptstr.TrimEnd('-');
                        deptstr += ",";
                    }
                }
            }

            string empname = emp.Name == string.Empty ? "&nbsp;" : emp.Name;
            string empITCode = emp.ITCode == string.Empty ? "&nbsp;" : emp.ITCode;
            string empMobile = emp.Mobile == string.Empty ? "&nbsp;" : emp.Mobile;
            string empEmail = emp.EMail == string.Empty ? "&nbsp;" : emp.EMail;
            string empTel = emp.Telephone == string.Empty ? "&nbsp;" : emp.Telephone.TrimEnd('-');
            string strdept = deptstr.TrimEnd(',') == string.Empty ? "&nbsp;" : deptstr.TrimEnd(',');

            // 获得用户的当前被启用的门卡考勤基本信息
            DateTime d1 = DateTime.Now;
            UserAttBasicInfoManager userCarInfoManager = new UserAttBasicInfoManager();
            UserAttBasicInfo userCardInfo = userCarInfoManager.GetEnableCardByUserid(userid);
            // ESP.Logging.Logger.Add("获取门卡信息花费时间：" + (DateTime.Now - d1).Ticks, "行政系统", ESP.Logging.LogLevel.Information);
            // 如果门卡信息不为空，则设置停用门卡和更换门卡两个按钮都可以使用
            if (userCardInfo != null && !string.IsNullOrEmpty(userCardInfo.CardNo))
            {
                txtCardno.Text = userCardInfo.CardNo;
                hidUserCardID.Value = userCardInfo.ID.ToString();
                btnEnable.Visible = false;
                btnUnEnable.Visible = true;
                btnExchange.Visible = true;
                btnBlankOut.Visible = true;
            }
            // 如果门卡信息为空，启用按钮可以使用
            else
            {
                btnEnable.Visible = true;
                btnUnEnable.Visible = false;
                btnExchange.Visible = false;
                btnBlankOut.Visible = false;
            }

            // 获得用户使用的历史门卡信息
            List<UserAttBasicInfo> historyCardList = userCarInfoManager.GetUserHistoryCard(userid);
            if (historyCardList != null && historyCardList.Count > 0)
            {
                string strHistoryCard = "";
                int i = 1;
                foreach (UserAttBasicInfo userAttBasicInfo in historyCardList)
                {
                    string enableTime = userAttBasicInfo.CardEnableTime == null ? "2009-07-01" : userAttBasicInfo.CardEnableTime.Value.ToString("yyyy-MM-dd");
                    string unEnableTime = userAttBasicInfo.CardUnEnableTime == null ? "现在" : userAttBasicInfo.CardUnEnableTime.Value.ToString("yyyy-MM-dd");
                    strHistoryCard += "(" + i + ")、" + enableTime + " 至 " + unEnableTime + "，使用门卡：" + userAttBasicInfo.CardNo + "，备注：" + userAttBasicInfo.Remark + "<br />";
                    i++;
                }
                labHistoryCard.Text = strHistoryCard;
            }

            labUserName.Text = empname;            // 员工姓名
            labUserCode.Text = emp.ID;             // 用户编号
            labUserTel.Text = empTel;              // 公司电话
            labUserDept.Text = strdept;            // 所属部门
            hidUserid.Value = userid.ToString();   // 用户编号
        }

        /// <summary>
        /// 地区ID
        /// </summary>
        public string AreaID
        {
            get
            {
                return this.ViewState["AreaID"] as string;
            }
            set
            {
                this.ViewState["AreaID"] = value;
            }
        }

        /// <summary>
        /// 返回查询列表页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CardNoList.aspx");
        }

        /// <summary>
        /// 启动门卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnable_Click(object sender, ImageClickEventArgs e)
        {
            // 用户编号
            int userid = 0;
            // 门卡号
            string cardno = "";
            if (!string.IsNullOrEmpty(hidUserid.Value))
            {
                userid = int.Parse(hidUserid.Value);
            }
            if (!string.IsNullOrEmpty(txtCardno.Text))
            {
                cardno = txtCardno.Text;
            }

            CardStoreManager cardStoreManager = new CardStoreManager();
            CardStoreInfo cardStoreModel = cardStoreManager.GetModelByCardNo(cardno);
            //if (cardStoreModel != null && cardStoreModel.State == (int)CardStoreState.NotUsed)
            //{
                EmployeeInfo employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(userid);
                if (employeeModel != null)
                {
                    UserAttBasicInfoManager userAttBasicInfoManager = new UserAttBasicInfoManager();
                    UserAttBasicInfo userAttBasicModel = userAttBasicInfoManager.GetLastUserAttBasicInfo(employeeModel.UserID);
                    // 要更换的门卡信息
                    UserAttBasicInfo model = new UserAttBasicInfo();
                    model.AnnualLeaveBase = (int)AnnualLeaveBase.Normal;
                    model.AttendanceType = (int)AttendanceType.Normal;
                    model.CardEnableTime = DateTime.Now;
                    model.CardState = (int)CardUseState.Enable;
                    model.CreateTime = DateTime.Now;
                    model.Deleted = false;
                    model.EmployeeName = employeeModel.FullNameCN;
                    model.GoWorkTime = Constants.DEFAULTGOWORKTIME;
                    model.OffWorkTime = Constants.DEFAULTOFFWORKTIME;
                    model.OperateorDept = 0;
                    model.OperateorID = UserID;
                    model.Sort = 0;
                    model.UpdateTime = DateTime.Now;
                    model.UserCode = employeeModel.Code;
                    model.Userid = userid;
                    model.UserName = employeeModel.Username;
                    model.AreaID = int.Parse(AreaID);
                    model.CardNo = txtCardno.Text;

                    if (userAttBasicModel != null)
                    {
                        model.AnnualLeaveBase = userAttBasicModel.AnnualLeaveBase;
                        model.AttendanceType = userAttBasicModel.AttendanceType;
                        model.GoWorkTime = userAttBasicModel.GoWorkTime;
                        model.OffWorkTime = userAttBasicModel.OffWorkTime;
                        model.WorkTimeBeginDate = userAttBasicModel.WorkTimeBeginDate;
                    }
                    
                    userAttBasicInfoManager.Add(model, cardStoreModel);
                    ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnEnable", "alert('" + employeeModel.FullNameCN + "的门卡启用成功！');", true);
                    getUserInfo(userid);

                    List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(model.AreaID, ESP.HumanResource.Common.Status.EntrySendMail);
                    List<MailAddress> mailList = new List<MailAddress>();
                    StringBuilder strBuilder = new StringBuilder();
                    foreach (ESP.HumanResource.Entity.UsersInfo info in list)
                    {
                        if (!string.IsNullOrEmpty(info.Email))
                        {
                            mailList.Add(new MailAddress(info.Email));
                            strBuilder.Append(info.Email);
                        }
                    }

                    ESP.Logging.Logger.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + UserInfo.FullNameCN + "启用门卡（" + model.CardNo + "）,收件人（" + strBuilder.ToString() + "）",
                        "行政系统", ESP.Logging.LogLevel.Information);

                    string url = "http://" + Request.Url.Authority + "/MailTemplate/UserCardMail.aspx?userid=" + userid + "&cardno=" + txtCardno.Text + "&flag=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                    ESP.Mail.MailManager.Send("启用门卡信息", body, true, null, mailList.ToArray(), null, null, null);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnEnable", "alert('" + employeeModel.FullNameCN + "的门卡启用失败！');", true);
                }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnUnEnable", "alert('您所启用的门卡不存在于门卡信息库中，请确认！');", true);
            //}
        }

        /// <summary>
        /// 停用门卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnEnable_Click(object sender, ImageClickEventArgs e)
        {
            // 用户编号
            int userid = 0;
            // 门卡号
            string cardno = "";
            if (!string.IsNullOrEmpty(hidUserid.Value))
            {
                userid = int.Parse(hidUserid.Value);
            }
            if (!string.IsNullOrEmpty(txtCardno.Text))
            {
                cardno = txtCardno.Text;
            }

            CardStoreManager cardStoreManager = new CardStoreManager();
            CardStoreInfo cardStoreModel = cardStoreManager.GetModelByCardNo(cardno);
            UserAttBasicInfoManager userAttBasicInfoManager = new UserAttBasicInfoManager();
            UserAttBasicInfo userAttBasicModel = userAttBasicInfoManager.GetEnableCardByUserid(userid);
            // 获得该用户的正在使用门卡信息，如果门卡信息不为空，就修改该用户的门卡信息(状态，停用时间，最后更新时间，操作人)
            if (userAttBasicModel != null)
            {
                userAttBasicModel.CardState = (int)CardUseState.UnEnable;
                userAttBasicModel.CardUnEnableTime = DateTime.Now;
                userAttBasicModel.UpdateTime = DateTime.Now;
                userAttBasicModel.OperateorID = UserID;
                userAttBasicModel.Remark = txtDesc.Text;
                userAttBasicInfoManager.Update(userAttBasicModel, cardStoreModel, 1);
                ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnUnEnable", "alert('" + userAttBasicModel.EmployeeName + "的门卡停用成功！');", true);
                getUserInfo(userid);

                List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(userAttBasicModel.AreaID, ESP.HumanResource.Common.Status.EntrySendMail);
                List<MailAddress> mailList = new List<MailAddress>();
                StringBuilder strBuilder = new StringBuilder();
                foreach (ESP.HumanResource.Entity.UsersInfo info in list)
                {
                    if (!string.IsNullOrEmpty(info.Email))
                    {
                        mailList.Add(new MailAddress(info.Email));
                        strBuilder.Append(info.Email);
                    }
                }
                ESP.Logging.Logger.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + UserInfo.FullNameCN + "停用门卡（" + userAttBasicModel.CardNo + "）,收件人（" + strBuilder.ToString() + "）",
                        "行政系统", ESP.Logging.LogLevel.Information);

                string url = "http://" + Request.Url.Authority + "/MailTemplate/UserCardMail.aspx?userid=" + userid + "&cardno=" + cardno + "&flag=2";
                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                ESP.Mail.MailManager.Send("停用门卡信息", body, true, null, mailList.ToArray(), null, null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnUnEnable", "alert('" + userAttBasicModel.EmployeeName + "的门卡停用失败，请与系统管理员联系！');", true);
            }
        }

        /// <summary>
        /// 换门卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExchange_Click(object sender, ImageClickEventArgs e)
        {
            // 用户编号
            int userid = 0;
            // 门卡号
            string cardno = "";
            if (!string.IsNullOrEmpty(hidUserid.Value))
            {
                userid = int.Parse(hidUserid.Value);
            }
            if (!string.IsNullOrEmpty(txtCardno.Text))
            {
                cardno = txtCardno.Text;
            }

            CardStoreManager cardStoreManager = new CardStoreManager();
            CardStoreInfo cardStoreModel = cardStoreManager.GetModelByCardNo(cardno);
            //if (cardStoreModel != null && cardStoreModel.State == (int)CardStoreState.NotUsed)
            //{
                UserAttBasicInfoManager userAttBasicInfoManager = new UserAttBasicInfoManager();
                UserAttBasicInfo userAttBasicModel = userAttBasicInfoManager.GetEnableCardByUserid(userid);
                // 获得该用户的正在使用门卡信息
                if (userAttBasicModel != null)
                {
                    if (userAttBasicModel.CardNo == cardno)
                    {
                        ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnExchange", "alert('" + userAttBasicModel.EmployeeName + "被更换的门卡号和正在使用的门卡号一样，请确认！');", true);
                        return;
                    }
                    else
                    {
                        // 修改原门卡信息
                        userAttBasicModel.CardState = (int)CardUseState.UnEnable;
                        userAttBasicModel.CardUnEnableTime = DateTime.Now;
                        userAttBasicModel.UpdateTime = DateTime.Now;
                        userAttBasicModel.OperateorID = UserID;
                        userAttBasicModel.Remark = txtDesc.Text;

                        // 要更换的门卡信息
                        UserAttBasicInfo model = new UserAttBasicInfo();
                        model.AnnualLeaveBase = userAttBasicModel.AnnualLeaveBase;
                        model.AttendanceType = userAttBasicModel.AttendanceType;
                        model.CardEnableTime = DateTime.Now;
                        model.CardState = (int)CardUseState.Enable;
                        model.CreateTime = DateTime.Now;
                        model.Deleted = false;
                        model.EmployeeName = userAttBasicModel.EmployeeName;
                        model.GoWorkTime = userAttBasicModel.GoWorkTime;
                        model.OffWorkTime = userAttBasicModel.OffWorkTime;
                        model.OperateorDept = 0;
                        model.OperateorID = UserID;
                        model.Sort = 0;
                        model.UpdateTime = DateTime.Now;
                        model.UserCode = userAttBasicModel.UserCode;
                        model.Userid = userAttBasicModel.Userid;
                        model.UserName = userAttBasicModel.UserName;
                        model.AreaID = userAttBasicModel.AreaID;
                        model.CardNo = cardno;

                        bool b = userAttBasicInfoManager.ExChangeCardNo(userAttBasicModel, model, cardStoreModel);
                        if (b)
                        {
                            ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnExchange", "alert('" + userAttBasicModel.EmployeeName + "的门卡更换成功！');", true);
                            getUserInfo(userid);
                            List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(userAttBasicModel.AreaID, ESP.HumanResource.Common.Status.EntrySendMail);
                            List<MailAddress> mailList = new List<MailAddress>();
                            StringBuilder strBuilder = new StringBuilder();
                            foreach (ESP.HumanResource.Entity.UsersInfo info in list)
                            {
                                if (!string.IsNullOrEmpty(info.Email))
                                {
                                    mailList.Add(new MailAddress(info.Email));
                                     strBuilder.Append(info.Email);
                                }
                            }

                            ESP.Logging.Logger.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + UserInfo.FullNameCN + "更换门卡（"
                                + userAttBasicModel.CardNo + "-->" + model.CardNo + "）,收件人（" + strBuilder.ToString() + "）",
                                "行政系统", ESP.Logging.LogLevel.Information);

                            string url = "http://" + Request.Url.Authority + "/MailTemplate/UserCardMail.aspx?userid=" + userid + "&cardno=" + cardno + "&oldcardno=" + userAttBasicModel.CardNo + "&flag=3";
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                            ESP.Mail.MailManager.Send("更换门卡信息", body, true, null, mailList.ToArray(), null, null, null);
                        }
                        else
                            ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnExchange", "alert('" + userAttBasicModel.EmployeeName + "的门卡更换失败，请与系统管理员联系！');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnExchange", "alert('" + userAttBasicModel.EmployeeName + "的门卡更换失败，请与系统管理员联系！');", true);
                }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnExchange", "alert('您所更换的门卡不存在于门卡信息库中，请确认！');", true);
            //}
        }

        /// <summary>
        /// 作废门卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBlankOut_Click(object sender, ImageClickEventArgs e)
        {
            // 用户编号
            int userid = 0;
            // 门卡号
            string cardno = "";
            if (!string.IsNullOrEmpty(hidUserid.Value))
            {
                userid = int.Parse(hidUserid.Value);
            }
            if (!string.IsNullOrEmpty(txtCardno.Text))
            {
                cardno = txtCardno.Text;
            }

            CardStoreManager cardStoreManager = new CardStoreManager();
            CardStoreInfo cardStoreModel = cardStoreManager.GetModelByCardNo(cardno);
            UserAttBasicInfoManager userAttBasicInfoManager = new UserAttBasicInfoManager();
            UserAttBasicInfo userAttBasicModel = userAttBasicInfoManager.GetEnableCardByUserid(userid);
            // 获得该用户的正在使用门卡信息，如果门卡信息不为空，就修改该用户的门卡信息(状态，停用时间，最后更新时间，操作人)
            if (userAttBasicModel != null)
            {
                userAttBasicModel.CardState = (int)CardUseState.UnEnable;
                userAttBasicModel.CardUnEnableTime = DateTime.Now;
                userAttBasicModel.UpdateTime = DateTime.Now;
                userAttBasicModel.OperateorID = UserID;
                userAttBasicModel.Remark = txtDesc.Text;
                //cardStoreModel.State = Status.
                userAttBasicInfoManager.Update(userAttBasicModel, cardStoreModel, 2);
                ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnUnEnable", "alert('" + userAttBasicModel.EmployeeName + "的门卡作废成功！');", true);
                getUserInfo(userid);

                List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(userAttBasicModel.AreaID, ESP.HumanResource.Common.Status.EntrySendMail);
                List<MailAddress> mailList = new List<MailAddress>();
                StringBuilder strBuilder = new StringBuilder();
                foreach (ESP.HumanResource.Entity.UsersInfo info in list)
                {
                    if (!string.IsNullOrEmpty(info.Email))
                    {
                        mailList.Add(new MailAddress(info.Email));
                        strBuilder.Append(info.Email);
                    }
                }

                ESP.Logging.Logger.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + UserInfo.FullNameCN + "作废门卡（"
                                + userAttBasicModel.CardNo + "）,收件人（" + strBuilder.ToString() + "）",
                                "行政系统", ESP.Logging.LogLevel.Information);

                string url = "http://" + Request.Url.Authority + "/MailTemplate/UserCardMail.aspx?userid=" + userid + "&cardno=" + cardno + "&flag=2";
                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);

                ESP.Mail.MailManager.Send("停用门卡信息", body, true, null, mailList.ToArray(), null, null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnUnEnable", "alert('" + userAttBasicModel.EmployeeName + "的门卡作废失败，请与系统管理员联系！');", true);
            }
        }

        ///// <summary>
        ///// 获取新卡号
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnGetNewCard_Click(object sender, ImageClickEventArgs e)
        //{
        //    // 获得最小的门卡记录信息
        //    CardStoreManager cardStoreManager = new CardStoreManager();
        //    CardStoreInfo cardStoreModel = cardStoreManager.GetFirstCardModel(int.Parse(AreaID));
        //    if (cardStoreModel != null)
        //    {
        //        txtCardno.Text = cardStoreModel.CardNo.ToString();
        //        ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnGetNewCard", "alert('门卡获取成功，新的门卡号是（" + cardStoreModel.CardNo.ToString() + "）！');", true);
        //    }
        //    else
        //        ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnGetNewCard", "alert('门卡库存不足，请导入新的门卡信息！');", true);
        //}

        ///// <summary>
        ///// 门卡库存信息
        ///// </summary>
        //public string CardStoreCount
        //{
        //    get
        //    {
        //        int count = new CardStoreManager().GetCardStoreCount(int.Parse(AreaID));
        //        if (count <= 5)
        //        {
        //            return "<span style=\"color: Red;font-weight:bold\">库存中剩余的门卡数为：" + count + "张。</span>";
        //        }
        //        else
        //        {
        //            return "<span style=\"font-weight:bold\">库存中剩余的门卡数为：" + count + "张。</span>";
        //        }
        //    }
        //}
    }
}