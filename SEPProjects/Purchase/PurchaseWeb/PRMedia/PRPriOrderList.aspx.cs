using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
public partial class PRPriOrderList : ESP.Web.UI.PageBase
{

    string generalids = string.Empty;
    static DataTable up3000Dt = null;
    static DataTable down3000Dt = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["GeneralID"]))
        {
            generalids = Request["GeneralID"].ToString();
            PRTopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(int.Parse(generalids));
        }
        ListBind();
    }

    private void ListBind()
    {
        up3000Dt = new DataTable();
        down3000Dt = new DataTable();

        string gvgvPriOrderStrWhere = string.Format(" and id in ({0}) ", generalids);
        //DataSet gvgvPriOrderDS = OrderInfoManager.GetListByGID(gvgvPriOrderStrWhere);
        DataSet gvgvPriOrderDS = GeneralInfoManager.GetStatusListByPriOrder(gvgvPriOrderStrWhere);

        DataColumn dc1 = new DataColumn("id");
        DataColumn dc2 = new DataColumn("prNo");
        DataColumn dc3 = new DataColumn("Item_No");
        DataColumn dc4 = new DataColumn("desctiprtion");
        DataColumn dc5 = new DataColumn("ototalprice");
        DataColumn dc6 = new DataColumn("pid");
        DataColumn dc7 = new DataColumn("requestor");
        DataColumn dc8 = new DataColumn("projectCode");
        up3000Dt.Columns.Add(dc1);
        up3000Dt.Columns.Add(dc2);
        up3000Dt.Columns.Add(dc3);
        up3000Dt.Columns.Add(dc4);
        up3000Dt.Columns.Add(dc5);
        up3000Dt.Columns.Add(dc6);
        up3000Dt.Columns.Add(dc7);
        up3000Dt.Columns.Add(dc8);

        down3000Dt = up3000Dt.Clone();
        DataRow dr = null;
        string tempCardNum = "";
        string tempBankNum = "";
        bool isNewRow = true;
        for (int i = (gvgvPriOrderDS.Tables[0].Rows.Count - 1); i >= 0; i--)
        {
                dr = up3000Dt.NewRow();
                dr["id"] = gvgvPriOrderDS.Tables[0].Rows[i]["id"].ToString();
                dr["prNo"] = gvgvPriOrderDS.Tables[0].Rows[i]["prNo"].ToString();
                dr["Item_No"] = gvgvPriOrderDS.Tables[0].Rows[i]["Item_No"].ToString();
                dr["desctiprtion"] = gvgvPriOrderDS.Tables[0].Rows[i]["desctiprtion"].ToString();
                dr["ototalprice"] = gvgvPriOrderDS.Tables[0].Rows[i]["ototalprice"].ToString();
                dr["pid"] = gvgvPriOrderDS.Tables[0].Rows[i]["pid"].ToString();
                dr["requestor"] = gvgvPriOrderDS.Tables[0].Rows[i]["requestor"].ToString();
                dr["projectCode"] = gvgvPriOrderDS.Tables[0].Rows[i]["project_code"].ToString();
                gvgvPriOrderDS.Tables[0].Rows.RemoveAt(i);
                if (decimal.Parse(dr["ototalprice"].ToString()) >= 3000)
                    up3000Dt.Rows.Add(dr);
                else
                {
                    DataRow dr1 = down3000Dt.NewRow();
                    dr1.ItemArray = dr.ItemArray;
                    down3000Dt.Rows.Add(dr1);
                }
        }
        gvMediaOrder.DataSource = up3000Dt;
        gvMediaOrder.DataBind();

        NewGridView1.DataSource = down3000Dt;
        NewGridView1.DataBind();
    }


    protected void gvMediaOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //小于3000
        string OrderIds = "";
        decimal totalPrice = 0;
        int financeID = 0;
        List<ESP.Purchase.Entity.MediaOrderOperationInfo> mediaList = new List<MediaOrderOperationInfo>();
        ESP.Purchase.Entity.GeneralInfo generalModel = null;
        ESP.Purchase.Entity.MediaOrderOperationInfo mediaModel=null;
        List<string[]> down3000RequestorMails = new List<string[]>();
        if (down3000Dt.Rows.Count > 0)
        {
            foreach (DataRow dr in down3000Dt.Rows)
            {
                OrderIds += dr["pid"].ToString() + ",";
                totalPrice += decimal.Parse(dr["ototalprice"].ToString());
            }
            if (!string.IsNullOrEmpty(Request["GeneralID"]))
            {
                generalids = Request["GeneralID"].ToString();
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(generalids));
            }
            string filePath =string.Empty;
            mediaModel = new MediaOrderOperationInfo();
            if (generalModel != null)
            {
                mediaModel.GeneralID = generalModel.id;
                mediaModel.PRNO = generalModel.PrNo;
                mediaModel.BankAccount = generalModel.account_number;
                mediaModel.BankAccountName = generalModel.account_name;
                mediaModel.BankName = generalModel.account_bank;
            }
            else
            {
                mediaModel.GeneralID = Convert.ToInt32(generalids);
            }
            mediaModel.CurrentUserID = CurrentUser.SysID;
            mediaModel.CurrentUserCode = CurrentUser.ID;
            mediaModel.CurrentUserEmpName = CurrentUser.Name;
            mediaModel.CurrentUserName = CurrentUser.ITCode;
            mediaModel.FileName = filePath;
            mediaModel.Flag = 5;
            mediaModel.TotalPrice = totalPrice;
            mediaModel.MediaOrderIDS = OrderIds.TrimEnd(',');
            mediaList.Add(mediaModel);
            if (down3000Dt.Rows[0]["requestor"] != DBNull.Value)
            {
                down3000RequestorMails.Add(new string[] { State.getEmployeeEmailBySysUserId(int.Parse(down3000Dt.Rows[0]["requestor"].ToString())), "您提交的" + down3000Dt.Rows[0]["prNo"] + "付款申请业务审核完成，已提交到财务系统！" });
            }
            financeID = GeneralInfoManager.GetFinanceAccounter(down3000Dt.Rows[0]["projectCode"].ToString());

        }
        //大于3000
        if (up3000Dt.Rows.Count > 0)
        {
            OrderIds = "";
            totalPrice = 0;
            foreach (DataRow dr in up3000Dt.Rows)
            {
                OrderIds += dr["pid"].ToString() + ",";
                totalPrice += decimal.Parse(dr["ototalprice"].ToString());
            }
            if (!string.IsNullOrEmpty(Request["GeneralID"]))
            {
                generalids = Request["GeneralID"].ToString();
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(generalids));
            }
            string filePath1 =string.Empty;
            mediaModel = new MediaOrderOperationInfo();
            if (generalModel != null)
            {
                mediaModel.GeneralID = generalModel.id;
                mediaModel.PRNO = generalModel.PrNo;
                mediaModel.BankAccount = generalModel.account_number;
                mediaModel.BankAccountName = generalModel.account_name;
                mediaModel.BankName = generalModel.account_bank;
            }
            else
            {
                mediaModel.GeneralID = Convert.ToInt32(generalids);
            }
            mediaModel.CurrentUserID = CurrentUser.SysID;
            mediaModel.CurrentUserCode = CurrentUser.ID;
            mediaModel.CurrentUserEmpName = CurrentUser.Name;
            mediaModel.CurrentUserName = CurrentUser.ITCode;
            mediaModel.FileName = filePath1;
            mediaModel.Flag = 4;
            mediaModel.TotalPrice = totalPrice;
            mediaModel.MediaOrderIDS = OrderIds.TrimEnd(',');
            mediaList.Add(mediaModel);
        }
        if (mediaList != null && mediaList.Count > 0)
        {
            if (ESP.Purchase.BusinessLogic.MediaOrderManager.MediaOrderOperation(mediaList) > 0)
            {
                if (down3000RequestorMails.Count > 0)//小于3000的给原申请人发邮件
                {
                    foreach (string[] item in down3000RequestorMails)
                    {
                        ESP.ConfigCommon.SendMail.Send1("付款申请业务审核完成", item[0].ToString(), item[1].ToString(), false, "");
                    }
                }
                if (financeID != 0)
                {
                    ESP.ConfigCommon.SendMail.Send1("媒介付款申请提交完成", State.getEmployeeEmailBySysUserId(financeID), down3000Dt.Rows[0]["prNo"] + "媒介付款申请完成，已提交到财务系统！", false, "");
                }
                ShowCompleteMessage("创建成功,3000以下已经提交到财务系统，3000以上请查看新PR单！", "PRPriList.aspx");
            }
            else
            {
                ShowCompleteMessage("创建失败！", "PRPriOrderList.aspx");
            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PRPriList.aspx");
    }
}
