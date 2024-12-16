using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
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
namespace FinanceWeb.RebateRegistration
{
    public partial class RebateRegistrationImp : ESP.Web.UI.PageBase
    {
        private string FileExtension;
        private string FilePath;
        private static List<RebateRegistrationInfo> errorList = new List<RebateRegistrationInfo>();
        private static List<RebateRegistrationInfo> impList = new List<RebateRegistrationInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                errorList.Clear();
                impList.Clear();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RebateRegistrationList.aspx?Type=csm");
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

            try
            {
                int ret = ExcelDataSource(FilePath);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('共验证通过" + ret.ToString() + "条数据!');", true);
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
            this.FileExtension = System.IO.Path.GetExtension(fileName); //获取文件的扩展名
            this.FilePath = tempPath + fileName; //获取上传后的文件路径 记录到前面声明的全局变量
            file.SaveAs(this.FilePath); //上传
        }


        private int ExcelDataSource(string filepath)
        {
            errorList.Clear();
            impList.Clear();
            List<RebateRegistrationInfo> tempList = new List<RebateRegistrationInfo>();
            string strConn;
            string HDR = "YES";
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
                    if (strSheetTableName.Contains("$") && strSheetTableName.Replace("'", "").EndsWith("$"))
                    {
                        firstSheetName = row["TABLE_NAME"].ToString();
                        break;
                    }
                }
                
                string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, strConn);
                DataSet set = new DataSet();
                ada.Fill(set);

                DataTable dt = set.Tables[0];
               

                #region"load and check list"
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string errorMsg = string.Empty;

                    ESP.Finance.Entity.RebateRegistrationInfo temp = new RebateRegistrationInfo();

                    string OrderYM = dt.Rows[j][5].ToString().TrimEnd('年') + "年" + dt.Rows[j][6].ToString().TrimEnd('月') + "月";
                    string projectCode = dt.Rows[j][3].ToString().Trim(); //项目号
                    string media = dt.Rows[j][8].ToString().Trim(); //媒体主体  
                    string Amount = dt.Rows[j][1].ToString().Trim();
                    string Description = dt.Rows[j][7].ToString().Trim();
                    string AccountingNum = dt.Rows[j][0].ToString().Trim();
                    string SettleType = dt.Rows[j][2].ToString().Trim();
                    string Branch = dt.Rows[j][4].ToString().Trim();

                    temp.CreditedDate = OrderYM;
                    temp.ProjectCode = projectCode;
                    temp.Remark = Description;
                    decimal rebateAmount = 0m;
                    temp.SupplierName = media ;
                    temp.RowNo = j + 2;
                    temp.Status = Common.RebateRegistration_Status.Submited;
                    temp.Branch = Branch;
                    temp.SettleType = SettleType;
                    temp.AccountingNum = AccountingNum;

                    tempList.Add(temp);

