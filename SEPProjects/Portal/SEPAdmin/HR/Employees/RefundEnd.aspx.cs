using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Common;
using System.Data;

namespace SEPAdmin.HR.Employees
{
    public partial class RefundEnd : ESP.Web.UI.PageBase
    {
        private int _refundid = 0;
        private int _userid = 0;
        /// <summary>
        /// flag值为1时表示要进行启动操作、为2时表示要进行结束操作
        /// </summary>
        private int _flag = 0;
        public string flagStr = "结束";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["flag"]))
            {
                _flag = int.Parse(Request["flag"]);
                hidFlag.Value = _flag.ToString();
                if (_flag == 1)
                {
                    flagStr = "启动";
                    RequiredFieldValidator4.ErrorMessage = "请选择租赁启动日期";
                }
                else
                {
                    flagStr = "结束";
                    RequiredFieldValidator4.ErrorMessage = "请选择租赁结束日期";
                }
            }
            if (!string.IsNullOrEmpty(Request["rid"]))
            {
                _refundid = int.Parse(Request["rid"]);
                BindInfo(Request["rid"]);
            }
        }

        private void BindInfo(string rid)
        {
            try
            {
                DataSet ds = new ESP.Administrative.BusinessLogic.RefundManager().GetListByUser(" and r.id=" + rid, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                    labUserName.Text = ds.Tables[0].Rows[0]["lastnamecn"].ToString() + ds.Tables[0].Rows[0]["firstnamecn"].ToString();
                }
            }
            catch { }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            string hidFlagValue = hidFlag.Value;
            if (hidFlagValue == "1")
                StartRefund();
            else
                EndRefund();
        }

        /// <summary>
        /// 启动笔记本租赁
        /// </summary>
        public void StartRefund()
        {
            try
            {
                RefundInfo model = new RefundManager().GetModel(_refundid);

                model.BeginOperator = UserInfo.UserID.ToString();
                model.BeginTime = DateTime.Parse(txtEndTime.Text);
                model.LastUpdateTime = DateTime.Now;
                model.LastUpdater = UserInfo.FullNameCN;
                model.LastUpdaterIP = HttpContext.Current.Request.UserHostAddress;
                model.Status = (int)RefundStatus.BeginStatus;   // 将笔记本租赁设置为启动状态
                new RefundManager().Update(model);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁启动成功！');window.location='ITRefundList.aspx';", true);

            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁启动失败！');", true);
            }
        }

        /// <summary>
        /// 结束笔记本租赁
        /// </summary>
        public void EndRefund()
        {
            try
            {
                RefundInfo model = new RefundManager().GetModel(_refundid);

                DateTime endTime = DateTime.Parse(txtEndTime.Text);
                if (endTime < model.BeginTime)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁结束时间不能小于租赁开始时间！');", true);
                    return;
                }
                model.EndOperator = UserInfo.UserID.ToString();
                model.EndTime = endTime;
                model.LastUpdateTime = DateTime.Now;
                model.LastUpdater = UserInfo.FullNameCN;
                model.LastUpdaterIP = HttpContext.Current.Request.UserHostAddress;
                model.Status = (int)RefundStatus.EndStatus;

                new RefundManager().Update(model);

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁结束成功！');window.location='ITRefundList.aspx';", true);

            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁结束失败！');", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ITRefundList.aspx");
        }
    }
}
