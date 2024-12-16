using System;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using ESP.Purchase.Common;
using System.Collections;

public partial class UserControls_Edit_genericInfo : System.Web.UI.UserControl
{
    #region 控件属性
    private GeneralInfo model;
    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>The model.</value>
    public GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }

    public string _enduser = string.Empty;
    /// <summary>
    /// Gets or sets the end user.
    /// </summary>
    /// <value>The end user.</value>
    public string EndUser
    {
        get { return _enduser; }
        set
        {
            _enduser = value;
            if (!string.IsNullOrEmpty(_enduser))
            {
                this.txtenduser.Text = _enduser;
            }
        }
    }

    public string _enduserid = string.Empty;
    /// <summary>
    /// Gets or sets the end user ID.
    /// </summary>
    /// <value>The end user ID.</value>
    public string EndUserID
    {
        get { return _enduserid; }
        set
        {
            _enduserid = value;
            if (!string.IsNullOrEmpty(_enduserid))
            {
                this.hidenduser.Value = _enduserid;

            }
        }
    }

    private string _enduserdept = string.Empty;
    /// <summary>
    /// Gets or sets the end user dept.
    /// </summary>
    /// <value>The end user dept.</value>
    public string EndUserDept
    {
        get { return _enduserdept; }
        set
        {
            _enduserdept = value;
            if (!string.IsNullOrEmpty(_enduserdept))
            {
                this.txtenduser_group.Text = _enduserdept;

            }
        }
    }

    private string _enduserphone = string.Empty;
    /// <summary>
    /// Gets or sets the end user phone.
    /// </summary>
    /// <value>The end user phone.</value>
    public string EndUserPhone
    {
        get { return _enduserphone; }
        set
        {
            _enduserphone = value;
            if (!string.IsNullOrEmpty(_enduserphone))
            {
                try
                {
                    this.txtenduser_con.Text = _enduserphone;
                    //txtenduser_area.Text = _enduserphone.Split('-')[1];
                    //txtenduser_phone.Text = _enduserphone.Split('-')[2];
                    //txtenduser_Ext.Text = _enduserphone.Split('-')[3];
                }
                catch
                {
                   // txtenduser_phone.Text = _enduserphone;
                }
            }
        }
    }

    private ESP.Compatible.Employee currentUser;
    /// <summary>
    /// Gets or sets the current user.
    /// </summary>
    /// <value>The current user.</value>
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }
    #endregion 控件属性

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
        }
    }

    /// <summary>
    /// Binds the info.
    /// </summary>
    public void BindInfo()
    {
        ESP.Framework.Entity.OperationAuditManageInfo operation = null;
        ESP.Framework.Entity.OperationAuditManageInfo userOperation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(Model.requestor);
        if (userOperation == null)
        {
            operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(Model.Departmentid);
        }
        else
        {
            operation = userOperation;
        }
        if (operation.AppendReceiverId != 0)
        {
            btnAppendReceiver.Enabled = false;
            txtappendReceiver.Enabled = false;
            txtAppen_con.Enabled = false;
            txtappendReceiverGroup.Enabled = false;
        }

        ddlrequestor.Text = Model.requestorname;
        txtrequestordate.Text = Model.app_date.ToString();
        txtrequestor_info.Text = Model.requestor_info;
        txtrequestor_group.Text = Model.requestor_group;

        if (Model.enduser > 0)
        {

            txtenduser.Text = Model.endusername;
            hidenduser.Value = Model.enduser.ToString() + "-" + Model.endusername;
            try
            {
                txtenduser_con.Text = Model.enduser_info;
                //txtenduser_area.Text = Model.enduser_info.Split('-')[1];
                //txtenduser_phone.Text = Model.enduser_info.Split('-')[2];
                //txtenduser_Ext.Text = Model.enduser_info.Split('-')[3];
            }
            catch
            { }
            txtenduser_group.Text = Model.enduser_group;

        }
        else
        {
            txtenduser.Text = Model.requestorname;
            hidenduser.Value = Model.requestor.ToString() + "-" + Model.requestorname;
            txtenduser_con.Text = Model.requestor_info;
            txtenduser_group.Text = Model.requestor_group;
        }

        if (Model.goods_receiver > 0)
        {

            txtgoods_receiver.Text = Model.receivername;
            hidreceiver.Value = Model.goods_receiver.ToString() + "-" + Model.receivername;
            try
            {
                txtreceiver_con.Text = Model.receiver_info;
                //txtreceiver_area.Text = Model.receiver_info.Split('-')[1];
                //txtreceiver_phone.Text = Model.receiver_info.Split('-')[2];
                //txtreceiver_Ext.Text = Model.receiver_info.Split('-')[3];
            }
            catch
            {
            }
        }
        else
        {
            txtgoods_receiver.Text = Model.requestorname;
            hidreceiver.Value = Model.requestor.ToString() + "-" + Model.requestorname;
            txtreceiver_con.Text = Model.requestor_info;
            txtReceiverGroup.Text = Model.requestor_group;
        }


        txtship_address.Text = Model.ship_address;
        txtReceiverGroup.Text = Model.ReceiverGroup;
        txtreceiver_Otherinfo.Text = Model.receiver_Otherinfo;
        if (Model.appendReceiver > 0)
        {

            txtappendReceiver.Text = Model.appendReceiverName;
            hidappendReceiver.Value = Model.appendReceiver.ToString() + "-" + Model.appendReceiverName;
            try
            {
                txtAppen_con.Text = Model.appendReceiverInfo;
                //txtAppen_area.Text = Model.appendReceiverInfo.Split('-')[1];
                //txtAppen_phone.Text = Model.appendReceiverInfo.Split('-')[2];
                //txtAppen_Ext.Text = Model.appendReceiverInfo.Split('-')[3];
            }
            catch
            { }
            txtappendReceiverGroup.Text = Model.appendReceiverGroup;

        }
        else
        {
            if (operation.AppendReceiverId != 0)
            {
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(operation.AppendReceiverId);
                txtappendReceiver.Text = operation.AppendReceiver;
                hidappendReceiver.Value = operation.AppendReceiverId.ToString() + "-" + operation.AppendReceiver;

                try
                {
                    txtAppen_con.Text = emp.Telephone ;
                }
                catch
                { }
                System.Collections.Generic.IList<string> deptlist = emp.GetDepartmentNames();
                string group = deptlist.Count == 0 ? "" : deptlist[0].ToString();

                txtappendReceiverGroup.Text = group;
            }
        }
    }

    protected void btnUpdateGeneralInfo_Click(object sender, EventArgs e)
    {
        model = GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
        model = setModelInfo();
        GeneralInfoManager.Update(model);
        BindInfo();
    }

    /// <summary>
    /// Sets the model info.
    /// </summary>
    /// <returns></returns>
    public GeneralInfo setModelInfo()
    {
        string[] enduser = hidenduser.Value.Split('-');
        if (enduser.Length > 1)
        {
            model.enduser = int.Parse(enduser[0]);
            model.endusername = enduser[1];
        }

        //2008.9.5周奇修改
        string enduser_con = txtenduser_con.Text.Trim();

        Model.enduser_info = enduser_con ;
        Model.enduser_group = txtenduser_group.Text.Trim();

        string[] receiver = hidreceiver.Value.Split('-');
        if (receiver.Length > 1)
        {
            Model.goods_receiver = int.Parse(receiver[0]);
            Model.receivername = receiver[1];
        }
        string receiver_con = txtreceiver_con.Text.Trim();

        Model.receiver_info = receiver_con;
        Model.ReceiverGroup = txtReceiverGroup.Text.Trim();
        Model.ship_address = txtship_address.Text.Trim();
        Model.receiver_Otherinfo = txtreceiver_Otherinfo.Text.Trim();

        string[] append = hidappendReceiver.Value.Split('-');
        if (append.Length > 1)
        {
            Model.appendReceiver = int.Parse(append[0]);
            Model.appendReceiverName = append[1];
        }
        string append_con = txtAppen_con.Text.Trim();

        Model.appendReceiverInfo = append_con;
        Model.appendReceiverGroup = txtappendReceiverGroup.Text.Trim();
        return Model;
    }
}