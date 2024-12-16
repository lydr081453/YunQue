using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Reports
{
    public partial class ImportProjectFee : ESP.Web.UI.PageBase
    {
        private string currFileExtension;
        private string currFilePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_selectOperationAuditor));

            if (!IsPostBack)
            {

                DepartmentDataBind();
                bindYear();
            }
        }

        private void bindYear()
        {
            int year = DateTime.Now.Year;

            ddlYear.Items.Add(new ListItem((year - 1).ToString(), (year - 1).ToString()));
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            ddlYear.Items.Add(new ListItem((year + 1).ToString(), (year + 1).ToString()));
            ddlYear.SelectedValue = year.ToString();

        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> getalist(int parentId)
        {
            List<List<string>> list = new List<List<string>>();
            ESP.Compatible.Department deps = new ESP.Compatible.Department();

            ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(parentId);
            try
            {

                list = ESP.Compatible.DepartmentManager.GetListForAJAX(dep.ParentID);
            }
            catch (Exception e)
            {
                e.ToString();
            }

            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("请选择...");
            list.Insert(0, c);
            return list;
        }

        private void DepartmentDataBind()
        {
            object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
            ddltype.DataSource = dt;
            ddltype.DataTextField = "NodeName";
            ddltype.DataValueField = "UniqID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (fileId.FileName == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择导入的excel文件！');", true);

                return;
            }
            string fileExtenSion;
            fileExtenSion = Path.GetExtension(fileId.FileName);
            if (fileExtenSion.ToLower() != ".xlsx")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择xlsx格式的excel！');", true);

                return;
            }
            Upload();

            ExcelDataSource(currFilePath, "YES");


        }


        private void Upload()
        {
            HttpPostedFile file = this.fileId.PostedFile;
            string fileName = file.FileName;
            string tempPath = System.IO.Path.GetTempPath(); //获取系统临时文件路径
            fileName = System.IO.Path.GetFileName(fileName); //获取文件名（不带路径）
            this.currFileExtension = System.IO.Path.GetExtension(fileName); //获取文件的扩展名
            this.currFilePath = tempPath + fileName; //获取上传后的文件路径 记录到前面声明的全局变量
            file.SaveAs(this.currFilePath); //上传
        }


        private int ExcelDataSource(string filepath, string HDR)
        {
            if (hidtype2.Value == "" || hidtype2.Value == "-1")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择导入的部门！');", true);
                return 0;
            }
            string strConn;
            int icount = 0;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=" + HDR + "\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字

                

                string firstSheetName = string.Empty;// sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字

                foreach (DataRow row in sheetsName.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString();
                    // Log(strSheetTableName);
                    if (strSheetTableName.Contains("$") && strSheetTableName.Replace("'", "").EndsWith("$"))
                    {
                        firstSheetName = row["TABLE_NAME"].ToString();//提取有效的sheet值
                        break;
                    }
                }

                string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, strConn);
                DataSet set = new DataSet();
                ada.Fill(set);

                DataTable dt = set.Tables[0];

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[j][3].ToString()) || dt.Rows[j][3].ToString() == "-")
                        continue;
                    string pcode = dt.Rows[j][2].ToString(); //N1609047V
                    string ccode = dt.Rows[j][0].ToString(); //SAM
                    string ptype = dt.Rows[j][1].ToString(); //P

                    string projectcode = pcode.Substring(0, 1) + "-" + ccode + "-" + ptype + "-" + pcode.Substring(1, pcode.Length - 1);

                    ProjectReportFeeInfo model = new ProjectReportFeeInfo();
                    model.Year = int.Parse(dt.Rows[j][4].ToString());
                    model.Month = int.Parse(dt.Rows[j][5].ToString());
                    model.ProjectCode = projectcode;
                    model.DeptId = int.Parse(hidtype2.Value);
                    model.Fee = decimal.Parse(dt.Rows[j][3].ToString().Replace(",", ""));
                    model.ProjectType = dt.Rows[j][6].ToString();

                    ProjectReportFeeManager.Add(model);

                    icount++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null || conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }

            return icount;
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void dataBind()
        {
            if (hidtype2.Value == "" || hidtype2.Value == "-1")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择部门！');", true);
                return;
            }

            string term = " deptId =@deptId and year=@year and month=@month";
            int year = int.Parse(ddlYear.SelectedValue);
            int month = int.Parse(ddlMonth.SelectedValue);

            string groupid = string.Empty;
            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                groupid = this.hidtype2.Value;
            }

            SqlParameter[] parameters = {
					new SqlParameter("@deptId", SqlDbType.Int),
                    new SqlParameter("@year", SqlDbType.Int),
                    new SqlParameter("@month", SqlDbType.Int)
                                        };
            parameters[0].Value = groupid;
            parameters[1].Value = year;
            parameters[2].Value = month;

            var reportdata = ESP.Finance.BusinessLogic.ProjectReportFeeManager.GetList(term, parameters.ToList());

            this.gvG.DataSource = reportdata;
            this.gvG.DataBind();

            lblTotal.Text = "总计：￥" + reportdata.Sum(x => x.Fee).ToString("#,##0.00");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hidtype2.Value == "" || hidtype2.Value == "-1")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择部门/年份/月份！');", true);
                return;
            }

            int year = int.Parse(ddlYear.SelectedValue);
            int month = int.Parse(ddlMonth.SelectedValue);

            int groupid = 0;
            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                groupid = int.Parse(this.hidtype2.Value);
            }

            ESP.Finance.BusinessLogic.ProjectReportFeeManager.Delete(groupid, year, month);

            dataBind();
        }
    }
}