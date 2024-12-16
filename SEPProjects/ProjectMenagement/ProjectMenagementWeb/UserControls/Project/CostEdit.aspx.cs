using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Project_CostEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<ESP.Finance.Entity.CostTypeViewInfo> typelist = ESP.Finance.BusinessLogic.CostTypeViewManager.GetList(" a.parentid<>0");
            List<ESP.Finance.Entity.ContractCostInfo> costlist = new List<ESP.Finance.Entity.ContractCostInfo>();
            foreach (ESP.Finance.Entity.CostTypeViewInfo type in typelist)
            {
                ESP.Finance.Entity.ContractCostInfo cost = new ESP.Finance.Entity.ContractCostInfo();
                cost.Description = type.TypeName;
                cost.CostTypeID = type.TypeID;
                costlist.Add(cost);
            }
            this.gvEdit.DataSource = costlist;
            this.gvEdit.DataBind();
        }
    }

    protected void gvEdit_RowDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.Header)
        {

        }
        if (e.Item.ItemType == ListItemType.Item)
        {
            ESP.Finance.Entity.ContractCostInfo item = (ESP.Finance.Entity.ContractCostInfo)e.Item.DataItem;
            Label lblMaterial = (Label)e.Item.FindControl("lblMaterial");
            lblMaterial.Text = item.Description;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem di in this.gvEdit.Items)
        {
            if (((CheckBox)di.FindControl("chkSelect")).Checked == true)
            {
                ((TextBox)di.FindControl("txtCost")).Visible = true;
                ((TextBox)di.FindControl("txtRemark")).Visible = true;
                ((Label)di.FindControl("lblCost")).Visible = false;
                ((Label)di.FindControl("lblRemark")).Visible = false;

            }
        }
    }

    protected void gvEdit_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            CheckBox chk = (CheckBox)e.Item.FindControl("chkSelect");
            chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
            
        }

    }

    protected void gvEdit_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0f0f0';this.style.color='buttontext';this.style.cursor='hand'");

        }
        if (e.Item.ItemType == ListItemType.Item)
        {
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        }
        else
        {
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#fcfcfc'");
        }
    }

   protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        DataGridItem di = (DataGridItem)chk.Parent.Parent;

        TextBox txtCost = ((TextBox)di.FindControl("txtCost"));
        TextBox txtRemark = ((TextBox)di.FindControl("txtRemark"));
        Label lblCost = ((Label)di.FindControl("lblCost"));
        Label lblRemark = ((Label)di.FindControl("lblRemark"));

        if (chk.Checked==true)
        {
            txtCost.Text = lblCost.Text;
            txtCost.Visible = true;
            lblCost.Visible = false;
            txtRemark.Text = lblRemark.Text;
            txtRemark.Visible = true;
            lblRemark.Visible = false;
        }
        else
        {
            txtCost.Visible = false;
            txtRemark.Visible = false;
            lblCost.Visible = true;
            ((Label)di.FindControl("lblRemark")).Visible = true;
        }
    }
}
