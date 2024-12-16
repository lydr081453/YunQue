<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="killForeGiftDetail.aspx.cs" Inherits="ForeGift_killForeGiftDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register src="../UserControls/ForeGift/ViewForeGift.ascx" tagname="ViewForeGift"
        tagprefix="uc1" %>
    <uc1:ViewForeGift ID="ViewForeGift" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                已抵押金申请列表
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="2">
                <asp:GridView ID="gvLog" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                     PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                    OnPageIndexChanging="gvLog_PageIndexChanging" AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("PRNO") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <%#Eval("ReturnCode")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="抵消金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("killPrice") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="起始时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# Eval("PReBeginDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("PReEndDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="抵押金描述" DataField="killRemark" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
        <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
