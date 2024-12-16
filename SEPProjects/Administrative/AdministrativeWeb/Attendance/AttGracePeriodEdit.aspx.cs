using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Framework.Entity;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.Attendance
{
    public partial class AttGracePeriodEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 通过用户ID获得用户信息
                if (Request["ApplicantID"] != null)
                {
                    string applicantID = Request["ApplicantID"];
                    SelectUserID = applicantID;
                    upUserInfo.Visible = true;
                    getUserInfo(int.Parse(applicantID));
                    txtEndTime.SelectedDate = DateTime.Now;
                }
                GetDepartmentInfo();
            }
        }

        /// <summary>
        /// 被选择用户的ID值
        /// </summary>
        public string SelectUserID
        {
            get
            {
                return this.ViewState["SelectUserID"] as string;
            }
            set
            {
                this.ViewState["SelectUserID"] = value;
            }
        }

        /// <summary>
        /// 通过用户编号获得用户信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        private void getUserInfo(int userid)
        {
            string script = string.Empty;
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
            IList<string> depts = emp.GetDepartmentNames();
            string deptstr = string.Empty;
            if (depts != null && depts.Count != 0)
            {
                for (int i = 0; i < depts.Count; i++)
                {
                    deptstr += depts[i].ToString() + ",";
                }
            }
            string empname = emp.Name == string.Empty ? "&nbsp;" : emp.Name;
            string empITCode = emp.ITCode == string.Empty ? "&nbsp;" : emp.ITCode;
            string empMobile = emp.Mobile == string.Empty ? "&nbsp;" : emp.Mobile;
            string empEmail = emp.EMail == string.Empty ? "&nbsp;" : emp.EMail;
            string empTel = emp.Telephone == string.Empty ? "&nbsp;" : emp.Telephone;
            string strdept = deptstr.TrimEnd(',') == string.Empty ? "&nbsp;" : deptstr.TrimEnd(',');

            labUserName.Text = empname;            // 员工姓名
            labUserCode.Text = emp.ID;             // 用户编号
            labUserITCode.Text = empITCode;        // 员工账号
            labUserTel.Text = empMobile;              // 公司电话
            labUserDept.Text = strdept;            // 所属部门
            hidUserid.Value = userid.ToString();   // 用户编号
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
                                if (employeeinfo.Status != ESP.HumanResource.Common.Status.Dimission
                                    && employeeinfo.Status != ESP.HumanResource.Common.Status.IsDeleted
                                    && employeeinfo.Status != ESP.HumanResource.Common.Status.WaitEntry
                                    && employeeinfo.Status != ESP.HumanResource.Common.Status.Fieldword)
                                {
                                    ComponentArt.Web.UI.TreeViewNode tnemployee = new ComponentArt.Web.UI.TreeViewNode();
                                    tnemployee.Text = employeeinfo.FullNameCN;
                                    tnemployee.Value = employeeinfo.UserID.ToString();
                                    tnemployee.ImageUrl = "/images/treeview/user.gif";
                                    tnemployee.NavigateUrl = "AttGracePeriodEdit.aspx?ApplicantID=" + employeeinfo.UserID;

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
            DataCodeInfo datamodel = new DataCodeManager().GetDataCodeByType("BeijingAttendanceAdmin")[0];
            string[] datacode = datamodel.Code.Split(new char[] { ',' });
            DataCodeInfo datamodelchongqing = new DataCodeManager().GetDataCodeByType("ChongQingAttendanceAdmin")[0];
            string[] datacodechongqing = datamodelchongqing.Code.Split(new char[] { ',' });


            //// 获得团队HRAdmin和考勤统计员的编号
            //DataCodeInfo HRAdminIdModel = new DataCodeManager().GetDataCodeByType("HRAdminIDs")[0];
            //string HRAdminIds = HRAdminIdModel.Code;
            //DataCodeInfo StatisticianIDModel = new DataCodeManager().GetDataCodeByType("StatisticianIDs")[0];
            //string StatisticianIds = StatisticianIDModel.Code;

            // 局部变量：保存部门信息包括1,2,3级部门信息
            IList<ESP.Framework.Entity.DepartmentInfo> depinfo = new List<ESP.Framework.Entity.DepartmentInfo>();
            //ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();

            // 各个地区系统管理员，可以查看各个地区所有员工的考勤信息
            if (UserID.ToString() == datacode[0] || UserID.ToString() == datacodechongqing[0]
                || (datacode.Length > 3 && UserID.ToString() == datacode[2]))
            {
                IList<ESP.Framework.Entity.DepartmentInfo> list = null;
                ESP.Framework.Entity.DepartmentInfo depart = new DepartmentInfo();
                if (UserID.ToString() == datacode[0] || (datacode.Length > 3 && UserID.ToString() == datacode[2]))
                {
                    list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion((int)AreaID.HeadOffic);
                    depart = ESP.Framework.BusinessLogic.DepartmentManager.Get((int)AreaID.HeadOffic);
                }
                else if (UserID.ToString() == datacodechongqing[0])
                {
                    list = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion((int)AreaID.ChongqingOffic);
                    depart = ESP.Framework.BusinessLogic.DepartmentManager.Get((int)AreaID.ChongqingOffic);
                }

                if (list != null && list.Count > 0)
                {
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();

                    // 判断这个部门是否已经在Dictionary中了
                    if (!dic.ContainsKey(depart.DepartmentID))
                        dic.Add(depart.DepartmentID, depart);

                    foreach (ESP.Framework.Entity.DepartmentInfo departmentinfo in list)
                    {
                        // 判断这个部门是否已经在Dictionary中了
                        if (!dic.ContainsKey(departmentinfo.DepartmentID))
                            dic.Add(departmentinfo.DepartmentID, departmentinfo);
                    }

                    // 将Dictionary中的值保存到一个List的集合中
                    Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
                    foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
                    {
                        depinfo.Add(departmentInfo);
                    }
                }
            }
            //else if (HRAdminIds.IndexOf(UserID.ToString()) != -1)
            //{
            //    List<ESP.Framework.Entity.EmployeeInfo> operationAuditList = operationAuditManager.GetHRAdminSubordinates(UserID);
            //    Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
            //    if (operationAuditList != null && operationAuditList.Count > 0)
            //    {
            //        foreach (ESP.Framework.Entity.EmployeeInfo operationAuditModel in operationAuditList)
            //        {
            //            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            //            int depid = new UserAttBasicInfoManager().GetUserDepartmentId(operationAuditModel.UserID);
            //            ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
            //            if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
            //            {
            //                // 添加当前用户上级部门信息
            //                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(depid, depList);
            //                foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
            //                {
            //                    // 判断这个部门是否已经在Dictionary中了
            //                    if (!dic.ContainsKey(dm.DepartmentID))
            //                        dic.Add(dm.DepartmentID, dm);
            //                }
            //            }
            //        }
            //    }
            //    // 将Dictionary中的值保存到一个List的集合中
            //    Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
            //    foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
            //    {
            //        depinfo.Add(departmentInfo);
            //    }
            //}
            //else if (StatisticianIds.IndexOf(UserID.ToString()) != -1)
            //{
            //    List<ESP.Framework.Entity.EmployeeInfo> operationAuditList = operationAuditManager.GetStatisticianSubordinates(UserID);
            //    Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
            //    if (operationAuditList != null && operationAuditList.Count > 0)
            //    {
            //        foreach (ESP.Framework.Entity.EmployeeInfo operationAuditModel in operationAuditList)
            //        {
            //            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            //            int depid = new UserAttBasicInfoManager().GetUserDepartmentId(operationAuditModel.UserID);
            //            ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
            //            if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
            //            {
            //                // 添加当前用户上级部门信息
            //                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(depid, depList);
            //                foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
            //                {
            //                    // 判断这个部门是否已经在Dictionary中了
            //                    if (!dic.ContainsKey(dm.DepartmentID))
            //                        dic.Add(dm.DepartmentID, dm);
            //                }
            //            }
            //        }
            //    }
            //    // 将Dictionary中的值保存到一个List的集合中
            //    Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
            //    foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
            //    {
            //        depinfo.Add(departmentInfo);
            //    }
            //}
            //else
            //{
            //    // 获得当前登录用户的人员部门信息
            //    IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);

            //    // 获得用
            //    List<ESP.Administrative.Entity.OperationAuditManageInfo> operationAuditList = operationAuditManager.GetModelList(" TeamLeaderID=" + UserID);
            //    if (list != null && list.Count > 0)
            //    {
            //        Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
            //        foreach (ESP.Framework.Entity.EmployeePositionInfo empPosInfo in list)
            //        {
            //            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            //            ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(empPosInfo.DepartmentID);
            //            if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
            //            {
            //                // 添加当前用户上级部门信息
            //                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(empPosInfo.DepartmentID, depList);
            //                foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
            //                {
            //                    // 判断这个部门是否已经在Dictionary中了
            //                    if (!dic.ContainsKey(dm.DepartmentID))
            //                        dic.Add(dm.DepartmentID, dm);
            //                }
            //            }
            //        }

            //        if (operationAuditList != null && operationAuditList.Count > 0)
            //        {
            //            foreach (ESP.Administrative.Entity.OperationAuditManageInfo operationAuditModel in operationAuditList)
            //            {
            //                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            //                int depid = new UserAttBasicInfoManager().GetUserDepartmentId(operationAuditModel.UserID);
            //                ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
            //                if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
            //                {
            //                    // 添加当前用户上级部门信息
            //                    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(depid, depList);
            //                    foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
            //                    {
            //                        // 判断这个部门是否已经在Dictionary中了
            //                        if (!dic.ContainsKey(dm.DepartmentID))
            //                            dic.Add(dm.DepartmentID, dm);
            //                    }
            //                }
            //            }
            //        }

            //        // 将Dictionary中的值保存到一个List的集合中
            //        Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
            //        foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
            //        {
            //            depinfo.Add(departmentInfo);
            //        }
            //    }
            //} 
            //depinfo = ESP.Framework.BusinessLogic.DepartmentManager.GetAll();

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
        /// 新添门卡记录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime endTime = DateTime.Now;
            if (txtEndTime.SelectedDate != null && txtEndTime.SelectedDate != DateTime.MinValue)
            {
                endTime = txtEndTime.SelectedDate;
            }
            if (endTime <= DateTime.Now)
            {
                ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnSave", "alert('开通截止时间必须大于当前时间， 请确认。');", true);
                return;
            }

            AttGracePeriodManager manager = new AttGracePeriodManager();
            AttGracePeriodInfo attGracePeriodModel = new AttGracePeriodInfo();
            attGracePeriodModel.UserID = Convert.ToInt32(hidUserid.Value != null ? hidUserid.Value : "0");
            attGracePeriodModel.UserCode = labUserCode.Text != null ? labUserCode.Text : "";
            attGracePeriodModel.EmployeeName = labUserName.Text != null ? labUserName.Text : "";
            attGracePeriodModel.BeginTime = DateTime.Now;
            attGracePeriodModel.EndTime = endTime;
            attGracePeriodModel.Remark = txtRemark.Text.Trim();
            attGracePeriodModel.CreateTime = DateTime.Now;
            attGracePeriodModel.OperatorID = UserID;
            attGracePeriodModel.OperatorName = UserInfo.FullNameCN;
            attGracePeriodModel.UpdateTime = DateTime.Now;
            manager.Add(attGracePeriodModel);
            ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnSave", "alert('" + attGracePeriodModel.EmployeeName + "的考勤提交权限已经开通。');", true);

            ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")开通了" + attGracePeriodModel.EmployeeName + "的七天提交限制(从" + attGracePeriodModel.BeginTime.ToString("yyyy-MM-dd HH:mm") + "开始到" + attGracePeriodModel.EndTime.ToString("yyyy-MM-dd HH:mm") + "结束)",
                "考勤系统基本信息", ESP.Logging.LogLevel.Information);
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttGracePeriodList.aspx");
        }
    }
}
