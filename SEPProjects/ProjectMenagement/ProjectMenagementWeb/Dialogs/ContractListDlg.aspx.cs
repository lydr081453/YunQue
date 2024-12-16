using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using System.Configuration;

public partial class Dialogs_ContractListDlg : System.Web.UI.Page
{
    private static string term = string.Empty;
    //private static List<SqlParameter> paramlist = null;
    private string uniqueContractId = string.Empty;
    private int contractID=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        uniqueContractId = "ctl00$ContentPlaceHolder1$ProjectInfo$";
        if (!IsPostBack)
        {         
            BindExistContract();
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ContractID]))
            {
                contractID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ContractID]);
                BindContract(contractID);
            }
        }
    }

    private void BindContract(int contractid)
    {
        ESP.Finance.Entity.ContractInfo contract = ESP.Finance.BusinessLogic.ContractManager.GetModel(contractid);
        this.txtContractDescription.Text = contract.Description;
        this.hypAttach.Visible = true;

        hypAttach.ToolTip = "下载附件：" + contract.Attachment;
        this.hypAttach.NavigateUrl = "~/Dialogs/ContractFileDownLoad.aspx?ContractID=" + contract.ContractID.Value.ToString();
        //this.txtRemark.Text = contract.Remark;
        //this.txtTotalAmount.Text = contract.TotalAmounts.ToString("#,##0.00");
        if (contract.ParentID != null && contract.ParentID.Value!=0)
        {
            ESP.Finance.Entity.ContractInfo parentContract = ESP.Finance.BusinessLogic.ContractManager.GetModel(contract.ParentID.Value);
            if (parentContract != null)
            {
                ListItem item = new ListItem(parentContract.Description, parentContract.ContractID.ToString());
                this.ddlOldContract.Items.Add(item);
            }
            this.ddlOldContract.SelectedValue = contract.ParentID.ToString();
        }
    }
    private void BindExistContract()
    {
        this.ddlOldContract.Items.Clear();
        ListItem item = new ListItem("请选择", "-1");
        ddlOldContract.Items.Add(item);
        ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        string term = " Usable = @Usable and Status is null";
        SqlParameter p = new SqlParameter("@Usable",SqlDbType.Bit,1);
        p.Value = "True";
        List<SqlParameter> pm = new List<SqlParameter>();
        pm.Add(p);
        
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ContractID]))
        {
            term += " and ContractID!=@ContractID";
            SqlParameter p2 = new SqlParameter("@ContractID",SqlDbType.Int,4);
            p2.Value = Request[ESP.Finance.Utility.RequestName.ContractID];
            pm.Add(p2);
        }
        IList<ContractInfo> contractlist = ESP.Finance.BusinessLogic.ContractManager.GetListByProject(Convert.ToInt32(project.ProjectId), term, pm);
        foreach (ContractInfo contract in contractlist)
        {
            ListItem contractItem = new ListItem(contract.Description, contract.ContractID.ToString());
            this.ddlOldContract.Items.Add(contractItem);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    { }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close();", true);
    }

    protected void btnSaveContract_Click(object sender, EventArgs e)
    {
        if (SaveContract() > 0)
        {
            string script = string.Empty;
            script = @"var uniqueId = 'ctl00$ContentPlaceHolder1$ProjectInfo$';
opener.__doPostBack(uniqueId + 'btnContract', '');
window.close(); ";

            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }
    }

    private void CreateContract(ProjectInfo projectinfo, string fileName)
    {
        ContractInfo contract =null;
        if(!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ContractID]))
           contract=ESP.Finance.BusinessLogic.ContractManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ContractID]));
        else
           contract = new ContractInfo();

        contract.ProjectID = projectinfo.ProjectId;
        contract.projectCode = projectinfo.ProjectCode;
        contract.Description = txtContractDescription.Text.Trim();
        //contract.TotalAmounts = Convert.ToDecimal(this.txtTotalAmount.Text.Trim());
        contract.IsDelay = this.chkDelay.Checked;

        if (chkDouble.Checked == true)
        {
            contract.ContractType = 1;
        }

        if (fileName!=string.Empty)
             contract.Attachment = fileName;
        if (this.ddlOldContract.SelectedIndex != 0)
        {
            contract.ParentID = Convert.ToInt32(this.ddlOldContract.SelectedValue);
            ESP.Finance.BusinessLogic.ContractManager.ReplaceContract(contract);//替换合同
        }
        else
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ContractID]))//编辑合同
                ESP.Finance.BusinessLogic.ContractManager.Update(contract);
            else
                ESP.Finance.BusinessLogic.ContractManager.Add(contract);//新增合同
        }
        this.txtContractDescription.Text = string.Empty;
        //this.txtTotalAmount.Text = "0.00";
        contract.Remark = string.Empty;



    }

    private int SaveContract()
    {
        decimal totalAmounts=0;
        int originalContractID = 0;
        ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        if (this.ddlOldContract.SelectedIndex != 0)
        {
            originalContractID = Convert.ToInt32(this.ddlOldContract.SelectedValue);
        }
        if (!string.IsNullOrEmpty(Request[RequestName.ContractID]))
        {
            totalAmounts = ESP.Finance.BusinessLogic.ContractManager.GetOddAmountByProject(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]), Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ContractID]), originalContractID);
        }
        else
        {
            totalAmounts = ESP.Finance.BusinessLogic.ContractManager.GetTotalAmountByProject(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]), originalContractID);
        }

        //if (totalAmounts + Convert.ToDecimal(this.txtTotalAmount.Text.Trim()) > project.TotalAmount.Value)
        //{
        //    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('合同总金额与项目号申请总金额不符!');", true);
        //    return -1;
        //}
        //else
        //{
            if (this.fileupContract.Visible)
            {
                if (this.fileupContract.FileName != string.Empty)
                {
                    string fileName = "Contract_" + Guid.NewGuid().ToString() + "_" + this.fileupContract.FileName;
                    
                    this.fileupContract.SaveAs(ESP.Finance.Configuration.ConfigurationManager.ContractPath + fileName);
                    CreateContract(project, fileName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ContractID]))
                    {
                        CreateContract(project, string.Empty);
                    }
                }
            }
        //}
        return 1;
    }

}