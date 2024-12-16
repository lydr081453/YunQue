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

namespace FinanceWeb.Return
{
    public partial class SalaryImport : ESP.Web.UI.PageBase
    {
        private string SalaryFileExtension;
        private string SalaryFilePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_selectOperationAuditor));

            if (!IsPostBack)
            {
                bindYear();
                bindMonth();
            }
        }

        private void bindYear()
        {
            int year = DateTime.Now.Year;

            ddlYear.Items.Add(new ListItem((year - 1).ToString(), (year - 1).ToString()));
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            ddlYear.SelectedValue = year.ToString();

        }

        private void bindMonth()
        {

            for (int i = 1; i < 14; i++)
            {
                ListItem m = new ListItem();
                m.Text = i.ToString() + "月";
                m.Value = i.ToString();
                ddlMonth.Items.Add(m);
            }

            int selectedValue = (DateTime.Now.Month - 1);
            if (selectedValue == 0)
                selectedValue = 1;
            ddlMonth.SelectedValue = selectedValue.ToString();
        }

        protected void btnSalaryImport_Click(object sender, EventArgs e)
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

            try
            {
                int ret = SalaryExcelDataSource(SalaryFilePath);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('共导入" + ret.ToString() + "条数据!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "!');", true);
            }
        }


        private void Upload()
        {
            HttpPostedFile file = this.fileId.PostedFile;
            string fileName = file.FileName;
            string tempPath = System.IO.Path.GetTempPath(); //获取系统临时文件路径
            fileName = System.IO.Path.GetFileName(fileName); //获取文件名（不带路径）
            this.SalaryFileExtension = System.IO.Path.GetExtension(fileName); //获取文件的扩展名
            this.SalaryFilePath = tempPath + fileName; //获取上传后的文件路径 记录到前面声明的全局变量
            file.SaveAs(this.SalaryFilePath); //上传
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void dataBind()
        {
            string term = " SalaryYear=@SalaryYear and SalaryMonth=@SalaryMonth";
            int year = int.Parse(ddlYear.SelectedValue);
            int month = int.Parse(ddlMonth.SelectedValue);

            SqlParameter[] parameters = {
                    new SqlParameter("@SalaryYear", SqlDbType.Int),
                     new SqlParameter("@SalaryMonth", SqlDbType.Int)
                                        };
            parameters[0].Value = year;
            parameters[1].Value = month;

            var reportdata = ESP.Finance.BusinessLogic.SalaryManager.GetList(term, parameters.ToList());

            this.gvG.DataSource = reportdata;
            this.gvG.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int year = int.Parse(ddlYear.SelectedValue);
            int month = int.Parse(ddlMonth.SelectedValue);
            ESP.Finance.BusinessLogic.SalaryManager.Delete(year, month,CurrentUserID);

            dataBind();
        }

        private int SalaryExcelDataSource(string filepath)
        {
            string strConn;
            string HDR = "YES";
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
                    //Log(strSheetTableName);
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
                string errorMsg = string.Empty;

                DataTable dt = set.Tables[0];
                for (int j = 1; j < dt.Rows.Count; j++)
                {
                    string namecn = dt.Rows[j][2].ToString().Trim(); //namecn
                    string code = dt.Rows[j][3].ToString().Trim(); //usercode
                    string idnumber = dt.Rows[j][4].ToString().Trim();

                    //Log(namecn + "-" + code + "-" + idnumber);

                    if (string.IsNullOrEmpty(dt.Rows[j][3].ToString()))
                    {
                        errorMsg = namecn + "员工编号为空";
                        break;
                    }
                    ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(code);

                    if (empModel == null)
                    {
                        errorMsg = code + "未找到对应的员工编号";
                        break;
                    }
                    if (empModel.IDNumber != idnumber)
                    {
                        errorMsg = namecn + "身份证号与系统不符";
                        break;
                    }
                }
                if (string.IsNullOrEmpty(errorMsg))
                {

                    for (int j = 1; j < dt.Rows.Count; j++)
                    {
                        string year = dt.Rows[j][0].ToString().Trim();
                        string month = dt.Rows[j][1].ToString().Trim();
                        string namecn = dt.Rows[j][2].ToString().Trim(); //namecn
                        string code = dt.Rows[j][3].ToString().Trim(); //usercode
                        string idnumber = dt.Rows[j][4].ToString().Trim();
                        string SalaryBased = dt.Rows[j][5].ToString().Trim();
                        string SalaryPerformance = dt.Rows[j][6].ToString().Trim();

                        string affairCut = dt.Rows[j][7].ToString().Trim();//事假扣款
                        string sickCut = dt.Rows[j][8].ToString();//病假扣款
                        string LateCut = dt.Rows[j][9].ToString();//迟到
                        string absenceCut = dt.Rows[j][10].ToString();//早退
                        string clockCut = dt.Rows[j][11].ToString();//忘打卡
                        string kaoqinTotal = dt.Rows[j][12].ToString();//考勤小计
                        string otherCut = dt.Rows[j][13].ToString();//其他减除费用
                        string otherIncome = dt.Rows[j][14].ToString();//其他增加费用

                        string income = dt.Rows[j][15].ToString();// 当月收入 
                        string housing = dt.Rows[j][16].ToString();//住房
                        string retirement = dt.Rows[j][17].ToString();//养老
                        string unEmp = dt.Rows[j][18].ToString();//失业
                        string medical = dt.Rows[j][19].ToString();//医疗
                        string insuranceTotal = dt.Rows[j][20].ToString();//社保小计
                        string salaryPretax = dt.Rows[j][21].ToString();//税前工资
                        string tax3 = dt.Rows[j][22].ToString();// 本期应预扣预缴税额 
                        string taxedCut = dt.Rows[j][23].ToString();// 税后扣款 

                        string salaryPaid = dt.Rows[j][24].ToString();// 实发金额 
                        string remark = dt.Rows[j][25].ToString();
                        string branchcode = dt.Rows[j][26].ToString();

                        ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(code);

                        SalaryInfo model = new SalaryInfo();
                        model.UserId = empModel.UserID;
                        model.SalaryYear = int.Parse(year);
                        model.SalaryMonth = int.Parse(month);
                        model.IDNumber = empModel.IDNumber;
                        model.NameEN = empModel.FullNameEN;
                        model.NameCN = namecn;
                        model.UserCode = code;

                        model.SalaryBased = ESP.Salary.Utility.DESEncrypt.Encode(SalaryBased);
                        model.SalaryPerformance = ESP.Salary.Utility.DESEncrypt.Encode(SalaryPerformance);

                        model.AffairCut = ESP.Salary.Utility.DESEncrypt.Encode(affairCut);
                        model.SickCut = ESP.Salary.Utility.DESEncrypt.Encode(sickCut);
                        model.LateCut = ESP.Salary.Utility.DESEncrypt.Encode(LateCut);
                        model.AbsenceCut = ESP.Salary.Utility.DESEncrypt.Encode(absenceCut);
                        model.ClockCut = ESP.Salary.Utility.DESEncrypt.Encode(clockCut);
                        model.KaoqinTotal = ESP.Salary.Utility.DESEncrypt.Encode(kaoqinTotal);
                        model.OtherCut = ESP.Salary.Utility.DESEncrypt.Encode(otherCut);
                        model.OtherIncome = ESP.Salary.Utility.DESEncrypt.Encode(otherIncome);
                        model.Income = ESP.Salary.Utility.DESEncrypt.Encode(income);

                        model.Housing = ESP.Salary.Utility.DESEncrypt.Encode(housing);
                        model.Retirement = ESP.Salary.Utility.DESEncrypt.Encode(retirement);
                        model.UnEmp = ESP.Salary.Utility.DESEncrypt.Encode(unEmp);
                        model.Medical = ESP.Salary.Utility.DESEncrypt.Encode(medical);
                        model.InsuranceTotal = ESP.Salary.Utility.DESEncrypt.Encode(insuranceTotal);

                        model.SalaryPretax = ESP.Salary.Utility.DESEncrypt.Encode(salaryPretax);
                        model.Tax3 = ESP.Salary.Utility.DESEncrypt.Encode(tax3);// 本期应预扣预缴税额 

                        model.TaxedCut = ESP.Salary.Utility.DESEncrypt.Encode(taxedCut);
                        model.SalaryPaid = ESP.Salary.Utility.DESEncrypt.Encode(salaryPaid);
                        model.Remark = remark;
                        model.BranchCode = branchcode;

                        model.EmailPassword = string.Empty;
                        model.EmailSendTime = new DateTime(1900, 1, 1);

                        model.Importer = CurrentUserID;
                        model.ImportTime = DateTime.Now;

                        SalaryManager.Add(model);
                        icount++;
                    }
                }
                else
                {
                    throw new Exception(errorMsg);
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



    }
}