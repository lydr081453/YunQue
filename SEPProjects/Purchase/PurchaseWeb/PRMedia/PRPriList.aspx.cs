using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Compatible;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

public partial class PRPriList : ESP.Web.UI.PageBase
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
        //string term = " and prtype=@prtype  and (status = @status )";
        //parms.Add(new SqlParameter("@prtype", (int)PRTYpe.PrivatePR));
        //parms.Add(new SqlParameter("@status", State.requisition_recipiented));
        string term = " and prtype=@prtype and (haveinvoice=0 or haveinvoice is null ) and totalprice>=3000 ";
        parms.Add(new SqlParameter("@prtype", (int)PRTYpe.PrivatePR));
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
        List<GeneralInfo> list = GeneralInfoManager.GetStatusListByPriOrder(term, parms);
        gvG.DataSource = list;
        gvG.DataBind();
    }

    protected void btnAll_Click(object sender, EventArgs e)
    {
        List<ESP.Purchase.Entity.MediaOrderOperationInfo> htList = getHt(Request["chkItem"]);
        if (htList.Count > 0)
        {
            if (ESP.Purchase.BusinessLogic.MediaOrderManager.MediaOrderOperation(htList)>0)
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
                ShowCompleteMessage("批量创建成功！", "PRPriList.aspx");
            }
            else
            {
                ShowCompleteMessage("批量创建失败！", "PRPriList.aspx");
            }
        }
    }

    private List<ESP.Purchase.Entity.MediaOrderOperationInfo> getHt(string ids)
    {
        System.Data.DataTable up3000Dt = null;
        System.Data.DataTable down3000Dt = null;
        string[] idList = ids.Split(',');
        List<ESP.Purchase.Entity.MediaOrderOperationInfo> mediaList = new List<MediaOrderOperationInfo>();
        for (int j = 0; j < idList.Length; j++)
        {
            ESP.Purchase.Entity.MediaOrderOperationInfo mediaModel = null;
            if (idList[j] == "")
                break;
            up3000Dt = new System.Data.DataTable();
            down3000Dt = new System.Data.DataTable();

            string gvgvPriOrderStrWhere = string.Format(" and id in ({0}) and inuse={1}", idList[j],(int)State.PRInUse.Use);
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

            //小于3000
            string OrderIds = "";
            decimal totalPrice = 0;

            if (down3000Dt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in down3000Dt.Rows)
                {
                    OrderIds += dr1["pid"].ToString() + ",";
                    totalPrice += decimal.Parse(dr1["ototalprice"].ToString());
                }
                if (down3000Dt.Rows[0]["requestor"] != DBNull.Value)
                {
                    down3000RequestorMails.Add(new string[] { State.getEmployeeEmailBySysUserId(int.Parse(down3000Dt.Rows[0]["requestor"].ToString())), "您提交的" + down3000Dt.Rows[0]["prNo"] + "付款申请业务审核完成，已提交到财务系统！" });
                }
                int financeid = GeneralInfoManager.GetFinanceAccounter(down3000Dt.Rows[0]["ProjectCode"].ToString());
                if (financeid != 0)
                {
                    down3000FinanceMails.Add(new string[] { State.getEmployeeEmailBySysUserId(financeid), down3000Dt.Rows[0]["prNo"] + "媒介付款申请完成，已提交到财务系统！" });
                }
                string filePath = string.Empty;
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(idList[j]));
                mediaModel = new MediaOrderOperationInfo();//新生成一个参数类，开始赋值
                mediaModel.GeneralID = Convert.ToInt32(idList[j]);
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
                mediaModel.Flag = 5;//5为对私3000以下的标记
                mediaModel.TotalPrice = totalPrice;//新PR单总金额
                mediaModel.MediaOrderIDS = OrderIds.TrimEnd(',');//新PR单涉及到的记者信息
                //完成赋值，计入list
                mediaList.Add(mediaModel);
            }

            //大于3000
            if (up3000Dt.Rows.Count > 0)
            {
                OrderIds = "";
                totalPrice = 0;
                foreach (DataRow dr1 in up3000Dt.Rows)
                {
                    OrderIds += dr1["pid"].ToString() + ",";
                    totalPrice += decimal.Parse(dr1["ototalprice"].ToString());
                }
                string filePath1 = string.Empty ;
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(idList[j]));
                mediaModel = new MediaOrderOperationInfo();//新生成一个参数类，开始赋值
                mediaModel.GeneralID = Convert.ToInt32(idList[j]);
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
                mediaModel.FileName = filePath1;//附件url
                mediaModel.Flag = 4;//4为对私3000以上的标记
                mediaModel.TotalPrice = totalPrice;//新PR单总金额
                mediaModel.MediaOrderIDS = OrderIds.TrimEnd(',');//新PR单涉及到的记者信息
                //完成赋值，计入list
                mediaList.Add(mediaModel);
                //System.Collections.ArrayList ht2 = OrderInfoManager.GetPRInPriOrderSql(OrderIds.TrimEnd(','), totalPrice, CurrentUser.SysID, CurrentUser.Name, filePath1);
                //ht.Add(ht2[0], ht2[1]);
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

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Change")
        {
            int generaId = int.Parse(e.CommandArgument.ToString());
            ESP.Purchase.Entity.GeneralInfo model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generaId);
            model.PRType = (int)ESP.Purchase.Common.PRTYpe.CommonPR;
            ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(model);
            ListBind();
        }

    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        ListBind();
    }
}

