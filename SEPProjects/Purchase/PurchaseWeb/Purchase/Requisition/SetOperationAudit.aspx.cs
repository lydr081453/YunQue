using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;

public partial class Purchase_Requisition_SetOperationAudit : ESP.Purchase.WebPage.EditPageForPR
{
    protected void Page_Load(object sender, EventArgs e)
    {

        newSetAuditor.CurrentUser = CurrentUser;
    }
}
