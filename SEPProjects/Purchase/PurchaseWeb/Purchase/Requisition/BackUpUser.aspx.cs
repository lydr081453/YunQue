using System;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

public partial class Purchase_BackUpUser : ESP.Web.UI.PageBase
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
        //labCurrentUser.Text = CurrentUser.Name;
        //AuditBackUpInfo model = AuditBackUpManager.GetModelByUserId(int.Parse(CurrentUser.SysID));
        //if (model != null)
        //{
        //    hidBackUserId.Value = model.backupUserId.ToString();
        //    ESP.Compatible.Employee emp = new ESP.Compatible.Employee(model.backupUserId);
        //    txtBackUpUser.Text = emp == null ? "" : emp.Name;
        //    txtBegin.Text = model.BeginDate;
        //    txtEnd.Text = model.EndDate;
        //    BackUpId = model.id;
        //}
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        AuditBackUpInfo model = new AuditBackUpInfo();
        //if (BackUpId > 0)
        //    model = AuditBackUpManager.GetModel(BackUpId);
        model.BackupUserID = int.Parse(hidBackUpUser.Value);
        model.BeginDate = DateTime.Parse(txtBegin.Text);
        model.EndDate = DateTime.Parse(txtEnd.Text);
        model.UserID = int.Parse(hidOldUser.Value);
        model.Status = 1;
        model.Type = 1;

        int resultCount = 0;
        //if (BackUpId > 0)
        //{
        //    resultCount = AuditBackUpManager.Update(model);
        //}
        //else
        //{
        AuditBackUpManager.DeleteByUserId(model.UserID);
            resultCount = AuditBackUpManager.Add(model);
        //}
        InitPage();
        if (resultCount > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！')", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！')", true);
        }
    }
}
