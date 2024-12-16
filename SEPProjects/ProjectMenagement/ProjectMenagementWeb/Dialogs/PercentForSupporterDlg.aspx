<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Title="预计完成百分比" CodeBehind="PercentForSupporterDlg.aspx.cs" Inherits="ProjectMenagementWeb.Dialogs.PercentForSupporterDlg" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" class="tableForm" id="tbMain">
        <tr>
            <td class="heading">
                各月完工百分比
                <input type="hidden" runat="server" id="hidYear" />
                <input type="hidden" runat="server" id="hidMonth" />
                <input type="hidden" runat="server" id="hidPercent" />
            </td>
        </tr>        
        <tr>
            <td class="heading">
                <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AutoGenerateColumns="false" DataKeyNames="SupporterScheduleID"  Width="100%" CellPadding="4">
                    <Columns>
                        <asp:TemplateField HeaderText="年" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="txtYear" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="月" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="txtMonth" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="百分比%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPercent" runat="server" OnTextChanged="txtPercent_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Fee" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFee" runat="server" OnTextChanged="txtFee_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" id="Table1">
        <tr>
            <td class="oddrow" width="15%">
                总计:
            </td>
            <td class="oddrow-l" width="32%" align="center">
                <asp:Label ID="lblTotal" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
            </td>
            <td class="oddrow-l" width="32%"  align="center">
            <asp:Label ID="lblTotalFee" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
            </td>
        </tr>
          <tr>
            <td class="oddrow" width="15%">
                总计比对:
            </td>
            <td class="oddrow-l" width="32%" align="center">
                <asp:Label ID="lblPercent2" runat="server" Font-Size="Larger" ></asp:Label>
            </td>
            <td class="oddrow-l" width="32%" align="center">
                <asp:Label ID="lblTotalFee2" runat="server" Font-Size="Larger" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnAutoCreate" runat="server" Text="平均分配未填百分比" CssClass="widebuttons" Visible="false" OnClick="btnAutoCreate_Click" />
                <asp:Button ID="btnNewSupporter" runat="server" Text="保存" class="widebuttons" 
                    OnClick="btnNewSupporter_Click" />
                <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="widebuttons" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>