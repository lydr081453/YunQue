<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BankInfo_BankView" Codebehind="BankView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="tableForm">
    <tr>
        <td colspan="4" class="heading" >分公司信息</td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">公司名称:</td>
        <td class="oddrow-l" style="width:30%"><asp:Label ID="txtBranchName" runat="server" /></td>
        <td class="oddrow" style="width:20%">公司代码:</td>
        <td class="oddrow-l" style="width:30%"><asp:Label ID="txtBranchCode" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">描述:</td>
        <td class="oddrow-l"  colspan="3"><asp:Label ID="labDes" runat="server" /></td>
    </tr>
</table>
<table width="100%" class="tableForm">
    <tr>
        <td colspan="4" class="heading">银行信息</td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">数据库代码:</td>
        <td class="oddrow-l" style="width:30%"><asp:Label ID="txtDBCode" runat="server" /></td>
        <td class="oddrow" style="width:20%">管理数据库:</td>
        <td class="oddrow-l" style="width:30%"><asp:Label ID="txtDBManager" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">银行名称:</td>
        <td class="oddrow-l"><asp:Label ID="txtBankName" runat="server" /></td>
        <td class="oddrow">帐户名称:</td>
        <td class="oddrow-l"><asp:Label ID="txtBankAccountName" runat="server" /></td>
    </tr>
        <tr>
        <td class="oddrow">帐号:</td>
        <td class="oddrow-l"><asp:Label ID="txtBankAccount" runat="server" /></td>
        <td class="oddrow">地址:</td>
        <td class="oddrow-l"><asp:Label ID="txtAddress" runat="server" /></td>
    </tr>
        <tr>
        <td class="oddrow">银行电话:</td>
        <td class="oddrow-l"><asp:Label ID="txtPhoneNo" runat="server" /></td>
        <td class="oddrow">交换行号:</td>
        <td class="oddrow-l"><asp:Label ID="txtExchangeNo" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">银行查询电话:</td>
        <td class="oddrow-l"  colspan="3"><asp:Label ID="txtRequestPhone" runat="server" /></td>
    </tr>
</table>
<table width="100%" class="XTable">
    <tr>
        <td><asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
        </td>
    </tr>
</table>
</asp:Content>

