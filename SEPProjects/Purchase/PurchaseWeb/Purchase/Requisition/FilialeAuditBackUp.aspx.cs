using System;
using ESP.Purchase.BusinessLogic;

public partial class Purchase_Requisition_FilialeAuditBackUp : ESP.Web.UI.PageBase
{
    static int BackUpId = 0;    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BackUpId = 0;
            InitPage();
        }
    }

    /// <summary>
    /// Inits the page.
    /// </summary>
    private void InitPage()
    {        
        labCurrentUser.Text = CurrentUser.Name;
        ESP.Purchase.Entity.FilialeAuditBackUpInfo model = FilialeAuditBackUpManager.GetModelByUserid(CurrentUserID);
        if (model != null)
        {
            if (model.isBackupUser > 0)
                chk.Checked = true;            
            BackUpId = model.id;            
        }       
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ESP.Purchase.Entity.FilialeAuditBackUpInfo model = new ESP.Purchase.Entity.FilialeAuditBackUpInfo();
        if (BackUpId > 0)
            model = FilialeAuditBackUpManager.GetModel(BackUpId);

        model.isBackupUser = chk.Checked ? 1 : 0;
        model.userid = CurrentUserID;

        int resultCount = 0;
        if (BackUpId > 0)
        {
            resultCount = FilialeAuditBackUpManager.Update(model);
        }
        else
        {
            resultCount = FilialeAuditBackUpManager.Add(model);
        }
        InitPage();
        if (resultCount > 0)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('保存成功！')", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('保存失败！')", true);
        }
    }
    
}
