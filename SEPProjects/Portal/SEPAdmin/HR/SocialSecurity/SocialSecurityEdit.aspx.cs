using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.SocialSecurity
{
    public partial class SocialSecurityEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initDrp();
                initForm();
            }
        }

        protected void initDrp()
        {
            int year = DateTime.Now.Year - 10;
            for (int i = 0; i < 20; i++)
            {
                drpBeginTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
                drpEndTimeY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));
            }

            for (int i = 1; i <= 12; i++)
            {
                drpBeginTimeM.Items.Insert(i - 1, new ListItem((i).ToString(), (i).ToString()));
                drpEndTimeM.Items.Insert(i - 1, new ListItem((i).ToString(), (i).ToString()));
            }

            object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
            drpCompany.DataSource = dt;
            drpCompany.DataTextField = "NodeName";
            drpCompany.DataValueField = "UniqID";
            drpCompany.DataBind();
 
        }
        protected void initForm()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                DataSet ds = ESP.HumanResource.BusinessLogic.SocialSecurityManager.GetList(" id=" + Request["id"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    //养老保险公司比例
                    txtEIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString() : "0");

                    //养老保险个人比例
                    txtEIProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString() : "0");

                    //失业保险公司比例
                    txtUIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString() : "0");
                    //失业保险个人比例
                    txtUIProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString() : "0");
                    //生育保险公司比例
                    txtBIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString() : "0");
                    //工伤险公司比例
                    txtCIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString() : "0");
                    //医疗保险公司比例
                    txtMIProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString() : "0");
                    //医疗保险个人比例
                    txtMIProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString() : "0");
                    //医疗保险大额医疗个人支付额
                    txtMIBigProportionOfIndividuals.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString()) ? ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString() : "0");

                    //公积金比例
                    txtPRFProportionOfFirms.Text = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString()) ? ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString() : "0");

                    DateTime begintime = DateTime.Parse(ds.Tables[0].Rows[0]["BeginTime"].ToString());
                    DateTime endtime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());

                    try
                    {
                        drpBeginTimeY.SelectedValue = begintime.Year.ToString();
                    }
                    catch
                    {
                        drpBeginTimeY.SelectedValue = DateTime.Now.Year.ToString();
                    }
                    try
                    {
                        drpEndTimeY.SelectedValue = endtime.Year.ToString();
                    }
                    catch
                    {
                        drpEndTimeY.SelectedValue = DateTime.Now.Year.ToString();
                    }



                    try
                    {
                        drpBeginTimeM.SelectedValue = begintime.Month.ToString();
                    }
                    catch
                    {
                        drpBeginTimeM.SelectedValue = DateTime.Now.Month.ToString();
                    }
                    try
                    {
                        drpEndTimeM.SelectedValue = endtime.Month.ToString();
                    }
                    catch
                    {
                        drpEndTimeM.SelectedValue = DateTime.Now.Month.ToString();
                    }

                    drpCompany.SelectedValue = ds.Tables[0].Rows[0]["SocialInsuranceCompany"].ToString();

                }
                else
                {

                    //养老保险公司比例
                    txtEIProportionOfFirms.Text = "0";
                    //养老保险个人比例
                    txtEIProportionOfIndividuals.Text = "0";
                    //失业保险公司比例
                    txtUIProportionOfFirms.Text = "0";
                    //失业保险个人比例
                    txtUIProportionOfIndividuals.Text = "0";
                    //生育保险公司比例
                    txtBIProportionOfFirms.Text = "0";
                    //工伤险公司比例
                    txtCIProportionOfFirms.Text = "0";
                    //医疗保险公司比例
                    txtMIProportionOfFirms.Text = "0";
                    //医疗保险个人比例
                    txtMIProportionOfIndividuals.Text = "0";
                    //医疗保险大额医疗个人支付额
                    txtMIBigProportionOfIndividuals.Text = "0";

                    //公积金比例
                    txtPRFProportionOfFirms.Text = "0";

                    drpBeginTimeY.SelectedValue = DateTime.Now.Year.ToString();
                    drpEndTimeY.SelectedValue = DateTime.Now.Year.ToString();
                    drpBeginTimeM.SelectedValue = DateTime.Now.Month.ToString();
                    drpEndTimeM.SelectedValue = DateTime.Now.Month.ToString();


                }              
                   
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESP.HumanResource.Entity.SocialSecurityInfo soc = null;
            if (string.IsNullOrEmpty(Request["id"]))
            {
                soc = new ESP.HumanResource.Entity.SocialSecurityInfo();
            }
            else
            {
                soc = SocialSecurityManager.GetModel(int.Parse(Request["id"].ToString()));
            }
            soc.BeginTime = DateTime.Parse(drpBeginTimeY.SelectedItem.Value + "-" + drpBeginTimeM.SelectedItem.Value + "-01");
            soc.BIProportionOfFirms = decimal.Parse(txtBIProportionOfFirms.Text.Trim());
            soc.BIProportionOfIndividuals = decimal.Parse("0");
            soc.CIProportionOfFirms = decimal.Parse(txtCIProportionOfFirms.Text.Trim());
            soc.CIProportionOfIndividuals = decimal.Parse("0");
            soc.EIProportionOfFirms = decimal.Parse(txtEIProportionOfFirms.Text.Trim());
            soc.EIProportionOfIndividuals = decimal.Parse(txtEIProportionOfIndividuals.Text.Trim());
            soc.EndTime = DateTime.Parse(drpEndTimeY.SelectedItem.Value + "-" + drpEndTimeM.SelectedItem.Value + "-01");
            soc.LastUpdateMan = UserInfo.UserID;
            soc.lastUpdateTime = DateTime.Now;
            soc.MIBigProportionOfIndividuals = decimal.Parse(txtMIBigProportionOfIndividuals.Text.Trim());
            soc.MIProportionOfFirms = decimal.Parse(txtMIProportionOfFirms.Text.Trim());
            soc.MIProportionOfIndividuals = decimal.Parse(txtMIProportionOfIndividuals.Text.Trim());
            soc.PRFProportionOfFirms = decimal.Parse(txtPRFProportionOfFirms.Text.Trim());
            soc.PRFProportionOfIndividuals = decimal.Parse(txtPRFProportionOfFirms.Text.Trim());
            soc.SocialInsuranceCompany = int.Parse(drpCompany.SelectedItem.Value);
            soc.UIProportionOfFirms = decimal.Parse(txtUIProportionOfFirms.Text.Trim());
            soc.UIProportionOfIndividuals = decimal.Parse(txtUIProportionOfIndividuals.Text.Trim());
            if (string.IsNullOrEmpty(Request["id"]))
            {

                soc.CreateTime = DateTime.Now;
                soc.Creator = UserInfo.UserID;

                int val = SocialSecurityManager.Add(soc);
                if (val > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='SocialSecurityList.aspx';alert('添加成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
                }
            }
            else
            {                
                int val = SocialSecurityManager.Update(soc);
                if (val > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='SocialSecurityList.aspx';alert('修改成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改失败！');", true);
                }
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SocialSecurityEdit.aspx");
        }
    }


}
