using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchaseWeb.UserPointRule.view
{
    public partial class AddUserPointRule : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetRequestId();
        }

        private void GetRequestId()
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ESP.UserPoint.Entity.UserPointRuleInfo userPointRuleInfo = ESP.UserPoint.BusinessLogic.UserPointRuleManager.GetModel(id);
                    if (userPointRuleInfo != null)
                    {
                        Binding(userPointRuleInfo);
                    }
                }
            }
        }

        private void Binding(ESP.UserPoint.Entity.UserPointRuleInfo info)
        {
            textName.Text = info.RuleName;
            textKey.Text = info.RuleKey;
            textScore.Text = info.Points.ToString();
            textDesc.Text = info.Description;
            txtId.Text = info.RuleID.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = textName.Text;
            string key = textKey.Text;
            string desc = textDesc.Text;
            int score;

            if (string.IsNullOrEmpty(name))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('规则名称不能为空!');", true);
                textName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(key))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('规则编码不能为空!');", true);
                textKey.Focus();
                return;
            }
            try
            {
                score = int.Parse(textScore.Text);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请输入一个大于0的整数!');", true);
                textScore.Focus();
                return;
            }
            if (score < 1)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('积分必须大于0!');", true);
                textScore.Focus();
                return;
            }
            if (string.IsNullOrEmpty(desc))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('描述不能为空!');", true);
                textDesc.Focus();
                return;
            }

            ESP.UserPoint.Entity.UserPointRuleInfo userPointRule = new ESP.UserPoint.Entity.UserPointRuleInfo
            {
                RuleName = name,
                RuleKey = key,
                Points = score,
                Description = desc
            };

            if (string.IsNullOrEmpty(txtId.Text))
            {
                int result = ESP.UserPoint.BusinessLogic.UserPointRuleManager.Add(userPointRule);
                if (result > 0)
                {
                    Response.Write("<script>alert('添加成功！')</script>");
                    Server.Transfer("ListUserPointRule.aspx");
                }
            }
            else
            {
                userPointRule.RuleID = int.Parse(txtId.Text);
                int result = ESP.UserPoint.BusinessLogic.UserPointRuleManager.Update(userPointRule);
                if (result > 0)
                {
                    Response.Write("<script>alert('修改成功！')</script>");
                    Server.Transfer("ListUserPointRule.aspx");
                }
            }
        }


        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListUserPointRule.aspx");
        }
    }
}
