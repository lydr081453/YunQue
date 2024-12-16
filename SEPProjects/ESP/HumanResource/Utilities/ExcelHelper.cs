using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace ESP.HumanResource.Utilities
{
    public class ExcelHelper
    {
        /// <summary>
        /// Gets the excel connection STR.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetExcelConnectionStr(string fileName)
        {
            return "provider=Microsoft.Jet.OLEDB.4.0" +
                   ";data source=" + fileName +
                   ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";
        }
        /// <summary>
        /// Gets the text connection STR.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetTextConnectionStr(string fileName)
        {
            string strPath = Path.GetDirectoryName(fileName);
            return "Provider=Microsoft.Jet.OLEDB.4.0" +
                   ";Data Source=" + strPath +
                   ";Extended Properties=\"text;HDR=Yes;IMEX=1;\"";//FMT=Delimited;
        }
        /// <summary>
        /// Gets the DB from text file.
        /// </summary>
        /// <param name="fn">The fn.</param>
        /// <returns></returns>
        public static DataSet GetDBFromTextFile(string fn)
        {
            string fileName = Path.GetFileName(fn);
            string strSql = "SELECT * From " + fileName;
            string connStr = GetTextConnectionStr(fn);
            DataSet ds = new DataSet("DataSet1");
            System.Data.OleDb.OleDbDataAdapter oAdap = new OleDbDataAdapter(strSql, connStr);
            oAdap.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Gets the DB from excel.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="strSql">The STR SQL.</param>
        /// <returns></returns>
        public static DataSet GetDBFromExcel(string fileName, string strSql)
        {
            DataSet ds = new DataSet("DataSet1");
            string connStr = GetExcelConnectionStr(fileName);
            using (System.Data.OleDb.OleDbDataAdapter adap = new OleDbDataAdapter(strSql, connStr))
            {

                adap.Fill(ds);
            }

            return ds;
        }

        /// <summary>
        /// Gets the DB from excel.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static DataSet GetDBFromExcel(string fileName)
        {
            DataSet ds = new DataSet("DataSet1");
            int num = 1;
            while (true)
            {
                try
                {
                    string strSql = "SELECT * FROM [Sheet" + num.ToString() + "$]";
                    DataSet ds2 = GetDBFromExcel(fileName, strSql);
                    ds2.Tables[0].TableName = "Table" + num.ToString();
                    UpdateTableColumn(ds2.Tables[0]);
                    ds.Merge(ds2);
                    num++;
                }
                catch
                {
                    break;
                }
            }
            ClearNullData(ds);
            return ds;
        }
        /// <summary>
        /// Updates the table column.
        /// </summary>
        /// <param name="dt">The dt.</param>
        private static void UpdateTableColumn(DataTable dt)
        {
            if (dt == null)
                return;
            foreach (DataColumn col in dt.Columns)
                col.ColumnName = col.ColumnName.Trim();
        }
        /// <summary>
        /// Clears the null data.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
        private static DataSet ClearNullData(DataSet ds)
        {
            if (ds == null)
                return ds;
            foreach (DataTable dt in ds.Tables)
            {
                ArrayList alCol = new ArrayList();
                if (dt.Rows.Count == 0)
                    continue;
                ArrayList alRow = new ArrayList();
                foreach (DataRow row in dt.Rows)
                {
                    if (IsNull(row))
                        alRow.Add(row);
                }
                foreach (DataRow row in alRow)
                    dt.Rows.Remove(row);
            }
            return ds;
        }
        /// <summary>
        /// Determines whether the specified row is null.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>
        /// 	<c>true</c> if the specified row is null; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsNull(DataRow row)
        {
            object[] items = row.ItemArray;
            bool isNull = true;
            foreach (object item in items)
            {
                if (item != null && item.ToString().Trim() != "")
                {
                    isNull = false;
                    break;
                }
            }
            return isNull;
        }
        /// <summary>
        /// Gets the temp directory.
        /// </summary>
        /// <returns></returns>
        public static string GetTempDirectory()
        {
            string directory = ESP.Configuration.ConfigurationManager.SafeAppSettings["TempDirectory"];
            //string directory = System.Web.HttpContext.Current.Request.MapPath(ESP.Configuration.ConfigurationManager.SafeAppSettings["TempDirectory"]);
            if (!System.IO.Directory.Exists(directory))
                System.IO.Directory.CreateDirectory(directory);
            return directory;
        }
        /// <summary>
        /// Gets the template directory.
        /// </summary>
        /// <returns></returns>
        public static string GetTemplateDirectory()
        {
            string directory = ESP.Configuration.ConfigurationManager.SafeAppSettings["TemplateDirectory"];
            //string directory = System.Web.HttpContext.Current.Request.MapPath(ESP.Configuration.ConfigurationManager.SafeAppSettings["TemplateDirectory"]);
            return directory;
        }
        /// <summary>
        /// Gets the name of the temp file.
        /// </summary>
        /// <param name="extName">Name of the ext.</param>
        /// <returns></returns>
        public static string GetTempFileName(string extName)
        {
            return GetTempDirectory() + @"\" + Math.Abs(System.Guid.NewGuid().ToString().GetHashCode()).ToString() + extName;
        }
        /// <summary>
        /// Gets the name of the temp file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="extName">Name of the ext.</param>
        /// <returns></returns>
        public static string GetTempFileName(string directory, string extName)
        {
            return directory + @"\" + System.Guid.NewGuid().ToString() + "." + extName;
        }

        /// <summary>
        /// Saves the temp file.
        /// </summary>
        /// <param name="fileUp">The file up.</param>
        /// <returns></returns>
        public static string SaveTempFile(string strDirectory, System.Web.UI.HtmlControls.HtmlInputFile fileUp)
        {
            string extName = Path.GetExtension(fileUp.PostedFile.FileName);
            string tempFile = GetTempFileName(strDirectory, extName);
            fileUp.PostedFile.SaveAs(tempFile);
            return tempFile;
        }

        /// <summary>
        /// Gets the temp file DB.
        /// </summary>
        /// <param name="fileUp">The file up.</param>
        /// <param name="strDirectory">The STR directory.</param>
        /// <returns></returns>
        public static DataSet GetTempFileDB(string strDirectory, System.Web.UI.HtmlControls.HtmlInputFile fileUp)
        {
            string fn = SaveTempFile(strDirectory, fileUp);

            DataSet ds = null;
            if (IsTextFile(fn))
            {
                ds = new DataSet("DataSet1");
                DataTable dt = CSVToDataTable(fn);
                dt.Rows.RemoveAt(0);
                UpdateTableColumn(dt);
                ds.Tables.Add(dt);
            }
            else
                ds = GetDBFromExcel(fn);

            if (File.Exists(fn))
            {
                File.Delete(fn);
            }

            return ds;
        }
        /// <summary>
        /// Determines whether [is text file] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// 	<c>true</c> if [is text file] [the specified file name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsTextFile(string fileName)
        {
            string extName = System.IO.Path.GetExtension(fileName).ToLower();
            if (extName == ".csv" || extName == ".txt")
                return true;
            return false;
        }

        /// <summary>
        /// Datas the table to CSV.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="title">The title.</param>
        /// <param name="ld">The ld.</param>
        /// <param name="isShowDBColName">if set to <c>true</c> [is show DB col name].</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static string DataTableToCSV(DataTable dt, string title, ListDictionary ld, bool isShowDBColName, System.Web.UI.Page page)
        {
            string path = page.MapPath(System.Web.HttpRuntime.AppDomainAppVirtualPath) + "\\Temp\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string fileName = System.Guid.NewGuid().ToString() + ".csv";
            FileStream oStream = File.Create(path + fileName);
            StreamWriter oWriter = new StreamWriter(oStream, Encoding.Default);
            oWriter.Write(title + ",");
            oWriter.Write(oWriter.NewLine);
            #region 以下代码为输出列头所用
            //if (ld != null)
            //{
            //    foreach (string name in ld.Values)
            //        oWriter.Write(name.Trim() + ",");
            //    oWriter.Write(oWriter.NewLine);
            //}
            #endregion
            if (dt != null)
            {
                if (isShowDBColName)
                {
                    foreach (string col in ld.Keys)
                        oWriter.Write(col + ",");
                    oWriter.Write(oWriter.NewLine);
                }
                foreach (DataRow row in dt.Rows)
                {
                    foreach (string col in ld.Keys)
                    {
                        //string str = row[col].ToString().Trim().Replace(',', '.').Replace('#', ',') ;
                        string str = row[col].ToString().Trim().Replace(',', '，').Replace("\r", "").Replace("\n", "");
                        oWriter.Write(str + ",");
                    }
                    oWriter.Write(oWriter.NewLine);
                }
            }
            oWriter.Flush();
            oWriter.Close();
            oStream.Close();
            return path + fileName;
        }

        #region
        /// <summary>
        /// CSVs to data table.
        /// </summary>
        /// <param name="fn">The fn.</param>
        /// <returns></returns>
        public static DataTable CSVToDataTable(string fn)
        {
            FileStream stream = File.Open(fn, FileMode.Open);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
            string line = "";
            DataTable dt = new DataTable();

            while ((line = reader.ReadLine()) != null)
            {
                if (line.IndexOf('"') > 0)
                {
                    //line = ReplaceMark(line);
                    string[] values = line.Split(new char[] { '"' });
                    ArrayList arr = new ArrayList();
                    //arr.ad
                    string strTmp = string.Empty;
                    bool flag = false;
                    if (line.IndexOf('"') == 1)
                        flag = true;
                    for (int i = 0; i < values.Length; i++)
                    {
                        //strTmp = line.Substring(0, line.IndexOf('"'));
                        //line = line.Substring(line.IndexOf('"'), (line.Length - line.IndexOf('"')));
                        if (values[i] != "")
                        {
                            if (!flag)
                            {
                                string[] tmplist = values[i].Split(new char[] { ';' });
                                for (int j = 0; j < tmplist.Length; j++)
                                {
                                    if (tmplist[j] != "")
                                    {
                                        arr.Add(tmplist[j]);
                                    }
                                }
                                flag = true;
                            }
                            else
                            {
                                arr.Add(values[i]);
                                flag = false;
                            }
                        }
                    }

                    int count = dt.Columns.Count;
                    int addNum = arr.Count - count;
                    if (addNum > 0)
                    {
                        //AddColumn(dt, addNum);
                        AddColumn(dt, addNum);
                    }

                    DataRow row = dt.NewRow();
                    for (int num = 0; num < arr.Count; num++)
                        row[num] = arr[num];
                    dt.Rows.Add(row);
                }
                else
                {
                    //line = ReplaceMark(line);
                    string[] values = line.Split(new char[] { ',' });

                    int count = dt.Columns.Count;
                    int addNum = values.Length - count;
                    if (addNum > 0)
                    {
                        //AddColumn(dt, addNum);
                        AddColumn(dt, values);
                    }

                    DataRow row = dt.NewRow();
                    for (int num = 0; num < values.Length; num++)
                        row[num] = values[num];
                    dt.Rows.Add(row);
                }
            }
            reader.Close();
            stream.Close();

            return dt;
        }

        private static string ReplaceMark(string line)
        {
            int begin = line.IndexOf('"');
            int end = line.IndexOf('"', begin + 1);

            if (end > begin)
            {
                string old = line.Substring(begin, end - begin + 1);
                string newLine = old.Replace(",", ".");
                line = line.Replace(old, newLine);
                line = line.Replace("\"", "");
            }
            return line;
        }

        /// <summary>
        /// Adds the column.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="addNum">The add num.</param>
        public static void AddColumn(DataTable dt, int addNum)
        {
            int count = dt.Columns.Count;
            for (int num = count - 1; num < count + addNum; num++)
            {
                int colIndex = num + 1;
                DataColumn col = new DataColumn(colIndex.ToString(), typeof(string));
                dt.Columns.Add(col);
            }
        }

        /// <summary>
        /// Adds the column.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="columns">The columns.</param>
        public static void AddColumn(DataTable dt, string[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                DataColumn dc = new DataColumn(columns[i].ToString(), typeof(string));
                dt.Columns.Add(dc);
            }
        }
        #endregion


        #region chencm test 2007-8-28
        public static DataTable CSVToDataTable_Data(string fn)
        {
            FileStream stream = File.Open(fn, FileMode.Open);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8"));
            string line = "";
            DataTable dt = new DataTable("sadf");

            while ((line = reader.ReadLine()) != null)
            {

                //string strValue = string.Empty;
                //string sValue = string.Empty;
                //for (int i = 0; i < line.Length; i++)
                //{
                //    if (line.Substring(i, 1).Trim() != string.Empty)
                //    {
                //        sValue = string.Concat(sValue, line.Substring(i, 1).Trim());
                //    }
                //    else
                //    {
                //        if (sValue != string.Empty)
                //        {
                //            if (strValue == string.Empty)
                //            {
                //                strValue = sValue;
                //            }
                //            else
                //            {
                //                strValue = string.Concat(strValue, ",", sValue);
                //            }
                //        }
                //        sValue = string.Empty;
                //    }
                //}

                //if (strValue != string.Empty)
                //{
                //    string[] values = strValue.Split(new char[] { ',' });

                //    int count = dt.Columns.Count;
                //    int addNum = values.Length - count;
                //    if (count == 0)
                //    {
                //        strValue = string.Concat("CHyperionName,PHyperionName,pd, ", strValue);
                //        values = strValue.Split(new char[] { ',' });
                //        AddColumn(dt, values);
                //    }
                //    else
                //    {
                //        if (addNum < 0)
                //        {
                //            strValue = string.Concat(" ,,, ", strValue);
                //            values = strValue.Split(new char[] { ',' });
                //        }
                //    }

                //    DataRow row = dt.NewRow();
                //    for (int num = 0; num < values.Length; num++)
                //    {
                //        row[num] = values[num];
                //    }
                //    dt.Rows.Add(row);
                //}

                string[] values_list = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (values_list.Length > 0)
                {
                    int count = dt.Columns.Count;
                    int addNum = values_list.Length - count;
                    if (count == 0)
                    {
                        string strValue = "CHyperionName,PHyperionName,pd ";
                        foreach (string str in values_list)
                        {
                            strValue = string.Concat(strValue, ",", str);
                        }
                        values_list = null;
                        values_list = strValue.Split(new char[] { ',' });
                        AddColumn(dt, values_list);
                    }
                    else
                    {
                        if (addNum < 0)
                        {
                            string strValue = " ,, ";
                            foreach (string str in values_list)
                            {
                                strValue = string.Concat(strValue, ",", str);
                            }
                            values_list = null;
                            values_list = strValue.Split(new char[] { ',' });
                        }
                    }

                    DataRow row = dt.NewRow();
                    for (int num = 0; num < values_list.Length; num++)
                    {
                        row[num] = values_list[num];
                    }
                    dt.Rows.Add(row);
                }

            }
            reader.Close();
            stream.Close();

            return dt;
        }

        public static DataSet GetTempFileDB_Data(string strDirectory, System.Web.UI.HtmlControls.HtmlInputFile fileUp)
        {
            string fn = SaveTempFile(strDirectory, fileUp);

            DataSet ds = null;
            if (IsTextFile(fn))
            {
                ds = new DataSet("DataSet1");
                DataTable dt = CSVToDataTable_Data(fn);
                dt.Rows.RemoveAt(0);
                UpdateTableColumn(dt);
                ds.Tables.Add(dt);
            }
            else
                ds = GetDBFromExcel(fn);

            return ds;
        }

        public static DataSet GetTempFileDB_Data2(string strDirectory, string sSeaverParth)
        {
            string fn = sSeaverParth;// SaveTempFile(strDirectory, fileUp);

            DataSet ds = null;
            if (IsTextFile(fn))
            {
                ds = new DataSet("DataSet1");
                DataTable dt = CSVToDataTable_Data(fn);
                dt.Rows.RemoveAt(0);
                UpdateTableColumn(dt);
                ds.Tables.Add(dt);
            }
            else
                ds = GetDBFromExcel(fn);

            return ds;
        }

        #endregion
    }
}
