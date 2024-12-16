<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Title="五险一金计算器" CodeBehind="PersonalInsuranceInfo.aspx.cs"
    Inherits="SEPAdmin.ServiceCenter.PersonalInsuranceInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/ServiceCenter/warefarestyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function selecttd(index) {
            var td0 = document.getElementById("td0");
            var td1 = document.getElementById("td1");
            var td2 = document.getElementById("td2")
            var td3 = document.getElementById("td3");
            var td4 = document.getElementById("td4");
            var td5 = document.getElementById("td5");
            var td6 = document.getElementById("td6");
            var td7 = document.getElementById("td7");
            var td8 = document.getElementById("td8");
            var td9 = document.getElementById("td9");
            var td10 = document.getElementById("td10");

            var tdcontent0 = document.getElementById("tdcontent0");
            var tdcontent1 = document.getElementById("tdcontent1");
            var tdcontent2 = document.getElementById("tdcontent2")
            var tdcontent3 = document.getElementById("tdcontent3");
            var tdcontent4 = document.getElementById("tdcontent4");
            var tdcontent5 = document.getElementById("tdcontent5");
            var tdcontent6 = document.getElementById("tdcontent6");
            var tdcontent7 = document.getElementById("tdcontent7");
            var tdcontent8 = document.getElementById("tdcontent8");
            var tdcontent9 = document.getElementById("tdcontent9");
            var tdcontent10 = document.getElementById("tdcontent10");

            var spanTitle = document.getElementById("spanTitle");

            tdcontent0.style.display = "none";
            tdcontent1.style.display = "none";
            tdcontent2.style.display = "none";
            tdcontent3.style.display = "none";
            tdcontent4.style.display = "none";
            tdcontent5.style.display = "none";
            tdcontent6.style.display = "none";
            tdcontent7.style.display = "none";
            tdcontent8.style.display = "none";
            tdcontent9.style.display = "none";
            tdcontent10.style.display = "none";

            td0.className = "menu";
            td1.className = "menu";
            td2.className = "menu";
            td3.className = "menu";
            td4.className = "menu";
            td5.className = "menu";
            td6.className = "menu";
            td7.className = "menu";
            td8.className = "menu";
            td9.className = "menu";
            td10.className = "menu";

            if (index == 0) {
                td0.className = "menu-a1";
                tdcontent0.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;意外险";
            }
            else if (index == 1) {
                td1.className = "menu-a1";
                tdcontent1.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;补充医疗";
            }
            else if (index == 2) {
                td2.className = "menu-a1";
                tdcontent2.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;节假日、生日祝福和礼物";
            }
            else if (index == 3) {
                td3.className = "menu-a1";
                tdcontent3.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;餐补";
            }
            else if (index == 4) {
                td4.className = "menu-a1";
                tdcontent4.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;电脑补贴";
            }
            else if (index == 5) {
                td5.className = "menu-a1";
                tdcontent5.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;年度体检";
            }
            else if (index == 6) {
                td6.className = "menu-a1";
                tdcontent6.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;年度OUTING";
            }
            else if (index == 7) {
                td7.className = "menu-a1";
                tdcontent7.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;生日会";
            }
            else if (index == 8) {
                td8.className = "menu-a1";
                tdcontent8.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;下午茶";
            }
            else if (index == 9) {
                td9.className = "menu-a1";
                tdcontent9.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;企业福利带薪假";
            }
            else if (index == 10) {
                td10.className = "menu-a1";
                tdcontent10.style.display = "block";
                spanTitle.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;长期服务奖励假";
            }
        }
    </script>

    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="70">&nbsp;
            </td>
        </tr>
        <tr>
            <td style="color: White;">由于各地社保、公积金政策及每年的社平工资不同，具体社保及公积金缴纳基数请以当地政策
为准.<br />
                <table border="0" cellspacing="0" cellpadding="0" style="margin-top: 7px;">
                    <tr>
                        <td style="color: White;">城市：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCity" runat="server" Style="border: solid 0px 
#b75400; height: 24px; width: 60px; font-size: 12px; color: #b75400; background-color: #f3de66;">
                                <asp:ListItem Text="北京" Value="北京" Selected="True" />
                                <asp:ListItem Text="上海" Value="上海" />
                                <asp:ListItem Text="广州" Value="广州" />
                                <asp:ListItem Text="长沙" Value="长沙" />
                                <asp:ListItem Text="重庆" Value="重庆" />
                                <asp:ListItem Text="南京" Value="南京" />
                            </asp:DropDownList>
                        </td>
                        <td style="color: White;">户口性质：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlHukou" runat="server" Style="border: solid 0px 
