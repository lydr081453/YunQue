using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.UserManagement
{
    public partial class DepartmentsForm : ESP.Web.UI.PageBase
    {
        private DepartmentInfo CurrentDepartment
        {
            get
            {
                if (null != ViewState["CurrentDepartment"])
                    return (DepartmentInfo)ViewState["CurrentDepartment"];
                else
                {
                    return new DepartmentInfo();
                }
            }
            set { ViewState["CurrentDepartment"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();                
            }
        }
        protected void btnAddNewChild_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentsForm.aspx?parentid=" + Request["depID"].ToString());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save(CurrentDepartment);
            Response.Redirect("DepartmentsTree.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "删除")            
            {
                DepartmentManager.Delete(Convert.ToInt32(lblID.Text.Trim()));
            }
            Response.Redirect("DepartmentsTree.aspx");
        }

        private void Bind()
        {
             IList<DepartmentTypeInfo> deptype = DepartmentTypeManager.GetAll();
             for (int i = 0; i < deptype.Count;i++ )
             {
                 ddlDepType.Items.Add(new ListItem(deptype[i].DepartmentTypeName,deptype[i].DepartmentTypeID.ToString()));                 
             }
             ddlDepType.DataBind();
             if (!string.IsNullOrEmpty(Request["depID"]))
             {
                 DepartmentInfo info = DepartmentManager.Get(Convert.ToInt32(Request["depID"]));
                 CurrentDepartment = info;
                 if (info != null)
                 {
                     lblID.Text = info.DepartmentID.ToString();
                     this.txtName.Text = info.DepartmentName;
                     this.txtDescription.Text = info.Description;
                     this.txtSort.Text = info.Ordinal.ToString();
                     ddlStatus.SelectedValue = info.Status.ToString();
                     ddlDepType.SelectedValue = info.DepartmentTypeID.ToString();
                     if (info.DepartmentLevel > 2)
                     {
                         btnAddNewChild.Visible = false;
                         ESP.Framework.Entity.OperationAuditManageInfo opearmodel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(Convert.ToInt32(Request["depID"]));
                         if (opearmodel != null)
                         {
                             txtdirectorName.Text = opearmodel.DirectorName;
                             hiddirectorId.Value = opearmodel.DirectorId.ToString();
                             txtmanagerName.Text = opearmodel.ManagerName;
                             hidmanagerId.Value = opearmodel.ManagerId.ToString();
                             txtceoName.Text = opearmodel.CEOName;
                             hidceoId.Value = opearmodel.CEOId.ToString();
                             txthrName.Text = opearmodel.HRName;
                             hidhrId.Value = opearmodel.HRId.ToString();
                             txthrattendanceName.Text = opearmodel.Hrattendancename;
                             hidhrattendanceId.Value = opearmodel.Hrattendanceid.ToString();
                             txtdimissionadauditorName.Text = opearmodel.DimissionADAuditorName;
                             hiddimissionadauditorId.Value = opearmodel.DimissionADAuditorId.ToString();
                             txtdimissionDirectorName.Text = opearmodel.DimissionDirectorName;
                             hiddimissionDirectorId.Value = opearmodel.DimissionDirectorid.ToString();
                             txtdimissionManagerName.Text = opearmodel.DimissionManagerName;
                             hiddimissionManagerId.Value = opearmodel.DimissionManagerId.ToString();
                             txtheadCountAuditorName.Text = opearmodel.HeadCountAuditor;
                             hidheadCountAuditorId.Value = opearmodel.HeadCountAuditorId.ToString();
                             txtheadCountDirectorName.Text = opearmodel.HeadCountDirector;
                             hidheadCountDirectorId.Value = opearmodel.HeadCountDirectorId.ToString();
                             txtpurchaseAuditorName.Text = opearmodel.PurchaseAuditor;
                             hidpurchaseAuditorId.Value = opearmodel.PurchaseAuditorId.ToString();
                             txtappendReceiverName.Text = opearmodel.AppendReceiver;
                             hidappendReceiverId.Value = opearmodel.AppendReceiverId.ToString();
                             txtpurchaseDirectorName.Text = opearmodel.PurchaseDirector;
                             hidpurchaseDirectorId.Value = opearmodel.PurchaseDirectorId.ToString();
                             txtdirectorAmount.Text = opearmodel.DirectorAmount.ToString("#,##0.00");
                             txtmanagerAmount.Text = opearmodel.ManagerAmount.ToString("#,##0.00");
                             txtCEOAmount.Text = opearmodel.CEOAmount.ToString("#,##0.00");
                         }
                         
                     }
                     else
                     {
                         tab1.Visible = false;
                     }
                     btnDelete.Text = "删除";
                     hypDepPost.NavigateUrl = string.Format("DepartmentPositionForm.aspx?depid={0}&backurl=DepartmentsTree",Request["depID"]);
                 }
                 else
                 {
                     btnAddNewChild.Visible = false;
                     tab1.Visible = false;
                     btnDelete.Text = "返回";
                 }

                 IList<DepartmentPositionInfo> list = DepartmentPositionManager.GetByDepartment(Convert.ToInt32(Request["depID"]),false);
                 gvView.DataSource = list;
                 gvView.DataBind();
             }
             else
             {
                 btnAddNewChild.Visible = false;
                 hypDepPost.Visible = false;
                 tab1.Visible = false;
                 btnDelete.Text = "返回";
             }
             
        }

        private void Save(DepartmentInfo info)
        {
            string depname = txtName.Text.Trim().Replace("-","－");
            info.DepartmentName = depname;
            info.Description = this.txtDescription.Text.Trim();
            info.Ordinal = Convert.ToInt32(this.txtSort.Text);
            info.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            info.DepartmentTypeID = Convert.ToInt32(ddlDepType.SelectedValue);
            info.DepartmentLevel = 0;            
            if (!string.IsNullOrEmpty(Request["ParentID"]))
            {
                info.ParentID = Convert.ToInt32(Request["ParentID"]);
                DepartmentInfo parentInfo = DepartmentManager.Get(Convert.ToInt32(Request["ParentID"]));
                if (parentInfo != null)
                    info.DepartmentLevel = ++parentInfo.DepartmentLevel;
            }
            else
            {
                info.ParentID = 0;
            }
            if (!string.IsNullOrEmpty(Request["depid"]))
            {
                //////
                try
                {
                    DepartmentManager.Update(info);
                    ///
                    if (info.DepartmentLevel == 3)
                    {
                        ESP.Framework.Entity.OperationAuditManageInfo opearmodel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(Convert.ToInt32(Request["depID"]));
                        if (opearmodel == null)
                        {
                            opearmodel = GetOpeartion(new ESP.Framework.Entity.OperationAuditManageInfo());
                            opearmodel.DepId = Convert.ToInt32(Request["depID"]);
                            int returnValue = ESP.Framework.BusinessLogic.OperationAuditManageManager.Add(opearmodel);
                        }
                        else
                        {
                            opearmodel = GetOpeartion(opearmodel);
                            ESP.Framework.BusinessLogic.OperationAuditManageManager.Update(opearmodel);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                }

            }
            else
            {
                //////
                DepartmentManager.Create(info);
                IList<DepartmentPositionInfo> list = DepartmentPositionManager.GetByDepartment(info.ParentID,true);
                if (list.Count > 0)
                {
                    foreach (DepartmentPositionInfo dpinfo in list)
                    {
                        dpinfo.DepartmentID = info.DepartmentID;
                        DepartmentPositionManager.Create(dpinfo);
                    }
                }
            }


            CurrentDepartment = info;
        }

        private ESP.Framework.Entity.OperationAuditManageInfo GetOpeartion(ESP.Framework.Entity.OperationAuditManageInfo model)
        {
            model.DirectorName = txtdirectorName.Text;
            model.DirectorId = int.Parse(hiddirectorId.Value);
            model.ManagerName = txtmanagerName.Text;
            model.ManagerId = int.Parse(hidmanagerId.Value);
            model.CEOName = txtceoName.Text;
            model.CEOId = int.Parse(hidceoId.Value);
            model.HRName = txthrName.Text;
            model.HRId = int.Parse(hidhrId.Value);
            model.Hrattendancename = txthrattendanceName.Text;
            model.Hrattendanceid = int.Parse(hidhrattendanceId.Value);
            model.DimissionADAuditorName = txtdimissionadauditorName.Text;
            model.DimissionADAuditorId = int.Parse(hiddimissionadauditorId.Value);
            model.DimissionDirectorName = txtdimissionDirectorName.Text;
            model.DimissionDirectorid = int.Parse(hiddimissionDirectorId.Value);
            model.DimissionManagerName = txtdimissionManagerName.Text;
            model.DimissionManagerId = int.Parse(hiddimissionManagerId.Value);
            model.HeadCountAuditor = txtheadCountAuditorName.Text;
            model.HeadCountAuditorId = int.Parse(hidheadCountAuditorId.Value);
            model.HeadCountDirector = txtheadCountDirectorName.Text; 
            model.HeadCountDirectorId = int.Parse(hidheadCountDirectorId.Value);
            model.PurchaseAuditor = txtpurchaseAuditorName.Text;
            model.PurchaseAuditorId = int.Parse(hidpurchaseAuditorId.Value);
            model.AppendReceiver = txtappendReceiverName.Text;
            model.AppendReceiverId = int.Parse(hidappendReceiverId.Value);
            model.PurchaseDirector = txtpurchaseDirectorName.Text;
            model.PurchaseDirectorId = int.Parse(hidpurchaseDirectorId.Value);
            model.DirectorAmount = decimal.Parse(txtdirectorAmount.Text);
            model.ManagerAmount = decimal.Parse(txtmanagerAmount.Text);
            model.CEOAmount = decimal.Parse(txtCEOAmount.Text);
            return model;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HyperLink hypDepID = (HyperLink)rw.FindControl("hypDepID");            
            if (hypDepID != null && !string.IsNullOrEmpty(hypDepID.Text))
            {
                DepartmentPositionManager.Delete(Convert.ToInt32(hypDepID.Text));
                Bind();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow rw = (GridViewRow)((ImageButton)sender).Parent.Parent;
            HyperLink hypDepID = (HyperLink)rw.FindControl("hypDepID");
            if (hypDepID != null && !string.IsNullOrEmpty(hypDepID.Text))
            {
                Response.Redirect("DepartmentPositionForm.aspx?depposid=" + (hypDepID.Text) + "&backurl=DepartmentsTree");
            }
        }

        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = (HyperLink)e.Row.FindControl("hypDepID");
                link.NavigateUrl = "DepartmentPositionForm.aspx?depposid=" + gvView.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&backurl=DepartmentsTre";
                link.Text = gvView.DataKeys[e.Row.RowIndex].Values[0].ToString();
            }
        }
    }
}