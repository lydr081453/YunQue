<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_ItemList" Title="Untitled Page" Codebehind="ItemList.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
                <tr>
            <td colspan="4" class="oddrow-l"><input type="button" class="widebuttons" value="关闭" onclick="window.close();" /></td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                检索</td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                物料类别</td>
            <td class="oddrow-l" style="width: 30%">
                <asp:DropDownList ID="ddlproductType" runat="server" /></td>
            <td class="oddrow" style="width: 20%">
                物品名称</td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtproductName" runat="server" /></td>
        </tr>
        <tr>
            <td class="oddrow">
                物品种类</td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtproductClass" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text="检索" OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" id="tabTop" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageTop" runat="server">
                    <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                </asp:Panel>
            </td>
            <td align="right">
                记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                    runat="server" /></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvProduct" runat="server" CssClass="tableView" AutoGenerateColumns="False" OnRowCommand="gvProduct_RowCommand"
        DataKeyNames="id" PageSize="20" AllowPaging="True" Width="100%" OnPageIndexChanging="gvProduct_PageIndexChanging" OnRowDataBound="gvProduct_RowDataBound">
        <RowStyle CssClass="evenrowdata" />
        <Columns>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:Button ID="btnS" runat="server" Text="选择" CommandArgument='<%# Eval("id") %>' CssClass="widebuttons" CommandName="Add" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="productName" HeaderText="物品名称" />
            <asp:BoundField DataField="typename" HeaderText="物料类别" />
            <asp:BoundField DataField="productClass" HeaderText="物品种类" />
            <asp:BoundField DataField="suppliername" HeaderText="供应商名称" />
            </Columns>
        <SelectedRowStyle BackColor="#008A8C" />
        <HeaderStyle HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="oddrowdata" />
        <PagerSettings Visible="false" />
    </asp:GridView>
    <table width="100%" id="tabBottom" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageBottom" runat="server">
                    <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                </asp:Panel>
            </td>
            <td align="right">
                记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                    runat="server" /></td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l"><input type="button" class="widebuttons" value="关闭" onclick="window.close();" /></td>
        </tr>
    </table>
</asp:Content>

