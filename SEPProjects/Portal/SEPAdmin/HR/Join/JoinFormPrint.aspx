<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JoinFormPrint.aspx.cs"
    Inherits="JoinFormPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="900" align="center" style="border-collapse: collapse;
            border: solid 1px black;">
            <tr>
                <td>
                    <img src="../../Images/xingyan.png" alt="xingyan" />
                </td>
                <td align="right">
                    星言云汇<br />
                </td>
            </tr>
             <tr>
                <td align="center" colspan="2">
                  &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <span style="font-size: 24pt; text-align: right; font-weight: bolder;">
                        星言云汇员工登记表 </span>
                </td>
            </tr>
             <tr>
                <td align="center" colspan="2">
                  &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="bottom" width="400" style="font-size: 10pt; font-weight: bold; padding: 0px 0px 5px 20px;
                    height: 30px;">
                    入职部门/职位：<span style="text-decoration: underline; margin: 0 5px 0px 5px;">
                        <asp:Label ID="labUserCode" runat="server" />
                    </span>
                </td>
                <td valign="bottom" width="250" style="font-size: 10pt; font-weight: bold; padding: 0px 0px 5px 0px;">
                    入职日期：<span style="text-decoration: underline; margin: 0 5px 0px 5px;"><asp:Label
                        ID="labJoinDate" runat="server" /></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="900" align="center">
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center" width="110px">
                                <span style="letter-spacing: 24pt;">姓</span>名
                            </td>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center" width="120px">
                                出生日期
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="center" width="120px">
                                <span style="letter-spacing: 24pt;">民</span>族
                            </td>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center" width="120px">
                                户口所在地
                            </td>
                            <td rowspan="8" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center" width="150px">
                                照片粘贴
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center">
                                <asp:Label ID="labUserName" runat="server" />&nbsp;
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="center">
                                <asp:Label ID="labBirthday" runat="server" />&nbsp;
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="center" width="120px">
                                <asp:Label ID="labPlaceOfBirth" runat="server" />&nbsp;
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="center">
                                <asp:Label ID="labDomicilePlace" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                毕业院校：<asp:Label ID="labFinishSchool" runat="server" />
                            </td>
                            <td colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                就读时间：
                                <asp:Label ID="labFinishSchoolDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                <span style="letter-spacing: 24pt;">学</span>历：
                                <asp:Label ID="labEducation" runat="server" />
                            </td>
                            <td colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                <span style="letter-spacing: 24pt;">专</span>业：<asp:Label ID="labSpeciality" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                紧急事件联系人：<asp:Label ID="labEmergencyLinkman" runat="server" />
                            </td>
                            <td height="35px" colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                紧急事件联系人电话：
                               <asp:Label ID="labEmergencyPhone" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                身份证号：<asp:Label ID="labIDNumber" runat="server" />
                            </td>
                            <td colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                健康状况：<asp:Label ID="labHealth" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                婚否/有无子女：<asp:Label ID="labMarriage" runat="server" />
                            </td>
                            <td colspan="2" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                配偶姓名：<asp:Label ID="labPeiOu" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                本人联系电话
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="left" colspan="3">
                                手机：
                                <asp:Label ID="labMobilePhone" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;宅电：<asp:Label
                                    ID="labHomePhone" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="Center">
                                户口性质
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                colspan="4" align="center">
                                <asp:Label ID="lblResidence" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center">
                                通讯地址
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                colspan="3" align="left">
                                <asp:Label ID="labAddress" runat="server" />&nbsp;
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="left">
                                邮编： <asp:Label ID="labPostCode" runat="server" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center">
                                本人现住址
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="left" colspan="3">
                                <asp:Label ID="labAddress1" runat="server" />&nbsp;
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="left">
                                邮编：
                                <asp:Label ID="labPostCode2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="105px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center">
                                家庭成员
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="left" colspan="4">
                                <asp:Label ID="lblFamilly" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="center">
                                工作特长
                            </td>
                            <td style="font-size: 12pt; font-weight: bold; border-collapse: collapse; border: solid 1px black;"
                                align="center" colspan="3">
                                <asp:Label ID="labWorkSpecialty" runat="server" />&nbsp;
                            </td>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left">
                                工作年限
                            </td>
                        </tr>
                          <tr>
                            <td height="105px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left" colspan="5">
                                工作简历：
                                <asp:Label ID="labWorkExperience" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left" colspan="5">
                                个人特长：
                                <asp:Label ID="Label6" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left" colspan="5">
                                1、个人特长、兴趣爱好：
                                <asp:Label ID="Label1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left" colspan="5">
                                2、参加过的培训：
                                <asp:Label ID="Label7" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left" colspan="5">
                                3、所获证书：
                                <asp:Label ID="Label8" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" align="left" colspan="5">
                                4、其他：
                                <asp:Label ID="labMemo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="90px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" valign="top" colspan="5">
                                在六个月内是否有严重的疾病或意外的事故，无/有，请详细说明：
                                <asp:Label ID="Label2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="90px" style="font-size: 12pt; font-weight: bold; border-collapse: collapse;
                                border: solid 1px black;" valign="top" colspan="5">
                                我承诺以上内容属实，并授权公司就此真实性实施调查，如有虚假内容，本人愿无条件接受辞退。<br />
                                <br />
                                <br />
                                <br />
                                <br />
                                员工签名：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期：
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="800" align="center" style="margin: 20px 0px 20px 0px;">
            <tr>
                <td height="20px">
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
    </form>
</body>
</html>
