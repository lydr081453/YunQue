using System;
using ESP.Purchase.Entity;

public partial class UserControls_View_genericInfo : System.Web.UI.UserControl
{
    private GeneralInfo model;
    public GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindInfo()
    {
        ESP.HumanResource.Entity.EmployeesInPositionsInfo reqPosition = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.requestor);
        ESP.Finance.Entity.DepartmentViewInfo reqDept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(reqPosition.DepartmentID);

        ESP.HumanResource.Entity.EmployeesInPositionsInfo receiverPosition = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.goods_receiver);
        ESP.Finance.Entity.DepartmentViewInfo receiverDept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(receiverPosition.DepartmentID);

        ESP.HumanResource.Entity.EmployeesInPositionsInfo appendPosition = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.appendReceiver);
        ESP.Finance.Entity.DepartmentViewInfo appendDept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(appendPosition.DepartmentID);
        
        ESP.HumanResource.Entity.EmployeesInPositionsInfo endPosition = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.enduser);
        ESP.Finance.Entity.DepartmentViewInfo endDept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(endPosition.DepartmentID);

        txtappdate.Text = Model.app_date.ToString();
        txtrequeator.Text = Model.requestorname;
        txtrequeator.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Model.requestor) + "');";
        txtrequestor_info.Text = Model.requestor_info;
        txtrequestor_group.Text = reqDept.level1 + "-" + reqDept.level2 + "-" + reqDept.level3;
        labenduser.Text = Model.endusername;
        labenduser.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Model.enduser) + "');";
        txtenduser_info.Text = Model.enduser_info;
        txtenduser_group.Text = endDept.level1 + "-" + endDept.level2 + "-" + endDept.level3;
        labgoods_receiver.Text = Model.receivername;
        labgoods_receiver.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Model.goods_receiver) + "');";
        txtreceiver_info.Text = Model.receiver_info;
        txtship_address.Text = Model.ship_address;
        txtReceiverGroup.Text = receiverDept.level1 + "-" + receiverDept.level2 + "-" + receiverDept.level3;
        txtreceiver_Otherinfo.Text = model.receiver_Otherinfo;
        txtappendReceiver.Text = Model.appendReceiverName;
        txtappendReceiver.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Model.appendReceiver) + "');";
        txtAppendInfo.Text = Model.appendReceiverInfo;
        txtappendReceiverGroup.Text = appendDept.level1 + "-" + appendDept.level2 + "-" + appendDept.level3;
    }
}
