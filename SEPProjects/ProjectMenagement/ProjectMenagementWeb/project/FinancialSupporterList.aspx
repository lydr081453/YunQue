<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_FinancialSupporterList" CodeBehind="FinancialSupporterList.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>


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
                                    <td class="oddrow" colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons" OnClick="btnSearchAll_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                        支持方申请列表
                                    </td>
                                </tr>
                                <tr id="trNoRecord" runat="server" visible="false">
                                    <td colspan="4" align="center">
                                        暂时没有相关记录.
                                    </td>
                                </tr>
                                <tr id="trSource" runat="server">
                                    <td class="oddrow">
                                        <asp:GridView ID="gridList" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportID"
                                            OnRowDataBound="gridList_RowDataBound" PageSize="20" AllowPaging="True" Width="100%"
                                            EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gridList_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="SupportID" HeaderText="SupportID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="SupporterCode" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                    <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="19%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("LeaderEmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="支持方费用">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBudgetAllocation" runat="server" Text='<%# Eval("BudgetAllocation")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IncomeType" HeaderText="费用类型" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:TemplateField HeaderText="打印预览" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <a target="_blank" href="SupporterPrint.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>&<% =ESP.Finance.Utility.RequestName.SupportID %>=<%#DataBinder.Eval(Container.DataItem,"SupportID")%>">
                                                            <img src="/images/SupporterPrint.gif" border="0px;" title="打印预览"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <a href="ProjectWorkFlow.aspx?Type=supporter&FlowID=<%#Eval("SupportID") %>" target="_blank">
                                                            <img src="/images/AuditStatus.gif" border="0px;" title="审批状态"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="使用状态" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# ESP.Finance.Utility.Common.ProjectInUse_Names[ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(Eval("ProjectId").ToString())).InUse] %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审批" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                            <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="审批支持方申请单"></asp:HyperLink>
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
    </table>
</asp:Content>
