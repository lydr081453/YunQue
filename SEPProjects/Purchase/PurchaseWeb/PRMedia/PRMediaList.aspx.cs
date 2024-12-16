using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;
using ESP.Compatible;

public partial class PRMedia_PRMediaList : ESP.Web.UI.PageBase
{
    private static List<string[]> down3000RequestorMails = new List<string[]>();
    private static List<string[]> down3000FinanceMails = new List<string[]>();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
        #endregion
        Page.Server.ScriptTimeout = 600;
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        string term = " and prtype=@prtype and (status = @status )";
        parms.Add(new SqlParameter("@prtype", (int)PRTYpe.MediaPR));
        parms.Add(new SqlParameter("@status", State.order_mediaAuditYes));
        if (txtProjectNo.Text.Trim() != "")
        {
            term += " and project_code like '%'+@projectcode+'%'";
            parms.Add(new SqlParameter("@projectcode", txtProjectNo.Text.Trim()));
        }
        if (txtItemNo.Text.Trim() != "")
        {
            term += " and aa.id in (select distinct general_id from t_orderinfo where item_no like '%'+@itemno+'%')";
            parms.Add(new SqlParameter("@itemno", txtItemNo.Text.Trim()));
        }
        if (txtBegin.Text.Trim() != "")
        {
            term += " and app_date >=CONVERT(datetime , @begin, 120 )";
            parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
        }
        if (txtEnd.Text.Trim() != "")
        {
            term += " and app_date <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
            parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
        }
        if (txtTotalMin.Text.Trim() != "")
        {
            int totalmin = 0;
            bool res = int.TryParse(txtTotalMin.Text, out totalmin);
            if (res)
            {
                term += " and bb.totalprice >=@totalmin";
                parms.Add(new SqlParameter("@totalmin", txtTotalMin.Text.Trim()));
            }

        }

