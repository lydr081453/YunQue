using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ComponentArt.Web.UI;

namespace AdministrativeWeb.Attendance
{
    public partial class CardNoList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = BingInfo();
                Grid1.DataSource = ds;
                Grid1.DataBind();
                LoadDepartmentInfo();
            }
        }

        /// <summary>
        /// 绑定门卡数据信息
        /// </summary>
        protected DataSet BingInfo()
        {
            StringBuilder strBuilder = new StringBuilder();
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            // 姓名
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                strBuilder.Append(" AND u.EmployeeName like '%'+@UserName+'%' ");
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@UserName", txtUserName.Text.Trim()));
            }
            // 卡号
            if (!string.IsNullOrEmpty(txtCardNo.Text))
            {
                strBuilder.Append(" AND u.CardNo LIKE '%'+@CardNo+'%' ");
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@CardNo", txtCardNo.Text.Trim()));
            }
            // 启用时间
            if (PickerFrom.SelectedDate != null && PickerFrom.SelectedDate.ToString() != "" && PickerFrom.SelectedDate != DateTime.MinValue
                && PickerTo.SelectedDate != null && PickerTo.SelectedDate.ToString() != "" && PickerTo.SelectedDate != DateTime.MinValue)
            {
                strBuilder.Append(" AND (u.CardEnableTime BETWEEN @BeginEnableTime AND @EndEnableTime) ");
                SqlParameter p2 = new SqlParameter("@BeginEnableTime", SqlDbType.DateTime, 8);
                p2.Value = PickerFrom.SelectedDate;
                paramlist.Add(p2);

                SqlParameter p3 = new SqlParameter("@EndEnableTime", SqlDbType.DateTime, 8);
                p3.Value = PickerTo.SelectedDate;
                paramlist.Add(p3);
            }
            // 停用时间
            if (PickerFrom1.SelectedDate != null && PickerFrom1.SelectedDate.ToString() != "" && PickerFrom1.SelectedDate != DateTime.MinValue
                && PickerTo1.SelectedDate != null && PickerTo1.SelectedDate.ToString() != "" && PickerTo1.SelectedDate != DateTime.MinValue)
            {
                strBuilder.Append(" AND (u.CardUnEnableTime BETWEEN @BeginUnEnableTime AND @EndUnEnableTime) ");
                SqlParameter p2 = new SqlParameter("@BeginUnEnableTime", SqlDbType.DateTime, 8);
                p2.Value = PickerFrom1.SelectedDate;
                paramlist.Add(p2);

                SqlParameter p3 = new SqlParameter("@EndUnEnableTime", SqlDbType.DateTime, 8);
                p3.Value = PickerTo1.SelectedDate;
                paramlist.Add(p3);
            }
            // 状态
            if (drpEnable.SelectedValue != "0")
            {
                strBuilder.Append(" AND u.CardState=@CardState ");
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@CardState", drpEnable.SelectedValue));
            }
            // 员工编号
            if (!string.IsNullOrEmpty(txtUserCode.Text))
            {
                strBuilder.Append(" AND u.UserCode=@UserCode ");
                SqlParameter p = new SqlParameter("@UserCode", SqlDbType.NVarChar);
                p.Value = txtUserCode.Text.Trim();
                paramlist.Add(p);
            }
            // 分公司
            if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
            {
                strBuilder.Append(" AND d.level1id=@level1id ");
                SqlParameter p = new SqlParameter("@level1id", SqlDbType.NVarChar);
                p.Value = cbCompany.SelectedValue;
                paramlist.Add(p);
            }
            // 团队
            if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
            {
                strBuilder.Append(" AND d.level2id=@level2id ");
                SqlParameter p = new SqlParameter("@level2id", SqlDbType.NVarChar);
                p.Value = cbDepartment1.SelectedValue;
                paramlist.Add(p);
            }
            // 部门
            if (!string.IsNullOrEmpty(cbDepartment2.SelectedValue))
            {
                strBuilder.Append(" AND d.level3id=@level3id ");
                SqlParameter p = new SqlParameter("@level3id", SqlDbType.NVarChar);
                p.Value = cbDepartment2.SelectedValue;
                paramlist.Add(p);
            }
            
            UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
            ESP.Framework.Entity.DepartmentInfo model = userAttBasicManager.GetRootDepartmentID(UserID);
            int areaid = 0;
            if (model != null)
            {
                areaid = model.DepartmentID;
            }
            strBuilder.Append(" AND u.AreaID=@AreaID");
            paramlist.Add(new SqlParameter("@AreaID", areaid));
            return userAttBasicManager.GetCardNoInfos(strBuilder.ToString(), paramlist);
        }

        /// <summary>
        /// 进入门卡管理页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = BingInfo();
            FileHelper.ExprotCardNos(ds, Server.MapPath("~"), Response);
        }

        /// <summary>
        /// 检索门卡信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = BingInfo();
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        /// <summary>
        /// 获得门卡使用状态文字描述
        /// </summary>
        /// <param name="cardstate">门卡状态</param>
        /// <returns>返回门卡状态文字描述</returns>
        public string GetCardState(string cardstate)
        {
            string state = "";
            if (cardstate == ((int)CardUseState.Enable).ToString())
            {
                state = "启用";
            }
            else
            {
                state = "停用";
            }
            return state;
        }

        /// <summary>
        /// 门卡库存信息
        /// </summary>
        public string CardStoreCount
        {
            get
            {
                int areaid = new UserAttBasicInfoManager().GetRootDepartmentID(UserID).DepartmentID;
                int count = new CardStoreManager().GetCardStoreCount(areaid);
                if (count <= 5)
                {
                    return "<span style=\"color: Red;font-weight:bold\">库存中剩余的门卡数为：" + count + "张。</span>";
                }
                else
                {
                    return "<span style=\"font-weight:bold\">库存中剩余的门卡数为：" + count + "张。</span>";
                }
            }
        }

        /// <summary>
        /// 获得编辑链接
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回编辑链接</returns>
        public string GetEditUrl(string userId)
        {
            return "<a href='CardNoEdit.aspx?ApplicantID=" + userId + "'><img src='../images/edit.gif' /></a>";
        }

        #region 部门信息加载
        /// <summary>
        /// 加载一级部门
        /// </summary>
        public void LoadDepartmentInfo()
        {
            IList<DepartmentInfo> deptlist = DepartmentManager.GetAll();
            UserAttBasicInfoManager userAttBasicManager = new UserAttBasicInfoManager();
            ESP.Framework.Entity.DepartmentInfo model = userAttBasicManager.GetRootDepartmentID(UserID);
            int areaid = (int)AreaID.HeadOffic;
            if (model != null)
            {
                areaid = model.DepartmentID;
            }
            if (deptlist != null && deptlist.Count > 0)
            {
                foreach (DepartmentInfo dept in deptlist)
                {
                    if (dept.DepartmentLevel == 1 && areaid == dept.DepartmentID)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Text = dept.DepartmentName;
                        item.Value = dept.DepartmentID.ToString();
                        cbCompany.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 根据用户选择的一级部门加载相应的二级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
                {
                    int companyId = int.Parse(cbCompany.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(companyId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment1.Items.Clear();
                        cbDepartment1.SelectedItem = null;
                        cbDepartment1.Text = "";
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }

        /// <summary>
        /// 根据用户选择的二级部门加载相应的三级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
                {
                    int departmentId = int.Parse(cbDepartment1.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(departmentId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment2.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }
        #endregion 
    }
}