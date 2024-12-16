using ESP.HumanResource.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmpContractEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userid = 0;

                IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
                if (branchList != null && branchList.Count > 0)
                {
                    drpContract_Company.DataSource = branchList;
                    drpContract_Company.DataTextField = "BranchName";
                    drpContract_Company.DataValueField = "BranchCode";
                    drpContract_Company.DataBind();
                    drpContract_Company.Width = 300;
                    txtBranch.Text = drpContract_Company.SelectedValue;
                }

                if (!String.IsNullOrEmpty(Request["contractid"]))
                {
                    int contractid = int.Parse(Request["contractid"]);
                    ESP.HumanResource.Entity.EmpContractInfo model = ESP.HumanResource.BusinessLogic.EmpContractManager.GetModel(contractid);
                    this.txtBegin.Text = model.BeginDate.ToString("yyyy-MM-dd");
                    this.txtEnd.Text = model.EndDate.ToString("yyyy-MM-dd");
                    this.txtBranch.Text = model.Branch;
                    drpContract_Company.SelectedValue = model.Branch;
                    txtBranch.Text = model.Branch;
                    this.txtSignDate.Text = model.SignDate.ToString("yyyy-MM-dd");

                    userid = model.UserId;
                }
                else
                {
                     userid = int.Parse(Request["userid"]);

                     ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(userid);

                    IList<ESP.HumanResource.Entity.EmpContractInfo> contractMainlist = ESP.HumanResource.BusinessLogic.EmpContractManager.GetList(" userid=" + userid + " and status=1");

                    if (contractMainlist != null && contractMainlist.Count > 0)
                    {
                        ESP.HumanResource.Entity.EmpContractInfo contractmodel = contractMainlist[0];

                        txtBegin.Text = contractmodel.EndDate.AddDays(1).ToString("yyyy-MM-dd");

                        txtEnd.Text = contractmodel.EndDate.AddYears(3).ToString("yyyy-MM-dd");

                        txtSignDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                        drpContract_Company.SelectedValue = contractmodel.Branch;
                        txtBranch.Text = contractmodel.Branch;

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.BranchCode))
                        {
                            drpContract_Company.SelectedValue = model.BranchCode;
                            txtBranch.Text = model.BranchCode;

                        }
                    }
                }

                  
            
            }
        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(Request["contractid"]))
        //    {
        //        int contractid = int.Parse(Request["contractid"]);
        //        ESP.HumanResource.Entity.EmpContractInfo model = ESP.HumanResource.BusinessLogic.EmpContractManager.GetModel(contractid);
        //        ESP.HumanResource.BusinessLogic.EmpContractManager.Delete(contractid);

        //        string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

        //        ClientScript.RegisterStartupScript(typeof(string), "", str, true);

        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = 0;
            ESP.HumanResource.Entity.EmployeeBaseInfo empModel = null;

            if (String.IsNullOrEmpty(Request["contractid"]))
            {
                userid = int.Parse(Request["userid"]);
                empModel = EmployeeBaseManager.GetModel(userid);

                ESP.HumanResource.Entity.EmpContractInfo model = new ESP.HumanResource.Entity.EmpContractInfo();
                model.UserId = userid;
                model.BeginDate =string.IsNullOrEmpty(txtBegin.Text)?DateTime.Now: Convert.ToDateTime(txtBegin.Text);
                model.EndDate =string.IsNullOrEmpty(txtEnd.Text)?DateTime.Now: Convert.ToDateTime(txtEnd.Text);
                model.Branch = this.txtBranch.Text;
                model.SignDate = string.IsNullOrEmpty(this.txtSignDate.Text)?DateTime.Now:Convert.ToDateTime(this.txtSignDate.Text);
                model.Status = 1;

                ESP.HumanResource.BusinessLogic.EmpContractManager.UpdateStatus(-1, " userid= "+ userid);
                ESP.HumanResource.BusinessLogic.EmpContractManager.Add(model);

                empModel.BranchCode = model.Branch;
                empModel.ContractBeginDate = model.BeginDate;
                empModel.ContractEndDate = model.EndDate;
            }
            else
            {
                int contractid = int.Parse(Request["contractid"]);
                ESP.HumanResource.Entity.EmpContractInfo model = ESP.HumanResource.BusinessLogic.EmpContractManager.GetModel(contractid);
                empModel = EmployeeBaseManager.GetModel(model.UserId);
                model.BeginDate = string.IsNullOrEmpty(txtBegin.Text) ? DateTime.Now : Convert.ToDateTime(txtBegin.Text);
                model.EndDate = string.IsNullOrEmpty(txtEnd.Text) ? DateTime.Now : Convert.ToDateTime(txtEnd.Text);
                model.Branch = this.txtBranch.Text;
                model.SignDate = string.IsNullOrEmpty(this.txtSignDate.Text) ? DateTime.Now : Convert.ToDateTime(this.txtSignDate.Text);


                ESP.HumanResource.BusinessLogic.EmpContractManager.Update(model);
                userid = model.UserId;

                empModel.BranchCode = model.Branch;
                empModel.ContractBeginDate = model.BeginDate;
                empModel.ContractEndDate = model.EndDate;
                
            }

            ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(empModel);

            string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

            ClientScript.RegisterStartupScript(typeof(string), "", str, true);

        }
    }
}
