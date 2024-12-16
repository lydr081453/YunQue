using System;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace ESP.Salary.Utility
{
    /**/
    /// <summary>
    /// ExcelHandle 的摘要说明。
    /// </summary>
    public class ExcelHandle
    {

        /**/
        /// <summary>
        /// Excel
        /// </summary>
        public Excel.Application CurExcel = null;

        /**/
        /// <summary>
        /// 工作簿
        /// </summary>
        public Excel._Workbook CurBook = null;

        /**/
        /// <summary>
        /// 工作表
        /// </summary>
        public Excel._Worksheet CurSheet = null;

        //private object mValue = System.Reflection.Missing.Value;

        /**/
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExcelHandle()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //    

            this.dtBefore = System.DateTime.Now;

            CurExcel = new Excel.Application();

            this.dtAfter = System.DateTime.Now;

            this.timestamp = System.DateTime.Now.ToShortDateString().Replace("-", "") + System.DateTime.Now.ToShortTimeString().Replace(":", "") + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

        }

        /**/
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strFilePath">加载的Excel文件名</param>
        public ExcelHandle(string strFilePath)
        {

            object mValue = System.Reflection.Missing.Value;
            this.dtBefore = System.DateTime.Now;

            CurExcel = new Excel.Application();

            this.dtAfter = System.DateTime.Now;

            CurBook = (Excel._Workbook)CurExcel.Workbooks.Open(strFilePath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);

            this.timestamp = System.DateTime.Now.ToShortDateString().Replace("-", "") + System.DateTime.Now.ToShortTimeString().Replace(":", "") + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

        }

        /**/
        /// <summary>
        /// 释放内存空间
        /// </summary>
        public void Dispose()
        {
            object mValue = System.Reflection.Missing.Value;
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurSheet);
                CurSheet = null;

                CurBook.Close(false, mValue, mValue);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurBook);
                CurBook = null;

                CurExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurExcel);
                CurExcel = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
            catch (System.Exception ex)
            {
                MessageWarning("在释放Excel内存空间时发生了一个错误：", ex);
            }
            finally
            {
                try
                {
                    foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcessesByName("Excel"))
                        if (pro.StartTime > this.dtBefore && pro.StartTime < this.dtAfter)
                            pro.Kill();
                }
                catch { }
            }
            System.GC.SuppressFinalize(this);
        }


        private string filepath;
        private string timestamp;
        private System.DateTime dtBefore;
        private System.DateTime dtAfter;


        /**/
        /// <summary>
        /// Excel文件名
        /// </summary>
        public string FilePath
        {
            get
            {
                return this.filepath;
            }
            set
            {
                this.filepath = value;
            }
        }

        /**/
        /// <summary>
        /// 是否打开Excel界面
        /// </summary>
        public bool Visible
        {
            set
            {
                CurExcel.Visible = value;
            }
        }

        /**/
        /// <summary>
        /// 以时间字符串作为保存文件的名称
        /// </summary>
        public string TimeStamp
        {
            get
            {
                return this.timestamp;
            }
            set
            {
                this.timestamp = value;
            }
        }


        /**/
        /// <summary>
        /// 加载Excel文件
        /// </summary>
        public void Load()
        {
            object mValue = System.Reflection.Missing.Value;
            if (CurBook == null && this.filepath != null)
                CurBook = (Excel._Workbook)CurExcel.Workbooks.Open(this.filepath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
        }

        /// <summary>
        /// 打开一个excel
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Excel._Workbook open(string filename, Excel.Workbooks workbooks)
        {
            object mValue = System.Reflection.Missing.Value;
            return (Excel._Workbook)workbooks.Open(filename, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
        }

        /**/
        /// <summary>
        /// 加载Excel文件
        /// </summary>
        /// <param name="strFilePath">Excel文件名</param>
        public void Load(string strFilePath)
        {
            object mValue = System.Reflection.Missing.Value;
            if (CurBook == null)
                CurBook = (Excel._Workbook)CurExcel.Workbooks.Open(strFilePath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
        }

        /**/
        /// <summary>
        /// 新建工作表
        /// </summary>
        /// <param name="strWorkSheetName">工作表名称</param>
        public void NewWorkSheet(string strWorkSheetName)
        {
            object mValue = System.Reflection.Missing.Value;
            CurSheet = (Excel._Worksheet)CurBook.Sheets.Add(CurBook.Sheets[1], mValue, mValue, mValue);
            CurSheet.Name = strWorkSheetName;
        }

        /**/
        /// <summary>
        /// 在指定单元格插入指定的值
        /// </summary>
        /// <param name="strCell">单元格，如“A4”</param>
        /// <param name="objValue">文本、数字等值</param>
        public static void WriteCell(Excel._Worksheet worksheet, string strCell, object objValue)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Value2 = objValue;
        }

        /**/
        /// <summary>
        /// 在指定Range中插入指定的值
        /// </summary>
        /// <param name="strStartCell">Range的开始单元格</param>
        /// <param name="strEndCell">Range的结束单元格</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteRange(string strStartCell, string strEndCell, object objValue)
        {
            CurSheet.get_Range(strStartCell, strEndCell).Value2 = objValue;
        }


        /**/
        /// <summary>
        /// 合并单元格，并在合并后的单元格中插入指定的值
        /// </summary>
        /// <param name="strStartCell"></param>
        /// <param name="strEndCell"></param>
        /// <param name="objValue"></param>
        public static void WriteAfterMerge(Excel._Worksheet worksheet, string strStartCell, string strEndCell, object objValue)
        {
            object mValue = System.Reflection.Missing.Value;
            Excel.Range range = worksheet.get_Range(strStartCell, strEndCell);
            range.Merge(mValue);
            range = worksheet.get_Range(strStartCell, mValue);
            range.Value2 = objValue;
        }


        /**/
        /// <summary>
        /// 合并单元格，并在合并后的单元格中插入指定的值
        /// </summary>
        /// <param name="strStartCell"></param>
        /// <param name="strEndCell"></param>
        /// <param name="objValue"></param>
        public static void WriteRangeTitle(Excel._Worksheet worksheet, string strStartCell, string strEndCell, object objValue)
        {
            object mValue = System.Reflection.Missing.Value;
            Excel.Range range = worksheet.get_Range(strStartCell, strEndCell);
            range.AddComment(objValue);
        }

        /**/
        /// <summary>
        /// 合并单元格，并在合并后的单元格中插入指定的值
        /// </summary>
        /// <param name="strStartCell"></param>
        /// <param name="strEndCell"></param>
        /// <param name="objValue"></param>
        public static void WriteCellTitle(Excel._Worksheet worksheet, string strCell, object objValue)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).AddComment(objValue);
        }


        /**/
        /// <summary>
        /// 在连续单元格中插入一个DataTable中的值
        /// </summary>
        /// <param name="strStartCell">开始的单元格</param>
        /// <param name="dtData">存储数据的DataTable</param>
        public static void WriteTable(Excel._Worksheet worksheet, string strStartCell, System.Data.DataTable dtData)
        {
            object[,] arrData = new object[dtData.Rows.Count, dtData.Columns.Count];

            for (int i = 0; i < dtData.Rows.Count; i++)
                for (int j = 0; j < dtData.Columns.Count; j++)
                    arrData[i, j] = dtData.Rows[i][j];

            worksheet.get_Range(strStartCell, GetEndCell(strStartCell, dtData.Rows.Count - 1, dtData.Columns.Count - 1)).Value2 = arrData;

            arrData = null;
        }

        /**/
        /// <summary>
        /// 在连续单元格中插入一个DataTable并作超级链接
        /// </summary>
        /// <param name="strStartCell">起始单元格标识符</param>
        /// <param name="dtData">存储数据的DataTable</param>
        /// <param name="strLinkField">链接的地址字段</param>
        /// <param name="strTextField">链接的文本字段</param>
        public void WriteTableAndLink(Excel._Worksheet worksheet, string strStartCell, System.Data.DataTable dtData, string strLinkField, string strTextField)
        {
            object[,] arrData = new object[dtData.Rows.Count, dtData.Columns.Count - 1];

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                for (int j = 0; j < dtData.Columns.Count; j++)
                {
                    if (j > dtData.Columns.IndexOf(strLinkField))
                        arrData[i, j - 1] = dtData.Rows[i][j];
                    else if (j < dtData.Columns.IndexOf(strLinkField))
                        arrData[i, j] = dtData.Rows[i][j];
                }
            }

            CurSheet.get_Range(strStartCell, GetEndCell(strStartCell, dtData.Rows.Count - 1, dtData.Columns.Count - 2)).Value2 = arrData;

            for (int i = 0; i < dtData.Rows.Count; i++)
                AddHyperLink(worksheet, NtoL(LtoN(GetCellLetter(strStartCell)) + dtData.Columns.IndexOf(strTextField)) + System.Convert.ToString(GetCellNumber(strStartCell) + i), dtData.Rows[i][strLinkField].ToString() + ".htm", "点击查看详细", dtData.Rows[i][strTextField].ToString());

            arrData = null;
        }

        /**/
        /// <summary>
        /// 为单元格设置公式
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="strFormula">公式</param>
        public static void SetFormula(Excel._Worksheet worksheet, string strCell, string strFormula)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Formula = strFormula;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的字体为黑体
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public static void SetBold(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Font.Bold = true;
        }

        /**/
        /// <summary>
        /// 设置连续区域的字体为黑体
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public static void SetBold(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            worksheet.get_Range(strStartCell, strEndCell).Font.Bold = true;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的字体颜色
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="clrColor">颜色</param>
        public static void SetColor(Excel._Worksheet worksheet, string strCell, System.Drawing.Color clrColor)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// 设置连续区域的字体颜色
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="clrColor">颜色</param>
        public static void SetColor(Excel._Worksheet worksheet, string strStartCell, string strEndCell, System.Drawing.Color clrColor)
        {
            worksheet.get_Range(strStartCell, strEndCell).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }


        /**/
        /// <summary>
        /// 设置连续区域的背景颜色
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="clrColor">颜色</param>
        public static void SetBackGroundColor(Excel._Worksheet worksheet, string strStartCell, string strEndCell, System.Drawing.Color clrColor)
        {
            worksheet.get_Range(strStartCell, strEndCell).Interior.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// 设置连续区域的边框颜色
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="clrColor">颜色</param>
        public static void SetBorderColor(Excel._Worksheet worksheet, string strStartCell, string strEndCell, System.Drawing.Color clrColor)
        {
            worksheet.get_Range(strStartCell, strEndCell).Borders.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// 设置连续区域的边框颜色
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="clrColor">颜色</param>
        public static void SetBorderColor(Excel._Worksheet worksheet, string strStartCell, System.Drawing.Color clrColor)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strStartCell, mValue).Borders.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的边框：上下左右都为黑色连续边框
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public static void SetBorderAll(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            worksheet.get_Range(strCell, mValue).Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;


        }

        /**/
        /// <summary>
        /// 设置连续区域的边框：上下左右都为黑色连续边框
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public static void SetBorderAll(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

            if (worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlInsideHorizontal] != null)
            {
                worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            }

            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            worksheet.get_Range(strStartCell, strEndCell).Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域水平居左
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public static void SetHAlignLeft(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        }

        /**/
        /// <summary>
        /// 设置连续区域水平居左
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public static void SetHAlignLeft(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            Excel.Range range = worksheet.get_Range(strStartCell, strEndCell);
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域水平居左
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public static void SetHAlignCenter(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.get_Range(strCell, mValue).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        /**/
        /// <summary>
        /// 设置连续区域水平居中
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public static void SetHAlignCenter(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            Excel.Range range = worksheet.get_Range(strStartCell, strEndCell);
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域水平居右
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public static void SetHAlignRight(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        /**/
        /// <summary>
        /// 设置连续区域水平居右
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        public static void SetHAlignRight(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            worksheet.get_Range(strStartCell, strEndCell).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的显示格式
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="strNF">如"#,##0.00"的显示格式</param>
        public static void SetNumberFormat(Excel._Worksheet worksheet, string strCell, string strNF)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).NumberFormat = strNF;
        }

        /**/
        /// <summary>
        /// 设置连续区域的显示格式
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="strNF">如"#,##0.00"的显示格式</param>
        public static void SetNumberFormat(Excel._Worksheet worksheet, string strStartCell, string strEndCell, string strNF)
        {
            worksheet.get_Range(strStartCell, strEndCell).NumberFormat = strNF;
        }

        /**/
        /// <summary>
        /// 设置单元格或连续区域的字体大小
        /// </summary>
        /// <param name="strCell">单元格或连续区域标识符</param>
        /// <param name="intFontSize"></param>
        public static void SetFontSize(Excel._Worksheet worksheet, string strCell, int intFontSize)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Font.Size = intFontSize.ToString();
        }

        /**/
        /// <summary>
        /// 设置连续区域的字体大小
        /// </summary>
        /// <param name="strStartCell">开始单元格标识符</param>
        /// <param name="strEndCell">结束单元格标识符</param>
        /// <param name="intFontSize">字体大小</param>
        public static void SetFontSize(Excel._Worksheet worksheet, string strStartCell, string strEndCell, int intFontSize)
        {
            worksheet.get_Range(strStartCell, strEndCell).Font.Size = intFontSize.ToString();
        }

        /**/
        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="strColID">列标识，如A代表第一列</param>
        /// <param name="decWidth">宽度</param>
        public static void SetColumnWidth(Excel._Worksheet worksheet, string strColID, double dblWidth)
        {
            ((Excel.Range)worksheet.Columns.GetType().InvokeMember("Item", System.Reflection.BindingFlags.GetProperty, null, worksheet.Columns, new object[] { (strColID + ":" + strColID).ToString() })).ColumnWidth = dblWidth;
        }

        /**/
        /// <summary>
        /// 为单元格添加超级链接
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <param name="strAddress">链接地址</param>
        /// <param name="strTip">屏幕提示</param>
        /// <param name="strText">链接文本</param>
        public static void AddHyperLink(Excel._Worksheet worksheet, string strCell, string strAddress, string strTip, string strText)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.Hyperlinks.Add(worksheet.get_Range(strCell, mValue), strAddress, mValue, strTip, strText);
        }

        /**/
        /// <summary>
        /// 已知开始的单元格标识，求intR行、intColumn列后的单元格标识
        /// </summary>
        /// <param name="strStartCell">开始单元格标识</param>
        /// <param name="intR">行数</param>
        /// <param name="intC">列数</param>
        /// <returns>单元格标识符结果</returns>
        public static string GetEndCell(string strStartCell, int intR, int intC)
        {

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");

            return NtoL(LtoN(regex.Match(strStartCell).Result("${vLetter}")) + intC) + System.Convert.ToString((System.Convert.ToInt32(regex.Match(strStartCell).Result("${vNumber}")) + intR));

        }

        /**/
        /// <summary>
        /// 获取单元格标识符中的字母
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        /// <returns>单元格标识符对应的字母</returns>
        public static string GetCellLetter(string strCell)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");
            return regex.Match(strCell).Result("${vLetter}");
        }

        /**/
        /// <summary>
        /// 获取单元格标识符中的数字
        /// </summary>
        /// <param name="strCell">单元格标识符</param>
        public static int GetCellNumber(string strCell)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");
            return System.Convert.ToInt32(regex.Match(strCell).Result("${vNumber}"));
        }


        /**/
        /// <summary>
        /// 另存为xls文件
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public static void SaveAS(Excel._Workbook workbook, string strFilePath)
        {
            workbook.SaveCopyAs(strFilePath);
        }


        /**/
        /// <summary>
        /// 另存为xls文件
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public static void Save(Excel._Workbook workbook)
        {
            workbook.Save();
        }

        /**/
        /// <summary>
        /// 另存为html文件
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public static void SaveHtml(Excel._Workbook workbook, string strFilePath)
        {
            object mValue = System.Reflection.Missing.Value;
            workbook.SaveAs(strFilePath, Excel.XlFileFormat.xlHtml, mValue, mValue, mValue, mValue, Excel.XlSaveAsAccessMode.xlNoChange, mValue, mValue, mValue, mValue, mValue);
        }

        public void CreateHtmlFile()
        {

        }



        #region 辅助函数

        /**/
        /// <summary>
        /// 调用MessageBox显示警告信息
        /// </summary>
        /// <param name="text">警告信息</param>
        private static void MessageWarning(string text)
        {
            System.Windows.Forms.MessageBox.Show(text, "Excel操作组件", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        /**/
        /// <summary>
        /// 调用MessageBox显示警告信息
        /// </summary>
        /// <param name="text">警告信息</param>
        /// <param name="ex">产生警告的异常</param>
        private static void MessageWarning(string text, System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(text + "\n\n错误信息：\n" + ex.Message + "\n堆栈跟踪：" + ex.StackTrace + "\n错误来源：" + ex.Source, "Excel操作组件", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        /**/
        /// <summary>
        /// 字母转换为数字，Excel列头，如A-1;AA-27
        /// </summary>
        /// <param name="strLetter">字母</param>
        /// <returns>字母对应的数字</returns>
        public static int LtoN(string strLetter)
        {
            int intRtn = 0;

            string strLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (strLetter.Length == 2)
                intRtn += (strLetters.IndexOf(strLetter.Substring(0, 1)) + 1) * 26;

            intRtn += strLetters.IndexOf(strLetter.Substring(strLetter.Length - 1, 1)) + 1;

            return intRtn;

        }

        /**/
        /// <summary>
        /// 数字转换为字母，Excel列头，如1-A;27-AA
        /// </summary>
        /// <param name="intNumber">数字</param>
        /// <returns>数字对应的字母</returns>
        public static string NtoL(int intNumber)
        {
            if (intNumber == 26)
                return "Z";
            if (intNumber > 702)
                return String.Empty;

            if (intNumber == 702)
                return "ZZ";

            string strRtn = String.Empty;

            string strLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (intNumber > 26)
            {
                if (intNumber > 0 && intNumber % 26 == 0)
                {
                    strRtn = strLetters.Substring(intNumber / 26 - 2, 1);
                }
                else
                {
                    strRtn = strLetters.Substring(intNumber / 26 - 1, 1);
                }
            }

            if (intNumber>0 && intNumber != 26 && intNumber % 26 == 0)
            {
                strRtn += "Z";
            }
            else
            {
                strRtn += strLetters.Substring((intNumber % 26) - 1, 1);
            }

            return strRtn;
        }



        #endregion 辅助函数



    }
}
