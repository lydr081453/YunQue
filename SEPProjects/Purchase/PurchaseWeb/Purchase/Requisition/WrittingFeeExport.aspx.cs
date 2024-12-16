using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.ConfigCommon;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class WrittingFeeExport : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkbtnReporter_Click(object sender, EventArgs e)
        {
            if (hidReporterIDs.Value != string.Empty)
            {
                string[] strReporterIDs = hidReporterIDs.Value.Split(',');

                IList<ESP.Media.Entity.ReportersInfo> list = new List<ESP.Media.Entity.ReportersInfo>();
                foreach (GridViewRow rw in this.gvReporter.Rows)
                {
                    HiddenField hidReporterID = (HiddenField)rw.FindControl("hidReporterID");
                    if (hidReporterID != null && hidReporterID.Value != string.Empty)
                    {
                        ESP.Media.Entity.ReportersInfo info = ESP.Media.BusinessLogic.ReportersManager.GetModel(Convert.ToInt32(hidReporterID.Value.Trim()));
                        if (info != null)
                            list.Add(info);
                    }
                }
                foreach (string rid in strReporterIDs)
                {
                    int id = 0;
                    int.TryParse(rid, out id);
                    if (id > 0)
                    {
                        ESP.Media.Entity.ReportersInfo info = ESP.Media.BusinessLogic.ReportersManager.GetModel(id);
                        if (info != null)
                        { 
                            list.Add(info);
                        }
                    }
                }

                ReporterList = list;
                this.gvReporter.DataSource = ReporterList;
                this.gvReporter.DataBind();
            }
        }

        protected IList<ESP.Media.Entity.ReportersInfo> ReporterList
        {
            get
            {
                if (null == ViewState["ReporterList"])
                    ViewState["ReporterList"] = new List<ESP.Media.Entity.ReportersInfo>();
                return (IList<ESP.Media.Entity.ReportersInfo>)ViewState["ReporterList"];
            }
            set
            {
                ViewState["ReporterList"] = value;
            }
        }

        protected void gvReporter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Media.Entity.ReportersInfo info = (ESP.Media.Entity.ReportersInfo)e.Row.DataItem;
                if (info != null)
                {
                    Label lblMediaName = (Label)e.Row.FindControl("lblMediaName");
                    if (lblMediaName != null)
                    {
                        ESP.Media.Entity.MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(info.Media_id);
                        if (media != null)
                        {
                            lblMediaName.Text = media.Mediacname;
                        }
                    }
                }
            }
        }

        protected void gvReporter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int mediaorderid = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                ReporterList.RemoveAt(Convert.ToInt32(e.CommandArgument.ToString()));
                this.gvReporter.DataSource = ReporterList;
                this.gvReporter.DataBind();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e) 
        {
            try
            {
                string filepath = ExportExcel(ReporterList, Response);
                GC.Collect();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        private string ExportExcel(IList<ESP.Media.Entity.ReportersInfo> list, System.Web.HttpResponse response)
        {
            string sourceFileName = ESP.Purchase.Common.ServiceURL.ReporterPath+"ReporterExportTemplate.xls";
            if (list == null || list.Count == 0) return sourceFileName;
            #region write sheet
            ExcelHandle excel = new ExcelHandle();
            excel.Load(sourceFileName);
            excel.CurSheet = (Microsoft.Office.Interop.Excel._Worksheet)excel.CurBook.Sheets[1];
            int lineoffset = 1;//行数索引

            foreach (ESP.Media.Entity.ReportersInfo model in list)
            {
                ESP.Media.Entity.MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(model.Media_id);
                //媒体名称
                string salary_cellA = "A" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellA, "'" + media.Mediacname);

                //文章标题
                string salary_cellB = "B" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellB, "'");
                //见报日期（仅用于刊后）
                string salary_cellC = "C" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellC, "'");
                //刊发字数
                string salary_cellD = "D" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellD, "'");
                //金额
                string salary_cellE = "E" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellE, "'");

                //联系人（记者）
                string salary_cellF = "F" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellF, "'" + model.Reportername);
                //联系人联系方式
                string salary_cellG = "G" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellG, "'" + model.Tel_h);
                //收款人
                string salary_cellH = "H" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellH, "'" + model.Reportername);
                //收款人所在城市
                string salary_cellI = "I" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellI, "'"+model.CityName);
                //开户行
                string salary_cellJ = "J" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellJ, "'" + model.Bankname);
                //帐号
                string salary_cellK = "K" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellK, "'");
                //身份证号
                string salary_cellL = "L" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellL, "'" + model.Cardnumber);
                //联系方式
                string salary_cellM = "M" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellM, "'" + model.Usualmobile);
                //图片尺寸
                string salary_cellN = "N" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellN, "'");
                //稿件链接
                string salary_cellO = "O" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellO, "'");
                //发刊起始日期（仅用于刊前形式）
                string salary_cellP= "P" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellP, "'");
                //发刊截止日期（仅用于刊前形式）
                string salary_cellQ = "Q" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellQ, "'");
                //是否代理
                string salary_cellR = "R" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellR, "'0");


                //记者系统ID
                string salary_cellS = "S" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellS, "'" + model.Reporterid.ToString());
                //媒体系统ID
                string salary_cellT = "T" + (lineoffset + 1).ToString();
                ExcelHandle.WriteCell(excel.CurSheet, salary_cellT, "'"+media.Mediaitemid.ToString());
                lineoffset++;
            }
            #endregion

            if (!System.IO.Directory.Exists(ESP.Purchase.Common.ServiceURL.ReporterPath))
                System.IO.Directory.CreateDirectory(ESP.Purchase.Common.ServiceURL.ReporterPath);
            string desFilename = Guid.NewGuid().ToString() + ".xls";
            string desFile = ESP.Purchase.Common.ServiceURL.ReporterPath + desFilename;
            ExcelHandle.SaveAS(excel.CurBook, desFile);
            excel.Dispose();

            try
            {
                ESP.Purchase.Common.FileHelper.outExcel(desFile, desFilename, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return desFilename;
        }
    }
}