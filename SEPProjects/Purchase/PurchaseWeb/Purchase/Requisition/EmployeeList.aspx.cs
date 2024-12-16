using System;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ESP.Compatible;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_EmployeeList : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_";


    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_Requisition_EmployeeList));
        #endregion

        divModuleTree.InnerHtml = DepartmentManager.GetHtml(Server.MapPath("DepartmentModify.xslt"));

        if (!string.IsNullOrEmpty(Request["Name"]))
        {
            if (!IsPostBack)
            {
                txtName.Text = Request["Name"];
            }
        }
        else
        {
            if (!IsPostBack)
            {
                try
                {
                    int depid = 0;
                    if (!string.IsNullOrEmpty(Request["gid"]))
                    {
                        GeneralInfo generalInfo = GeneralInfoManager.GetModel(int.Parse(Request["gid"]));
                        if (generalInfo.requestor > 0)
                            depid = new ESP.Compatible.Employee(generalInfo.requestor).GetDepartmentIDs()[0];
                        else
                            depid = CurrentUser.GetDepartmentIDs()[0];
                    }
                    else
                    {
                        depid = CurrentUser.GetDepartmentIDs()[0];
                    }
                    if(!string.IsNullOrEmpty(Request["oldUserId"]))
                    {
                        depid = new ESP.Compatible.Employee(int.Parse(Request["oldUserId"])).GetDepartmentIDs()[0];
                    }
                    Department d = DepartmentManager.GetDepartmentByPK(depid);
                    if (d.Level == 1)
                    {
                        hidtype.Value = d.UniqID.ToString();
                    }
                    else if (d.Level == 2)
                    {
                        hidtype1.Value = d.UniqID.ToString();
                        hidtype.Value = d.Parent.UniqID.ToString();
                    }
                    else if (d.Level == 3)
                    {
                        hidtype2.Value = d.UniqID.ToString();
                        hidtype1.Value = d.Parent.UniqID.ToString();
                        hidtype.Value = d.Parent.Parent.UniqID.ToString();
                    }
                }
                catch { }
            }
        }
        if (!IsPostBack)
        {
            DepartmentDataBind();
            gvDataBind();
        }
        listBind();
    }

    private void listBind()
    {
        btnClean.Visible = false;
        if (!string.IsNullOrEmpty(txtName.Text) || (hidtype.Value != "" && hidtype.Value != "-1") || (hidtype1.Value != "" && hidtype1.Value != "-1") || (hidtype2.Value != "" && hidtype2.Value != "-1"))
        {
            btnClean.Visible = true;
        }

        if (!string.IsNullOrEmpty(Request["clientId"]))
        {
            clientId = Request["clientId"];// "ctl00_ContentPlaceHolder1_genericInfo_";
        }
    }

    protected void add(string sysUserId)
    {
        Employee y = new Employee(int.Parse(sysUserId));
        string username = y.Name;
        string phone = y.Telephone;
        string sysuserid = y.SysID;
        string group = y.GetDepartmentNames().Count == 0 ? "" : y.GetDepartmentNames()[0].ToString();
       // string[] phonenum = phone.Split('-');
        switch (Request["type"])
        {
            case "supplier":
                Response.Write("<script>opener.document.all." + clientId + "txtAudit.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidAuditId.value= '" + sysuserid + "'</script>");
                break;
            case "enduser":
                Response.Write("<script>opener.document.all." + clientId + "txtenduser.value= '" + username + "'</script>");

               // if (phonenum.Length == 4)
               // {
                    Response.Write("<script>opener.document.all." + clientId + "txtenduser_con.value= '" + phone + "'</script>");
                   // Response.Write("<script>opener.document.all." + clientId + "txtenduser_area.value= '" + phonenum[1] + "'</script>");
                   // Response.Write("<script>opener.document.all." + clientId + "txtenduser_phone.value= '" + phonenum[2] + "'</script>");
                   // Response.Write("<script>opener.document.all." + clientId + "txtenduser_Ext.value='" + phonenum[3] + "'</script>");
                //}
                //else
                //{
                //    Response.Write("<script>opener.document.all." + clientId + "txtenduser_con.value= '" + phone + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtenduser_area.value= ''</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtenduser_phone.value= ''</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtenduser_Ext.value=''</script>");
                //}
                Response.Write("<script>opener.document.all." + clientId + "txtenduser_group.value= '" + group + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidenduser.value= '" + sysuserid + "-" + username + "'</script>");
                Response.Write("<script>opener.__doPostBack('" + clientId.Replace('_', '$') + "btnUpdateGeneralInfo','');</script>");
                break;
            case "receiver":
                //收货人和附加收货人不能相同
                GeneralInfo generalInfo = GeneralInfoManager.GetModel(int.Parse(Request["gid"]));
                if (generalInfo.appendReceiver > 0 && generalInfo.appendReceiver == int.Parse(sysuserid))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('收货人和附加收货人不能相同！');", true);
                    return;
                }
                Response.Write("<script>opener.document.all." + clientId + "txtgoods_receiver.value= '" + username + "'</script>");
                //if (phonenum.Length == 4)
                //{
                //    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_con.value= '" + phonenum[0] + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_area.value= '" + phonenum[1] + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_phone.value= '" + phonenum[2] + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_Ext.value= '" + phonenum[3] + "'</script>");
                //}
                //else
                //{
                    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_con.value= '" + phone + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_area.value= ''</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_phone.value= ''</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtreceiver_Ext.value= ''</script>");
                //}
                //Response.Write("<script>opener.document.all." + clientId + "txtship_address.value= '" + y.Address + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidreceiver.value= '" + sysuserid + "-" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "txtReceiverGroup.value= '" + group + "'</script>");
                Response.Write("<script>opener.__doPostBack('" + clientId.Replace('_', '$') + "btnUpdateGeneralInfo','');</script>");
                break;
            case "appendReceiver":
                //收货人和附加收货人不能相同
                GeneralInfo generalInfo1 = GeneralInfoManager.GetModel(int.Parse(Request["gid"]));
                if (generalInfo1.goods_receiver > 0 && int.Parse(sysuserid) == generalInfo1.goods_receiver)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('收货人和附加收货人不能相同！');", true);
                    return;
                }
                Response.Write("<script>opener.document.all." + clientId + "txtappendReceiver.value= '" + username + "'</script>");
                //if (phonenum.Length == 4)
                //{
                //    Response.Write("<script>opener.document.all." + clientId + "txtAppen_con.value= '" + phonenum[0] + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtAppen_area.value= '" + phonenum[1] + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtAppen_phone.value= '" + phonenum[2] + "'</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtAppen_Ext.value= '" + phonenum[3] + "'</script>");
                //}
                //else
                //{
                    Response.Write("<script>opener.document.all." + clientId + "txtAppen_con.value= ''</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtAppen_area.value= ''</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtAppen_phone.value= ''</script>");
                //    Response.Write("<script>opener.document.all." + clientId + "txtAppen_Ext.value= ''</script>");
                //}
                Response.Write("<script>opener.document.all." + clientId + "hidappendReceiver.value= '" + sysuserid + "-" + username + "'</script>");
               // Response.Write("<script>opener.document.all." + clientId + "txtappendReceiverGroup.value= '" + group + "'</script>");
                Response.Write("<script>opener.__doPostBack('" + clientId.Replace('_', '$') + "btnUpdateGeneralInfo','');</script>");
                break;
            case "first_assessor":
                Response.Write("<script>opener.document.all." + clientId + "txtfirst_assessor.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidfirst_assessor.value= '" + sysuserid + "-" + username + "'</script>");
                break;
                //Filiale_accessor changed 
            case "filiale":
                Response.Write("<script>opener.document.all." + clientId + "txtFiliale.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidFiliale.value= '" + sysuserid + "-" + username + "'</script>");
                break;
            case "NextFili":
                Response.Write("<script>opener.document.all." + clientId + "txtNextFili.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidNextFili.value= '" + sysuserid + "'</script>");
                break;
            case "afterwards":
                Response.Write("<script>opener.document.all." + clientId + "txtafterwards.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidafterwards.value= '" + sysuserid + "-" + username + "'</script>");
                break;
            case "backup":
                Response.Write("<script>opener.document.all." + clientId + "txt" + Request["ctl"] + ".value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hid"+Request["ctl"]+".value= '" + sysuserid + "'</script>");
                break;
            case "changeAuditor":
                Response.Write("<script>opener.document.all." + clientId + "txtfirst_assessor.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidfirst_assessor.value= '" + sysuserid + "-" + username + "'</script>");
                break;
            case "producttype1":
                Response.Write("<script>opener.document.all." + clientId + "txtAuditor.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidAuditor.value= '" + sysuserid + "'</script>");
                break;
            case "producttype2":
                Response.Write("<script>opener.document.all." + clientId + "txtAuditor1.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidAuditor1.value= '" + sysuserid + "'</script>");
                break;
            case "producttype3":
                Response.Write("<script>opener.document.all." + clientId + "txtSHAuditor.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidSHAuditor.value= '" + sysuserid + "'</script>");
                break;
            case "producttype4":
                Response.Write("<script>opener.document.all." + clientId + "txtGZAuditor.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidGZAuditor.value= '" + sysuserid + "'</script>");
                break;
            case "producttype5":
                Response.Write("<script>opener.document.all." + clientId + "txtSHAuditor1.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidSHAuditor1.value= '" + sysuserid + "'</script>");
                break;
            case "producttype6":
                Response.Write("<script>opener.document.all." + clientId + "txtGZAuditor1.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + clientId + "hidGZAuditor1.value= '" + sysuserid + "'</script>");
                break;
            case "changeuser1":
                Response.Write("<script>opener.document.all." + clientId + "hidOldUser.value= '" + sysuserid + "#" + username + "'</script>");
                Response.Write("<script>opener.__doPostBack('" + clientId.Replace('_', '$') + "lnkPost','');</script>");
                break;
            case "changeuser2":
                Response.Write("<script>opener.document.all." + clientId + "hidNewUser.value= '" + sysuserid + "#" + username + "'</script>");
                Response.Write("<script>opener.__doPostBack('" + clientId.Replace('_', '$') + "lnkPost1','');</script>");
                break;
            case "prAuthorization":
                Response.Write("<script>opener.document.all." + Request["clientId"] + "txtUser.value= '" + username + "'</script>");
                Response.Write("<script>opener.document.all." + Request["clientId"] + "hidUser.value= '" + sysuserid + "'</script>");
                break;
        }
        Response.Write(@"<script>window.close();</script>");
    }   

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string sysUserID = gv.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(sysUserID);
        }
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        List<ESP.Compatible.Employee> ds = ESP.Compatible.Employee.GetDataSetByName(txtName.Text);

        if (null != ds && ds.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
        }
        if (!string.IsNullOrEmpty(Request["clientId"]))
        {
            clientId = Request["clientId"];//"ctl00_ContentPlaceHolder1_genericInfo_";
        }
        hidtype.Value = "";
        hidtype1.Value = "";
        hidtype2.Value = "";
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        gvDataBind();
    }

    //绑定
    private void gvDataBind()
    {
        string value = txtName.Text.Trim();
        int[] depids = null;
        List<Department> dlist;
        //Department dep;
        string typevalue = null;

        if (hidtype2.Value != "" && hidtype2.Value != "-1")
        {
            typevalue = hidtype2.Value;
        }
        else if (hidtype1.Value != "" && hidtype1.Value != "-1")
        {
            typevalue = hidtype1.Value;
        }
        else if (hidtype.Value != "" && hidtype.Value != "-1")
        {
            typevalue = hidtype.Value;
        }

        if (typevalue != null && typevalue.Length != 0)
        {
            int selectedDep = int.Parse(typevalue);
            dlist = GetLeafChildDepartments(selectedDep);
            if (dlist != null && dlist.Count > 0)
            {
                depids = new int[dlist.Count];
                for (int i = 0; i < dlist.Count; i++)
                {
                    depids[i] = dlist[i].UniqID;
                }
            }
            else
            {
                depids = new int[] { selectedDep };
            }
        }
        int[] newDepids = null;
        if (!string.IsNullOrEmpty(Request["type"]) && Request["type"] == "NextFili")
        {
            newDepids = new int[2];
            newDepids[0] = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["StockDeparmentUniqID"].ToString());
            newDepids[1] = int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["CQStockDeparmentUniqID"].ToString());
        }

        DataSet ds = ESP.Compatible.Employee.GetDataSetUserByKey(value, (newDepids == null ? depids : newDepids));
        gv.DataSource = ds;
        gv.DataBind();
    }

    private void DepartmentDataBind()
    {
        object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
        ddltype.DataSource = dt;
        ddltype.DataTextField = "NodeName";
        ddltype.DataValueField = "Uniqid";
        ddltype.DataBind();
        ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> getalist(int parentId)
    {
        List<List<string>> list = new List<List<string>>();
        try
        {

            list = ESP.Compatible.DepartmentManager.GetListForAJAX(parentId);
        }
        catch (Exception e)
        {
            e.ToString();
        }

        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }
}
