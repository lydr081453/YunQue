<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferLetter.aspx.cs" Inherits="OfferLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .title {
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
                    <td colspan="2" align="right">
                        <br />
                        <img id="imgShunya" runat="server" alt="xingyan" /><br />
                        <span style="font-size: 9px;">星言云汇<br />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <span style="font-size: 15pt; font-weight: bold;">录 用 通 知 书
                        OFFER LETTER</span>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="50%" valign="top">
                        <span style="font-size: 12pt; font-weight: bold;">尊敬的：</span><span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                            <asp:Label ID="labUserName" runat="server" />
                        </span><span style="font-size: 12pt; font-weight: bold;">先生/小姐</span>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="font-size: 12pt;">
                            <p />
                            <span style="margin: 0px 0px 0px 35px;">北京星声场网络科技有限公司很高兴地通知您，您已经通过了公司的笔试/面试考核，公司拟录用您为正式员工并拟与您签订正式劳动合同。欢迎您加入公司 <span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                                <asp:Label ID="labGroupInfo" runat="server" />
                            </span>，任 <span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                                <asp:Label ID="labPosition" runat="server" />
                            </span>职位，目前您的汇报对象是  <asp:Label ID="lblLeader" runat="server" /> 。您入职后的工作职责请与您的直属上级确认。</span>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="margin: 0px 0px 0px 35px;font-weight:bold">您入职后的薪酬待遇：</span><br />
                        <span style="margin: 0px 0px 0px 35px;">您转正后的税前月固定工资是人民币<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                            ID="labZSze" runat="server" /></span>元整（包含基本工资<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                ID="labZSjb" runat="server" /></span>元；岗位薪资<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                    ID="labZSgw" runat="server" /></span>元；考勤绩效<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                        ID="labZSkq" runat="server" /></span>元），全年 12 个月薪资，试用期薪资为人民币<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                            ID="labSYze" runat="server" /></span>元整（包含基本工资<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                                ID="labSYjb" runat="server" /></span>元；岗位薪资<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                                    ID="labSYgw" runat="server" /></span>元；考勤绩效<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                                                        ID="labSYkq" runat="server" /></span>元），试用期<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">3</span>个月。公司将从您的月工资中按国家劳动法规定代扣您个人所得税的个人缴纳部分。</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="margin: 0px 0px 0px 35px;font-weight:bold">您入职后的福利待遇：</span><br />
                        <span style="margin: 0px 0px 0px 35px;">社会福利：按照国家及地方政府规定缴纳的养老保险、失业保险、基本医疗保险、工伤保险、生育保险、住房公积金，公司将从您的月工资中代扣个人应缴部分；</span><br />
                        <span style="margin: 0px 0px 0px 35px;">公司福利：带薪休假、生日礼物等。</span>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="margin: 0px 0px 0px 35px;font-weight:bold">您入职后的年度薪酬调整：</span><br />
                        <span style="margin: 0px 0px 0px 35px;">公司每年将结合市场薪酬水平、岗位调整、个人绩效考核的结果进行薪酬调整。</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="margin: 0px 0px 0px 35px;font-weight:bold">本通知书的确认及报到</span><br />
                        <span style="margin: 0px 0px 0px 35px;">请您于<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                            <asp:Label ID="labJoinDate" runat="server" />
                        </span>前回复确认接受此录用通知书并于<span style="font-size: 12pt; font-weight: bold; text-decoration: underline; margin: 0 5px 0px 5px;">
                            <asp:Label ID="labJoinDate1" runat="server" />
                        </span>携带已签字确认的录用通知书到公司报到。第一天入职时，您需要携带以下文件：</span>
                        <ul>
                            <li>离职证明原件（需加盖人事部门章或者公司公章）</li>
                            <li>身份证（原件及复印件）</li>
                            <li>毕业证和学位证（原件及复印件）</li>
                            <li>一寸照片：近期免冠白底彩色照片</li>
                            <li>本人招商银行或宁波银行储蓄卡一类卡账号复印件（北京工资卡）具体银行卡请与HR联系</li>
                            <li>本人工商银行储蓄卡一类卡账号复印件（重庆工资卡）</li>
                            <li>近三月入职体检报告</li>
                        </ul>
                        <span style="margin: 0px 0px 0px 35px;">逾期未确认接受此录用通知书或逾期未报到，以及报到时不能提供上述真实有效文件的，本通知书自始不发生法律效力。</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="margin: 0px 0px 0px 35px;font-weight:bold">其他说明：</span><br />
                        1.	工作时间：公司的工作时间为9:30至18:30。<br />
                        2.	您有义务对您的薪资内容保密，不将其告知第三方。<br />
                        3.	办公地点：<asp:Label ID="labAddress" runat="server" /><br />
                        4.	如您接受本聘书，请回复人力资源部，以方便公司尽快为您做入职准备。<br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="margin: 0px 0px 0px 35px;font-weight:bold">特别提示：</span><br />
                        <span style="margin: 0px 0px 0px 35px;">本通知书的生效是以您向我公司提供的信息全面、真实为前提。您对本通知书的确认并不表示双方劳动关系的建立，只有在您按照本通知书的要求报道、提供的上述材料真实有效的前提下，才具备签订劳动合同的条件，双方劳动关系的建立以及具体的权利义务最终将以我公司与您签订的书面劳动合同为准。如果您向我公司提供的学历、工作经历等信息不真实，或向我公司隐瞒了以前的不良记录（包括但不限于信用记录、违法记录等），或未向我公司披露与其他雇主尚未解除的劳动关系，或对前雇主仍然负有竞业限制等义务，本通知书自始不发生法律效力且公司有权立即解除与您的劳动合同。</span><br />
                        <span style="margin: 0px 0px 0px 35px;">本通知书中的内容属我公司机密，不得向第三方透露，否则也将导致本通知书的失效。</span><br />
                        <span style="margin: 0px 0px 0px 35px;">我们真诚欢迎您的加入，我们期待与您共同成长。</span><br />
                        <span style="margin: 0px 0px 0px 35px;">若有任何疑问，请随时来电或发送邮件提出。</span><br />
                        <span style="margin: 0px 0px 0px 35px;">Mobile：<asp:Label ID="labMobile" runat="server" /> </span><br />
                        <span style="margin: 0px 0px 0px 35px;">Email：<asp:Label ID="labEmail" runat ="server" /> </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                                                    <span style="font-style: italic;">
                                <asp:Label ID="labMemo" runat="server" /></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">北京星声场网络科技有限公司<br />
                        日期： <asp:Label ID="labDateTime" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">本人确认接受本录用通知书的职位及与其相关的上述所列的条款与条件。<span style="color: Red; font-size: 12pt; font-weight: bold;">请<a id="linkOk" runat="server" style="font-size: 12pt; font-weight: bold;">点击</a>下面的链接确认入职，并填写相关信息。</span>
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
