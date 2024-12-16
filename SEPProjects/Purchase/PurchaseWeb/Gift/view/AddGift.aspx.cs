using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchaseWeb.Gift.view
{
    public partial class AddGift : ESP.Web.UI.PageBase
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
                    ESP.UserPoint.Entity.GiftInfo giftInfo = ESP.UserPoint.BusinessLogic.GiftManager.GetModel(id);
                    if (giftInfo != null)
                    {
                        Binding(giftInfo);
                    }
                }
            }
        }

        private void Binding(ESP.UserPoint.Entity.GiftInfo info)
        {
            txtCode.Text = info.Code;
            txtName.Text = info.Name;
            txtDesc.Text = info.Description;
            txtPorints.Text = info.Points.ToString();
            txtCount.Text = info.Count.ToString();
            ddlStatus.SelectedValue = info.State.ToString();
            txtId.Text = info.GiftID.ToString();
            txtImgUrl.Text = info.Images;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string imageUrl = btnImage.Value;
            string code = txtCode.Text;
            string name = txtName.Text;
            string desc = txtDesc.Text;
            string state = ddlStatus.SelectedValue;
            int score = 0;
            int count = 0;
            if (string.IsNullOrEmpty(code))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('礼品编码不能为空!');", true);
                txtCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('礼品名称不能为空!');", true);
                txtName.Focus();
                return;
            }
            try
            {
                score = int.Parse(txtPorints.Text);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('积分必须是大于0的整数!');", true);
            }

            try
            {
                count = int.Parse(txtCount.Text);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('数量必须是大于0的整数!');", true);
            }

            if (score < 1)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请输入一个大于0的整数!');", true);
                txtPorints.Focus();
                return;
            }
            if (count < 1)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请输入一个大于0的整数!');", true);
                txtCount.Focus();
                return;
            }
            if (string.IsNullOrEmpty(desc))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('礼品描述不能为空!');", true);
                txtDesc.Focus();
                return;
            }
            string filePath="";
            if (string.IsNullOrEmpty(txtId.Text))
            {
                if (string.IsNullOrEmpty(imageUrl))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('点击浏览选择一个礼品图片!');", true);
                    return;
                }
                HttpContext context = new HttpContext(Request, Response);
                filePath = UploadFile(context);
                if (string.IsNullOrEmpty(filePath))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('您上传的图片必须是[png,bmp,jpg,gif]格式!');", true);
                    return;
                }
            }

            ESP.UserPoint.Entity.GiftInfo giftInfo = new ESP.UserPoint.Entity.GiftInfo
            {
                Code = code,
                CreateTime = DateTime.Now,
                Count = count,
                Description = desc,
                Name = name,
                Points = score,
                State = int.Parse(state),
                Creator = int.Parse(CurrentUser.SysID)
            };
            if (string.IsNullOrEmpty(txtId.Text))
            {
                giftInfo.Images = filePath;
                int result = ESP.UserPoint.BusinessLogic.GiftManager.Add(giftInfo);
                if (result > 0)
                {
                    Response.Write("<script>alert('添加成功！')</script>");
                    Server.Transfer("ListGift.aspx");
                }
            }
            else
            {
                giftInfo.GiftID = int.Parse(txtId.Text);
                giftInfo.Images = txtImgUrl.Text;
                int result = ESP.UserPoint.BusinessLogic.GiftManager.Update(giftInfo);
                if (result == 1)
                {
                    Response.Write("<script>alert('修改成功！')</script>");
                    Server.Transfer("ListGift.aspx");
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListGift.aspx");
        }
        
        public string UploadFile(HttpContext context)
        {
            string result = string.Empty;
            HttpFileCollection fileCollection = context.Request.Files;//获取上传文件的集合
            string dirpath = Server.MapPath("~/Gift/uploadImages/");
            if (fileCollection.Count != 0)
            {
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile file = fileCollection[i];//获取单个文件
                    if (file.ContentLength == 0) //如果该文件大小为0
                    {
                        continue;
                    }
                    string fileName = string.Empty;
                    string fileExtention = string.Empty;
                    fileExtention = System.IO.Path.GetExtension(file.FileName); //获取文件后缀
                    if (fileExtention.Equals(".png") || fileExtention.Equals(".bmp") || fileExtention.Equals(".jpg") || fileExtention.Equals(".gif"))
                    {
                        fileName = CurrentUser.SysID + "_" + file.FileName;
                        dirpath = (dirpath + fileName).Replace(@"\\", @"\");
                        file.SaveAs(dirpath);
                        result = "/Gift/uploadImages/" + fileName;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            return result;
        }
    }
}
