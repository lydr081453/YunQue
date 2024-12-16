using System;
using System.Net;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using Excel = Microsoft.Office.Interop.Excel;
using ESP.ConfigCommon;

namespace ESP.Purchase.Common
{
    /// <summary>
    /// FileHelper 的摘要说明
    /// </summary>
    public class FileHelper
    {
        public FileHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string upFile(string fileName,string mapPath, FileUpload file)
        {
            string savePath = "";
            string newfilename = Guid.NewGuid().ToString();
            string extension = System.IO.Path.GetExtension(file.PostedFile.FileName).ToLower();
            savePath = mapPath + @"upFile\" + newfilename + extension;
            file.PostedFile.SaveAs(savePath);
            return "upFile\\" + newfilename + extension;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string upFile(string fileName, string mapPath, HttpPostedFile postedFile)
        {
            string savePath = "";
            string newfilename = Guid.NewGuid().ToString();
            string extension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
            savePath = mapPath + @"upFile\" + newfilename + extension;
            postedFile.SaveAs(savePath);
            return "upFile\\" + newfilename + extension;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string upFile1(string fileName, string mapPath, HttpPostedFile postedFile)
        {
            string savePath = "";
            string newfilename = Guid.NewGuid().ToString();
            string extension = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
            savePath = mapPath + newfilename + extension;
            postedFile.SaveAs(savePath);
            return newfilename + extension;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void deleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static string ToOrderInfoExcel(int generalid, string mapPath, HttpResponse response)
        {
            return ToOrderInfoExcel(generalid, mapPath, response, false);
        }

        /// <summary>
        /// 导出YANE对账信息(根据旧PR对象列表）
        /// </summary>
        /// <param name="list"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        public static void ExportCollateInfo(List<GeneralInfo> list, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "Collate.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            int beginIndex = 3;
            for(int i=0;i<list.Count;i++)
            {
                #region 原PR信息
                GeneralInfo model = list[i];
                //项目号
                sheet.Cells[beginIndex + i, 2] = model.project_code;
                //公司代码
                sheet.Cells[beginIndex + i, 3] = model.project_code.Substring(0, 1);
                //申请人
                sheet.Cells[beginIndex + i, 4] = model.requestorname;
                //组别
                sheet.Cells[beginIndex + i, 5] = model.requestor_group;
                //原PR号
                sheet.Cells[beginIndex + i, 6] = model.PrNo;
                //原PR金额
                sheet.Cells[beginIndex + i, 7] = model.OTotalPrice.ToString("#,##0.####");
                #endregion

                #region 新PR信息
                ESP.Purchase.Entity.MediaPREditHisInfo mediaPREditHis = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(model.id);
                if (mediaPREditHis != null)
                {
                    GeneralInfo newModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(mediaPREditHis.NewPRId == null ? 0 : mediaPREditHis.NewPRId.Value);
                    if (newModel != null)
                    {
                        //新PR号(中创)
                        sheet.Cells[beginIndex + i, 8] = newModel.PrNo;
                        //新PR号金额
                        List<OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(newModel.id);
                        decimal totalPrice = 0;
                        foreach (OrderInfo order in orderList)
                        {
                            totalPrice += order.total;
                        }
                        sheet.Cells[beginIndex + i, 9] = totalPrice.ToString("#,##0.####");
                        //付款日期
                        DataTable paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetPaymentPeriodList(newModel.id);
                        if (paymentList != null && paymentList.Rows.Count > 0)
                        {
                            sheet.Cells[beginIndex + i, 10] = DateTime.Parse(paymentList.Rows[0]["beginDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                    }
                }
                #endregion
            }


            workbook.Saved = true;

            string tmpFileName = "tmpCollate" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 导出YANE对账信息(根据新PR对象列表）
        /// </summary>
        /// <param name="list"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        public static void ExportCollateInfoByNewPR(DataTable list, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "Collate.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            int beginIndex = 3;
            for (int i = 0; i < list.Rows.Count; i++)
            {
                #region 原PR信息
                GeneralInfo model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(int.Parse(list.Rows[i]["id"].ToString()));
                ESP.Purchase.Entity.MediaPREditHisInfo mediaPREditHis = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByNewPRID(model.id);
                if (mediaPREditHis != null)
                {
                    GeneralInfo oldModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(mediaPREditHis.OldPRId.Value);
                    //项目号
                    sheet.Cells[beginIndex + i, 2] = oldModel.project_code;
                    //公司代码
                    sheet.Cells[beginIndex + i, 3] = oldModel.project_code.Substring(0, 1);
                    //申请人
                    sheet.Cells[beginIndex + i, 4] = oldModel.requestorname;
                    //组别
                    sheet.Cells[beginIndex + i, 5] = oldModel.requestor_group;
                    //原PR号
                    sheet.Cells[beginIndex + i, 6] = oldModel.PrNo;
                    //原PR金额
                    sheet.Cells[beginIndex + i, 7] = ESP.Purchase.BusinessLogic.OrderInfoManager.getTotalPriceByGID(oldModel.id).ToString("#,##0.####");
                }
                #endregion

                #region 新PR信息
                
                //新PR号(中创)
                sheet.Cells[beginIndex + i, 8] = model.id.ToString();
                sheet.Cells[beginIndex + i, 9] = model.PrNo;
                //新PR号金额
                List<OrderInfo> orderList = ESP.Purchase.BusinessLogic.OrderInfoManager.GetListByGeneralId(model.id);
                decimal totalPrice = 0;
                foreach (OrderInfo order in orderList)
                {
                    totalPrice += order.total;
                }
                sheet.Cells[beginIndex + i, 10] = totalPrice.ToString("#,##0.####");

                sheet.Cells[beginIndex + i, 11] = model.supplier_name;
                //付款日期
                DataTable paymentList = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetPaymentPeriodList(model.id);
                if (paymentList != null && paymentList.Rows.Count > 0)
                {
                    sheet.Cells[beginIndex + i, 12] = DateTime.Parse(paymentList.Rows[0]["beginDate"].ToString()).ToString("yyyy-MM-dd");
                }
                #endregion
            }


            workbook.Saved = true;

            string tmpFileName = "tmpCollate" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 导出日常采购数据报表
        /// </summary>
        /// <param name="items"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ExportDailyPurchase(DataRow[] items,  string strDate, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "DailyPurchase.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            sheet.Cells[2, 1] = "更新日期： " + strDate;
            int startRowIndex = 31;//起始行索引
            int rowIndex = 0; //顺序行索引
            string prePrNo = "";
            foreach (DataRow dr in items)
            {
                sheet.Cells[startRowIndex + rowIndex, 1] = rowIndex + 1;
                sheet.Cells[startRowIndex + rowIndex, 2] = dr["requisition_committime"].ToString() == State.datetime_minvalue ? "" : dr["requisition_committime"];
                sheet.Cells[startRowIndex + rowIndex, 3] = dr["PrNo"];
                sheet.Cells[startRowIndex + rowIndex, 4] = dr["supplier_name"];
                sheet.Cells[startRowIndex + rowIndex, 5] = dr["source"];
                sheet.Cells[startRowIndex + rowIndex, 6] = dr["item_no"];
                sheet.Cells[startRowIndex + rowIndex, 7] = dr["project_code"];
                sheet.Cells[startRowIndex + rowIndex, 8] = dr["desctiprtion"];
                sheet.Cells[startRowIndex + rowIndex, 9] = decimal.Parse(dr["total"] == DBNull.Value ? "0" : dr["total"].ToString()).ToString("#,##0.####");
                if (prePrNo != dr["PrNo"].ToString())
                {
                    sheet.Cells[startRowIndex + rowIndex, 10] = decimal.Parse(dr["sow4"] == DBNull.Value ? "0" : dr["sow4"].ToString()).ToString("#,##0.####");

                    sheet.Cells[startRowIndex + rowIndex, 12] = dr["contrast"];
                    sheet.Cells[startRowIndex + rowIndex, 13] = dr["consult"];
                }
                sheet.Cells[startRowIndex + rowIndex, 11] = dr["type"];

                Hashtable ht = State.InitNameHashtable();
                string tmpCode = dr["project_code"].ToString().Length > 0 ? dr["project_code"].ToString().Substring(0,1).ToUpper() : "";
                if(ht.ContainsKey(tmpCode))
                    sheet.Cells[startRowIndex + rowIndex, 14] = ht[tmpCode].ToString();
                sheet.Cells[startRowIndex + rowIndex, 15] = dr["Department"];
                sheet.Cells[startRowIndex + rowIndex, 16] = dr["requestorname"];
                sheet.Cells[startRowIndex + rowIndex, 17] = dr["endusername"];
                sheet.Cells[startRowIndex + rowIndex, 18] = dr["requestor_info"];
                sheet.Cells[startRowIndex + rowIndex, 19] = dr["typename"];
                sheet.Cells[startRowIndex + rowIndex, 20] = dr["first_assessorname"];
                sheet.Cells[startRowIndex + rowIndex, 21] = dr["order_audittime"].ToString() == State.datetime_minvalue ? "" : dr["order_audittime"];
                sheet.Cells[startRowIndex + rowIndex, 22] = dr["afterwardsname"];
                sheet.Cells[startRowIndex + rowIndex, 23] = "";
                sheet.Cells[startRowIndex + rowIndex, 24] = dr["supplier_linkman"];
                sheet.Cells[startRowIndex + rowIndex, 25] = dr["supplier_phone"];
                sheet.Cells[startRowIndex + rowIndex, 26] = "";

                prePrNo = dr["PrNo"].ToString();
                rowIndex++;
            }

            workbook.Saved = true;

            string tmpFileName = "tmpDailyPurchase" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally{
            
            }
            return "";
        }

        /// <summary>
        /// 导出事后申请报表
        /// </summary>
        /// <param name="items"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ExportAfterwards(DataRow[] items, string strDate, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "Afterwards.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            //插入行
            if (items.Length > 14)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[4, Type.Missing];
                for (int i = 0; i < (items.Length - 14); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                }
            }

            sheet.Cells[1, 15] = strDate;//月份
            int startRowIndex = 3;//起始行索引
            int rowIndex = 0; //顺序行索引
            string ids = ",";
            foreach (DataRow dr in items)
            {
                if (ids.IndexOf("," + dr["id"].ToString().Trim() + ",") >= 0)
                {
                    continue;
                }
                sheet.Cells[startRowIndex + rowIndex, 2] = rowIndex + 1;
                sheet.Cells[startRowIndex + rowIndex, 3] = dr["requestorname"];
                sheet.Cells[startRowIndex + rowIndex, 4] = dr["Department"];
                sheet.Cells[startRowIndex + rowIndex, 5] = dr["app_date"];
                sheet.Cells[startRowIndex + rowIndex, 6] = dr["project_code"];
                sheet.Cells[startRowIndex + rowIndex, 7] = dr["project_descripttion"];
                sheet.Cells[startRowIndex + rowIndex, 8] = dr["supplier_name"];
                sheet.Cells[startRowIndex + rowIndex, 9] = dr["source"]; 
                sheet.Cells[startRowIndex + rowIndex, 10] = dr["typename"];
                sheet.Cells[startRowIndex + rowIndex, 11] = dr["item_no"];
                sheet.Cells[startRowIndex + rowIndex, 12] = ESP.Purchase.BusinessLogic.OrderInfoManager.getTotalPriceByGID(Convert.ToInt32(dr["id"])).ToString("#,##0.####");//decimal.Parse(dr["total"].ToString()).ToString("#,##0.####");
                sheet.Cells[startRowIndex + rowIndex, 13] = dr["afterwardsReason"];
                sheet.Cells[startRowIndex + rowIndex, 14] = dr["intend_receipt_date"].ToString().Replace('#', '至');
                sheet.Cells[startRowIndex + rowIndex, 15] = dr["prNo"];
                sheet.Cells[startRowIndex + rowIndex, 16] = "";//备注

                ids += dr["id"].ToString().Trim() + ",";
                rowIndex++;
            }

            workbook.Saved = true;

            string tmpFileName = "tmpAfterwards" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }


        /// <summary>
        /// 导出客户指定供应商报表
        /// </summary>
        /// <param name="items"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ExportCusAsk(DataRow[] items, string strDate, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "CusAsk.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            //插入行
            if (items.Length > 14)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[4, Type.Missing];
                for (int i = 0; i < (items.Length - 14); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                }
            }

            sheet.Cells[1, 15] = strDate;//月份
            int startRowIndex = 3;//起始行索引
            int rowIndex = 0; //顺序行索引
            foreach (DataRow dr in items)
            {
                sheet.Cells[startRowIndex + rowIndex, 2] = rowIndex + 1;
                sheet.Cells[startRowIndex + rowIndex, 3] = dr["requestorname"];
                sheet.Cells[startRowIndex + rowIndex, 4] = dr["Department"];
                sheet.Cells[startRowIndex + rowIndex, 5] = dr["app_date"];
                sheet.Cells[startRowIndex + rowIndex, 6] = dr["project_code"];
                sheet.Cells[startRowIndex + rowIndex, 7] = dr["project_descripttion"];
                sheet.Cells[startRowIndex + rowIndex, 8] = dr["supplier_name"];
                sheet.Cells[startRowIndex + rowIndex, 9] = dr["CusName"];
                sheet.Cells[startRowIndex + rowIndex, 10] = dr["typename"];
                sheet.Cells[startRowIndex + rowIndex, 11] = dr["item_no"];
                sheet.Cells[startRowIndex + rowIndex, 12] = decimal.Parse(dr["total"].ToString()).ToString("#,##0.####");
                sheet.Cells[startRowIndex + rowIndex, 13] = dr["CusAskYesReason"];
                sheet.Cells[startRowIndex + rowIndex, 14] = dr["intend_receipt_date"].ToString().Replace('#', '至');
                sheet.Cells[startRowIndex + rowIndex, 15] = dr["prNo"];
                sheet.Cells[startRowIndex + rowIndex, 16] = "";//备注


                rowIndex++;
            }

            workbook.Saved = true;

            string tmpFileName = "tmpCusAsk" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }

        /// <summary>
        /// 特殊申请报表
        /// </summary>
        /// <param name="items"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ExportEmBuy(DataRow[] items,  string strDate, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "EmBuy.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            //插入行
            if (items.Length > 14)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[4, Type.Missing];
                for (int i = 0; i < (items.Length - 14); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                }
            }

            sheet.Cells[1, 15] = strDate;//月份
            int startRowIndex = 3;//起始行索引
            int rowIndex = 0; //顺序行索引
            foreach (DataRow dr in items)
            {
                sheet.Cells[startRowIndex + rowIndex, 2] = rowIndex + 1;
                sheet.Cells[startRowIndex + rowIndex, 3] = dr["requestorname"];
                sheet.Cells[startRowIndex + rowIndex, 4] = dr["Department"];
                sheet.Cells[startRowIndex + rowIndex, 5] = dr["app_date"];
                sheet.Cells[startRowIndex + rowIndex, 6] = dr["project_code"];
                sheet.Cells[startRowIndex + rowIndex, 7] = dr["project_descripttion"];
                sheet.Cells[startRowIndex + rowIndex, 8] = dr["supplier_name"];
                sheet.Cells[startRowIndex + rowIndex, 9] = dr["source"];
                sheet.Cells[startRowIndex + rowIndex, 10] = dr["typename"];
                sheet.Cells[startRowIndex + rowIndex, 11] = dr["item_no"];
                sheet.Cells[startRowIndex + rowIndex, 12] = decimal.Parse(dr["total"].ToString()).ToString("#,##0.####");
                sheet.Cells[startRowIndex + rowIndex, 13] = dr["EmBuyReason"];
                sheet.Cells[startRowIndex + rowIndex, 14] = dr["intend_receipt_date"].ToString().Replace('#', '至');
                sheet.Cells[startRowIndex + rowIndex, 15] = dr["prNo"];
                sheet.Cells[startRowIndex + rowIndex, 16] = "";//备注


                rowIndex++;
            }

            workbook.Saved = true;

            string tmpFileName = "tmpEmBuy" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }

        public static void ExprotCostReport(DataTable dt, string strDate, string mapPath, HttpResponse response)
        {
            #region 创建新的Table
            DataTable newDT = new DataTable();
            newDT.Columns.Add("A");
            newDT.Columns.Add("AID");
            newDT.Columns.Add("B");
            newDT.Columns.Add("BID");
            newDT.Columns.Add("C");
            newDT.Columns.Add("D");
            newDT.Columns.Add("E");
            newDT.Columns.Add("F");
            newDT.Columns.Add("G");
            newDT.Columns.Add("H");
            newDT.Columns.Add("I");
            newDT.Columns.Add("J");
            newDT.Columns.Add("K");
            newDT.Columns.Add("L");
            newDT.Columns.Add("M");
            newDT.Columns.Add("N");
            newDT.Columns.Add("O");
            newDT.Columns.Add("P");
            newDT.Columns.Add("Q");
            newDT.Columns.Add("R");
            newDT.Columns.Add("S");
            newDT.Columns.Add("T");
            newDT.Columns.Add("U");
            newDT.Columns.Add("V");
            newDT.Columns.Add("W");
            newDT.Columns.Add("X");
            newDT.Columns.Add("Y");
            newDT.Columns.Add("Z");

            DataTable newSheet2DT = newDT.Copy();
            DataTable newSheet3DT = newDT.Copy();
            DataTable newMediaDT = newDT.Copy();
            #endregion


            //加物料类别
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (newDT.Select("AID='" + dt.Rows[i]["producttype"] + "'").Length == 0 && newMediaDT.Select("AID='" + dt.Rows[i]["producttype"] + "'").Length == 0)//sheet1 第三级物料类别
                {
                    ESP.Purchase.Entity.TypeInfo typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(int.Parse(dt.Rows[i]["producttype"].ToString()));
                    ESP.Framework.Entity.EmployeeInfo employeeModel;
                    
                        DataRow dr = newDT.NewRow();
                        dr["A"] = dt.Rows[i]["typename"].ToString();
                        dr["AID"] = dt.Rows[i]["producttype"].ToString();
                        employeeModel = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(typeModel.auditorid));
                        dr["B"] = employeeModel == null ? "" : employeeModel.FullNameCN;//dt.Rows[i]["first_assessorname"].ToString();
                        dr["BID"] = employeeModel == null ? "" : employeeModel.UserID.ToString(); //dt.Rows[i]["first_assessor"].ToString();
                        newDT.Rows.Add(dr);
                    
                }

                if (newSheet2DT.Select("AID='" + dt.Rows[i]["typleLevel2id"] + "'").Length == 0)//sheet2 第二级物料类别
                {
                    DataRow dr = newSheet2DT.NewRow();
                    dr["A"] = dt.Rows[i]["typeLevel2Name"].ToString();
                    dr["AID"] = dt.Rows[i]["typleLevel2id"].ToString();
                    dr["B"] = "";
                    dr["BID"] = "";
                    newSheet2DT.Rows.Add(dr);
                }

                if (newSheet3DT.Select("AID='" + dt.Rows[i]["typleLevel3id"] + "'").Length == 0)//sheet2 第一级物料类别
                {
                    DataRow dr = newSheet3DT.NewRow();
                    dr["A"] = dt.Rows[i]["typeLevel3Name"].ToString();
                    dr["AID"] = dt.Rows[i]["typleLevel3id"].ToString();
                    dr["B"] = "";
                    dr["BID"] = "";
                    newSheet3DT.Rows.Add(dr);
                }
            }

