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
using ESP.Administrative.BusinessLogic;
using ESP.Framework.Entity;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.Attendance
{
    public partial class OperationManageEdit : ESP.Web.UI.PageBase
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
                                    tnemployee.ShowCheckBox = true;
                                    tnemployee.Text = employeeinfo.FullNameCN;
                                    tnemployee.Value = employeeinfo.UserID.ToString();
                                    tnemployee.ImageUrl = "/images/treeview/user.gif";
                                    //tnemployee.NavigateUrl = "OperationManageEdit.aspx?ApplicantID=" + employeeinfo.UserID;

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
            UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
            // 分公司信息
            DepartmentInfo rootDepartment = userAttBasicManager.GetRootDepartmentID(UserID);
            int areaId = rootDepartment.DepartmentID;
            Dictionary<int, ESP.Framework.Entity.DepartmentInfo> dic = new Dictionary<int, ESP.Framework.Entity.DepartmentInfo>();
            // 总部
            if (areaId == (int)AreaID.HeadOffic)
            {
                DataCodeManager dataCodeManager = new DataCodeManager();
                List<DataCodeInfo> dataCodeList = dataCodeManager.GetDataCodeByType(UserID.ToString());
                if (dataCodeList != null && dataCodeList.Count > 0)
                {
                    string departmentIds = dataCodeList[0].Code;
                    string[] departmentid = departmentIds.Split(new char[] { ',' });
                    if (departmentid != null && departmentid.Length > 0)
                    {
                        foreach (string deptid in departmentid)
                        {
                            IList<DepartmentInfo> departmentList = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(int.Parse(deptid));
                            foreach (ESP.Framework.Entity.DepartmentInfo depart in departmentList)
                            {
                                // 判断这个部门是否已经在Dictionary中了
                                if (!dic.ContainsKey(depart.DepartmentID))
                                    dic.Add(depart.DepartmentID, depart);
                            }

                            ESP.Framework.Entity.DepartmentInfo departmentModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(int.Parse(deptid));
                            // 判断这个部门是否已经在Dictionary中了
                            if (!dic.ContainsKey(departmentModel.DepartmentID))
                                dic.Add(departmentModel.DepartmentID, departmentModel);
                        }
                    }
                }

            }
            else if (areaId == (int)AreaID.ChongqingOffic)  //Chongqing
            {
                IList<ESP.Framework.Entity.DepartmentInfo> childrenList = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(areaId);
                foreach (ESP.Framework.Entity.DepartmentInfo childerDepartment in childrenList)
                {
                    // 判断这个部门是否已经在Dictionary中了
                    if (!dic.ContainsKey(childerDepartment.DepartmentID))
                        dic.Add(childerDepartment.DepartmentID, childerDepartment);
                }
            }


            // 判断这个部门是否已经在Dictionary中了
            if (!dic.ContainsKey(rootDepartment.DepartmentID))
                dic.Add(rootDepartment.DepartmentID, rootDepartment);

            // 局部变量：保存部门信息包括1,2,3级部门信息
            IList<ESP.Framework.Entity.DepartmentInfo> depinfo = new List<ESP.Framework.Entity.DepartmentInfo>();
            // 将Dictionary中的值保存到一个List的集合中
            Dictionary<int, ESP.Framework.Entity.DepartmentInfo>.ValueCollection valueColl = dic.Values;
            foreach (ESP.Framework.Entity.DepartmentInfo departmentInfo in valueColl)
            {
                depinfo.Add(departmentInfo);
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

            UserAttBasicInfoManager userCarInfoManager = new UserAttBasicInfoManager();
            UserAttBasicInfo userCardInfo = userCarInfoManager.GetModelByUserid(userid);
            //if (userCardInfo != null)
            //{
            //    hidUserCardID.Value = userCardInfo.ID.ToString();
            //}
            //labUserName.Text = empname;            // 员工姓名
            //labUserCode.Text = emp.ID;             // 用户编号
            //labUserITCode.Text = empITCode;        // 员工账号
            //labUserTel.Text = empTel;              // 公司电话
            //labUserDept.Text = strdept;            // 所属部门
            //hidUserid.Value = userid.ToString();   // 用户编号
        }

        /// <summary>
        /// 当前选择用户的信息
        /// </summary>
        public string CurAttendanceUserInfo
        {
            get
            {
                string username = ESP.Framework.BusinessLogic.UserManager.Get(int.Parse(SelectUserID)).FullNameCN;
                return username + "";
            }
        }

        /// <summary>
        /// 补充用户上下班时间信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ComponentArt.Web.UI.TreeViewNode[] treeViewNodes = userTreeView.CheckedNodes;

                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager b = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                MonthStatManager monthStatManager = new MonthStatManager();
                //if (!string.IsNullOrEmpty(hidUserCardID.Value) && hidUserCardID.Value != "0")
                //{
                    //int id = int.Parse(hidUserCardID.Value);
                    //ESP.Administrative.Entity.UserAttBasicInfo usercardinfo = b.GetModel(id);

                    // 判断用户是否可以操作该考勤记录信息，如果月度考勤已经提交就不可以操作该考勤记录信息
                    //if (!monthStatManager.TryOperateData(usercardinfo.Userid, selectDate))
                    //{
                    //    ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnSave", "alert('月度考勤已经提交，您不能对该月考勤记录进行任何操作。');", true);
                    //    return;
                    //}

                    //if (txtGoWorkTime.Text != null && !string.IsNullOrEmpty(txtGoWorkTime.Text.Trim()))
                    //{
                    //    DateTime readTime = DateTime.Parse(selectDate.ToString("yyyy-MM-dd ") + txtGoWorkTime.Text.Trim());
                    //    ClockInInfo clockInInfo = new ClockInInfo();
                    //    clockInInfo.CardNO = usercardinfo.CardNo;
                    //    clockInInfo.CreateTime = DateTime.Now;
                    //    clockInInfo.Deleted = false;
                    //    clockInInfo.DoorName = "";
                    //    clockInInfo.InOrOut = true;

                    //    clockInInfo.OperatorID = UserID;
                    //    clockInInfo.OperatorName = UserInfo.FullNameCN;
                    //    clockInInfo.ReadTime = readTime;
                    //    clockInInfo.UpdateTime = DateTime.Now;
                    //    clockInInfo.UserCode = usercardinfo.UserCode;
                    //    clockInInfo.Remark = txtRemark.Text.Trim();
                    //    new ClockInManager().Add(clockInInfo);
                    //}
                    //if (txtOffWorkTime.Text != null && !string.IsNullOrEmpty(txtOffWorkTime.Text.Trim()))
                    //{
                    //    DateTime readTime = DateTime.Parse(selectDate.ToString("yyyy-MM-dd ") + txtOffWorkTime.Text.Trim());
                    //    TimeSpan span = TimeSpan.Parse(txtOffWorkTime.Text.Trim());
                    //    if (span <= TimeSpan.Parse(Status.OffWorkTimePoint))
                    //    {
                    //        readTime = readTime.AddDays(1);
                    //    }

                    //    ClockInInfo clockInInfo = new ClockInInfo();
                    //    clockInInfo.CardNO = usercardinfo.CardNo;
                    //    clockInInfo.CreateTime = DateTime.Now;
                    //    clockInInfo.Deleted = false;
                    //    clockInInfo.DoorName = "";
                    //    clockInInfo.InOrOut = false;

                    //    clockInInfo.OperatorID = UserID;
                    //    clockInInfo.OperatorName = UserInfo.FullNameCN;
                    //    clockInInfo.ReadTime = readTime;
                    //    clockInInfo.UpdateTime = DateTime.Now;
                    //    clockInInfo.UserCode = usercardinfo.UserCode;
                    //    clockInInfo.Remark = txtRemark.Text.Trim();
                    //    new ClockInManager().Add(clockInInfo);
                    //}
                    //ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnSave", "alert('数据保存成功！');", true);
                //}
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(upUserInfo, this.GetType(), "btnSave", "alert('数据保存失败！');", true);
            }
        }

        /// <summary>
        /// 上下班时间类型变换后，自动变换时间内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpInOrOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserAttBasicInfoManager b = new UserAttBasicInfoManager();
            UserAttBasicInfo userAttBasicModel = b.GetModelByUserid(UserID);
            //if (userAttBasicModel != null && userAttBasicModel.GoWorkTime != null && userAttBasicModel.OffWorkTime != null)
            //{
            //    //if (drpInOrOut.SelectedValue == "1")
            //    //    txtReadTime.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + userAttBasicModel.GoWorkTime);
            //    //else
            //    //    txtReadTime.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + userAttBasicModel.OffWorkTime);
            //}
            //else
            //    txtReadTime.SelectedDate = DateTime.Now;
        }
    }
}
