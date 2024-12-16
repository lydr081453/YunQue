<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Client_ClientContentsList" Title="客户列表" Codebehind="ClientContentsList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table border="0" width="100%">
        <tr>
            <td class="headinglist" colspan="4">
                客户历史数据列表
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting"
                    DataKeyNames="id">
                </cc4:MyGridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: right">
                <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Text="返回" OnClick="btnBack_Click">
                </asp:Button>
                <asp:Button ID="btnClose" runat="server" CssClass="widebuttons" Text="关闭" OnClientClick="javascipt:window.close();return false;" />                  
            </td>
        </tr>
    </table>
</asp:Content>
