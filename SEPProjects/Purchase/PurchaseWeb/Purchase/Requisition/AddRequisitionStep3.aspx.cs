using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_AddRequisitionStep3 : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;
    string query = string.Empty;
    decimal totalamount = 0;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        query = Request.Url.Query;
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    /// <summary>
    /// Binds the info.
    /// </summary>
    public void BindInfo()
    {
        if (generalid > 0)
        {
            ESP.Purchase.Entity.GeneralInfo g = GeneralInfoManager.GetModel(generalid);
            if (null != g)
            {
                projectInfo.Model = g;
                projectInfo.BindInfo();

                genericInfo.CurrentUser = CurrentUser;
                genericInfo.Model = g;
                genericInfo.BindInfo();
            }
        }
    }

    /// <summary>
    /// Saves the info.
    /// </summary>
    /// <returns></returns>
    public int SaveInfo()
    {
        ESP.Purchase.Entity.GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        genericInfo.CurrentUser = CurrentUser;
        genericInfo.Model = g;
        g = genericInfo.setModelInfo();
        g.Addstatus = 3;

        try
        {
            GeneralInfoManager.Update(g);
            if (g.goods_receiver == 0 || g.appendReceiver==0)
            {
                return 2;
            }
            else
                return 1;
        }
        catch (Exception e)
        {
            return 0;
        }

    }

    /// <summary>
    /// Handles the Click event of the btnPre control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPre_Click(object sender, EventArgs e)
    {
        ESP.Purchase.Entity.GeneralInfo g = GeneralInfoManager.GetModel(generalid);
        g.Addstatus = 1;
        try
        {
            GeneralInfoManager.Update(g);
            //��¼������־
            ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}��������� {2} �Ĳ���", CurrentUser.Name, generalid.ToString(), "�����ɹ����뵥�ڶ�����ת������"), "�����ɹ����뵥");
        }
        catch (Exception ex)
        {
            //
        }
        finally
        {
            Response.Redirect("AddRequisitionStep2.aspx" + query);
        }
    }


    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        int ret =SaveInfo();
        if ( ret == 1)
        {
            //��¼������־
            // ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}��������� {2} �Ĳ���", CurrentUser.Name, generalid.ToString(), "�����ɹ����뵥�ڶ�����ת������"), "�����ɹ����뵥");

            if (generalid > 0)//ý��ϵͳ��ͨ,���û����Ӹ�ѱ�����ת��mediaproductѡ�񣬷�����һ��
            {
                GeneralInfo ginfo = GeneralInfoManager.GetModel(generalid);
                if (ginfo.PRType == (int)PRTYpe.MediaPR)
                {
                    ESP.Purchase.Entity.OrderInfo oinfo = OrderInfoManager.GetModelByGeneralID(generalid);
                    if (oinfo != null)
                    {
                        Response.Redirect("AddRequisitionStep6.aspx" + query);
                    }
                    else
                    {
                        Response.Redirect("MediaProduct.aspx" + query);
                    }
                }
                else
                {
                    Response.Redirect("AddRequisitionStep6.aspx" + query);
                }
            }
            else
            {
                Response.Redirect("AddRequisitionStep6.aspx" + query);
            }
        }
        else if (ret == 2)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('����д(����)�ջ�����Ϣ!');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('�����ύʧ��!');", true);
        }

    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveInfo() == 1)
        {
            //��¼������־
            ESP.Logging.Logger.Add(string.Format("{0}��T_GeneralInfo���е�id={1}��������� {2} �Ĳ���", CurrentUser.Name, generalid.ToString(), "�����ɹ����뵥�ڶ������沢�����б�ҳ��"), "�����ɹ����뵥");
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('���ݱ���ɹ�!');window.location='RequisitionSaveList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('���ݱ���ʧ��!');", true);
        }

    }
}
