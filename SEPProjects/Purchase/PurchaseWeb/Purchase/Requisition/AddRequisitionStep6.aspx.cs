using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using System.Collections.Generic;

public partial class Purchase_Requisition_AddRequisitionStep6 : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;
    string query = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);

        }
        if (!IsPostBack)
        {
            BindInfo();
            productInfo.ItemListBind(" general_id = " + Request[RequestName.GeneralID]);
        }
        productInfo.CurrentUser = CurrentUser;
        query = Request.Url.Query;
    }

    public void BindInfo()
    {
        if (generalid > 0)
        {
            GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            if (null != g)
            {
                GenericInfo.Model = g;
                GenericInfo.BindInfo();

                projectInfo.Model = g;
                projectInfo.BindInfo();

                productInfo.Model = g;
                productInfo.BindInfo();
                if (g.Project_id != 0)
                {
                    ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(g.Project_id);
                    productInfo.ValidProfit(projectModel);
                }
            }
        }
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        g.Addstatus = 1;
        try
        {
            GeneralInfoDataProvider.Update(g);


            //记录操作日志
          //  ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第三步跳转第二步"), "创建采购申请单");
        }
        catch (Exception ex)
        {
            //
        }
        finally
        {
            query = query.AddParam(RequestName.GeneralID, generalid);
            Response.Redirect("AddRequisitionStep3.aspx?" + query);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (productInfo.getItemRowCount() > 0)
        {
            int ret = SaveInfo();
            if (ret == 1)
            {
                //记录操作日志
                // ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第三步跳转第四步"), "创建采购申请单");

                query = query.AddParam(RequestName.GeneralID, generalid);
                Response.Redirect("AddRequisitionStep7.aspx?" + query);
            }
            else if (ret == 3)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择采购审批流向!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
            }
        }
        else
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请添加采购物品!');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ret = SaveInfo();
        if (ret == 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }
        else if (ret == 1)
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, generalid.ToString(), "创建采购申请单第三步保存并返回列表页"), "创建采购申请单");

            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');", true);
        }

    }


    public int SaveInfo()
    {
        GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        decimal totalprice = 0;
        string typeId = string.Empty;
        ESP.Purchase.Entity.SupplierInfo supplier = null;
        productInfo.Model = g;
        g = productInfo.setModelInfo();
        if ((string.IsNullOrEmpty(g.contrastUpFiles) && string.IsNullOrEmpty(g.contrastRemark)) && g.PRType != (int)ESP.Purchase.Common.PRTYpe.MediaPR)
        {
            g.Addstatus = 3;
        }
        else
            g.Addstatus = 5;

        if (g.ValueLevel == 0)
        {
            g.Addstatus = 3;
        }
        else
            g.Addstatus = 5;


        List<ESP.Purchase.Entity.OrderInfo> orderlist = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(g.id);
        typeId = orderlist[0].producttype.ToString();

        //2309	北京腾信互动广告有限责任公司
        if (g.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement && string.IsNullOrEmpty(g.supplier_name))
        {
            supplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADSupplierId"]));
            g.supplier_name = supplier.supplier_name;
            g.supplier_address = supplier.contact_address;
            g.Supplier_cellphone = supplier.contact_mobile;
            g.supplier_email = supplier.contact_email;
            g.supplier_fax = supplier.contact_fax;
            g.supplier_linkman = supplier.contact_name;
            g.supplier_phone = supplier.contact_tel;
            g.account_bank = supplier.account_bank;
            g.account_name = supplier.account_name;
            g.account_number = supplier.account_number;
            g.source = supplier.supplier_source;
            g.fa_no = supplier.supplier_frameNO;



            foreach (OrderInfo order in orderlist)
            {
                totalprice += order.total;
                order.supplierId = supplier.id;
                order.supplierName = supplier.supplier_name;
                ESP.Purchase.BusinessLogic.OrderInfoManager.Update(order, int.Parse(CurrentUser.SysID), CurrentUser.Name);
            }


        }
        else
        {
            foreach (OrderInfo order in orderlist)
            {
                    totalprice += order.total;
            }
        }
        g.totalprice = totalprice;
        try
        {
            if (g.ValueLevel != 0)
            {
                    GeneralInfoDataProvider.Update(g);
                    return 1;
            }
            else
            {
                return 3;
            }

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
