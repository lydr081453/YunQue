using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Framework.Entity;
using ESP.HumanResource.Entity;
using System.Text;
using System.Data.SqlClient;

namespace Portal.WebSite
{
    public partial class HumanMap : ESP.Web.UI.PageBase
    {
        IList<ESP.Framework.Entity.UserInfo> userCommentList;
        protected void Page_Load(object sender, EventArgs e)
        {

            int n = 0;
            userCommentList = ESP.Framework.BusinessLogic.UserManager.GetAll();
            // 获得当前登录用户的人员部门信息
            IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(UserID);

            // 保存部门信息包括1,2,3级部门信息
            IList<ESP.Framework.Entity.DepartmentInfo> depinfo = new List<ESP.Framework.Entity.DepartmentInfo>();
            // 总监级别数
            ESP.Framework.Entity.SettingsInfo settings = ESP.Framework.BusinessLogic.SettingManager.Get();
            int majordomoPosLevel = settings.Value<int>("MajordomoPositongLevel");

            // 人力地图人力管理员
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

            // 获得各团队HR用户编号（北京四个团队）
            string teamHRAdminIDs = "";
            List<DataCodeInfo> HRAdminIDList = dataCodeManager.GetDataCodeByType("TeamHRAdminIDs");
            if (HRAdminIDList != null && HRAdminIDList.Count > 0)
            {
                DataCodeInfo hrAdminIDModel = HRAdminIDList[0];
                if (hrAdminIDModel != null)
                {
                    teamHRAdminIDs = hrAdminIDModel.Code;
                }
            }

            // 分公司HR用户编号（广州、上海）
            string companyHRAdminIDs = "";
            List<DataCodeInfo> companyHRAdminIDList = dataCodeManager.GetDataCodeByType("CompanyHRAdminIDs");
            if (companyHRAdminIDList != null && companyHRAdminIDList.Count > 0)
            {
                DataCodeInfo companyHRAdminIDModel = companyHRAdminIDList[0];
                if (companyHRAdminIDModel != null)
                {
                    companyHRAdminIDs = companyHRAdminIDModel.Code;
                }
            }

            // 获得所有的部门信息
            depinfo = ESP.Framework.BusinessLogic.DepartmentManager.GetAll();

            string str = "";
            bool isP = false;
            bool isHerf = false;

            depinfo = depinfo.OrderBy(c => c.Ordinal).ToList<ESP.Framework.Entity.DepartmentInfo>();
            BindTree(depinfo, stv1.Nodes, 0, ref  n, str, isP, isHerf);

            if (Request["flag"] != null && Request["flag"].ToString() == "1" && Request["userid"] != null)
            {
                Response.Write(getUserInfo(Convert.ToInt32(Request["userid"])));
            }
            //获取通讯录快照具有使用权限的用户

            string userList = ESP.Configuration.ConfigurationManager.SafeAppSettings["AddressBookHistoryUsers"];
            if (userList.IndexOf("," + UserID.ToString() + ",") >= 0)
            {
                AddAddressBook.Visible = true;
            }
            else
            {
                AddAddressBook.Visible = false;
            }


            if (!IsPostBack)
            {
                getUserInfo();
                getAssetInfo();
                imgUserIcon.Src = UserIcon;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depinfo">部门信息</param>
        /// <param name="nds">树节点</param>
        /// <param name="Id">部门id</param>
        /// <param name="n">计数器</param>
        /// <param name="treenode">树选择框形式</param>
        /// <param name="isP">是否不要选择框</param>
        /// <param name="isHerf">是否要链接</param>
        void BindTree(IList<ESP.Framework.Entity.DepartmentInfo> depinfo, TreeNodeCollection nds, int Id, ref int n, string treenode, bool isP, bool isHerf)
        {

            TreeNode tn = null;
            // 部门下面的人员列表
            IList<ESP.Framework.Entity.EmployeeInfo> employeeList = null;
            int departmentId = 0;
            int currentuserdeptid = CurrentUser.GetDepartmentIDs()[0];
            foreach (ESP.Framework.Entity.DepartmentInfo info in depinfo)
            {
                if (info.ParentID == Id && info.DepartmentName.IndexOf("作废") < 0 && info.Status == 1)
                {

                    if (isHerf)
                    {
                        tn = new TreeNode(string.Format("<a href=\"DepartmentsForm.aspx?depid={0}&ParentID={2}\" style=\"font-size:12px;color:Black\">{1}</a>", info.DepartmentID.ToString(), info.DepartmentName, info.ParentID.ToString()), info.DepartmentID.ToString());
                    }
                    else if (isP)
                    {
                        int empCount = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeeCountByDepartment(info.DepartmentID);
                        tn = new TreeNode(info.DepartmentName+"("+empCount.ToString()+")", info.DepartmentID.ToString());
                    }
                    else
                    {
                        if (info.DepartmentLevel == 3)
                        {
                            employeeList = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(info.DepartmentID);
                            departmentId = info.DepartmentID;
                            tn = new TreeNode(info.DepartmentName+"("+employeeList.Count.ToString()+")", info.DepartmentID.ToString());
                        }
                        else
                        {
                            int empCount = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeeCountByDepartment(info.DepartmentID);
                            tn = new TreeNode(info.DepartmentName + "(" + empCount.ToString() + ")", info.DepartmentID.ToString());
                        }
                    }

                    //SelectAction设为None和Expand，则TreeNode不会生成超级链接
                    tn.SelectAction = TreeNodeSelectAction.None;
                    tn.ShowCheckBox = false;
                    n++;
                    tn.ToolTip = info.DepartmentName;
                    nds.Add(tn);



                    if (info.DepartmentLevel == 3 && employeeList != null && employeeList.Count > 0 && departmentId == info.DepartmentID)
                    {
                        int t = 0;
                        string humanmapNotView = System.Configuration.ConfigurationManager.AppSettings["HumanmapNotView"].ToString();

                        foreach (ESP.Framework.Entity.EmployeeInfo employeeinfo in employeeList)
                        {
                            if ((employeeinfo.Status == ESP.HumanResource.Common.Status.Entry
                                || employeeinfo.Status == ESP.HumanResource.Common.Status.Passed) && humanmapNotView.IndexOf("," + employeeinfo.UserID.ToString() + ",") < 0)
                            {
                                bool ret = ESP.HumanResource.BusinessLogic.DimissionFormManager.ExistsUser(employeeinfo.UserID);
                                string fullNameCN = employeeinfo.FullNameCN;
                                if (ret)
                                {
                                    if (int.Parse(CurrentUser.SysID) == int.Parse(System.Configuration.ConfigurationManager.AppSettings["DavidZhangID"]) || System.Configuration.ConfigurationManager.AppSettings["AdministrativeIDs"].IndexOf("," + currentuserdeptid.ToString() + ",") >= 0)
                                        fullNameCN += "  <font color='red'>(已提交离职)</font>";
                                }
                                ESP.Framework.Entity.UserInfo userModel = userCommentList.Where(x => x.UserID == employeeinfo.UserID).FirstOrDefault();
                                if (!string.IsNullOrEmpty(userModel.Comment))
                                {
                                    fullNameCN += "  <font color='red'>(" + userModel.Comment + ")</font>";
                                }
                                TreeNode tnemployee = new TreeNode(string.Format("<a href=\"javascript:getUserInfo(" + employeeinfo.UserID.ToString() + ");\" style=\"font-size:12px;color:Black\" >[" + (++t) + "] {1}</a>",
                                    employeeinfo.UserID.ToString(), fullNameCN, employeeinfo.UserID.ToString()), employeeinfo.UserID.ToString());

                                //SelectAction设为None和Expand，则TreeNode不会生成超级链接
                                tnemployee.SelectAction = TreeNodeSelectAction.None;
                                tnemployee.ShowCheckBox = false;
                                n++;
                                tnemployee.ToolTip = fullNameCN;

                                tn.ChildNodes.Add(tnemployee);
                            }
                        }
                        employeeList = null;
                        departmentId = 0;
                    }

                    BindTree(depinfo, tn.ChildNodes, info.DepartmentID, ref n, treenode, isP, isHerf);

                }
            }
        }
  
        /// <summary>
        /// 用户头像
        /// </summary>
        protected string UserIcon
        {
            get
            {
                if (UserID > 0)
                {
                    ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                    if (info != null && info.Photo.Trim().Length > 0)
                    {
                        return Portal.Common.Global.USER_ICON_FOLDER + info.Photo;
                    }
                }
                return "";
            }
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        private string getUserInfo(int userid)
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

            script += "<span class=\"title_red\">" + empname + "</span><br />";
            script += "<span class=\"text_red\">员工账号：" + empITCode + "</span><br />";
            script += "员工编号：" + emp.ID + "<br />";
            script += "所属部门：" + strdept + "<br />";

            return script;
        }

        private void getUserInfo()
        {
            string script = string.Empty;
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(UserID);
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

            script += "<span class=\"title_red\">" + empname + "</span><br />";
            script += "<span class=\"text_red\">员工账号：" + empITCode + "</span><br />";
            script += "员工编号：" + emp.ID + "<br />";
            script += "所属部门：" + strdept + "<br />";

            labUserInfo.Text = script;

        }

        private void getAssetInfo()
        {
            string script = "<font style='color:orange;'>●</font>&nbsp;&nbsp;";
            string assetString = string.Empty;
            string terms = " usercode=@usercode and status=@status";
            List<SqlParameter> parms = new List<SqlParameter>();
            int icount = 0;
            parms.Add(new SqlParameter("@usercode", CurrentUser.ID));
            parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

            IList<ESP.Finance.Entity.ITAssetReceivingInfo> list = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(terms, parms);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.Finance.Entity.ITAssetReceivingInfo rec in list)
                {
                    if (icount >= 2)
                        break;
                    ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(rec.AssetId);
                    if (model != null)
                    {
                        assetString += script + model.AssetName + "&nbsp;&nbsp;" + rec.ReceiveDate.ToString("yyyy-MM-dd") + "<br/>";
                        icount++;
                    }
                }
                if (list.Count > 2)
                    assetString += "<font style='color:orange;'>●</font>&nbsp;&nbsp; <span onclick=\"ShowAssetMsg(" + CurrentUserID + ")\" onmouseover=\"this.style.cursor='pointer',this.style.color='#f97b02'\" onmouseout=\"this.style.cursor='auto',this.style.color='#666666'\" >更多(" + list.Count + ")...</span>";
            }

            this.lblAsset.Text = assetString;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddAddressBook_Click(object sender, EventArgs e)
        {
            AddressBookInfo addressBook = new AddressBookInfo();
            addressBook.CreateId = UserID;
            try
            {
                addressBook.Id = ESP.HumanResource.BusinessLogic.AddressBookManager.Add(addressBook);
                bool ret = ESP.HumanResource.BusinessLogic.AddressBookManager.AddItem(addressBook.Id);
                if (ret)
                {
                    ShowCompleteMessage("添加成功！", "HumanMap.aspx");
                }
                else
                {
                    ShowCompleteMessage("添加失败！", "HumanMap.aspx");
                }
            }
            catch (Exception)
            {
                ShowCompleteMessage("添加失败！", "HumanMap.aspx");
                throw;
            }
        }
    }
}