            #region 第一级物料
            List<string> tmpGid3 = new List<string>();
            for (int i = 0; i < newDT.Rows.Count; i++)
            {
                DataRow[] dr = dt.Select("producttype=" + newDT.Rows[i]["AID"].ToString());
                int contractNum = 0; //合同数量
                int XcontractNum = 0;//协议供应商合同数量
                int FcontractNum = 0;//非协议供应商合同数量
                int ScontractNum = 0;//事后合同数量

                decimal YPrice = 0; //一般信息金额
                decimal XPrice = 0; //协议供应商金额
                decimal FPrice = 0; //非协议供应商金额
                decimal SPrice = 0; //事后金额
                decimal YFPrice = 0; //预付款
                decimal BJPrice = 0; //比价节约
                decimal YJPrice = 0; //议价节约


                List<string> XSupplier = new List<string>(); //协议供应商
                List<string> FSupplier = new List<string>(); //非协议供应商

                #region 赋值
                foreach (DataRow tmp in dr)
                {
                    if (!tmpGid3.Contains(tmp["general_id"].ToString()))
                    {
                        tmpGid3.Add(tmp["general_id"].ToString());
                        contractNum++;
                        if (tmp["source"].ToString() == "协议供应商")
                            XcontractNum++;
                        else
                            FcontractNum++;
                        if (tmp["afterwardsname"].ToString() == "是")
                            ScontractNum++;

                        YFPrice += decimal.Parse(tmp["sow4"].ToString());
                        BJPrice += decimal.Parse(tmp["contrast"].ToString());
                        YJPrice += decimal.Parse(tmp["consult"].ToString());
                    }
                    YPrice += decimal.Parse(tmp["total"].ToString());
                    if (tmp["source"].ToString() == "协议供应商")
                    {
                        XPrice += decimal.Parse(tmp["total"].ToString());
                        if (!XSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            XSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    else
                    {
                        FPrice += decimal.Parse(tmp["total"].ToString());
                        if (!FSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            FSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    if (tmp["afterwardsname"].ToString() == "是")
                        SPrice += decimal.Parse(tmp["total"].ToString());

                }
                #endregion

                newDT.Rows[i]["C"] = contractNum;
                newDT.Rows[i]["E"] = YPrice.ToString("#,##0.00");
                newDT.Rows[i]["G"] = XSupplier.Count;
                newDT.Rows[i]["H"] = XcontractNum;
                newDT.Rows[i]["J"] = XPrice.ToString("#,##0.00");
                newDT.Rows[i]["L"] = FSupplier.Count;
                newDT.Rows[i]["M"] = FcontractNum;
                newDT.Rows[i]["O"] = FPrice.ToString("#,##0.00");
                newDT.Rows[i]["Q"] = ScontractNum;
                newDT.Rows[i]["S"] = SPrice.ToString("#,##0.00");
                newDT.Rows[i]["U"] = YFPrice.ToString("#,##0.00");
                newDT.Rows[i]["W"] = BJPrice.ToString("#,##0.00");
                newDT.Rows[i]["Y"] = YJPrice.ToString("#,##0.00");

            }

            List<string> tmpGid4 = new List<string>();
            for (int i = 0; i < newMediaDT.Rows.Count; i++)
            {
                DataRow[] dr = dt.Select("producttype=" + newMediaDT.Rows[i]["AID"].ToString());
                int contractNum = 0; //合同数量
                int XcontractNum = 0;//协议供应商合同数量
                int FcontractNum = 0;//非协议供应商合同数量
                int ScontractNum = 0;//事后合同数量

                decimal YPrice = 0; //一般信息金额
                decimal XPrice = 0; //协议供应商金额
                decimal FPrice = 0; //非协议供应商金额
                decimal SPrice = 0; //事后金额
                decimal YFPrice = 0; //预付款
                decimal BJPrice = 0; //比价节约
                decimal YJPrice = 0; //议价节约


                List<string> XSupplier = new List<string>(); //协议供应商
                List<string> FSupplier = new List<string>(); //非协议供应商

                #region 赋值
                foreach (DataRow tmp in dr)
                {
                    if (!tmpGid4.Contains(tmp["general_id"].ToString()))
                    {
                        tmpGid4.Add(tmp["general_id"].ToString());
                        contractNum++;
                        if (tmp["source"].ToString() == "协议供应商")
                            XcontractNum++;
                        else
                            FcontractNum++;
                        if (tmp["afterwardsname"].ToString() == "是")
                            ScontractNum++;

                        YFPrice += decimal.Parse(tmp["sow4"].ToString());
                        BJPrice += decimal.Parse(tmp["contrast"].ToString());
                        YJPrice += decimal.Parse(tmp["consult"].ToString());
                    }
                    YPrice += decimal.Parse(tmp["total"].ToString());
                    if (tmp["source"].ToString() == "协议供应商")
                    {
                        XPrice += decimal.Parse(tmp["total"].ToString());
                        if (!XSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            XSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    else
                    {
                        FPrice += decimal.Parse(tmp["total"].ToString());
                        if (!FSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            FSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    if (tmp["afterwardsname"].ToString() == "是")
                        SPrice += decimal.Parse(tmp["total"].ToString());

                }
                #endregion

                newMediaDT.Rows[i]["C"] = contractNum;
                newMediaDT.Rows[i]["E"] = YPrice.ToString("#,##0.00");
                newMediaDT.Rows[i]["G"] = XSupplier.Count;
                newMediaDT.Rows[i]["H"] = XcontractNum;
                newMediaDT.Rows[i]["J"] = XPrice.ToString("#,##0.00");
                newMediaDT.Rows[i]["L"] = FSupplier.Count;
                newMediaDT.Rows[i]["M"] = FcontractNum;
                newMediaDT.Rows[i]["O"] = FPrice.ToString("#,##0.00");
                newMediaDT.Rows[i]["Q"] = ScontractNum;
                newMediaDT.Rows[i]["S"] = SPrice.ToString("#,##0.00");
                newMediaDT.Rows[i]["U"] = YFPrice.ToString("#,##0.00");
                newMediaDT.Rows[i]["W"] = BJPrice.ToString("#,##0.00");
                newMediaDT.Rows[i]["Y"] = YJPrice.ToString("#,##0.00");

            }
            #endregion

            #region 第二级物料
            List<string> tmpGid2 = new List<string>();
            for (int i = 0; i < newSheet2DT.Rows.Count; i++)
            {
                DataRow[] dr = dt.Select("typleLevel2id=" + newSheet2DT.Rows[i]["AID"].ToString());
                int contractNum = 0; //合同数量
                int XcontractNum = 0;//协议供应商合同数量
                int FcontractNum = 0;//非协议供应商合同数量
                int ScontractNum = 0;//事后合同数量

                decimal YPrice = 0; //一般信息金额
                decimal XPrice = 0; //协议供应商金额
                decimal FPrice = 0; //非协议供应商金额
                decimal SPrice = 0; //事后金额
                decimal YFPrice = 0; //预付款
                decimal BJPrice = 0; //比价节约
                decimal YJPrice = 0; //议价节约


                List<string> XSupplier = new List<string>(); //协议供应商
                List<string> FSupplier = new List<string>(); //非协议供应商

                #region 赋值
                foreach (DataRow tmp in dr)
                {
                    if (!tmpGid2.Contains(tmp["general_id"].ToString()))
                    {
                        tmpGid2.Add(tmp["general_id"].ToString());
                        contractNum++;
                        if (tmp["source"].ToString() == "协议供应商")
                            XcontractNum++;
                        else
                            FcontractNum++;
                        if (tmp["afterwardsname"].ToString() == "是")
                            ScontractNum++;

                        YFPrice += decimal.Parse(tmp["sow4"].ToString());
                        BJPrice += decimal.Parse(tmp["contrast"].ToString());
                        YJPrice += decimal.Parse(tmp["consult"].ToString());
                    }
                    YPrice += decimal.Parse(tmp["total"].ToString());
                    if (tmp["source"].ToString() == "协议供应商")
                    {
                        XPrice += decimal.Parse(tmp["total"].ToString());
                        if (!XSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            XSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    else
                    {
                        FPrice += decimal.Parse(tmp["total"].ToString());
                        if (!FSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            FSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    if (tmp["afterwardsname"].ToString() == "是")
                        SPrice += decimal.Parse(tmp["total"].ToString());

                }
                #endregion

                newSheet2DT.Rows[i]["C"] = contractNum;
                newSheet2DT.Rows[i]["E"] = YPrice.ToString("#,##0.00");
                newSheet2DT.Rows[i]["G"] = XSupplier.Count;
                newSheet2DT.Rows[i]["H"] = XcontractNum;
                newSheet2DT.Rows[i]["J"] = XPrice.ToString("#,##0.00");
                newSheet2DT.Rows[i]["L"] = FSupplier.Count;
                newSheet2DT.Rows[i]["M"] = FcontractNum;
                newSheet2DT.Rows[i]["O"] = FPrice.ToString("#,##0.00");
                newSheet2DT.Rows[i]["Q"] = ScontractNum;
                newSheet2DT.Rows[i]["S"] = SPrice.ToString("#,##0.00");
                newSheet2DT.Rows[i]["U"] = YFPrice.ToString("#,##0.00");
                newSheet2DT.Rows[i]["W"] = BJPrice.ToString("#,##0.00");
                newSheet2DT.Rows[i]["Y"] = YJPrice.ToString("#,##0.00");

            }
            #endregion 第二级物料

            #region 第一级物料
            List<string> tmpGid1 = new List<string>();
            for (int i = 0; i < newSheet3DT.Rows.Count; i++)
            {
                DataRow[] dr = dt.Select("typleLevel3id=" + newSheet3DT.Rows[i]["AID"].ToString());
                int contractNum = 0; //合同数量
                int XcontractNum = 0;//协议供应商合同数量
                int FcontractNum = 0;//非协议供应商合同数量
                int ScontractNum = 0;//事后合同数量

                decimal YPrice = 0; //一般信息金额
                decimal XPrice = 0; //协议供应商金额
                decimal FPrice = 0; //非协议供应商金额
                decimal SPrice = 0; //事后金额
                decimal YFPrice = 0; //预付款
                decimal BJPrice = 0; //比价节约
                decimal YJPrice = 0; //议价节约


                List<string> XSupplier = new List<string>(); //协议供应商
                List<string> FSupplier = new List<string>(); //非协议供应商

                #region 赋值
                foreach (DataRow tmp in dr)
                {
                    if (!tmpGid1.Contains(tmp["general_id"].ToString()))
                    {
                        tmpGid1.Add(tmp["general_id"].ToString());
                        contractNum++;
                        if (tmp["source"].ToString() == "协议供应商")
                            XcontractNum++;
                        else
                            FcontractNum++;
                        if (tmp["afterwardsname"].ToString() == "是")
                            ScontractNum++;

                        YFPrice += decimal.Parse(tmp["sow4"].ToString());
                        BJPrice += decimal.Parse(tmp["contrast"].ToString());
                        YJPrice += decimal.Parse(tmp["consult"].ToString());
                    }
                    YPrice += decimal.Parse(tmp["total"].ToString());
                    if (tmp["source"].ToString() == "协议供应商")
                    {
                        XPrice += decimal.Parse(tmp["total"].ToString());
                        if (!XSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            XSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    else
                    {
                        FPrice += decimal.Parse(tmp["total"].ToString());
                        if (!FSupplier.Contains(tmp["supplier_name"].ToString()))
                        {
                            FSupplier.Add(tmp["supplier_name"].ToString());
                        }
                    }
                    if (tmp["afterwardsname"].ToString() == "是")
                        SPrice += decimal.Parse(tmp["total"].ToString());

                }
                #endregion

                newSheet3DT.Rows[i]["C"] = contractNum;
                newSheet3DT.Rows[i]["E"] = YPrice.ToString("#,##0.00");
                newSheet3DT.Rows[i]["G"] = XSupplier.Count;
                newSheet3DT.Rows[i]["H"] = XcontractNum;
                newSheet3DT.Rows[i]["J"] = XPrice.ToString("#,##0.00");
                newSheet3DT.Rows[i]["L"] = FSupplier.Count;
                newSheet3DT.Rows[i]["M"] = FcontractNum;
                newSheet3DT.Rows[i]["O"] = FPrice.ToString("#,##0.00");
                newSheet3DT.Rows[i]["Q"] = ScontractNum;
                newSheet3DT.Rows[i]["S"] = SPrice.ToString("#,##0.00");
                newSheet3DT.Rows[i]["U"] = YFPrice.ToString("#,##0.00");
                newSheet3DT.Rows[i]["W"] = BJPrice.ToString("#,##0.00");
                newSheet3DT.Rows[i]["Y"] = YJPrice.ToString("#,##0.00");

            }
            #endregion

            #region 非协议供应商类别统计
            DataTable dtFitems = new DataTable();
            dtFitems.Columns.Add("A");
            dtFitems.Columns.Add("AID");
            dtFitems.Columns.Add("B");
            dtFitems.Columns.Add("C");
            dtFitems.Columns.Add("D");
            dtFitems.Columns.Add("E");
            dtFitems.Columns.Add("F");

            DataRow TJdr = dtFitems.NewRow();
            dtFitems.Rows.Add(TJdr);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dtFitems.Select("AID='" + dt.Rows[i]["first_assessor"] + "'").Length == 0)
            //    {
            //        DataRow dr = dtFitems.NewRow();
            //        dr["A"] = dt.Rows[i]["first_assessorname"].ToString();
            //        dr["AID"] = dt.Rows[i]["first_assessor"].ToString();
            //        dtFitems.Rows.Add(dr);
            //    }
            //}


            //for (int i = 0; i < dtFitems.Rows.Count; i++)
            //{
            //    DataRow[] dr = dt.Select("first_assessor=" + dtFitems.Rows[i]["AID"].ToString());
                int cgCount = 0; //采购部推荐供应商数量
                int khCount = 0; //客户指定供应商数量
                int ywCount = 0; //业务推荐供应商数量
                int lsCount = 0; //临时供应商数量

                List<string> tmpGid = new List<string>();
                foreach (DataRow tmp in dt.Rows)
                {
                    if (!tmpGid.Contains(tmp["id"].ToString()))
                    {
                        if (tmp["source"].ToString() == State.suppliersource_cg)
                            cgCount++;
                        else if (tmp["source"].ToString() == State.suppliersource_kh)
                            khCount++;
                        else if (tmp["source"].ToString() == State.suppliersource_yw)
                            ywCount++;
                        else if (tmp["source"].ToString() == State.suppliersource_ls)
                            lsCount++;
                        tmpGid.Add(tmp["general_id"].ToString());
                    }
                }
                dtFitems.Rows[0]["B"] = cgCount;
                dtFitems.Rows[0]["C"] = khCount;
                dtFitems.Rows[0]["D"] = ywCount;
                dtFitems.Rows[0]["E"] = lsCount;
                dtFitems.Rows[0]["F"] = cgCount + khCount + ywCount + lsCount;
            //}
            #endregion

            ExprotCostReport(newDT.Select(), newMediaDT.Select(), newSheet2DT.Select(),newSheet3DT.Select(), dtFitems.Select(), strDate, mapPath, response);
            GC.Collect();
        }

        /// <summary>
        /// 导出成本分析报告
        /// </summary>
        /// <param name="items"></param>
        /// <param name="strDate"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string ExprotCostReport(DataRow[] items,DataRow[] mediaItems ,DataRow[] sheet2Rows,DataRow[] sheet3Rows, DataRow[] fitems, string strDate, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "CostReport.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
            Microsoft.Office.Interop.Excel.Worksheet sheet2 = (Microsoft.Office.Interop.Excel.Worksheet)sheets[2];
            Microsoft.Office.Interop.Excel.Worksheet sheet3 = (Microsoft.Office.Interop.Excel.Worksheet)sheets[3];

            #region sheet1
            int insertRowCount = 0;
            //插入行
            if (items.Length > 2)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[7, Type.Missing];
                for (int i = 0; i < (items.Length - 2); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                    insertRowCount++;
                }
            }

            int startRowIndex = 6; //起始的行索引
            int rowIndex = 0;

            #region 成本分析报告
            foreach (DataRow dr in items)
            {
                sheet.Cells[startRowIndex + rowIndex, 1] = dr["A"];
                sheet.Cells[startRowIndex + rowIndex, 2] = dr["B"];
                sheet.Cells[startRowIndex + rowIndex, 3] = dr["C"];
                sheet.Cells[startRowIndex + rowIndex, 4] = "=C" + (startRowIndex + rowIndex).ToString() + "/$C$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 5] = dr["E"];
                sheet.Cells[startRowIndex + rowIndex, 6] = "=E" + (startRowIndex + rowIndex).ToString() + "/$E$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 7] = dr["G"];
                sheet.Cells[startRowIndex + rowIndex, 8] = dr["H"];
                sheet.Cells[startRowIndex + rowIndex, 9] = "=H" + (startRowIndex + rowIndex).ToString() + "/$H$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 10] = dr["J"];
                sheet.Cells[startRowIndex + rowIndex, 11] = "=J" + (startRowIndex + rowIndex).ToString() + "/$J$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 12] = dr["L"];
                sheet.Cells[startRowIndex + rowIndex, 13] = dr["M"];
                sheet.Cells[startRowIndex + rowIndex, 14] = "=M" + (startRowIndex + rowIndex).ToString() + "/$M$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 15] = dr["O"];
                sheet.Cells[startRowIndex + rowIndex, 16] = "=O" + (startRowIndex + rowIndex).ToString() + "/$O$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 17] = dr["Q"];
                sheet.Cells[startRowIndex + rowIndex, 18] = "=Q" + (startRowIndex + rowIndex).ToString() + "/$Q$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 19] = dr["S"];
                sheet.Cells[startRowIndex + rowIndex, 20] = "=S" + (startRowIndex + rowIndex).ToString() + "/$S$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 21] = dr["U"];
                sheet.Cells[startRowIndex + rowIndex, 22] = "=U" + (startRowIndex + rowIndex).ToString() + "/$U$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 23] = dr["W"];
                sheet.Cells[startRowIndex + rowIndex, 24] = "=W" + (startRowIndex + rowIndex).ToString() + "/$W$" + (items.Length + 6).ToString();
                sheet.Cells[startRowIndex + rowIndex, 25] = dr["Y"];
                sheet.Cells[startRowIndex + rowIndex, 26] = "=Y" + (startRowIndex + rowIndex).ToString() + "/$Y$" + (items.Length + 6).ToString();

                rowIndex++;
            }

            if (mediaItems.Length > 2)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[7 + 11 + insertRowCount, Type.Missing];
                for (int i = 0; i < (mediaItems.Length - 2); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                    insertRowCount++;
                }
            }

            foreach (DataRow dr in mediaItems)
            {
                int rowNum = startRowIndex + rowIndex + 9;
                sheet.Cells[rowNum, 1] = dr["A"];
                sheet.Cells[rowNum, 2] = dr["B"];
                sheet.Cells[rowNum, 3] = dr["C"];
                sheet.Cells[rowNum, 4] = "=C" + (rowNum).ToString() + "/$C$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 5] = dr["E"];
                sheet.Cells[rowNum, 6] = "=E" + (rowNum).ToString() + "/$E$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 7] = dr["G"];
                sheet.Cells[rowNum, 8] = dr["H"];
                sheet.Cells[rowNum, 9] = "=H" + (rowNum).ToString() + "/$H$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 10] = dr["J"];
                sheet.Cells[rowNum, 11] = "=J" + (rowNum).ToString() + "/$J$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 12] = dr["L"];
                sheet.Cells[rowNum, 13] = dr["M"];
                sheet.Cells[rowNum, 14] = "=M" + (rowNum).ToString() + "/$M$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 15] = dr["O"];
                sheet.Cells[rowNum, 16] = "=O" + (rowNum).ToString() + "/$O$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 17] = dr["Q"];
                sheet.Cells[rowNum, 18] = "=Q" + (rowNum).ToString() + "/$Q$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 19] = dr["S"];
                sheet.Cells[rowNum, 20] = "=S" + (rowNum).ToString() + "/$S$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 21] = dr["U"];
                sheet.Cells[rowNum, 22] = "=U" + (rowNum).ToString() + "/$U$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 23] = dr["W"];
                sheet.Cells[rowNum, 24] = "=W" + (rowNum).ToString() + "/$W$" + (mediaItems.Length + items.Length + 15).ToString();
                sheet.Cells[rowNum, 25] = dr["Y"];
                sheet.Cells[rowNum, 26] = "=Y" + (rowNum).ToString() + "/$Y$" + (mediaItems.Length + items.Length + 15).ToString();

                rowIndex++;
            }

            #endregion

            int FstartRowIndex = 25 + insertRowCount; //非协议供应商类别统计起始行索引

            //插入行
            if (fitems.Length > 2)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[17+insertRowCount, Type.Missing];
                for (int i = 0; i < (fitems.Length - 2); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                }
            }
            rowIndex = 0;
            foreach (DataRow dr in fitems)
            {
                //sheet.Cells[FstartRowIndex + rowIndex, 1] = dr["A"];
                sheet.Cells[FstartRowIndex + rowIndex, 1] = dr["B"];
                sheet.Cells[FstartRowIndex + rowIndex, 3] = dr["C"];
                sheet.Cells[FstartRowIndex + rowIndex, 4] = dr["D"];
                sheet.Cells[FstartRowIndex + rowIndex, 5] = dr["E"];
                sheet.Cells[FstartRowIndex + rowIndex, 6] = dr["F"];

                rowIndex++;
            }
            #endregion

            #region sheet2
            int insertRowCount2 = 0;
            //插入行
            if (sheet2Rows.Length > 2)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet2.Rows[7, Type.Missing];
                for (int i = 0; i < (sheet2Rows.Length - 2); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                    insertRowCount2++;
                }
            }

            int startRowIndex2 = 6; //起始的行索引
            int rowIndex2 = 0;

            #region 成本分析报告
            foreach (DataRow dr in sheet2Rows)
            {
                sheet2.Cells[startRowIndex2 + rowIndex2, 1] = dr["A"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 2] = dr["B"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 3] = dr["C"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 4] = "=C" + (startRowIndex2 + rowIndex2).ToString() + "/$C$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 5] = dr["E"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 6] = "=E" + (startRowIndex2 + rowIndex2).ToString() + "/$E$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 7] = dr["G"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 8] = dr["H"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 9] = "=H" + (startRowIndex2 + rowIndex2).ToString() + "/$H$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 10] = dr["J"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 11] = "=J" + (startRowIndex2 + rowIndex2).ToString() + "/$J$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 12] = dr["L"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 13] = dr["M"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 14] = "=M" + (startRowIndex2 + rowIndex2).ToString() + "/$M$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 15] = dr["O"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 16] = "=O" + (startRowIndex2 + rowIndex2).ToString() + "/$O$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 17] = dr["Q"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 18] = "=Q" + (startRowIndex2 + rowIndex2).ToString() + "/$Q$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 19] = dr["S"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 20] = "=S" + (startRowIndex2 + rowIndex2).ToString() + "/$S$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 21] = dr["U"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 22] = "=U" + (startRowIndex2 + rowIndex2).ToString() + "/$U$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 23] = dr["W"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 24] = "=W" + (startRowIndex2 + rowIndex2).ToString() + "/$W$" + (sheet2Rows.Length + 6).ToString();
                sheet2.Cells[startRowIndex2 + rowIndex2, 25] = dr["Y"];
                sheet2.Cells[startRowIndex2 + rowIndex2, 26] = "=Y" + (startRowIndex2 + rowIndex2).ToString() + "/$Y$" + (sheet2Rows.Length + 6).ToString();

                rowIndex2++;
            }
            #endregion

            #endregion

            #region sheet2
            int insertRowCount3 = 0;
            //插入行
            if (sheet3Rows.Length > 2)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet3.Rows[7, Type.Missing];
                for (int i = 0; i < (sheet3Rows.Length - 2); i++)
                {
                    range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                    insertRowCount3++;
                }
            }

            int startRowIndex3 = 6; //起始的行索引
            int rowIndex3 = 0;

            #region 成本分析报告
            foreach (DataRow dr in sheet3Rows)
            {
                sheet3.Cells[startRowIndex3 + rowIndex3, 1] = dr["A"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 2] = dr["B"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 3] = dr["C"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 4] = "=C" + (startRowIndex3 + rowIndex3).ToString() + "/$C$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 5] = dr["E"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 6] = "=E" + (startRowIndex3 + rowIndex3).ToString() + "/$E$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 7] = dr["G"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 8] = dr["H"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 9] = "=H" + (startRowIndex3 + rowIndex3).ToString() + "/$H$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 10] = dr["J"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 11] = "=J" + (startRowIndex3 + rowIndex3).ToString() + "/$J$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 12] = dr["L"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 13] = dr["M"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 14] = "=M" + (startRowIndex3 + rowIndex3).ToString() + "/$M$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 15] = dr["O"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 16] = "=O" + (startRowIndex3 + rowIndex3).ToString() + "/$O$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 17] = dr["Q"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 18] = "=Q" + (startRowIndex3 + rowIndex3).ToString() + "/$Q$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 19] = dr["S"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 20] = "=S" + (startRowIndex3 + rowIndex3).ToString() + "/$S$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 21] = dr["U"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 22] = "=U" + (startRowIndex3 + rowIndex3).ToString() + "/$U$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 23] = dr["W"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 24] = "=W" + (startRowIndex3 + rowIndex3).ToString() + "/$W$" + (sheet3Rows.Length + 6).ToString();
                sheet3.Cells[startRowIndex3 + rowIndex3, 25] = dr["Y"];
                sheet3.Cells[startRowIndex3 + rowIndex3, 26] = "=Y" + (startRowIndex3 + rowIndex3).ToString() + "/$Y$" + (sheet3Rows.Length + 6).ToString();

