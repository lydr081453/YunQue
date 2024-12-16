<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferLetterPrint.aspx.cs"
    Inherits="OfferLetterPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .title
        {
            font-size: 12pt;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DefaultOfferLetter" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="650" align="center" style="line-height: 200%;">
            <tr>
                <td colspan="2" align="right">
                    <br />
                    <img id="imgShunya" src="~/Images/xingyan.png" runat="server" alt="xingyan" /><br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <p />
                    <span style="font-size: 15pt; font-weight: bold;">聘 用 任 职 信<p />
                        OFFER LETTER</span>
                </td>
            </tr>
            <tr>
                <td align="left" width="50%" valign="top">
                    <span style="font-size: 12pt; font-weight: bold;">致：</span><span style="font-size: 12pt;
                        font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labUserName1" runat="server" />
                    </span><span style="font-size: 12pt; font-weight: bold;">先生/女士</span>
                </td>
                <td>
                    <span style="font-size: 12pt; font-weight: bold;">身份证号：</span><span style="font-size: 12pt;
                        font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labIDNumber1" runat="server" />
                    </span>
                    <p />
                    <span style="font-size: 12pt; font-weight: bold;">英文名：</span><span style="font-size: 12pt;
                        font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labFullUserNameEN1" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt;">
                        <p />
                        <span style="margin: 0px 0px 0px 35px;">此信确认星言云汇同意聘请您加入我们公司。同时您确认您与其他公司没有劳动合同关系，并同意以下聘用条件：</span>
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">1．职位：</span><span style="font-size: 12pt;">您将在公司担任“<span
                        style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labPosition" runat="server" />
                    </span>”（<span style="font-size: 12pt; font-weight: bold; text-decoration: underline;
                        margin: 0 5px 0px 5px;">
                        <asp:Label ID="labGroupInfo" runat="server" />
                    </span>）一职。</span>
                    <p />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">2．试用期：</span><span style="font-size: 12pt;">
                        自您加盟之日(<span style="font-size: 12pt; font-weight: bold; text-decoration: underline;
                            margin: 0 5px 0px 5px;">
                            <asp:Label ID="labJoinDate" runat="server" />
                        </span>)起，您将开始为期3个月的试用期。试用期间，我们将每月对您进行试用评估，了解工作表现情况。</span><p />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">3．劳动合同：</span><span style="font-size: 12pt;">入职当月即与您签订三年固定期限的劳动合同（含试用期）。</span><p />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">4．薪酬：</span><span style="font-size: 12pt;">公司严格按照国家相关规定执行，所有报酬均须缴纳个人所得税。根据我们公司的薪资政策，员工薪酬构成包括：<br />
                        <span style="margin: 0px 0px 0px 80px">a）月基本工资（<asp:Label ID="labPreBasePay" runat="server" />%）+
                            月绩效工资（<asp:Label ID="labPreMeritPay" runat="server" />%）；<br />
                        </span><span style="margin: 0px 0px 0px 80px">b）第13个月双薪（发放双薪当月在职员工）；<br />
                        </span><span style="margin: 0px 0px 0px 80px">c）奖金，年底视公司效益、团队业绩和个人工作表现而决定。<br />
                        </span><span style="margin: 0px 0px 0px 35px">据此，您每月薪资包括税前基本工资<span style="font-size: 12pt;
                            font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                ID="labNowBasePay" runat="server" Width="50" /></span>元整，税前绩效工资<span style="font-size: 12pt;
                                    font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                        ID="labNowMeritPay" runat="server" Width="50" /></span>元整。另外，公司将为您代扣代缴个人所得税，并严格按照国家相关规定为您缴纳社保和住房公积金。<br />
                            <br />
                        </span><span style="margin: 0px 0px 0px 35px">期待着您与公司共同发展。同时，公司将尽力为您实现个人 事业目标提供一切必要的支持。<br />
                            <br />
                        </span><span style="margin: 0px 0px 0px 35px">您的其他福利与待遇将在员工合同中全面体现。同时，我们双方都同意，该聘用任职信所涵盖的必要内容将随着公司的发展与您实际工作中的表现进行相应调整。<br />
                            <br />
                        </span><span style="margin: 0px 0px 0px 35px">本聘用任职信所有内容有效期限为信函发出后3个自然日。请您在收到此信之后三个自然日内，给予电子邮件确认，并填写好所有相关的信息资料。人力资源部将提前为您的入职做好各项必要准备工作，谢谢您的合作。<br />
                            <br />
                        </span><span style="font-style: italic;">
                            <asp:Label ID="labMemo1" runat="server" /></span>
                        <br />
                        <%--<span style="font-style: italic;">另附自带笔记本的配置要求:<br />
                            处理器品牌：Intel<br />
                            处理器主频：Core i5、i7<br />
                            标准内存容量(MB)：4G
                            <br />
                            硬盘容量：250GB<br />
                            显卡类型： 集成、独立<br />
                            网卡：集成100/1000M局域以太网，802.11 ac/n无线网络<br />
                            预装 ：Windows专业版(公司安装)<br />
                            出厂日期：2014年1月1日以后出厂<br />
                            品牌范围 DELL(戴尔) 、   HP(惠普)、    Lenovo(联想)、 APPLE(苹果)、   TOSHIBA(东芝) 、   ASUS(华硕)<br />
                            <strong>公司配置电脑：笔记本电脑</strong><br />
                            IT部咨询电话：
                            <asp:Label ID="lblITPhone" runat="server"></asp:Label><br />
                        </span><span style="font-weight: bolder;">特别强调：请务必提前确认您的电脑配置符合公司要求，若因各种原因未能符合公司<br />
                            要求，在您到职后将无法及时安装局域网，由此将影响到您的正常工作。 </span>--%>

                                                                                  </span>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" width="50%" valign="top">
                    <div style="font-size: 12pt; margin: 0px 0px 0px 80px;">
                        星言云汇
                    </div>
                    <p />
                    <div style="font-size: 12pt; margin: 0px 0px 0px 80px;">
                        人力资源部</div>
                    <p />
                    <div style="font-size: 12pt; margin: 0px 0px 0px 80px;">
                        <asp:Label ID="labDateTime" runat="server" /></div>
                </td>
                <td align="center" width="50%" valign="top">
                    <span style="font-size: 12pt;">应聘方:<p />
                        （签字）</span>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 10px; color: Gray" align="center">
                    <br />
                    <br />
                    中国北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="1" height="20px">
                    &nbsp;
                </td>
                <td align="right">
                    <table border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                                class="white_font">
                                <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
                            </td>
                            <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                                class="white_font">
                                <a style="cursor: pointer" onclick="javascript:window.close();">关闭</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="InternOfferLetter" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="650" align="center" style="line-height: 200%;">
            <tr>
                <td colspan="2" align="right">
                    <br />
                    <img id="img1" src="~/Images/xingyan.png" runat="server" alt="xingyan" /><br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <p />
                    <span style="font-size: 15pt; font-weight: bold;">实习生聘用任职信<p />
                        OFFER LETTER</span>
                </td>
            </tr>
            <tr>
                <td align="left" width="50%" valign="top">
                    <span style="font-size: 12pt; font-weight: bold;">致：</span><span style="font-size: 12pt;
                        font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labUserName2" runat="server" />
                    </span><span style="font-size: 12pt; font-weight: bold;">先生/女士</span>
                </td>
                <td>
                    <span style="font-size: 12pt; font-weight: bold;">身份证号：</span><span style="font-size: 12pt;
                        font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labIDNumber2" runat="server" />
                    </span>
                    <p />
                    <span style="font-size: 12pt; font-weight: bold;">英文名：</span><span style="font-size: 12pt;
                        font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labFullUserNameEN2" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt;">
                        <p />
                        <span style="margin: 0px 0px 0px 35px;">此信确认星言云汇同意聘请您加入我们公司，并同意以下实习条件：</span>
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">1．职位：</span><span style="font-size: 12pt;">您将在公司担任“<span
                        style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        实习生 </span>”（<span style="font-size: 12pt; font-weight: bold; text-decoration: underline;
                            margin: 0 5px 0px 5px;"> Intern </span>）一职。</span>
                    <p />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">2．实习期：</span><span style="font-size: 12pt;">
                        自您加盟之日(<span style="font-size: 12pt; font-weight: bold; text-decoration: underline;
                            margin: 0 5px 0px 5px;">
                            <asp:Label ID="Label2" runat="server" />
                        </span>)起至毕业证发放之日止，入职当日即与您签订实习合同。实习期间，每周保证至少<span style="font-size: 12pt; font-weight: bold;
                            text-decoration: underline; margin: 0 5px 0px 5px;">四</span>天全职工作。</span><p />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">3．薪酬福利：</span><span style="font-size: 12pt;">在实习期间，您每天实习报酬为税前人民币<span
                        style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                            ID="Label3" runat="server" /></span>元整。</span><p />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt;"><span style="margin: 0px 0px 0px 35px">在实习期间，公司为您购买人身意外伤害保险。此外，不享受其他待遇（如社保，带薪休假，休假或餐补等其他任何保险福利）。<br />
                        <br />
                    </span><span style="margin: 0px 0px 0px 35px">实习期间，如遇不可抗力，或经营原因(如业务结构改变等)，或是由于能力达不到公司的要求，公司有权根据具体情况终止实习协议，并不予任何补偿。如您不能继续在公司实习，需要提前三天通知公司。<br />
                        <br />
                    </span><span style="margin: 0px 0px 0px 35px">本聘用任职信所有内容有效期限为信函发出后3个自然日。请您在收到此信之后三个自然日内，给予电子邮件确认，并填写好所有相关的信息资料。人力资源部将提前为您的入职做好各项必要准备工作，谢谢您的合作。<br />
                        <br />
                    </span><span style="font-style: italic;">
                        <asp:Label ID="labMemo2" runat="server" /></span> </span>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" width="50%" valign="top">
                    <div style="font-size: 12pt; margin: 0px 0px 0px 80px;">
                        星言云汇
                    </div>
                    <p />
                    <div style="font-size: 12pt; margin: 0px 0px 0px 80px;">
                        人力资源部</div>
                    <p />
                    <div style="font-size: 12pt; margin: 0px 0px 0px 80px;">
                        <asp:Label ID="Label5" runat="server" /></div>
                </td>
                <td align="center" width="50%" valign="top">
                    <span style="font-size: 12pt;">应聘方:<p />
                        （签字）</span>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 10px; color: Gray" align="center">
                    <br />
                    <br />
                    中国北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="1" height="20px">
                    &nbsp;
                </td>
                <td align="right">
                    <table border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50" id="Td1" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                                class="white_font">
                                <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
                            </td>
                            <td width="50" id="Td2" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                                class="white_font">
                                <a style="cursor: pointer" onclick="javascript:window.close();">关闭</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    </form>
</body>
</html>
