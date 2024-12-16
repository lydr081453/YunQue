<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_ProjectAuditedModifiy" CodeBehind="ProjectAuditedModifyList.aspx.cs" %>

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
                                        项目号列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                                            OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvG_RowDataBound"
                                            PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Step" HeaderText="Step" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CreatorID" HeaderText="CreatorID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ApplicantUserID" HeaderText="ApplicantUserID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SerialCode" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="11%" />
                                                <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("ApplicantEmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CreateDate" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="BusinessTypeName" HeaderText="业务类型" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="ProjectTypeName" HeaderText="项目类型" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="ContractStatusName" HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labState" Text='<%#Eval("Status") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="使用状态" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# ESP.Finance.Utility.Common.ProjectInUse_Names[int.Parse(Eval("inUse").ToString())] %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="变更" ItemStyle-Width="3%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="变更"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印预览" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href='ProjectPrint.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>'
                                                            target="_blank">
                                                            <img title="项目号申请单打印预览" src="/images/ProjectPrint.gif" border="0px;" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="历史" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <a href='ProjectHist.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>'
                                                            target="_blank">
                                                            <img title="项目号历史" src="/images/history.gif" border="0px;" /></a>
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
