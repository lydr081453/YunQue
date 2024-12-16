using System;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using ESP.Compatible;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

namespace FinanceWeb.ExpenseAccount
{
    public partial class TicketDlg : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.ExpenseAccount.TicketDlg));

            if (!IsPostBack)
            {
                ESP.Compatible.Department d = ESP.Compatible.DepartmentManager.GetDepartmentByPK(CurrentUser.GetDepartmentIDs()[0]);

                string canUseOtherBoarders = System.Configuration.ConfigurationManager.AppSettings["CanUseOtherBoarders"];

                if (canUseOtherBoarders.IndexOf("," + CurrentUserID.ToString() + ",") >= 0)
                {
                    this.tbBoarderOthers.Visible = true;
                }

                if (d.Level == 1)
                {
                    hidtype.Value = d.UniqID.ToString();
                }
                else if (d.Level == 2)
                {
                    hidtype1.Value = d.UniqID.ToString();
                    hidtype.Value = d.Parent.UniqID.ToString();
                }
                else if (d.Level == 3)
                {
                    hidtype2.Value = d.UniqID.ToString();
                    hidtype1.Value = d.Parent.UniqID.ToString();
                    hidtype.Value = d.Parent.Parent.UniqID.ToString();
                }

                this.hidGroupName.Value = d.DepartmentName;

                IList<Department> deparmentsOfUserID = Employee.GetDepartments(int.Parse(CurrentUser.SysID));
                int[] depts = new int[deparmentsOfUserID.Count];
                for (int i = 0; i < deparmentsOfUserID.Count; i++)
                {
                    Department d1 = deparmentsOfUserID[i];
                    depts[i] = d1.UniqID;
                }
                if (depts != null && depts.Length > 0)
                {
                    Department d2 = DepartmentManager.GetDepartmentByPK(depts[0]);
                    if (d2.Level == 1)
                    {
                        hidtype.Value = d2.UniqID.ToString();
                    }
                    else if (d2.Level == 2)
                    {
                        hidtype1.Value = d2.UniqID.ToString();
                        hidtype.Value = d2.Parent.UniqID.ToString();
                    }
                    else if (d2.Level == 3)
                    {
                        hidtype2.Value = d2.UniqID.ToString();
                        hidtype1.Value = d2.Parent.UniqID.ToString();
                        hidtype.Value = d2.Parent.Parent.UniqID.ToString();
                    }
                }
            }

            if (!IsPostBack)
            {
                DepartmentDataBind();
                gvDataBind();
                bindOtherUsers();
            }

        }

        private void bindOtherUsers()
        {
            string where = " userid=" + CurrentUser.SysID;

            IList<ESP.Finance.Entity.ExpenseBoarderInfo> modellist = ESP.Finance.BusinessLogic.ExpenseBoarderManager.GetList(where);
            this.gvUsers.DataSource = modellist;
            this.gvUsers.DataBind();
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            // this.panEmp.Visible = true;

        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            // this.panEmp.Visible = false;

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            ESP.Finance.Entity.ExpenseBoarderInfo boader =new ExpenseBoarderInfo();
            boader.Boarder=this.txtBoarder.Text;
            boader.CardNo=this.txtID.Text;
            boader.CardType = this.ddlIDType.Text;
            boader.Mobile=this.txtPhone.Text;
            boader.UserId = int.Parse(CurrentUser.SysID);
            ESP.Finance.BusinessLogic.ExpenseBoarderManager.Add(boader);
            add(this.txtBoarder.Text, this.txtID.Text, this.txtPhone.Text, this.ddlIDType.Text);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            // this.panEmp.Visible = false;
            gvDataBind();
            bindOtherUsers();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;

                Label lblPhone = (Label)e.Row.FindControl("lblPhone");
                if (lblPhone != null)
                    lblPhone.Text = StringHelper.FormatPhoneLastChar(lblPhone.Text);
            }

        }


        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int userId = int.Parse(this.gv.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                add(userId);
            }
           
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(this.gvUsers.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
            ESP.Finance.Entity.ExpenseBoarderInfo boarder = ESP.Finance.BusinessLogic.ExpenseBoarderManager.GetModel(id);
            if (e.CommandName == "Add")
            {
                add(boarder.Boarder, boarder.CardNo, boarder.Mobile, boarder.CardType);
            }
            if (e.CommandName == "BoarderEdit")
            {
                string script = "document.getElementById('ctl00_ContentPlaceHolder1_trAdd').style.display='block';";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
                this.txtBoarder.Text = boarder.Boarder;
                this.txtID.Text = boarder.CardNo;
                this.txtPhone.Text = boarder.Mobile;
            }
        }

        private void DepartmentDataBind()
        {
            object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
            ddltype.DataSource = dt;
            ddltype.DataTextField = "NodeName";
            ddltype.DataValueField = "UniqID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
        }

        private void gvDataBind()
        {
            string value = txtName.Text.Trim();
            int[] depids = null;
            List<Department> dlist;
            string typevalue = null;

            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                typevalue = hidtype2.Value;
            }
            else if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                typevalue = hidtype1.Value;
            }
            else if (hidtype.Value != "" && hidtype.Value != "-1")
            {
                typevalue = hidtype.Value;
            }
            else
            {
            }

            if (typevalue != null && typevalue.Length != 0)
            {
                int selectedDep = int.Parse(typevalue);
                dlist = GetLeafChildDepartments(selectedDep);
                if (dlist != null && dlist.Count > 0)
                {
                    depids = new int[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        depids[i] = dlist[i].UniqID;
                    }
                }
                else
                {
                    depids = new int[] { selectedDep };
                }
            }

            string selectedusers = GetUserSelected(Convert.ToInt32(Request[RequestName.ProjectID]));
            DataSet ds;
            if (!string.IsNullOrEmpty(selectedusers))
                ds = ESP.Compatible.Employee.GetDataSetUserByKey(value, depids, " and e.userid not in(" + selectedusers + ")");
            else
                ds = ESP.Compatible.Employee.GetDataSetUserByKey(value, depids);

            gv.DataSource = ds;
            gv.DataBind();

        }

        private string GetUserSelected(int projectID)
        {
            string UserSelected = string.Empty;
            IList<ESP.Finance.Entity.ProjectMemberInfo> projectMember = ESP.Finance.BusinessLogic.ProjectMemberManager.GetListByProject(projectID, null, null);
            if (projectMember != null && projectMember.Count > 0)
            {
                foreach (ESP.Finance.Entity.ProjectMemberInfo m in projectMember)
                {
                    UserSelected += m.MemberUserID.ToString() + ",";
                }
            }

            IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(projectID, null, null);
            if (supporterList != null && supporterList.Count > 0)
            {
                foreach (ESP.Finance.Entity.SupporterInfo sup in supporterList)
                {
                    IList<ESP.Finance.Entity.SupportMemberInfo> supMember = ESP.Finance.BusinessLogic.SupportMemberManager.GetList("SupportID=" + sup.SupportID.ToString());
                    if (supMember != null && supMember.Count > 0)
                    {
                        foreach (ESP.Finance.Entity.SupportMemberInfo mem in supMember)
                        {
                            UserSelected += mem.MemberUserID.Value.ToString() + ",";
                        }
                    }
                }
            }

            return UserSelected.TrimEnd(',');
        }

        protected void add(int userId)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo employee = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(userId);
            ESP.Compatible.Employee emp = new Employee(userId);
            string clientId = "ctl00_ContentPlaceHolder1_";
            string script = "opener.document.getElementById('" + clientId + "lblBoarder').innerHTML= '" + emp.Name + "';";
            script += "opener.document.getElementById('" + clientId + "hidIDCard').value= '" + employee.IDNumber + "';";
            script += "opener.document.getElementById('" + clientId + "hidBoarder').value= '" + emp.Name + "';";
            script += "opener.document.getElementById('" + clientId + "txtPhone').value= '" + employee.MobilePhone + "';";
            script += "opener.document.getElementById('" + clientId + "hidBoarderId').value= '" + employee.UserID + "';";
            script += @"window.close();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }

        protected void add(string boarder, string id, string mobile,string cardtype)
        {

            string clientId = "ctl00_ContentPlaceHolder1_";
            string script = "opener.document.getElementById('" + clientId + "lblBoarder').innerHTML= '" + boarder + "';";
            script += "opener.document.getElementById('" + clientId + "hidIDCard').value= '" + id + "';";
            script += "opener.document.getElementById('" + clientId + "hidBoarder').value= '" + boarder + "';";
            script += "opener.document.getElementById('" + clientId + "txtPhone').value= '" + mobile + "';";
            script += "opener.document.getElementById('" + clientId + "hidBoarderId').value= '0';";
            script += "opener.document.getElementById('" + clientId + "hidCardType').value= '"+cardtype+"';";
            script += @"window.close();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> getalist(int parentId)
        {
            List<List<string>> list = new List<List<string>>();
            try
            {

                list = ESP.Compatible.DepartmentManager.GetListForAJAX(parentId);
            }
            catch (Exception e)
            {
                e.ToString();
            }

            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("请选择...");
            list.Insert(0, c);
            return list;
        }

    }
}
