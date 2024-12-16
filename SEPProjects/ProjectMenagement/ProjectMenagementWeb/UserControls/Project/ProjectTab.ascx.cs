using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class UserControls_Project_ProjectTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string type = string.IsNullOrEmpty(Request["Type"]) ? "project" : Request["Type"].ToString();
                switch (type)
                {
                    case "project":
                        tabProject.Attributes["class"] = "button_on";
                        tabOOP.Attributes["class"] = "button_over";
                        tabPayment.Attributes["class"] = "button_over";
                        tabReturn.Attributes["class"] = "button_over";
                        tabSupporter.Attributes["class"] = "button_over";
                        tabConsumption.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_over";
                        tabRefund.Attributes["class"] = "button_over";

                        tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                        tabOOP.Attributes.Add("onmouseout","changeClass2(this);");
                        tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                        tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                        tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                        tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                         tabConsumption.Attributes.Add("onmouseover", "changeClass(this);");
                        tabConsumption.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRebateRegistration.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRebateRegistration.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                        break;
                    case "supporter":
                        tabProject.Attributes["class"] = "button_over";
                        tabOOP.Attributes["class"] = "button_over";
                        tabPayment.Attributes["class"] = "button_over";
                        tabReturn.Attributes["class"] = "button_over";
                        tabSupporter.Attributes["class"] = "button_on";
                        tabConsumption.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_over";
                        tabRefund.Attributes["class"] = "button_over";

                        tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                        tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                        tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                        tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                        tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                         tabConsumption.Attributes.Add("onmouseover", "changeClass(this);");
                        tabConsumption.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRebateRegistration.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRebateRegistration.Attributes.Add("onmouseout", "changeClass2(this);");
                         tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                        break;
                    case "return":
                        tabProject.Attributes["class"] = "button_over";
                        tabOOP.Attributes["class"] = "button_over";
                        tabPayment.Attributes["class"] = "button_over";
                        tabReturn.Attributes["class"] = "button_on";
                        tabSupporter.Attributes["class"] = "button_over";
                        tabConsumption.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_over";
                        tabRefund.Attributes["class"] = "button_over";

                        tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                        tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                        tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                        tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                        tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                         tabConsumption.Attributes.Add("onmouseover", "changeClass(this);");
                        tabConsumption.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRebateRegistration.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRebateRegistration.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                        break;
                    case "payment":
                        tabProject.Attributes["class"] = "button_over";
                        tabOOP.Attributes["class"] = "button_over";
                        tabPayment.Attributes["class"] = "button_on";
                        tabReturn.Attributes["class"] = "button_over";
                        tabSupporter.Attributes["class"] = "button_over";
                        tabConsumption.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_over";
                        tabRefund.Attributes["class"] = "button_over";

                        tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                        tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                        tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                        tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                        tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                         tabConsumption.Attributes.Add("onmouseover", "changeClass(this);");
                        tabConsumption.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRebateRegistration.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRebateRegistration.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                        break;
                    case "oop":
                        tabProject.Attributes["class"] = "button_over";
                        tabOOP.Attributes["class"] = "button_on";
                        tabPayment.Attributes["class"] = "button_over";
                        tabReturn.Attributes["class"] = "button_over";
                        tabSupporter.Attributes["class"] = "button_over";
                        tabConsumption.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_over";
                        tabRefund.Attributes["class"] = "button_over";

                        tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                        tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                        tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                        tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                        tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                         tabConsumption.Attributes.Add("onmouseover", "changeClass(this);");
                        tabConsumption.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRebateRegistration.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRebateRegistration.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                        break;
                    case "csm":
                        tabProject.Attributes["class"] = "button_over";
                        tabOOP.Attributes["class"] = "button_over";
                        tabPayment.Attributes["class"] = "button_over";
                        tabReturn.Attributes["class"] = "button_over";
                        tabSupporter.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_over";
                        tabConsumption.Attributes["class"] = "button_on";
                        tabRefund.Attributes["class"] = "button_over";
                       
                        tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                        tabOOP.Attributes.Add("onmouseout","changeClass2(this);");
                        tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                        tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                        tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                        tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                        tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRebateRegistration.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRebateRegistration.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");
                        break;
                    case "rr":
                        tabProject.Attributes["class"] = "button_over";
                        tabOOP.Attributes["class"] = "button_over";
                        tabPayment.Attributes["class"] = "button_over";
                        tabReturn.Attributes["class"] = "button_over";
                        tabSupporter.Attributes["class"] = "button_over";
                        tabConsumption.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_on";
                        tabRefund.Attributes["class"] = "button_over";

                        tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                        tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                        tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                        tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                        tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                        tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRefund.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRefund.Attributes.Add("onmouseout", "changeClass2(this);");

                        break;
                    case "refund":
                        tabRefund.Attributes["class"] = "button_on";
                        tabProject.Attributes["class"] = "button_over";
                        tabOOP.Attributes["class"] = "button_over";
                        tabPayment.Attributes["class"] = "button_over";
                        tabReturn.Attributes["class"] = "button_over";
                        tabSupporter.Attributes["class"] = "button_over";
                        tabConsumption.Attributes["class"] = "button_over";
                        tabRebateRegistration.Attributes["class"] = "button_over";

                        tabOOP.Attributes.Add("onmouseover", "changeClass(this);");
                        tabOOP.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabPayment.Attributes.Add("onmouseover", "changeClass(this);");
                        tabPayment.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabReturn.Attributes.Add("onmouseover", "changeClass(this);");
                        tabReturn.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabSupporter.Attributes.Add("onmouseover", "changeClass(this);");
                        tabSupporter.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabProject.Attributes.Add("onmouseover", "changeClass(this);");
                        tabProject.Attributes.Add("onmouseout", "changeClass2(this);");
                        tabRebateRegistration.Attributes.Add("onmouseover", "changeClass(this);");
                        tabRebateRegistration.Attributes.Add("onmouseout", "changeClass2(this);");

                        break;
                }
            }
        }
