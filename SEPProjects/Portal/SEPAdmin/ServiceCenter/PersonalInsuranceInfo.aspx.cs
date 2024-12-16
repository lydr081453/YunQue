using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.ServiceCenter
{
    public partial class PersonalInsuranceInfo : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            InsuranceBaseInfo model = InsuranceBaseInfoManager.GetModel(ddlCity.SelectedValue);
            List<InsuranceBase> blist = InsuranceBaseInfoManager.GetBaseList(ddlCity.SelectedValue);
 
            ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(CurrentUser.SysID));
                

            JS.Value = decimal.Parse(txtPrice.Text).ToString("#,##0.00");
            QJS.Text = blist.Where(x => x.InsuranceType == "养老").FirstOrDefault().MinPrice.ToString("#,##0.00");
            QYLJS.Text = blist.Where(x => x.InsuranceType == "医疗").FirstOrDefault().MinPrice.ToString("#,##0.00");

            //养老
            string YLXS_C = model.YangLao_C;
            if (ddlCity.SelectedValue == "广州")
            {
               if (emp.Residence != "本市城镇")
                    YLXS_C = model.YangLao_C_FBD;
            }
            decimal sjYangLao = decimal.Parse(JS.Value.Replace(",", ""));
            var yalaoModel = blist.Where(n => n.InsuranceType == "养老").FirstOrDefault();
            if (yalaoModel != null)
            {
                if (yalaoModel.MinPrice == 0)
                {
                    sjYangLao = decimal.Parse(JS.Value.Replace(",", ""));
                }
                else if (decimal.Parse(txtPrice.Text) < yalaoModel.MinPrice)
                {
                    sjYangLao = yalaoModel.MinPrice;
                }
                if (decimal.Parse(txtPrice.Text) > yalaoModel.MaxPrice)
                {
                    sjYangLao = yalaoModel.MaxPrice;
                }
            }
            JS9.Text = sjYangLao.ToString("#,##0.00");

            YangLao_C.Text = QYangLao_C.Text = YLXS_C + "%";
            YangLao_P.Text = QYangLao_P.Text = model.YangLao_P + "%";
            YangLao_CJ.Text = (sjYangLao * decimal.Parse(YLXS_C) * 0.01M).ToString("#,##0.00");
            YangLao_PJ.Text = (sjYangLao * decimal.Parse(model.YangLao_P) * 0.01M).ToString("#,##0.00");
            QYangLao_CJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(YLXS_C) * 0.01M).ToString("#,##0.00");
            QYangLao_PJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.YangLao_P) * 0.01M).ToString("#,##0.00");

            YangLao_CE.Text = (decimal.Parse(YangLao_CJ.Text.Replace(",", "")) - decimal.Parse(QYangLao_CJ.Text.Replace(",", ""))).ToString("#,##0.00");

            //失业
            decimal sjShiYe = decimal.Parse(JS.Value.Replace(",", ""));
            var shiyeModel = blist.Where(n => n.InsuranceType == "失业").FirstOrDefault();
            if (shiyeModel != null)
            {
                if (shiyeModel.MinPrice == 0)
                {
                    sjShiYe = decimal.Parse(JS.Value.Replace(",", ""));
                }
                else if (decimal.Parse(txtPrice.Text) < shiyeModel.MinPrice)
                {
                    sjShiYe = shiyeModel.MinPrice;
                }
                if (decimal.Parse(txtPrice.Text) > shiyeModel.MaxPrice)
                {
                    sjShiYe = shiyeModel.MaxPrice;
                }
            }
            JS0.Text = sjShiYe.ToString("#,##0.00");

            decimal shiye_p = decimal.Parse(model.ShiYe_P);

            if (ddlCity.SelectedValue == "北京")
            {
                if (ddlHukou.SelectedValue=="农业")
                    shiye_p = 0;
            }
            

            ShiYe_C.Text = QShiYe_C.Text = model.ShiYe_C + "%";
            ShiYe_P.Text = QShiYe_P.Text = shiye_p + "%";
            ShiYe_CJ.Text = (sjShiYe * decimal.Parse(model.ShiYe_C) * 0.01M).ToString("#,##0.00");
            ShiYe_PJ.Text = (sjShiYe * shiye_p * 0.01M).ToString("#,##0.00");
            QShiYe_CJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.ShiYe_C) * 0.01M).ToString("#,##0.00");
            QShiYe_PJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * shiye_p * 0.01M).ToString("#,##0.00");

            ShiYe_CE.Text = (decimal.Parse(ShiYe_CJ.Text.Replace(",", "")) - decimal.Parse(QShiYe_CJ.Text.Replace(",", ""))).ToString("#,##0.00");

            //工伤
            decimal sjGongShang = decimal.Parse(JS.Value.Replace(",", ""));
            var gongshangModel = blist.Where(n => n.InsuranceType == "工伤").FirstOrDefault();
            if (gongshangModel != null)
            {
                if (gongshangModel.MinPrice == 0)
                {
                    sjGongShang = decimal.Parse(JS.Value.Replace(",", ""));
                }
                else if (decimal.Parse(txtPrice.Text) < gongshangModel.MinPrice)
                {
                    sjGongShang = gongshangModel.MinPrice;
                }
                if (decimal.Parse(txtPrice.Text) > gongshangModel.MaxPrice)
                {
                    sjGongShang = gongshangModel.MaxPrice;
                }
            }
            JS1.Text = sjGongShang.ToString("#,##0.00");

            GongShang_C.Text = QGongShang_C.Text = model.GongShang_C + "%";
            GongShang_P.Text = QGongShang_P.Text = model.GongShang_P + "%";
            GongShang_CJ.Text = (sjGongShang * decimal.Parse(model.GongShang_C) * 0.01M).ToString("#,##0.00");
            GongShang_PJ.Text = (sjGongShang * decimal.Parse(model.GongShang_P) * 0.01M).ToString("#,##0.00");
            QGongShang_CJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.GongShang_C) * 0.01M).ToString("#,##0.00");
            QGongShang_PJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.GongShang_P) * 0.01M).ToString("#,##0.00");

            GongShang_CE.Text = (decimal.Parse(GongShang_CJ.Text.Replace(",", "")) - decimal.Parse(QGongShang_CJ.Text.Replace(",", ""))).ToString("#,##0.00");

            //生育
            decimal sjShengYu = decimal.Parse(JS.Value.Replace(",", ""));
            var shengyuModel = blist.Where(n => n.InsuranceType == "生育").FirstOrDefault();
            if (shengyuModel != null)
            {
                if (shengyuModel.MinPrice == 0)
                {
                    sjShengYu = decimal.Parse(JS.Value.Replace(",", ""));
                }
                else if (decimal.Parse(txtPrice.Text) < shengyuModel.MinPrice)
                {
                    sjShengYu = shengyuModel.MinPrice;
                }
                if (decimal.Parse(txtPrice.Text) > shengyuModel.MaxPrice)
                {
                    sjShengYu = shengyuModel.MaxPrice;
                }
            }
            JS2.Text = sjShengYu.ToString("#,##0.00");

            ShengYu_C.Text = QShengYu_C.Text = model.ShengYu_C + "%";
            ShengYu_P.Text = QShengYu_P.Text = model.ShengYu_P + "%";
            ShengYu_CJ.Text = (sjShengYu * decimal.Parse(model.ShengYu_C) * 0.01M).ToString("#,##0.00");
            ShengYu_PJ.Text = (sjShengYu * decimal.Parse(model.ShengYu_P) * 0.01M).ToString("#,##0.00");
            QShengYu_CJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.ShengYu_C) * 0.01M).ToString("#,##0.00");
            QShengYu_PJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.ShengYu_P) * 0.01M).ToString("#,##0.00");

            ShengYu_CE.Text = (decimal.Parse(ShengYu_CJ.Text.Replace(",", "")) - decimal.Parse(QShengYu_CJ.Text.Replace(",", ""))).ToString("#,##0.00");

            //住房
            decimal sjZhuFang = decimal.Parse(JS.Value.Replace(",", ""));
            var zhufangModel = blist.Where(n => n.InsuranceType == "住房").FirstOrDefault();
            if (zhufangModel != null)
            {
                if (zhufangModel.MinPrice == 0)
                {
                    sjZhuFang = decimal.Parse(JS.Value.Replace(",", ""));
                }
                else if (decimal.Parse(txtPrice.Text) < zhufangModel.MinPrice)
                {
                    sjZhuFang = zhufangModel.MinPrice;
                }
                if (decimal.Parse(txtPrice.Text) > zhufangModel.MaxPrice)
                {
                    sjZhuFang = zhufangModel.MaxPrice;
                }
            }
            JS3.Text = sjZhuFang.ToString("#,##0.00");

            ZhuFang_C.Text = QZhuFang_C.Text = model.ZhuFang_C + "%";
            ZhuFang_P.Text = QZhuFang_P.Text = model.ZhuFang_P + "%";
            ZhuFang_CJ.Text = (sjZhuFang * decimal.Parse(model.ZhuFang_C) * 0.01M).ToString("#,##0.00");
            ZhuFang_PJ.Text = (sjZhuFang * decimal.Parse(model.ZhuFang_P) * 0.01M).ToString("#,##0.00");
            QZhuFang_CJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.ZhuFang_C) * 0.01M).ToString("#,##0.00");
            QZhuFang_PJ.Text = (decimal.Parse(QJS.Text.Replace(",", "")) * decimal.Parse(model.ZhuFang_P) * 0.01M).ToString("#,##0.00");

            ZhuFang_CE.Text = (decimal.Parse(ZhuFang_CJ.Text.Replace(",", "")) - decimal.Parse(QZhuFang_CJ.Text.Replace(",", ""))).ToString("#,##0.00");

            //医疗
            decimal sjYiLiao = decimal.Parse(JS.Value.Replace(",", ""));
            var yiliaoModel = blist.Where(n => n.InsuranceType == "医疗").FirstOrDefault();
            if (yiliaoModel != null)
            {
                if (yiliaoModel.MinPrice == 0)
                {
                    sjYiLiao = decimal.Parse(JS.Value.Replace(",", ""));
                }
                else if (decimal.Parse(txtPrice.Text) < yiliaoModel.MinPrice)
                {
                    sjYiLiao = yiliaoModel.MinPrice;
                }
                else if (decimal.Parse(txtPrice.Text) > yiliaoModel.MaxPrice)
                {
                    sjYiLiao = yiliaoModel.MaxPrice;
                }
            }
            JS4.Text = sjYiLiao.ToString("#,##0.00");
            YiLiao_C.Text = QYiLiao_C.Text = model.YiLiao_C + "%";
            decimal ZJ = model.YiLiao_P.Split('+').Length > 1 ? decimal.Parse(model.YiLiao_P.Split('+')[1]) : 0;
            YiLiao_P.Text = QYiLiao_P.Text = model.YiLiao_P.Split('+').Length > 1 ? (model.YiLiao_P.Split('+')[0] + "%" + "+" + model.YiLiao_P.Split('+')[1]) : model.YiLiao_P;
            YiLiao_CJ.Text = (sjYiLiao * decimal.Parse(model.YiLiao_C) * 0.01M).ToString("#,##0.00");
            YiLiao_PJ.Text = (sjYiLiao * decimal.Parse(model.YiLiao_P.Split('+').Length > 1 ? (model.YiLiao_P.Split('+')[0]) : model.YiLiao_P) * 0.01M + ZJ).ToString("#,##0.00");
            QYiLiao_CJ.Text = (decimal.Parse(QYLJS.Text.Replace(",", "")) * decimal.Parse(model.YiLiao_C) * 0.01M).ToString("#,##0.00");
            QYiLiao_PJ.Text = (decimal.Parse(QYLJS.Text.Replace(",", "")) * decimal.Parse(model.YiLiao_P.Split('+').Length > 1 ? (model.YiLiao_P.Split('+')[0]) : model.YiLiao_P) * 0.01M + ZJ).ToString("#,##0.00");

            YiLiao_CE.Text = (decimal.Parse(YiLiao_CJ.Text.Replace(",", "")) - decimal.Parse(QYiLiao_CJ.Text.Replace(",", ""))).ToString("#,##0.00");

            //合计
            HJ_CJ.Text = (decimal.Parse(YangLao_CJ.Text.Replace(",", "")) + decimal.Parse(ShiYe_CJ.Text.Replace(",", "")) + decimal.Parse(GongShang_CJ.Text.Replace(",", ""))
                            + decimal.Parse(ShengYu_CJ.Text.Replace(",", "")) + decimal.Parse(ZhuFang_CJ.Text.Replace(",", "")) + decimal.Parse(YiLiao_CJ.Text.Replace(",", ""))).ToString("#,##0.00");
            HJ_PJ.Text = (decimal.Parse(YangLao_PJ.Text.Replace(",", "")) + decimal.Parse(ShiYe_PJ.Text.Replace(",", "")) + decimal.Parse(GongShang_PJ.Text.Replace(",", ""))
                + decimal.Parse(ShengYu_PJ.Text.Replace(",", "")) + decimal.Parse(ZhuFang_PJ.Text.Replace(",", "")) + decimal.Parse(YiLiao_PJ.Text.Replace(",", ""))).ToString("#,##0.00");

            QHJ_CJ.Text = (decimal.Parse(QYangLao_CJ.Text.Replace(",", "")) + decimal.Parse(QShiYe_CJ.Text.Replace(",", "")) + decimal.Parse(QGongShang_CJ.Text.Replace(",", ""))
                + decimal.Parse(QShengYu_CJ.Text.Replace(",", "")) + decimal.Parse(QZhuFang_CJ.Text.Replace(",", "")) + decimal.Parse(QYiLiao_CJ.Text.Replace(",", ""))).ToString("#,##0.00");
            QHJ_PJ.Text = (decimal.Parse(QYangLao_PJ.Text.Replace(",", "")) + decimal.Parse(QShiYe_PJ.Text.Replace(",", "")) + decimal.Parse(QGongShang_PJ.Text.Replace(",", ""))
                + decimal.Parse(QShengYu_PJ.Text.Replace(",", "")) + decimal.Parse(QZhuFang_PJ.Text.Replace(",", "")) + decimal.Parse(QYiLiao_PJ.Text.Replace(",", ""))).ToString("#,##0.00");

            HJ_CE.Text = (decimal.Parse(HJ_CJ.Text.Replace(",", "")) - decimal.Parse(QHJ_CJ.Text.Replace(",", ""))).ToString("#,##0.00");

        }
    }
}
