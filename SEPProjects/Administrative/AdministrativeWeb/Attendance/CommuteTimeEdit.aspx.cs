using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;
using ESP.Administrative.BusinessLogic;

namespace AdministrativeWeb
{
    public partial class CommuteTimeEdit : ESP.Web.UI.PageBase
    {
        private string lowerUserIds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cldBeginDate.SelectedDate = DateTime.Now;
                // 通过用户ID获得用户信息
                if (Request["ApplicantID"] != null)
                {
                    int applicantID = Convert.ToInt32(Request["ApplicantID"]);
                    upUserInfo.Visible = true;
                    getUserInfo(applicantID);
                }

                // 创建一个人员节点数的根节点
                ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
                rootNode.Text = "星言云汇人力结构图";
                rootNode.Expanded = true;
                rootNode.ImageUrl = "/images/treeview/root.gif";
                userTreeView.Nodes.Add(rootNode);
                int n = 0;

                lowerUserIds = new UserAttBasicInfoManager().GetStatUserIDs(UserID);

                // 局部变量：保存部门信息包括1,2,3级部门信息
                IList<ESP.Framework.Entity.DepartmentInfo> depinfo = new List<ESP.Framework.Entity.DepartmentInfo>();


                // 获得当前登录用户的人员部门信息
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);
                ESP.Administrative.BusinessLogic.OperationAuditManageManager operationAuditManager = new ESP.Administrative.BusinessLogic.OperationAuditManageManager();
                // 获得用
                List<ESP.Administrative.Entity.OperationAuditManageInfo> operationAuditList = operationAuditManager.GetModelList(" StatisticianID=" + UserID + " or hradminid=" + UserID);
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
                                // 判断这个部门是否已经在Dictionary中了
                                if (!dic.ContainsKey(dm.DepartmentID))
                                    dic.Add(dm.DepartmentID, dm);
                            }
                        }
                    }

                    if (operationAuditList != null && operationAuditList.Count > 0)
                    {
                        foreach (ESP.Administrative.Entity.OperationAuditManageInfo operationAuditModel in operationAuditList)
                        {
                            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                            int depid = new UserAttBasicInfoManager().GetUserDepartmentId(operationAuditModel.UserID);
                            ESP.Framework.Entity.DepartmentInfo departmentInfo = ESP.Framework.BusinessLogic.DepartmentManager.Get(depid);
                            if (departmentInfo != null && departmentInfo.DepartmentLevel == 3)
                            {
                                // 添加当前用户上级部门信息
                                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(depid, depList);
                                foreach (ESP.Framework.Entity.DepartmentInfo dm in depList)
                                {
                                    // 判断这个部门是否已经在Dictionary中了
                                    if (!dic.ContainsKey(dm.DepartmentID))
                                        dic.Add(dm.DepartmentID, dm);
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


                string str = "";
                bool isP = false;
                bool isHerf = false;
                // 开始绑定人员节点数
                depinfo = depinfo.OrderBy(c => c.DepartmentName).ToList<ESP.Framework.Entity.DepartmentInfo>();
                BindTree(depinfo, rootNode, 0, ref  n, str, isP, isHerf);
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
                            employeeList = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(info.DepartmentID).Where(x => x.Status == 1).ToList();
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
                                if (lowerUserIds.IndexOf(employeeinfo.UserID.ToString()) == -1 && employeeinfo.UserID == UserID)
                                {
                                    continue;
                                }

                                if (employeeinfo.Status != ESP.HumanResource.Common.Status.Dimission
                                    && employeeinfo.Status != ESP.HumanResource.Common.Status.IsDeleted
                                    && employeeinfo.Status != ESP.HumanResource.Common.Status.WaitEntry
                                    && employeeinfo.Status != ESP.HumanResource.Common.Status.Fieldword)
                                {
                                    ComponentArt.Web.UI.TreeViewNode tnemployee = new ComponentArt.Web.UI.TreeViewNode();
                                    tnemployee.Text = employeeinfo.FullNameCN;
                                    tnemployee.Value = employeeinfo.UserID.ToString();
                                    tnemployee.ImageUrl = "/images/treeview/user.gif";
                                    tnemployee.NavigateUrl = "CommuteTimeEdit.aspx?ApplicantID=" + employeeinfo.UserID;

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
        /// 绑定部门节点信息
        /// </summary>
        /// <param name="depinfo">部门信息</param>
        /// <param name="parentNode">节点树的根节点</param>
        /// <param name="Id"></param>
        /// <param name="n"></param>
        /// <param name="treenode"></param>
        /// <param name="isP"></param>
        /// <param name="isHerf"></param>
        void BindTreeUserInfo(IList<ESP.Framework.Entity.DepartmentInfo> depinfo,
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
                                if (employeeinfo.Status == ESP.HumanResource.Common.Status.Entry
                                    || employeeinfo.Status == ESP.HumanResource.Common.Status.Passed)
                                {
                                    ComponentArt.Web.UI.TreeViewNode tnemployee = new ComponentArt.Web.UI.TreeViewNode();
                                    tnemployee.Text = employeeinfo.FullNameCN;
                                    tnemployee.Value = employeeinfo.UserID.ToString() + ",0"; // 节点值后添加一个0是用来标识改节点是否可以被选择
                                    tnemployee.ImageUrl = "/images/treeview/user.gif";
                                    //tnemployee.NavigateUrl = "CardNoVindicate.aspx?ApplicantID=" + employeeinfo.UserID;

                                    n++;
                                    tnemployee.ToolTip = employeeinfo.FullNameCN;
                                    tn.Nodes.Add(tnemployee);
                                }
                            }
                            employeeList = null;
                            departmentId = 0;
                        }
                        BindTreeUserInfo(depinfo, tn, info.DepartmentID, ref n, treenode, isP, isHerf);
                    }
                }
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

            DataCodeManager datacode = new DataCodeManager();
            List<DataCodeInfo> list = datacode.GetDataCodeByType("AttendanceType");
            if (list != null && list.Count > 0)
            {
                drpAttendanceType.DataSource = list;
                drpAttendanceType.DataTextField = "Name";
                drpAttendanceType.DataValueField = "Code";
                drpAttendanceType.DataBind();
            }

            UserAttBasicInfoManager userCarInfoManager = new UserAttBasicInfoManager();
            UserAttBasicInfo userCardInfo = userCarInfoManager.GetModelByUserid(userid);
            if (userCardInfo != null)
            {
                //txtCardno.Text = userCardInfo.CardNo;
                hidUserCardID.Value = userCardInfo.ID.ToString();
                drpAttendanceType.SelectedValue = userCardInfo.AttendanceType.ToString();
                txtGoWorkTime.Text = userCardInfo.GoWorkTime;
                txtOffWorkTime.Text = userCardInfo.OffWorkTime;
                //txtAnnualLeaveBase.Text = userCardInfo.AnnualLeaveBase.ToString();
                cldBeginDate.SelectedDate = userCardInfo.WorkTimeBeginDate == null ? DateTime.Now : userCardInfo.WorkTimeBeginDate.Value;
            }
            labUserName.Text = empname;            // 员工姓名
            labUserCode.Text = emp.ID;             // 用户编号
            labUserITCode.Text = empITCode;        // 员工账号
            labUserTel.Text = empMobile;              // 公司电话
            labUserDept.Text = strdept;            // 所属部门
            hidUserid.Value = userid.ToString();   // 用户编号
        }

        /// <summary>
        /// 保存用户卡号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESP.Administrative.BusinessLogic.UserAttBasicInfoManager b = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
             ESP.Administrative.Entity.UserAttBasicInfo usercardinfo=null;

            if (!string.IsNullOrEmpty(hidUserCardID.Value) && hidUserCardID.Value != "0")
            {
                int id = int.Parse(hidUserCardID.Value);
                usercardinfo= b.GetModel(id);
                //usercardinfo.CardNo = txtCardno.Text != null ? txtCardno.Text.Trim() : "";
                usercardinfo.WorkTimeBeginDate = cldBeginDate.SelectedDate;
                if (!string.IsNullOrEmpty(txtGoWorkTime.Text))
                    usercardinfo.GoWorkTime = txtGoWorkTime.Text.Trim();
                if (!string.IsNullOrEmpty(txtOffWorkTime.Text))
                    usercardinfo.OffWorkTime = txtOffWorkTime.Text.Trim();
                usercardinfo.AttendanceType = int.Parse(drpAttendanceType.SelectedValue);
                //if (!string.IsNullOrEmpty(txtAnnualLeaveBase.Text))
                //    usercardinfo.AnnualLeaveBase = int.Parse(txtAnnualLeaveBase.Text.Trim());

                //if (!chkIsHaveAnnualLeave.Checked)
                //{
                //    usercardinfo.AnnualLeaveBase = 0;
                //}

                usercardinfo.UpdateTime = DateTime.Now;
                b.UpdateAttBasicAndCommuterTime(usercardinfo);
                //ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")修改了" + usercardinfo.EmployeeName + "(" + usercardinfo.Userid + ")的考勤系统基本信息",
                //    "考勤系统基本信息", ESP.Logging.LogLevel.Information);
            }
            else
            {
                usercardinfo = new ESP.Administrative.Entity.UserAttBasicInfo();
                usercardinfo.Userid = Convert.ToInt32(hidUserid.Value != null ? hidUserid.Value : "0");
                usercardinfo.UserCode = labUserCode.Text != null ? labUserCode.Text : "";
                usercardinfo.UserName = labUserITCode.Text != null ? labUserITCode.Text : "";
                usercardinfo.EmployeeName = labUserName.Text != null ? labUserName.Text : "";
                //usercardinfo.CardNo = txtCardno.Text != null ? txtCardno.Text.Trim() : "";
                usercardinfo.OperateorID = UserID;
                if (!string.IsNullOrEmpty(txtGoWorkTime.Text))
                    usercardinfo.GoWorkTime = txtGoWorkTime.Text.Trim();
                if (!string.IsNullOrEmpty(txtOffWorkTime.Text))
                    usercardinfo.OffWorkTime = txtOffWorkTime.Text.Trim();
                usercardinfo.AttendanceType = int.Parse(drpAttendanceType.SelectedValue);
                //if (!string.IsNullOrEmpty(txtAnnualLeaveBase.Text))
                //    usercardinfo.AnnualLeaveBase = int.Parse(txtAnnualLeaveBase.Text.Trim());
                usercardinfo.CardState = (int)CardUseState.Enable;
                usercardinfo.AreaID = b.GetRootDepartmentID(UserID).DepartmentID;
                usercardinfo.CardEnableTime = DateTime.Now;

                //if (!chkIsHaveAnnualLeave.Checked)
                //{
                //    usercardinfo.AnnualLeaveBase = 0;
                //}

                b.AddAttBasicAndCommuterTime(usercardinfo);
                //ESP.Logging.Logger.Add(UserInfo.FullNameCN + "(" + UserID + ")添加了" + usercardinfo.EmployeeName + "(" + usercardinfo.Userid + ")的考勤系统基本信息",
                //    "考勤系统基本信息", ESP.Logging.LogLevel.Information);
            }
            ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnSave", "alert('更新成功！');", true);
            Response.Redirect("CommuteTimeEdit.aspx?ApplicantID=" + usercardinfo.Userid);
        }
    }
}