using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ESP.ITIL.BusinessLogic;

namespace FinanceWeb.CostView
{
    public partial class prj : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string[] str = txtPrj.Text.Split(',');
            //{
            //"G-QTF-P-0809013",
            //"G-NCI-P-0901001",
            //"G-TMT-P-0904009",
            //"G-CMS-P-0907007",
            //"G-JDB-P-0907013",
            //"G-CMS-P-0908005",
            //"G-CMS-P-0911002",
            //"G-TMT-P-0911010",
            //"G-EPC-P-0912003",
            //"G-CMS-P-0912005",
            //"G-CMS-P-1001003",
            //"G-CMS-P-1003001",
            //"G-EPC-P-1003003",
            //"G-CMS-P-1004004",
            //"G-CMS-P-1004005",
            //"G-LGE-P-1004009",
            //"G-CMS-P-1004010",
            //"G-CMS-P-1005004",
            //"G-CMS-P-1005005",
            //"G-CMS-P-1006001",
            //"G-CMS-P-1006002",
            //"G-FXS-P-1007003",
            //"G-LGE-P-1007005",
            //"G-MAR-P-1008001",
            //"G-FXS-P-1009001",
            //"G-FXS-P-1009002",
            //"G-LGE-P-1009003",
            //"G-LGE-P-1010002",
            //"G-FXS-P-1011001",
            //"G-CMS-P-1011002",
            //"G-LGE-P-1011003",
            //"G-FXS-P-1012001",
            //"G-FXS-P-1012002",
            //"G-MAR-P-1012003",
            //"G-FXS-P-1012004",
            //"G-CMS-P-1012005",
            //"G-LGE-R-0901005",
            //"G-LVM-R-0908012",
            //"G-SMT-R-0911003",
            //"G-LGE-R-1001007",
            //"G-LGE-R-1001010",
            //"G-TMT-R-1002003",
            //"G-LVM-R-1003007",
            //"G-CMS-R-1003008",
            //"G-EPC-R-1004008",
            //"G-FXS-R-1005002" 
            // S-ZJKJ-E-202310073-D
            List<prjInfo> list = new List<prjInfo>();
            for (int i = 0; i < str.Length; i++)
            {
                string project = str[i];
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(project);
                prjInfo model = new prjInfo();
                model.Branch = project.Substring(0, 1);
                model.CustomerCode = project.Substring(2, 4);
                model.PrjCode = model.Branch + project.Substring(9, 11);
                model.Total = projectModel.TotalAmount.Value;
                model.PrjType = project.Substring(7, 1);
                model.Leader = projectModel.ApplicantEmployeeName;
                model.Dept = projectModel.GroupName;
                model.Cost = ESP.Finance.BusinessLogic.ContractCostManager.GetTotalAmountByProject(projectModel.ProjectId);
                model.OOP = ESP.Finance.BusinessLogic.ProjectExpenseManager.GetTotalExpense(projectModel.ProjectId);
                model.Type = "主";
                decimal usedCost = ESP.Finance.BusinessLogic.CheckerManager.GetOccupyCost(projectModel.ProjectId, projectModel.GroupID.Value);
                usedCost += ESP.Finance.BusinessLogic.CheckerManager.得到消耗使用总额(projectModel.ProjectCode, projectModel.GroupID.Value);
                usedCost += 第三方费用统计.实际花费_除OOP外其他的费用(projectModel.ProjectId, projectModel.GroupID.Value, 0);
                usedCost += OOP费用统计.实际花费_单项目中所有OOP费总和(projectModel.ProjectId, projectModel.GroupID.Value);
                model.Used = usedCost;
                decimal totalCost = model.Cost + model.OOP - usedCost;
                model.Balance = totalCost;
                list.Add(model);
                IList<ESP.Finance.Entity.SupporterInfo> supports = ESP.Finance.BusinessLogic.SupporterManager.GetList("projectid=" + projectModel.ProjectId.ToString());
                foreach (ESP.Finance.Entity.SupporterInfo sup in supports)
                {
                    prjInfo supmodel = new prjInfo();
                    supmodel.Branch = model.Branch;
                    supmodel.CustomerCode = model.CustomerCode;
                    supmodel.PrjCode = model.PrjCode;
                    supmodel.Total = sup.BudgetAllocation.Value;
                    supmodel.PrjType = model.PrjType;
                    supmodel.Leader = sup.LeaderEmployeeName;
                    supmodel.Dept = sup.GroupName;
                    supmodel.Cost = ESP.Finance.BusinessLogic.SupporterCostManager.GetTotalCostBySupporter(sup.SupportID);
                    supmodel.OOP = ESP.Finance.BusinessLogic.SupporterExpenseManager.GetTotalExpense(sup.SupportID);
                    supmodel.Type = "支持方";
                    decimal supusedCost = ESP.Finance.BusinessLogic.CheckerManager.GetOccupyCost(projectModel.ProjectId, sup.GroupID.Value);
                    usedCost += ESP.Finance.BusinessLogic.CheckerManager.得到消耗使用总额(projectModel.ProjectCode, sup.GroupID.Value);
                    usedCost += 第三方费用统计.实际花费_除OOP外其他的费用(projectModel.ProjectId, sup.GroupID.Value, 0);
                    usedCost += OOP费用统计.实际花费_单项目中所有OOP费总和(projectModel.ProjectId, sup.GroupID.Value);
                    supmodel.Used = usedCost;
                    decimal suptotalCost = supmodel.Cost + supmodel.OOP - supusedCost;
                    supmodel.Balance = suptotalCost;
                    list.Add(supmodel);
                }
            }
            this.GridView1.DataSource = list;
            this.GridView1.DataBind();

        }
    }

    public class prjInfo
    {
        //客户		项目号	负责人	团队	总金额	税率	支持方	Cost	OOP	类型						已使用成本	剩余成本	

        public string Branch { get; set; }
        public string CustomerCode { get; set; }
        public string PrjType { get; set; }
        public string PrjCode { get; set; }
        public string Leader { get; set; }
        public string Dept { get; set; }
        public decimal Total { get; set; }
        public decimal Cost { get; set; }
        public decimal OOP { get; set; }
        public string Type { get; set; }
        public decimal Used { get; set; }
        public decimal Balance { get; set; }

    }
}
