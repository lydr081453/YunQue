<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InternAuditOfferLetter.aspx.cs"
    Inherits="InternAuditOfferLetter" %>

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
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="650" align="center" style="line-height: 200%;">
            <tr>
                <td colspan="2" align="left">
                  <span style="font-size: 15pt; font-weight: bold;">有新的入职员工需要您审核,以下是实习生聘用任职信</span>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <br />
                    <img id="imgShunya" runat="server" alt="xingyan" /><br />
                    <span style="font-size: 9px;">星言云汇<br /></span>
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
                    <span style="font-size: 15pt; font-weight: bold;">实习生聘用任职信
                   <p />
                        OFFER LETTER</span>
                </td>
            </tr>
            <tr>
                <td align="left" width="50%" valign="top">
                    <span style="font-size: 12pt; font-weight: bold;">致：</span><span style="font-size: 12pt;
                        font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labUserName" runat="server" />
                    </span><span style="font-size: 12pt; font-weight: bold;">先生/女士</span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt;"><span style="margin: 0px 0px 0px 35px;">此信确认星言云汇同意聘请您加入我们公司，并同意以下实习条件：</span>
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
                            <asp:Label ID="labJoinDate" runat="server" />
                        </span>)起至毕业证发放之日止，入职当日即与您签订实习合同。实习期间，每周保证至少<span style="font-size: 12pt; font-weight: bold;
                            text-decoration: underline; margin: 0 5px 0px 5px;">五</span>天全职工作。</span><p />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 12pt; font-weight: bold;">3．薪酬福利：</span><span style="font-size: 12pt;">在实习期间，您每月实习报酬为税前人民币<span
                        style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                            ID="labNowBasePay" runat="server" /></span>元整。</span><p />
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
                        <asp:Label ID="labMemo" runat="server" /></span> </span>
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
                <%-- <td align="center" width="50%" valign="top">
                    <span style="font-size: 12pt;">应聘方:<p />
                        （签字）</span>
                </td>--%>
            </tr>
            <%--<tr>
                <td align="left" colspan="2" valign="top">
                    <p />
                    <span style="color: Red; font-size: 12pt; font-weight: bold;">请<a id="linkOk" runat="server"
                        style="font-size: 12pt; font-weight: bold;">点击</a>下面的链接确认入职，并填写相关信息。</span>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2" style="font-size: 10px; color: Gray" align="center">
                    <br />
                    <br />
                    北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼（请着正装）
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
