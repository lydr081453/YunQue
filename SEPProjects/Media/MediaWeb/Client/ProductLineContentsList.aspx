<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Title="产品线历史数据" Inherits="Client_ProductLineContentsList" Codebehind="ProductLineContentsList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table border="0" width="100%">
        <tr>
            <td class="headinglist" colspan="4">
                产品线历史数据列表
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" DataKeyNames="id">
                </cc4:MyGridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: right">
            <asp:button id="btnBack" runat="server" CssClass="widebuttons" Text="返回"  OnClick="btnBack_Click" ></asp:button>
                <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                    class="widebuttons" />
                
            </td>
        </tr>
    </table>
</asp:Content>
