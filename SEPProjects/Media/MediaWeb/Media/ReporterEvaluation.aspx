<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ReporterEvaluation.aspx.cs" Inherits="MediaWeb.Media.ReporterEvaluation" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
<table style="width: 100%" border="1" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                基本信息：
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                姓名：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labName" runat="server" ForeColor="blue" Font-Size="12"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                笔名：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labPenName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                所属媒体名称：
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:LinkButton ID="lnkMediaName" runat="server" />
            </td>            
        </tr>
        <tr>
            <td class="oddrow">
                性别：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labSex" runat="server" />
            </td>
            <td class="oddrow">
                职务：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labreporterposition" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                负责领域：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labresponsibledomain" runat="server" />
            </td>
            <td class="oddrow">
                固话：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labOfficePhone" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                常用手机：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labUsualMobile" runat="server" />
            </td>
            <td class="oddrow">
                备用手机：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labBackupMobile" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                Msn 号码：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labMsn" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                QQ 号码：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labQq" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                其它通讯方式：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labOtherMessageSoftware" runat="server" MaxLength="50"></asp:Label>
            </td>
            <td class="oddrow">
                邮箱：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmailOne" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                备用邮箱1：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmailTwo" runat="server" MaxLength="50"></asp:Label>
            </td>
            <td class="oddrow">
                备用邮箱2：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmailThree" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                办公邮编：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblOfficePostID" runat="server" MaxLength="50"></asp:Label>
            </td>
            <td class="oddrow">
                传真：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblFax" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                办公地址：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblOfficeAddress" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                评价：
            </td>
            <td class="oddrow-l" colspan="3">
                <CKEditor:CKEditorControl ID="txtEvaluation" runat="server" ></CKEditor:CKEditorControl>
                <br /><asp:Label ID="labEvaluation" runat="server" />
            </td>
        </tr>
        <tr>
                        <td class="oddrow">
                修订原因：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="80%" Height="80px" />
            </td>
        </tr>
        </table>
            <table style="width:100%">
        <tr>
            <td align="right" colspan="4">
                <asp:Button ID="btnSave"
                    runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
