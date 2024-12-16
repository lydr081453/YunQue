using System;
using ESP.Purchase.Common;

public partial class Purchase_Requisition_AddRequisitionStep4 : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;
    string query = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
            productInfo.CurrentUserId = CurrentUserID;
        }
        if (!IsPostBack)
        {
            BindInfo();
          //  productInfo.ItemListBind(" general_id = " + generalid);
        }

        query = Request.Url.Query;
    }

    public void BindInfo()
    {
        if (generalid > 0)
        {
            ESP.Purchase.Entity.GeneralInfo g = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
            if (null != g)
            {
                GenericInfo.Model = g;
                GenericInfo.BindInfo();

                projectInfo.Model = g;
                projectInfo.BindInfo();

                supplierInfo.Model = g;
                supplierInfo.BindInfo();

                productInfo.Model = g;
                
                productInfo.BindInfo();
            }
        }
    }

    public int SaveInfo()
    {
        ESP.Purchase.Entity.GeneralInfo g = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);

        g.Addstatus = 5;
        try
        {
            ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(g);
            return 1;
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        ESP.Purchase.Entity.GeneralInfo g = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
        g.Addstatus = 3;
        try
        {
            ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(g);
        }
        catch (Exception ex)
        {
            //
        }
        finally
        {
            query = query.AddParam(RequestName.GeneralID, generalid);
            Response.Redirect("AddRequisitionStep6.aspx?" + query);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (SaveInfo() != 0)
        {
            query = query.AddParam(RequestName.GeneralID, generalid);
            Response.Redirect("AddRequisitionStep7.aspx?" + query);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据提交失败!');", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveInfo() != 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');window.location='RequisitionSaveList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }

    }

}
