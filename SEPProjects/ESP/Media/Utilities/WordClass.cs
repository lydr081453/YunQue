using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;

namespace ESP.Media.Access.Utilities
{
    public class WordClass : IDisposable
    {
        #region 字段
        private _Application m_WordApp = null;
        private _Document m_Document = null;
        private object missing = System.Reflection.Missing.Value;
        #endregion
        #region 构造函数与析构函数

        public WordClass()
        {
            //m_WordApp = new ApplicationClass();
            m_WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            Object myNothing = System.Reflection.Missing.Value;

            m_Document = m_WordApp.Documents.Add(ref myNothing, ref myNothing, ref myNothing, ref myNothing);
        }

        ~WordClass()
        {
            try
            {
                if (m_WordApp != null)
                    m_WordApp.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        #endregion

        #region 属性
        public _Document Document
        {
            get
            {
                return m_Document;
            }
        }
        public _Application WordApplication
        {
            get
            {
                return m_WordApp;
            }
        }
        public int WordCount
        {
            get
            {
                if (m_Document != null)
                {
                    Range rng = m_Document.Content;
                    rng.Select();
                    return m_Document.Characters.Count;
                }
                else
                    return -1;
            }
        }
        public object Missing
        {
            get
            {
                return missing;
            }
        }
        #endregion
        #region 基本任务
        #region CreateDocument
        public void CreateDocument(string template)
        {
            object obj_template = template;
            if (template.Length <= 0) obj_template = missing;
            m_Document = m_WordApp.Documents.Add(ref obj_template, ref missing, ref missing, ref missing);
        }
        public void CreateDocument()
        {
            this.CreateDocument("");
        }
        #endregion
        #region OpenDocument
        public void OpenDocument(string fileName, bool readOnly)
        {
            object obj_FileName = fileName;
            object obj_ReadOnly = readOnly;
            m_Document = m_WordApp.Documents.Open(ref obj_FileName, ref missing, ref obj_ReadOnly, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing);
        }
        public void OpenDocument(string fileName)
        {
            this.OpenDocument(fileName, false);
        }
        #endregion
        #region Save & SaveAs
        public void Save()
        {
            if (m_Document != null)
                m_Document.Save();
        }
        public void SaveAs(string fileName)
        {
            object obj_FileName = fileName;
            if (m_Document != null)
            {
                m_Document.SaveAs(ref obj_FileName, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing);
            }
        }
        #endregion
        #region Close
        public void Close(bool isSaveChanges)
        {
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            if (isSaveChanges)
                saveChanges = WdSaveOptions.wdSaveChanges;
            if (m_Document != null)
                m_Document.Close(ref saveChanges, ref missing, ref missing);
        }
        #endregion
        #region 添加数据
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="picName"></param>
        public void AddPicture(string picName)
        {
            if (m_WordApp != null)
                m_WordApp.Selection.InlineShapes.AddPicture(picName, ref missing, ref missing, ref missing);
        }
        /// <summary>
        /// 插入页眉
        /// </summary>
        /// <param name="text"></param>
        /// <param name="align"></param>
        public void SetHeader(string text, WdParagraphAlignment align)
        {
            this.m_WordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
            this.m_WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
            this.m_WordApp.ActiveWindow.ActivePane.Selection.InsertAfter(text); //插入文本
            this.m_WordApp.Selection.ParagraphFormat.Alignment = align;  //设置对齐方式
            this.m_WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument; // 跳出页眉设置
        }
        /// <summary>
        /// 插入页脚
        /// </summary>
        /// <param name="text"></param>
        /// <param name="align"></param>
        public void SetFooter(string text, WdParagraphAlignment align)
        {
            this.m_WordApp.ActiveWindow.View.Type = WdViewType.wdOutlineView;
            this.m_WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryFooter;
            this.m_WordApp.ActiveWindow.ActivePane.Selection.InsertAfter(text); //插入文本
            this.m_WordApp.Selection.ParagraphFormat.Alignment = align;  //设置对齐方式
            this.m_WordApp.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument; // 跳出页眉设置
        }
        #endregion
        #region Print
        public void PrintOut()
        {
            object copies = "1";
            object pages = "";
            object range = WdPrintOutRange.wdPrintAllDocument;
            object items = WdPrintOutItem.wdPrintDocumentContent;
            object pageType = WdPrintOutPages.wdPrintAllPages;
            object oTrue = true;
            object oFalse = false;
            this.m_Document.PrintOut(
                ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
                ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
                ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);
        }
        public void PrintPreview()
        {
            if (m_Document != null)
                m_Document.PrintPreview();
        }
        #endregion
        public void Paste()
        {
            try
            {
                if (m_Document != null)
                {
                    m_Document.ActiveWindow.Selection.Paste();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
        #endregion
        #region 文档中的文本和对象
        public void AppendText(string text)
        {
            Selection currentSelection = this.m_WordApp.Selection;
            // Store the user's current Overtype selection
            bool userOvertype = this.m_WordApp.Options.Overtype;
            // Make sure Overtype is turned off.
            if (this.m_WordApp.Options.Overtype)
            {
                this.m_WordApp.Options.Overtype = false;
            }
            // Test to see if selection is an insertion point.
            if (currentSelection.Type == WdSelectionType.wdSelectionIP)
            {
                currentSelection.TypeText(text);
                currentSelection.TypeParagraph();
            }
            else
                if (currentSelection.Type == WdSelectionType.wdSelectionNormal)
                {
                    // Move to start of selection.
                    if (this.m_WordApp.Options.ReplaceSelection)
                    {
                        object direction = WdCollapseDirection.wdCollapseStart;
                        currentSelection.Collapse(ref direction);
                    }
                    currentSelection.TypeText(text);
                    currentSelection.TypeParagraph();
                }
                else
                {
                    // Do nothing.
                }
            // Restore the user's Overtype selection
            this.m_WordApp.Options.Overtype = userOvertype;
        }

        public void AppendTextNoParagraph(string text)
        {
            Selection currentSelection = this.m_WordApp.Selection;
            // Store the user's current Overtype selection
            bool userOvertype = this.m_WordApp.Options.Overtype;
            // Make sure Overtype is turned off.
            if (this.m_WordApp.Options.Overtype)
            {
                this.m_WordApp.Options.Overtype = false;
            }
            // Test to see if selection is an insertion point.
            if (currentSelection.Type == WdSelectionType.wdSelectionIP)
            {
                currentSelection.TypeText(text);
                //currentSelection.TypeParagraph();
            }
            else
                if (currentSelection.Type == WdSelectionType.wdSelectionNormal)
                {
                    // Move to start of selection.
                    if (this.m_WordApp.Options.ReplaceSelection)
                    {
                        object direction = WdCollapseDirection.wdCollapseStart;
                        currentSelection.Collapse(ref direction);
                    }
                    currentSelection.TypeText(text);
                }
                else
                {
                    // Do nothing.
                }
            // Restore the user's Overtype selection
            this.m_WordApp.Options.Overtype = userOvertype;
        }
        #endregion
        #region 搜索和替换文档中的文本
        public void Replace(string oriText, string replaceText)
        {
            object replaceAll = WdReplace.wdReplaceAll;
            this.m_WordApp.Selection.Find.ClearFormatting();
            this.m_WordApp.Selection.Find.Text = oriText;
            this.m_WordApp.Selection.Find.Replacement.ClearFormatting();
            this.m_WordApp.Selection.Find.Replacement.Text = replaceText;
            this.m_WordApp.Selection.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing);
        }

        #endregion
        #region 文档中的Word表格
        /// <summary>
        /// 向文档中插入表格
        /// </summary>
        /// <param name="startIndex">开始位置</param>
        /// <param name="endIndex">结束位置</param>
        /// <param name="rowCount">行数</param>
        /// <param name="columnCount">列数</param>
        /// <returns></returns>
        public Table AppendTable(int startIndex, int endIndex, int rowCount, int columnCount)
        {
            object start = startIndex;
            object end = endIndex;
            Range tableLocation = this.m_Document.Range(ref start, ref end);
            return this.m_Document.Tables.Add(tableLocation, rowCount, columnCount, ref missing, ref missing);
        }

        /// <summary>
        /// 向文档中插入表格
        /// </summary>
        /// <param name="rowCount">行数</param>
        /// <param name="columnCount">列数</param>
        /// <returns></returns>
        public Table AppendTable(int rowCount, int columnCount)
        {
            object _DefaultTableBehavior = WdDefaultTableBehavior.wdWord9TableBehavior;
            object _AutoFitBehavior = WdAutoFitBehavior.wdAutoFitFixed;

            return this.m_Document.Tables.Add(this.m_WordApp.Selection.Range, rowCount, columnCount, ref _DefaultTableBehavior, ref _AutoFitBehavior);
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Row AppendRow(Table table)
        {
            object row = table.Rows[1];
            return table.Rows.Add(ref row);
        }
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Column AppendColumn(Table table)
        {
            object column = table.Columns[1];
            return table.Columns.Add(ref column);
        }
        /// <summary>
        /// 设置单元格的文本和对齐方式
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="text">文本</param>
        /// <param name="align">对齐方式</param>
        public void SetCellText(Cell cell, string text, WdParagraphAlignment align)
        {
            cell.Range.Text = text;
            cell.Range.ParagraphFormat.Alignment = align;
        }
        #endregion
        #region IDisposable 成员
        public void Dispose()
        {
            try
            {
                if (m_WordApp != null)
                    m_WordApp.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        #endregion
    }

    public class XConvert
    {
        public static string ToRMB(Double e)
        {
            return ToRMB(System.Convert.ToDecimal(e));
        }

        public static string ToRMB(Decimal e)
        {
            string eString;//数字的格式化字符串
            string eNum;//单数字
            int eLen;//格式化字符串长度

            System.Text.StringBuilder rmb = new System.Text.StringBuilder();//人民币大写
            string yuan;//圆
            bool seriesZero;//连续0标志
            bool minus = false;//负数标志

            if (e == 0m)
            {
                return "零圆整";
            }
            if (e < 0m)
            {
                minus = true;
                e = System.Math.Abs(e);
            }
            if (e > 999999999999.99m)
            {
                throw new Exception("超过最大范围");
            }

            eString = e.ToString("0.00");
            eLen = eString.Length;
            yuan = (eString.Substring(0, 1) == "0" ? "" : "圆");

            eNum = eString.Substring(eLen - 1, 1);//分位
            if (eNum == "0")
            {
                rmb.Append("整");
                seriesZero = true;
            }
            else
            {
                rmb.Append(stringNum(eNum) + "分");
                seriesZero = false;
            }

            eNum = eString.Substring(eLen - 2, 1);//角位
            if (eNum == "0")
            {
                if (!seriesZero)
                {
                    if (!(eLen == 4 && eString.Substring(0, 1) == "0"))
                    {
                        rmb.Insert(0, "零");
                    }
                }
            }
            else
            {
                rmb.Insert(0, stringNum(eNum) + "角");
                seriesZero = false;
            }

            if (eLen <= 7)
            {
                rmb.Insert(0, stringNum4(eString.Substring(0, eLen - 3)) + yuan);
            }
            else if (eLen <= 11)
            {
                rmb.Insert(0, stringNum4(eString.Substring(eLen - 7, 4)) + yuan);
                rmb.Insert(0, stringNum4(eString.Substring(0, eLen - 7)) + "万");
            }
            else if (eLen <= 15)
            {
                rmb.Insert(0, stringNum4(eString.Substring(eLen - 7, 4)) + yuan);
                rmb.Insert(0, stringNum4(eString.Substring(eLen - 11, 4)) + (eString.Substring(eLen - 11, 4) == "0000" ? "" : "万"));
                rmb.Insert(0, stringNum4(eString.Substring(0, eLen - 11)) + "亿");
            }

            if (minus) rmb.Insert(0, "负");

            return rmb.ToString();
        }

        private static string stringNum4(string eNum4)
        {
            string eNum;
            bool seriesZero = false;
            System.Text.StringBuilder rmb4 = new System.Text.StringBuilder();
            int eLen = eNum4.Length;

            eNum = eNum4.Substring(eLen - 1, 1);//个位
            if (eNum == "0")
            {
                seriesZero = true;
            }
            else
            {
                rmb4.Append(stringNum(eNum));
            }

            if (eLen >= 2)//十位
            {
                eNum = eNum4.Substring(eLen - 2, 1);
                if (eNum == "0")
                {
                    if (!seriesZero)
                    {
                        rmb4.Insert(0, "零");
                        seriesZero = true;
                    }
                }
                else
                {
                    rmb4.Insert(0, stringNum(eNum) + "拾");
                    seriesZero = false;
                }
            }

            if (eLen >= 3)//百位
            {
                eNum = eNum4.Substring(eLen - 3, 1);
                if (eNum == "0")
                {
                    if (!seriesZero)
                    {
                        rmb4.Insert(0, "零");
                        seriesZero = true;
                    }
                }
                else
                {
                    rmb4.Insert(0, stringNum(eNum) + "佰");
                    seriesZero = false;
                }
            }

            if (eLen == 4)//千位
            {
                eNum = eNum4.Substring(0, 1);
                if (eNum == "0")
                {
                    if (!seriesZero)
                    {
                        rmb4.Insert(0, "零");
                        seriesZero = true;
                    }
                }
                else
                {
                    rmb4.Insert(0, stringNum(eNum) + "仟");
                    seriesZero = false;
                }
            }

            return rmb4.ToString();
        }

        private static string stringNum(string eNum)
        {
            switch (eNum)
            {
                case "1":
                    return "壹";
                case "2":
                    return "贰";
                case "3":
                    return "叁";
                case "4":
                    return "肆";
                case "5":
                    return "伍";
                case "6":
                    return "陆";
                case "7":
                    return "柒";
                case "8":
                    return "捌";
                case "9":
                    return "玖";
                default:
                    return "";
            }
        }
    }
}