#b75400; height: 24px; width: 60px; font-size: 12px; color: #b75400; background-color: #f3de66;">
                                <asp:ListItem Text="城镇" Value="城镇" Selected="True" />
                                <asp:ListItem Text="农业" Value="农业" />
                            </asp:DropDownList>
                        </td>
                        <td style="padding-left: 10px; color: White;">税前工资：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice" runat="server" Style="background-image: url(images/input-bg.jpg);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Display="Static"
                                ControlToValidate="txtPrice"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator2" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtPrice"
                                    ValidationExpression="(\d)*"></asp:RegularExpressionValidator>
                        </td>
                        <td style="color: White;">元/月
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Button ID="btnView" runat="server" Width="78" Height="25"
                                Style="border: 0; cursor: pointer; background-image: url(images/btn.jpg);"
                                OnClick="btnView_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40">&nbsp;
            </td>
        </tr>
    </table>
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="95" rowspan="9" align="center">
                <strong>五险一金</strong>：<br />
                社保+公积金
            </td>
            <td height="20" valign="middle" colspan="6" align="center" class="colorA" style="font-size: 14px;">
                <strong>星言云汇<span style="font-size: 11px;">（严格按照劳动法规定的基数缴纳）                </span></strong>
            </td>
            <td>&nbsp;
            </td>
            <td colspan="5" align="center" class="colorB" style="font-size: 14px;">
                <strong>其他公司（<span style="font-size: 11px;">按照法定最低标准缴纳</span>）
                </strong>
            </td>
            <td>&nbsp;
            </td>
            <td rowspan="2" align="center" class="colorC">前两者由公司支付的差别
            </td>
            <td width="10" rowspan="2" align="center" class="colorC">&nbsp;
            </td>
        </tr>
        <tr>
            <td width="80" height="43" class="colorA">&nbsp;
            </td>
            <td width="82" align="center" class="colorA">
                <strong>缴费基数</strong>
            </td>
            <td width="63" align="center" class="colorA">
                <strong>公司<br />
                    支付比例</strong>
            </td>
            <td width="62" align="center" class="colorA">
                <strong>公司<br />
                    支付金额</strong>
            </td>
            <td width="63" align="center" class="colorA">
                <strong>个人<br />
                    支付比例</strong>
            </td>
            <td width="95" align="center" class="colorA">
                <strong>个人<br />
                    支付金额</strong>
            </td>
            <td width="7">&nbsp;
            </td>
            <td width="70" align="center" class="colorB">
                <strong>缴费基数</strong>
            </td>
            <td width="67" align="center" class="colorB">
                <strong>公司<br />
                    支付比例</strong>
            </td>
            <td width="75" align="center" class="colorB">
                <strong>公司<br />
                    支付金额</strong>
            </td>
            <td width="61" align="center" class="colorB">
                <strong>个人<br />
                    支付比例</strong>
            </td>
            <td width="85" align="center" class="colorB">
                <strong>个人<br />
                    支付金额</strong>
            </td>
            <td width="7">&nbsp;
            </td>
        </tr>
        <tr>
            <td height="33" align="center" class="paddingA">养老保险
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="JS9" /><asp:HiddenField ID="JS" runat="server" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YangLao_C" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YangLao_CJ" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YangLao_P" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YangLao_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QJS" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYangLao_C" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYangLao_CJ" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYangLao_P" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYangLao_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorC">
                <asp:Label runat="server" ID="YangLao_CE" />
            </td>
            <td class="colorC">&nbsp;
            </td>
        </tr>
        <tr>
            <td height="32" align="center" class="paddingA">失业保险
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="JS0" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShiYe_C" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShiYe_CJ" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShiYe_P" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShiYe_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorB">&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShiYe_C" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShiYe_CJ" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShiYe_P" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShiYe_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorC">
                <asp:Label runat="server" ID="ShiYe_CE" />
            </td>
            <td class="colorC">&nbsp;
            </td>
        </tr>
        <tr>
            <td height="33" align="center" class="paddingA">工伤保险
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="JS1" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="GongShang_C" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="GongShang_CJ" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="GongShang_P" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="GongShang_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorB">&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QGongShang_C" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QGongShang_CJ" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QGongShang_P" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QGongShang_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorC">
                <asp:Label runat="server" ID="GongShang_CE" />
            </td>
            <td class="colorC">&nbsp;
            </td>
        </tr>
        <tr>
            <td height="32" align="center" class="paddingA">生育保险
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="JS2" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShengYu_C" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShengYu_CJ" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShengYu_P" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ShengYu_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorB">&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShengYu_C" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShengYu_CJ" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShengYu_P" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QShengYu_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorC">
                <asp:Label runat="server" ID="ShengYu_CE" />
            </td>
            <td class="colorC">&nbsp;
            </td>
        </tr>
        <tr>
            <td height="33" align="center" class="paddingA">住房公积金
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="JS3" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ZhuFang_C" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ZhuFang_CJ" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ZhuFang_P" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="ZhuFang_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorB">&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QZhuFang_C" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QZhuFang_CJ" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QZhuFang_P" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QZhuFang_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorC">
                <asp:Label runat="server" ID="ZhuFang_CE" />
            </td>
            <td class="colorC">&nbsp;
            </td>
        </tr>
        <tr>
            <td height="32" align="center" class="paddingA">医疗保险
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="JS4" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YiLiao_C" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YiLiao_CJ" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YiLiao_P" />
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="YiLiao_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYLJS" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYiLiao_C" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYiLiao_CJ" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYiLiao_P" />
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QYiLiao_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorC">
                <asp:Label runat="server" ID="YiLiao_CE" />
            </td>
            <td class="colorC">&nbsp;
            </td>
        </tr>
        <tr>
            <td height="32" align="center" class="paddingA">合计：
            </td>
            <td align="center" class="colorA">&nbsp;
            </td>
            <td align="center" class="colorA">&nbsp;
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="HJ_CJ" />
            </td>
            <td align="center" class="colorA">&nbsp;
            </td>
            <td align="center" class="colorA">
                <asp:Label runat="server" ID="HJ_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorB">&nbsp;
            </td>
            <td align="center" class="colorB">&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QHJ_CJ" />
            </td>
            <td align="center" class="colorB">&nbsp;
            </td>
            <td align="center" class="colorB">
                <asp:Label runat="server" ID="QHJ_PJ" />
            </td>
            <td>&nbsp;
            </td>
            <td align="center" class="colorC">
                <asp:Label runat="server" ID="HJ_CE" />
            </td>
            <td class="colorC">&nbsp;
            </td>
        </tr>
    </table>
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="45">&nbsp;
            </td>
        </tr>
    </table>
    <table width="1030" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" style="padding: 13px 30px 0 50px; line-height: 20px;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="font-size: 12px; font-weight: bold; color: #632d00;">
                            <span id="spanTitle">&nbsp;&nbsp;&nbsp;&nbsp;意外险</span>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent0" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; color: White; display: block;">
                            <span style="font-weight: bolder;">在国家法律规定的社会保险之外,公司还