                    if (decimal.TryParse(Amount, out rebateAmount))
                    {
                        temp.RebateAmount = rebateAmount;
                        ESP.Purchase.Entity.SupplierInfo supplierModel = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(temp.SupplierName, -1);
                        if (!string.IsNullOrEmpty(temp.ProjectCode))
                        {
                            #region 项目媒体返点
                            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(temp.ProjectCode);
                            if (projectModel == null)//项目不存在
                            {
                                errorMsg = "未找到对应的项目号;";
                            }
                            temp.ProjectId = projectModel.ProjectId;
                            //如果有项目号，判断excel内的媒体主体是否在项目中存在
                            //项目有多个媒体的情况
                            if (supplierModel != null)//供应商库中存在
                            {
                                temp.SupplierId = supplierModel.id;
                                //检查项目号中是否存在媒体
                                string strWhere = " projectId=@projectId and supplierId=@supplierId ";
                                List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
                                SqlParameter projectId = new SqlParameter("@projectId", SqlDbType.Int, 4);
                                projectId.Value = projectModel.ProjectId;
                                paramlist.Add(projectId);
                                SqlParameter supplierId = new SqlParameter("@supplierId", SqlDbType.Int, 4);
                                supplierId.Value = supplierModel.id;
                                paramlist.Add(supplierId);
                                var mediaList = ESP.Finance.BusinessLogic.ProjectMediaManager.GetList(strWhere, paramlist);
                                if (mediaList == null || mediaList.Count == 0)
                                {
                                    errorMsg += "项目号中无媒体主体记录;";
                                }
                                else//检查返点金额是否超出充值金额
                                {
                                    string returnStr = " a.projectId=@projectId and a.supplierId=@supplierId and a.status<>@status ";
                                    List<System.Data.SqlClient.SqlParameter> plist = new List<System.Data.SqlClient.SqlParameter>();
                                    SqlParameter pid = new SqlParameter("@projectId", SqlDbType.Int, 4);
                                    pid.Value = projectModel.ProjectId;
                                    plist.Add(pid);

                                    SqlParameter supplierName = new SqlParameter("@supplierId", SqlDbType.Int, 4);
                                    supplierName.Value = supplierModel.id;
                                    plist.Add(supplierName);

                                    SqlParameter status = new SqlParameter("@status", SqlDbType.Int, 4);
                                    status.Value = (int)ESP.Finance.Utility.Common.RebateRegistration_Status.Deleted;
                                    plist.Add(status);

                                    decimal tempTotal = tempList.Where(x => x.ProjectCode == temp.ProjectCode && x.SupplierName == temp.SupplierName ).Sum(x => x.RebateAmount);//本次导入该项目对应媒体主体返点总金额
                                    decimal rechargeTotal = mediaList.Sum(x => x.Recharge);
                                    decimal importedTotal = ESP.Finance.BusinessLogic.RebateRegistrationManager.GetList(returnStr, plist).Sum(x => x.RebateAmount);//该项目对应媒体主体已导入返点总金额

                                    if (importedTotal + tempTotal > rechargeTotal)
                                    {
                                        errorMsg += "大于充值金额." + "本次导入：" + tempTotal.ToString("#,##0.00") + "已导入：" + importedTotal.ToString("#,##0.00") + "充值金额：" + rechargeTotal.ToString("#,##0.00") + ";";
                                    }
                                }
                            }
                            else//供应商库中无此媒体
                            {
                                errorMsg = "媒体主体不存在;";
                            }
                            #endregion
                        }
                        else
                        {
                            if (supplierModel != null)//供应商库中存在
                            {
                                temp.SupplierId = supplierModel.id;
                            }
                            else
                            {
                                errorMsg = "媒体主体不存在;";
                            }
                        }
                    }
                    else
                    {
                        errorMsg = "返点金额格式不正确;";
                    }

                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        temp.ErrorContent = errorMsg;

                        errorList.Add(temp);

                        errorMsg = string.Empty;
                    }
                    else
                    {
                        impList.Add(temp);
                    }

                   
                }
                #endregion

                #region"bind List"

 
                    this.gvImp.DataSource = impList;
                    this.gvImp.DataBind();

                    this.GvError.DataSource = errorList;
                    this.GvError.DataBind();
                
                #endregion

                    return impList.Count;
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
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            if (errorList.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请先处理返点异常数据，暂时无法提交！');", true);

                return;
            }

            if (fileAtt.FileName == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择证明文件！');", true);
                return;
            }

            if (impList != null && impList.Count > 0)
            {
                //添加批次
                ESP.Finance.Entity.PNBatchInfo batchModel = new PNBatchInfo();
                batchModel.Amounts = impList.Sum(x => x.RebateAmount);
                batchModel.PurchaseBatchCode = PNBatchManager.CreatePurchaseBatchCode();
                batchModel.CreateDate = DateTime.Now;
                batchModel.Status = (int)ESP.Finance.Utility.PaymentStatus.Submit;
                batchModel.CreatorID = CurrentUserID;
                batchModel.Creator = CurrentUser.Name;
                batchModel.BatchType = 5;
                if (string.IsNullOrEmpty(txtDesc.Text))
                    batchModel.Description = "媒体返点导入" + DateTime.Now.ToString("yyyy-MM-dd");
                else
                    batchModel.Description = txtDesc.Text;
                batchModel.BatchCode = "";
                batchModel.PaymentUserID = 0;
                batchModel.PaymentUserName = "";
                batchModel.PaymentEmployeeName = "";

                if (this.fileAtt.FileName != string.Empty)
                {
                    string fileName = "RebateRegistration_" + Guid.NewGuid().ToString() + "_" + this.fileAtt.FileName;

                    this.fileAtt.SaveAs(ESP.Finance.Configuration.ConfigurationManager.ContractPath + fileName);
                    batchModel.ProveFile = fileName;
                }

                //excel信息批量导入数据库
                int icount = ESP.Finance.BusinessLogic.RebateRegistrationManager.ImpList(batchModel, impList);

                ClientScript.RegisterStartupScript(typeof(string), "", "alert('共导入" + icount.ToString() + "条数据!');", true);

                Response.Redirect("RebateRegistrationList.aspx?Type=rr");
            }
        }

        protected void GvError_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void gvImp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }



    }
}