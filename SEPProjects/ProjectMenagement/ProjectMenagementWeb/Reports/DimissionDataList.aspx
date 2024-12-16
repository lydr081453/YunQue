<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="DimissionDataList.aspx.cs" Inherits="FinanceWeb.Reports.DimissionDataList" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                员工帐号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtKey" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />&nbsp;
                <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" Style="width: 50px" OnClick="btnExport_Click" />&nbsp;
                <asp:Button ID="btnCloseUser" runat="server" Text=" 封锁帐号 " OnClientClick="return confirm('确定要关闭该员工帐号么?');" OnClick="btnCloseUser_Click" CssClass="widebuttons" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                申请单信息
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    PageSize="10" EmptyDataText="暂时没有发票记录" PagerSettings-Mode="NumericFirstLast"
                    OnPageIndexChanging="gvG_PageIndexChanging" AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ReturnStatus" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Status" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ReturnCode" HeaderText="PN No." ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="prefee" HeaderText="PN申请金额" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="totalprice" HeaderText="PR申请金额" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="PN状态" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPnStatus" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PR状态" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPrStatus" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="id" HeaderText="PR 流水" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="prno" HeaderText="PR No" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDesc" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="requestor_group" HeaderText="业务组" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="12%" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                项目信息
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProject_RowDataBound"
                    PageSize="10" EmptyDataText="暂时没有发票记录" PagerSettings-Mode="NumericFirstLast"
                    OnPageIndexChanging="gvProject_PageIndexChanging" AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Status" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:BoundField DataField="applicantemployeename" HeaderText="负责人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="15%" />
                        <asp:BoundField DataField="groupname" HeaderText="业务组" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:BoundField DataField="businessdescription" HeaderText="项目描述" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="35%" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
