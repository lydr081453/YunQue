<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_SupporterList" CodeBehind="SupporterList.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>

    <table style="width: 100%">
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
                            <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                OnClick="btnSearchAll_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gridList" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportID"
                                OnRowDataBound="gridList_RowDataBound" PageSize="20" AllowPaging="True" Width="100%"
                                EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gridList_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="SupportID" HeaderText="SupportID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ProjectID" HeaderText="PrjID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%" />
                                   <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("LeaderEmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="支持方费用">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBudgetAllocation" runat="server" Text='<%# Eval("BudgetAllocation")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SupporterCode" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="IncomeType" HeaderText="费用类型" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="使用状态" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# ESP.Finance.Utility.Common.ProjectInUse_Names[ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(Eval("ProjectId").ToString())).InUse] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <a  target="_blank" href="SupporterDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>&<% =ESP.Finance.Utility.RequestName.SupportID %>=<%#DataBinder.Eval(Container.DataItem,"SupportID")%>">
                                                <img src="../images/dc.gif" border="0px;" title="查看"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                                <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="编辑"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印预览" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <a target="_blank" href="SupporterPrint.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>&<% =ESP.Finance.Utility.RequestName.SupportID %>=<%#DataBinder.Eval(Container.DataItem,"SupportID")%>">
                                                <img src="/images/SupporterPrint.gif" border="0px;" title="打印预览"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="testID" style="display: none">
        <div id="wl111" style="border-right: 1px inset; border-top: 1px inset; overflow: auto;
            border-left: 1px inset; width: 400px; border-bottom: 1px inset; height: 300px;
            background-color: white">
        </div>
    </div>
</asp:Content>
