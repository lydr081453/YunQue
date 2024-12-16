<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectPercent.ascx.cs"
    Inherits="UserControls_Project_ProjectPercent" %>
<script type="text/javascript">
    function selectPercent() {
        var year = "<%=BeginYear %>";
        var month = "<%=BeginMonth %>";
        var monthAmount = "<%=MonthAmount %>";
        var win = window.open('/Dialogs/PercentForProjectDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.Percent %>=' + monthAmount + '&<% =ESP.Finance.Utility.RequestName.BeginYear %>=' + year + '&<% =ESP.Finance.Utility.RequestName.BeginMonth %>=' + month, null, 'height=600px,width=800px,scrollbars=yes,top=400px,left=400px');
        win.resizeTo(screen.availWidth * 0.5, screen.availHeight * 0.8);
        return false;
    }
</script>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <table width="100%" class="tableForm">
            <tr>
                <td class="heading" colspan="4">预计各月完工百分比
                </td>
            </tr>
            <tr id="trPercent">
                <td class="oddrow" colspan="2">
                    <asp:Button ID="btnAddPercent" runat="server" OnClientClick="return selectPercent();"
                        Text="编辑各月完成百分比" CssClass="widebuttons"></asp:Button>
                    &nbsp;<font color="red">*</font>
                    <asp:LinkButton runat="server" ID="btnPercent" OnClick="btnPercent_Click" />
                </td>
            </tr>
            <tr id="trNoRecord" runat="server" visible="false">
                <td colspan="4">
                    <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                        <tr class="Gheading" align="center">
                            <th scope="col">序号
                            </th>
                            <th scope="col">年
                            </th>
                            <th scope="col">月
                            </th>
                            <th scope="col">完工百分比(%)
                            </th>
                            <th scope="col">当月Fee
                            </th>
                            <th scope="col">当月Fee(含税)
                            </th>
                        </tr>
                        <tr class="td" align="left">
                            <td colspan="6" align="center">
                                <span>没有填写预计完工百分比信息</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trGrid" runat="server" visible="true">
                <td colspan="4">
                    <asp:GridView ID="gvPercent" Width="100%" runat="server" DataKeyNames="ScheduleID"
                        AutoGenerateColumns="false" EmptyDataText="没有填写预计完工百分比信息" OnRowDataBound="gvPercent_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="YearValue" HeaderText="年" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:BoundField DataField="monthValue" HeaderText="月" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:TemplateField ItemStyle-Width="20%" HeaderText="完工百分比(%)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPercent" runat="server" Text='<%#Eval("MonthPercent") == null ? "0" : Convert.ToDecimal(Eval("MonthPercent")).ToString("0.00") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblFee" runat="server" Text='<%#Eval("Fee") == null ? "0" : Convert.ToDecimal(Eval("Fee")).ToString("#,##0.00")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee(含税)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblFeeTax" runat="server" Text=''></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trTotal" runat="server">
                <td class="oddrow-l" colspan="4" align="right">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 5%; border: 0 0 0 0"></td>
                            <td style="width: 10%; border: 0 0 0 0"></td>
                            <td style="width: 10%; border: 0 0 0 0"></td>
                            <td style="width: 20%; border: 0 0 0 0"></td>
                            <td style="width: 20%; border: 0 0 0 0">
                                <asp:Label ID="lblTotalPercent" runat="server" Style="text-align: right" Width="100%" />
                            </td>
                            <td style="width: 20%; border: 0 0 0 0" align="right">
                                <asp:Label ID="lblTotalFee" runat="server" Style="text-align: right" Width="100%" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
