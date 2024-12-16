using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace PurchaseWeb.Gift.view
{
    public partial class ExchangeManager :ESP.Web.UI.PageBase
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllGift();
                GetMyGift();
                BindingMyPoint();
            }
        }

        private void GetAllGift()
        {
            string sqlWhere = " and State=1";
            IList<ESP.UserPoint.Entity.GiftInfo> list = ESP.UserPoint.BusinessLogic.GiftManager.GetList(sqlWhere);
            if (list != null && list.Count > 0)
            {
                //将礼品集合显示在页面中
                rpGift.DataSource = list;
                rpGift.DataBind();
            }
        }

              protected void rpExchanged_DataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo record = (ESP.UserPoint.Entity.UserPointRecordInfo)e.Item.DataItem;

                ESP.UserPoint.Entity.GiftInfo gift = ESP.UserPoint.BusinessLogic.GiftManager.GetModel(record.GiftID);
                Image imgGift = (Image)e.Item.FindControl("imgGift");
                Label lblGiftName= (Label)e.Item.FindControl("lblGiftName");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                Label lblGiftPoint = (Label)e.Item.FindControl("lblGiftPoint");

                if(gift!=null)
                {
                    imgGift.ImageUrl = gift.Images;
                    lblGiftName.Text = gift.Name;
                    lblAmount.Text = gift.Count.ToString();
                    lblGiftPoint.Text = gift.Points.ToString();
                }
            }
        }

        private void BindingMyPoint()
        {
            int userPorints = ESP.UserPoint.BusinessLogic.UserPointManager.GetModel(int.Parse(CurrentUser.SysID)).SP;
            lblMyPoints.Text = userPorints.ToString();
        }

        /// <summary>
        /// 获取当前用户已经兑换的礼品集合
        /// </summary>
        private void GetMyGift()
        {
            string a = " and UserID='" + CurrentUser.SysID + "' and GiftID <> 0";
            IList<ESP.UserPoint.Entity.UserPointRecordInfo> myGifts = ESP.UserPoint.BusinessLogic.UserPointRecordManager.GetList(a);
            rpExchanged.DataSource = myGifts;
            rpExchanged.DataBind();
        }

        protected void gvRecord_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.UserPoint.Entity.UserPointRecordInfo model = (ESP.UserPoint.Entity.UserPointRecordInfo)e.Row.DataItem;
                ESP.UserPoint.Entity.GiftInfo giftModel = ESP.UserPoint.BusinessLogic.GiftManager.GetModel(model.GiftID);

                Label lblGiftName = (Label)e.Row.FindControl("lblGiftName");
                if (lblGiftName != null)
                {
                    lblGiftName.Text = giftModel.Name;
                }
                Label lblGiftDesc = (Label)e.Row.FindControl("lblGiftDesc");
                if (lblGiftDesc != null)
                {
                    lblGiftDesc.Text = giftModel.Description;
                }
                Label lblGiftPoint = (Label)e.Row.FindControl("lblGiftPoint");
                if (lblGiftPoint != null)
                {
                    lblGiftPoint.Text = giftModel.Points.ToString();
                }
                Label lblOperationTime = (Label)e.Row.FindControl("lblOperationTime");
                if (lblOperationTime != null)
                {
                    lblOperationTime.Text = model.OperationTime.ToString("yyyy-MM-dd");
                }

            }
        }
        /// <summary>
        /// 获取用户选中的礼品id集合
        /// </summary>
        /// <returns></returns>
        private List<int> GetCheckBox()
        {
            List<int> listId = new List<int>();
            foreach (DataListItem item in this.rpGift.Items)
            {
                CheckBox chkbox = item.FindControl("cboItem") as CheckBox;
                Label lbl = item.FindControl("lblModelId") as Label;
                if (chkbox != null)
                {
                    if (chkbox.Checked)
                    {
                        listId.Add(int.Parse(lbl.Text));
                    }
                }
            }
            return listId;
        }

        /// <summary>
        /// 兑换礼品
        /// </summary>
        /// <param name="list">用户选中的礼品Id集合</param>
        private void GetSelectedPoints(List<int> list)
        {
            //获取用户选中兑换礼品所需要的总积分
            int points = 0;
            foreach (int item in list)
                points += ESP.UserPoint.BusinessLogic.GiftManager.GetModel(item).Points;
            //获取用户的总积分
            int userPorints = ESP.UserPoint.BusinessLogic.UserPointManager.GetModel(int.Parse(CurrentUser.SysID)).SP;
            if (userPorints < points)
            {
                //用户积分不足
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('您的积分不足以兑换您选中的所有礼品!');", true);
            }
            else
            {
                List<ESP.UserPoint.Entity.UserPointRecordInfo> listRecord = new List<ESP.UserPoint.Entity.UserPointRecordInfo>();
                for (int i = 0; i < list.Count; i++)
                {
                    ESP.UserPoint.Entity.UserPointRecordInfo record = new ESP.UserPoint.Entity.UserPointRecordInfo
                    {
                        GiftID = list[i],
                        Memo = "",
                        OperationTime = DateTime.Now,
                        Points = ESP.UserPoint.BusinessLogic.GiftManager.GetModel(list[i]).Points * -1,
                        UserID = int.Parse(CurrentUser.SysID)
                    };
                    listRecord.Add(record);
                }
                int result = ESP.UserPoint.BusinessLogic.UserPointRecordManager.Add(listRecord);
                if (result >0)
                {
                    Response.Write("<script languge=javascript>alert('礼品兑换成功!')</script>");
                    Response.Write("<script>window.location ='ExchangeManager.aspx'</script>");
                }
            }
        }

        //按钮事件，兑换礼品
        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            List<int> list = GetCheckBox();
            if (list.Count > 0)
            {
                GetSelectedPoints(list);
            }
            else
            {
                //没有勾选礼品
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择要兑换的礼品!');", true);
            }
        }
    }
    
}
