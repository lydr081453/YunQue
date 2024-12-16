<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Reporter.Master" CodeBehind="ReporterHistView.aspx.cs" Inherits="MediaWeb.newReporter.ReporterHistView" %>

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
                           <asp:Label runat="server" ID="lblReproterName"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            笔名：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblPenName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            性别：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblSex"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            职务：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                              <asp:Label runat="server" ID="lblPosition"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            负责领域：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                              <asp:Label runat="server" ID="lblDomain"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            办公电话：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                              <asp:Label runat="server" ID="lblOfficePhone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            常用手机：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                              <asp:Label runat="server" ID="lblUsualMobile"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            备用手机：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                              <asp:Label runat="server" ID="lblBackupMobile"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            Msn 号码：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblMsn"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            QQ 号码：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:Label runat="server" ID="lblQQ"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            其它通讯方式：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                             <asp:Label runat="server" ID="lblOtherPhone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            电子邮件1：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblEmail1"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            电子邮件2：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                           <asp:Label runat="server" ID="lblEmail2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            办公邮编：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblOfficePost"></asp:Label>
                        </td>
                        <td id="Td1" style="border-bottom: 1px dotted #CCC;">
                            传 真：
                        </td>
                        <td id="Td2" style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblFax"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            办公地址：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                             <asp:Label runat="server" ID="lblOfficeAddress"></asp:Label>
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
                            <asp:Label runat="server" ID="lblBirthday"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            身份证号：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:Label runat="server" ID="lblIdCard"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            籍贯：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblHomeTown"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            婚姻状况：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            <asp:Label runat="server" ID="lblMarrige"></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭邮编：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblHomePost"></asp:Label>
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭电话：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;">
                             <asp:Label runat="server" ID="lblHomePhone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭地址：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                              <asp:Label runat="server" ID="lblHomeAddress"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            性格特点：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                               <asp:Label runat="server" ID="lblCharacter"></asp:Label>
                        </td>
                        </tr><tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            兴趣爱好：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                               <asp:Label runat="server" ID="lblHobby"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            家庭成员：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                           <asp:Label runat="server" ID="lblFamilly"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            主要作品：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                           <asp:Label runat="server" ID="lblWritting"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            教育背景：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                           <asp:Label runat="server" ID="lblEducation"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px dotted #CCC;">
                            职业经历：
                        </td>
                        <td style="border-bottom: 1px dotted #CCC;" colspan="3">
                            <asp:Label runat="server" ID="lblExperience"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Style="background-image: url(/images/btn-return.gif);"
                                BorderWidth="0" Width="71" Height="32" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
