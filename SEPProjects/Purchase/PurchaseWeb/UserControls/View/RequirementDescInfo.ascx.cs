using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;

public partial class UserControls_View_RequirementDescInfo : System.Web.UI.UserControl
{
    private bool editRequisitionFlow = false;
    /// <summary>
    /// 是否可以编辑审批流向
    /// </summary>
    public bool EditRequisitionFlow
    {
        get { return editRequisitionFlow; }
        set { editRequisitionFlow = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 设置对象信息
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public void setModelInfo(ESP.Purchase.Entity.GeneralInfo g)
    {
        g.Requisitionflow = !string.IsNullOrEmpty(rblrequisitionflow.SelectedValue) ? int.Parse(rblrequisitionflow.SelectedValue) : 0;
    }

    public void BindInfo(ESP.Purchase.Entity.GeneralInfo g)
    {
        radioBind();
        if (EditRequisitionFlow)
        {
            rblrequisitionflow.Visible = true;
            labrequisitionflow.Visible = false;
        }
        txtsow.Text = g.sow;
        labdownSow.Text = g.sow2 == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + g.id.ToString() + "&Index=0&Type=Sow2'><img src='/images/ico_04.gif' border='0' /></a>";
        labrequisitionflow.Text = ESP.Purchase.Common.State.requisitionflow_state[g.Requisitionflow];
        rblrequisitionflow.SelectedValue = g.Requisitionflow.ToString();
    }

    public void radioBind()
    {
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toFC], State.requisitionflow_toFC.ToString()));
        rblrequisitionflow.SelectedValue = State.requisitionflow_toO.ToString();
    }
}