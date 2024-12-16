<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="Return_PaymentNotifyList" CodeBehind="PaymentNotifyList.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
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
                                    <%--           <td class="oddrow" style="width: 15%">
                            通知状态:
                        </td>--%>
                                    <td class="oddrow-l" colspan="2">
                                        <%--<asp:DropDownList runat="server" ID="ddlStatus">
                                <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                <asp:ListItem Text="已保存" Value="0"></asp:ListItem>
                                <asp:ListItem Text="已提交" Value="1"></asp:ListItem>
                                <asp:ListItem Text="总监已审批" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已挂账" Value="3"></asp:ListItem>
                                <asp:ListItem Text="已付款" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;--%>
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />
                                        <asp:Button runat="server" ID="btnExport" Text="导出" CssClass="widebuttons" OnClick="btnExport_Click" />
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
                                    <td class="heading" colspan="2">
                                        付款通知列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentID"
                                            OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="10"
                                            EmptyDataText="暂时没有付款通知记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="PaymentID" HeaderText="PaymentID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PaymentCode" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblResponser" CssClass="userLabel" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="付款内容" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPaymentContent" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="预计日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPaymentPreDate" Text='<%# Eval("PaymentPreDate") == null ? "" : DateTime.Parse(Eval("PaymentPreDate").ToString()).ToString("yyyy-MM-dd")  %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="预计金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPaymentBudget" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BranchCode" HeaderText="公司代码" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="PaymentTypeName" HeaderText="付款类型" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("PaymentStatus") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="使用状态" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# ESP.Finance.Utility.Common.ProjectInUse_Names[ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(Eval("ProjectId").ToString())).InUse] %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="编辑"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印预览" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href='NotificationPrint.aspx?<% =ESP.Finance.Utility.RequestName.PaymentID %>=<%#Eval("PaymentID")%>'
                                                            target="_blank">
                                                            <img title=" 付款通知打印预览" src="/images/PrintDefault.gif" border="0px;" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <a href="/project/ProjectWorkFlow.aspx?Type=payment&FlowID=<%#Eval("PaymentID") %>"
                                                            target="_blank">
                                                            <img src="/images/AuditStatus.gif" border="0px;" title="审批状态"></a>
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
