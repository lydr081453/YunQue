using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using ESP.Media.Access;
using ESP.Media.Entity;

namespace ESP.Media.BusinessLogic
{
    public class ExcelExportManager
    {

        private static DataTable GetDt(Type type, string FunName, object[] param)
        {
            DataTable dt = null;
            try
            {
                MethodInfo mi = type.GetMethod(FunName);
                dt = (DataTable)(mi.Invoke(null, param));
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return dt;
        }
        



        /// <summary>
        /// 媒体签到表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filename"></param>
        /// <param name="errmsg"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string SaveSignExcel(DataTable dt, string filename,out string fname,out bool isdel, out string errmsg, int userid)
        {
            errmsg = "成功";
            fname = filename.Substring(filename.LastIndexOf('\\') + 1);
            string serverpath = filename.Substring(0, filename.LastIndexOf('\\'));



            if (dt == null || dt.Rows.Count == 0)
            {
                errmsg = "没有记者!";
                fname = "\\Sign.xls";
                isdel = false;
                return serverpath + fname;
            }


            if (!Directory.Exists(serverpath))
                Directory.CreateDirectory(serverpath);

            ExcelHandle excel = new ExcelHandle();
            excel.Load(serverpath + "\\Sign.xls");
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            try
            {
                string cell = "A4";
                string medianame = dt.Rows[0]["MediaName"].ToString().Trim();
                string oldcell = "C4";
                string fcell = string.Format("C{0}", dt.Rows.Count + 3);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cell = string.Format("A{0}", i + 4);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, ((int)(i + 1)).ToString());

                    cell = string.Format("B{0}", i + 4);
                    string cityname = dt.Rows[i]["CityName"] == DBNull.Value ? "未知" : dt.Rows[i]["CityName"].ToString();
                    ExcelHandle.WriteCell(excel.CurSheet, cell, cityname.Trim());

                    cell = string.Format("C{0}", i + 3);
                    if (dt.Rows[i]["MediaName"].ToString().Trim() != medianame)
                    {
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, oldcell, cell, medianame);
                        medianame = dt.Rows[i]["MediaName"].ToString().Trim();
                        oldcell = string.Format("C{0}", i + 4);
                    }

                    cell = string.Format("D{0}", i + 4);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterName"].ToString().Trim());

                    cell = string.Format("E{0}", i + 4);
                }
                ExcelHandle.WriteAfterMerge(excel.CurSheet, oldcell, fcell, medianame);
                ExcelHandle.SetHAlignCenter(excel.CurSheet, "A4", cell);
                ExcelHandle.SetBorderAll(excel.CurSheet, "A3", cell);

