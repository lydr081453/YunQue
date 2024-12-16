using System;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace ESP.Salary.Utility
{
    /**/
    /// <summary>
    /// ExcelHandle ��ժҪ˵����
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
        /// ������
        /// </summary>
        public Excel._Workbook CurBook = null;

        /**/
        /// <summary>
        /// ������
        /// </summary>
        public Excel._Worksheet CurSheet = null;

        //private object mValue = System.Reflection.Missing.Value;

        /**/
        /// <summary>
        /// ���캯��
        /// </summary>
        public ExcelHandle()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //    

            this.dtBefore = System.DateTime.Now;

            CurExcel = new Excel.Application();

            this.dtAfter = System.DateTime.Now;

            this.timestamp = System.DateTime.Now.ToShortDateString().Replace("-", "") + System.DateTime.Now.ToShortTimeString().Replace(":", "") + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

        }

        /**/
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="strFilePath">���ص�Excel�ļ���</param>
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
        /// �ͷ��ڴ�ռ�
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
                MessageWarning("���ͷ�Excel�ڴ�ռ�ʱ������һ������", ex);
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
        /// Excel�ļ���
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
        /// �Ƿ��Excel����
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
        /// ��ʱ���ַ�����Ϊ�����ļ�������
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
        /// ����Excel�ļ�
        /// </summary>
        public void Load()
        {
            object mValue = System.Reflection.Missing.Value;
            if (CurBook == null && this.filepath != null)
                CurBook = (Excel._Workbook)CurExcel.Workbooks.Open(this.filepath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
        }

        /// <summary>
        /// ��һ��excel
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
        /// ����Excel�ļ�
        /// </summary>
        /// <param name="strFilePath">Excel�ļ���</param>
        public void Load(string strFilePath)
        {
            object mValue = System.Reflection.Missing.Value;
            if (CurBook == null)
                CurBook = (Excel._Workbook)CurExcel.Workbooks.Open(strFilePath, mValue, false, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue, mValue);
        }

        /**/
        /// <summary>
        /// �½�������
        /// </summary>
        /// <param name="strWorkSheetName">����������</param>
        public void NewWorkSheet(string strWorkSheetName)
        {
            object mValue = System.Reflection.Missing.Value;
            CurSheet = (Excel._Worksheet)CurBook.Sheets.Add(CurBook.Sheets[1], mValue, mValue, mValue);
            CurSheet.Name = strWorkSheetName;
        }

        /**/
        /// <summary>
        /// ��ָ����Ԫ�����ָ����ֵ
        /// </summary>
        /// <param name="strCell">��Ԫ���硰A4��</param>
        /// <param name="objValue">�ı������ֵ�ֵ</param>
        public static void WriteCell(Excel._Worksheet worksheet, string strCell, object objValue)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Value2 = objValue;
        }

        /**/
        /// <summary>
        /// ��ָ��Range�в���ָ����ֵ
        /// </summary>
        /// <param name="strStartCell">Range�Ŀ�ʼ��Ԫ��</param>
        /// <param name="strEndCell">Range�Ľ�����Ԫ��</param>
        /// <param name="objValue">�ı������ֵ�ֵ</param>
        public void WriteRange(string strStartCell, string strEndCell, object objValue)
        {
            CurSheet.get_Range(strStartCell, strEndCell).Value2 = objValue;
        }


        /**/
        /// <summary>
        /// �ϲ���Ԫ�񣬲��ںϲ���ĵ�Ԫ���в���ָ����ֵ
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
        /// �ϲ���Ԫ�񣬲��ںϲ���ĵ�Ԫ���в���ָ����ֵ
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
        /// �ϲ���Ԫ�񣬲��ںϲ���ĵ�Ԫ���в���ָ����ֵ
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
        /// ��������Ԫ���в���һ��DataTable�е�ֵ
        /// </summary>
        /// <param name="strStartCell">��ʼ�ĵ�Ԫ��</param>
        /// <param name="dtData">�洢���ݵ�DataTable</param>
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
        /// ��������Ԫ���в���һ��DataTable������������
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="dtData">�洢���ݵ�DataTable</param>
        /// <param name="strLinkField">���ӵĵ�ַ�ֶ�</param>
        /// <param name="strTextField">���ӵ��ı��ֶ�</param>
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
                AddHyperLink(worksheet, NtoL(LtoN(GetCellLetter(strStartCell)) + dtData.Columns.IndexOf(strTextField)) + System.Convert.ToString(GetCellNumber(strStartCell) + i), dtData.Rows[i][strLinkField].ToString() + ".htm", "����鿴��ϸ", dtData.Rows[i][strTextField].ToString());

            arrData = null;
        }

        /**/
        /// <summary>
        /// Ϊ��Ԫ�����ù�ʽ
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        /// <param name="strFormula">��ʽ</param>
        public static void SetFormula(Excel._Worksheet worksheet, string strCell, string strFormula)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Formula = strFormula;
        }

        /**/
        /// <summary>
        /// ���õ�Ԫ����������������Ϊ����
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        public static void SetBold(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Font.Bold = true;
        }

        /**/
        /// <summary>
        /// �����������������Ϊ����
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        public static void SetBold(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            worksheet.get_Range(strStartCell, strEndCell).Font.Bold = true;
        }

        /**/
        /// <summary>
        /// ���õ�Ԫ������������������ɫ
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        /// <param name="clrColor">��ɫ</param>
        public static void SetColor(Excel._Worksheet worksheet, string strCell, System.Drawing.Color clrColor)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// �������������������ɫ
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        /// <param name="clrColor">��ɫ</param>
        public static void SetColor(Excel._Worksheet worksheet, string strStartCell, string strEndCell, System.Drawing.Color clrColor)
        {
            worksheet.get_Range(strStartCell, strEndCell).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }


        /**/
        /// <summary>
        /// ������������ı�����ɫ
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        /// <param name="clrColor">��ɫ</param>
        public static void SetBackGroundColor(Excel._Worksheet worksheet, string strStartCell, string strEndCell, System.Drawing.Color clrColor)
        {
            worksheet.get_Range(strStartCell, strEndCell).Interior.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// ������������ı߿���ɫ
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        /// <param name="clrColor">��ɫ</param>
        public static void SetBorderColor(Excel._Worksheet worksheet, string strStartCell, string strEndCell, System.Drawing.Color clrColor)
        {
            worksheet.get_Range(strStartCell, strEndCell).Borders.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// ������������ı߿���ɫ
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        /// <param name="clrColor">��ɫ</param>
        public static void SetBorderColor(Excel._Worksheet worksheet, string strStartCell, System.Drawing.Color clrColor)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strStartCell, mValue).Borders.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /**/
        /// <summary>
        /// ���õ�Ԫ�����������ı߿��������Ҷ�Ϊ��ɫ�����߿�
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
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
        /// ������������ı߿��������Ҷ�Ϊ��ɫ�����߿�
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
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
        /// ���õ�Ԫ�����������ˮƽ����
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        public static void SetHAlignLeft(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        }

        /**/
        /// <summary>
        /// ������������ˮƽ����
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        public static void SetHAlignLeft(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            Excel.Range range = worksheet.get_Range(strStartCell, strEndCell);
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        }

        /**/
        /// <summary>
        /// ���õ�Ԫ�����������ˮƽ����
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        public static void SetHAlignCenter(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.get_Range(strCell, mValue).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        /**/
        /// <summary>
        /// ������������ˮƽ����
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        public static void SetHAlignCenter(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            Excel.Range range = worksheet.get_Range(strStartCell, strEndCell);
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        /**/
        /// <summary>
        /// ���õ�Ԫ�����������ˮƽ����
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        public static void SetHAlignRight(Excel._Worksheet worksheet, string strCell)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        /**/
        /// <summary>
        /// ������������ˮƽ����
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        public static void SetHAlignRight(Excel._Worksheet worksheet, string strStartCell, string strEndCell)
        {
            worksheet.get_Range(strStartCell, strEndCell).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        /**/
        /// <summary>
        /// ���õ�Ԫ��������������ʾ��ʽ
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        /// <param name="strNF">��"#,##0.00"����ʾ��ʽ</param>
        public static void SetNumberFormat(Excel._Worksheet worksheet, string strCell, string strNF)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).NumberFormat = strNF;
        }

        /**/
        /// <summary>
        /// ���������������ʾ��ʽ
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        /// <param name="strNF">��"#,##0.00"����ʾ��ʽ</param>
        public static void SetNumberFormat(Excel._Worksheet worksheet, string strStartCell, string strEndCell, string strNF)
        {
            worksheet.get_Range(strStartCell, strEndCell).NumberFormat = strNF;
        }

        /**/
        /// <summary>
        /// ���õ�Ԫ�����������������С
        /// </summary>
        /// <param name="strCell">��Ԫ������������ʶ��</param>
        /// <param name="intFontSize"></param>
        public static void SetFontSize(Excel._Worksheet worksheet, string strCell, int intFontSize)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.get_Range(strCell, mValue).Font.Size = intFontSize.ToString();
        }

        /**/
        /// <summary>
        /// ������������������С
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ��</param>
        /// <param name="strEndCell">������Ԫ���ʶ��</param>
        /// <param name="intFontSize">�����С</param>
        public static void SetFontSize(Excel._Worksheet worksheet, string strStartCell, string strEndCell, int intFontSize)
        {
            worksheet.get_Range(strStartCell, strEndCell).Font.Size = intFontSize.ToString();
        }

        /**/
        /// <summary>
        /// �����п�
        /// </summary>
        /// <param name="strColID">�б�ʶ����A�����һ��</param>
        /// <param name="decWidth">���</param>
        public static void SetColumnWidth(Excel._Worksheet worksheet, string strColID, double dblWidth)
        {
            ((Excel.Range)worksheet.Columns.GetType().InvokeMember("Item", System.Reflection.BindingFlags.GetProperty, null, worksheet.Columns, new object[] { (strColID + ":" + strColID).ToString() })).ColumnWidth = dblWidth;
        }

        /**/
        /// <summary>
        /// Ϊ��Ԫ����ӳ�������
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        /// <param name="strAddress">���ӵ�ַ</param>
        /// <param name="strTip">��Ļ��ʾ</param>
        /// <param name="strText">�����ı�</param>
        public static void AddHyperLink(Excel._Worksheet worksheet, string strCell, string strAddress, string strTip, string strText)
        {
            object mValue = System.Reflection.Missing.Value;
            worksheet.Hyperlinks.Add(worksheet.get_Range(strCell, mValue), strAddress, mValue, strTip, strText);
        }

        /**/
        /// <summary>
        /// ��֪��ʼ�ĵ�Ԫ���ʶ����intR�С�intColumn�к�ĵ�Ԫ���ʶ
        /// </summary>
        /// <param name="strStartCell">��ʼ��Ԫ���ʶ</param>
        /// <param name="intR">����</param>
        /// <param name="intC">����</param>
        /// <returns>��Ԫ���ʶ�����</returns>
        public static string GetEndCell(string strStartCell, int intR, int intC)
        {

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");

            return NtoL(LtoN(regex.Match(strStartCell).Result("${vLetter}")) + intC) + System.Convert.ToString((System.Convert.ToInt32(regex.Match(strStartCell).Result("${vNumber}")) + intR));

        }

        /**/
        /// <summary>
        /// ��ȡ��Ԫ���ʶ���е���ĸ
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        /// <returns>��Ԫ���ʶ����Ӧ����ĸ</returns>
        public static string GetCellLetter(string strCell)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");
            return regex.Match(strCell).Result("${vLetter}");
        }

        /**/
        /// <summary>
        /// ��ȡ��Ԫ���ʶ���е�����
        /// </summary>
        /// <param name="strCell">��Ԫ���ʶ��</param>
        public static int GetCellNumber(string strCell)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(?<vLetter>[A-Z]+)(?<vNumber>\d+)");
            return System.Convert.ToInt32(regex.Match(strCell).Result("${vNumber}"));
        }


        /**/
        /// <summary>
        /// ���Ϊxls�ļ�
        /// </summary>
        /// <param name="strFilePath">�ļ�·��</param>
        public static void SaveAS(Excel._Workbook workbook, string strFilePath)
        {
            workbook.SaveCopyAs(strFilePath);
        }


        /**/
        /// <summary>
        /// ���Ϊxls�ļ�
        /// </summary>
        /// <param name="strFilePath">�ļ�·��</param>
        public static void Save(Excel._Workbook workbook)
        {
            workbook.Save();
        }

        /**/
        /// <summary>
        /// ���Ϊhtml�ļ�
        /// </summary>
        /// <param name="strFilePath">�ļ�·��</param>
        public static void SaveHtml(Excel._Workbook workbook, string strFilePath)
        {
            object mValue = System.Reflection.Missing.Value;
            workbook.SaveAs(strFilePath, Excel.XlFileFormat.xlHtml, mValue, mValue, mValue, mValue, Excel.XlSaveAsAccessMode.xlNoChange, mValue, mValue, mValue, mValue, mValue);
        }

        public void CreateHtmlFile()
        {

        }



        #region ��������

        /**/
        /// <summary>
        /// ����MessageBox��ʾ������Ϣ
        /// </summary>
        /// <param name="text">������Ϣ</param>
        private static void MessageWarning(string text)
        {
            System.Windows.Forms.MessageBox.Show(text, "Excel�������", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        /**/
        /// <summary>
        /// ����MessageBox��ʾ������Ϣ
        /// </summary>
        /// <param name="text">������Ϣ</param>
        /// <param name="ex">����������쳣</param>
        private static void MessageWarning(string text, System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(text + "\n\n������Ϣ��\n" + ex.Message + "\n��ջ���٣�" + ex.StackTrace + "\n������Դ��" + ex.Source, "Excel�������", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        /**/
        /// <summary>
        /// ��ĸת��Ϊ���֣�Excel��ͷ����A-1;AA-27
        /// </summary>
        /// <param name="strLetter">��ĸ</param>
        /// <returns>��ĸ��Ӧ������</returns>
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
        /// ����ת��Ϊ��ĸ��Excel��ͷ����1-A;27-AA
        /// </summary>
        /// <param name="intNumber">����</param>
        /// <returns>���ֶ�Ӧ����ĸ</returns>
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



        #endregion ��������



    }
}