为每一位员工参加了商业意外险,理赔内容如下：</span><br />
                            <table width="100%" border="1" cellspacing="0" cellpadding="0"
                                style="border: solid gray; border-width: 1px 0px 0px 1px">
                                <tr align="center">
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">类别
                                    </td>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">险种
                                    </td>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">最高理赔金额
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="3" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">意外
                                    </td>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">意外身故
                                    </td>
                                    <td align="right" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">¥30,000.00&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">意外医疗
                                    </td>
                                    <td align="right" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">¥8,000.00&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">意外残疾
                                    </td>
                                    <td align="right" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">¥30,000.00&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="4" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">乘坐交通工具意外
                                    </td>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">民航客运飞机意外伤害
                                    </td>
                                    <td align="right" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">¥500,000.00
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">轨道客运车辆意外伤害
                                    </td>
                                    <td align="right" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">¥150,000.00
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">客运汽车意外伤害
                                    </td>
                                    <td align="right" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">¥50,000.00&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: solid gray; border-width: 0px 1px 1px 0px; color: White;">非营运汽车（自驾车）意外伤害
                                    </td>
                                    <td align="right" style="border: solid gray; border-width: 0px 
1px 1px 0px; color: White;">¥50,000.00&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent1" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">
                            <span style="font-weight: bolder;">在国家法律规定的社会保险之外,公司还
