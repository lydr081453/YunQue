<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Dialogs_AreaDlg" Title="地区选择" Codebehind="AreaDlg.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                地区查询
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                关键字：
                <asp:TextBox ID="txtCode" runat="server"/>
                <asp:Button ID="btnOK" runat="server" CssClass="widebuttons" Text=" 检索 " OnClick="btnOK_Click" />&nbsp;
                <asp:Button ID="btnClean" runat="server" CssClass="widebuttons" Text=" 重新搜索 " OnClick="btnClean_Click" />
            </td>
            <tr>
                <td colspan="4" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                    padding-top: 4px;">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvArea" runat="server" AutoGenerateColumns="False" DataKeyNames="AreaID"
                                    OnRowCommand="gvArea_RowCommand" OnPageIndexChanging="gvArea_PageIndexChanging"
                                    OnRowDataBound="gvArea_RowDataBound"  EmptyDataText="暂时没有地区信息"
                                    AllowPaging="false" Width="100%">
                                    <Columns>
                                        <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="AreaID" HeaderText="AreaID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="AreaCode" HeaderText="地区代码" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="AreaName" HeaderText="地区名称" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="SearchCode" HeaderText="查询代码" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="Description" HeaderText="地区描述" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Others" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                    </Columns>
                                    <PagerStyle  Font-Size="X-Large" ForeColor="SteelBlue"/>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
</asp:Content>
