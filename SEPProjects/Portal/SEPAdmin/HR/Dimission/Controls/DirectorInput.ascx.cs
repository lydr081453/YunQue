using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Dimission.Controls
{
    public partial class DirectorInput : ESP.Web.UI.UserControlBase
    {
        private int dimissionid = 0;
        public int DimissionId
        {
            get { return dimissionid; }
            set { dimissionid = value; }
        }
        /// <summary>
        /// 离职单据信息
        /// </summary>
        public Dictionary<int, ESP.HumanResource.Entity.DimissionDetailsInfo> DicDimission
        {
            get
            {
                return this.ViewState["DicDimission"] == null ? null :
                    (Dictionary<int, ESP.HumanResource.Entity.DimissionDetailsInfo>)this.ViewState["DicDimission"];
            }
            set
            {
                this.ViewState["DicDimission"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage(DimissionId);
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage(int dimissionid)
        {
            ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionid);  // 获得当前登录用户的离职单信息
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> list = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionData(dimissionFormInfo.UserId, dimissionFormInfo.DimissionId);
            SetDimissionInfo(list);
            gvDetailList.DataSource = list;
            gvDetailList.DataBind();
            labAllNum.Text = labAllNumT.Text = gvDetailList.Rows.Count.ToString();

            if (gvDetailList.Rows.Count > 0)
            {
                pnlBottom.Visible = true;
                pnlTop.Visible = true;
            }
            else
            {
                pnlBottom.Visible = false;
                pnlTop.Visible = false;
            }
        }

        /// <summary>
        /// 设置离职单据信息
        /// </summary>
        protected void SetDimissionInfo(List<ESP.HumanResource.Entity.DimissionDetailsInfo> list)
        {
            if (list != null && list.Count > 0)
            {
                Dictionary<int, ESP.HumanResource.Entity.DimissionDetailsInfo> dimissionInfo = new Dictionary<int, ESP.HumanResource.Entity.DimissionDetailsInfo>();
                foreach (ESP.HumanResource.Entity.DimissionDetailsInfo dimissionDetailInfo in list)
                {
                    if (!dimissionInfo.ContainsKey(dimissionDetailInfo.FormId))
                    {
                        dimissionInfo.Add(dimissionDetailInfo.FormId, dimissionDetailInfo);
                    }
                }
                DicDimission = dimissionInfo;
            }
        }

        protected void gvDetailList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                Label lab = e.Row.FindControl("labReceiverName") as Label;
                HiddenField hid = e.Row.FindControl("hidReceiverName") as HiddenField;
                ESP.HumanResource.Entity.DimissionDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.DimissionDetailsInfo;
                lab.Text = detailsInfo.ReceiverName;
                hid.Value = detailsInfo.ReceiverId.ToString();

            }
        }

        /// <summary>
        /// 从当前页收集选中项的情况
        /// </summary>
        protected void CollectSelected()
        {
            for (int i = 0; i < this.gvDetailList.Rows.Count; i++)
            {
                int formId = 0;
                if (!int.TryParse(gvDetailList.DataKeys[i].Value.ToString(), out formId))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                        "alert('系统出现错误，请与系统管理员联系');", true);
                    ESP.Logging.Logger.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + gvDetailList.DataKeys[i].Value + "转换成int型数据时出现错误(登录人：" + UserInfo.Username + ")",
                            "人事管理", ESP.Logging.LogLevel.Information);
                    return;
                }
                if (DicDimission.ContainsKey(formId))
                {
                    HiddenField hid = this.gvDetailList.Rows[i].FindControl("hidReceiverName") as HiddenField;
                    string val = hid.Value;
                    int receiverId = int.Parse(val);
                    ESP.HumanResource.Entity.DimissionDetailsInfo dimissionDetailInfo = DicDimission[formId];
                    dimissionDetailInfo.ReceiverId = receiverId;
                    dimissionDetailInfo.ReceiverName = ESP.Framework.BusinessLogic.UserManager.Get(receiverId).FullNameCN;
                    IList<ESP.Framework.Entity.EmployeePositionInfo> empPositionList = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(receiverId);
                    if (empPositionList != null && empPositionList.Count > 0)
                    {
                        dimissionDetailInfo.ReceiverDepartmentId = empPositionList[0].DepartmentID;
                        dimissionDetailInfo.ReceiverDepartmentName = empPositionList[0].DepartmentName;
                    }
                }
            }
        }
    }
}