                excel.CurSheet.Name = DateTime.Now.ToString().Split(' ')[0];
                ExcelHandle.SaveAS(excel.CurBook, filename);
                excel.Dispose();
                isdel = true;
                return filename;
                
            }
            catch (Exception exp)
            {
                excel.Dispose();
                errmsg = exp.Message;


                fname = "\\Sign.xls";
                isdel = false;
                return serverpath + fname;
            }
        }



        /// <summary>
        /// 通联表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filename"></param>
        /// <param name="errmsg"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string SaveCommunicateExcel(DataTable dt,string filename,out string fname,out bool isdel, out string errmsg, int userid)
        {
            fname = filename.Substring(filename.LastIndexOf('\\') + 1);
            string serverpath = filename.Substring(0, filename.LastIndexOf('\\'));
            errmsg = "成功";
            if (dt == null || dt.Rows.Count == 0)
            {
                errmsg = "没有记者!";


                fname = "\\Contract.xls";
                isdel = false;
                return serverpath + fname;
            }

            if (!Directory.Exists(serverpath))
                Directory.CreateDirectory(serverpath);
            ExcelHandle excel = new ExcelHandle();
            excel.Load(serverpath + "\\Contract.xls");
            excel.CurSheet = (Excel._Worksheet)excel.CurBook.Sheets[1];
            try
            {
                string typeName = string.Empty;
                int rownum = 3;
                string cell = string.Empty;
                string medianame = string.Empty;
                int mcount = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (typeName != dt.Rows[i]["TypeName"].ToString().Trim())
                    {
                        typeName = dt.Rows[i]["TypeName"].ToString().Trim();
                        ExcelHandle.SetHAlignLeft(excel.CurSheet, "A" + rownum.ToString(), "K" + rownum.ToString());
                        ExcelHandle.SetBackGroundColor(excel.CurSheet, "A" + rownum.ToString(), "K" + rownum.ToString(), System.Drawing.Color.LightYellow);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, "A" + rownum.ToString(), "K" + rownum.ToString(), typeName);
                        //ExcelHandle.WriteCell(excel.CurSheet, "A" + rownum.ToString(), typeName);
                        rownum++;
                    }

                    if (medianame != dt.Rows[i]["MediaName"].ToString().Trim())
                    {
                        medianame = dt.Rows[i]["MediaName"].ToString().Trim();
                        int j = i;
                        int c = -1;
                        while (j < dt.Rows.Count && medianame == dt.Rows[j]["MediaName"].ToString().Trim())
                        {
                            j++;
                            c++;
                        }
                        string scell = string.Format("A{0}", rownum);
                        string ecell = string.Format("A{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, mcount.ToString());
                        //ExcelHandle.WriteCell(excel.CurSheet, scell, mcount.ToString());

                        scell = string.Format("B{0}", rownum);
                        ecell = string.Format("B{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["CityName"].ToString().Trim());
                        //ExcelHandle.WriteCell(excel.CurSheet, scell, mcount.ToString());

                        scell = string.Format("C{0}", rownum);
                        ecell = string.Format("C{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["TypeName"].ToString().Trim());
                        //ExcelHandle.WriteCell(excel.CurSheet, scell, mcount.ToString());

                        scell = string.Format("D{0}", rownum);
                        ecell = string.Format("D{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        if (dt.Rows[i]["TypeName"].ToString().Trim() == "电视媒体")
                            ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["TopicProperty"].ToString().Trim());
                        //    ExcelHandle.WriteCell(excel.CurSheet, scell, dt.Rows[i]["TopicProperty"].ToString().Trim());
                        else
                          //  ExcelHandle.WriteCell(excel.CurSheet, scell, dt.Rows[i]["ReaderSort"].ToString().Trim());
                            ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["ReaderSort"].ToString().Trim());

                        scell = string.Format("E{0}", rownum);
                        ecell = string.Format("E{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, dt.Rows[i]["MediaType"].ToString().Trim());
                        //ExcelHandle.WriteCell(excel.CurSheet, scell, mcount.ToString());

                        scell = string.Format("F{0}", rownum);
                        ecell = string.Format("F{0}", rownum + c);
                        ExcelHandle.SetHAlignCenter(excel.CurSheet, scell, ecell);
                        ExcelHandle.WriteAfterMerge(excel.CurSheet, scell, ecell, medianame);
                        //ExcelHandle.WriteCell(excel.CurSheet, scell, mcount.ToString());
                        mcount++;
                    }

                    cell = string.Format("G{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterName"].ToString().Trim());

                    cell = string.Format("H{0}", rownum);
                   ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterLevel"].ToString().Trim());

                    cell = string.Format("I{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["ReporterPosition"].ToString().Trim());

                    cell = string.Format("J{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.SetColumnWidth(excel.CurSheet, "J", 120.0);
                    ExcelHandle.SetFormula(excel.CurSheet, cell, "#");
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["Mobile"].ToString().Trim());

                    cell = string.Format("K{0}", rownum);
                    ExcelHandle.SetHAlignCenter(excel.CurSheet, cell);
                    ExcelHandle.WriteCell(excel.CurSheet, cell, dt.Rows[i]["Email"].ToString().Trim());
                    rownum++;
                }

                ExcelHandle.SetBorderAll(excel.CurSheet, "A3", cell);

                excel.CurSheet.Name = DateTime.Now.ToString().Split(' ')[0];
                ExcelHandle.SaveAS(excel.CurBook, filename);

                excel.Dispose();

                isdel = true;
                return filename;
            }
            catch (Exception exp)
            {
                excel.Dispose();
                errmsg = exp.Message;

                fname = "\\Contract.xls";
                isdel = false;
                return serverpath + fname;
            }
        }
    }
}
