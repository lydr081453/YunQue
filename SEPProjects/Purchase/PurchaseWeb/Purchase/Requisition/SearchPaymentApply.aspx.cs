using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_SearchPaymentApply : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["recid"]))
            {
                string recids = Request["recid"];
                string recid = recids.Substring(0, recids.Length - 1);

                IList<string> listg = RecipientManager.getGroupByRecipient(recid);
                string genid = "";
                foreach (string gid in listg)
                {
                    genid += gid + ",";
                }

                if (!string.IsNullOrEmpty(genid))
                {
                    genid = genid.Substring(0, genid.Length - 1);
                    IList<string[]> listgroup = getGroupBySuppliername(genid);
                    ExportExcel(recid,genid, listgroup);
                }
            }
            bindInfo();
        }
    }

    protected void gvSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    /// <summary>
    /// 绑定列表信息
    /// </summary>
    public void bindInfo()
    {
        isCQStockDeparment = false;
        isZBStockDeparment = false;
        SetDeparmentValue();

        List<SqlParameter> parms = new List<SqlParameter>();

        string strWhere = " and a.receivePrice = 0 and (isconfirm = " + State.recipentConfirm_Supplier + " or isconfirm = " + State.recipentConfirm_Emp2 + ")";
        if (!isCQStockDeparment && !isZBStockDeparment)
        {
            strWhere += " and b.requestor=" + CurrentUser.SysID;
        }
        if (!string.IsNullOrEmpty(txtsupplier_name.Text.Trim()))
        {
            strWhere += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplier_name.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtGlideNo.Text.Trim()))
        {
            int totalgno = 0;
            bool res = int.TryParse(txtGlideNo.Text, out totalgno);
            if (res)
            {
                strWhere += " and a.Gid = @Gid";
                parms.Add(new SqlParameter("@Gid", txtGlideNo.Text.Trim()));
            }
        }
        if (!string.IsNullOrEmpty(txtPrNo.Text.Trim()))
        {
            strWhere += " and b.orderid like '%'+@orderid+'%'";
            parms.Add(new SqlParameter("@orderid", txtPrNo.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtRNo.Text.Trim()))
        {
            strWhere += " and a.RecipientNo like '%'+@RecipientNo+'%'";
            parms.Add(new SqlParameter("@RecipientNo", txtRNo.Text.Trim()));
        }

        DataSet ds = RecipientManager.GetRecipientList(strWhere,parms);
        gvSupplier.DataSource = ds;
        gvSupplier.DataBind();

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string generalids = Request["chkItem"];
        if (!string.IsNullOrEmpty(generalids) && generalids.EndsWith(","))
        {
            generalids = generalids.Substring(0, generalids.Length - 1);
        }
        IList<GeneralInfo> list = getConfirmGeneral(generalids);  // 获得收货确认信息集合
        List<string> projectlist = new List<string>();
        IList<string[]> listgroup = getGroupBySuppliername(generalids);    // 获得供应商名称分组的收货确认条数

        bindInfo();
    }

    /// <summary>
    /// 将收货确认信息导出到Excel文件中
    /// </summary>
    /// <param name="recid">收货确认信息.</param>
    /// <param name="genid">The genid.</param>
    /// <param name="listgroup">按供应商名称分组的收货确认信息条数集合</param>
    public void ExportExcel(string recid,string genid, IList<string[]> listgroup)
    {
        if (!string.IsNullOrEmpty(recid) && !string.IsNullOrEmpty(genid) && listgroup.Count > 0)
        {
            // 整个Excel文件中的数据信息集合
            IList<object[]> allInfoList = new List<object[]>(); 
            for (int i = 0; i < listgroup.Count; i++)
            {
                string code = listgroup[i][2].ToString();
                string suppliername = listgroup[i][0].ToString();
               
                string term = string.Format(" and a.id in ({0}) and a.gid in ({2}) and b.project_code like '{1}-%' and supplier_name = '{3}' ", recid, code, genid, suppliername);
                List<SqlParameter> parmrec = new List<SqlParameter>();
                DataSet ds = RecipientManager.GetRecipientList(term, parmrec);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    // 付款申请单基本信息
                      IList<string[]> gerneralinfolist = new List<string[]>();
                      // 付款金额小计
                      decimal sumnum = 0.00m;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string[] arrGerneralInfo = new string[8];
                        arrGerneralInfo[0] = ds.Tables[0].Rows[j]["project_code"].ToString();
                        arrGerneralInfo[1] = !string.IsNullOrEmpty(ds.Tables[0].Rows[j]["RecipientDate"].ToString()) ? DateTime.Parse(ds.Tables[0].Rows[j]["RecipientDate"].ToString()).ToString("yyyy-MM-dd") : "";
                        arrGerneralInfo[2] = ds.Tables[0].Rows[j]["requestorname"].ToString();
                        arrGerneralInfo[3] = !string.IsNullOrEmpty(ds.Tables[0].Rows[j]["requestor"].ToString()) ? new ESP.Compatible.Employee(int.Parse(ds.Tables[0].Rows[j]["requestor"].ToString())).ID : "";
                        arrGerneralInfo[4] = ds.Tables[0].Rows[j]["requestor_group"].ToString();
                        arrGerneralInfo[5] = ds.Tables[0].Rows[j]["supplier_name"].ToString() + ":" + ds.Tables[0].Rows[j]["project_descripttion"].ToString() + "," + ds.Tables[0].Rows[j]["thirdParty_materielDesc"].ToString();
                        arrGerneralInfo[6] = decimal.Parse(ds.Tables[0].Rows[j]["RecipientAmount"].ToString()).ToString("#,##0.00");
                        //累计小计金额
                        sumnum += decimal.Parse(ds.Tables[0].Rows[j]["RecipientAmount"].ToString());
                        arrGerneralInfo[7] = ds.Tables[0].Rows[j]["orderid"].ToString();
                        gerneralinfolist.Add(arrGerneralInfo);  // 添加到付款单申请基本信息集合中
                    }

                    // 公司帐户信息
                    string[] arrDeptInfo = new string[3];
                    arrDeptInfo[0] = ds.Tables[0].Rows[0]["account_name"].ToString();
                    arrDeptInfo[1] = ds.Tables[0].Rows[0]["account_bank"].ToString();
                    arrDeptInfo[2] = ds.Tables[0].Rows[0]["account_number"].ToString();
                
                    // 供应商类型
                     string depttype = code;
                     object[] objarr = new object[4];
                     objarr[0] = gerneralinfolist;   // 付款申请单详细信息
                     objarr[1] = sumnum;    // 付款费用总计
                     objarr[2] = arrDeptInfo;    // 供应商帐户信息 
                     objarr[3] = depttype;
                     allInfoList.Add(objarr);    // 将每个表格的信息添加到集合中                     
                }
            }
            export(allInfoList);    // 调用excel导出方法
        }
    }

    /// <summary>
    /// 获得所有收货确认信息
    /// </summary>
    public IList<GeneralInfo> getConfirmGeneral(string wherestr)
    {
        string term = string.Empty;
        if (!string.IsNullOrEmpty(wherestr))
        {
            term += " and a.id in (" + wherestr + ")";
        }
        // 添加一个查询条件，标识申请单已经确认的数据
        term += " ORDER BY substring(project_code, 1,1), supplier_name;";   // status=7表示已经收货的采购信息
        IList<GeneralInfo> list = GeneralInfoManager.GetConfrimStatusList(term);
        return list;
    }

    /// <summary>
    /// 获得按供应商名称分组的收货确认信息条数
    /// </summary>
    /// <returns></returns>
    public IList<string[]> getGroupBySuppliername(string wherestr)
    {
        IList<string[]> list = GeneralInfoManager.getGroupBySuppliername(wherestr);
        return list;
    }

    public void export(IList<object[]> allInfoList)
    {
        if (allInfoList != null && allInfoList.Count > 0)
        {
            string filename = Server.MapPath("~") + "ExcelTemplate\\" + "paymentapply.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
            sheet.Rows.Font.Size = 9;
            sheet.Columns.Font.Size = 9;

            int tempnum = 0;
            for (int i = 0; i < allInfoList.Count; i++)
            {
                object[] objarr = allInfoList[i];
                if (objarr != null && objarr.Length > 0)
                {
                    IList<string[]> gerneralinfolist = (IList<string[]>)objarr[0];    // 付款申请单详细信息
                    decimal sumnum = (decimal)objarr[1];    // 申请金额小计
                    string[] arrDeptInfo = (string[])objarr[2];    // 供应商帐户信息
                    string suppliertype = (string)objarr[3];
                    if (i == 0)
                    {
                        sheet.get_Range("B6", "H6").MergeCells = true;
                        sheet.get_Range("B6", "H6").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;    // 将字体居中
                        sheet.get_Range("B6", "H6").Font.Bold = true;
                        sheet.Cells[6, 2] = "支票/电汇付款申请单";

                        sheet.get_Range("B8", "H8").MergeCells = true;
                        sheet.get_Range("B8", "H8").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;    // 将字体居中
                        sheet.get_Range("B8", "H8").Font.Bold = true;
                        sheet.get_Range("B8", "H8").Font.Size = 9;
                        sheet.get_Range("B8", "H8").Font.ColorIndex = 3;  // 红色
                        sheet.Cells[8, 2] = suppliertype;


                        sheet.get_Range("A10", "S10").RowHeight = 27.75;
                        sheet.get_Range("A10", "S10").Font.Bold = true;
                        sheet.get_Range("A10", "S10").Font.Size = 9;
                        sheet.Cells[10, 1] = "序号";
                        sheet.Cells[10, 2] = "项目号";
                        sheet.Cells[10, 3] = "费用发生日期";
                        sheet.Cells[10, 4] = "申请人";
                        sheet.Cells[10, 5] = "员工编号";
                        sheet.Cells[10, 6] = "费用所属组";
                        sheet.Cells[10, 7] = "费  用  明  细  描  述";
                        sheet.Cells[10, 8] = "申请金额";
                        sheet.Cells[10, 9] = "订单号";

                        if (gerneralinfolist != null && gerneralinfolist.Count > 0)
                        {
                            for (int j = 0; j < gerneralinfolist.Count; j++)
                            {
                                string[] gerneralinfoarr = gerneralinfolist[j];
                                sheet.Cells[10 + j + 1, 1] = j + 1;   // 序号
                                sheet.Cells[10 + j + 1, 2] = gerneralinfoarr[0];    // 项目号
                                sheet.Cells[10 + j + 1, 3] = gerneralinfoarr[1];    // 费用发生日期
                                sheet.Cells[10 + j + 1, 4] = gerneralinfoarr[2];    // 申请人
                                sheet.Cells[10 + j + 1, 5] = gerneralinfoarr[3];    // 员工编号
                                sheet.Cells[10 + j + 1, 6] = gerneralinfoarr[4];    // 费用所属组
                                sheet.Cells[10 + j + 1, 7] = gerneralinfoarr[5];    // 费用明细描述
                                sheet.get_Range("G" + (10 + j + 1).ToString(), "G" + (10 + j + 1).ToString()).WrapText = true;
                                sheet.Cells[10 + j + 1, 8] = gerneralinfoarr[6];    // 申请金额
                                sheet.Cells[10 + j + 1, 9] = gerneralinfoarr[7];    // 订单号
                            }
                        }

                        sheet.get_Range("G" + (12 + gerneralinfolist.Count), "G" + (12 + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.get_Range("G" + (12 + gerneralinfolist.Count), "G" + (12 + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.Cells[12 + gerneralinfolist.Count, 7] = "小计(按供应商）";
                        sheet.get_Range("H" + (12 + gerneralinfolist.Count), "H" + (12 + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[12 + gerneralinfolist.Count, 8] = sumnum.ToString();
                        sheet.get_Range("D" + (13 + gerneralinfolist.Count), "F" + (13 + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.get_Range("D" + (13 + gerneralinfolist.Count), "F" + (13 + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("G" + (13 + gerneralinfolist.Count), "G" + (13 + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[13 + gerneralinfolist.Count, 4] = "公司名称";
                        sheet.get_Range("D" + (14 + gerneralinfolist.Count), "F" + (14 + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.get_Range("D" + (14 + gerneralinfolist.Count), "F" + (14 + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("G" + (14 + gerneralinfolist.Count), "G" + (14 + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[14 + gerneralinfolist.Count, 4] = "开户行名称";
                        sheet.get_Range("D" + (15 + gerneralinfolist.Count), "F" + (15 + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.get_Range("D" + (15 + gerneralinfolist.Count), "F" + (15 + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("G" + (15 + gerneralinfolist.Count), "G" + (15 + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[15 + gerneralinfolist.Count, 4] = "银行帐号";
                        if (!string.IsNullOrEmpty(arrDeptInfo[0]))
                        {
                            sheet.Cells[13 + gerneralinfolist.Count, 7] = arrDeptInfo[0];
                        }
                        if (!string.IsNullOrEmpty(arrDeptInfo[1]))
                        {
                            sheet.Cells[14 + gerneralinfolist.Count, 7] = arrDeptInfo[1];
                        }
                        if (!string.IsNullOrEmpty(arrDeptInfo[2]))
                        {
                            sheet.Cells[15 + gerneralinfolist.Count, 7] = arrDeptInfo[2];
                        }

                        sheet.get_Range("A10", "I" + (15 + gerneralinfolist.Count)).Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;    // 边框细线

                        sheet.get_Range("A" + (17 + gerneralinfolist.Count), "B" + (22 + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("A" + (17 + gerneralinfolist.Count), "B" + (22 + gerneralinfolist.Count)).Font.Size = 12;
                        sheet.Cells[17 + gerneralinfolist.Count, 1] = "申请人签字:\n\n__________________\n日期:";

                        sheet.get_Range("D" + (17 + gerneralinfolist.Count), "F" + (22 + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("D" + (17 + gerneralinfolist.Count), "F" + (22 + gerneralinfolist.Count)).Font.Size = 12;
                        sheet.Cells[17 + gerneralinfolist.Count, 4] = "第一级批准人签字:\n\n__________________\n日期:";

                        sheet.get_Range("H" + (17 + gerneralinfolist.Count), "I" + (22 + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("H" + (17 + gerneralinfolist.Count), "I" + (22 + gerneralinfolist.Count)).Font.Size = 12;
                        sheet.Cells[17 + gerneralinfolist.Count, 8] = "第二级核准人签字:\n\n__________________\n日期:";
                        tempnum = 22 + gerneralinfolist.Count;
                    }
                    else
                    {
                        sheet.get_Range("B" + (tempnum + 4), "H" + (tempnum + 4)).MergeCells = true;
                        sheet.get_Range("B" + (tempnum + 4), "H" + (tempnum + 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;    // 将字体居中
                        sheet.get_Range("B" + (tempnum + 4), "H" + (tempnum + 4)).Font.Bold = true;
                        sheet.Cells[(tempnum + 4), 2] = "支票/电汇付款申请单";

                        sheet.get_Range("B" + (tempnum + 6), "H" + (tempnum + 6)).MergeCells = true;
                        sheet.get_Range("B" + (tempnum + 6), "H" + (tempnum + 6)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;    // 将字体居中
                        sheet.get_Range("B" + (tempnum + 6), "H" + (tempnum + 6)).Font.Bold = true;
                        sheet.get_Range("B" + (tempnum + 6), "H" + (tempnum + 6)).Font.Size = 9;
                        sheet.get_Range("B" + (tempnum + 6), "H" + (tempnum + 6)).Font.ColorIndex = 3;  // 红色
                        sheet.Cells[(tempnum + 6), 2] = suppliertype;

                        sheet.get_Range("A" + (tempnum + 8), "S" + (tempnum + 8)).RowHeight = 27.75;
                        sheet.get_Range("A" + (tempnum + 8), "S" + (tempnum + 8)).Font.Bold = true;
                        sheet.get_Range("A" + (tempnum + 8), "S" + (tempnum + 8)).Font.Size = 9;
                        sheet.Cells[(tempnum + 8), 1] = "序号";
                        sheet.Cells[(tempnum + 8), 2] = "项目号";
                        sheet.Cells[(tempnum + 8), 3] = "费用发生日期";
                        sheet.Cells[(tempnum + 8), 4] = "申请人";
                        sheet.Cells[(tempnum + 8), 5] = "员工编号";
                        sheet.Cells[(tempnum + 8), 6] = "费用所属组";
                        sheet.Cells[(tempnum + 8), 7] = "费  用  明  细  描  述";
                        sheet.Cells[(tempnum + 8), 8] = "申请金额";
                        sheet.Cells[(tempnum + 8), 9] = "订单号";

                        if (gerneralinfolist != null && gerneralinfolist.Count > 0)
                        {
                            for (int j = 0; j < gerneralinfolist.Count; j++)
                            {
                                string[] gerneralinfoarr = gerneralinfolist[j];
                                sheet.Cells[(tempnum + 8) + j + 1, 1] = j + 1;   // 序号
                                sheet.Cells[(tempnum + 8) + j + 1, 2] = gerneralinfoarr[0];    // 项目号
                                sheet.Cells[(tempnum + 8) + j + 1, 3] = gerneralinfoarr[1];    // 费用发生日期
                                sheet.Cells[(tempnum + 8) + j + 1, 4] = gerneralinfoarr[2];    // 申请人
                                sheet.Cells[(tempnum + 8) + j + 1, 5] = gerneralinfoarr[3];    // 员工编号
                                sheet.Cells[(tempnum + 8) + j + 1, 6] = gerneralinfoarr[4];    // 费用所属组
                                sheet.Cells[(tempnum + 8) + j + 1, 7] = gerneralinfoarr[5];    // 费用明细描述
                                sheet.get_Range("G" + ((tempnum + 8) + j + 1).ToString(), "G" + ((tempnum + 8) + j + 1).ToString()).WrapText = true;
                                sheet.Cells[(tempnum + 8) + j + 1, 8] = gerneralinfoarr[6];    // 申请金额
                                sheet.Cells[(tempnum + 8) + j + 1, 9] = gerneralinfoarr[7];    // 订单号
                            }
                        }
                        sheet.get_Range("G" + ((tempnum + 10) + gerneralinfolist.Count), "G" + ((tempnum + 10) + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.get_Range("G" + ((tempnum + 10) + gerneralinfolist.Count), "G" + ((tempnum + 10) + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.Cells[(tempnum + 10) + gerneralinfolist.Count, 7] = "小计(按供应商）";
                        sheet.get_Range("H" + ((tempnum + 10) + gerneralinfolist.Count), "H" + ((tempnum + 10) + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[(tempnum + 10) + gerneralinfolist.Count, 8] = sumnum.ToString();
                        sheet.get_Range("D" + ((tempnum + 11) + gerneralinfolist.Count), "F" + ((tempnum + 11) + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.get_Range("D" + ((tempnum + 11) + gerneralinfolist.Count), "F" + ((tempnum + 11) + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("G" + ((tempnum + 11) + gerneralinfolist.Count), "G" + ((tempnum + 11) + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[(tempnum + 11) + gerneralinfolist.Count, 4] = "公司名称";
                        sheet.get_Range("D" + ((tempnum + 12) + gerneralinfolist.Count), "F" + ((tempnum + 12) + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.get_Range("D" + ((tempnum + 12) + gerneralinfolist.Count), "F" + ((tempnum + 12) + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("G" + ((tempnum + 12) + gerneralinfolist.Count), "G" + ((tempnum + 12) + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[(tempnum + 12) + gerneralinfolist.Count, 4] = "开户行名称";
                        sheet.get_Range("D" + ((tempnum + 13) + gerneralinfolist.Count), "F" + ((tempnum + 13) + gerneralinfolist.Count)).Font.Bold = true;
                        sheet.get_Range("D" + ((tempnum + 13) + gerneralinfolist.Count), "F" + ((tempnum + 13) + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("G" + ((tempnum + 13) + gerneralinfolist.Count), "G" + ((tempnum + 13) + gerneralinfolist.Count)).Interior.ColorIndex = 43;    // 绿色
                        sheet.Cells[(tempnum + 13) + gerneralinfolist.Count, 4] = "银行帐号";

                        if (!string.IsNullOrEmpty(arrDeptInfo[0]))
                        {
                            sheet.Cells[(tempnum + 11) + gerneralinfolist.Count, 7] = arrDeptInfo[0];
                        }
                        if (!string.IsNullOrEmpty(arrDeptInfo[1]))
                        {
                            sheet.Cells[(tempnum + 12) + gerneralinfolist.Count, 7] = arrDeptInfo[1];
                        }
                        if (!string.IsNullOrEmpty(arrDeptInfo[2]))
                        {
                            sheet.Cells[(tempnum + 13) + gerneralinfolist.Count, 7] = arrDeptInfo[2];
                        }

                        sheet.get_Range("A" + (tempnum + 8), "I" + ((tempnum + 13) + gerneralinfolist.Count)).Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;    // 边框细线

                        sheet.get_Range("A" + ((tempnum + 15) + gerneralinfolist.Count), "B" + ((tempnum + 19) + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("A" + ((tempnum + 15) + gerneralinfolist.Count), "B" + ((tempnum + 19) + gerneralinfolist.Count)).Font.Size = 12;
                        sheet.Cells[(tempnum + 15) + gerneralinfolist.Count, 1] = "申请人签字:\n\n__________________\n日期:";

                        sheet.get_Range("D" + ((tempnum + 15) + gerneralinfolist.Count), "F" + ((tempnum + 19) + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("D" + ((tempnum + 15) + gerneralinfolist.Count), "F" + ((tempnum + 19) + gerneralinfolist.Count)).Font.Size = 12;
                        sheet.Cells[(tempnum + 15) + gerneralinfolist.Count, 4] = "第一级批准人签字:\n\n__________________\n日期:";

                        sheet.get_Range("H" + ((tempnum + 15) + gerneralinfolist.Count), "I" + ((tempnum + 19) + gerneralinfolist.Count)).MergeCells = true;    // 合并单元格
                        sheet.get_Range("H" + ((tempnum + 15) + gerneralinfolist.Count), "I" + ((tempnum + 19) + gerneralinfolist.Count)).Font.Size = 12;
                        sheet.Cells[(tempnum + 15) + gerneralinfolist.Count, 8] = "第二级核准人签字:\n\n__________________\n日期:";
                        tempnum = ((tempnum + 19) + gerneralinfolist.Count);
                    }
                }
            }

            string url = "http://127.0.0.1:7777/Purchase/Requisition/Print/PaymantPrint.aspx?recid=" + Request["recid"];
            sheet.get_Range("F" + (tempnum + 5).ToString(), "F" + (tempnum + 5).ToString()).Hyperlinks.Add(sheet.get_Range("F" + (tempnum + 5).ToString(), "F" + (tempnum + 5).ToString()), url, Type.Missing, Type.Missing, "付款确认");

            workbook.Saved = true;

            string tmpFileName = "tmppaymentapply" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(Server.MapPath("~") + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                outExcel(Server.MapPath("~") + "ExcelTemplate\\" + tmpFileName, tmpFileName, Response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else    // 如果收货确认信息为空就导出一个空的模板文件
        {
            string filename = Server.MapPath("~") + "ExcelTemplate\\" + "paymentapplytwo.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
            workbook.Saved = true;

            string tmpFileName = "tmppaymentapply" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(Server.MapPath("~") + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                outExcel(Server.MapPath("~") + "ExcelTemplate\\" + tmpFileName, tmpFileName, Response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    private static void outExcel(string pathandname, string filename, HttpResponse response)
    {
        FileStream fin = new FileStream(pathandname, FileMode.Open);
        response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
        response.AddHeader("Connection", "Close");
        response.AddHeader("Content-Transfer-Encoding", "binary");
        response.ContentType = "application/octet-stream";
        response.AddHeader("Content-Length", fin.Length.ToString());
        byte[] buf = new byte[1024];
        while (true)
        {
            int length = fin.Read(buf, 0, buf.Length);
            if (length > 0)
                response.OutputStream.Write(buf, 0, length);
            if (length < buf.Length)
                break;
        }
        fin.Close();
        response.Flush();
        response.Close();
        FileInfo finfo = new FileInfo(pathandname);
        finfo.Delete();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindInfo();
    }

    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            DataRowView dv = (DataRowView)e.Row.DataItem;
            //Button btnQR = (Button)e.Row.FindControl("btnQR");

            //采购部的人员只能勾选各自公司的申请人提交的协议供应商的单子
            //普通申请人可以勾选任何人提交的非协议供应商的单子
            //david.duan 没有限制
            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
            {
                if (isCQStockDeparment)
                {
                    if (dv["source"].ToString() != "协议供应商" ||
                        GetLevel1DeparmentID(int.Parse(dv["requestor"].ToString())) !=
                        State.filialeName_CQ)
                    {
                        e.Row.Cells[0].Text = "";
                    }
                }
                if (isZBStockDeparment)
                {
                    if (dv["source"].ToString() != "协议供应商" ||
                        GetLevel1DeparmentID(int.Parse(dv["requestor"].ToString())) != "总部")
                    {
                        e.Row.Cells[0].Text = "";
                        //btnQR.Visible = false;
                    }

                }
                if ( !isCQStockDeparment && !isZBStockDeparment)
                {
                    if (dv["source"].ToString() == "协议供应商")
                    {
                        e.Row.Cells[0].Text = "";
                        //btnQR.Visible = false;
                    }
                    else
                    {
                        //if (GetLevel1DeparmentID(int.Parse(CurrentUser.SysID)) != GetLevel1DeparmentID(model.requestor)) 
                        if (int.Parse(dv["requestor"].ToString()) != int.Parse(CurrentUser.SysID))
                        {
                            e.Row.Cells[0].Text = "";
                            //btnQR.Visible = false;
                        }
                    }
                }
            }
        }
    }

    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "QR")
        {
            int recipientId = int.Parse(e.CommandArgument.ToString());
            RecipientInfo model = RecipientManager.GetModel(recipientId);
            model.receivePrice = (int)State.receivePrice.Yes;
            RecipientManager.Update(model);
            bindInfo();
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('确认成功！');", true);
        }
    }

    static bool isCQStockDeparment = false;
    static bool isZBStockDeparment = false;

    /// <summary>
    /// 检查当前登陆人是否为采购部的人员
    /// </summary>
    private bool checkIsStockDeparmentUser(string dep)
    {
        int[] deparments = CurrentUser.GetDepartmentIDs();
        foreach (int uniqID in deparments)
        {
            if (dep == "CQ")
            {
                if (uniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["CQStockDeparmentUniqID"].ToString()))
                    return true;
            }
            else
            {
                if (uniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["StockDeparmentUniqID"].ToString()))
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetDeparmentValue()
    {
        if (checkIsStockDeparmentUser("CQ"))
            isCQStockDeparment = true;
        if(checkIsStockDeparmentUser("ZB"))
            isZBStockDeparment = true;
    }

    private string GetLevel1DeparmentID(int sysId)
    {
        IList<ESP.Compatible.Department> dtdep = ESP.Compatible.Employee.GetDepartments(sysId);
        string nodename = "";
        if (dtdep.Count > 0)
        {
            string level = dtdep[0].Level.ToString();
            if (level == "1")
            {
                nodename = dtdep[0].NodeName;
            }
            else if (level == "2")
            {
                ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                nodename = dep.Parent.DepartmentName;

            }
            else if (level == "3")
            {
                ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
                nodename = dep2.Parent.DepartmentName;

            }
        }
        return nodename;
    }
}
