using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchaseWeb.UserControls.Edit
{
    public partial class Score : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetControls(ESP.Purchase.Entity.ScoreContentInfo model)
        {
            litContentName.Text = model.Description;
            litContentName.ToolTip = System.Web.HttpContext.Current.Server.HtmlDecode(model.Remark);
            hidContentId.Value = model.ScoreContentID.ToString();
            List<ESP.Purchase.Entity.ScoreInfo> scoreList = ESP.Purchase.BusinessLogic.ScoreManager.GetListByContentId(model.ScoreContentID);
            foreach (ESP.Purchase.Entity.ScoreInfo score in scoreList)
            {
                ddlScore.Items.Add(new ListItem(score.ScoreName + "&nbsp;&nbsp;" + score.Description , score.ScoreID.ToString()));
            }
            //ddlScore.SelectedIndex = 0;
            CV1.ErrorMessage = "请对供应商"+model.Description+"进行评价";
        }

        protected void ddlScore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ESP.Purchase.Entity.ScoreInfo model = ESP.Purchase.BusinessLogic.ScoreManager.GetModel(int.Parse(ddlScore.SelectedValue));
            if (model.IsNeedRemark)
            {
                P.Visible = true;
                RV1.Enabled = true;
            }
            else
            {
                P.Visible = false;
                RV1.Enabled = false;
            }
        }

        public int GetScore()
        {
            if (string.IsNullOrEmpty(ddlScore.SelectedValue))
            {
                return -1;
            }
            ESP.Purchase.Entity.ScoreInfo scoreModel = ESP.Purchase.BusinessLogic.ScoreManager.GetModel(int.Parse(ddlScore.SelectedValue));
            return scoreModel.Scores; 
        }

        public ESP.Purchase.Entity.ScoreRecordInfo GetRecordInfo(ESP.Purchase.Entity.GeneralInfo generalModel)
        {
            ESP.Purchase.Entity.ScoreRecordInfo score = new ESP.Purchase.Entity.ScoreRecordInfo();
            ESP.Purchase.Entity.ScoreInfo scoreModel = ESP.Purchase.BusinessLogic.ScoreManager.GetModel(int.Parse(ddlScore.SelectedValue));
            score.PRID = generalModel.id;
            score.PRNO = generalModel.PrNo;
            score.ScoreContent = litContentName.Text.Trim();
            score.ScoreContentID = int.Parse(hidContentId.Value);
            score.ScoreID = int.Parse(ddlScore.SelectedValue);
            score.ScoreName = scoreModel.ScoreName;
            score.Scores = scoreModel.Scores;
            int supplierId = 0;
            string supplierName = "";
            ESP.Purchase.BusinessLogic.OrderInfoManager.getSupplierId(" and general_id=" + generalModel.id, out supplierId, out supplierName);
            score.SupplierID = supplierId;
            score.SupplierName = supplierName;
            ESP.Purchase.Entity.ScoreInfo s = ESP.Purchase.BusinessLogic.ScoreManager.GetModel(score.ScoreID);
            if (s.IsNeedRemark)
                score.Remark = txtRemark.Text.Trim();
            return score;
        }
    }
}