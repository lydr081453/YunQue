using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Branch_BranchInfo : System.Web.UI.UserControl
{
    private int _branchid;

    public int Branch_Id
    {
        get { return _branchid; }
        set { _branchid = value; }
    }

    private ESP.Finance.Entity.BranchInfo branchinfo;

    public ESP.Finance.Entity.BranchInfo BranchModel
    {
        get { return branchinfo; }
        set { branchinfo = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitBranchInfo();
        }
    }


    public void InitBranchInfo()
    {
        if (_branchid > 0)//
        {
            branchinfo = ESP.Finance.BusinessLogic.BranchManager.GetModel(_branchid);
        }
        else
        {
            return;
        }
        if (branchinfo != null)
        {

            txtBranchName.Text = branchinfo.BranchName;
            hidBranchCode.Value = branchinfo.BranchCode.ToString();
            hidBranchID.Value = branchinfo.BranchID.ToString();
        }
    }

    public void setBranchInfo()
    {

        if (_branchid > 0)//
        {
            branchinfo = ESP.Finance.BusinessLogic.BranchManager.GetModel(_branchid);
        }
        if (branchinfo == null)
            branchinfo = new ESP.Finance.Entity.BranchInfo();

        if (hidBranchID.Value.Length > 0)
        {
            branchinfo.BranchID = Convert.ToInt32(hidBranchID.Value);
        }

        if (hidBranchCode.Value.Length > 0)
        {
            branchinfo.BranchCode = hidBranchCode.Value;
        }

        if (txtBranchName.Text.Trim().Length > 0)
        {
            branchinfo.BranchName = txtBranchName.Text;
        }
    }


}
