<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailClosingList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.IT.EmailClosingList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">关键字:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>&nbsp;
            </td>
             <td class="oddrow" style="width: 10%">状态:
            </td>
            <td class="oddrow-l" >
                <asp:DropDownList runat="server" ID="ddlStatus">
                    <asp:ListItem Text="未关闭" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="已关闭" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="NameCN" HeaderText="离职员工" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="Email" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="DeptName" HeaderText="所属部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="KeepDate" HeaderText="保留日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                        <asp:TemplateField HeaderText="确认" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblClose"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
