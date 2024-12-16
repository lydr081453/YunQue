<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="SupportHist.aspx.cs" Inherits="FinanceWeb.project.SupportHist" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>
    
    
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            支持方项目号申请
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            所支持之项目号:
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="lblPrjCode" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            项目组别:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblGroup" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            由客户支付之税金:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="txtBilledTax" Text="0.00"></asp:Label>
        </td>
        <td class="oddrow">
            项目经理:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblLeaderEmployeeName" runat="server" CssClass="userLabel"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            服务费收入:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="txtIncomeFee" Text="0.00"></asp:Label>
        </td>
        <td class="oddrow">
            业务总额:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblBudgetAllocation" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            成本合计:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="lblTotal" Text="0.00"></asp:Label>
        </td>
        <td class="oddrow">
            服务类型:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblServiceType" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            业务描述:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="lblServiceDescription" Width="80%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            项目组成员
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="4">
            <input type="hidden" id="hidMembers" runat="server" />
            <asp:GridView ID="gvMember" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportMemberId"
                OnRowDataBound="gvMember_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField DataField="SupportMemberId" HeaderText="SupportMemberId" ItemStyle-HorizontalAlign="Center"
                        Visible="false" />
                    <asp:BoundField DataField="MemberUserID" HeaderText="系统ID" ItemStyle-HorizontalAlign="Center"
                        Visible="false" />
                    <asp:TemplateField HeaderText="真实姓名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("MemberEmployeeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MemberCode" HeaderText="成员编号" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="15%" />
                    <asp:BoundField DataField="MemberUserName" HeaderText="成员帐号" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="10%" />
                    <asp:BoundField DataField="GroupID" HeaderText="组ID" ItemStyle-HorizontalAlign="Center"
                        Visible="false" />
                    <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MemberEmail" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="17%" />
                    <asp:TemplateField ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderText="电话">
                        <ItemTemplate>
                            <asp:Label ID="lblMemberPhone" runat="server" Text='<%# Eval("MemberPhone")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Visible="false" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            预计完成百分比
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            支持方起始日期:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                onclick="setDate(this);" />
        </td>
        <td class="oddrow">
            支持方预计结束日期:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                onclick="setDate(this);" />
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            成本明细
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="4">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportCostId"
                        Width="100%" OnRowDataBound="gvCost_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40%" />
                            <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" DataKeyNames="SupporterExpenseID"
                        Width="100%" OnRowDataBound="gvExpense_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="OOP成本描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40%" />
                            <asp:TemplateField HeaderText="OOP成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpense" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="trTotalCost" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 40%; border: 0 0 0 0">
                    </td>
                    <td style="width: 25%; border: 0 0 0 0">
                    </td>
                    <td style="width: 25%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotalCost" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            各月完工百分比
        </td>
    </tr>
    <tr id="trNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">
                        序号
                    </th>
                    <th scope="col">
                        年
                    </th>
                    <th scope="col">
                        月
                    </th>
                    <th scope="col">
                        完工百分比(%)
                    </th>
                    <th scope="col">
                        当月Fee
                    </th>
                       <th scope="col">
                        当月Fee(含税)
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
            <asp:GridView ID="gvPercent" Width="100%" runat="server" DataKeyNames="SupporterScheduleID"
                AutoGenerateColumns="false" EmptyDataText="没有填写预计完工百分比信息" OnRowDataBound="gvPercent_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="YearValue" HeaderText="年" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:BoundField DataField="monthValue" HeaderText="月" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="完工百分比(%)" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPercent" runat="server" Text='<%#Eval("MonthPercent") == null ? "0.00" : Convert.ToDecimal(Eval("MonthPercent")).ToString("0.00") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblFee" runat="server" Text='<%#Eval("Fee") == null ? "0.00" : Convert.ToDecimal(Eval("Fee")).ToString("#,##0.00")%>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee(含税)" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblFeeTax" runat="server" Text=''></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 5%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
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

 <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" align="right">
                <asp:Label Font-Size="XX-Small" ForeColor="SteelBlue" ID="lblRepresent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnFirst" runat="server" Text="  |<  " class="widebuttons" OnClick="btnFirst_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnPret" runat="server" Text=" <<< " class="widebuttons" OnClick="btnPret_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNext" runat="server" Text=" >>> " class="widebuttons" OnClick="btnNext_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnLast" runat="server" Text="  >|  " class="widebuttons" OnClick="btnLast_Click" />
            </td>
        </tr>
        <tr>
        <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " class="widebuttons" OnClientClick="window.close();" />
            </td>
        </tr>
    </table>

</asp:Content>