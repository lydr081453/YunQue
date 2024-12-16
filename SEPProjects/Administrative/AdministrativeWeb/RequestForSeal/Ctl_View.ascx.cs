using ESP.Administrative.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.RequestForSeal
{
    public partial class Ctl_View : System.Web.UI.UserControl
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
            if (RfsId > 0)
            {
                var model = manager.GetModel(RfsId);
                //if (int.Parse(CurrentUser.SysID) != model.RequestorId)
                //{
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('您没有该数据的权限！');window.location.href='RequestForSealList.aspx';", true);
                //    return;
                //}
                labSANo.Text = model.SANo;
                labRequestorName.Text = model.RequestorName;
                txtDataNum.Text = model.DataNum;
                ddlBrandch.Text = model.BranchName;
                ddlDepartments.Text = model.DeptName1 + "-" + model.DeptName2 + "-" + model.DeptName3;
                PickerFrom1.Text = model.RequestDate.ToString("yyyy-MM-dd") ;
                ddlSealType.Text = model.SealType;
                ddlFileType.Text = model.FileType;
                txtFileName.Text = model.FileName;
                txtFileQuantity.Text = model.FileQuantity.ToString();
                txtRemark.Text = model.Remark;
                if (!string.IsNullOrEmpty(model.Files))
                {
                    repFiles.DataSource = model.Files.Trim('#').Split('#');
                    repFiles.DataBind();
                }
            }
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

    }
}