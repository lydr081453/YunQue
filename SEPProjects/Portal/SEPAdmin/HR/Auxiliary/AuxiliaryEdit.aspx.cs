using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Auxiliary
{
    public partial class AuxiliaryEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["auxiliaryid"]))
                {
                    InitPage();
                }
                else
                {
                    hidId.Value = "0";
                    object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
                    drpCompany.DataSource = dt;
                    drpCompany.DataTextField = "NodeName";
                    drpCompany.DataValueField = "UniqID";
                    drpCompany.DataBind();
                }
            }
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage()
        {
            try
            {
                ESP.HumanResource.Entity.AuxiliaryInfo model = ESP.HumanResource.BusinessLogic.AuxiliaryManager.GetModel(int.Parse(Request["auxiliaryid"]));
                if (model != null)
                {
                    txtauxiliaryName.Text = model.auxiliaryName;
                    txtDescription.Text = model.Description;
                    hidId.Value = model.id.ToString();
                    object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
                    drpCompany.DataSource = dt;
                    drpCompany.DataTextField = "NodeName";
                    drpCompany.DataValueField = "UniqID";
                    drpCompany.DataBind();
                    drpCompany.SelectedValue = model.companyID.ToString();
                    drpApply.SelectedValue = model.apply.ToString();
                }
                else
                {
                    hidId.Value = "0";
                    object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
                    drpCompany.DataSource = dt;
                    drpCompany.DataTextField = "NodeName";
                    drpCompany.DataValueField = "UniqID";
                    drpCompany.DataBind();
                }
            }
            catch { }
        }   
        
        protected void btnCommit_Click(object sender, EventArgs e)
        {
            if (hidId.Value != "" && hidId.Value == "0")
            {
                AuxiliaryInfo aux = new AuxiliaryInfo();
                aux.auxiliaryName = txtauxiliaryName.Text.Trim();
                aux.Description = txtDescription.Text.Trim();
                aux.isDisable = false;
                aux.companyID = Convert.ToInt32(drpCompany.SelectedItem.Value);
                aux.apply = Convert.ToInt32(drpApply.SelectedItem.Value);

                LogInfo logmodel = new LogInfo();
                logmodel.Des = string.Format("[{0}]添加了待入职的辅助工作[{1}]", UserInfo.FullNameCN, txtauxiliaryName.Text.Trim());
                logmodel.Status = 0;
                logmodel.LogUserId = UserID;
                logmodel.LogUserName = UserInfo.FullNameEN;
                logmodel.LogMedifiedTeme = DateTime.Now;
                int returnValue = AuxiliaryManager.Add(aux,logmodel);
                if (returnValue > 0)
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AuxiliaryList.aspx';alert('待入职辅助工作添加成功！');", true);
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('待入职辅助工作添加失败！');", true);
                
            }
            else if (hidId.Value != "")
            {
                AuxiliaryInfo aux = new AuxiliaryInfo();
                try
                {
                    aux = AuxiliaryManager.GetModel(int.Parse(hidId.Value.Trim()));
                    aux.auxiliaryName = txtauxiliaryName.Text.Trim();
                    aux.Description = txtDescription.Text.Trim();
                    aux.companyID = Convert.ToInt32(drpCompany.SelectedItem.Value);
                    aux.apply = Convert.ToInt32(drpApply.SelectedItem.Value);
                }
                catch { }

                if (aux != null)
                {
                    LogInfo logmodel = new LogInfo();
                    logmodel.Des = string.Format("[{0}]修改了待入职的辅助工作[{1}]", UserInfo.FullNameCN, aux.auxiliaryName);
                    logmodel.Status = 0;
                    logmodel.LogUserId = UserID;
                    logmodel.LogUserName = UserInfo.FullNameEN;
                    logmodel.LogMedifiedTeme = DateTime.Now;
                    int returnValue = AuxiliaryManager.Update(aux, logmodel);
                    if (returnValue > 0)
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AuxiliaryList.aspx';alert('待入职辅助工作修改成功！');", true);
                    else
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('待入职辅助工作修改失败！');", true);
                }
                else 
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('待入职辅助工作修改失败！');", true);
                }
 
            }
        }

        /// <summary>
        /// 返回按钮的Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/HR/Auxiliary/AuxiliaryList.aspx");
        }

        
    }
}
