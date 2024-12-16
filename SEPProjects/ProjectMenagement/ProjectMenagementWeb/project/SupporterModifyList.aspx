<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_SupporterModifyList" CodeBehind="SupporterModifyList.aspx.cs" %>

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
                                        支持方列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportID"
                                            OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvG_RowDataBound"
                                            PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="SupportID" HeaderText="SupportID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SupporterCode" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="16%" />
                                                <asp:BoundField DataField="LeaderEmployeeName" HeaderText="负责人" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="5%" />
                                                  <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ApplicantDate" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="ServiceType" HeaderText="服务类型" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="申请状态" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labState" Text='<%#Eval("Status") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <a  target="_blank" href="SupporterDisplay.aspx?<% =ESP.Finance.Utility.RequestName.SupportID %>=<%#DataBinder.Eval(Container.DataItem,"SupportID")%>&<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>">
                                                            <img src="../images/dc.gif" border="0px;" title="查看"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="使用状态" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# ESP.Finance.Utility.Common.ProjectInUse_Names[ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(int.Parse(Eval("ProjectId").ToString())).InUse] %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="编辑">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="编辑"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印预览" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href='SupporterPrint.aspx?<% =ESP.Finance.Utility.RequestName.SupportID %>=<%#DataBinder.Eval(Container.DataItem,"SupportID")%>'
                                                            target="_blank">
                                                            <img title="项目号申请单打印预览" src="/images/SupporterPrint.gif" border="0px;" /></a>
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
