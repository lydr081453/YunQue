<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CheckInfo_CheckList" Codebehind="CheckList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <li>
                                <asp:LinkButton ID="lbNewCheck" runat="server" Text="登记新支票" OnClick="lbNewCheck_Click" /></li>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        票号区间:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtBegin" runat="server" />--<asp:TextBox ID="txtEnd" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        支票状态:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlStatus">
                                            <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="可用" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="已用" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="作废" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="oddrow" colspan="2" align="right">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                        支票列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvCheck" runat="server" AutoGenerateColumns="False" DataKeyNames="CheckID"
                                            OnRowDataBound="gvCheck_RowDataBound" PageSize="10" EmptyDataText="暂时没有登记的支票"
                                            PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvCheck_PageIndexChanging"
                                            OnRowCommand="gvCheck_RowCommand" AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="CheckID" HeaderText="CheckID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CheckStatus" HeaderText="CheckStatus" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CheckSysCode" HeaderText="支票号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="CheckCode" HeaderText="支票序号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="登记日期" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="CreatorEmployeeName" HeaderText="登记人" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="支票状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlStatus">
                                                            <asp:ListItem Text="可用" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="已使用" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="作废" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="设置" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSave" runat="server" CommandArgument='<%# Eval("CheckID") %>'
                                                            CommandName="Save" Text="<img src='../../images/edit.gif' title='设置' border='0'>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
