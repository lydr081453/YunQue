<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Reporter.Master"
    CodeBehind="ReporterEdit.aspx.cs" Inherits="MediaWeb.newReporter.ReporterEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
        <tr>
            <td>
                <a href="#"></a>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="1">
                            <a href="#">
                                <asp:Image ID="uploadimage" runat="server" Width="190px" Height="130px" />
                                 <asp:FileUpload ID="ImageUpload" runat="server" Width="190px" />
                        </td>
                        <td style="padding-left: 20px;">
                            <span class="fontsize-36"><strong>
                                <asp:Label ID="labName" runat="server" /></strong></span><br />
                            <span class="fontsize-20">所属媒体：<asp:LinkButton ID="lnkMediaName" runat="server" /></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%" bgcolor="#fafafa" style="line-height: 22px; border: 1px solid #e6e6e6;">
                <table width="100%" border="0" cellpadding="5" cellspacing="0" class="fontsize-12">
                    <tr>
                        <td colspan="4" style="border-bottom: 1px dotted #CCC;">
                            基本信息
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            姓名：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtName" runat="server" MaxLength="30"></asp:TextBox><font color="red">
                                *</font>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            笔名：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtPenName" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            性别：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:DropDownList ID="ddlSex" runat="server" CssClass="fixddl" Width="60px">
                                <asp:ListItem Selected="True" Text="男" Value="1"></asp:ListItem>
                                <asp:ListItem Text="女" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            职务：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtReporterPosition" runat="server" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            负责领域：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtresponsibledomain" runat="server" MaxLength="100" />
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            办公电话：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            常用手机：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtUsualMobile" runat="server" MaxLength="50"></asp:TextBox>
                            <font color="red">*</font>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            备用手机：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtBackupMobile" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            Msn 号码：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtMsn" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            QQ 号码：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtQq" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            其它通讯方式：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtOtherMessageSoftware" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            电子邮件1：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtEmailOne" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="labEmailOne" runat="server">例如: aa@163.com</asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            电子邮件2：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtEmailTwo" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="labEmailTwo" runat="server">例如: aa@163.com</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            办公邮编：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtOfficePostID" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                        <td id="Td1" style="border-bottom: 1px dotted #CCC;">
                            传 真：
                        </td>
                        <td id="Td2" style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtFax" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="labFax" runat="server">例如: 010-88888888</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            办公地址：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtOfficeAddress" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="4">
                            个人信息
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            生日：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox runat="server" ID="txtBirthday"></asp:TextBox>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            身份证号：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtIdCard" runat="server" MaxLength="30"></asp:TextBox>
                            <asp:Label ID="labIdCard" runat="server">请正确输入身份证号码</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            籍贯：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtHometown" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            婚姻状况：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:DropDownList ID="ddlMarriage" runat="server" CssClass="fixddl" Width="60px">
                                <asp:ListItem Text="保密" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="未婚" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已婚" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭邮编：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtPostCord" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭电话：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="labHomePhone" runat="server">例如: 010-88888888</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭地址：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtAddress" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            性格特点：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtCharacter" runat="server" Height="80px" Width="80%" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                        </td>
                        </tr><tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            兴趣爱好：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtHobby" runat="server" Height="80px" Width="80%" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭成员：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtFamily" Height="80px" Width="80%" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            主要作品：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtWriting" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            教育背景：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtEducation" runat="server" Height="80px" TextMode="MultiLine"
                                Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            职业经历：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:TextBox ID="txtExperience" runat="server" Height="80px" TextMode="MultiLine"
                                Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" Style="background-image: url(/images/baocun.jpg);"
                                BorderWidth="0" Width="71" Height="32" OnClick="btnSave_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Style="background-image: url(/images/btn-return.gif);"
                                BorderWidth="0" Width="71" Height="32" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
