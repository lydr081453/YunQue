using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Edit
{
    public partial class ProjectTabEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.GridProject.CausedCallback)
            {
                Search();
            }
        }
        private string GetUser()
        {
            string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
            user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ",";
            return user;
        }

        void GridProject_NeedRebind(object sender, EventArgs e)
        {
            Search();
        }

        void GridProject_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridProject.CurrentPageIndex = e.NewIndex;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            GridProject.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(GridProject_DeleteCommand);
            GridProject.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridProject_NeedRebind);
            GridProject.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridProject_PageIndexChanged);

        }

        void GridProject_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            int projectid = int.Parse(e.Item["ProjectID"].ToString());
            ESP.Finance.BusinessLogic.ProjectManager.PhysicalDelete(projectid);
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }
        private void Search()
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<SqlParameter>();
            string users = GetUser();

            if (users.IndexOf("," + CurrentUserID.ToString() + ",") >= 0)
            {
                term = " and Status in('" + (int)Status.Saved + "','" + (int)Status.BizReject + "','" + (int)Status.ContractReject + "','" + (int)Status.FinanceReject + "')";

            }
            else
            {
                term = " and Status in('" + (int)Status.Saved + "','" + (int)Status.BizReject + "','" + (int)Status.ContractReject + "','" + (int)Status.FinanceReject + "') " +
                       " and (projectid in(select projectid from f_projectmember where memberuserid=@memberuserid) or creatorid=@memberuserid or applicantUserID=@memberuserid";
                    term += ")";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@memberuserid", CurrentUserID));
            }

            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                string customerIDs = string.Empty;
                IList<ESP.Finance.Entity.CustomerTmpInfo> customerList = ESP.Finance.BusinessLogic.CustomerTmpManager.GetList(" NameCN1 like '%" + this.txtKey.Text.Trim() + "%' or  NameEN1 like '%" + this.txtKey.Text.Trim() + "%' ");
                foreach (ESP.Finance.Entity.CustomerTmpInfo cusModel in customerList)
                {
                    customerIDs += cusModel.CustomerTmpID.ToString() + ",";
                }
                customerIDs = customerIDs.TrimEnd(',');

                term += " and (serialcode like '%'+@serialcode+'%' or BusinessDescription like '%'+@serialcode+'%' or projectcode like '%'+@serialcode+'%' or GroupName like '%'+@serialcode+'%' or BranchName like '%'+@serialcode+'%' or BusinessTypeName like '%'+@serialcode+'%' or applicantEmployeename like '%'+@serialcode+'%' ";
                if (!string.IsNullOrEmpty(customerIDs))
                    term += " or CustomerID in(" + customerIDs + "))";
                else
                    term += ")";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
            }

            this.GridProject.DataSource = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
            this.GridProject.DataBind();
        }


        protected void GridProject_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = (ESP.Finance.Entity.ProjectInfo)e.DataItem;
            //所有部门级联字符串拼接
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(int.Parse(e.Item["GroupID"].ToString()), depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            if (!string.IsNullOrEmpty(groupname))
                e.Item["GroupName"] = groupname.Substring(0, groupname.Length - 1);
            e.Item["StatusText"] = State.SetState(int.Parse(e.Item["Status"].ToString()));
            e.Item["View"] = "<a target=\"_blank\" href=\"/project/ProjectDisplay.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + e.Item["ProjectID"] + "\">" +
                                                            "<img src=\"../images/dc.gif\" border=\"0px;\" title=\"查看\" /></a>";
            if (int.Parse(CurrentUser.SysID) == projectModel.CreatorID || int.Parse(CurrentUser.SysID) == projectModel.ApplicantUserID)
            {
                switch (e.Item["Step"].ToString())
                {
                    case "1":
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_1, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                    case "2":
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_2, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                    case "3":
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_3, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                    case "4":
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_4, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                    case "5":
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_5, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                    case "6":
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_6, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                    case "7":
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_7, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                    default:
                        e.Item["Edit"] = "<a href=\"/project/" + string.Format(State.addstatus_8, e.Item["ProjectID"]) + "\"><img src=\"../images/edit.gif\" border=\"0px;\" title=\"编辑\" /></a>";
                        break;
                }
            }
            else
            {
                e.Item["Edit"] = "";
            }
            e.Item["Print"] = "<a href='/project/ProjectPrint.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + e.Item["ProjectID"] +
                                                            "' target=\"_blank\"><img title=\"项目号申请单打印预览\" src=\"/images/ProjectPrint.gif\" border=\"0px;\" /></a>";

        }

    }
}
