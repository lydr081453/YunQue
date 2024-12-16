using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using ESP.Purchase.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.RequestForSeal
{
    public partial class RequestForSealEdit : ESP.Web.UI.PageBase
    {
        private int RfsId = 0;
        RequestForSealManager manager = new RequestForSealManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["RfsId"]))
            {
                RfsId = int.Parse(Request["RfsId"]);
            }
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        public void BindInfo()
        {
            ddlFileType.DataSource = ESP.Administrative.Common.Statics.RequestForSeal_FileType;
            ddlFileType.DataBind();

            ddlSealType.DataSource = ESP.Administrative.Common.Statics.RequestForSeal_SealType;
            ddlSealType.DataBind();

            ddlBrandch.DataSource = ESP.Finance.BusinessLogic.BranchManager.GetList("");
            ddlBrandch.DataTextField = "BranchName";
            ddlBrandch.DataValueField = "BranchID";
            ddlBrandch.DataBind();

            
            labRequestorName.Text = CurrentUser.Name;
            hidRequestorId.Value = CurrentUser.SysID;
            PickerFrom1.SelectedDate = DateTime.Now;


            if (RfsId > 0)
            {
                var model = manager.GetModel(RfsId);

                if (int.Parse(CurrentUser.SysID) != model.RequestorId)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该数据的权限！');window.location.href='RequestForSealList.aspx';", true);
                    return;
                }

                labRequestorName.Text = model.RequestorName;
                hidRequestorId.Value = model.RequestorId.ToString();
                txtDataNum.Text = model.DataNum;
                ddlBrandch.SelectedValue = model.BranchId.ToString();
                ddlDepartments.BindByLevel3Id(model.DepartmentId);
                PickerFrom1.SelectedDate = model.RequestDate;
                ddlSealType.SelectedValue = model.SealType;
                ddlFileType.SelectedValue = model.FileType;
                txtFileName.Text = model.FileName;
                txtFileQuantity.Text = model.FileQuantity.ToString();
                txtRemark.Text = model.Remark;
                if (!string.IsNullOrEmpty(model.Files))
                {
                    repFiles.DataSource = model.Files.Trim('#').Split('#');
                    repFiles.DataBind();
                }
            }else
                ddlDepartments.BindByLevel3Id(CurrentUser.GetDepartmentIDs().FirstOrDefault());
        }

        
 

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            RequestForSealInfo model = SaveModel(ESP.Administrative.Common.Status.RequestForSealStatus.Auditing);
            if(model != null)
            if(setAuditor(model)>0)
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交审核成功！');window.location.href='RequestForSealList.aspx';", true);
        }

        private int setAuditor(RequestForSealInfo model)
        {
            var manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.DepartmentId);
            List<ConsumptionAuditInfo> auditList = new List<ConsumptionAuditInfo>();
            int level = 1;//审批次序
            if (model.Project != null &&  model.RequestorId != model.Project.ApplicantUserID)
            {
                //项目负责人
                auditList.Add(initAuditor(model.Project.ApplicantUserID, model.Id, auditorType.operationAudit_Type_XMFZ, ref level));
            }

            if(auditList.Count(x=>x.AuditorUserID == manageModel.DirectorId) == 0)
                //总监
                auditList.Add(initAuditor(manageModel.DirectorId, model.Id, auditorType.operationAudit_Type_ZJSP, ref level));
            if (auditList.Count(x => x.AuditorUserID == manageModel.ManagerId) == 0)
                //总经理
                auditList.Add(initAuditor(manageModel.ManagerId, model.Id, auditorType.operationAudit_Type_ZJLSP, ref level));
            if (auditList.Count(x => x.AuditorUserID == manageModel.CEOId) == 0)
                //CEO
                auditList.Add(initAuditor(manageModel.CEOId, model.Id, auditorType.operationAudit_Type_CEO, ref level));

            model.AuditorId = auditList[0].AuditorUserID;
            model.AuditorName = auditList[0].AuditorEmployeeName;
            if(string.IsNullOrEmpty(model.SANo)){
                model.SANo = "SA" + DateTime.Now.ToString("yyMM")+model.Id.ToString("00000") ;
            }
            manager.Update(model);

            return ESP.Finance.BusinessLogic.ConsumptionAuditManager.Add(auditList);
        }

        private ConsumptionAuditInfo initAuditor(int auditorId, int batchId, int auditType, ref int level)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(auditorId);
            ConsumptionAuditInfo auditor = new ConsumptionAuditInfo();
            auditor.AuditorEmployeeName = emp.FullNameCN;
            auditor.AuditorUserCode = emp.Code;
            auditor.AuditorUserID = emp.UserID;
            auditor.AuditorUserName = emp.Username;
            auditor.BatchID = batchId;
            auditor.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
            auditor.SquenceLevel = level;
            auditor.FormType = (int)ESP.Finance.Utility.FormType.RequestForSeal;
            auditor.AuditType = auditType;
            level++;
            return auditor;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestForSealList.aspx");
        }

        private RequestForSealInfo SaveModel(ESP.Administrative.Common.Status.RequestForSealStatus status = ESP.Administrative.Common.Status.RequestForSealStatus.Save)
        {
            int deptId = ddlDepartments.Level3;

            RequestForSealManager manager = new RequestForSealManager();
            RequestForSealInfo model = new RequestForSealInfo();
            if (RfsId != 0)
                model = manager.GetModel(RfsId);
            else
            {
                model.RequestorId = int.Parse(hidRequestorId.Value);
                model.RequestorName = labRequestorName.Text;
                model.CreatedDate = DateTime.Now;
            }
            model.DataNum = txtDataNum.Text.Trim();
            model.BranchId = int.Parse(ddlBrandch.SelectedValue);
            model.DepartmentId = deptId;
            model.RequestDate = PickerFrom1.SelectedDate;
            model.SealType = ddlSealType.SelectedValue;
            model.FileType = ddlFileType.SelectedValue;
            model.FileQuantity = int.Parse(txtFileQuantity.Text);
            model.FileName = txtFileName.Text.Trim();
            model.Remark = txtRemark.Text.Trim();
            model.Status = status;
            if (!string.IsNullOrEmpty(hidFiles.Value))
            {
                model.Files += hidFiles.Value;
            }

            if (model.Id > 0)
            {
                manager.Update(model);
            }
            else
            {
                model.Id = manager.Add(model);   
            }
            return model;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var m = SaveModel();
            if(m != null)
                Response.Redirect("RequestForSealEdit.aspx?RfsId=" + m.Id);
        }

        protected void lnkFile_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            DownloadFile(ESP.Configuration.ConfigurationManager.SafeAppSettings["RequestForSealPath"] + lnk.CommandArgument.ToString());
        }

        private void DownloadFile(string filename)
        {
            //打开要下载的文件
            System.IO.FileStream r = new System.IO.FileStream(filename, System.IO.FileMode.Open);
            //设置基本信息
            Response.Buffer = false;
            Response.AddHeader("Connection", "Keep-Alive");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.IO.Path.GetFileName(filename));
            Response.AddHeader("Content-Length", r.Length.ToString());

            try
            {
                while (true)
                {
                    //开辟缓冲区空间
                    byte[] buffer = new byte[1024];
                    //读取文件的数据
                    int leng = r.Read(buffer, 0, 1024);
                    if (leng == 0)//到文件尾，结束
                        break;
                    if (leng == 1024)//读出的文件数据长度等于缓冲区长度，直接将缓冲区数据写入
                        Response.BinaryWrite(buffer);
                    else
                    {
                        //读出文件数据比缓冲区小，重新定义缓冲区大小，只用于读取文件的最后一个数据块
                        byte[] b = new byte[leng];
                        for (int i = 0; i < leng; i++)
                            b[i] = buffer[i];
                        Response.BinaryWrite(b);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.ClearHeaders();
                Response.ClearContent();
                Response.Write(ex.Message);
            }
            r.Close();//关闭下载文件
            Response.End();//结束文件下载
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            var model = manager.GetModel(RfsId);
            model.Files = model.Files.Replace(lnk.CommandArgument.ToString()+"#", "");
            manager.Update(model);
            Response.Redirect("RequestForSealEdit.aspx?RfsId=" + RfsId);
        }

        protected void txtDataNum_TextChanged(object sender, EventArgs e)
        {
            int deptId = CurrentUser.GetDepartmentIDs().FirstOrDefault();
            if (!string.IsNullOrEmpty(txtDataNum.Text))
            {
                bool chkNum = false;
                if (txtDataNum.Text.Substring(0, 2) == "PR")
                {
                    var gList = GeneralInfoManager.GetModelList(" prNo=" + txtDataNum.Text.Trim(), null);
                    if (gList != null && gList.Count > 0)
                    {
                        deptId = gList[0].Departmentid;
                        chkNum = true;
                    }
                }
                else
                {
                    var project = ProjectManager.GetModelByProjectCode(txtDataNum.Text.Trim());
                    if (project != null)
                    {
                        deptId = project.GroupID.Value;
                        chkNum = true;
                    }
                }
                ddlDepartments.BindByLevel3Id(deptId);
                if (!chkNum)
                {
                    txtDataNum.Text = "";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('关联单据号错误，请检查！');", true);
                }
            }
            else
            {
                ddlDepartments.BindByLevel3Id(deptId);
            }
        }
    }
}