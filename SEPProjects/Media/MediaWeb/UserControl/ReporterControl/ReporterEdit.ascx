<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_ReporterControl_ReporterEdit" EnableViewState="true" Codebehind="ReporterEdit.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<table style="width: 100%" border="0">
    <tr>
        <td style="width: 100%">
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%" border="1" class="tableForm">
                <tr>
                    <td class="heading" colspan="4">
                        基本信息
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" style="width: 20%">
                        姓名：
                    </td>
                    <td class="oddrow-l" style="width: 30%">
                        <asp:TextBox ID="txtName" runat="server" MaxLength="30"></asp:TextBox><font color="red">
                            * </font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                            ValidationGroup="SaveReporter" ErrorMessage="必填" />
                        <asp:HiddenField ID="hidReporterID" runat="server" Value="" />
                    </td>
                    <td class="oddrow" style="width: 20%">
                        笔名：
                    </td>
                    <td class="oddrow-l" style="width: 30%">
                        <asp:TextBox ID="txtPenName" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        所属媒体名称：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtMediaName" runat="server" Enabled="false" /><font color="red"> *
                        </font>
                        <asp:HiddenField ID="hidMedia" runat="server" />
                        &nbsp;
                        <asp:Button Text="变更所属媒体" ID="btnMediaSelect" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMediaName"
                            ValidationGroup="SaveReporter" ErrorMessage="必填" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        性别：
                    </td>
                    <td class="oddrow-l">
                        <asp:DropDownList ID="ddlSex" runat="server" CssClass="fixddl" Width="60px">
                            <asp:ListItem Selected="True" Text="男" Value="1"></asp:ListItem>
                            <asp:ListItem Text="女" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="oddrow">
                        职务：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtReporterPosition" runat="server" MaxLength="50" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        负责领域：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtresponsibledomain" runat="server" MaxLength="100" />
                    </td>
                    <td class="oddrow">
                        办公电话：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labOfficePhone" runat="server">例如: 010-88888888</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        常用手机：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtUsualMobile" runat="server" MaxLength="50"></asp:TextBox>
                        <font color="red">* </font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUsualMobile"
                            ValidationGroup="SaveReporter" ErrorMessage="必填" />
                        <br />
                        <asp:Label ID="labUsualMobile" runat="server">例如: 13088888888</asp:Label>
                    </td>
                    <td class="oddrow">
                        备用手机：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtBackupMobile" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labBackupMobile" runat="server">例如: 13688888888</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        Msn 号码：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtMsn" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labMsn" runat="server">例如: aa@163.com</asp:Label>
                    </td>
                    <td class="oddrow">
                        QQ 号码：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtQq" runat="server" MaxLength="20"></asp:TextBox><br />
                        <asp:Label ID="labQq" runat="server">请正确输入QQ 号码</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        其它通讯方式：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtOtherMessageSoftware" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        电子邮件1：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtEmailOne" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labEmailOne" runat="server">例如: aa@163.com</asp:Label>
                    </td>
                    <td class="oddrow">
                        电子邮件2：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtEmailTwo" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labEmailTwo" runat="server">例如: aa@163.com</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        电子邮件3：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtEmailThree" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labEmailThree" runat="server">例如: aa@163.com</asp:Label>
                    </td>
                    <td id="Td1" class="oddrow">
                        传 真：
                    </td>
                    <td id="Td2" class="oddrow-l">
                        <asp:TextBox ID="txtFax" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labFax" runat="server">例如: 010-88888888</asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%" border="0">
                <tr>
                    <td colspan="4" style="border: 0px; height: 30px">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%" border="1" class="tableForm">
                <tr>
                    <td class="heading" colspan="4">
                        个人信息
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" style="width: 20%">
                        生日：
                    </td>
                    <td class="oddrow-l" style="width: 30%">
                     <cc2:DatePicker ID="dpBirthday" runat="server"></cc2:DatePicker>
                    </td>
                    <td class="oddrow">
                        身份证号：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtIdCard" runat="server" MaxLength="30"></asp:TextBox><br />
                        <asp:Label ID="labIdCard" runat="server">请正确输入身份证号码</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        籍贯：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtHometown" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td class="oddrow">
                        婚姻状况：
                    </td>
                    <td class="oddrow-l">
                        <asp:DropDownList ID="ddlMarriage" runat="server" CssClass="fixddl" Width="60px">
                            <asp:ListItem Text="保密" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="未婚" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已婚" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        性格特点：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtCharacter" runat="server" MaxLength="200"></asp:TextBox>
                    </td>
                    <td class="oddrow">
                        兴趣爱好：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtHobby" runat="server" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        家庭邮编：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtPostCord" runat="server" MaxLength="10"></asp:TextBox><br />
                        <asp:Label ID="labPostCord" runat="server">例如: 100000</asp:Label>
                    </td>
                    <td class="oddrow">
                        家庭电话：
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="50"></asp:TextBox><br />
                        <asp:Label ID="labHomePhone" runat="server">例如: 010-88888888</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        家庭地址：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtAddress" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        家庭成员：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtFamily" Height="137px" Width="80%" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        个人照片：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:FileUpload ID="ImageUpload" runat="server" Width="80%" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        主要作品：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtWriting" runat="server" Height="137px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        教育背景：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtEducation" runat="server" Height="137px" TextMode="MultiLine"
                            Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        职业经历：
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtExperience" runat="server" Height="137px" TextMode="MultiLine"
                            Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <asp:HiddenField ID="hidBackUrl" runat="server" />
                <asp:HiddenField ID="hidRid" runat="server" />
            </table>
        </td>
    </tr>
  
</table>