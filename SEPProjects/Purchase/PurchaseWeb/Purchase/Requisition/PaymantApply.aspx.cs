using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ESP.Purchase.Entity;
using ESP.Purchase.DataAccess;
using ESP.Purchase.BusinessLogic;

public partial class Purchase_Requisition_PaymantApply : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        IList<GeneralInfo> list = getConfirmGeneral();  // 获得收货确认信息集合
        List<string> projectlist = new List<string>();
        IList<string[]> listgroup = getGroupBySuppliername();    // 获得供应商名称分组的收货确认条数
        ExportExcel(list, listgroup);
    }

    /// <summary>
    /// 将收货确认信息导出到Excel文件中
    /// </summary>
    /// <param name="list">收货确认信息集合</param>
    /// <param name="listgroup">按供应商名称分组的收货确认信息条数集合</param>
    public void ExportExcel(IList<GeneralInfo> list, IList<string[]> listgroup)
    {
        if (list != null && listgroup != null && list.Count > 0 && listgroup.Count > 0)
        {
            int sum = 0;
            // 整个Excel文件中的数据信息
            IList<object[]> allInfoList = new List<object[]>();
            for (int i = 0; i < listgroup.Count; i++)
            {
                string[] arr = listgroup[i];    //
                
                if (arr != null && arr.Length > 0)
                {
                    object[] objarr = new object[4];
                    int count = Int32.Parse(arr[1]);
                    // 付款申请单基本信息
                    IList<string[]> gerneralinfolist = new List<string[]>();
                    // 小计数目
                    decimal sumnum = 0.00m;
                    // 公司帐户信息
                    string[] arrDeptInfo = new string[3];
                    // 供应商类型
                    string depttyep = string.Empty;
                    for (int j = sum; j < count + sum; j++)
                    {
                        string[] arrGerneralInfo = new string[8];
                        GeneralInfo tGeneralInfo = list[j];
                        arrGerneralInfo[0] = tGeneralInfo.project_code;
                        arrGerneralInfo[1] = tGeneralInfo.app_date.ToString();
                        arrGerneralInfo[2] = tGeneralInfo.requestorname;

                        ESP.Framework.Entity.EmployeeInfo e = ESP.Framework.BusinessLogic.EmployeeManager.Get(tGeneralInfo.requestor);
                        arrGerneralInfo[3] = (e == null || e.Code == null ) ? "" : e.Code;
                        arrGerneralInfo[4] = tGeneralInfo.Department;
                        arrGerneralInfo[5] = tGeneralInfo.supplier_name + ":";
                        arrGerneralInfo[6] = tGeneralInfo.totalprice.ToString();
                        sumnum += tGeneralInfo.totalprice;   // 累计小计数量值
                        arrGerneralInfo[7] = tGeneralInfo.orderid;
                        gerneralinfolist.Add(arrGerneralInfo);  // 添加到付款单申请集合中
                        depttyep = tGeneralInfo.source;   // 供应商类型
                    }

                    if (!string.IsNullOrEmpty(depttyep) && depttyep.Equals("协议供应商"))
                    {
                        string deptname = arr[0];
                        List<SqlParameter> parms = new List<SqlParameter>();
                        string term = string.Empty;
                        term = " and supplier_name = @status ";
                        parms.Add(new SqlParameter("@status", deptname));

                        List<SupplierInfo> listSupplier = SupplierManager.getModelList(term, parms);
                        if (listSupplier != null && listSupplier.Count > 0)
                        {
                            SupplierInfo supplierpo = listSupplier[0];
                            arrDeptInfo[0] = supplierpo.account_name;
                            arrDeptInfo[1] = supplierpo.account_bank;
                            arrDeptInfo[2] = supplierpo.account_number;
                        }
                    }
                    else
                    {
                        arrDeptInfo[0] = arr[0];
                    }
                    sum += count;
                    objarr[0] = gerneralinfolist;   // 付款申请单详细信息
                    objarr[1] = sumnum;    // 付款费用总计
                    objarr[2] = arrDeptInfo;    // 供应商帐户信息 
                    objarr[3] = arr[2];
                    allInfoList.Add(objarr);    // 将每个表格的信息添加到集合中
                }
            }
            export(allInfoList);    // 调用excel导出方法
        }
    }

    /// <summary>
    /// 获得所有收货确认信息
    /// </summary>
    public IList<ESP.Purchase.Entity.GeneralInfo> getConfirmGeneral()
    {
        string term = string.Empty;
        // 添加一个查询条件，标识申请单已经确认的数据
        term = " and status = 7 ORDER BY substring(project_code, 1,1), supplier_name;";   // status=7表示已经收货的采购信息
        IList<GeneralInfo> list = GeneralInfoManager.GetConfrimStatusList(term);
        return list;
    }

    /// <summary>
    /// 获得按供应商名称分组的收货确认信息条数
    /// </summary>
    /// <returns></returns>
    public IList<string[]> getGroupBySuppliername()
    {
        IList<string[]> list = GeneralInfoManager.getGroupBySuppliername("");
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

                sheet = null;
                workbook = null;
                app = null;
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

                sheet = null;
                workbook = null;
                app = null;
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
}
