<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BankInfo_BankList" Codebehind="BankList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <li><a href="BankEdit.aspx">添加银行信息</a></li>
    <br /><br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">银行名称:
            </td>
            <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtBankName" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">帐户名称:
            </td>
            <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtBankAccountName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">银行地址:
            </td>
            <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtAddress" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">分公司名称:
            </td>
            <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtBranchName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4" align="center">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
        <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">银行列表
            </td>
        </tr>
        <tr>
            <td class="oddrow">
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="BankID" OnRowCommand="gvList_RowCommand"
        PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
        OnPageIndexChanging="gvList_PageIndexChanging" AllowPaging="true" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="银行名称" DataField="BankName" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
            <asp:BoundField HeaderText="帐户名称" DataField="BankAccountName" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
            <asp:BoundField HeaderText="银行地址" DataField="Address" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%"/>
            <asp:BoundField HeaderText="银行电话" DataField="PhoneNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"/>
            <asp:BoundField HeaderText="分公司名称" DataField="BranchName" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%"/>
            <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <a href='BankView.aspx?<% =ESP.Finance.Utility.RequestName.BankID %>=<%# Eval("BankID")%>'>
                        <img src="../images/dc.gif" border="0px;" title="查看"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='BankEdit.aspx?<% =ESP.Finance.Utility.RequestName.BankID %>=<%# Eval("BankID")%>'>
                        <img src="../images/edit.gif" border="0px;" title="编辑"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("BankID") %>'
                        CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>"
                        OnClientClick="return confirm('你确定删除吗？');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
                            </td>
                    </tr>
                </table>
</asp:Content>
