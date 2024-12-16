using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Common;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserProfile
{
    public partial class UserProfile : System.Web.UI.Page
    {
        private int _supplierId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                _supplierId = int.Parse(Session["user"].ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            //if(!IsPostBack)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {
            SC_Supplier model = (new SC_SupplierManager()).GetModel(_supplierId);
            if(null != model)
            {
                List<SC_SupplierInfomation> listnews = SC_SupplierInfomationDataProvider.GetListBySupplierId(model.id);
                if (null != listnews && listnews.Count > 0)
                {
                    rtNews.DataSource = listnews;
                    rtNews.DataBind();
                    labNewShowing.Visible = false;
                }
                else
                {
                    labNewShowing.Visible = true;
                }

                List<SC_SupplierPhoto> listphoto = SC_SupplierPhotoDataProvider.GetListBySupplierId(model.id);
                if (null != listphoto && listphoto.Count > 0)
                {
                    rtPhoto.DataSource = listphoto;
                    rtPhoto.DataBind();
                    labPhotoShowing.Visible = false;
                }
                else
                {
                    labPhotoShowing.Visible = true;
                }
            }
        }

        protected void rtNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                SC_SupplierInfomation news = e.Item.DataItem as SC_SupplierInfomation;

                Label labTitle = e.Item.FindControl("labTitle") as Label;
                Label labContent = e.Item.FindControl("labContent") as Label;
                Label labCreatTime = e.Item.FindControl("labCreatTime") as Label;
                Label labLastUpdateTime = e.Item.FindControl("labLastUpdateTime") as Label;

                HyperLink hypUpdateNews = e.Item.FindControl("hypUpdateNews") as HyperLink;


                if (news != null)
                {
                    labTitle.Text = news.Title;
                    labContent.Text = news.Content;
                    labCreatTime.Text = news.CreatTime.ToString("yyyy-MM-dd");
                    labLastUpdateTime.Text = news.LastUpdateTime.ToString("yyyy-MM-dd");

                    hypUpdateNews.NavigateUrl = string.Format("UserProfileUpdateNews.aspx?iid={0}", news.Id.ToString());
                }
            }
        }

        protected void rtPhoto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                SC_SupplierPhoto photo = e.Item.DataItem as SC_SupplierPhoto;

                Label labShowTxt = e.Item.FindControl("labShowTxt") as Label;
                Literal htmlPhoto = e.Item.FindControl("htmlPhoto") as Literal;
                Label labCreatTime = e.Item.FindControl("labCreatTime") as Label;
                Label labLastUpdateTime = e.Item.FindControl("labLastUpdateTime") as Label;

                if (photo != null)
                {
                    labShowTxt.Text = photo.ShowTxt;

                    if (photo.PhotoUrl.Trim().Length > 0)
                    {
                        htmlPhoto.Text = Config.BuildImageLink(photo.Id, Config.USER_ICON_DIR + photo.PhotoUrl, ImageHelper.GetPhoto_120(photo.PhotoUrl), "", "");
                    }
                    else
                    {
                        htmlPhoto.Text = "<img src='/images/nopic.gif'/>";
                    }

                    //imgPhoto.ImageUrl = photo.PhotoUrl;
                    labCreatTime.Text = photo.CreatTime.ToString("yyyy-MM-dd");
                    labLastUpdateTime.Text = photo.LastUpdateTime.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void btnAddNews_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfileUpdateNews.aspx");
        }

        protected void btnAddPhoto_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfileUpdatePhoto.aspx");
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoUpdateLinkMan.aspx");
        }

        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoUpdateLinkMan.aspx");
        }
    }
}
