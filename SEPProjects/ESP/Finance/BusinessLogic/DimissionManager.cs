using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Finance.DataAccess;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using Excel = Microsoft.Office.Interop.Excel;

namespace ESP.Finance.BusinessLogic
{
   public static class DimissionManager
    {
       public static DataTable GetPRList(string username)
       {
           SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,50)};
           parameters[0].Value = username;

           DataSet ds = DbHelperSQL.RunProcedure("GetDimissionPRData", parameters, "PR");
           return ds.Tables[0];
       }

       //public static DataTable GetProjectList(string username)
       //{
       //    SqlParameter[] parameters = {
       //             new SqlParameter("@UserName", SqlDbType.NVarChar,50)};
       //    parameters[0].Value = username;

       //    DataSet ds = DbHelperSQL.RunProcedure("GetDimissionProject", parameters, "Project");
       //    return ds.Tables[0];
       //}

       public static string ExportPrList( DataTable dtPr,DataTable dtPrProject, System.Web.HttpResponse response)
       {
           string temppath = "/Templates/DimissionTemplate.xls";
           string sourceFile = Common.GetLocalPath(temppath);
           ExcelHandle excel = new ExcelHandle();
           try
           {
               excel.Load(sourceFile);
               excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[1];
               int rownum = 2;
               string cell = "A2";

               for (int i = 0; i < dtPr.Rows.Count; i++)
               {
                   //PN No	PN申请金额	PR申请金额	PN状态	状态	PR 流水	PR No	申请人	申请日期	业务组	项目号	供应商
                   //returncode,prefee,totalprice,returnstatus,status,a.id,a.prno,requestorname,app_date,requestor_group,project_code,supplier_name 
                   cell = string.Format("A{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["returncode"].ToString());

                   cell = string.Format("B{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["prefee"].ToString());

                   cell = string.Format("C{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["totalprice"].ToString());

                   cell = string.Format("D{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, ReturnPaymentType.ReturnStatusString(dtPr.Rows[i]["returnstatus"]==DBNull.Value?0:int.Parse(dtPr.Rows[i]["returnstatus"].ToString()),0,null));

                   cell = string.Format("E{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, "'" + ESP.Purchase.Common.State.requistionOrorder_state[dtPr.Rows[i]["status"]==DBNull.Value?0:int.Parse( dtPr.Rows[i]["status"].ToString())].ToString());

                   cell = string.Format("F{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["id"].ToString());

                   cell = string.Format("G{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["prno"].ToString());

                   cell = string.Format("H{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["requestorname"].ToString());

                   cell = string.Format("I{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["app_date"]);

                   cell = string.Format("J{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["requestor_group"].ToString());

                   cell = string.Format("K{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["project_code"].ToString());

                   cell = string.Format("L{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPr.Rows[i]["supplier_name"].ToString());

                   rownum++;
               }

               excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[2];
               rownum = 2;

               for (int i = 0; i < dtPrProject.Rows.Count; i++)
               {
                   //status,projectid,projectcode,groupname,businessdescription,applicantemployeename
                   cell = string.Format("A{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, State.SetState(int.Parse(dtPrProject.Rows[i]["status"].ToString())));

                   cell = string.Format("B{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPrProject.Rows[i]["projectcode"].ToString());

                   cell = string.Format("C{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPrProject.Rows[i]["groupname"].ToString());

                   cell = string.Format("D{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPrProject.Rows[i]["businessdescription"].ToString());

                   cell = string.Format("E{0}", rownum);
                   ExcelHandle.WriteCell(excel.CurSheet, cell, dtPrProject.Rows[i]["applicantemployeename"].ToString());

               }

               string serverpath = Common.GetLocalPath("/Templates");
               if (!System.IO.Directory.Exists(serverpath))
                   System.IO.Directory.CreateDirectory(serverpath);
               string desFilename = Guid.NewGuid().ToString() + ".xls";
               string desFile = serverpath + "/" + desFilename;
               string desPath = "/Templates" + desFilename;
               ExcelHandle.SaveAS(excel.CurBook, desFile);
               excel.Dispose();
               ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
               return desPath;
           }
           catch
           {
               excel.Dispose();
               return "";
           }
       }


    }
}