为每一位员工参加了商业补充医疗保障，如您入职后顺利的通过试用期评估，即可享有如下保障,补充医疗报销类
别及金额：</span>
                            <br />
                            门诊：在基本医疗保险范围内按100%报销，无免赔，无上限，如果员工在外地出
差期间生病看急诊费用，凭单位出具的出差证明和当地医院
                            <br />
                            住院：在基本医疗保险范围内按100%报销，无免赔，无上限
                            <br />
                            女员工生育：女员工分娩及孕期检查的费用，参照北京市生育保险规定的范围按
100%报销，最高限额8000元
                            <br />
                            同时附加,员工子女（0-18岁）补充医疗：
                            <br />
                            报销门/急诊医疗及住院费用按50%报销，门诊报销最高限额为2000元、住院报销
最高限额为20000元，报销项目限药费、输血费、手术费、检查费、化验费五项。
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent2" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">各类节假日，公司都会准备特别的礼物，并送上我们深深的祝福与关怀；
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent3" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">星言云汇员工享有每日15元餐补，OT另有补贴。
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent4" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">只要配置符合要求，您可以申请用自己的电脑，当您每个月工作日满15天，即可
享有300元电脑津贴。
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent5" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">每一年，公司会定时安排员工进行年度体检；
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent6" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">近到坝上，远至普吉，集体出游，分享欢乐，尽在年度Outing。
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent7" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">每个月，行政人事部都会为当月的寿星筹备快乐精彩的生日Party，为您送上这难
忘的一刻；
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent8" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">平凡工作日，精心下午茶，水果鲜切、蛋糕、饮料等小零食，
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent9" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">
                            <p>
                                星言云汇员工不仅享有年假，更享有企业福利带薪假（具体享受办法欢
迎咨询各团队BU HR）;
                            </p>
                            <p>
                                另外员工工作满一年即可增加一天企业福利带薪假；
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdcontent10" valign="top" style="padding: 10px 10px 0 10px; line-
height: 20px; display: none; color: White;">星言云汇员工连续工作满3年，5年，8年，就分别享有3天、5天、8天（工作日）的长
期服务奖励假，具体内容详见《员工手册》
                        </td>
                    </tr>
                </table>
            </td>
            <td width="240">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td id="td0" class="menu-a1" onclick="selecttd(0);" style="cursor: pointer;">意外险
                        </td>
                    </tr>
                    <tr>
                        <td id="td1" class="menu" onclick="selecttd(1);" style="cursor: pointer;">补充医疗
                        </td>
                    </tr>
                    <tr>
                        <td id="td2" class="menu" onclick="selecttd(2);" style="cursor: pointer;">节假日、生日祝福和礼物
                        </td>
                    </tr>
                    <tr>
                        <td id="td3" class="menu" onclick="selecttd(3);" style="cursor: pointer;">餐补
                        </td>
                    </tr>
                    <tr>
                        <td id="td4" class="menu" onclick="selecttd(4);" style="cursor: pointer;">电脑补贴
                        </td>
                    </tr>
                    <tr>
                        <td id="td5" class="menu" onclick="selecttd(5);" style="cursor: pointer;">年度体检
                        </td>
                    </tr>
                    <tr>
                        <td id="td6" class="menu" onclick="selecttd(6);" style="cursor: pointer;">年度OUTING
                        </td>
                    </tr>
                    <tr>
                        <td id="td7" class="menu" onclick="selecttd(7);" style="cursor: pointer;">生日会
                        </td>
                    </tr>
                    <tr>
                        <td id="td8" class="menu" onclick="selecttd(8);" style="cursor: pointer;">下午茶
                        </td>
                    </tr>
                    <tr>
                        <td id="td9" class="menu" onclick="selecttd(9);" style="cursor: pointer;">企业福利带薪假
                        </td>
                    </tr>
                    <tr>
                        <td id="td10" class="menu" onclick="selecttd(10);" style="cursor: pointer;">长期服务奖励假
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;
    </p>


    <table border="0" cellspacing="0" cellpadding="0" width="790">
        <tr>
            <td width="15%">&nbsp;</td>
            <td align="left">
                <p style="font-size: smaller; font-weight: bolder;">
                    集团人力资源部对以上内容拥有最终的解释权。
                </p>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
</asp:Content>