        if (txtTotalMax.Text.Trim() != "")
        {
            int totalmax = 0;
            bool res = int.TryParse(txtTotalMax.Text, out totalmax);
            if (res)
            {
                term += " and bb.totalprice <= @totalmax";
                parms.Add(new SqlParameter("@totalmax", txtTotalMax.Text.Trim()));
            }
        }
        List<GeneralInfo> list = GeneralInfoManager.GetStatusListByMedia(term, parms);
        gvG.DataSource = list;
        gvG.DataBind();
    }

    protected void btnAll_Click(object sender, EventArgs e)
    {
        List<ESP.Purchase.Entity.MediaOrderOperationInfo> htList = getHt(Request["chkItem"]);
        if (htList.Count > 0)
        {
            if (MediaOrderManager.MediaOrderOperation(htList)>0)
            {
                if (down3000RequestorMails.Count > 0)//小于3000的给原申请人发邮件
                {
                    foreach (string[] item in down3000RequestorMails)
                    {
                        ESP.ConfigCommon.SendMail.Send1("付款申请业务审核完成", item[0].ToString(), item[1].ToString(), false, "");
                    }
                }
                if (down3000FinanceMails.Count > 0)//小于3000的给finance发邮件
                {
                    foreach (string[] item in down3000FinanceMails)
                    {
                        ESP.ConfigCommon.SendMail.Send1("媒介付款申请提交完成", item[0].ToString(), item[1].ToString(), false, "");
                    }
                }
                down3000RequestorMails.Clear();
                down3000FinanceMails.Clear();
                ShowCompleteMessage("批量创建成功！", "PRMediaList.aspx");
            }
            else
            {
                ShowCompleteMessage("批量创建失败！", "PRMediaList.aspx");
            }
        }
    }

    private List<ESP.Purchase.Entity.MediaOrderOperationInfo> getHt(string ids)
    {
        System.Data.DataTable up3000Dt = null;
        System.Data.DataTable down3000Dt = null;
        System.Data.DataTable taxDt = null;
        bool isTax = false;
        string[] idList = ids.Split(',');
        List<ESP.Purchase.Entity.MediaOrderOperationInfo> mediaList = new List<MediaOrderOperationInfo>();
        for (int j = 0; j < idList.Length; j++)
        {
            ESP.Purchase.Entity.MediaOrderOperationInfo mediaModel=null;
            if (idList[j] == "")
                break;
            up3000Dt = new System.Data.DataTable();
            down3000Dt = new System.Data.DataTable();
            taxDt = new DataTable();

            string gvgvMediaOrderStrWhere = string.Format(" a.id in ({0}) and b.billtype={1} and c.status=0 and a.inuse={2}", idList[j], ((int)BillType.WritingFeeBill).ToString(),(int)State.PRInUse.Use);
            DataSet gvgvMediaOrderDS = MediaOrderManager.GetListByGID(gvgvMediaOrderStrWhere);

            DataColumn dc1 = new DataColumn("id");
            DataColumn dc2 = new DataColumn("prNo");
            DataColumn dc3 = new DataColumn("CardNumber");
            DataColumn dc4 = new DataColumn("receiverName");
            DataColumn dc5 = new DataColumn("totalAmount");
            DataColumn dc6 = new DataColumn("mediaOrderIds");
            DataColumn dc7 = new DataColumn("requestor");
            DataColumn dc8 = new DataColumn("ProjectCode");
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
            for (int i = (gvgvMediaOrderDS.Tables[0].Rows.Count - 1); i >= 0; i--)
            {
                if (isNewRow)
                {
                    dr = up3000Dt.NewRow();
                    tempCardNum = gvgvMediaOrderDS.Tables[0].Rows[i]["CardNumber"].ToString().Trim();
                    tempreporterName = gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"] == DBNull.Value ? gvgvMediaOrderDS.Tables[0].Rows[i]["reporterName"].ToString().Trim() : gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"].ToString().Trim();
                    if (dr["IsTax"] != DBNull.Value)
                        isTax = dr["IsTax"].ToString().Trim() == "True" ? true : false;
                    dr["id"] = gvgvMediaOrderDS.Tables[0].Rows[i]["gid"].ToString();
                    dr["prNo"] = gvgvMediaOrderDS.Tables[0].Rows[i]["prNo"].ToString();
                    dr["CardNumber"] = gvgvMediaOrderDS.Tables[0].Rows[i]["CardNumber"].ToString();
                    dr["receiverName"] = tempreporterName;//gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"].ToString();
                    dr["totalAmount"] = gvgvMediaOrderDS.Tables[0].Rows[i]["totalAmount"].ToString();
                    dr["mediaOrderIds"] = gvgvMediaOrderDS.Tables[0].Rows[i]["MeidaOrderID"].ToString();
                    dr["requestor"] = gvgvMediaOrderDS.Tables[0].Rows[i]["requestor"].ToString();
                    dr["ProjectCode"] = gvgvMediaOrderDS.Tables[0].Rows[i]["project_code"].ToString();
                    gvgvMediaOrderDS.Tables[0].Rows.RemoveAt(i);
                    isNewRow = false;
                }
                else
                {
                    string tempCheckString = "";
                    if (dr["IsTax"] != DBNull.Value)
                        isTax = dr["IsTax"].ToString().Trim() == "True" ? true : false;
                    if (gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"] == DBNull.Value || gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"].ToString().Trim() == "")
                        tempCheckString = gvgvMediaOrderDS.Tables[0].Rows[i]["reporterName"].ToString().Trim();
                    else
                        tempCheckString = gvgvMediaOrderDS.Tables[0].Rows[i]["receiverName"].ToString().Trim();
                    if (gvgvMediaOrderDS.Tables[0].Rows[i]["CardNumber"].ToString() == tempCardNum && tempCheckString == tempreporterName)
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
            //以上代码完成3000以上和3000以下的分类，以下代码开始写入参数mediaList值
            //小于3000
            string mediaOrderIds = "";
            decimal totalPrice = 0;

            if (down3000Dt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in down3000Dt.Rows)
                {
                    mediaOrderIds += dr1["mediaOrderIds"].ToString() + ",";
                    totalPrice += decimal.Parse(dr1["totalAmount"].ToString());
                }
                if (down3000Dt.Rows[0]["requestor"] != DBNull.Value)
                {
                    down3000RequestorMails.Add(new string[]{State.getEmployeeEmailBySysUserId(int.Parse(down3000Dt.Rows[0]["requestor"].ToString())), "您提交的" + down3000Dt.Rows[0]["prNo"] + "付款申请业务审核完成，已提交到财务系统！"});
                }
                int financeid = GeneralInfoManager.GetFinanceAccounter(down3000Dt.Rows[0]["ProjectCode"].ToString());
                if (financeid != 0)
                {
                    down3000FinanceMails.Add(new string[] { State.getEmployeeEmailBySysUserId(financeid), down3000Dt.Rows[0]["prNo"] + "媒介付款申请完成，已提交到财务系统！" });
                }
                string filePath = FileHelper.SavePage(mediaOrderIds.TrimEnd(','), Server.MapPath("~"));
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(idList[j]));
                mediaModel = new MediaOrderOperationInfo();//新生成一个参数类，开始赋值
                mediaModel.GeneralID =Convert.ToInt32( idList[j]);
                if (generalModel != null)
                {
                    mediaModel.PRNO = generalModel.PrNo;//PR NO
                    mediaModel.BankAccount = generalModel.account_number;//银行信息
                    mediaModel.BankAccountName = generalModel.account_name;
                    mediaModel.BankName = generalModel.account_bank;
                }
                mediaModel.CurrentUserID = CurrentUser.SysID;//新PR单申请人更改为当前的登录人
                mediaModel.CurrentUserCode = CurrentUser.ID;
                mediaModel.CurrentUserEmpName = CurrentUser.Name;
                mediaModel.CurrentUserName = CurrentUser.ITCode;
                mediaModel.FileName = filePath;//附件url
                mediaModel.Flag = 2;//2为3000以下的标记
                mediaModel.TotalPrice = totalPrice;//新PR单总金额
                mediaModel.MediaOrderIDS = mediaOrderIds.TrimEnd(',');//新PR单涉及到的记者信息
               //完成赋值，计入list
                mediaList.Add(mediaModel);
            }
            //需要税单的PR
             mediaOrderIds = "";
             totalPrice = 0;
            if (taxDt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in taxDt.Rows)
                {
                    mediaOrderIds += dr1["mediaOrderIds"].ToString() + ",";
                    totalPrice += decimal.Parse(dr1["totalAmount"].ToString());
                }
                if (taxDt.Rows[0]["requestor"] != DBNull.Value)
                {
                    down3000RequestorMails.Add(new string[] { State.getEmployeeEmailBySysUserId(int.Parse(taxDt.Rows[0]["requestor"].ToString())), "您提交的" + taxDt.Rows[0]["prNo"] + "付款申请业务审核完成，已提交到财务系统！" });
                }
                int financeid = GeneralInfoManager.GetFinanceAccounter(taxDt.Rows[0]["ProjectCode"].ToString());
                if (financeid != 0)
                {
                    down3000FinanceMails.Add(new string[] { State.getEmployeeEmailBySysUserId(financeid), taxDt.Rows[0]["prNo"] + "媒介付款申请完成，已提交到财务系统！" });
                }
                string filePath = FileHelper.SavePage(mediaOrderIds.TrimEnd(','), Server.MapPath("~"));
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(idList[j]));
                mediaModel = new MediaOrderOperationInfo();//新生成一个参数类，开始赋值
                mediaModel.GeneralID = Convert.ToInt32(idList[j]);
                if (generalModel != null)
                {
                    mediaModel.PRNO = generalModel.PrNo;
                    mediaModel.BankAccount = generalModel.account_number;
                    mediaModel.BankAccountName = generalModel.account_name;
                    mediaModel.BankName = generalModel.account_bank;
                }
                mediaModel.CurrentUserID = CurrentUser.SysID;//新PR单申请人更改为当前的登录人
                mediaModel.CurrentUserCode = CurrentUser.ID;
                mediaModel.CurrentUserEmpName = CurrentUser.Name;
                mediaModel.CurrentUserName = CurrentUser.ITCode;
                mediaModel.FileName = filePath;//附件url
                mediaModel.Flag = 3;//3为需要税单的PR的标记
                mediaModel.TotalPrice = totalPrice;
                mediaModel.MediaOrderIDS = mediaOrderIds.TrimEnd(',');//新PR单涉及到的记者信息
                mediaList.Add(mediaModel);
             }

            //大于3000
            if (up3000Dt.Rows.Count > 0)
            {
                mediaOrderIds = "";
                totalPrice = 0;
                foreach (DataRow dr1 in up3000Dt.Rows)
                {
                    mediaOrderIds += dr1["mediaOrderIds"].ToString() + ",";
                    totalPrice += decimal.Parse(dr1["totalAmount"].ToString());
                }
                string filePath1 = FileHelper.SavePage(mediaOrderIds.TrimEnd(','), Server.MapPath("~"));
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(idList[j]));
                mediaModel = new MediaOrderOperationInfo();
                mediaModel.GeneralID = Convert.ToInt32(idList[j]);
                if (generalModel != null)
                {
                    mediaModel.PRNO = generalModel.PrNo;
                    mediaModel.BankAccount = generalModel.account_number;
                    mediaModel.BankAccountName = generalModel.account_name;
                    mediaModel.BankName = generalModel.account_bank;
                }
                mediaModel.CurrentUserID = CurrentUser.SysID;
                mediaModel.CurrentUserCode = CurrentUser.ID;
                mediaModel.CurrentUserEmpName = CurrentUser.Name;
                mediaModel.CurrentUserName = CurrentUser.ITCode;
                mediaModel.FileName = filePath1;
                mediaModel.MediaOrderIDS = mediaOrderIds.TrimEnd(',');
                mediaModel.Flag = 1;//1为3000以上的PR标记
                mediaModel.TotalPrice = totalPrice;
                mediaList.Add(mediaModel);
            }
        }
        return mediaList;
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.HyperLink hylEdit = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hylEdit");
            if (null != hylEdit)
            {
                switch (e.Row.Cells[1].Text.ToString())
                {
                    case "1":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_1, e.Row.Cells[0].Text.ToString());
                        break;
                    case "2":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_2, e.Row.Cells[0].Text.ToString());
                        break;
                    case "3":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_3, e.Row.Cells[0].Text.ToString());
                        break;
                    case "4":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_4, e.Row.Cells[0].Text.ToString());
                        break;
                    case "5":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_5, e.Row.Cells[0].Text.ToString());
                        break;
                    case "6":
                        hylEdit.NavigateUrl = string.Format(State.addstatus_6, e.Row.Cells[0].Text.ToString());
                        break;
                    default:
                        hylEdit.NavigateUrl = string.Format(State.addstatus_7, e.Row.Cells[0].Text.ToString());
                        break;
                }
            }
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            if (model.InUse != (int)State.PRInUse.Use)
            {
                e.Row.Cells[2].Controls.Clear();
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Label labFiliAudi = ((System.Web.UI.WebControls.Label)e.Row.FindControl("labFiliAudi"));
            if (null != labFiliAudi)
            {
                if (!string.IsNullOrEmpty(labFiliAudi.Text))
                {
                    ESP.Compatible.Employee em = new Employee(int.Parse(labFiliAudi.Text));
                    labFiliAudi.Text = em.Name;
                }
            }
        }
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        ListBind();
    }
}
