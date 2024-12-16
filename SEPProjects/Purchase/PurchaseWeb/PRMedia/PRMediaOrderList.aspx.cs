using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
public partial class PRMedia_PRMediaOrderList : ESP.Web.UI.PageBase
{
    string generalids = string.Empty;
    static DataTable up3000Dt = null;
    static DataTable down3000Dt = null;
    static DataTable taxDt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["GeneralID"]))
        {
            generalids = Request["GeneralID"].ToString();
            PRTopMessage.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(int.Parse(generalids));
        }
        ListBind();
    }

    private string GetBankCount(string bankcount)
    {
        string ret = string.Empty;
        int tryvalue;
        for (int i = 0; i < bankcount.Length; i++)
        {
            if (int.TryParse(bankcount.Substring(i, 1),out tryvalue))
            {
                ret += tryvalue.ToString();
            }
        }
        return ret;
    }

    private void ListBind()
    {
        up3000Dt = new DataTable();
        down3000Dt = new DataTable();
        taxDt = new DataTable();
        generalids = Request["GeneralID"].ToString();
        string gvgvMediaOrderStrWhere = string.Empty;
        if (string.IsNullOrEmpty(generalids))
        {
            gvgvMediaOrderStrWhere = string.Format(" b.billtype={0} and c.status=0",  ((int)BillType.WritingFeeBill).ToString());
        }
        else
            gvgvMediaOrderStrWhere = string.Format(" a.id in ({0}) and b.billtype={1} and c.status=0", generalids, ((int)BillType.WritingFeeBill).ToString());

        DataSet gvgvMediaOrderDS = MediaOrderManager.GetListByGID(gvgvMediaOrderStrWhere);

        DataColumn dc1 = new DataColumn("id");
        DataColumn dc2 = new DataColumn("prNo");
        DataColumn dc3 = new DataColumn("CardNumber");
        DataColumn dc4 = new DataColumn("receiverName");
        DataColumn dc5 = new DataColumn("totalAmount");
        DataColumn dc6 = new DataColumn("mediaOrderIds");
        DataColumn dc7 = new DataColumn("requestor");
        DataColumn dc8 = new DataColumn("projectCode");
        DataColumn dc9 = new DataColumn("IsTax");
        up3000Dt.Columns.Add(dc1);
        up3000Dt.Columns.Add(dc2);
        up3000Dt.Columns.Add(dc3);
        up3000Dt.Columns.Add(dc4);
        up3000Dt.Columns.Add(dc5);
        up3000Dt.Columns.Add(dc6);
        up3000Dt.Columns.Add(dc7);
        up3000Dt.Columns.Add(dc8);
        up3000Dt.Columns.Add(dc9);
        down3000Dt = up3000Dt.Clone();
        taxDt = up3000Dt.Clone();
        DataRow dr = null;
        string tempCardNum = "";
        string tempreporterName = "";
        bool isNewRow = true;
        bool isTax = false;
        for (int i = (gvgvMediaOrderDS.Tables[0].Rows.Count - 1); i >= 0; i--)
        {

            if (isNewRow)
            {
                dr = up3000Dt.NewRow();
                tempCardNum = this.GetBankCount(gvgvMediaOrderDS.Tables[0].Rows[i]["BankAccountName"].ToString());

                tempreporterName = gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"] == DBNull.Value ? gvgvMediaOrderDS.Tables[0].Rows[i]["reporterName"].ToString().Trim() : gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"].ToString().Trim();
                if (dr["IsTax"] != DBNull.Value)
                    isTax = dr["IsTax"].ToString().Trim() == "True" ? true : false;
                dr["id"] = gvgvMediaOrderDS.Tables[0].Rows[i]["gid"].ToString();
                dr["prNo"] = gvgvMediaOrderDS.Tables[0].Rows[i]["prNo"].ToString();
                dr["CardNumber"] = gvgvMediaOrderDS.Tables[0].Rows[i]["CardNumber"].ToString();
                dr["receiverName"] = tempreporterName;
                dr["totalAmount"] = gvgvMediaOrderDS.Tables[0].Rows[i]["totalAmount"].ToString();
                dr["mediaOrderIds"] = gvgvMediaOrderDS.Tables[0].Rows[i]["MeidaOrderID"].ToString();
                dr["requestor"] = gvgvMediaOrderDS.Tables[0].Rows[i]["requestor"].ToString();
                dr["projectCode"] = gvgvMediaOrderDS.Tables[0].Rows[i]["project_code"].ToString();
                dr["IsTax"] = gvgvMediaOrderDS.Tables[0].Rows[i]["IsTax"].ToString();
                gvgvMediaOrderDS.Tables[0].Rows.RemoveAt(i);
                isNewRow = false;
            }
            else
            {
                if (dr["IsTax"] != DBNull.Value)
                    isTax = dr["IsTax"].ToString().Trim() == "True" ? true : false;
                string tempCheckString = "";
                if (gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"] == DBNull.Value)
                    tempCheckString = gvgvMediaOrderDS.Tables[0].Rows[i]["reporterName"].ToString().Trim();
                else
                    tempCheckString = gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"].ToString().Trim();
                if (this.GetBankCount(gvgvMediaOrderDS.Tables[0].Rows[i]["BankAccountName"].ToString()) == tempCardNum && isTax == false)
                {
                    dr["mediaOrderIds"] += "," + gvgvMediaOrderDS.Tables[0].Rows[i]["MeidaOrderID"].ToString();
                    dr["totalAmount"] = decimal.Parse(dr["totalAmount"].ToString()) + decimal.Parse(gvgvMediaOrderDS.Tables[0].Rows[i]["totalAmount"].ToString());
                    gvgvMediaOrderDS.Tables[0].Rows.RemoveAt(i);
                }
            }
            if (i == 0)
            {
                if (isTax == false)
                {
                    if (decimal.Parse(dr["totalAmount"].ToString()) >= 3000)
                        up3000Dt.Rows.Add(dr);
                    else
                    {
                        DataRow dr1 = down3000Dt.NewRow();
                        dr1.ItemArray = dr.ItemArray;
                        down3000Dt.Rows.Add(dr1);
                    }
                }
                else
                {
                    DataRow dr1 = taxDt.NewRow();
                    dr1.ItemArray = dr.ItemArray;
                    taxDt.Rows.Add(dr1);
                }
                i = gvgvMediaOrderDS.Tables[0].Rows.Count;
                isNewRow = true;
            }
        }
        gvMediaOrder.DataSource = up3000Dt;
        gvMediaOrder.DataBind();

        NewGridView1.DataSource = down3000Dt;
        NewGridView1.DataBind();

        NewGridView2.DataSource = taxDt;
        NewGridView2.DataBind();
    }


    protected void gvMediaOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //小于3000
        string mediaOrderIds = "";
        decimal totalPrice = 0;
        int financeID = 0;
        List<ESP.Purchase.Entity.MediaOrderOperationInfo> mediaList = new List<MediaOrderOperationInfo>();
        ESP.Purchase.Entity.GeneralInfo generalModel = null;
        ESP.Purchase.Entity.MediaOrderOperationInfo mediaModel;
        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        List<string[]> down3000RequestorMails = new List<string[]>();
        //3000以下
        if (down3000Dt.Rows.Count > 0)
        {

            foreach (DataRow dr in down3000Dt.Rows)
            {
                mediaOrderIds += dr["mediaOrderIds"].ToString() + ",";
                totalPrice += decimal.Parse(dr["totalAmount"].ToString());
            }
            if (!string.IsNullOrEmpty(Request["GeneralID"]))
            {
                generalids = Request["GeneralID"].ToString();
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(generalids));
            }
            string filePath = FileHelper.SavePage(mediaOrderIds.TrimEnd(','), Server.MapPath("~"));
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
            mediaModel.Flag = 2;
            mediaModel.TotalPrice = totalPrice;
            mediaModel.MediaOrderIDS = mediaOrderIds.TrimEnd(',');
            mediaList.Add(mediaModel);
            //System.Collections.ArrayList ht1 = MediaOrderManager.GetReturnInMediaOrderSql(mediaOrderIds.TrimEnd(','), totalPrice, CurrentUser.SysID, CurrentUserCode, CurrentUser.ITCode, CurrentUser.Name, filePath);
            //ht.Add(ht1[0], ht1[1]);
            if (down3000Dt.Rows[0]["requestor"] != DBNull.Value)
            {
                down3000RequestorMails.Add(new string[] { State.getEmployeeEmailBySysUserId(int.Parse(down3000Dt.Rows[0]["requestor"].ToString())), "您提交的" + down3000Dt.Rows[0]["prNo"] + "付款申请业务审核完成，已提交到财务系统！" });
            }
            financeID = GeneralInfoManager.GetFinanceAccounter(down3000Dt.Rows[0]["projectCode"].ToString());

        }
        //需要税单的PR单
        mediaOrderIds = "";
        totalPrice = 0;
        if (taxDt.Rows.Count > 0)
        {
            foreach (DataRow dr in taxDt.Rows)
            {
                mediaOrderIds += dr["mediaOrderIds"].ToString() + ",";
                totalPrice += decimal.Parse(dr["totalAmount"].ToString());
            }
            if (!string.IsNullOrEmpty(Request["GeneralID"]))
            {
                generalids = Request["GeneralID"].ToString();
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(generalids));
            }
            string filePath = FileHelper.SavePage(mediaOrderIds.TrimEnd(','), Server.MapPath("~"));
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
            mediaModel.Flag = 3;
            mediaModel.TotalPrice = totalPrice;
            mediaModel.MediaOrderIDS = mediaOrderIds.TrimEnd(',');
            mediaList.Add(mediaModel);
            //System.Collections.ArrayList ht2 = MediaOrderManager.GetTaxReturnInMediaOrderSql(mediaOrderIds.TrimEnd(','), totalPrice, CurrentUser.SysID, CurrentUserCode, CurrentUser.ITCode, CurrentUser.Name, filePath);
            //ht.Add(ht2[0], ht2[1]);
            if (taxDt.Rows[0]["requestor"] != DBNull.Value)
            {
                down3000RequestorMails.Add(new string[] { State.getEmployeeEmailBySysUserId(int.Parse(taxDt.Rows[0]["requestor"].ToString())), "您提交的" + taxDt.Rows[0]["prNo"] + "付款申请业务审核完成，已提交到财务系统！" });
            }
            financeID = GeneralInfoManager.GetFinanceAccounter(taxDt.Rows[0]["projectCode"].ToString());
        }
        //大于3000
        if (up3000Dt.Rows.Count > 0)
        {
            mediaOrderIds = "";
            totalPrice = 0;
            if (!string.IsNullOrEmpty(Request["GeneralID"]))
            {
                generalids = Request["GeneralID"].ToString();
                generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(generalids));
            }
            foreach (DataRow dr in up3000Dt.Rows)
            {
                mediaOrderIds += dr["mediaOrderIds"].ToString() + ",";
                totalPrice += decimal.Parse(dr["totalAmount"].ToString());
            }
            string filePath1 = FileHelper.SavePage(mediaOrderIds.TrimEnd(','), Server.MapPath("~"));
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
            mediaModel.Flag = 1;
            mediaModel.TotalPrice = totalPrice;
            mediaModel.MediaOrderIDS = mediaOrderIds.TrimEnd(',');
            mediaList.Add(mediaModel);
            //System.Collections.ArrayList ht2 = MediaOrderManager.GetPRInMediaOrderSql(mediaOrderIds.TrimEnd(','), totalPrice, CurrentUser.SysID, CurrentUser.Name, filePath1);
            //ht.Add(ht2[0], ht2[1]);
        }

        if (mediaList.Count > 0)
        {
            if (MediaOrderManager.MediaOrderOperation(mediaList) > 0)
            {
                if (down3000RequestorMails.Count > 0)//小于3000的给原申请人发邮件
                {
                    foreach (string[] item in down3000RequestorMails)
                    {
                        ESP.ConfigCommon.SendMail.Send1("付款申请业务审核完成", item[0].ToString(), item[1].ToString(), false);
                    }
                }
                if (financeID != 0)
                {
                    if (down3000Dt.Rows.Count > 0)
                        ESP.ConfigCommon.SendMail.Send1("媒介付款申请提交完成", State.getEmployeeEmailBySysUserId(financeID), down3000Dt.Rows[0]["prNo"] + "媒介付款申请完成，已提交到财务系统！", false);
                    if (taxDt.Rows.Count > 0)
                        ESP.ConfigCommon.SendMail.Send1("媒介付款申请提交完成", State.getEmployeeEmailBySysUserId(financeID), taxDt.Rows[0]["prNo"] + "媒介付款申请完成，已提交到财务系统！", false);
                }
                ShowCompleteMessage("创建成功,3000以下和需税单的已经提交到财务系统，3000以上请查看新PR单！", "PRMediaList.aspx");
            }
            else
            {
                ShowCompleteMessage("创建失败！", "PRMediaOrderList.aspx");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PRMediaList.aspx");
    }
}