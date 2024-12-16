using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class ContrSupplement : ESP.Purchase.WebPage.EditPageForPR
    {
        int generalid = 0;
        GeneralInfo geralModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
            {
                generalid = int.Parse(Request[RequestName.GeneralID]);
                geralModel = GeneralInfoManager.GetModel(generalid);

                productInfo.CurrentUserId = CurrentUserID;
            }
          
            if (!IsPostBack)
            {
                BindInfo();

            }
        }

        /// <summary>
        /// Binds the info.
        /// </summary>
        public void BindInfo()
        {
           
            if (null != geralModel)
            {
                GenericInfo.Model = geralModel;
                GenericInfo.BindInfo();

                projectInfo.PurchaseAuditor = UserID;
                projectInfo.Model = geralModel;
                projectInfo.BindInfo();

                productInfo.Model = geralModel;
                productInfo.BindInfo();

                supplierInfo.Model = geralModel;
                supplierInfo.BindInfo();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveOrder() > 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('合同附件补充说明追加成功!');window.close();", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('操作发生错误，请检查!');", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "window.close();", true);
        }


        private int SaveOrder()
        {
            if(geralModel==null)
                geralModel = GeneralInfoManager.GetModel(generalid);
            OrderInfo firstModel = OrderInfoManager.GetModelByGeneralID(geralModel.id);

            OrderInfo model = new OrderInfo();
            model.general_id = geralModel.id;
            model.Item_No = txtRemark.Text;
            model.desctiprtion = txtRemark.Text;
            model.intend_receipt_date = DateTime.Now.ToString("yyyy-MM-dd");
            model.price = model.oldPrice = 0;
            model.uom = "";
            model.quantity = model.oldQuantity = 0;
            model.total = 0;
            model.producttype = firstModel.producttype;
            model.supplierId = firstModel.supplierId;
            model.supplierName = firstModel.supplierName;
            model.productAttribute = (int)State.productAttribute.fml;

            if (string.IsNullOrEmpty(model.upfile))
                model.upfile = "";

            //添加寻价附件
            if (this.FileUpML.FileName != string.Empty)
            {
                string fileName = "wuliao_" + Guid.NewGuid().ToString() + "_" + this.FileUpML.FileName;
                string urlHost = System.Configuration.ConfigurationManager.AppSettings["UpFilePath"] + @"upfile\";
                this.FileUpML.SaveAs(urlHost + fileName);
                if (string.IsNullOrEmpty(txtRemark.Text))
                {
                    model.Item_No = this.FileUpML.FileName.Substring(0, this.FileUpML.FileName.IndexOf("."));
                    model.desctiprtion = model.Item_No;
                }
                model.upfile = @"upfile\" + fileName;
            }

            if (!string.IsNullOrEmpty(model.upfile))
                model.upfile = model.upfile.TrimEnd('#');

            return OrderInfoManager.addByTrans(model);
        }


    }
}