                rowIndex3++;
            }
            #endregion

            #endregion

            workbook.Saved = true;

            string tmpFileName = "tmpCostReport" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }

        public static string ExportRequisition(int generalid, string mapPath, HttpResponse response)
        {
            GeneralInfo generalInfo = new GeneralInfo();
            generalInfo = GeneralInfoManager.GetModel(generalid);
            string filename = mapPath + "ExcelTemplate\\" + "UploadRequisition.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];


            //申请人
            sheet.Cells[3, 1] = generalInfo.requestorname;
            //申请时间
            sheet.Cells[3, 2] = generalInfo.app_date.ToString("yyyy-MM-dd");
            //申请人业务组
            sheet.Cells[3, 4] = generalInfo.requestor_group;
            //申请人联络方式
            sheet.Cells[3, 3] = generalInfo.requestor_info;

            //使用人
            sheet.Cells[3, 5] = generalInfo.endusername;
            //使用人联络方式
            sheet.Cells[3, 6] = generalInfo.enduser_info.Replace("-", "#");
            //业务组
            sheet.Cells[3, 7] = generalInfo.enduser_group;
            //收货人
            sheet.Cells[3, 8] = generalInfo.receivername;
            //联络方式
            sheet.Cells[3, 9] = generalInfo.receiver_info.Replace("-", "#");
            //收货地址
            sheet.Cells[3, 10] = generalInfo.ship_address;
            //项目号
            sheet.Cells[3, 11] = generalInfo.project_code;
            //项目号内容描述
            sheet.Cells[3, 12] = generalInfo.project_descripttion;
            //第三方采购物料描述
            sheet.Cells[3, 13] = generalInfo.thirdParty_materielDesc;
            //第三方采购成本预算
            sheet.Cells[3, 14] = generalInfo.buggeted;
            //货币
            sheet.Cells[3, 15] = generalInfo.moneytype;
            //供应商名称
            sheet.Cells[3, 16] = generalInfo.supplier_name;
            //供应商来源
            sheet.Cells[3, 17] = generalInfo.source;
            //供应商地址
            sheet.Cells[3, 18] = generalInfo.supplier_address;
            //供应商联系人
            sheet.Cells[3, 19] = generalInfo.supplier_linkman;
            //联系电话
            sheet.Cells[3, 20] = generalInfo.supplier_phone.Replace("-", "#");
            //供应商手机
            sheet.Cells[3, 21] = generalInfo.Supplier_cellphone;
            //供应商传真
            sheet.Cells[3, 22] = generalInfo.supplier_fax.Replace("-", "#"); 
            //供应商邮件
            sheet.Cells[3, 23] = generalInfo.supplier_email;
            //框架协议号
            sheet.Cells[3, 24] = generalInfo.fa_no;
            //付款金额
            sheet.Cells[3, 25] = generalInfo.sow4;
            //付款账期
            sheet.Cells[3, 26] = generalInfo.payment_terms;
            //业务需求
            sheet.Cells[3, 27] = generalInfo.sow;

            List<OrderInfo> items = OrderInfoManager.GetOrderList(" and general_id = " + generalid);
            int rowIndex = 7;
            foreach (OrderInfo orderInfo in items)
            {
                sheet.Cells[rowIndex, 1] = orderInfo.producttypename;
                sheet.Cells[rowIndex, 2] = orderInfo.Item_No;
                sheet.Cells[rowIndex, 3] = orderInfo.desctiprtion;
                sheet.Cells[rowIndex, 4] = orderInfo.intend_receipt_date;
                sheet.Cells[rowIndex, 5] = orderInfo.price;
                sheet.Cells[rowIndex, 6] = orderInfo.uom;
                sheet.Cells[rowIndex, 7] = orderInfo.quantity;
                sheet.Cells[rowIndex, 8] = orderInfo.total;

                rowIndex++;
            }
            workbook.Saved = true;

            string tmpFileName = "tmpUploadRequisition" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return "";
        }

        /// <summary>
        /// 导出订单Excel
        /// </summary>
        /// <param name="generalid"></param>
        /// <param name="mapPath"></param>
        /// <param name="response"></param>
        public static string ToOrderInfoExcel(int generalid, string mapPath,HttpResponse response,bool isMail)
        {
                GeneralInfo generalInfo = new GeneralInfo();
                generalInfo = GeneralInfoManager.GetModel(generalid);
                string filename = mapPath + "ExcelTemplate\\" + "PO.xls";
                Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

                int insertrowcount = 0;
                //item列表项
                List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + generalInfo.id);


                if (orderList.Count > 3)
                {
                    //插入采购物品行
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[21, Type.Missing];
                    for (int i = 0; i < (orderList.Count - 3); i++)
                    {
                        range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                        insertrowcount++;
                    }
                }

                //订单号
                //sheet.Cells[1, 9] = generalInfo.orderid;
                sheet.Cells[2, 8] = generalInfo.orderid;
                ////供应商
                //sheet.Cells[8, 2] = generalInfo.supplier_name;
                //供应商名称
                sheet.Cells[8, 2] = generalInfo.supplier_name;
                //地址
                sheet.Cells[9, 2] = generalInfo.supplier_address;
                //联系人
                sheet.Cells[10, 2] = generalInfo.supplier_linkman;
                //联系电话
                sheet.Cells[11, 2] = generalInfo.supplier_phone;
                //传真 
                sheet.Cells[12, 2] = generalInfo.supplier_fax;
                //电子邮件
                sheet.Cells[13, 2] = generalInfo.supplier_email;
                //供应商来源
                //sheet.Cells[12, 3] = generalInfo.source;

                ////采购人
                //ESP.Compatible.Employee buyer = new ESP.Compatible.Employee(generalInfo.buyerid);
                //if (buyer != null)
                //{
                //    //公司名称
                //    sheet.Cells[8, 6] = buyer.GetDepartmentNames().Count == 0 ? "" : buyer.GetDepartmentNames()[0].ToString();
                //    //公司地址
                //    sheet.Cells[9, 6] = buyer.Address;
                //    //联系人
                //    sheet.Cells[10, 6] = buyer.Name;
                //    //联系电话
                //    sheet.Cells[11, 6] = buyer.Telephone;
                //    //传真
                //    //sheet.Cells[12, 6] = 暂无
                //    //电子邮件
                //    sheet.Cells[13, 6] = buyer.EMail;
                //}

                //送货至
                sheet.Cells[15, 2] = generalInfo.ship_address;

                //收货人
                sheet.Cells[29 + insertrowcount, 6] = generalInfo.receivername;
                //联络
                sheet.Cells[29 + insertrowcount, 8] = generalInfo.receiver_info;

                //备注
                sheet.Cells[25 + insertrowcount, 2] = generalInfo.sow3;

                //预付金额
                sheet.Cells[23 + insertrowcount, 6] = generalInfo.sow4;
                //付款账期
                sheet.Cells[24 + insertrowcount, 6] = generalInfo.payment_terms;    

                int count = 20;
                int num = 1;
                decimal totalprice = 0;
                foreach (OrderInfo o in orderList)
                {

                    sheet.Cells[count, 1] = num;
                    sheet.Cells[count, 2] = o.Item_No;
                    sheet.Cells[count, 3] = o.desctiprtion;
                    sheet.Cells[count, 4] = o.intend_receipt_date;
                    sheet.Cells[count, 5] = o.price;
                    sheet.Cells[count, 6] = generalInfo.moneytype;
                    sheet.Cells[count, 7] = o.uom;
                    sheet.Cells[count, 8] = o.quantity;
                    sheet.Cells[count, 9] = o.total;
                    totalprice += o.total;
                    if (count == (22+ insertrowcount))
                    {
                        break;
                    }
                    else
                        count++;
                    num++;
                }
                //总价
                sheet.Cells[23 + insertrowcount, 9] = (generalInfo.moneytype == "美元" ? "＄" : "￥") + totalprice.ToString("#.##0.00");

                //给行变颜色
                //for (int i = count; i <= 29; i++)
                //{
                //    //sheet.get_Range(sheet.Cells[i, 2], sheet.Cells[i, 15]).Select();
                //    sheet.get_Range(sheet.Cells[i, 1], sheet.Cells[i, 9]).Interior.ColorIndex = 15;
                //}

                ////36行
                //if ((generalInfo.sow != null && !"".Equals(generalInfo.sow.Trim())) || (generalInfo.sow2 != null && !"".Equals(generalInfo.sow2.Trim())) || (generalInfo.sow3 != null && !"".Equals(generalInfo.sow3.Trim())) || (generalInfo.sow4 != null && !"".Equals(generalInfo.sow4.Trim())) || (generalInfo.sow5 != null && !"".Equals(generalInfo.sow5.Trim())))
                //{
                //    sheet.Cells[36, 4] = "有.具体工作需求在附件《工作说明》中做出描述.";
                //    Microsoft.Office.Interop.Excel.Worksheet sheet2 = (Microsoft.Office.Interop.Excel.Worksheet)sheets[2];
                //    sheet2.Cells[7, 2] = generalInfo.sow;
                //}
                //else
                //{
                //    sheet.Cells[36, 4] = "没有";
                //}
                ////sheet.Cells[36,4] = generalInfo.sow;
                //sheet.Cells[36, 8] = generalInfo.payment_terms;

                ////46行
                //sheet.Cells[46, 2] = generalInfo.buyername;
                //sheet.Cells[46, 5] = generalInfo.buyer_contactinfo;
                //sheet.Cells[46, 7] = generalInfo.fa_no;
                //sheet.Cells[46, 9] = generalInfo.PrNo;
                //sheet.Cells[46, 11] = generalInfo.project_code;

                ////48行
                //sheet.Cells[48, 2] = generalInfo.requestorname;
                //sheet.Cells[48, 5] = generalInfo.requestor_info;
                //sheet.Cells[48, 7] = generalInfo.requestor_group;
                //sheet.Cells[48, 9] = generalInfo.endusername;
                //sheet.Cells[48, 11] = generalInfo.enduser_info;
                //sheet.Cells[48, 14] = generalInfo.enduser_group;

                ////50行
                //sheet.Cells[50, 2] = generalInfo.receivername;
                //sheet.Cells[50, 5] = generalInfo.receiver_info;
                //sheet.Cells[50, 6] = generalInfo.others;

                //申请人
                sheet.Cells[28 + insertrowcount, 2] = generalInfo.requestorname;
                //申请日期
                sheet.Cells[28 + insertrowcount, 4] = generalInfo.app_date;
                //联络电话
                sheet.Cells[28 + insertrowcount, 6] = generalInfo.requestor_info;
                //业务组别
                sheet.Cells[28 + insertrowcount, 8] = generalInfo.requestor_group;

                //使用人
                sheet.Cells[29 + insertrowcount, 2] = generalInfo.endusername;
                //联络电话
                sheet.Cells[29 + insertrowcount, 4] = generalInfo.enduser_info;

                ////采购专员
                //sheet.Cells[30 + insertrowcount, 2] = generalInfo.buyername;
                ////联络电话
                //sheet.Cells[30 + insertrowcount, 4] = generalInfo.buyer_contactinfo;

                //框架协议号码
                sheet.Cells[30 + insertrowcount, 6] = generalInfo.fa_no;
                //申请单号
                sheet.Cells[30 + insertrowcount, 8] = generalInfo.PrNo;
                //项目号
                sheet.Cells[31 + insertrowcount, 2] = generalInfo.project_code;
                //项目描述
                sheet.Cells[31 + insertrowcount, 4] = generalInfo.project_descripttion;
                //其他
                sheet.Cells[31 + insertrowcount, 8] = generalInfo.others;
                //工作需求描述
                sheet.Cells[23 + insertrowcount, 2] = generalInfo.sow;
                

                workbook.Saved = true;

                string tmpFileName = "tmpOrder" + DateTime.Now.Ticks.ToString() + ".xls";
                try
                {
                    workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                    if (isMail)
                        return mapPath + "ExcelTemplate\\" + tmpFileName;
                    else
                        outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    
                }
                return "";
        }

        public static void ToGeneralInfoExcel(int generalid, string mapPath, HttpResponse response)
        {
                GeneralInfo generalInfo = new GeneralInfo();
                generalInfo = GeneralInfoManager.GetModel(generalid);
                //string filename = mapPath + "ExcelTemplate\\" + "Requisition.xls";
                string filename = mapPath + "ExcelTemplate\\" + "PR.xls";
                Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

                int insertrowcount = 0;
                //item列表项
                List<OrderInfo> orderList = OrderInfoManager.GetOrderList(" and general_id =" + generalInfo.id);


                if (orderList.Count > 3)
                {
                    //插入采购物品行
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Rows[26, Type.Missing];
                    for (int i = 0; i < (orderList.Count - 3); i++)
                    { 
                        range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                        insertrowcount++;
                    }
                }

                //订单号
                sheet.Cells[1, 9] = generalInfo.PrNo;

                //申请人
                sheet.Cells[33+insertrowcount, 1] = generalInfo.requestorname;
                
                //申请日期
                sheet.Cells[33 + insertrowcount, 2] = generalInfo.app_date.ToString("yyyy-MM-dd");
                //联络
                sheet.Cells[33 + insertrowcount, 3] = generalInfo.requestor_info;
                //业务组别
                sheet.Cells[33 + insertrowcount, 4] = generalInfo.requestor_group;
                //使用人
                sheet.Cells[33 + insertrowcount, 6] = generalInfo.endusername;
                //联络
                sheet.Cells[33 + insertrowcount, 7] = generalInfo.enduser_info;
                //业务组别

                //收货人
                sheet.Cells[17, 3] = generalInfo.receivername;
                sheet.Cells[33 + insertrowcount, 8] = generalInfo.receivername;
                //联络
                sheet.Cells[18, 3] = generalInfo.receiver_info;
                sheet.Cells[33 + insertrowcount, 9] = generalInfo.receiver_info;
                //收货地
                sheet.Cells[16, 3] = generalInfo.ship_address;
                //项目号
                sheet.Cells[35 + insertrowcount, 1] = generalInfo.project_code;
                //项目号内容描述 
                sheet.Cells[35 + insertrowcount, 3] = generalInfo.project_descripttion;
                //第三方采购成本预算 
                sheet.Cells[35 + insertrowcount, 8] = generalInfo.buggeted;

                //第11行
                //供应商名称
                sheet.Cells[7, 3] = generalInfo.supplier_name;
                //地址
                sheet.Cells[8, 3] = generalInfo.supplier_address;
                //联系人
                sheet.Cells[9, 3] = generalInfo.supplier_linkman;
                //联系电话
                sheet.Cells[10, 3] = generalInfo.supplier_phone;
                //手机 
                //传真 
                sheet.Cells[11, 3] = generalInfo.supplier_fax;
                //电子邮件
                sheet.Cells[12, 3] = generalInfo.supplier_email;
                //供应商来源
                sheet.Cells[13, 3] = generalInfo.source;
                //备注
                sheet.Cells[30 + insertrowcount, 3] = generalInfo.sow3;

                //预付金额
                sheet.Cells[28 + insertrowcount, 6] = generalInfo.sow4;
                //付款账期
                sheet.Cells[29 + insertrowcount, 6] = generalInfo.payment_terms;

                ////采购人
                //ESP.Compatible.Employee buyer = new ESP.Compatible.Employee(generalInfo.buyerid);
                //if (buyer != null)
                //{
                //    sheet.Cells[16, 7] = buyer.GetDepartmentNames().Count == 0 ? "" : buyer.GetDepartmentNames()[0].ToString();
                //    sheet.Cells[17, 7] = buyer.Address;
                //    sheet.Cells[18, 7] = buyer.Name;
                //    sheet.Cells[19, 7] = buyer.Telephone;
                //    sheet.Cells[20, 7] = buyer.EMail;
                //}
                
                int count = 25;
                int num = 1;
                decimal totalprice = 0;
                foreach (OrderInfo o in orderList)
                {

                    sheet.Cells[count, 1] = num;
                    sheet.Cells[count, 2] = o.Item_No;
                    sheet.Cells[count, 3] = o.desctiprtion;
                    sheet.Cells[count, 4] = o.intend_receipt_date;
                    sheet.Cells[count, 5] = o.price;
                    sheet.Cells[count, 6] = generalInfo.moneytype;
                    sheet.Cells[count, 7] = o.uom;
                    sheet.Cells[count, 8] = o.quantity;
                    sheet.Cells[count, 9] = o.total;
                    totalprice += o.total;
                    if (count == (27+insertrowcount))
                    {
                        break;
                    }
                    else
                        count++;
                    num++;
                }

                //给行变颜色
                //for (int i = count; i <= 28; i++)
                //{
                //    //sheet.get_Range(sheet.Cells[i, 2], sheet.Cells[i, 15]).Select();
                //    sheet.get_Range(sheet.Cells[i, 1], sheet.Cells[i, 9]).Interior.ColorIndex = 15;
                //}

                //总价
                sheet.Cells[28 + insertrowcount, 9] = (generalInfo.moneytype == "美元" ? "＄" : "￥") + totalprice.ToString("#.##0.00");
                //工作需求描述
                sheet.Cells[28+insertrowcount, 2] = generalInfo.sow;

                workbook.Saved = true;

                string tmpFileName = "tmpRequisition" + DateTime.Now.Ticks + ".xls";
                try
                {
                    workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                    outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName,response);


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    
                }
        }
        
        private static decimal getMediaAvgPrice(int mediaID)
        {
            decimal avgPrice=0;
            List<MediaOrderInfo> medialist = MediaOrderManager.GetModelList("mediaid=" + mediaID.ToString());
            if(medialist==null || medialist.Count==0)
                return 0;
            else
            {
                int totalWordLength=0;
                decimal totalAmount=0;
            foreach(MediaOrderInfo item in medialist)
            {
                totalWordLength += item.WordLength == null ? 0 : item.WordLength.Value;
                totalAmount += item.TotalAmount == null ? 0 : item.TotalAmount.Value ;
            }
            if (totalWordLength == 0)
                return 0;
            else
                avgPrice=totalAmount/totalWordLength;
            }
            return avgPrice;
        }
        /// <summary>
        /// create excel for media writting fee
        /// </summary>
        /// <param name="userid">current user ID</param>
        /// <param name="serverpath"> purchase system server path</param>
        /// <param name="mediaorderIDs"> all of selected media order's ID</param>
        /// <returns>file name</returns>
        public static string CreateExcelForMedia(int userid, string serverpath,string templateServerPath,string mediaorderIDs)
        {
            List<MediaOrderInfo> reporterlist = new List<MediaOrderInfo>();
            string[] ids = mediaorderIDs.Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                MediaOrderInfo item = MediaOrderManager.GetModel(Convert.ToInt32(ids[i]),null);
                reporterlist.Add(item);
            }

           string ofname = "WritingFeeBill" + DateTime.Now.Ticks.ToString() + ".xls";
            if (System.IO.File.Exists(serverpath + ofname))//如果有则删除
            {
                try
                {
                    ofname = "WritingFeeBill_" + new Guid().ToString() + ".xls";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            string temppath = templateServerPath + "MediaWrittingFee.xls";
            //string filename = "WritingFeeBill.xls";
            //string filepath = serverpath + filename;
            //ENDTODO

            ExcelHandle excel = new ExcelHandle();
            try
            {
                excel.Load(temppath);
                excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
                if (reporterlist.Count > 0)
                {
                    string username = emp.Name;
                    string userdepartmentname = emp.PositionDescription;
                    string projectcode = MediaOrderManager.GetProjectCodeByMeidaOrderID(mediaorderIDs);
                    ExcelHandle.WriteAfterMerge(excel.CurSheet, "A1", "P1", string.Format("稿费报销明细单      \r\n 项目号：{0}               组别：{1}         分机号：{2}            申请人：{3}                报销日期：", projectcode, userdepartmentname, emp.Telephone, username));
                }

                int rownum = 3;
                string cell = "A3";
                var result = from item in reporterlist
                             group item by item.ReporterID;
                foreach (var rep in result)
                {
                    decimal total=0;
                    decimal wordlength = 0;
                    foreach (var r in rep)
                    {
                        total+=r.TotalAmount==null?0:r.TotalAmount.Value;
                        wordlength += r.WordLength == null ? 0 : r.WordLength.Value;
                    }
                        MediaOrderInfo reporter = rep.First() as  MediaOrderInfo;

                        cell = string.Format("A{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.MediaName);

                        cell = string.Format("B{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.Subject);

                        cell = string.Format("C{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "");

                        cell = string.Format("D{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, wordlength.ToString());

                        cell = string.Format("E{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, total.ToString());


                        cell = string.Format("F{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.ReporterName);

                        cell = string.Format("G{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.ReporterName);

                        cell = string.Format("H{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.CityName);

                        cell = string.Format("I{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.BankName);

                        cell = string.Format("J{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "");

                        cell = string.Format("K{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.CardNumber);

                        cell = string.Format("L{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.Tel);

                        cell = string.Format("M{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, total.ToString());
                    decimal projectAvgPrice =0;
                    if(wordlength==0)
                    {
                        cell = string.Format("N{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "");
                    }
                    else
                    {
                        projectAvgPrice = reporter.TotalAmount.Value / (wordlength);
                            cell = string.Format("N{0}", rownum);
                            ExcelHandle.WriteCell(excel.CurSheet, cell, projectAvgPrice.ToString("#,##0.00"));
                    }
                        cell = string.Format("O{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, getMediaAvgPrice(reporter.MediaID.Value).ToString("#,##0.00"));//row["MediaAvgprice"].ToString());

                        cell = string.Format("P{0}", rownum);
                        ExcelHandle.WriteCell(excel.CurSheet, cell, "");//row["isupload"].ToString());
                        rownum++;
                    }
                //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", cell);

                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("N{0}", rownum), "合计：");
                ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", string.Format("N{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                string signed = "  申请人签字:                       第一级批准人:                      第二级批准人:                       *第三级批准人:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                signed = "  日期:                             日期:                              日期:                               日期:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum += 2;

                cell = string.Format("A{0}", rownum);
                signed = "  媒介中心:                                    采购部:                                     财务部:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                signed = "  日期:                                        日期:                                       日期:";
                ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
                //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
                rownum++;

                cell = string.Format("A{0}", rownum);
                ExcelHandle.WriteCell(excel.CurSheet, cell, "注：*第三级批准人在金额超过一定标准时才需签署");
                //ExcelHandle.SetColor(excel.CurSheet, cell, System.Drawing.Color.Red);

                ExcelHandle.SaveAS(excel.CurBook, serverpath + ofname);
                excel.Dispose();
            }
            catch (Exception exp)
            {
                excel.Dispose();
                ofname = "稿费报销明细单.xls";
            }
            return ofname;
        }
        
        //public static string GetWritingFeeBill(int userid, OrderInfo orderinfo, GeneralInfo generalinfo, string serverpath, out string ofname, out bool isDelete, bool submitCreate)
        //{
        //    if (submitCreate)
        //    {
        //        ofname = "WritingFeeBill_" + orderinfo.id + ".xls";
        //    }
        //    else
        //    {
        //        ofname = "WritingFeeBill" + DateTime.Now.Ticks.ToString() + ".xls";
        //    }
        //    if (System.IO.File.Exists(serverpath + ofname))//如果有则删除
        //    {
        //        try
        //        {
        //            ofname = "WritingFeeBill_" +new Guid().ToString() + ".xls";
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //    PRMediaBLL.Reporter.Media_ReporterList reporterlist = (PRMediaBLL.Reporter.Media_ReporterList)ESP.Purchase.BusinessLogic.SerializeFactory.DeserializeObject(orderinfo.OrderContent);


        //    string temppath = serverpath + "稿费报销明细单.xls";
        //    //string filename = "WritingFeeBill.xls";
        //    //string filepath = serverpath + filename;
        //    //ENDTODO

        //    ExcelHandle excel = new ExcelHandle();
        //    try
        //    {
        //        excel.Load(temppath);
        //        excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
        //        ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
        //        if (reporterlist.ReporterList.Count > 0)
        //        {
        //            string username = emp.Name;
        //            string userdepartmentname = emp.PositionDescription;
        //            string projectcode = generalinfo.project_code;
        //            ExcelHandle.WriteAfterMerge(excel.CurSheet, "A1", "P1", string.Format("记者发稿费用申请单      \r\n 项目号：{0}               组别：{1}         分机号：{2}            申请人：{3}                报销日期：", projectcode, userdepartmentname, emp.Telephone, username));
        //        }

        //        int rownum = 3;
        //        string cell = "A3";
        //        foreach (PRMediaBLL.Reporter.Media_QueryReporter reporter in reporterlist.ReporterList)
        //        {
        //            cell = string.Format("A{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.Medianame);

        //            cell = string.Format("B{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.Subject);

        //            cell = string.Format("C{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, "");

        //            cell = string.Format("D{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.WordLength.ToString());

        //            cell = string.Format("E{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.TotalAmount.ToString());


        //            cell = string.Format("F{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.Reportername);

        //            cell = string.Format("G{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.Reportername);

        //            cell = string.Format("H{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell,reporter.CityName);

        //            cell = string.Format("I{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.BankName);

        //            cell = string.Format("J{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, "");

        //            cell = string.Format("K{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.CardNumber);

        //            cell = string.Format("L{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.Tel);

        //            cell = string.Format("M{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, reporter.TotalAmount.ToString());

        //            cell = string.Format("N{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, "");//row["ProjectAvgPrice"].ToString());

        //            cell = string.Format("O{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, "");//row["MediaAvgprice"].ToString());

        //            cell = string.Format("P{0}", rownum);
        //            ExcelHandle.WriteCell(excel.CurSheet, cell, "");//row["isupload"].ToString());
        //            rownum++;
        //        }

        //        //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", cell);

        //        cell = string.Format("A{0}", rownum);
        //        ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("N{0}", rownum), "合计：");
        //        ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
        //        //ExcelHandle.SetBorderAll(excel.CurSheet, "A4", string.Format("N{0}", rownum));
        //        rownum++;

        //        cell = string.Format("A{0}", rownum);
        //        string signed = "  申请人签字:                       第一级批准人:                      第二级批准人:                       *第三级批准人:";
        //        ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
        //        //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
        //        rownum++;

        //        cell = string.Format("A{0}", rownum);
        //        signed = "  日期:                             日期:                              日期:                               日期:";
        //        ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
        //        //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
        //        rownum += 2;

        //        cell = string.Format("A{0}", rownum);
        //        signed = "  媒介中心:                                    采购部:                                     财务部:";
        //        ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
        //        //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
        //        rownum++;

        //        cell = string.Format("A{0}", rownum);
        //        signed = "  日期:                                        日期:                                       日期:";
        //        ExcelHandle.WriteAfterMerge(excel.CurSheet, cell, string.Format("P{0}", rownum), signed);
        //        //ExcelHandle.SetBold(excel.CurSheet, cell, string.Format("P{0}", rownum));
        //        rownum++;

        //        cell = string.Format("A{0}", rownum);
        //        ExcelHandle.WriteCell(excel.CurSheet, cell, "注：*第三级批准人在金额超过一定标准时才需签署");
        //        //ExcelHandle.SetColor(excel.CurSheet, cell, System.Drawing.Color.Red);

        //        ExcelHandle.SaveAS(excel.CurBook, serverpath + ofname);
        //        excel.Dispose();
        //        if (submitCreate)
        //        {
        //            isDelete = false;//不用册除
        //        }
        //        else
        //        {
        //            isDelete = true;
        //        }
        //        return ofname;
        //    }
        //    catch (Exception exp)
        //    {
        //        excel.Dispose();
        //        ofname = "稿费报销明细单.xls";
        //        isDelete = false;
        //        return "稿费报销明细单.xls";
        //    }
        //}

        public static void ExportSupplier(List<SupplierInfo> supplierList, string mapPath, HttpResponse response)
        {
            string filename = mapPath + "ExcelTemplate\\" + "supplierInfo.xls";
            Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];

            int beginRowIndex = 8;
            foreach (SupplierInfo model in supplierList)
            {
                sheet.Cells[beginRowIndex, 1] = model.supplier_name;
                sheet.Cells[beginRowIndex, 2] = model.service_area;
                sheet.Cells[beginRowIndex, 3] = model.supplier_industry;
                sheet.Cells[beginRowIndex, 4] = model.supplier_scale;
                sheet.Cells[beginRowIndex, 5] = model.supplier_principal;
                sheet.Cells[beginRowIndex, 6] = model.supplier_builttime;
                sheet.Cells[beginRowIndex, 7] = model.supplier_website;
                sheet.Cells[beginRowIndex, 8] = model.supplier_source;
                sheet.Cells[beginRowIndex, 9] = model.supplier_frameNO;
                sheet.Cells[beginRowIndex, 10] = State.supplierType_Names[model.supplier_type];
                sheet.Cells[beginRowIndex, 11] = State.supplierstatus[model.supplier_status];
                sheet.Cells[beginRowIndex,12] = model.contact_name;
                sheet.Cells[beginRowIndex,13] = model.contact_tel;
                sheet.Cells[beginRowIndex,14] = model.contact_mobile;
                sheet.Cells[beginRowIndex,15] = model.contact_email;
                sheet.Cells[beginRowIndex,16] = model.contact_fax;
                sheet.Cells[beginRowIndex,17] = model.contact_address;
                sheet.Cells[beginRowIndex,18] = model.service_content;
                sheet.Cells[beginRowIndex,19] = model.service_forshunya;
                sheet.Cells[beginRowIndex,20] = model.service_area;
                sheet.Cells[beginRowIndex,21] = model.service_workamount;
                sheet.Cells[beginRowIndex,22] = model.service_customization;
                sheet.Cells[beginRowIndex,23] = model.service_ohter;
                sheet.Cells[beginRowIndex,24] = model.business_paytime;
                sheet.Cells[beginRowIndex,25] = model.business_prepay;
                sheet.Cells[beginRowIndex,26] = model.evaluation_department;
                sheet.Cells[beginRowIndex,27] = model.evaluation_level;
                sheet.Cells[beginRowIndex,28] = model.evaluation_feedback;
                sheet.Cells[beginRowIndex,29] = model.evaluation_note;
                sheet.Cells[beginRowIndex,30] = model.account_name;
                sheet.Cells[beginRowIndex,31] = model.account_bank;
                sheet.Cells[beginRowIndex, 32] = model.account_number;

                beginRowIndex++;
            }

            workbook.Saved = true;
            string tmpFileName = "supplierInfo_" + DateTime.Now.Ticks.ToString() + ".xls";
            try
            {
                workbook.SaveAs(mapPath + "ExcelTemplate\\" + tmpFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                outExcel(mapPath + "ExcelTemplate\\" + tmpFileName, tmpFileName, response);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public static void outExcel(string pathandname, string filename,HttpResponse response)
        {
            FileStream fin = new FileStream(pathandname, FileMode.Open);
            response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
            response.AddHeader("Connection", "Close");
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application/octet-stream";
            //response.ContentType = "application/vnd.ms-excel";
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
            response.Clear();
            response.End();
        }

        public static void SaveFile(string fn, string body)
        {
            using (StreamWriter savefile = new StreamWriter(fn, false,System.Text.Encoding.Default))
            {
                savefile.WriteLine(body);
                savefile.Close();
            }
        }

        public static void DeleteFile(string fn)
        {
            if (!Directory.Exists(fn))
            {
                FileInfo finfo = new FileInfo(fn);
                finfo.Delete();
                //File.Delete(fn);
            }
 
        }

        public static string SavePage(string PageFile,string ServerPath)
        {
            int bytecount = 1024;
            string filename="MediaPrint"+Guid.NewGuid().ToString()+".html";
            string filePath = ESP.Purchase.Common.ServiceURL.UpFilePath +"upFile\\"+ filename;
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");

            HttpWebRequest hwq = (HttpWebRequest)WebRequest.Create(hostUrl + "Purchase/Requisition/Print/MediaPrint.aspx?OrderID=" + PageFile + "&InternalPassword=f67u7b6i8asdf"); 
            HttpWebResponse hwr = (HttpWebResponse)hwq.GetResponse();
            byte[] bytes = new byte[bytecount];
            Stream stream = hwr.GetResponseStream();

            int read = stream.Read(bytes, 0, bytecount);
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            while (read > 0)
            {
                fs.Write(bytes, 0, read);
                read = stream.Read(bytes, 0, bytecount);
            }
            fs.Close();
            hwr.Close();
            stream.Close();
            return "upFile\\" + filename;
        }
    }
}
